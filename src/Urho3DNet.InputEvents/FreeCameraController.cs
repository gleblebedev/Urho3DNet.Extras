using System;
using System.Collections.Generic;

namespace Urho3DNet.InputEvents
{
    public class FreeCameraController : AbstractInputListener, IDisposable
    {
        private readonly SharedPtr<Camera> _camera;

        private readonly SharedPtr<Object> _subscriptionObject;

        private readonly CoreEventsAdapter _coreEvents;
        private readonly KeyAction _forward = new KeyAction();
        private readonly KeyAction _backward = new KeyAction();
        private readonly KeyAction _left = new KeyAction();
        private readonly KeyAction _right = new KeyAction();
        private readonly KeyAction _fastMode = new KeyAction();
        private readonly KeyAction _panMode = new KeyAction();
        private readonly AxisAction _leftRight = new AxisAction();
        private readonly AxisAction _forwardBackward = new AxisAction();
        private readonly AxisAction _yaw = new AxisAction();
        private readonly AxisAction _pitch = new AxisAction();

        public FreeCameraController(Camera camera): this (camera.Context, camera)
        {

        }
        public FreeCameraController(Context context, Camera camera = null)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            var keyActionCollection = new KeyActionCollection<KeyActionType>
            {
                {KeyActionType.Forward, _forward},
                {KeyActionType.Backward, _backward},
                {KeyActionType.Left, _left},
                {KeyActionType.Right, _right},
                {KeyActionType.FastMode, _fastMode},
                {KeyActionType.PanMode, _panMode}
            };
            var keyMappings = new KeyActionMapping<KeyActionType>(keyActionCollection)
            {
                {UniKey.KeyW, KeyActionType.Forward},
                {UniKey.KeyS, KeyActionType.Backward},
                {UniKey.KeyA, KeyActionType.Left},
                {UniKey.KeyD, KeyActionType.Right},
                {UniKey.KeyShift, KeyActionType.FastMode},
                {UniKey.MouseButtonRight, KeyActionType.PanMode}
            };

            var axisMappings = new AxisActionMapping<AxisActionType>(new Dictionary<AxisActionType, IAxisAction>
            {
                {AxisActionType.LeftRight, _leftRight},
                {AxisActionType.ForwardBackward, _forwardBackward},
                {AxisActionType.Pitch, _pitch},
                {AxisActionType.Yaw, _yaw}
            })
            {
                {UniAxis.LeftX, AxisActionType.LeftRight},
                {UniAxis.LeftY, AxisActionType.ForwardBackward},
                {UniAxis.RightX, AxisActionType.Yaw},
                {UniAxis.RightY, AxisActionType.Pitch}
            };

            _camera = camera;
            Context = context;
            _subscriptionObject = new Object(context);
            _coreEvents = new CoreEventsAdapter(_subscriptionObject);
            _coreEvents.Update += UpdateCameraPosition;
            FallbackInputListener = new InputDemultiplexer(keyMappings, axisMappings);
        }

        private enum KeyActionType
        {
            Forward,
            Backward,
            Left,
            Right,
            FastMode,
            PanMode
        }

        private enum AxisActionType
        {
            LeftRight,
            ForwardBackward,
            Yaw,
            Pitch,
            Up,
            Down
        }

        public float FastCameraSpeed { get; set; } = 4;
        public float CameraSpeed { get; set; } = 1;
        public float MouseSensitivityX { get; set; } = 0.2f;
        public float MouseSensitivityY { get; set; } = 0.2f;
        public bool InvertMouse { get; set; } = false;
        public float MinPitch { get; set; } = -80f;
        public float MaxPitch { get; set; } = 80f;
        public Camera Camera
        {
            get { return _camera?.Value; }
            set { _camera.Value = value; }
        }

        public Scene Scene => Camera?.Scene;

        public Node CameraNode => Camera?.Node;

        public Context Context { get; private set; }

        public bool InvertGamepad { get; set; }

        public float GamepadSensitivityY { get; set; } = 1.0f;

        public float GamepadSensitivityX { get; set; } = 1.0f;

        private MouseMode MouseMode => Context.Input.GetMouseMode();

        public override void OnMousePointerMoved(object sender, PointerEventArgs args)
        {
            var mouseMode = MouseMode;
            switch (mouseMode)
            {
                case MouseMode.MmFree:
                    if (_panMode)
                    {
                        RotateCamera(args);
                        return;
                    }

                    break;
                case MouseMode.MmAbsolute:

                    RotateCamera(args);
                    return;
            }

            base.OnMousePointerMoved(sender, args);
        }

        public void Dispose()
        {
            _coreEvents.Update -= UpdateCameraPosition;
            _coreEvents.Dispose();
            _subscriptionObject.Dispose();
            _camera.Dispose();
        }

        private void UpdateCameraPosition(object sender, CoreEventsAdapter.UpdateEventArgs e)
        {
            var mouseMode = MouseMode;
            if (mouseMode == MouseMode.MmFree)
                if (!_panMode)
                    return;
            var cameraNode = _camera.Value?.Node;
            if (cameraNode == null)
                return;

            var direction = Vector3.Zero;
            var forward = cameraNode.LocalToWorld(new Vector4(Vector3.Forward, 0));
            var right = cameraNode.LocalToWorld(new Vector4(Vector3.Right, 0));
            float leftRightSpeed = 0;
            float forwardSpeed = 0;
            if (_forward) forwardSpeed -= 1;
            if (_backward) forwardSpeed += 1;
            if (_right) leftRightSpeed += 1;
            if (_left) leftRightSpeed -= 1;
            leftRightSpeed = _leftRight.MergeWith(leftRightSpeed);
            forwardSpeed = _forwardBackward.MergeWith(forwardSpeed);

            direction += forward * -forwardSpeed + right * leftRightSpeed;

            var cameraVelocity = direction * (_fastMode ? FastCameraSpeed : CameraSpeed);
            var cameraNodeParent = cameraNode.Parent;
            if (cameraNodeParent == null)
                cameraNode.Position += cameraVelocity * e.TimeStep;
            else
                cameraNode.Position += cameraNodeParent.WorldToLocal(new Vector4(cameraVelocity, 0)) * e.TimeStep;

            if (_yaw.Value != 0 || _pitch.Value != 0)
            {
                var rot = cameraNode.WorldRotation;
                var angles = rot.EulerAngles;

                angles.Y += _yaw * GamepadSensitivityX;
                angles.X = Clamp(angles.X + _pitch * GamepadSensitivityY * (InvertGamepad ? -1 : 1), MinPitch, MaxPitch);
                rot.FromEulerAngles(angles.X, angles.Y, 0);
                cameraNode.WorldRotation = rot;
            }
        }

        private void RotateCamera(PointerEventArgs args)
        {
            var node = CameraNode;
            if (node == null)
                return;
            var rot = node.WorldRotation;
            var angles = rot.EulerAngles;
            angles.Y += args.Dx * MouseSensitivityX;
            angles.X = Clamp(angles.X + args.Dy * MouseSensitivityY * (InvertMouse ? -1 : 1), MinPitch, MaxPitch);
            rot.FromEulerAngles(angles.X, angles.Y, 0);
            node.WorldRotation = rot;
        }

        private float Clamp(float val, float min, float max)
        {
            if (val < min)
                return min;
            if (val > max)
                return max;
            return val;
        }
    }
}