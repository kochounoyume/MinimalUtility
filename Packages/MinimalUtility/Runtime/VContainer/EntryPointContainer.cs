#pragma warning disable SA1402

using System;
using VContainer.Unity;

namespace MinimalUtility.VContainer
{
    /// <summary>
    /// エントリーポイントを一括管理するコンテナクラス.
    /// </summary>
    /// <typeparam name="T1">発火する1番目のインスタンスの型.</typeparam>
    public sealed class EntryPointContainer<T1> : IStartable
        where T1 : class
    {
        private readonly T1 instance1;
        private readonly Action<T1> start;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryPointContainer{T1}"/> class.
        /// </summary>
        /// <param name="instance1">発火する1番目のインスタンス.</param>
        /// <param name="start">発火する処理.</param>
        public EntryPointContainer(
            T1 instance1,
            Action<T1> start)
        {
            this.instance1 = instance1;
            this.start = start;
        }

        /// <inheritdoc />
        void IStartable.Start()
        {
            start(instance1);
        }
    }

    /// <summary>
    /// エントリーポイントを一括管理するコンテナクラス.
    /// </summary>
    /// <typeparam name="T1">発火する1番目のインスタンスの型.</typeparam>
    /// <typeparam name="T2">発火する2番目のインスタンスの型.</typeparam>
    public sealed class EntryPointContainer<T1, T2> : IStartable
        where T1 : class where T2 : class
    {
        private readonly T1 instance1;
        private readonly T2 instance2;
        private readonly Action<T1, T2> start;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryPointContainer{T1, T2}"/> class.
        /// </summary>
        /// <param name="instance1">発火する1番目のインスタンス.</param>
        /// <param name="instance2">発火する2番目のインスタンス.</param>
        /// <param name="start">発火する処理.</param>
        public EntryPointContainer(
            T1 instance1,
            T2 instance2,
            Action<T1, T2> start)
        {
            this.instance1 = instance1;
            this.instance2 = instance2;
            this.start = start;
        }

        /// <inheritdoc />
        void IStartable.Start()
        {
            start(instance1, instance2);
        }
    }

    /// <summary>
    /// エントリーポイントを一括管理するコンテナクラス.
    /// </summary>
    /// <typeparam name="T1">発火する1番目のインスタンスの型.</typeparam>
    /// <typeparam name="T2">発火する2番目のインスタンスの型.</typeparam>
    /// <typeparam name="T3">発火する3番目のインスタンスの型.</typeparam>
    public sealed class EntryPointContainer<T1, T2, T3> : IStartable
        where T1 : class where T2 : class where T3 : class
    {
        private readonly T1 instance1;
        private readonly T2 instance2;
        private readonly T3 instance3;
        private readonly Action<T1, T2, T3> start;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryPointContainer{T1, T2, T3}"/> class.
        /// </summary>
        /// <param name="instance1">発火する1番目のインスタンス.</param>
        /// <param name="instance2">発火する2番目のインスタンス.</param>
        /// <param name="instance3">発火する3番目のインスタンス.</param>
        /// <param name="start">発火する処理.</param>
        public EntryPointContainer(
            T1 instance1,
            T2 instance2,
            T3 instance3,
            Action<T1, T2, T3> start)
        {
            this.instance1 = instance1;
            this.instance2 = instance2;
            this.instance3 = instance3;
            this.start = start;
        }

        /// <inheritdoc />
        void IStartable.Start()
        {
            start(instance1, instance2, instance3);
        }
    }

    /// <summary>
    /// エントリーポイントを一括管理するコンテナクラス.
    /// </summary>
    /// <typeparam name="T1">発火する1番目のインスタンスの型.</typeparam>
    /// <typeparam name="T2">発火する2番目のインスタンスの型.</typeparam>
    /// <typeparam name="T3">発火する3番目のインスタンスの型.</typeparam>
    /// <typeparam name="T4">発火する4番目のインスタンスの型.</typeparam>
    public sealed class EntryPointContainer<T1, T2, T3, T4> : IStartable
        where T1 : class where T2 : class where T3 : class where T4 : class
    {
        private readonly T1 instance1;
        private readonly T2 instance2;
        private readonly T3 instance3;
        private readonly T4 instance4;
        private readonly Action<T1, T2, T3, T4> start;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryPointContainer{T1, T2, T3, T4}"/> class.
        /// </summary>
        /// <param name="instance1">発火する1番目のインスタンス.</param>
        /// <param name="instance2">発火する2番目のインスタンス.</param>
        /// <param name="instance3">発火する3番目のインスタンス.</param>
        /// <param name="instance4">発火する4番目のインスタンス.</param>
        /// <param name="start">発火する処理.</param>
        public EntryPointContainer(
            T1 instance1,
            T2 instance2,
            T3 instance3,
            T4 instance4,
            Action<T1, T2, T3, T4> start)
        {
            this.instance1 = instance1;
            this.instance2 = instance2;
            this.instance3 = instance3;
            this.instance4 = instance4;
            this.start = start;
        }

        /// <inheritdoc />
        void IStartable.Start()
        {
            start(instance1, instance2, instance3, instance4);
        }
    }

