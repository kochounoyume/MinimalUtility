#if ENABLE_VCONTAINER
using VContainer.Unity;

namespace MinimalUtility.MultiLibraries
{
    /// <summary>
    /// <see cref="IAsyncStartable"/>を実装するための分割部分.
    /// </summary>
    public sealed partial class DebugProfiler : IAsyncStartable
    {
    }
}
#endif