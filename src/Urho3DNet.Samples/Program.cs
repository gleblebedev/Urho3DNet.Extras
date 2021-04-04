using System;

namespace Urho3DNet.Samples
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Launcher.Run(_ => new SamplesManager(_));
        }
    }
}
