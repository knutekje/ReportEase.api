public class TemperaturePoint
{
    public string Unit { get; set; } // E.g., "Fridge", "Freezer", etc.
    public double Temperature { get; set; } // Recorded temperature
    public bool WithinLimits { get; set; } // Whether the temperature is within acceptable limits
}
