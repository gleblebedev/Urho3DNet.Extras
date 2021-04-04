using System;
using System.Linq;
using NUnit.Framework;

namespace Urho3DNet.Actions.Tests
{
    [TestFixture]
    public class EaseTestFixture
    {
        [Test]
        public void Gen()
        {
            foreach (var ease in typeof(EaseBackIn).Assembly.GetTypes().Where(_ => IsFiniteTimeAction(_))) //.Where(_ => _.Name.StartsWith("Ease"))
            {
                foreach (var constructor in ease.GetConstructors())
                {
                    var args = constructor.GetParameters();
                    var parameters = (new string[] { "this ActionBuilder<Node> builder" }).Concat(args.Select(_ => $"{_.ParameterType.Name} {_.Name}"));
                    var names = args.Select(_=>$"{_.Name}");
                    System.Console.WriteLine($" public static ActionBuilder<Node> {ease.Name}({string.Join(", ", parameters)}) => builder.Add(new {ease.Name}({string.Join(", ", names)}));");
                }
            }
        }

        private bool IsFiniteTimeAction(Type type)
        {
            return typeof(FiniteTimeAction).IsAssignableFrom(type);
        }
    }
}
