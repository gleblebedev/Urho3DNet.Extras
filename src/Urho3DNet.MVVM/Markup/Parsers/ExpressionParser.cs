using System;
using Urho.Data.Core;
using Urho3DNet.MVVM.Binding;
using Urho3DNet.MVVM.Controls;
using Urho3DNet.MVVM.Data.Core;
using Urho3DNet.MVVM.Markup.Parsers.Nodes;
using Urho3DNet.MVVM.Utilities;

namespace Urho3DNet.MVVM.Markup.Parsers
{
    internal class ExpressionParser
    {
        private readonly bool _enableValidation;
        private readonly Func<string, string, Type>? _typeResolver;
        private readonly INameScope? _nameScope;

        public ExpressionParser(bool enableValidation, Func<string, string, Type>? typeResolver, INameScope? nameScope)
        {
            _typeResolver = typeResolver;
            _nameScope = nameScope;
            _enableValidation = enableValidation;
        }

        public (ExpressionNode? Node, SourceMode Mode) Parse(ref CharacterReader r)
        {
            ExpressionNode? rootNode = null;
            ExpressionNode? node = null;
            var (astNodes, mode) = BindingExpressionGrammar.Parse(ref r);

            foreach (var astNode in astNodes)
            {
                ExpressionNode? nextNode = null;
                switch (astNode)
                {
                    case BindingExpressionGrammar.EmptyExpressionNode _:
                        nextNode = new EmptyExpressionNode();
                        break;
                    case BindingExpressionGrammar.NotNode _:
                        nextNode = new LogicalNotNode();
                        break;
                    case BindingExpressionGrammar.StreamNode _:
                        nextNode = new StreamNode();
                        break;
                    case BindingExpressionGrammar.PropertyNameNode propName:
                        nextNode = new PropertyAccessorNode(propName.PropertyName, _enableValidation);
                        break;
                    case BindingExpressionGrammar.IndexerNode indexer:
                        nextNode = new StringIndexerNode(indexer.Arguments);
                        break;
                    case BindingExpressionGrammar.AttachedPropertyNameNode attachedProp:
                        nextNode = ParseAttachedProperty(attachedProp);
                        break;
                    case BindingExpressionGrammar.SelfNode _:
                        nextNode = new SelfNode();
                        break;
                    case BindingExpressionGrammar.AncestorNode ancestor:
                        nextNode = ParseFindAncestor(ancestor);
                        break;
                    case BindingExpressionGrammar.NameNode elementName:
                        nextNode = new ElementNameNode(_nameScope ?? throw new NotSupportedException("Invalid element name binding with null name scope!"), elementName.Name);
                        break;
                    case BindingExpressionGrammar.TypeCastNode typeCast:
                        nextNode = ParseTypeCastNode(typeCast);
                        break;
                }
                if (node is null)
                {
                    rootNode = node = nextNode;
                }
                else
                {
                    node.Next = nextNode;
                    node = nextNode;
                }
            }

            return (rootNode, mode);
        }

        private FindAncestorNode ParseFindAncestor(BindingExpressionGrammar.AncestorNode node)
        {
            Type? ancestorType = null;
            var ancestorLevel = node.Level;

            if (!(node.Namespace is null) && !(node.TypeName is null))
            {
                if (_typeResolver == null)
                {
                    throw new InvalidOperationException("Cannot parse a binding path with a typed FindAncestor without a type resolver. Maybe you can use a LINQ Expression binding path instead?");
                }

                ancestorType = _typeResolver(node.Namespace, node.TypeName);
            }

            return new FindAncestorNode(ancestorType, ancestorLevel);
        }

        private TypeCastNode ParseTypeCastNode(BindingExpressionGrammar.TypeCastNode node)
        {
            Type? castType = null;
            if (!(node.Namespace is null) && !(node.TypeName is null))
            {
                if (_typeResolver == null)
                {
                    throw new InvalidOperationException("Cannot parse a binding path with a typed Cast without a type resolver. Maybe you can use a LINQ Expression binding path instead?");
                }

                castType = _typeResolver(node.Namespace, node.TypeName);
            }

            return new TypeCastNode(castType);
        }

        private UrhoPropertyAccessorNode ParseAttachedProperty(BindingExpressionGrammar.AttachedPropertyNameNode node)
        {
            if (_typeResolver == null)
            {
                throw new InvalidOperationException("Cannot parse a binding path with an attached property without a type resolver. Maybe you can use a LINQ Expression binding path instead?");
            }

            var property = UrhoPropertyRegistry.Instance.FindRegistered(_typeResolver(node.Namespace, node.TypeName), node.PropertyName);

            return new UrhoPropertyAccessorNode(property, _enableValidation);
        }
    }
}