    /// <summary>
    /// エントリーポイントを一括管理するコンテナクラス.
    /// </summary>
    /// <typeparam name="T1">発火する1番目のインスタンスの型.</typeparam>
    /// <typeparam name="T2">発火する2番目のインスタンスの型.</typeparam>
    /// <typeparam name="T3">発火する3番目のインスタンスの型.</typeparam>
    /// <typeparam name="T4">発火する4番目のインスタンスの型.</typeparam>
    /// <typeparam name="T5">発火する5番目のインスタンスの型.</typeparam>
    public sealed class EntryPointContainer<T1, T2, T3, T4, T5> : IStartable
        where T1 : class where T2 : class where T3 : class where T4 : class where T5 : class
    {
        private readonly T1 instance1;
        private readonly T2 instance2;
        private readonly T3 instance3;
        private readonly T4 instance4;
        private readonly T5 instance5;
        private readonly Action<T1, T2, T3, T4, T5> start;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryPointContainer{T1, T2, T3, T4, T5}"/> class.
        /// </summary>
        /// <param name="instance1">発火する1番目のインスタンス.</param>
        /// <param name="instance2">発火する2番目のインスタンス.</param>
        /// <param name="instance3">発火する3番目のインスタンス.</param>
        /// <param name="instance4">発火する4番目のインスタンス.</param>
        /// <param name="instance5">発火する5番目のインスタンス.</param>
        /// <param name="start">発火する処理.</param>
        public EntryPointContainer(
            T1 instance1,
            T2 instance2,
            T3 instance3,
            T4 instance4,
            T5 instance5,
            Action<T1, T2, T3, T4, T5> start)
        {
            this.instance1 = instance1;
            this.instance2 = instance2;
            this.instance3 = instance3;
            this.instance4 = instance4;
            this.instance5 = instance5;
            this.start = start;
        }

        /// <inheritdoc />
        void IStartable.Start()
        {
            start(instance1, instance2, instance3, instance4, instance5);
        }
    }

    /// <summary>
    /// エントリーポイントを一括管理するコンテナクラス.
    /// </summary>
    /// <typeparam name="T1">発火する1番目のインスタンスの型.</typeparam>
    /// <typeparam name="T2">発火する2番目のインスタンスの型.</typeparam>
    /// <typeparam name="T3">発火する3番目のインスタンスの型.</typeparam>
    /// <typeparam name="T4">発火する4番目のインスタンスの型.</typeparam>
    /// <typeparam name="T5">発火する5番目のインスタンスの型.</typeparam>
    /// <typeparam name="T6">発火する6番目のインスタンスの型.</typeparam>
    public sealed class EntryPointContainer<T1, T2, T3, T4, T5, T6> : IStartable
        where T1 : class where T2 : class where T3 : class where T4 : class where T5 : class where T6 : class
    {
        private readonly T1 instance1;
        private readonly T2 instance2;
        private readonly T3 instance3;
        private readonly T4 instance4;
        private readonly T5 instance5;
        private readonly T6 instance6;
        private readonly Action<T1, T2, T3, T4, T5, T6> start;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryPointContainer{T1, T2, T3, T4, T5, T6}"/> class.
        /// </summary>
        /// <param name="instance1">発火する1番目のインスタンス.</param>
        /// <param name="instance2">発火する2番目のインスタンス.</param>
        /// <param name="instance3">発火する3番目のインスタンス.</param>
        /// <param name="instance4">発火する4番目のインスタンス.</param>
        /// <param name="instance5">発火する5番目のインスタンス.</param>
        /// <param name="instance6">発火する6番目のインスタンス.</param>
        /// <param name="start">発火する処理.</param>
        public EntryPointContainer(
            T1 instance1,
            T2 instance2,
            T3 instance3,
            T4 instance4,
            T5 instance5,
            T6 instance6,
            Action<T1, T2, T3, T4, T5, T6> start)
        {
            this.instance1 = instance1;
            this.instance2 = instance2;
            this.instance3 = instance3;
            this.instance4 = instance4;
            this.instance5 = instance5;
            this.instance6 = instance6;
            this.start = start;
        }

        /// <inheritdoc />
        void IStartable.Start()
        {
            start(instance1, instance2, instance3, instance4, instance5, instance6);
        }
    }

    /// <summary>
    /// エントリーポイントを一括管理するコンテナクラス.
    /// </summary>
    /// <typeparam name="T1">発火する1番目のインスタンスの型.</typeparam>
    /// <typeparam name="T2">発火する2番目のインスタンスの型.</typeparam>
    /// <typeparam name="T3">発火する3番目のインスタンスの型.</typeparam>
    /// <typeparam name="T4">発火する4番目のインスタンスの型.</typeparam>
    /// <typeparam name="T5">発火する5番目のインスタンスの型.</typeparam>
    /// <typeparam name="T6">発火する6番目のインスタンスの型.</typeparam>
    /// <typeparam name="T7">発火する7番目のインスタンスの型.</typeparam>
    public sealed class EntryPointContainer<T1, T2, T3, T4, T5, T6, T7> : IStartable
        where T1 : class where T2 : class where T3 : class where T4 : class where T5 : class where T6 : class where T7 : class
    {
        private readonly T1 instance1;
        private readonly T2 instance2;
        private readonly T3 instance3;
        private readonly T4 instance4;
        private readonly T5 instance5;
        private readonly T6 instance6;
        private readonly T7 instance7;
        private readonly Action<T1, T2, T3, T4, T5, T6, T7> start;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryPointContainer{T1, T2, T3, T4, T5, T6, T7}"/> class.
        /// </summary>
        /// <param name="instance1">発火する1番目のインスタンス.</param>
        /// <param name="instance2">発火する2番目のインスタンス.</param>
        /// <param name="instance3">発火する3番目のインスタンス.</param>
        /// <param name="instance4">発火する4番目のインスタンス.</param>
        /// <param name="instance5">発火する5番目のインスタンス.</param>
        /// <param name="instance6">発火する6番目のインスタンス.</param>
        /// <param name="instance7">発火する7番目のインスタンス.</param>
        /// <param name="start">発火する処理.</param>
        public EntryPointContainer(
            T1 instance1,
            T2 instance2,
            T3 instance3,
            T4 instance4,
            T5 instance5,
            T6 instance6,
            T7 instance7,
            Action<T1, T2, T3, T4, T5, T6, T7> start)
        {
            this.instance1 = instance1;
            this.instance2 = instance2;
            this.instance3 = instance3;
            this.instance4 = instance4;
            this.instance5 = instance5;
            this.instance6 = instance6;
            this.instance7 = instance7;
            this.start = start;
        }

        /// <inheritdoc />
        void IStartable.Start()
        {
            start(instance1, instance2, instance3, instance4, instance5, instance6, instance7);
        }
    }

