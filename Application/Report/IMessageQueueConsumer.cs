namespace Core.Interfaces;

public interface IMessageQueueConsumer
{
    
    void StartConsumingReports(Action<string> onReportProcessed);
    void Dispose();
}