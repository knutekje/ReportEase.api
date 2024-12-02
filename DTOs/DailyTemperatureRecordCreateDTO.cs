namespace ReportEase.api.DTOs;

public class DailyTemperatureRecordCreateDTO
{
    public DateTime Date { get; set; }
    public string SubmittedBy { get; set; } 

    public List<MeasuringPointReadingDTO> MeasuringPoints { get; set; } = new List<MeasuringPointReadingDTO>();
}

public class MeasuringPointReadingDTO
{
    public string Name { get; set; } 
    public double Temperature { get; set; } 
    public bool WithinLimits { get; set; } 
}
