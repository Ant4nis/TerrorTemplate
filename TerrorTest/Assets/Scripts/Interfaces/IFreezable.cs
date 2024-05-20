/// <summary>
/// Defines a freezable entity in the game.
/// Any object that implements this interface can be frozen and must define methods to start and stop losing or recovering temperature.
/// </summary>
public interface IFreezable
{
    /// <summary>
    /// Starts the process of losing temperature.
    /// </summary>
    /// <param name="amount">The amount of temperature to lose over time.</param>
    void StartLosingTemperature(float amount);

    /// <summary>
    /// Stops the process of losing temperature.
    /// </summary>
    void StopLosingTemperature();

    /// <summary>
    /// Starts the process of recovering temperature.
    /// </summary>
    /// <param name="amount">The amount of temperature to recover over time.</param>
    void StartRecoveringTemperature(float amount);

    /// <summary>
    /// Stops the process of recovering temperature.
    /// </summary>
    void StopRecoveringTemperature();
}