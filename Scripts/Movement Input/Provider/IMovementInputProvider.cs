public interface IMovementInputProvider<out TInput>
{
    TInput GetMovementInput();
}