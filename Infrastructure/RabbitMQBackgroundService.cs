using System.Text;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infrastructure;

public class RabbitMQBackgroundService : BackgroundService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMQBackgroundService()
    {
        Console.WriteLine("RabbitMQ Background Service is starting.");
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: "ReportQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
       Console.WriteLine("RabbitMQ Background Service is starting.");
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

           
            Console.WriteLine($"Mesaj alındı: {message}");

            
            _channel.BasicAck(ea.DeliveryTag, false);
        };

       
        _channel.BasicConsume(queue: "ReportQueue", autoAck: false, consumer: consumer);

        return Task.CompletedTask;
    }

    public override void Dispose()
    {
       
        _channel.Close();
        _connection.Close();
        base.Dispose();
    }
}