#if ENABLE_VCONTAINER
using VContainer.Unity;

namespace MinimalUtility.VContainer
{
    /// <summary>
    /// <see cref="IStartable"/>のエントリーポイントを持つデバッグプロファイラー
    /// </summary>
    public sealed class VContainerDebugProfiler : R3.DebugProfiler, IStartable
    {
        const MemoryUnit defaultUnit = MemoryUnit.GB;

        public VContainerDebugProfiler() : base(defaultUnit)
        {
        }
    }
}
#endif