using Urho3DNet.InputEvents;

namespace Urho3DNet.Samples
{
    public class FreeCameraSample : Sample
    {
        private readonly FreeCameraController _cameraController;

        public FreeCameraSample(Context context) : base(context)
        {
            var scene = new Scene(Context);
            scene.CreateComponent<Octree>();
            DefaultFogColor = Color.Blue;
            var teapotNode = scene.CreateChild("Teapot");
            var teapotModel = teapotNode.CreateComponent<StaticModel>();
            teapotModel.SetModel(ResourceCache.GetResource<Model>("Models/Teapot.mdl"));
            var cameraNode = scene.CreateChild("Camera");
            cameraNode.Position = new Vector3(0, 0, -10);
            var camera = cameraNode.CreateComponent<Camera>();
            SetViewport(0, camera);
            _cameraController = new FreeCameraController(camera);
            FallbackInputListener = _cameraController;
        }

        public override void OnKeyboardButtonDown(object sender, KeyEventArgs args)
        {
            switch (args.Key)
            {
                case UniKey.KeyF4:
                    if (MouseMode == MouseMode.MmFree)
                    {
                        MouseMode = MouseMode.MmAbsolute;
                        IsMouseVisible = false;
                    }
                    else
                    {
                        MouseMode = MouseMode.MmFree;
                        IsMouseVisible = true;
                    }
                    return;
            }
            base.OnKeyboardButtonDown(sender, args);
        }

        protected override void Dispose(bool disposing)
        {
            FallbackInputListener = null;
            _cameraController.Dispose();
            base.Dispose(disposing);
        }
    }
}