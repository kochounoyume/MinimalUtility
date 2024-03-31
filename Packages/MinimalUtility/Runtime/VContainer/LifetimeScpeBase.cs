#if ENABLE_VCONTAINER
using VContainer;
using VContainer.Unity;
using UnityEngine;

namespace MinimalUtility.VContainer
{
    public class LifetimeScpeBase : LifetimeScope
    {
        [SerializeField, Tooltip("Bind も Inject もする")]
        Component[] autoBindComponents = null;

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
