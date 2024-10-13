using System.Text;
using Core.Interfaces;
using RabbitMQ.Client;

namespace Infrastructure;

public class RabbitMQMessageQueueProducer : IMessageQueueProducer
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMQMessageQueueProducer()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

      
        _channel.QueueDeclare(queue: "reportResultQueue", 
            durable: true, 
            exclusive: false, 
            autoDelete: false, 
            arguments: null);
    }

   
    public void PublishReportResult(string reportData)
    {
        var body = Encoding.UTF8.GetBytes(reportData);
        _channel.BasicPublish(exchange: "", routingKey: "reportResultQueue", basicProperties: null, body: body);
        Console.WriteLine($"Rapor sonucu 'reportResultQueue' kuyruğuna gönderildi: {reportData}");
    }
}