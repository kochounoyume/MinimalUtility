﻿#if ENABLE_VCONTAINER && ENABLE_R3
using VContainer.Unity;

namespace MinimalUtility.VContainer
{
    using R3;

    /// <summary>
    /// <see cref="IStartable"/>のエントリーポイントを持つデバッグプロファイラー.
    /// </summary>
    public sealed class VContainerDebugProfiler : DebugProfiler, IStartable
    {
        private const MemoryUnit DefaultUnit = MemoryUnit.GB;

        /// <summary>
        /// Initializes a new instance of the <see cref="VContainerDebugProfiler"/> class.
        /// </summary>
        public VContainerDebugProfiler() : base(DefaultUnit)
        {
        }
    }
}
#endif