    /// <summary>
    /// エントリーポイントを一括管理するコンテナクラス.
    /// </summary>
    /// <typeparam name="T1">発火する1番目のインスタンスの型.</typeparam>
    /// <typeparam name="T2">発火する2番目のインスタンスの型.</typeparam>
    /// <typeparam name="T3">発火する3番目のインスタンスの型.</typeparam>
    /// <typeparam name="T4">発火する4番目のインスタンスの型.</typeparam>
    /// <typeparam name="T5">発火する5番目のインスタンスの型.</typeparam>
    /// <typeparam name="T6">発火する6番目のインスタンスの型.</typeparam>
    /// <typeparam name="T7">発火する7番目のインスタンスの型.</typeparam>
    /// <typeparam name="T8">発火する8番目のインスタンスの型.</typeparam>
    public sealed class EntryPointContainer<T1, T2, T3, T4, T5, T6, T7, T8> : IStartable
        where T1 : class where T2 : class where T3 : class where T4 : class where T5 : class where T6 : class where T7 : class where T8 : class
    {
        private readonly T1 instance1;
        private readonly T2 instance2;
        private readonly T3 instance3;
        private readonly T4 instance4;
        private readonly T5 instance5;
        private readonly T6 instance6;
        private readonly T7 instance7;
        private readonly T8 instance8;
        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8> start;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryPointContainer{T1, T2, T3, T4, T5, T6, T7, T8}"/> class.
        /// </summary>
        /// <param name="instance1">発火する1番目のインスタンス.</param>
        /// <param name="instance2">発火する2番目のインスタンス.</param>
        /// <param name="instance3">発火する3番目のインスタンス.</param>
        /// <param name="instance4">発火する4番目のインスタンス.</param>
        /// <param name="instance5">発火する5番目のインスタンス.</param>
        /// <param name="instance6">発火する6番目のインスタンス.</param>
        /// <param name="instance7">発火する7番目のインスタンス.</param>
        /// <param name="instance8">発火する8番目のインスタンス.</param>
        /// <param name="start">発火する処理.</param>
        public EntryPointContainer(
            T1 instance1,
            T2 instance2,
            T3 instance3,
            T4 instance4,
            T5 instance5,
            T6 instance6,
            T7 instance7,
            T8 instance8,
            Action<T1, T2, T3, T4, T5, T6, T7, T8> start)
        {
            this.instance1 = instance1;
            this.instance2 = instance2;
            this.instance3 = instance3;
            this.instance4 = instance4;
            this.instance5 = instance5;
            this.instance6 = instance6;
            this.instance7 = instance7;
            this.instance8 = instance8;
            this.start = start;
        }

        /// <inheritdoc />
        void IStartable.Start()
        {
            start(instance1, instance2, instance3, instance4, instance5, instance6, instance7, instance8);
        }
    }

    /// <summary>
    /// エントリーポイントを一括管理するコンテナクラス.
    /// </summary>
    /// <typeparam name="T1">発火する1番目のインスタンスの型.</typeparam>
    /// <typeparam name="T2">発火する2番目のインスタンスの型.</typeparam>
    /// <typeparam name="T3">発火する3番目のインスタンスの型.</typeparam>
    /// <typeparam name="T4">発火する4番目のインスタンスの型.</typeparam>
    /// <typeparam name="T5">発火する5番目のインスタンスの型.</typeparam>
    /// <typeparam name="T6">発火する6番目のインスタンスの型.</typeparam>
    /// <typeparam name="T7">発火する7番目のインスタンスの型.</typeparam>
    /// <typeparam name="T8">発火する8番目のインスタンスの型.</typeparam>
    /// <typeparam name="T9">発火する9番目のインスタンスの型.</typeparam>
    public sealed class EntryPointContainer<T1, T2, T3, T4, T5, T6, T7, T8, T9> : IStartable
        where T1 : class where T2 : class where T3 : class where T4 : class where T5 : class where T6 : class where T7 : class where T8 : class where T9 : class
    {
        private readonly T1 instance1;
        private readonly T2 instance2;
        private readonly T3 instance3;
        private readonly T4 instance4;
        private readonly T5 instance5;
        private readonly T6 instance6;
        private readonly T7 instance7;
        private readonly T8 instance8;
        private readonly T9 instance9;
        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> start;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryPointContainer{T1, T2, T3, T4, T5, T6, T7, T8, T9}"/> class.
        /// </summary>
        /// <param name="instance1">発火する1番目のインスタンス.</param>
        /// <param name="instance2">発火する2番目のインスタンス.</param>
        /// <param name="instance3">発火する3番目のインスタンス.</param>
        /// <param name="instance4">発火する4番目のインスタンス.</param>
        /// <param name="instance5">発火する5番目のインスタンス.</param>
        /// <param name="instance6">発火する6番目のインスタンス.</param>
        /// <param name="instance7">発火する7番目のインスタンス.</param>
        /// <param name="instance8">発火する8番目のインスタンス.</param>
        /// <param name="instance9">発火する9番目のインスタンス.</param>
        /// <param name="start">発火する処理.</param>
        public EntryPointContainer(
            T1 instance1,
            T2 instance2,
            T3 instance3,
            T4 instance4,
            T5 instance5,
            T6 instance6,
            T7 instance7,
            T8 instance8,
            T9 instance9,
            Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> start)
        {
            this.instance1 = instance1;
            this.instance2 = instance2;
            this.instance3 = instance3;
            this.instance4 = instance4;
            this.instance5 = instance5;
            this.instance6 = instance6;
            this.instance7 = instance7;
            this.instance8 = instance8;
            this.instance9 = instance9;
            this.start = start;
        }

        /// <inheritdoc />
        void IStartable.Start()
        {
            start(instance1, instance2, instance3, instance4, instance5, instance6, instance7, instance8, instance9);
        }
    }

