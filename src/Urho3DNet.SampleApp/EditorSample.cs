using System;
using Urho3DNet.Editor;
using Urho3DNet.Editor.Commands;
using Urho3DNet.Editor.Gizmos;
using Urho3DNet.InputEvents;

namespace Urho3DNet.Samples
{
    public class EditorSample:Sample
    {
        private readonly FreeCameraController _cameraController;
        private readonly Selection _selection = new Selection();
        private SelectGizmo _gizmo;
        private Camera _camera;
        private Object _subscription;
        private InputEventsAdapter _inputEventAdapter;
        private IGizmo _currentGizmo;
        private bool _useGizmo;
        private GizmoRaycast _raycast;
        private BoxGizmo _selectionBoundingBoxGizmo;
        private readonly UndoStack _undoStack = new UndoStack();
        private Scene _scene;
        private ViewportRay _screenRay;
        private GizmoMode _gizmoMode = GizmoMode.Local;

        public EditorSample(Context context) : base(context)
        {
            _scene = new Scene(Context);
            _scene.CreateComponent<Octree>();
            DefaultFogColor = Color.Blue;
            _subscription = new Object(context);

            var teapotNode = _scene.CreateChild("Teapot");
            var teapotModel = teapotNode.CreateComponent<StaticModel>();
            teapotModel.SetModel(ResourceCache.GetResource<Model>("Models/Teapot.mdl"));
            
            //_selection.Add(teapotNode);
            
            var cameraNode = _scene.CreateChild("Camera");
            cameraNode.Position = new Vector3(0, 0, -2);
            _camera = cameraNode.CreateComponent<Camera>();
            SetViewport(0, _camera);
            _cameraController = new FreeCameraController(_camera);
            FallbackInputListener = _cameraController;

            MouseMode = MouseMode.MmFree;
            IsMouseVisible = true;

            _gizmo = new SelectGizmo(Context);
            _gizmo.Add(new MoveGizmo(Context));
            _gizmo.Add(new RotateGizmo(Context));
            _gizmo.Add(new ScaleGizmo(Context));
            _gizmo.ResizeGizmo(_camera);

            _selection.SelectionChanged += OnSelectionChanged;

            _selectionBoundingBoxGizmo = new BoxGizmo(Context, 1.0f, Color.White, Color.White);
            //_selectionBoundingBoxGizmo.Show(_camera.Scene);
            //_selectionBoundingBoxGizmo.ResizeGizmo(_camera);

            _inputEventAdapter = new InputEventsAdapter(_subscription);
        }

        private void OnSelectionChanged(object sender, EventArgs e)
        {
            if (_selection.IsEmpty)
            {
                _gizmo.Hide();
            }
            else
            {
                _gizmo.Show(_scene);
                _gizmo.ResizeGizmo(_camera);
            }
        }

        public override void OnKeyboardButtonDown(object sender, KeyEventArgs args)
        {
            if (args.Buttons == 0)
            {
                switch (args.Key)
                {
                    case UniKey.KeyQ:
                        _gizmo.SelectedIndex = -1;
                        return;
                    case UniKey.KeyW:
                        _gizmo.SelectedIndex = 0;
                        return;
                    case UniKey.KeyE:
                        _gizmo.SelectedIndex = 1;
                        return;
                    case UniKey.KeyR:
                        _gizmo.SelectedIndex = 2;
                        return;
                    case UniKey.KeyS:
                        _undoStack.Push(new SnapCommand(_selection));
                        return;
                    case UniKey.KeyZ:
                        if (0 != (args.Qualifiers & Qualifier.QualCtrl))
                        {
                            var editorCommand = _undoStack.Pop();
                            if (editorCommand != null)
                            {
                                editorCommand.Undo();
                                editorCommand.Dispose();
                            }

                            return;
                        }
                        break;
                    case UniKey.KeyM:
                        _gizmoMode = (GizmoMode)(((int) _gizmoMode + 1) % 3);
                        break;
                }
            }

            base.OnKeyboardButtonDown(sender, args);
        }

