using UnityEngine;

namespace VContainer.Unity
{
    /// <summary>
    /// <see cref="ContainerBuilderUnityExtensions"/>にはない、カスタムの拡張メソッド.
    /// </summary>
    public static class CustomContainerBuilderUnityExtensions
    {
        /// <summary>
        /// Register a component to the container.
        /// </summary>
        /// <param name="builder">IContainerBuilder.</param>
        /// <param name="component">Component.</param>
        /// <returns>RegistrationBuilder.</returns>
        public static RegistrationBuilder RegisterComponent(
            this IContainerBuilder builder,
            Component component)
        {
            var type = component.GetType();
            var registrationBuilder = new ComponentRegistrationBuilder(component).As(type).AsImplementedInterfaces();
            // Force inject execution
            builder.RegisterBuildCallback(container => container.Resolve(type));
            return builder.Register(registrationBuilder);
        }
    }
}