    /// <summary>
    /// エントリーポイントを一括管理するコンテナクラス.
    /// </summary>
    /// <typeparam name="T1">発火する1番目のインスタンスの型.</typeparam>
    /// <typeparam name="T2">発火する2番目のインスタンスの型.</typeparam>
    /// <typeparam name="T3">発火する3番目のインスタンスの型.</typeparam>
    /// <typeparam name="T4">発火する4番目のインスタンスの型.</typeparam>
    /// <typeparam name="T5">発火する5番目のインスタンスの型.</typeparam>
    /// <typeparam name="T6">発火する6番目のインスタンスの型.</typeparam>
    /// <typeparam name="T7">発火する7番目のインスタンスの型.</typeparam>
    /// <typeparam name="T8">発火する8番目のインスタンスの型.</typeparam>
    /// <typeparam name="T9">発火する9番目のインスタンスの型.</typeparam>
    /// <typeparam name="T10">発火する10番目のインスタンスの型.</typeparam>
    public sealed class EntryPointContainer<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : IStartable
        where T1 : class where T2 : class where T3 : class where T4 : class where T5 : class where T6 : class where T7 : class where T8 : class where T9 : class where T10 : class
    {
        private readonly T1 instance1;
        private readonly T2 instance2;
        private readonly T3 instance3;
        private readonly T4 instance4;
        private readonly T5 instance5;
        private readonly T6 instance6;
        private readonly T7 instance7;
        private readonly T8 instance8;
        private readonly T9 instance9;
        private readonly T10 instance10;
        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> start;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryPointContainer{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10}"/> class.
        /// </summary>
        /// <param name="instance1">発火する1番目のインスタンス.</param>
        /// <param name="instance2">発火する2番目のインスタンス.</param>
        /// <param name="instance3">発火する3番目のインスタンス.</param>
        /// <param name="instance4">発火する4番目のインスタンス.</param>
        /// <param name="instance5">発火する5番目のインスタンス.</param>
        /// <param name="instance6">発火する6番目のインスタンス.</param>
        /// <param name="instance7">発火する7番目のインスタンス.</param>
        /// <param name="instance8">発火する8番目のインスタンス.</param>
        /// <param name="instance9">発火する9番目のインスタンス.</param>
        /// <param name="instance10">発火する10番目のインスタンス.</param>
        /// <param name="start">発火する処理.</param>
        public EntryPointContainer(
            T1 instance1,
            T2 instance2,
            T3 instance3,
            T4 instance4,
            T5 instance5,
            T6 instance6,
            T7 instance7,
            T8 instance8,
            T9 instance9,
            T10 instance10,
            Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> start)
        {
            this.instance1 = instance1;
            this.instance2 = instance2;
            this.instance3 = instance3;
            this.instance4 = instance4;
            this.instance5 = instance5;
            this.instance6 = instance6;
            this.instance7 = instance7;
            this.instance8 = instance8;
            this.instance9 = instance9;
            this.instance10 = instance10;
            this.start = start;
        }

        /// <inheritdoc />
        void IStartable.Start()
        {
            start(instance1, instance2, instance3, instance4, instance5, instance6, instance7, instance8, instance9, instance10);
        }
    }

    /// <summary>
    /// エントリーポイントを一括管理するコンテナクラス.
    /// </summary>
    /// <typeparam name="T1">発火する1番目のインスタンスの型.</typeparam>
    /// <typeparam name="T2">発火する2番目のインスタンスの型.</typeparam>
    /// <typeparam name="T3">発火する3番目のインスタンスの型.</typeparam>
    /// <typeparam name="T4">発火する4番目のインスタンスの型.</typeparam>
    /// <typeparam name="T5">発火する5番目のインスタンスの型.</typeparam>
    /// <typeparam name="T6">発火する6番目のインスタンスの型.</typeparam>
    /// <typeparam name="T7">発火する7番目のインスタンスの型.</typeparam>
    /// <typeparam name="T8">発火する8番目のインスタンスの型.</typeparam>
    /// <typeparam name="T9">発火する9番目のインスタンスの型.</typeparam>
    /// <typeparam name="T10">発火する10番目のインスタンスの型.</typeparam>
    /// <typeparam name="T11">発火する11番目のインスタンスの型.</typeparam>
    public sealed class EntryPointContainer<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : IStartable
        where T1 : class where T2 : class where T3 : class where T4 : class where T5 : class where T6 : class where T7 : class where T8 : class where T9 : class where T10 : class where T11 : class
    {
        private readonly T1 instance1;
        private readonly T2 instance2;
        private readonly T3 instance3;
        private readonly T4 instance4;
        private readonly T5 instance5;
        private readonly T6 instance6;
        private readonly T7 instance7;
        private readonly T8 instance8;
        private readonly T9 instance9;
        private readonly T10 instance10;
        private readonly T11 instance11;
        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> start;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryPointContainer{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11}"/> class.
        /// </summary>
        /// <param name="instance1">発火する1番目のインスタンス.</param>
        /// <param name="instance2">発火する2番目のインスタンス.</param>
        /// <param name="instance3">発火する3番目のインスタンス.</param>
        /// <param name="instance4">発火する4番目のインスタンス.</param>
        /// <param name="instance5">発火する5番目のインスタンス.</param>
        /// <param name="instance6">発火する6番目のインスタンス.</param>
        /// <param name="instance7">発火する7番目のインスタンス.</param>
        /// <param name="instance8">発火する8番目のインスタンス.</param>
        /// <param name="instance9">発火する9番目のインスタンス.</param>
        /// <param name="instance10">発火する10番目のインスタンス.</param>
        /// <param name="instance11">発火する11番目のインスタンス.</param>
        /// <param name="start">発火する処理.</param>
        public EntryPointContainer(
            T1 instance1,
            T2 instance2,
            T3 instance3,
            T4 instance4,
            T5 instance5,
            T6 instance6,
            T7 instance7,
            T8 instance8,
            T9 instance9,
            T10 instance10,
            T11 instance11,
            Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> start)
        {
            this.instance1 = instance1;
            this.instance2 = instance2;
            this.instance3 = instance3;
            this.instance4 = instance4;
            this.instance5 = instance5;
            this.instance6 = instance6;
            this.instance7 = instance7;
            this.instance8 = instance8;
            this.instance9 = instance9;
            this.instance10 = instance10;
            this.instance11 = instance11;
            this.start = start;
        }

        /// <inheritdoc />
        void IStartable.Start()
        {
            start(instance1, instance2, instance3, instance4, instance5, instance6, instance7, instance8, instance9, instance10, instance11);
        }
    }