        public override void OnMouseButtonDown(object sender, KeyEventArgs args)
        {
            switch (args.Key)
            {
                case UniKey.MouseButtonLeft:
                    if (_currentGizmo != null)
                    {
                        var editorCommand = _currentGizmo.Start(_selection);
                        if (editorCommand != null)
                        {
                            _undoStack.Push(editorCommand);
                            _useGizmo = true;
                            return;
                        }
                    }

                    SelectObject(args, 0 != (args.Qualifiers & Qualifier.QualCtrl));
                    break;
            }
            base.OnMouseButtonDown(sender, args);
        }

        private void SelectObject(KeyEventArgs args, bool toggle)
        {
            var octree = _scene.GetComponent<Octree>();
            var queryResultList = new RayQueryResultList();
            var rayOctreeQuery = new RayOctreeQuery(queryResultList, _screenRay.Ray, RayQueryLevel.RayTriangle, _camera.FarClip);
            octree.RaycastSingle(rayOctreeQuery);
            if (queryResultList.Count > 0)
            {
                var node = queryResultList[0]?.Node;
                if (toggle)
                    _selection.Toggle(node);
                else
                    _selection.Add(node);
            }
        }

        public override void OnMouseButtonCanceled(object sender, KeyEventArgs args)
        {
            switch (args.Key)
            {
                case UniKey.MouseButtonLeft:
                    if (_useGizmo)
                    {
                        _undoStack.Pop()?.Dispose();
                        _useGizmo = false;
                    }
                    break;
            }
            base.OnMouseButtonCanceled(sender, args);
        }

        public override void OnMouseButtonUp(object sender, KeyEventArgs args)
        {
            switch (args.Key)
            {
                case UniKey.MouseButtonLeft:
                    if (_useGizmo)
                    {
                        _useGizmo = false;
                    }
                    break;
            }
            base.OnMouseButtonUp(sender, args);
        }

        public override void OnMousePointerMoved(object sender, PointerEventArgs e)
        {
            var screenRay = GetScreenRay(e.X, e.Y);
            _screenRay = screenRay;
            if (screenRay.Viewport != null)
            {

                _raycast = new GizmoRaycast()
                {
                    ViewProj = screenRay.Viewport.Camera.ViewProj,
                    Origin = screenRay.Ray.Origin,
                    Direction = screenRay.Ray.Direction,
                    Contact = screenRay.Ray.Origin + screenRay.Ray.Direction * _camera.FarClip,
                    Threshold = 0.01f,
                };

                if (_useGizmo)
                {
                    _currentGizmo.Preview(ref _raycast);
                }
                else
                {
                    _gizmo.Raycast(ref _raycast);
                    var gizmo = _raycast.Gizmo;
                    //_selectionBoundingBoxGizmo.Position = _raycast.Contact;
                    if (gizmo != _currentGizmo)
                    {
                        _currentGizmo?.Highlight(false);
                        _currentGizmo = gizmo;
                        _currentGizmo?.Highlight(true);
                    }
                }
            }
                
            base.OnMousePointerMoved(sender, e);
        }

        public override void OnUpdate(CoreEventsAdapter.UpdateEventArgs arg)
        {
            _gizmo.Node.WorldPosition = _selection.GetWorldPosition();
            switch (_gizmoMode)
            {
                case GizmoMode.Local:
                    _gizmo.Node.Rotation = _selection.GetWorldRotation();
                    break;
                case GizmoMode.World:
                    _gizmo.Node.Rotation = Quaternion.IDENTITY;
                    break;
                case GizmoMode.Parent:
                    _gizmo.Node.Rotation = _selection.GetParentRotation();
                    break;
            }
            _gizmo.ResizeGizmo(_camera);
            if (_selection.TryGetBoundingBox(out var bbox))
            {
                _selectionBoundingBoxGizmo.Show(_scene);
                _selectionBoundingBoxGizmo.Position = bbox.Center;
                _selectionBoundingBoxGizmo.Scale = bbox.Size;
            }
            else
            {
                _selectionBoundingBoxGizmo.Hide();
            }
            //_selectionBoundingBoxGizmo.ResizeGizmo(_camera);
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