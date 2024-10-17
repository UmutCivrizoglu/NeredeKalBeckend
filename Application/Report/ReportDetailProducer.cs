using MassTransit;

namespace Application.Report;

public class ReportDetailProducer
{
    private readonly IBus _bus;

    public ReportDetailProducer(IBus bus)
    {
        _bus = bus;
    }

    public async Task PublishMessage(ReportDetailRequest message)
    {
        await _bus.Publish(message);
    }
}

public class ReportDetailRequest
{
    public Guid ReportId { get; set; }
    public string City { get; set; }
    public int phoneNumberCount { get; set; }
    public int hotelCount { get; set; }
    
}