    /// <summary>
    /// エントリーポイントを一括管理するコンテナクラス.
    /// </summary>
    /// <typeparam name="T1">発火する1番目のインスタンスの型.</typeparam>
    /// <typeparam name="T2">発火する2番目のインスタンスの型.</typeparam>
    /// <typeparam name="T3">発火する3番目のインスタンスの型.</typeparam>
    /// <typeparam name="T4">発火する4番目のインスタンスの型.</typeparam>
    /// <typeparam name="T5">発火する5番目のインスタンスの型.</typeparam>
    /// <typeparam name="T6">発火する6番目のインスタンスの型.</typeparam>
    /// <typeparam name="T7">発火する7番目のインスタンスの型.</typeparam>
    /// <typeparam name="T8">発火する8番目のインスタンスの型.</typeparam>
    /// <typeparam name="T9">発火する9番目のインスタンスの型.</typeparam>
    /// <typeparam name="T10">発火する10番目のインスタンスの型.</typeparam>
    /// <typeparam name="T11">発火する11番目のインスタンスの型.</typeparam>
    /// <typeparam name="T12">発火する12番目のインスタンスの型.</typeparam>
    public sealed class EntryPointContainer<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : IStartable
        where T1 : class where T2 : class where T3 : class where T4 : class where T5 : class where T6 : class where T7 : class where T8 : class where T9 : class where T10 : class where T11 : class where T12 : class
    {
        private readonly T1 instance1;
        private readonly T2 instance2;
        private readonly T3 instance3;
        private readonly T4 instance4;
        private readonly T5 instance5;
        private readonly T6 instance6;
        private readonly T7 instance7;
        private readonly T8 instance8;
        private readonly T9 instance9;
        private readonly T10 instance10;
        private readonly T11 instance11;
        private readonly T12 instance12;
        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> start;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryPointContainer{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12}"/> class.
        /// </summary>
        /// <param name="instance1">発火する1番目のインスタンス.</param>
        /// <param name="instance2">発火する2番目のインスタンス.</param>
        /// <param name="instance3">発火する3番目のインスタンス.</param>
        /// <param name="instance4">発火する4番目のインスタンス.</param>
        /// <param name="instance5">発火する5番目のインスタンス.</param>
        /// <param name="instance6">発火する6番目のインスタンス.</param>
        /// <param name="instance7">発火する7番目のインスタンス.</param>
        /// <param name="instance8">発火する8番目のインスタンス.</param>
        /// <param name="instance9">発火する9番目のインスタンス.</param>
        /// <param name="instance10">発火する10番目のインスタンス.</param>
        /// <param name="instance11">発火する11番目のインスタンス.</param>
        /// <param name="instance12">発火する12番目のインスタンス.</param>
        /// <param name="start">発火する処理.</param>
        public EntryPointContainer(
            T1 instance1,
            T2 instance2,
            T3 instance3,
            T4 instance4,
            T5 instance5,
            T6 instance6,
            T7 instance7,
            T8 instance8,
            T9 instance9,
            T10 instance10,
            T11 instance11,
            T12 instance12,
            Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> start)
        {
            this.instance1 = instance1;
            this.instance2 = instance2;
            this.instance3 = instance3;
            this.instance4 = instance4;
            this.instance5 = instance5;
            this.instance6 = instance6;
            this.instance7 = instance7;
            this.instance8 = instance8;
            this.instance9 = instance9;
            this.instance10 = instance10;
            this.instance11 = instance11;
            this.instance12 = instance12;
            this.start = start;
        }

        /// <inheritdoc />
        void IStartable.Start()
        {
            start(instance1, instance2, instance3, instance4, instance5, instance6, instance7, instance8, instance9, instance10, instance11, instance12);
        }
    }

