using VContainer.Unity;

namespace MinimalUtility.VContainer
{
    /// <summary>
    /// <see cref="VContainer"/>のエントリーポイントを利用して、デバッグプロファイル情報を表示するクラス.
    /// </summary>
    public class VContainerDebugProfileViewer : DebugProfileViewer, IStartable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VContainerDebugProfileViewer"/> class.
        /// </summary>
        public VContainerDebugProfileViewer() : base()
        {
        }

        /// <inheritdoc/>
        void IStartable.Start() => IsGUIVisible = true;
    }
}