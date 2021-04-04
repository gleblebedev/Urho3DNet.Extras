using System;
using System.Threading.Tasks;

namespace Urho3DNet.Actions
{
    public static class ExtensionMethods
    {
        public static ActionBuilder<Node> Hide(this ActionBuilder<Node> builder)
        {
            return builder.Add(new Hide());
        }

        public static ActionBuilder<Node> Place(this ActionBuilder<Node> builder, Vector3 pos)
        {
            return builder.Add(new Place(pos));
        }

        public static ActionBuilder<Node> Place(this ActionBuilder<Node> builder, int posX, int posY, int posZ)
        {
            return builder.Add(new Place(posX, posY, posZ));
        }

        public static ActionBuilder<Node> RemoveSelf(this ActionBuilder<Node> builder)
        {
            return builder.Add(new RemoveSelf());
        }

        public static ActionBuilder<Node> Show(this ActionBuilder<Node> builder)
        {
            return builder.Add(new Show());
        }

        public static ActionBuilder<Node> ToggleVisibility(this ActionBuilder<Node> builder)
        {
            return builder.Add(new ToggleVisibility());
        }

        public static ActionBuilder<Node> BezierBy(this ActionBuilder<Node> builder, float t, BezierConfig config)
        {
            return builder.Add(new BezierBy(t, config));
        }

        public static ActionBuilder<Node> BezierTo(this ActionBuilder<Node> builder, float t, BezierConfig c)
        {
            return builder.Add(new BezierTo(t, c));
        }

        public static ActionBuilder<Node> Blink(this ActionBuilder<Node> builder, float duration, uint numOfBlinks)
        {
            return builder.Add(new Blink(duration, numOfBlinks));
        }

        public static ActionBuilder<Material> ShaderParameterFloatFromTo(this ActionBuilder<Material> builder,
            string parameter, float fromValue, float toValue, float duration)
        {
            return builder.Add(new ShaderParameterFloatFromTo(parameter, fromValue, toValue, duration));
        }

        public static ActionBuilder<Material> ShaderParameterVector2FromTo(this ActionBuilder<Material> builder,
            string parameter, Vector2 fromValue, Vector2 toValue, float duration)
        {
            return builder.Add(new ShaderParameterVector2FromTo(parameter, fromValue, toValue, duration));
        }

        public static ActionBuilder<Material> ShaderParameterVector3FromTo(this ActionBuilder<Material> builder,
            string parameter, Vector3 fromValue, Vector3 toValue, float duration)
        {
            return builder.Add(new ShaderParameterVector3FromTo(parameter, fromValue, toValue, duration));
        }

        public static ActionBuilder<Material> ShaderParameterVector4FromTo(this ActionBuilder<Material> builder,
            string parameter, Vector4 fromValue, Vector4 toValue, float duration)
        {
            return builder.Add(new ShaderParameterVector4FromTo(parameter, fromValue, toValue, duration));
        }

        public static ActionBuilder<Material> ShaderParameterColorFromTo(this ActionBuilder<Material> builder,
            string parameter, Color fromValue, Color toValue, float duration)
        {
            return builder.Add(new ShaderParameterColorFromTo(parameter, fromValue, toValue, duration));
        }

        public static ActionBuilder<Material> MatDiffColorFromTo(this ActionBuilder<Material> builder,
            Color fromValue, Color toValue, float duration)
        {
            return builder.Add(new ShaderParameterColorFromTo("MatDiffColor", fromValue, toValue, duration));
        }

        public static ActionBuilder<Material> ShaderParameterFromTo<TShaderParamType>(
            this ActionBuilder<Material> builder, string parameter, TShaderParamType fromValue,
            TShaderParamType toValue, Action<string, TShaderParamType, TShaderParamType, float, Material> valueAction,
            float duration)
        {
            return builder.Add(
                new ShaderParameterFromTo<TShaderParamType>(parameter, fromValue, toValue, valueAction, duration));
        }

        public static ActionBuilder<Node> JumpBy(this ActionBuilder<Node> builder, float duration, Vector3 position,
            float height, uint jumps)
        {
            return builder.Add(new JumpBy(duration, position, height, jumps));
        }

        public static ActionBuilder<Node> JumpTo(this ActionBuilder<Node> builder, float duration, Vector3 position,
            float height, uint jumps)
        {
            return builder.Add(new JumpTo(duration, position, height, jumps));
        }

        public static ActionBuilder<Node> MoveBy(this ActionBuilder<Node> builder, float duration, Vector3 position)
        {
            return builder.Add(new MoveBy(duration, position));
        }

        public static ActionBuilder<Node> MoveTo(this ActionBuilder<Node> builder, float duration, Vector3 position)
        {
            return builder.Add(new MoveTo(duration, position));
        }

        public static ActionBuilder<Node> RotateAroundBy(this ActionBuilder<Node> builder, float duration,
            Vector3 point, float deltaX, float deltaY, float deltaZ, TransformSpace ts)
        {
            return builder.Add(new RotateAroundBy(duration, point, deltaX, deltaY, deltaZ, ts));
        }

