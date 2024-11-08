#pragma warning disable SA1204
#pragma warning disable SA1402

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using VContainer;
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
        private readonly CancellationToken cancellationToken;
        private readonly Action<T1, CancellationToken> start;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryPointContainer{T1}"/> class.
        /// </summary>
        /// <param name="instance1">発火する1番目のインスタンス.</param>
        /// <param name="cancellationToken">キャンセルトークン.</param>
        /// <param name="start">発火する処理.</param>
        public EntryPointContainer(
            T1 instance1,
            CancellationToken cancellationToken,
            Action<T1, CancellationToken> start)
        {
            this.instance1 = instance1;
            this.cancellationToken = cancellationToken;
            this.start = start;
        }

        /// <inheritdoc />
        void IStartable.Start()
        {
            start(instance1, cancellationToken);
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
        private readonly CancellationToken cancellationToken;
        private readonly Action<T1, T2, CancellationToken> start;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryPointContainer{T1, T2}"/> class.
        /// </summary>
        /// <param name="instance1">発火する1番目のインスタンス.</param>
        /// <param name="instance2">発火する2番目のインスタンス.</param>
        /// <param name="cancellationToken">キャンセルトークン.</param>
        /// <param name="start">発火する処理.</param>
        public EntryPointContainer(
            T1 instance1,
            T2 instance2,
            CancellationToken cancellationToken,
            Action<T1, T2, CancellationToken> start)
        {
            this.instance1 = instance1;
            this.instance2 = instance2;
            this.cancellationToken = cancellationToken;
            this.start = start;
        }

        /// <inheritdoc />
        void IStartable.Start()
        {
            start(instance1, instance2, cancellationToken);
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
        private readonly CancellationToken cancellationToken;
        private readonly Action<T1, T2, T3, CancellationToken> start;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryPointContainer{T1, T2, T3}"/> class.
        /// </summary>
        /// <param name="instance1">発火する1番目のインスタンス.</param>
        /// <param name="instance2">発火する2番目のインスタンス.</param>
        /// <param name="instance3">発火する3番目のインスタンス.</param>
        /// <param name="cancellationToken">キャンセルトークン.</param>
        /// <param name="start">発火する処理.</param>
        public EntryPointContainer(
            T1 instance1,
            T2 instance2,
            T3 instance3,
            CancellationToken cancellationToken,
            Action<T1, T2, T3, CancellationToken> start)
        {
            this.instance1 = instance1;
            this.instance2 = instance2;
            this.instance3 = instance3;
            this.cancellationToken = cancellationToken;
            this.start = start;
        }

        /// <inheritdoc />
        void IStartable.Start()
        {
            start(instance1, instance2, instance3, cancellationToken);
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
        private readonly CancellationToken cancellationToken;
        private readonly Action<T1, T2, T3, T4, CancellationToken> start;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryPointContainer{T1, T2, T3, T4}"/> class.
        /// </summary>
        /// <param name="instance1">発火する1番目のインスタンス.</param>
        /// <param name="instance2">発火する2番目のインスタンス.</param>
        /// <param name="instance3">発火する3番目のインスタンス.</param>
        /// <param name="instance4">発火する4番目のインスタンス.</param>
        /// <param name="cancellationToken">キャンセルトークン.</param>
        /// <param name="start">発火する処理.</param>
        public EntryPointContainer(
            T1 instance1,
            T2 instance2,
            T3 instance3,
            T4 instance4,
            CancellationToken cancellationToken,
            Action<T1, T2, T3, T4, CancellationToken> start)
        {
            this.instance1 = instance1;
            this.instance2 = instance2;
            this.instance3 = instance3;
            this.instance4 = instance4;
            this.cancellationToken = cancellationToken;
            this.start = start;
        }

        /// <inheritdoc />
        void IStartable.Start()
        {
            start(instance1, instance2, instance3, instance4, cancellationToken);
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
        private readonly CancellationToken cancellationToken;
        private readonly Action<T1, T2, T3, T4, T5, CancellationToken> start;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryPointContainer{T1, T2, T3, T4, T5}"/> class.
        /// </summary>
        /// <param name="instance1">発火する1番目のインスタンス.</param>
        /// <param name="instance2">発火する2番目のインスタンス.</param>
        /// <param name="instance3">発火する3番目のインスタンス.</param>
        /// <param name="instance4">発火する4番目のインスタンス.</param>
        /// <param name="instance5">発火する5番目のインスタンス.</param>
        /// <param name="cancellationToken">キャンセルトークン.</param>
        /// <param name="start">発火する処理.</param>
        public EntryPointContainer(
            T1 instance1,
            T2 instance2,
            T3 instance3,
            T4 instance4,
            T5 instance5,
            CancellationToken cancellationToken,
            Action<T1, T2, T3, T4, T5, CancellationToken> start)
        {
            this.instance1 = instance1;
            this.instance2 = instance2;
            this.instance3 = instance3;
            this.instance4 = instance4;
            this.instance5 = instance5;
            this.cancellationToken = cancellationToken;
            this.start = start;
        }

        /// <inheritdoc />
        void IStartable.Start()
        {
            start(instance1, instance2, instance3, instance4, instance5, cancellationToken);
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
        private readonly CancellationToken cancellationToken;
        private readonly Action<T1, T2, T3, T4, T5, T6, CancellationToken> start;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryPointContainer{T1, T2, T3, T4, T5, T6}"/> class.
        /// </summary>
        /// <param name="instance1">発火する1番目のインスタンス.</param>
        /// <param name="instance2">発火する2番目のインスタンス.</param>
        /// <param name="instance3">発火する3番目のインスタンス.</param>
        /// <param name="instance4">発火する4番目のインスタンス.</param>
        /// <param name="instance5">発火する5番目のインスタンス.</param>
        /// <param name="instance6">発火する6番目のインスタンス.</param>
        /// <param name="cancellationToken">キャンセルトークン.</param>
        /// <param name="start">発火する処理.</param>
        public EntryPointContainer(
            T1 instance1,
            T2 instance2,
            T3 instance3,
            T4 instance4,
            T5 instance5,
            T6 instance6,
            CancellationToken cancellationToken,
            Action<T1, T2, T3, T4, T5, T6, CancellationToken> start)
        {
            this.instance1 = instance1;
            this.instance2 = instance2;
            this.instance3 = instance3;
            this.instance4 = instance4;
            this.instance5 = instance5;
            this.instance6 = instance6;
            this.cancellationToken = cancellationToken;
            this.start = start;
        }

        /// <inheritdoc />
        void IStartable.Start()
        {
            start(instance1, instance2, instance3, instance4, instance5, instance6, cancellationToken);
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
        private readonly CancellationToken cancellationToken;
        private readonly Action<T1, T2, T3, T4, T5, T6, T7, CancellationToken> start;

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
        /// <param name="cancellationToken">キャンセルトークン.</param>
        /// <param name="start">発火する処理.</param>
        public EntryPointContainer(
            T1 instance1,
            T2 instance2,
            T3 instance3,
            T4 instance4,
            T5 instance5,
            T6 instance6,
            T7 instance7,
            CancellationToken cancellationToken,
            Action<T1, T2, T3, T4, T5, T6, T7, CancellationToken> start)
        {
            this.instance1 = instance1;
            this.instance2 = instance2;
            this.instance3 = instance3;
            this.instance4 = instance4;
            this.instance5 = instance5;
            this.instance6 = instance6;
            this.instance7 = instance7;
            this.cancellationToken = cancellationToken;
            this.start = start;
        }

        /// <inheritdoc />
        void IStartable.Start()
        {
            start(instance1, instance2, instance3, instance4, instance5, instance6, instance7, cancellationToken);
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
        private readonly CancellationToken cancellationToken;
        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, CancellationToken> start;

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
        /// <param name="cancellationToken">キャンセルトークン.</param>
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
            CancellationToken cancellationToken,
            Action<T1, T2, T3, T4, T5, T6, T7, T8, CancellationToken> start)
        {
            this.instance1 = instance1;
            this.instance2 = instance2;
            this.instance3 = instance3;
            this.instance4 = instance4;
            this.instance5 = instance5;
            this.instance6 = instance6;
            this.instance7 = instance7;
            this.instance8 = instance8;
            this.cancellationToken = cancellationToken;
            this.start = start;
        }

        /// <inheritdoc />
        void IStartable.Start()
        {
            start(instance1, instance2, instance3, instance4, instance5, instance6, instance7, instance8, cancellationToken);
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
        private readonly CancellationToken cancellationToken;
        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, CancellationToken> start;

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
        /// <param name="cancellationToken">キャンセルトークン.</param>
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
            CancellationToken cancellationToken,
            Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, CancellationToken> start)
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
            this.cancellationToken = cancellationToken;
            this.start = start;
        }

        /// <inheritdoc />
        void IStartable.Start()
        {
            start(instance1, instance2, instance3, instance4, instance5, instance6, instance7, instance8, instance9, cancellationToken);
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
        private readonly CancellationToken cancellationToken;
        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, CancellationToken> start;

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
        /// <param name="cancellationToken">キャンセルトークン.</param>
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
            CancellationToken cancellationToken,
            Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, CancellationToken> start)
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
            this.cancellationToken = cancellationToken;
            this.start = start;
        }

        /// <inheritdoc />
        void IStartable.Start()
        {
            start(instance1, instance2, instance3, instance4, instance5, instance6, instance7, instance8, instance9, instance10, cancellationToken);
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
        private readonly CancellationToken cancellationToken;
        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, CancellationToken> start;

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
        /// <param name="cancellationToken">キャンセルトークン.</param>
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
            CancellationToken cancellationToken,
            Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, CancellationToken> start)
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
            this.cancellationToken = cancellationToken;
            this.start = start;
        }

        /// <inheritdoc />
        void IStartable.Start()
        {
            start(instance1, instance2, instance3, instance4, instance5, instance6, instance7, instance8, instance9, instance10, instance11, cancellationToken);
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
        private readonly CancellationToken cancellationToken;
        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, CancellationToken> start;

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
        /// <param name="cancellationToken">キャンセルトークン.</param>
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
            CancellationToken cancellationToken,
            Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, CancellationToken> start)
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
            this.cancellationToken = cancellationToken;
            this.start = start;
        }

        /// <inheritdoc />
        void IStartable.Start()
        {
            start(instance1, instance2, instance3, instance4, instance5, instance6, instance7, instance8, instance9, instance10, instance11, instance12, cancellationToken);
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
        private readonly CancellationToken cancellationToken;
        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, CancellationToken> start;

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
        /// <param name="cancellationToken">キャンセルトークン.</param>
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
            CancellationToken cancellationToken,
            Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, CancellationToken> start)
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
            this.cancellationToken = cancellationToken;
            this.start = start;
        }

        /// <inheritdoc />
        void IStartable.Start()
        {
            start(instance1, instance2, instance3, instance4, instance5, instance6, instance7, instance8, instance9, instance10, instance11, instance12, instance13, cancellationToken);
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
        private readonly CancellationToken cancellationToken;
        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, CancellationToken> start;

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
        /// <param name="cancellationToken">キャンセルトークン.</param>
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
            CancellationToken cancellationToken,
            Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, CancellationToken> start)
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
            this.cancellationToken = cancellationToken;
            this.start = start;
        }

        /// <inheritdoc />
        void IStartable.Start()
        {
            start(instance1, instance2, instance3, instance4, instance5, instance6, instance7, instance8, instance9, instance10, instance11, instance12, instance13, instance14, cancellationToken);
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
        private readonly CancellationToken cancellationToken;
        private readonly Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, CancellationToken> start;

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
        /// <param name="cancellationToken">キャンセルトークン.</param>
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
            CancellationToken cancellationToken,
            Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, CancellationToken> start)
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
            this.cancellationToken = cancellationToken;
            this.start = start;
        }

        /// <inheritdoc />
        void IStartable.Start()
        {
            start(instance1, instance2, instance3, instance4, instance5, instance6, instance7, instance8, instance9, instance10, instance11, instance12, instance13, instance14, instance15, cancellationToken);
        }
    }

    /// <summary>
    /// <see cref="VContainer"/>の拡張メソッド.
    /// </summary>
    public static class VContainerEntryPointExtensions
    {
        /// <summary>
        /// <see cref="EntryPointContainer{T1}"/>に登録するエントリーポイント制御引数.
        /// </summary>
        /// <param name="builder"><see cref="RegistrationBuilder"/>.</param>
        /// <param name="entry">エントリーポイント制御引数.</param>
        /// <typeparam name="T1">エントリーポイント制御引数の1番目の型.</typeparam>
        /// <returns><see cref="RegistrationBuilder.WithParameter{T}(T)"/>で登録済みの<see cref="RegistrationBuilder"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RegistrationBuilder WithEntryParameter<T1>(this RegistrationBuilder builder, Action<T1, CancellationToken> entry)
        {
            return builder.WithParameter(entry);
        }

        /// <summary>
        /// <see cref="EntryPointContainer{T1, T2}"/>に登録するエントリーポイント制御引数.
        /// </summary>
        /// <param name="builder"><see cref="RegistrationBuilder"/>.</param>
        /// <param name="entry">エントリーポイント制御引数.</param>
        /// <typeparam name="T1">エントリーポイント制御引数の1番目の型.</typeparam>
        /// <typeparam name="T2">エントリーポイント制御引数の2番目の型.</typeparam>
        /// <returns><see cref="RegistrationBuilder.WithParameter{T}(T)"/>で登録済みの<see cref="RegistrationBuilder"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RegistrationBuilder WithEntryParameter<T1, T2>(this RegistrationBuilder builder, Action<T1, T2, CancellationToken> entry)
        {
            return builder.WithParameter(entry);
        }

        /// <summary>
        /// <see cref="EntryPointContainer{T1, T2, T3}"/>に登録するエントリーポイント制御引数.
        /// </summary>
        /// <param name="builder"><see cref="RegistrationBuilder"/>.</param>
        /// <param name="entry">エントリーポイント制御引数.</param>
        /// <typeparam name="T1">エントリーポイント制御引数の1番目の型.</typeparam>
        /// <typeparam name="T2">エントリーポイント制御引数の2番目の型.</typeparam>
        /// <typeparam name="T3">エントリーポイント制御引数の3番目の型.</typeparam>
        /// <returns><see cref="RegistrationBuilder.WithParameter{T}(T)"/>で登録済みの<see cref="RegistrationBuilder"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RegistrationBuilder WithEntryParameter<T1, T2, T3>(this RegistrationBuilder builder, Action<T1, T2, T3, CancellationToken> entry)
        {
            return builder.WithParameter(entry);
        }

        /// <summary>
        /// <see cref="EntryPointContainer{T1, T2, T3, T4}"/>に登録するエントリーポイント制御引数.
        /// </summary>
        /// <param name="builder"><see cref="RegistrationBuilder"/>.</param>
        /// <param name="entry">エントリーポイント制御引数.</param>
        /// <typeparam name="T1">エントリーポイント制御引数の1番目の型.</typeparam>
        /// <typeparam name="T2">エントリーポイント制御引数の2番目の型.</typeparam>
        /// <typeparam name="T3">エントリーポイント制御引数の3番目の型.</typeparam>
        /// <typeparam name="T4">エントリーポイント制御引数の4番目の型.</typeparam>
        /// <returns><see cref="RegistrationBuilder.WithParameter{T}(T)"/>で登録済みの<see cref="RegistrationBuilder"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RegistrationBuilder WithEntryParameter<T1, T2, T3, T4>(this RegistrationBuilder builder, Action<T1, T2, T3, T4, CancellationToken> entry)
        {
            return builder.WithParameter(entry);
        }

        /// <summary>
        /// <see cref="EntryPointContainer{T1, T2, T3, T4, T5}"/>に登録するエントリーポイント制御引数.
        /// </summary>
        /// <param name="builder"><see cref="RegistrationBuilder"/>.</param>
        /// <param name="entry">エントリーポイント制御引数.</param>
        /// <typeparam name="T1">エントリーポイント制御引数の1番目の型.</typeparam>
        /// <typeparam name="T2">エントリーポイント制御引数の2番目の型.</typeparam>
        /// <typeparam name="T3">エントリーポイント制御引数の3番目の型.</typeparam>
        /// <typeparam name="T4">エントリーポイント制御引数の4番目の型.</typeparam>
        /// <typeparam name="T5">エントリーポイント制御引数の5番目の型.</typeparam>
        /// <returns><see cref="RegistrationBuilder.WithParameter{T}(T)"/>で登録済みの<see cref="RegistrationBuilder"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RegistrationBuilder WithEntryParameter<T1, T2, T3, T4, T5>(this RegistrationBuilder builder, Action<T1, T2, T3, T4, T5, CancellationToken> entry)
        {
            return builder.WithParameter(entry);
        }

        /// <summary>
        /// <see cref="EntryPointContainer{T1, T2, T3, T4, T5, T6}"/>に登録するエントリーポイント制御引数.
        /// </summary>
        /// <param name="builder"><see cref="RegistrationBuilder"/>.</param>
        /// <param name="entry">エントリーポイント制御引数.</param>
        /// <typeparam name="T1">エントリーポイント制御引数の1番目の型.</typeparam>
        /// <typeparam name="T2">エントリーポイント制御引数の2番目の型.</typeparam>
        /// <typeparam name="T3">エントリーポイント制御引数の3番目の型.</typeparam>
        /// <typeparam name="T4">エントリーポイント制御引数の4番目の型.</typeparam>
        /// <typeparam name="T5">エントリーポイント制御引数の5番目の型.</typeparam>
        /// <typeparam name="T6">エントリーポイント制御引数の6番目の型.</typeparam>
        /// <returns><see cref="RegistrationBuilder.WithParameter{T}(T)"/>で登録済みの<see cref="RegistrationBuilder"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RegistrationBuilder WithEntryParameter<T1, T2, T3, T4, T5, T6>(this RegistrationBuilder builder, Action<T1, T2, T3, T4, T5, T6, CancellationToken> entry)
        {
            return builder.WithParameter(entry);
        }

        /// <summary>
        /// <see cref="EntryPointContainer{T1, T2, T3, T4, T5, T6, T7}"/>に登録するエントリーポイント制御引数.
        /// </summary>
        /// <param name="builder"><see cref="RegistrationBuilder"/>.</param>
        /// <param name="entry">エントリーポイント制御引数.</param>
        /// <typeparam name="T1">エントリーポイント制御引数の1番目の型.</typeparam>
        /// <typeparam name="T2">エントリーポイント制御引数の2番目の型.</typeparam>
        /// <typeparam name="T3">エントリーポイント制御引数の3番目の型.</typeparam>
        /// <typeparam name="T4">エントリーポイント制御引数の4番目の型.</typeparam>
        /// <typeparam name="T5">エントリーポイント制御引数の5番目の型.</typeparam>
        /// <typeparam name="T6">エントリーポイント制御引数の6番目の型.</typeparam>
        /// <typeparam name="T7">エントリーポイント制御引数の7番目の型.</typeparam>
        /// <returns><see cref="RegistrationBuilder.WithParameter{T}(T)"/>で登録済みの<see cref="RegistrationBuilder"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RegistrationBuilder WithEntryParameter<T1, T2, T3, T4, T5, T6, T7>(this RegistrationBuilder builder, Action<T1, T2, T3, T4, T5, T6, T7, CancellationToken> entry)
        {
            return builder.WithParameter(entry);
        }

        /// <summary>
        /// <see cref="EntryPointContainer{T1, T2, T3, T4, T5, T6, T7, T8}"/>に登録するエントリーポイント制御引数.
        /// </summary>
        /// <param name="builder"><see cref="RegistrationBuilder"/>.</param>
        /// <param name="entry">エントリーポイント制御引数.</param>
        /// <typeparam name="T1">エントリーポイント制御引数の1番目の型.</typeparam>
        /// <typeparam name="T2">エントリーポイント制御引数の2番目の型.</typeparam>
        /// <typeparam name="T3">エントリーポイント制御引数の3番目の型.</typeparam>
        /// <typeparam name="T4">エントリーポイント制御引数の4番目の型.</typeparam>
        /// <typeparam name="T5">エントリーポイント制御引数の5番目の型.</typeparam>
        /// <typeparam name="T6">エントリーポイント制御引数の6番目の型.</typeparam>
        /// <typeparam name="T7">エントリーポイント制御引数の7番目の型.</typeparam>
        /// <typeparam name="T8">エントリーポイント制御引数の8番目の型.</typeparam>
        /// <returns><see cref="RegistrationBuilder.WithParameter{T}(T)"/>で登録済みの<see cref="RegistrationBuilder"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RegistrationBuilder WithEntryParameter<T1, T2, T3, T4, T5, T6, T7, T8>(this RegistrationBuilder builder, Action<T1, T2, T3, T4, T5, T6, T7, T8, CancellationToken> entry)
        {
            return builder.WithParameter(entry);
        }

        /// <summary>
        /// <see cref="EntryPointContainer{T1, T2, T3, T4, T5, T6, T7, T8, T9}"/>に登録するエントリーポイント制御引数.
        /// </summary>
        /// <param name="builder"><see cref="RegistrationBuilder"/>.</param>
        /// <param name="entry">エントリーポイント制御引数.</param>
        /// <typeparam name="T1">エントリーポイント制御引数の1番目の型.</typeparam>
        /// <typeparam name="T2">エントリーポイント制御引数の2番目の型.</typeparam>
        /// <typeparam name="T3">エントリーポイント制御引数の3番目の型.</typeparam>
        /// <typeparam name="T4">エントリーポイント制御引数の4番目の型.</typeparam>
        /// <typeparam name="T5">エントリーポイント制御引数の5番目の型.</typeparam>
        /// <typeparam name="T6">エントリーポイント制御引数の6番目の型.</typeparam>
        /// <typeparam name="T7">エントリーポイント制御引数の7番目の型.</typeparam>
        /// <typeparam name="T8">エントリーポイント制御引数の8番目の型.</typeparam>
        /// <typeparam name="T9">エントリーポイント制御引数の9番目の型.</typeparam>
        /// <returns><see cref="RegistrationBuilder.WithParameter{T}(T)"/>で登録済みの<see cref="RegistrationBuilder"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RegistrationBuilder WithEntryParameter<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this RegistrationBuilder builder, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, CancellationToken> entry)
        {
            return builder.WithParameter(entry);
        }

        /// <summary>
        /// <see cref="EntryPointContainer{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10}"/>に登録するエントリーポイント制御引数.
        /// </summary>
        /// <param name="builder"><see cref="RegistrationBuilder"/>.</param>
        /// <param name="entry">エントリーポイント制御引数.</param>
        /// <typeparam name="T1">エントリーポイント制御引数の1番目の型.</typeparam>
        /// <typeparam name="T2">エントリーポイント制御引数の2番目の型.</typeparam>
        /// <typeparam name="T3">エントリーポイント制御引数の3番目の型.</typeparam>
        /// <typeparam name="T4">エントリーポイント制御引数の4番目の型.</typeparam>
        /// <typeparam name="T5">エントリーポイント制御引数の5番目の型.</typeparam>
        /// <typeparam name="T6">エントリーポイント制御引数の6番目の型.</typeparam>
        /// <typeparam name="T7">エントリーポイント制御引数の7番目の型.</typeparam>
        /// <typeparam name="T8">エントリーポイント制御引数の8番目の型.</typeparam>
        /// <typeparam name="T9">エントリーポイント制御引数の9番目の型.</typeparam>
        /// <typeparam name="T10">エントリーポイント制御引数の10番目の型.</typeparam>
        /// <returns><see cref="RegistrationBuilder.WithParameter{T}(T)"/>で登録済みの<see cref="RegistrationBuilder"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RegistrationBuilder WithEntryParameter<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this RegistrationBuilder builder, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, CancellationToken> entry)
        {
            return builder.WithParameter(entry);
        }

        /// <summary>
        /// <see cref="EntryPointContainer{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11}"/>に登録するエントリーポイント制御引数.
        /// </summary>
        /// <param name="builder"><see cref="RegistrationBuilder"/>.</param>
        /// <param name="entry">エントリーポイント制御引数.</param>
        /// <typeparam name="T1">エントリーポイント制御引数の1番目の型.</typeparam>
        /// <typeparam name="T2">エントリーポイント制御引数の2番目の型.</typeparam>
        /// <typeparam name="T3">エントリーポイント制御引数の3番目の型.</typeparam>
        /// <typeparam name="T4">エントリーポイント制御引数の4番目の型.</typeparam>
        /// <typeparam name="T5">エントリーポイント制御引数の5番目の型.</typeparam>
        /// <typeparam name="T6">エントリーポイント制御引数の6番目の型.</typeparam>
        /// <typeparam name="T7">エントリーポイント制御引数の7番目の型.</typeparam>
        /// <typeparam name="T8">エントリーポイント制御引数の8番目の型.</typeparam>
        /// <typeparam name="T9">エントリーポイント制御引数の9番目の型.</typeparam>
        /// <typeparam name="T10">エントリーポイント制御引数の10番目の型.</typeparam>
        /// <typeparam name="T11">エントリーポイント制御引数の11番目の型.</typeparam>
        /// <returns><see cref="RegistrationBuilder.WithParameter{T}(T)"/>で登録済みの<see cref="RegistrationBuilder"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RegistrationBuilder WithEntryParameter<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this RegistrationBuilder builder, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, CancellationToken> entry)
        {
            return builder.WithParameter(entry);
        }

        /// <summary>
        /// <see cref="EntryPointContainer{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12}"/>に登録するエントリーポイント制御引数.
        /// </summary>
        /// <param name="builder"><see cref="RegistrationBuilder"/>.</param>
        /// <param name="entry">エントリーポイント制御引数.</param>
        /// <typeparam name="T1">エントリーポイント制御引数の1番目の型.</typeparam>
        /// <typeparam name="T2">エントリーポイント制御引数の2番目の型.</typeparam>
        /// <typeparam name="T3">エントリーポイント制御引数の3番目の型.</typeparam>
        /// <typeparam name="T4">エントリーポイント制御引数の4番目の型.</typeparam>
        /// <typeparam name="T5">エントリーポイント制御引数の5番目の型.</typeparam>
        /// <typeparam name="T6">エントリーポイント制御引数の6番目の型.</typeparam>
        /// <typeparam name="T7">エントリーポイント制御引数の7番目の型.</typeparam>
        /// <typeparam name="T8">エントリーポイント制御引数の8番目の型.</typeparam>
        /// <typeparam name="T9">エントリーポイント制御引数の9番目の型.</typeparam>
        /// <typeparam name="T10">エントリーポイント制御引数の10番目の型.</typeparam>
        /// <typeparam name="T11">エントリーポイント制御引数の11番目の型.</typeparam>
        /// <typeparam name="T12">エントリーポイント制御引数の12番目の型.</typeparam>
        /// <returns><see cref="RegistrationBuilder.WithParameter{T}(T)"/>で登録済みの<see cref="RegistrationBuilder"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RegistrationBuilder WithEntryParameter<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this RegistrationBuilder builder, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, CancellationToken> entry)
        {
            return builder.WithParameter(entry);
        }

        /// <summary>
        /// <see cref="EntryPointContainer{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13}"/>に登録するエントリーポイント制御引数.
        /// </summary>
        /// <param name="builder"><see cref="RegistrationBuilder"/>.</param>
        /// <param name="entry">エントリーポイント制御引数.</param>
        /// <typeparam name="T1">エントリーポイント制御引数の1番目の型.</typeparam>
        /// <typeparam name="T2">エントリーポイント制御引数の2番目の型.</typeparam>
        /// <typeparam name="T3">エントリーポイント制御引数の3番目の型.</typeparam>
        /// <typeparam name="T4">エントリーポイント制御引数の4番目の型.</typeparam>
        /// <typeparam name="T5">エントリーポイント制御引数の5番目の型.</typeparam>
        /// <typeparam name="T6">エントリーポイント制御引数の6番目の型.</typeparam>
        /// <typeparam name="T7">エントリーポイント制御引数の7番目の型.</typeparam>
        /// <typeparam name="T8">エントリーポイント制御引数の8番目の型.</typeparam>
        /// <typeparam name="T9">エントリーポイント制御引数の9番目の型.</typeparam>
        /// <typeparam name="T10">エントリーポイント制御引数の10番目の型.</typeparam>
        /// <typeparam name="T11">エントリーポイント制御引数の11番目の型.</typeparam>
        /// <typeparam name="T12">エントリーポイント制御引数の12番目の型.</typeparam>
        /// <typeparam name="T13">エントリーポイント制御引数の13番目の型.</typeparam>
        /// <returns><see cref="RegistrationBuilder.WithParameter{T}(T)"/>で登録済みの<see cref="RegistrationBuilder"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RegistrationBuilder WithEntryParameter<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this RegistrationBuilder builder, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, CancellationToken> entry)
        {
            return builder.WithParameter(entry);
        }

        /// <summary>
        /// <see cref="EntryPointContainer{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14}"/>に登録するエントリーポイント制御引数.
        /// </summary>
        /// <param name="builder"><see cref="RegistrationBuilder"/>.</param>
        /// <param name="entry">エントリーポイント制御引数.</param>
        /// <typeparam name="T1">エントリーポイント制御引数の1番目の型.</typeparam>
        /// <typeparam name="T2">エントリーポイント制御引数の2番目の型.</typeparam>
        /// <typeparam name="T3">エントリーポイント制御引数の3番目の型.</typeparam>
        /// <typeparam name="T4">エントリーポイント制御引数の4番目の型.</typeparam>
        /// <typeparam name="T5">エントリーポイント制御引数の5番目の型.</typeparam>
        /// <typeparam name="T6">エントリーポイント制御引数の6番目の型.</typeparam>
        /// <typeparam name="T7">エントリーポイント制御引数の7番目の型.</typeparam>
        /// <typeparam name="T8">エントリーポイント制御引数の8番目の型.</typeparam>
        /// <typeparam name="T9">エントリーポイント制御引数の9番目の型.</typeparam>
        /// <typeparam name="T10">エントリーポイント制御引数の10番目の型.</typeparam>
        /// <typeparam name="T11">エントリーポイント制御引数の11番目の型.</typeparam>
        /// <typeparam name="T12">エントリーポイント制御引数の12番目の型.</typeparam>
        /// <typeparam name="T13">エントリーポイント制御引数の13番目の型.</typeparam>
        /// <typeparam name="T14">エントリーポイント制御引数の14番目の型.</typeparam>
        /// <returns><see cref="RegistrationBuilder.WithParameter{T}(T)"/>で登録済みの<see cref="RegistrationBuilder"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RegistrationBuilder WithEntryParameter<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this RegistrationBuilder builder, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, CancellationToken> entry)
        {
            return builder.WithParameter(entry);
        }

        /// <summary>
        /// <see cref="EntryPointContainer{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15}"/>に登録するエントリーポイント制御引数.
        /// </summary>
        /// <param name="builder"><see cref="RegistrationBuilder"/>.</param>
        /// <param name="entry">エントリーポイント制御引数.</param>
        /// <typeparam name="T1">エントリーポイント制御引数の1番目の型.</typeparam>
        /// <typeparam name="T2">エントリーポイント制御引数の2番目の型.</typeparam>
        /// <typeparam name="T3">エントリーポイント制御引数の3番目の型.</typeparam>
        /// <typeparam name="T4">エントリーポイント制御引数の4番目の型.</typeparam>
        /// <typeparam name="T5">エントリーポイント制御引数の5番目の型.</typeparam>
        /// <typeparam name="T6">エントリーポイント制御引数の6番目の型.</typeparam>
        /// <typeparam name="T7">エントリーポイント制御引数の7番目の型.</typeparam>
        /// <typeparam name="T8">エントリーポイント制御引数の8番目の型.</typeparam>
        /// <typeparam name="T9">エントリーポイント制御引数の9番目の型.</typeparam>
        /// <typeparam name="T10">エントリーポイント制御引数の10番目の型.</typeparam>
        /// <typeparam name="T11">エントリーポイント制御引数の11番目の型.</typeparam>
        /// <typeparam name="T12">エントリーポイント制御引数の12番目の型.</typeparam>
        /// <typeparam name="T13">エントリーポイント制御引数の13番目の型.</typeparam>
        /// <typeparam name="T14">エントリーポイント制御引数の14番目の型.</typeparam>
        /// <typeparam name="T15">エントリーポイント制御引数の15番目の型.</typeparam>
        /// <returns><see cref="RegistrationBuilder.WithParameter{T}(T)"/>で登録済みの<see cref="RegistrationBuilder"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RegistrationBuilder WithEntryParameter<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this RegistrationBuilder builder, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, CancellationToken> entry)
        {
            return builder.WithParameter(entry);
        }
    }
}
#pragma warning restore SA1204
#pragma warning restore SA1402