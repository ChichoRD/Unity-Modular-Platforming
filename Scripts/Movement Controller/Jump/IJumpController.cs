public interface IJumpController
{
#nullable enable
    IMovementPerformer? GetGravityPerformer();
    IMovementPerformer? GetImpulsePerformer();
    IMovementPerformer? GetCancelerPerformer();
#nullable disable
}
