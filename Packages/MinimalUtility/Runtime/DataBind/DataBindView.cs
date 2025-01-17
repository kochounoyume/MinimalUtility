#nullable enable

using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace MinimalUtility.DataBind
{
    /// <summary>
    /// 汎用Viewクラス.
    /// </summary>
    public class DataBindView : MonoBehaviour
    {
        private static readonly RuntimeTypeHandle s_boolTypeHandle = typeof(bool).TypeHandle;
        private static readonly RuntimeTypeHandle s_floatTypeHandle = typeof(float).TypeHandle;
        private static readonly RuntimeTypeHandle s_intTypeHandle = typeof(int).TypeHandle;
        private static readonly RuntimeTypeHandle s_dateTimeTypeHandle = typeof(DateTime).TypeHandle;
        private static readonly RuntimeTypeHandle s_timeSpanTypeHandle = typeof(TimeSpan).TypeHandle;
        private static readonly RuntimeTypeHandle s_charArrayTypeHandle = typeof(char[]).TypeHandle;
        private static readonly RuntimeTypeHandle s_arraySegmentTypeHandle = typeof(ArraySegment<char>).TypeHandle;
        private static readonly RuntimeTypeHandle s_stringTypeHandle = typeof(string).TypeHandle;
        private static readonly RuntimeTypeHandle s_textureTypeHandle = typeof(Texture).TypeHandle;
        private static readonly RuntimeTypeHandle s_spriteTypeHandle = typeof(Sprite).TypeHandle;
        private static readonly RuntimeTypeHandle s_color32TypeHandle = typeof(Color32).TypeHandle;
        private static readonly BindElement s_emptyElement = new("");

        [SerializeReference]
        private BindElement[] _elements = Array.Empty<BindElement>();

        public void Bind(string propertyName, bool value)
        {
            BindInternal(propertyName, ref value);
        }

        public void Bind(string propertyName, float value)
        {
            BindInternal(propertyName, ref value);
        }

        public void Bind(string propertyName, int value)
        {
            BindInternal(propertyName, ref value);
        }

        public void Bind(string propertyName, char[] value)
        {
            BindInternal(propertyName, ref value);
        }

        public void Bind(string propertyName, ArraySegment<char> value)
        {
            BindInternal(propertyName, ref value);
        }

        public void Bind(string propertyName, string value)
        {
            BindInternal(propertyName, ref value);
        }

        public void Bind(string propertyName, Texture value)
        {
            BindInternal(propertyName, ref value);
        }

        public void Bind(string propertyName, Sprite value)
        {
            BindInternal(propertyName, ref value);
        }

        public void Bind<T1, T2>((T1, T2) tuple, [CallerArgumentExpression("tuple")]string propertyName = "")
        {
            BindInternal(GetPropertyName(propertyName, 0), ref tuple.Item1);
            BindInternal(GetPropertyName(propertyName, 1), ref tuple.Item2);
        }

        public void Bind<T1, T2, T3>((T1, T2, T3) tuple, [CallerArgumentExpression("tuple")]string propertyName = "")
        {
            BindInternal(GetPropertyName(propertyName, 0), ref tuple.Item1);
            BindInternal(GetPropertyName(propertyName, 1), ref tuple.Item2);
            BindInternal(GetPropertyName(propertyName, 2), ref tuple.Item3);
        }

        public void Bind<T1, T2, T3, T4>((T1, T2, T3, T4) tuple, [CallerArgumentExpression("tuple")]string propertyName = "")
        {
            BindInternal(GetPropertyName(propertyName, 0), ref tuple.Item1);
            BindInternal(GetPropertyName(propertyName, 1), ref tuple.Item2);
            BindInternal(GetPropertyName(propertyName, 2), ref tuple.Item3);
            BindInternal(GetPropertyName(propertyName, 3), ref tuple.Item4);
        }

        public void Bind<T1, T2, T3, T4, T5>((T1, T2, T3, T4, T5) tuple,
            [CallerArgumentExpression("tuple")]string propertyName = "")
        {
            BindInternal(GetPropertyName(propertyName, 0), ref tuple.Item1);
            BindInternal(GetPropertyName(propertyName, 1), ref tuple.Item2);
            BindInternal(GetPropertyName(propertyName, 2), ref tuple.Item3);
            BindInternal(GetPropertyName(propertyName, 3), ref tuple.Item4);
            BindInternal(GetPropertyName(propertyName, 4), ref tuple.Item5);
        }

        public void Bind<T1, T2, T3, T4, T5, T6>((T1, T2, T3, T4, T5, T6) tuple,
            [CallerArgumentExpression("tuple")]string propertyName = "")
        {
            BindInternal(GetPropertyName(propertyName, 0), ref tuple.Item1);
            BindInternal(GetPropertyName(propertyName, 1), ref tuple.Item2);
            BindInternal(GetPropertyName(propertyName, 2), ref tuple.Item3);
            BindInternal(GetPropertyName(propertyName, 3), ref tuple.Item4);
            BindInternal(GetPropertyName(propertyName, 4), ref tuple.Item5);
            BindInternal(GetPropertyName(propertyName, 5), ref tuple.Item6);
        }

        public void Bind<T1, T2, T3, T4, T5, T6, T7>((T1, T2, T3, T4, T5, T6, T7) tuple,
            [CallerArgumentExpression("tuple")]string propertyName = "")
        {
            BindInternal(GetPropertyName(propertyName, 0), ref tuple.Item1);
            BindInternal(GetPropertyName(propertyName, 1), ref tuple.Item2);
            BindInternal(GetPropertyName(propertyName, 2), ref tuple.Item3);
            BindInternal(GetPropertyName(propertyName, 3), ref tuple.Item4);
            BindInternal(GetPropertyName(propertyName, 4), ref tuple.Item5);
            BindInternal(GetPropertyName(propertyName, 5), ref tuple.Item6);
            BindInternal(GetPropertyName(propertyName, 6), ref tuple.Item7);
        }

        public void Bind<T1, T2, T3, T4, T5, T6, T7, T8>((T1, T2, T3, T4, T5, T6, T7, T8) tuple,
            [CallerArgumentExpression("tuple")]string propertyName = "")
        {
            BindInternal(GetPropertyName(propertyName, 0), ref tuple.Item1);
            BindInternal(GetPropertyName(propertyName, 1), ref tuple.Item2);
            BindInternal(GetPropertyName(propertyName, 2), ref tuple.Item3);
            BindInternal(GetPropertyName(propertyName, 3), ref tuple.Item4);
            BindInternal(GetPropertyName(propertyName, 4), ref tuple.Item5);
            BindInternal(GetPropertyName(propertyName, 5), ref tuple.Item6);
            BindInternal(GetPropertyName(propertyName, 6), ref tuple.Item7);
            BindInternal(GetPropertyName(propertyName, 7), ref tuple.Item8);
        }

        public void Bind<T1, T2, T3, T4, T5, T6, T7, T8, T9>((T1, T2, T3, T4, T5, T6, T7, T8, T9) tuple,
            [CallerArgumentExpression("tuple")]string propertyName = "")
        {
            BindInternal(GetPropertyName(propertyName, 0), ref tuple.Item1);
            BindInternal(GetPropertyName(propertyName, 1), ref tuple.Item2);
            BindInternal(GetPropertyName(propertyName, 2), ref tuple.Item3);
            BindInternal(GetPropertyName(propertyName, 3), ref tuple.Item4);
            BindInternal(GetPropertyName(propertyName, 4), ref tuple.Item5);
            BindInternal(GetPropertyName(propertyName, 5), ref tuple.Item6);
            BindInternal(GetPropertyName(propertyName, 6), ref tuple.Item7);
            BindInternal(GetPropertyName(propertyName, 7), ref tuple.Item8);
            BindInternal(GetPropertyName(propertyName, 8), ref tuple.Item9);
        }

        public void Bind<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>((T1, T2, T3, T4, T5, T6, T7, T8, T9, T10) tuple,
            [CallerArgumentExpression("tuple")]string propertyName = "")
        {
            BindInternal(GetPropertyName(propertyName, 0), ref tuple.Item1);
            BindInternal(GetPropertyName(propertyName, 1), ref tuple.Item2);
            BindInternal(GetPropertyName(propertyName, 2), ref tuple.Item3);
            BindInternal(GetPropertyName(propertyName, 3), ref tuple.Item4);
            BindInternal(GetPropertyName(propertyName, 4), ref tuple.Item5);
            BindInternal(GetPropertyName(propertyName, 5), ref tuple.Item6);
            BindInternal(GetPropertyName(propertyName, 6), ref tuple.Item7);
            BindInternal(GetPropertyName(propertyName, 7), ref tuple.Item8);
            BindInternal(GetPropertyName(propertyName, 8), ref tuple.Item9);
            BindInternal(GetPropertyName(propertyName, 9), ref tuple.Item10);
        }

        public void Bind<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>((T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11) tuple,
            [CallerArgumentExpression("tuple")]string propertyName = "")
        {
            BindInternal(GetPropertyName(propertyName, 0), ref tuple.Item1);
            BindInternal(GetPropertyName(propertyName, 1), ref tuple.Item2);
            BindInternal(GetPropertyName(propertyName, 2), ref tuple.Item3);
            BindInternal(GetPropertyName(propertyName, 3), ref tuple.Item4);
            BindInternal(GetPropertyName(propertyName, 4), ref tuple.Item5);
            BindInternal(GetPropertyName(propertyName, 5), ref tuple.Item6);
            BindInternal(GetPropertyName(propertyName, 6), ref tuple.Item7);
            BindInternal(GetPropertyName(propertyName, 7), ref tuple.Item8);
            BindInternal(GetPropertyName(propertyName, 8), ref tuple.Item9);
            BindInternal(GetPropertyName(propertyName, 9), ref tuple.Item10);
            BindInternal(GetPropertyName(propertyName, 10), ref tuple.Item11);
        }

        public void Bind<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>((T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12) tuple,
            [CallerArgumentExpression("tuple")]string propertyName = "")
        {
            BindInternal(GetPropertyName(propertyName, 0), ref tuple.Item1);
            BindInternal(GetPropertyName(propertyName, 1), ref tuple.Item2);
            BindInternal(GetPropertyName(propertyName, 2), ref tuple.Item3);
            BindInternal(GetPropertyName(propertyName, 3), ref tuple.Item4);
            BindInternal(GetPropertyName(propertyName, 4), ref tuple.Item5);
            BindInternal(GetPropertyName(propertyName, 5), ref tuple.Item6);
            BindInternal(GetPropertyName(propertyName, 6), ref tuple.Item7);
            BindInternal(GetPropertyName(propertyName, 7), ref tuple.Item8);
            BindInternal(GetPropertyName(propertyName, 8), ref tuple.Item9);
            BindInternal(GetPropertyName(propertyName, 9), ref tuple.Item10);
            BindInternal(GetPropertyName(propertyName, 10), ref tuple.Item11);
            BindInternal(GetPropertyName(propertyName, 11), ref tuple.Item12);
        }

        public void Bind<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>((T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13) tuple,
            [CallerArgumentExpression("tuple")]string propertyName = "")
        {
            BindInternal(GetPropertyName(propertyName, 0), ref tuple.Item1);
            BindInternal(GetPropertyName(propertyName, 1), ref tuple.Item2);
            BindInternal(GetPropertyName(propertyName, 2), ref tuple.Item3);
            BindInternal(GetPropertyName(propertyName, 3), ref tuple.Item4);
            BindInternal(GetPropertyName(propertyName, 4), ref tuple.Item5);
            BindInternal(GetPropertyName(propertyName, 5), ref tuple.Item6);
            BindInternal(GetPropertyName(propertyName, 6), ref tuple.Item7);
            BindInternal(GetPropertyName(propertyName, 7), ref tuple.Item8);
            BindInternal(GetPropertyName(propertyName, 8), ref tuple.Item9);
            BindInternal(GetPropertyName(propertyName, 9), ref tuple.Item10);
            BindInternal(GetPropertyName(propertyName, 10), ref tuple.Item11);
            BindInternal(GetPropertyName(propertyName, 11), ref tuple.Item12);
            BindInternal(GetPropertyName(propertyName, 12), ref tuple.Item13);
        }

        public void Bind<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>((T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14) tuple,
            [CallerArgumentExpression("tuple")]string propertyName = "")
        {
            BindInternal(GetPropertyName(propertyName, 0), ref tuple.Item1);
            BindInternal(GetPropertyName(propertyName, 1), ref tuple.Item2);
            BindInternal(GetPropertyName(propertyName, 2), ref tuple.Item3);
            BindInternal(GetPropertyName(propertyName, 3), ref tuple.Item4);
            BindInternal(GetPropertyName(propertyName, 4), ref tuple.Item5);
            BindInternal(GetPropertyName(propertyName, 5), ref tuple.Item6);
            BindInternal(GetPropertyName(propertyName, 6), ref tuple.Item7);
            BindInternal(GetPropertyName(propertyName, 7), ref tuple.Item8);
            BindInternal(GetPropertyName(propertyName, 8), ref tuple.Item9);
            BindInternal(GetPropertyName(propertyName, 9), ref tuple.Item10);
            BindInternal(GetPropertyName(propertyName, 10), ref tuple.Item11);
            BindInternal(GetPropertyName(propertyName, 11), ref tuple.Item12);
            BindInternal(GetPropertyName(propertyName, 12), ref tuple.Item13);
            BindInternal(GetPropertyName(propertyName, 13), ref tuple.Item14);
        }

        public void Bind<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>((T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15) tuple,
            [CallerArgumentExpression("tuple")]string propertyName = "")
        {
            BindInternal(GetPropertyName(propertyName, 0), ref tuple.Item1);
            BindInternal(GetPropertyName(propertyName, 1), ref tuple.Item2);
            BindInternal(GetPropertyName(propertyName, 2), ref tuple.Item3);
            BindInternal(GetPropertyName(propertyName, 3), ref tuple.Item4);
            BindInternal(GetPropertyName(propertyName, 4), ref tuple.Item5);
            BindInternal(GetPropertyName(propertyName, 5), ref tuple.Item6);
            BindInternal(GetPropertyName(propertyName, 6), ref tuple.Item7);
            BindInternal(GetPropertyName(propertyName, 7), ref tuple.Item8);
            BindInternal(GetPropertyName(propertyName, 8), ref tuple.Item9);
            BindInternal(GetPropertyName(propertyName, 9), ref tuple.Item10);
            BindInternal(GetPropertyName(propertyName, 10), ref tuple.Item11);
            BindInternal(GetPropertyName(propertyName, 11), ref tuple.Item12);
            BindInternal(GetPropertyName(propertyName, 12), ref tuple.Item13);
            BindInternal(GetPropertyName(propertyName, 13), ref tuple.Item14);
            BindInternal(GetPropertyName(propertyName, 14), ref tuple.Item15);
        }

        public void Bind<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>((T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16) tuple,
            [CallerArgumentExpression("tuple")]string propertyName = "")
        {
            BindInternal(GetPropertyName(propertyName, 0), ref tuple.Item1);
            BindInternal(GetPropertyName(propertyName, 1), ref tuple.Item2);
            BindInternal(GetPropertyName(propertyName, 2), ref tuple.Item3);
            BindInternal(GetPropertyName(propertyName, 3), ref tuple.Item4);
            BindInternal(GetPropertyName(propertyName, 4), ref tuple.Item5);
            BindInternal(GetPropertyName(propertyName, 5), ref tuple.Item6);
            BindInternal(GetPropertyName(propertyName, 6), ref tuple.Item7);
            BindInternal(GetPropertyName(propertyName, 7), ref tuple.Item8);
            BindInternal(GetPropertyName(propertyName, 8), ref tuple.Item9);
            BindInternal(GetPropertyName(propertyName, 9), ref tuple.Item10);
            BindInternal(GetPropertyName(propertyName, 10), ref tuple.Item11);
            BindInternal(GetPropertyName(propertyName, 11), ref tuple.Item12);
            BindInternal(GetPropertyName(propertyName, 12), ref tuple.Item13);
            BindInternal(GetPropertyName(propertyName, 13), ref tuple.Item14);
            BindInternal(GetPropertyName(propertyName, 14), ref tuple.Item15);
            BindInternal(GetPropertyName(propertyName, 15), ref tuple.Item16);
        }

        private void BindInternal<T>(in string propertyName, ref T value)
        {
            s_emptyElement.propertyName = propertyName;
            var index = Array.IndexOf(_elements, s_emptyElement);
            if (index == -1)
            {
                throw new ArgumentOutOfRangeException(propertyName);
            }

            var element = _elements[index];
            if (typeof(T).TypeHandle.Equals(s_boolTypeHandle))
            {
                element.Bind(UnsafeUtility.As<T, bool>(ref value));
            }
            else if (typeof(T).TypeHandle.Equals(s_floatTypeHandle))
            {
                element.Bind(UnsafeUtility.As<T, float>(ref value));
            }
            else if (typeof(T).TypeHandle.Equals(s_intTypeHandle))
            {
                element.Bind(UnsafeUtility.As<T, int>(ref value));
            }
            else if (typeof(T).TypeHandle.Equals(s_dateTimeTypeHandle))
            {
                element.Bind(UnsafeUtility.As<T, DateTime>(ref value));
            }
            else if (typeof(T).TypeHandle.Equals(s_timeSpanTypeHandle))
            {
                element.Bind(UnsafeUtility.As<T, TimeSpan>(ref value));
            }
            else if (typeof(T).TypeHandle.Equals(s_charArrayTypeHandle))
            {
                element.Bind(UnsafeUtility.As<T, char[]>(ref value));
            }
            else if (typeof(T).TypeHandle.Equals(s_arraySegmentTypeHandle))
            {
                element.Bind(UnsafeUtility.As<T, ArraySegment<char>>(ref value));
            }
            else if (typeof(T).TypeHandle.Equals(s_stringTypeHandle))
            {
                element.Bind(UnsafeUtility.As<T, string>(ref value));
            }
            else if (typeof(T).TypeHandle.Equals(s_textureTypeHandle))
            {
                element.Bind(UnsafeUtility.As<T, Texture>(ref value));
            }
            else if (typeof(T).TypeHandle.Equals(s_spriteTypeHandle))
            {
                element.Bind(UnsafeUtility.As<T, Sprite>(ref value));
            }
            else if (typeof(T).TypeHandle.Equals(s_color32TypeHandle))
            {
                element.Bind(UnsafeUtility.As<T, Color32>(ref value));
            }
            else
            {
                throw new InvalidOperationException($"Type of {typeof(T).Name} is specified, check that this is correct.");
            }
        }

        private static string GetPropertyName(in ReadOnlySpan<char> tupleStr, int index)
        {
            var span = tupleStr.Slice(1, tupleStr.Length - 2);
            for (int i = 0; i < index; i++)
            {
                span = span[(span.IndexOf(',') + 1)..];
            }
            span = span[..Mathf.Min(span.IndexOf(':'), span.IndexOf(','))].Trim();
            return new string(span);
        }
    }
}