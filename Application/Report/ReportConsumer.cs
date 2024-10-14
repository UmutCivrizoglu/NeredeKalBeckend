

using MassTransit;

namespace Application.Report;

public class ReportConsumer : IConsumer<ReportRequest>
{
    private readonly IReportService _reportService;

    public ReportConsumer(IReportService reportService)
    {
        _reportService = reportService;
    }


    public async Task Consume(ConsumeContext<ReportRequest> context)
    {
        Console.WriteLine($"Received message: {context.Message.City}");
        await _reportService.PrepareReportAsync(context.Message.ReportId,context.Message.City);
    }
}

public class ReportRequest
{
    public Guid ReportId { get; set; }
    public string City { get; set; }
}