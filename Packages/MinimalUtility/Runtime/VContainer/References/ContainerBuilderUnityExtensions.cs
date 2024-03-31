using UnityEngine;

namespace VContainer.Unity
{
    public static class CustomContainerBuilderUnityExtensions
    {
        public static RegistrationBuilder RegisterComponent(
            this IContainerBuilder builder,
            Component component)
        {
            var type = component.GetType();
            var registrationBuilder = new ComponentRegistrationBuilder(component).As(type);
            // Force inject execution
            builder.RegisterBuildCallback(container => container.Resolve(type));
            return builder.Register(registrationBuilder);
        }
    }
}