    /// <summary>
    /// エントリーポイントを一括管理するコンテナクラス.
    /// </summary>
    /// <typeparam name="T1">発火する1番目のインスタンスの型.</typeparam>
    /// <typeparam name="T2">発火する2番目のインスタンスの型.</typeparam>
    /// <typeparam name="T3">発火する3番目のインスタンスの型.</typeparam>
    /// <typeparam name="T4">発火する4番目のインスタンスの型.</typeparam>
    /// <typeparam name="T5">発火する5番目のインスタンスの型.</typeparam>
    /// <typeparam name="T6">発火する6番目のインスタンスの型.</typeparam>
    /// <typeparam name="T7">発火する7番目のインスタンスの型.</typeparam>
    /// <typeparam name="T8">発火する8番目のインスタンスの型.</typeparam>
    /// <typeparam name="T9">発火する9番目のインスタンスの型.</typeparam>
    /// <typeparam name="T10">発火する10番目のインスタンスの型.</typeparam>
    /// <typeparam name="T11">発火する11番目のインスタンスの型.</typeparam>
    /// <typeparam name="T12">発火する12番目のインスタンスの型.</typeparam>
    /// <typeparam name="T13">発火する13番目のインスタンスの型.</typeparam>
    public sealed class EntryPointContainer<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : IStartable
        where T1 : class where T2 : class where T3 : class where T4 : class where T5 : class where T6 : class where T7 : class where T8 : class where T9 : class where T10 : class where T11 : class where T12 : class where T13 : class
    {
        private readonly T1 instance1;
        private readonly T2 instance2;
        private readonly T3 instance3;
        private readonly T4 instance4;
        private readonly T5 instance5;
        private readonly T6 instance6;
        private readonly T7 instance7;
        private readonly T8 instance8;
        private readonly T9 instance9;
        private readonly T10 instance10;
        private readonly T11 instance11;
        private readonly T12 instance12;
        private readonly T13 instance13;
        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> start;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryPointContainer{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13}"/> class.
        /// </summary>
        /// <param name="instance1">発火する1番目のインスタンス.</param>
        /// <param name="instance2">発火する2番目のインスタンス.</param>
        /// <param name="instance3">発火する3番目のインスタンス.</param>
        /// <param name="instance4">発火する4番目のインスタンス.</param>
        /// <param name="instance5">発火する5番目のインスタンス.</param>
        /// <param name="instance6">発火する6番目のインスタンス.</param>
        /// <param name="instance7">発火する7番目のインスタンス.</param>
        /// <param name="instance8">発火する8番目のインスタンス.</param>
        /// <param name="instance9">発火する9番目のインスタンス.</param>
        /// <param name="instance10">発火する10番目のインスタンス.</param>
        /// <param name="instance11">発火する11番目のインスタンス.</param>
        /// <param name="instance12">発火する12番目のインスタンス.</param>
        /// <param name="instance13">発火する13番目のインスタンス.</param>
        /// <param name="start">発火する処理.</param>
        public EntryPointContainer(
            T1 instance1,
            T2 instance2,
            T3 instance3,
            T4 instance4,
            T5 instance5,
            T6 instance6,
            T7 instance7,
            T8 instance8,
            T9 instance9,
            T10 instance10,
            T11 instance11,
            T12 instance12,
            T13 instance13,
            Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> start)
        {
            this.instance1 = instance1;
            this.instance2 = instance2;
            this.instance3 = instance3;
            this.instance4 = instance4;
            this.instance5 = instance5;
            this.instance6 = instance6;
            this.instance7 = instance7;
            this.instance8 = instance8;
            this.instance9 = instance9;
            this.instance10 = instance10;
            this.instance11 = instance11;
            this.instance12 = instance12;
            this.instance13 = instance13;
            this.start = start;
        }

        /// <inheritdoc />
        void IStartable.Start()
        {
            start(instance1, instance2, instance3, instance4, instance5, instance6, instance7, instance8, instance9, instance10, instance11, instance12, instance13);
        }
    }

    /// <summary>
    /// エントリーポイントを一括管理するコンテナクラス.
    /// </summary>
    /// <typeparam name="T1">発火する1番目のインスタンスの型.</typeparam>
    /// <typeparam name="T2">発火する2番目のインスタンスの型.</typeparam>
    /// <typeparam name="T3">発火する3番目のインスタンスの型.</typeparam>
    /// <typeparam name="T4">発火する4番目のインスタンスの型.</typeparam>
    /// <typeparam name="T5">発火する5番目のインスタンスの型.</typeparam>
    /// <typeparam name="T6">発火する6番目のインスタンスの型.</typeparam>
    /// <typeparam name="T7">発火する7番目のインスタンスの型.</typeparam>
    /// <typeparam name="T8">発火する8番目のインスタンスの型.</typeparam>
    /// <typeparam name="T9">発火する9番目のインスタンスの型.</typeparam>
    /// <typeparam name="T10">発火する10番目のインスタンスの型.</typeparam>
    /// <typeparam name="T11">発火する11番目のインスタンスの型.</typeparam>
    /// <typeparam name="T12">発火する12番目のインスタンスの型.</typeparam>
    /// <typeparam name="T13">発火する13番目のインスタンスの型.</typeparam>
    /// <typeparam name="T14">発火する14番目のインスタンスの型.</typeparam>
    public sealed class EntryPointContainer<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : IStartable
        where T1 : class where T2 : class where T3 : class where T4 : class where T5 : class where T6 : class where T7 : class where T8 : class where T9 : class where T10 : class where T11 : class where T12 : class where T13 : class where T14 : class
    {
        private readonly T1 instance1;
        private readonly T2 instance2;
        private readonly T3 instance3;
        private readonly T4 instance4;
        private readonly T5 instance5;
        private readonly T6 instance6;
        private readonly T7 instance7;
        private readonly T8 instance8;
        private readonly T9 instance9;
        private readonly T10 instance10;
        private readonly T11 instance11;
        private readonly T12 instance12;
        private readonly T13 instance13;
        private readonly T14 instance14;
        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> start;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryPointContainer{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14}"/> class.
        /// </summary>
        /// <param name="instance1">発火する1番目のインスタンス.</param>
        /// <param name="instance2">発火する2番目のインスタンス.</param>
        /// <param name="instance3">発火する3番目のインスタンス.</param>
        /// <param name="instance4">発火する4番目のインスタンス.</param>
        /// <param name="instance5">発火する5番目のインスタンス.</param>
        /// <param name="instance6">発火する6番目のインスタンス.</param>
        /// <param name="instance7">発火する7番目のインスタンス.</param>
        /// <param name="instance8">発火する8番目のインスタンス.</param>
        /// <param name="instance9">発火する9番目のインスタンス.</param>
        /// <param name="instance10">発火する10番目のインスタンス.</param>
        /// <param name="instance11">発火する11番目のインスタンス.</param>
        /// <param name="instance12">発火する12番目のインスタンス.</param>
        /// <param name="instance13">発火する13番目のインスタンス.</param>
        /// <param name="instance14">発火する14番目のインスタンス.</param>
        /// <param name="start">発火する処理.</param>
        public EntryPointContainer(
            T1 instance1,
            T2 instance2,
            T3 instance3,
            T4 instance4,
            T5 instance5,
            T6 instance6,
            T7 instance7,
            T8 instance8,
            T9 instance9,
            T10 instance10,
            T11 instance11,
            T12 instance12,
            T13 instance13,
            T14 instance14,
            Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> start)
        {
            this.instance1 = instance1;
            this.instance2 = instance2;
            this.instance3 = instance3;
            this.instance4 = instance4;
            this.instance5 = instance5;
            this.instance6 = instance6;
            this.instance7 = instance7;
            this.instance8 = instance8;
            this.instance9 = instance9;
            this.instance10 = instance10;
            this.instance11 = instance11;
            this.instance12 = instance12;
            this.instance13 = instance13;
            this.instance14 = instance14;
            this.start = start;
        }

        /// <inheritdoc />
        void IStartable.Start()
        {
            start(instance1, instance2, instance3, instance4, instance5, instance6, instance7, instance8, instance9, instance10, instance11, instance12, instance13, instance14);
        }
    }

