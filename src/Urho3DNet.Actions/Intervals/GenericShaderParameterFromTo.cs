using System;

namespace Urho3DNet.Actions
{
    public class ShaderParameterFromTo<TShaderParamType> : FiniteTimeAction
    {
        public ShaderParameterFromTo(string parameter,
            TShaderParamType fromValue,
            TShaderParamType toValue,
            Action<string, TShaderParamType, TShaderParamType, float, Material>
                valueAction, // if only generics would support '+'/'-' constraints...
            float duration) : base(duration)
        {
            ValueAction = valueAction;
            Parameter = parameter;
            FromValue = fromValue;
            ToValue = toValue;
        }

        public string Parameter { get; set; }
        public TShaderParamType ToValue { get; set; }
        public TShaderParamType FromValue { get; set; }
        public Action<string, TShaderParamType, TShaderParamType, float, Material> ValueAction { get; set; }

        public override FiniteTimeAction Reverse()
        {
            return new ShaderParameterFromTo<TShaderParamType>(Parameter, ToValue, FromValue, ValueAction, Duration);
        }

        protected internal override ActionState StartAction(Object target)
        {
            return new ShaderParameterFromToState<TShaderParamType>(this, target);
        }
    }

    public class ShaderParameterFloatFromTo : ShaderParameterFromTo<float>
    {
        public ShaderParameterFloatFromTo(string parameter, float fromValue, float toValue, float duration)
            : base(parameter, fromValue, toValue, ValueAction, duration)
        {
        }

        private new static void ValueAction(string parameter, float from, float to, float duration, Material material)
        {
            material.SetShaderParameter(parameter, from + (to - from) * duration);
        }
    }

    public class ShaderParameterVector2FromTo : ShaderParameterFromTo<Vector2>
    {
        public ShaderParameterVector2FromTo(string parameter, Vector2 fromValue, Vector2 toValue, float duration)
            : base(parameter, fromValue, toValue, ValueAction, duration)
        {
        }

        private new static void ValueAction(string parameter, Vector2 from, Vector2 to, float duration, Material material)
        {
            material.SetShaderParameter(parameter, from + (to - from) * duration);
        }
    }

    public class ShaderParameterVector3FromTo : ShaderParameterFromTo<Vector3>
    {
        public ShaderParameterVector3FromTo(string parameter, Vector3 fromValue, Vector3 toValue, float duration)
            : base(parameter, fromValue, toValue, ValueAction, duration)
        {
        }

        private new static void ValueAction(string parameter, Vector3 from, Vector3 to, float duration, Material material)
        {
            material.SetShaderParameter(parameter, from + (to - from) * duration);
        }
    }

    public class ShaderParameterVector4FromTo : ShaderParameterFromTo<Vector4>
    {
        public ShaderParameterVector4FromTo(string parameter, Vector4 fromValue, Vector4 toValue, float duration)
            : base(parameter, fromValue, toValue, ValueAction, duration)
        {
        }

        private new static void ValueAction(string parameter, Vector4 from, Vector4 to, float duration, Material material)
        {
            material.SetShaderParameter(parameter, from + (to - from) * duration);
        }
    }

    public class ShaderParameterColorFromTo : ShaderParameterFromTo<Color>
    {
        public ShaderParameterColorFromTo(string parameter, Color fromValue, Color toValue, float duration)
            : base(parameter, fromValue, toValue, ValueAction, duration)
        {
        }

        private new static void ValueAction(string parameter, Color from, Color to, float duration, Material material)
        {
            material.SetShaderParameter(parameter, from + (to - from) * duration);
        }
    }

    public class ShaderParameterFromToState<TShaderParamType> : FiniteTimeActionState
    {
        public ShaderParameterFromToState(ShaderParameterFromTo<TShaderParamType> action, Object target)
            : base(action, target)
        {
            ParameterName = action.Parameter;
            ToValue = action.ToValue;
            FromValue = action.FromValue;
            ValueAction = action.ValueAction;
            Material = target as Material;
        }

        public Action<string, TShaderParamType, TShaderParamType, float, Material> ValueAction { get; set; }
        protected string ParameterName { get; set; }
        protected TShaderParamType FromValue { get; set; }
        protected TShaderParamType ToValue { get; set; }
        protected Material Material { get; set; }

        public override void Update(float time)
        {
            ValueAction(ParameterName, FromValue, ToValue, time, Material);
        }
    }
}