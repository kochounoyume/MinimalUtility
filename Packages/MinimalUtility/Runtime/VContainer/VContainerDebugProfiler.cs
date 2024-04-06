#if ENABLE_VCONTAINER
using VContainer.Unity;

namespace MinimalUtility.VContainer
{
    /// <summary>
    /// <see cref="IStartable"/>のエントリーポイントを持つデバッグプロファイラー.
    /// </summary>
    public sealed class VContainerDebugProfiler : R3.DebugProfiler, IStartable
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