#if ENABLE_VCONTAINER
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MinimalUtility.VContainer
{
    /// <summary>
    /// <see cref="LifetimeScope"/>の共通基底クラス.
    /// </summary>
    public class LifetimeScopeBase : LifetimeScope
    {
        [SerializeField]
        [Tooltip("Bind も Inject もする")]
        private Component[] autoBindComponents = null;

        /// <inheritdoc/>
        protected override void Configure(IContainerBuilder builder)
        {
            foreach (Component component in autoBindComponents)
            {
                if (component == null || !component.gameObject.scene.IsValid())
                {
                    const string message = "autoBindComponentsにnullまたはシーン外のコンポーネントが指定されています";
                    Debug.LogError(message);
                    continue;
                }
                builder.RegisterComponent(component);
            }
        }
    }
}
#endif
