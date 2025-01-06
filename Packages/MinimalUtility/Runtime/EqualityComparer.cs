#nullable enable

using System;
using System.Collections.Generic;

namespace MinimalUtility
{
    /// <summary>
    /// <see cref="EqualityComparer{T}"/>の拡張クラス.
    /// </summary>
    public static class EqualityComparer
    {
        /// <summary>
        /// 指定した比較処理を使用して<see cref="EqualityComparer{T}"/>を生成します.
        /// </summary>
        ///<example>
        ///<code>
        /// <![CDATA[
        /// using System.Collections.Generic;
        /// using MinimalUtility;
        ///
        /// public enum Fruits : int
        /// {
        ///     Apple,
        ///     Orange,
        ///     Banana,
        /// }
        ///
        /// public class EqualityComparerFactoryExample
        /// {
        ///     private readonly Dictionary<Fruits, string> _fruits;
        ///
        ///     public EqualityComparerFactoryExample()
        ///     {
        ///         var equalityComparer = EqualityComparer.Create<Fruits>(
        ///             static (x, y) => (int) x == (int) y,
        ///             static x => ((int) x).GetHashCode());
        ///         _fruits = new Dictionary<Fruits, string>(3, equalityComparer)
        ///         {
        ///             {Fruits.Apple, "Apple"},
        ///             {Fruits.Orange, "Orange"},
        ///             {Fruits.Banana, "Banana"},
        ///         };
        ///     }
        /// }
        /// ]]>
        /// </code>
        /// </example>
        /// <param name="equals"><see cref="EqualityComparer{T}.Equals(T, T)"/>に使用する処理.</param>
        /// <param name="getHashCode"><see cref="EqualityComparer{T}.GetHashCode(T)"/>に使用する処理.</param>
        /// <typeparam name="T">比較対象の型.</typeparam>
        /// <returns>生成された<see cref="EqualityComparer{T}"/>.</returns>
        /// <exception cref="ArgumentNullException"><see cref="equals"/>がnullです.</exception>
        /// <exception cref="NotSupportedException"><see cref="getHashCode"/>がnullです.</exception>
        public static EqualityComparer<T> Create<T>(Func<T, T, bool> equals, Func<T, int>? getHashCode)
        {
            if (equals == null)
            {
                throw new ArgumentNullException(nameof(equals));
            }
            getHashCode ??= static _ => throw new NotSupportedException(nameof(getHashCode));
            return new DelegateEqualityComparer<T>(equals, getHashCode);
        }

        // Licensed to the .NET Foundation under one or more agreements.
        // The .NET Foundation licenses this file to you under the MIT license.
        // https://github.com/dotnet/runtime/blob/main/src/libraries/System.Private.CoreLib/src/System/Collections/Generic/EqualityComparer.cs
        private class DelegateEqualityComparer<T> : EqualityComparer<T>
        {
            private readonly Func<T, T, bool> equals;
            private readonly Func<T, int> getHashCode;

            public DelegateEqualityComparer(Func<T, T, bool> equals, Func<T, int> getHashCode)
            {
                this.equals = equals;
                this.getHashCode = getHashCode;
            }

            public override bool Equals(T x, T y) => equals(x, y);

            public override int GetHashCode(T obj) => getHashCode(obj);

            public override bool Equals(object? obj) =>
                obj is DelegateEqualityComparer<T> other &&
                equals == other.equals &&
                getHashCode == other.getHashCode;

            public override int GetHashCode() => HashCode.Combine(equals.GetHashCode(), getHashCode.GetHashCode());
        }
    }
}