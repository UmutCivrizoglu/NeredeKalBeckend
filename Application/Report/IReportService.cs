namespace Application.Report;

public interface IReportService
{
    
    Task PrepareReportAsync(Guid reportId, string cityName);
}