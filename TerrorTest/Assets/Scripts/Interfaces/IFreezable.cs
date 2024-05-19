/// <summary>
/// Defines a freezable entity in the game.
/// Any object that implements this interface can be freezed and must define the LossTemperature method.
/// </summary>
public interface IFreezable
{
    void LossTemperature(float amount);
    
}