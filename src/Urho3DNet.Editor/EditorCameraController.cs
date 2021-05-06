using System;
using Urho3DNet.Editor.Commands;
using Urho3DNet.Editor.Gizmos;
using Urho3DNet.InputEvents;

namespace Urho3DNet.Editor
{
    public class EditorCameraController : AbstractInputListener, IDisposable
    {
        private FreeCameraController _cameraController;
        private readonly Selection _selection = new Selection();
        private readonly UndoStack _undoStack = new UndoStack();
        private SelectGizmo _gizmo;
        private BoxGizmo _selectionBoundingBoxGizmo;
        private Scene _scene;
        private Camera _camera;
        private GizmoMode _gizmoMode = GizmoMode.Local;
        private IGizmo _currentGizmo;
        private Ray _screenRay;
        private bool _useGizmo;
        private Viewport _viewport;
        private Context _context;
        private GizmoRaycast _raycast;
        private readonly SharedPtr<Object> _subscriptionObject;
        private CoreEventsAdapter _coreEvents;

        public EditorCameraController(Viewport viewport)
        {
            _viewport = viewport;
            _camera = viewport.Camera;
            _scene = viewport.Scene;
            _context = viewport.Context;
            _cameraController = new FreeCameraController(_context, _camera);
            FallbackInputListener = _cameraController;

            _gizmo = new SelectGizmo(_context);
            _gizmo.Add(new MoveGizmo(_context));
            _gizmo.Add(new RotateGizmo(_context));
            _gizmo.Add(new ScaleGizmo(_context));
            _gizmo.ResizeGizmo(_camera);

            _selection.SelectionChanged += OnSelectionChanged;

            _selectionBoundingBoxGizmo = new BoxGizmo(_context, 1.0f, Color.White, Color.White);

            _subscriptionObject = new Object(_context);
            _coreEvents = new CoreEventsAdapter(_subscriptionObject);
            _coreEvents.Update += OnUpdate;

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
                        _gizmoMode = (GizmoMode)(((int)_gizmoMode + 1) % 3);
                        return;
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
            var rayOctreeQuery = new RayOctreeQuery(queryResultList, _screenRay, RayQueryLevel.RayTriangle, _camera.FarClip);
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
        public Ray GetScreenRay(int x, int y)
        {
            var viewport = _viewport;
            var view = viewport?.View;
            if (view != null)
            {
                var rect = viewport.View.ViewRect;
                if (rect.Min.X <= x && rect.Max.X >= x && rect.Min.Y <= y && rect.Max.Y >= y)
                    return viewport.Camera.GetScreenRay((x - rect.Min.X) / (float) rect.Width,
                        (y - rect.Min.Y) / (float) rect.Height);
            }

            return new Ray();
        }
        public override void OnMousePointerMoved(object sender, PointerEventArgs e)
        {
            var screenRay = GetScreenRay(e.X, e.Y);
            _screenRay = screenRay;
            if (screenRay.Direction != Vector3.Zero)
            {
                _raycast = new GizmoRaycast()
                {
                    ViewProj = _camera.ViewProj,
                    Origin = screenRay.Origin,
                    Direction = screenRay.Direction,
                    Contact = screenRay.Origin + screenRay.Direction * _camera.FarClip,
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

        public void OnUpdate(object sender, CoreEventsAdapter.UpdateEventArgs arg)
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
        }
        public void Dispose()
        {
            FallbackInputListener = null;
            _cameraController.Dispose();
            _coreEvents.Dispose();
            _subscriptionObject.Dispose();
        }
    }
}