    /// <summary>
    /// エントリーポイントを一括管理するコンテナクラス.
    /// </summary>
    /// <typeparam name="T1">発火する1番目のインスタンスの型.</typeparam>
    /// <typeparam name="T2">発火する2番目のインスタンスの型.</typeparam>
    /// <typeparam name="T3">発火する3番目のインスタンスの型.</typeparam>
    /// <typeparam name="T4">発火する4番目のインスタンスの型.</typeparam>
    /// <typeparam name="T5">発火する5番目のインスタンスの型.</typeparam>
    /// <typeparam name="T6">発火する6番目のインスタンスの型.</typeparam>
    /// <typeparam name="T7">発火する7番目のインスタンスの型.</typeparam>
    /// <typeparam name="T8">発火する8番目のインスタンスの型.</typeparam>
    /// <typeparam name="T9">発火する9番目のインスタンスの型.</typeparam>
    /// <typeparam name="T10">発火する10番目のインスタンスの型.</typeparam>
    /// <typeparam name="T11">発火する11番目のインスタンスの型.</typeparam>
    /// <typeparam name="T12">発火する12番目のインスタンスの型.</typeparam>
    /// <typeparam name="T13">発火する13番目のインスタンスの型.</typeparam>
    /// <typeparam name="T14">発火する14番目のインスタンスの型.</typeparam>
    /// <typeparam name="T15">発火する15番目のインスタンスの型.</typeparam>
    public sealed class EntryPointContainer<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : IStartable
        where T1 : class where T2 : class where T3 : class where T4 : class where T5 : class where T6 : class where T7 : class where T8 : class where T9 : class where T10 : class where T11 : class where T12 : class where T13 : class where T14 : class where T15 : class
    {
        private readonly T1 instance1;
        private readonly T2 instance2;
        private readonly T3 instance3;
        private readonly T4 instance4;
        private readonly T5 instance5;
        private readonly T6 instance6;
        private readonly T7 instance7;
        private readonly T8 instance8;
        private readonly T9 instance9;
        private readonly T10 instance10;
        private readonly T11 instance11;
        private readonly T12 instance12;
        private readonly T13 instance13;
        private readonly T14 instance14;
        private readonly T15 instance15;
        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> start;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryPointContainer{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15}"/> class.
        /// </summary>
        /// <param name="instance1">発火する1番目のインスタンス.</param>
        /// <param name="instance2">発火する2番目のインスタンス.</param>
        /// <param name="instance3">発火する3番目のインスタンス.</param>
        /// <param name="instance4">発火する4番目のインスタンス.</param>
        /// <param name="instance5">発火する5番目のインスタンス.</param>
        /// <param name="instance6">発火する6番目のインスタンス.</param>
        /// <param name="instance7">発火する7番目のインスタンス.</param>
        /// <param name="instance8">発火する8番目のインスタンス.</param>
        /// <param name="instance9">発火する9番目のインスタンス.</param>
        /// <param name="instance10">発火する10番目のインスタンス.</param>
        /// <param name="instance11">発火する11番目のインスタンス.</param>
        /// <param name="instance12">発火する12番目のインスタンス.</param>
        /// <param name="instance13">発火する13番目のインスタンス.</param>
        /// <param name="instance14">発火する14番目のインスタンス.</param>
        /// <param name="instance15">発火する15番目のインスタンス.</param>
        /// <param name="start">発火する処理.</param>
        public EntryPointContainer(
            T1 instance1,
            T2 instance2,
            T3 instance3,
            T4 instance4,
            T5 instance5,
            T6 instance6,
            T7 instance7,
            T8 instance8,
            T9 instance9,
            T10 instance10,
            T11 instance11,
            T12 instance12,
            T13 instance13,
            T14 instance14,
            T15 instance15,
            Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> start)
        {
            this.instance1 = instance1;
            this.instance2 = instance2;
            this.instance3 = instance3;
            this.instance4 = instance4;
            this.instance5 = instance5;
            this.instance6 = instance6;
            this.instance7 = instance7;
            this.instance8 = instance8;
            this.instance9 = instance9;
            this.instance10 = instance10;
            this.instance11 = instance11;
            this.instance12 = instance12;
            this.instance13 = instance13;
            this.instance14 = instance14;
            this.instance15 = instance15;
            this.start = start;
        }

        /// <inheritdoc />
        void IStartable.Start()
        {
            start(instance1, instance2, instance3, instance4, instance5, instance6, instance7, instance8, instance9, instance10, instance11, instance12, instance13, instance14, instance15);
        }
    }

