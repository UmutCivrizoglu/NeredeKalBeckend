namespace Core.Interfaces;

public interface IMessageQueueProducer
{
    void PublishReportResult(string reportData);
}
