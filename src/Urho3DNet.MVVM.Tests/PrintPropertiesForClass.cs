using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace Urho3DNet.MVVM.Tests
{
    [TestFixture]
    public class PrintPropertiesForClass
    {
        [Test]
        public void Gen()
        {
            var visited = new HashSet<Type>();
            visited.Add(typeof(Urho3DNet.Object));
            foreach (var type in typeof(UIElement).Assembly.GetTypes()
                .Where(_ => typeof(UIElement).IsAssignableFrom(_)))
            {
                var t = type;
                while (visited.Add(t))
                {
                    System.Console.WriteLine(
                        $"PrintPropertiesForClass(\"{t.Name}\", \"{t.BaseType.Name}\", new Tuple<string,Type>[]{{");
                    foreach (var propertyInfo in t.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                    {
                        if (propertyInfo.DeclaringType != t)
                            continue;
                        if (!propertyInfo.CanWrite)
                            continue;
                        string typeName = propertyInfo.PropertyType.Name;
                        switch (typeName)
                        {
                            case "Boolean":
                                typeName = "bool";
                                break;
                            case "String":
                                typeName = "string";
                                break;
                            case "Int32":
                                typeName = "int";
                                break;
                            case "UInt32":
                                typeName = "uint";
                                break;
                            case "Single":
                                typeName = "float";
                                break;
                            case "Color":
                                typeName = "Color";
                                break;
                            case "Vector2":
                                typeName = "Vector2";
                                break;
                            case "IntVector2":
                                typeName = "IntVector2";
                                break;
                            case "TraversalMode":
                                typeName = "TraversalMode";
                                break;
                            case "IntRect":
                                typeName = "IntRect";
                                break;
                            case "HorizontalAlignment":
                                typeName = "HorizontalAlignment";
                                break;
                            case "VerticalAlignment":
                                typeName = "VerticalAlignment";
                                break;
                            case "FocusMode":
                                typeName = "FocusMode";
                                break;
                            case "DragAndDropMode":
                                typeName = "DragAndDropMode";
                                break;
                            case "LayoutMode":
                                typeName = "LayoutMode";
                                break;
                            case "Orientation":
                                typeName = "Orientation";
                                break;
                            case "BlendMode":
                                typeName = "BlendMode";
                                break;
                            case "Material":
                                typeName = "Material";
                                break;
                            default:
                                System.Console.Write("//");
                                break;
                        }

                        System.Console.WriteLine($"    Tuple.Create(\"{propertyInfo.Name}\", typeof({typeName})),");
                    }

                    System.Console.WriteLine($"}});");

                    t = t.BaseType;
                }
            }
            //foreach (var ease in typeof(Urho3DNet.UIElement).Assembly.GetTypes())
            //{
            //    foreach (var constructor in ease.GetConstructors())
            //    {
            //        var args = constructor.GetParameters();
            //        var parameters = (new string[] { "this ActionBuilder<Node> builder" }).Concat(args.Select(_ => $"{_.ParameterType.Name} {_.Name}"));
            //        var names = args.Select(_ => $"{_.Name}");
            //        System.Console.WriteLine($" public static ActionBuilder<Node> {ease.Name}({string.Join(", ", parameters)}) => builder.Add(new {ease.Name}({string.Join(", ", names)}));");
            //    }
            //}
        }
    }
}
