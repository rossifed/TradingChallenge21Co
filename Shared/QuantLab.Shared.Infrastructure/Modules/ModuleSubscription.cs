namespace QuantLab.Shared.Infrastructure.Modules
{
    internal class ModuleSubscription
    {
        public ModuleSubscription(Type subscriptionType, Func<object, CancellationToken, Task> action)
        {
            SubscriptionType = subscriptionType;
            Action = action;
        }
        public Type SubscriptionType { get; }

        public Func<object, CancellationToken, Task> Action { get; }

        public string Key => SubscriptionType.Name;
    }
}
