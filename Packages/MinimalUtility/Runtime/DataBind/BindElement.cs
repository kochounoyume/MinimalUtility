#nullable enable

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MinimalUtility.DataBind
{
    [Serializable]
    public class BindElement : IEquatable<BindElement>
    {
        [SerializeField]
        private string _propertyName;

        public string propertyName
        {
            get => _propertyName;
            internal set => _propertyName = value;
        }

        internal BindElement(string propertyName)
        {
            _propertyName = propertyName;
        }

        public bool Equals(BindElement? other)
        {
            return other != null && _propertyName == other._propertyName;
        }

        public virtual void Bind(bool value) => throw new InvalidOperationException("Type of bool is specified, check that this is correct.");
        public virtual void Bind(float value) => throw new InvalidOperationException("Type of float is specified, check that this is correct.");
        public virtual void Bind(int value) => throw new InvalidOperationException("Type of int is specified, check that this is correct.");
        public virtual void Bind(char[] value) => throw new InvalidOperationException("Type of char[] is specified, check that this is correct.");
        public virtual void Bind(ArraySegment<char> value) => throw new InvalidOperationException("Type of ArraySegment<char> is specified, check that this is correct.");
        public virtual void Bind(string value) => throw new InvalidOperationException("Type of string is specified, check that this is correct.");
        public virtual void Bind(Texture value) => throw new InvalidOperationException("Type of Texture is specified, check that this is correct.");
        public virtual void Bind(Sprite value) => throw new InvalidOperationException("Type of Sprite is specified, check that this is correct.");
        public virtual void Bind(in Color32 value) => throw new InvalidOperationException("Type of Color32 is specified, check that this is correct.");

        [Conditional("UNITY_EDITOR"), Conditional("ENABLE_MINIMAL_DEBUGGING")]
        protected static void ThrowIfNull(UnityEngine.Object? obj, [CallerArgumentExpression("obj")]string name = "")
        {
            if (obj == null)
            {
                throw new NullReferenceException($"{name} is null.");
            }
        }
    }

    public abstract class TargetBindElement<T> : BindElement where T : Component
    {
        [SerializeField]
        protected T? _target;

        protected TargetBindElement(string propertyName) : base(propertyName)
        {
        }
    }

    public abstract class EnableElement<T> : TargetBindElement<T> where T : Behaviour
    {
        protected EnableElement() : base(nameof(Behaviour.enabled))
        {
        }

        public override void Bind(bool value)
        {
            ThrowIfNull(_target);
            _target!.enabled = value;
        }
    }
}