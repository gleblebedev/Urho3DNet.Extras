using Urho3DNet.Editor;
using Urho3DNet.InputEvents;

namespace Urho3DNet.Samples
{
    public class EditorSample:Sample
    {
        private readonly FreeCameraController _cameraController;
        private MoveGizmo _moveGizmo;
        private Camera _camera;
        private Object _subscription;
        private InputEventsAdapter _inputEventAdapter;
        private IGizmo _currentGizmo;

        public EditorSample(Context context) : base(context)
        {
            var scene = new Scene(Context);
            scene.CreateComponent<Octree>();
            DefaultFogColor = Color.Blue;
            _subscription = new Object(context);

            var teapotNode = scene.CreateChild("Teapot");
            var teapotModel = teapotNode.CreateComponent<StaticModel>();
            teapotModel.SetModel(ResourceCache.GetResource<Model>("Models/Teapot.mdl"));
            
            var cameraNode = scene.CreateChild("Camera");
            cameraNode.Position = new Vector3(0, 0, -2);
            _camera = cameraNode.CreateComponent<Camera>();
            SetViewport(0, _camera);
            _cameraController = new FreeCameraController(_camera);
            FallbackInputListener = _cameraController;

            MouseMode = MouseMode.MmFree;
            IsMouseVisible = true;

            _moveGizmo = new MoveGizmo(Context);
            _moveGizmo.Show(_camera);

            _inputEventAdapter = new InputEventsAdapter(_subscription);
            _inputEventAdapter.MouseMove += HandleMouseMove;
        }

        private void HandleMouseMove(object sender, InputEventsAdapter.MouseMoveEventArgs e)
        {
            var screenRay = GetScreenRay(e.X, e.Y);

            var raycast = new GizmoRaycast()
            {
                ViewProj = screenRay.Viewport.Camera.ViewProj,
                Origin = screenRay.Ray.Origin,
                Direction = screenRay.Ray.Direction,
                Contact = screenRay.Ray.Origin + screenRay.Ray.Direction*_camera.FarClip,
                Threshold = 0.01f,
            };
            _moveGizmo.Raycast(ref raycast);
            var gizmo = raycast.Gizmo;
            if (gizmo != _currentGizmo)
            {
                _currentGizmo?.Select(false);
                _currentGizmo = gizmo;
                _currentGizmo?.Select(true);
            }
        }

        public override void OnUpdate(CoreEventsAdapter.UpdateEventArgs arg)
        {
            _moveGizmo.ResizeGizmo(_camera);
            base.OnUpdate(arg);
        }

        protected override void Dispose(bool disposing)
        {
            FallbackInputListener = null;
            _cameraController.Dispose();
            base.Dispose(disposing);
        }
    }
}