    /// <summary>
    /// エントリーポイントを一括管理するコンテナクラス.
    /// </summary>
    /// <typeparam name="T1">発火する1番目のインスタンスの型.</typeparam>
    /// <typeparam name="T2">発火する2番目のインスタンスの型.</typeparam>
    /// <typeparam name="T3">発火する3番目のインスタンスの型.</typeparam>
    /// <typeparam name="T4">発火する4番目のインスタンスの型.</typeparam>
    /// <typeparam name="T5">発火する5番目のインスタンスの型.</typeparam>
    /// <typeparam name="T6">発火する6番目のインスタンスの型.</typeparam>
    /// <typeparam name="T7">発火する7番目のインスタンスの型.</typeparam>
    /// <typeparam name="T8">発火する8番目のインスタンスの型.</typeparam>
    /// <typeparam name="T9">発火する9番目のインスタンスの型.</typeparam>
    /// <typeparam name="T10">発火する10番目のインスタンスの型.</typeparam>
    /// <typeparam name="T11">発火する11番目のインスタンスの型.</typeparam>
    /// <typeparam name="T12">発火する12番目のインスタンスの型.</typeparam>
    /// <typeparam name="T13">発火する13番目のインスタンスの型.</typeparam>
    /// <typeparam name="T14">発火する14番目のインスタンスの型.</typeparam>
    /// <typeparam name="T15">発火する15番目のインスタンスの型.</typeparam>
    /// <typeparam name="T16">発火する16番目のインスタンスの型.</typeparam>
    public sealed class EntryPointContainer<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> : IStartable
        where T1 : class where T2 : class where T3 : class where T4 : class where T5 : class where T6 : class where T7 : class where T8 : class where T9 : class where T10 : class where T11 : class where T12 : class where T13 : class where T14 : class where T15 : class where T16 : class
    {
        private readonly T1 instance1;
        private readonly T2 instance2;
        private readonly T3 instance3;
        private readonly T4 instance4;
        private readonly T5 instance5;
        private readonly T6 instance6;
        private readonly T7 instance7;
        private readonly T8 instance8;
        private readonly T9 instance9;
        private readonly T10 instance10;
        private readonly T11 instance11;
        private readonly T12 instance12;
        private readonly T13 instance13;
        private readonly T14 instance14;
        private readonly T15 instance15;
        private readonly T16 instance16;
        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> start;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryPointContainer{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16}"/> class.
        /// </summary>
        /// <param name="instance1">発火する1番目のインスタンス.</param>
        /// <param name="instance2">発火する2番目のインスタンス.</param>
        /// <param name="instance3">発火する3番目のインスタンス.</param>
        /// <param name="instance4">発火する4番目のインスタンス.</param>
        /// <param name="instance5">発火する5番目のインスタンス.</param>
        /// <param name="instance6">発火する6番目のインスタンス.</param>
        /// <param name="instance7">発火する7番目のインスタンス.</param>
        /// <param name="instance8">発火する8番目のインスタンス.</param>
        /// <param name="instance9">発火する9番目のインスタンス.</param>
        /// <param name="instance10">発火する10番目のインスタンス.</param>
        /// <param name="instance11">発火する11番目のインスタンス.</param>
        /// <param name="instance12">発火する12番目のインスタンス.</param>
        /// <param name="instance13">発火する13番目のインスタンス.</param>
        /// <param name="instance14">発火する14番目のインスタンス.</param>
        /// <param name="instance15">発火する15番目のインスタンス.</param>
        /// <param name="instance16">発火する16番目のインスタンス.</param>
        /// <param name="start">発火する処理.</param>
        public EntryPointContainer(
            T1 instance1,
            T2 instance2,
            T3 instance3,
            T4 instance4,
            T5 instance5,
            T6 instance6,
            T7 instance7,
            T8 instance8,
            T9 instance9,
            T10 instance10,
            T11 instance11,
            T12 instance12,
            T13 instance13,
            T14 instance14,
            T15 instance15,
            T16 instance16,
            Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> start)
        {
            this.instance1 = instance1;
            this.instance2 = instance2;
            this.instance3 = instance3;
            this.instance4 = instance4;
            this.instance5 = instance5;
            this.instance6 = instance6;
            this.instance7 = instance7;
            this.instance8 = instance8;
            this.instance9 = instance9;
            this.instance10 = instance10;
            this.instance11 = instance11;
            this.instance12 = instance12;
            this.instance13 = instance13;
            this.instance14 = instance14;
            this.instance15 = instance15;
            this.instance16 = instance16;
            this.start = start;
        }

        /// <inheritdoc />
        void IStartable.Start()
        {
            start(instance1, instance2, instance3, instance4, instance5, instance6, instance7, instance8, instance9, instance10, instance11, instance12, instance13, instance14, instance15, instance16);
        }
    }
}

#pragma warning restore SA1402