using UnityEngine;

/// <summary>
/// Custom attribute to display a warning message above a property in the inspector.
/// </summary>
public class WarningAttribute : PropertyAttribute
{
    /// <summary>
    /// The warning message to display.
    /// </summary>
    public string Message { get; private set; }

    /// <summary>
    /// Initializes a new instance of the WarningAttribute class with the specified message.
    /// </summary>
    /// <param name="message">The warning message to display.</param>
    public WarningAttribute(string message)
    {
        Message = message;
    }
}