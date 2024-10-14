namespace Core.Interfaces;

public interface IMessageQueueProducer
{
    public void PublishReportResult(Guid reportId, string cityName, int hotelCount, int phoneNumberCount);
}
