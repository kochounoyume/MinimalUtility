#if ENABLE_VCONTAINER
using VContainer.Unity;

namespace MinimalUtility.VContainer
{
    /// <summary>
    /// <see cref="IStartable"/>のエントリーポイントを持つデバッグプロファイラー
    /// </summary>
    public sealed class VContainerDebugProfiler : R3.DebugProfiler, IStartable
    {
        public VContainerDebugProfiler(MemoryUnit memoryUnit) : base(memoryUnit)
        {
        }
    }
}
#endif