        public static ActionBuilder<Node> RotateBy(this ActionBuilder<Node> builder, float duration, float deltaAngleX,
            float deltaAngleY, float deltaAngleZ)
        {
            return builder.Add(new RotateBy(duration, deltaAngleX, deltaAngleY, deltaAngleZ));
        }

        public static ActionBuilder<Node> RotateBy(this ActionBuilder<Node> builder, float duration, float deltaAngle)
        {
            return builder.Add(new RotateBy(duration, deltaAngle));
        }

        public static ActionBuilder<Node> RotateTo(this ActionBuilder<Node> builder, float duration, float deltaAngleX,
            float deltaAngleY, float deltaAngleZ)
        {
            return builder.Add(new RotateTo(duration, deltaAngleX, deltaAngleY, deltaAngleZ));
        }

        public static ActionBuilder<Node> RotateTo(this ActionBuilder<Node> builder, float duration, float deltaAngle)
        {
            return builder.Add(new RotateTo(duration, deltaAngle));
        }

        public static ActionBuilder<Node> ScaleBy(this ActionBuilder<Node> builder, float duration, float scale)
        {
            return builder.Add(new ScaleBy(duration, scale));
        }

        public static ActionBuilder<Node> ScaleBy(this ActionBuilder<Node> builder, float duration, float scaleX,
            float scaleY, float scaleZ)
        {
            return builder.Add(new ScaleBy(duration, scaleX, scaleY, scaleZ));
        }

        public static ActionBuilder<Node> ScaleTo(this ActionBuilder<Node> builder, float duration, float scale)
        {
            return builder.Add(new ScaleTo(duration, scale));
        }

        public static ActionBuilder<Node> ScaleTo(this ActionBuilder<Node> builder, float duration, float scaleX,
            float scaleY, float scaleZ)
        {
            return builder.Add(new ScaleTo(duration, scaleX, scaleY, scaleZ));
        }


        /// <param name="action"></param>
        /// <summary></summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Task<ActionState> RunActionsAsync(this Object target, ActionManager actionManager,
            FiniteTimeAction action)
        {
            var tcs = new TaskCompletionSource<ActionState>();
            actionManager.AddAction(new Sequence(action, new TaskSource(tcs))
            {
                CancelAction = s => tcs.TrySetCanceled()
            }, target);
            return tcs.Task;
        }


        /// <param name="actions">An array of FiniteTimeAction objects.</param>
        /// <summary>Runs a sequence of Actions so that it can be awaited.</summary>
        /// <returns>Task representing the actions.</returns>
        /// <remarks></remarks>
        public static Task<ActionState> RunActionsAsync(this Object target, ActionManager actionManager,
            params FiniteTimeAction[] actions)
        {
            if (actions.Length == 0)
                return Task.FromResult((ActionState) null);
            var tcs = new TaskCompletionSource<ActionState>();
            FiniteTimeAction other = new TaskSource(tcs);
            FiniteTimeAction finiteTimeAction;
            if (actions.Length == 0)
            {
                finiteTimeAction = other;
            }
            else
            {
                finiteTimeAction = new Sequence(actions, other);
                ((Sequence) finiteTimeAction).CancelAction = s => tcs.TrySetCanceled();
            }

            actionManager.AddAction(finiteTimeAction, target);
            return tcs.Task;
        }

        /// <param name="actions">Actions to execute.</param>
        /// <summary>Runs the specified actions.</summary>
        /// <remarks>The actions are groupped in a Sequence action.</remarks>
        public static void RunActions(this Object target, ActionManager actionManager,
            params FiniteTimeAction[] actions)
        {
            actionManager.AddAction(actions.Length > 1 ? new Sequence(actions) : (BaseAction) actions[0], target);
        }

        /// <param name="state"></param>
        /// <summary></summary>
        /// <remarks></remarks>
        public static void RemoveAction(this Object target, ActionManager actionManager, ActionState state)
        {
            actionManager.RemoveAction(state);
        }

        /// <param name="action"></param>
        /// <summary></summary>
        /// <remarks></remarks>
        public static void RemoveAction(this Object target, ActionManager actionManager, BaseAction action)
        {
            actionManager.RemoveAction(action, target);
        }

        /// <summary>Removes all actions that have been started with <see cref="T:Urho.Node.RunActionsAsync" />.</summary>
        /// <remarks></remarks>
        public static void RemoveAllActions(this Object target, ActionManager actionManager)
        {
            actionManager.RemoveAllActionsFromTarget(target);
        }

        /// <summary>Pauses all actions that have been started with <see cref="T:Urho.Node.RunActionsAsync" />.</summary>
        /// <remarks></remarks>
        public static void PauseAllActions(this Object target, ActionManager actionManager)
        {
            actionManager.PauseTarget(target);
        }

        /// <summary>Resumes all actions that have been started with <see cref="T:Urho.Node.RunActionsAsync" />.</summary>
        /// <remarks></remarks>
        public static void ResumeAllActions(this Object target, ActionManager actionManager)
        {
            actionManager.ResumeTarget(target);
        }
    }
}