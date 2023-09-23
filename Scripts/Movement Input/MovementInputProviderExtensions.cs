using Object = UnityEngine.Object;

public static class MovementInputProviderExtensions
{
    public static IMovementInputProvider<U> InputProviderFromObject<T, U>(this IMovementInputProvider<T> _, Object @object) => @object as IMovementInputProvider<U>;
}