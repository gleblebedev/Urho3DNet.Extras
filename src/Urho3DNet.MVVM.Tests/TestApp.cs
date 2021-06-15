using System;

namespace Urho3DNet.MVVM.Tests
{
    public class TestApp : Urho3DNet.Application
    {
        private readonly Action<Context> _testCode;

        public TestApp(Context context, Action<Context> testCode) : base(context)
        {
            _testCode = testCode;
        }

        public override void Setup()
        {
            EngineParameters[Urho3D.EpHeadless] = true;
            base.Setup();
        }

        public override void Start()
        {
            _testCode(Context);
            base.Start();
            ExitCode = 1;
        }

        public static int Launch(Action<Context> testCode)
        {
            using (Context context = new Context())
            {
                using (TestApp application = new TestApp(context, testCode))
                {
                    return application.Run();
                }
            }
        }
    }
}