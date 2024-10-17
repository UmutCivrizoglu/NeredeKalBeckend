using System.Text;
using Application.Report;
using Core.Entity;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class RabbitMQBackgroundService : BackgroundService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMessageQueueProducer _messageQueueProducer;
    
  

    public RabbitMQBackgroundService(IServiceProvider serviceProvider, IMessageQueueProducer messageQueueProducer)
    {
        _serviceProvider = serviceProvider;
       _messageQueueProducer = messageQueueProducer;
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

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("RabbitMQ Background Service is starting.");
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine($"Mesaj alındı: {message}");
            var parts = message.Split(':');

            var reportId = Guid.Parse(parts[0]);
            var cityName = parts[1];

           
            using (var scope = _serviceProvider.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<HotelDbContext>();
                await YPrepareReportAsync(reportId, cityName, _context);
            }
            
            _channel.BasicAck(ea.DeliveryTag, false);
        };

        _channel.BasicConsume(queue: "ReportQueue", autoAck: false, consumer: consumer);

        await Task.CompletedTask;
    }

    async Task YPrepareReportAsync(Guid reportId, string cityName,HotelDbContext _context )
    {
        Console.WriteLine($"Rapor hazırlanmaya başlandı. Rapor ID: {reportId}, Şehir: {cityName}");
        
        var hotelsInCity = await _context.Hotels
            .Where(h => h.City == cityName)
            .ToListAsync();

       
        var hotelCount = hotelsInCity.Count();
        if (hotelsInCity == null || !hotelsInCity.Any())
        {
            Console.WriteLine("Şehirde otel bulunamadı.");
            return;
        }
        
        var phoneNumberCount = hotelsInCity
            .SelectMany(h => h.ContactInformations ?? Enumerable.Empty<ContactInformation>())  // Eğer null ise boş bir koleksiyon döner
            .Count(c => c.InfoType == "Phone");

     
        Console.WriteLine($"Şehir: {cityName}, Otel Sayısı: {hotelCount}");
        Console.WriteLine($"Otel Telefon Numarası Sayısı: {phoneNumberCount}");
        
        await Task.Delay(2000);
        _messageQueueProducer.PublishReportResult(reportId,cityName,hotelCount, phoneNumberCount);
        Console.WriteLine($"Rapor hazırlandı: Rapor ID: {reportId}, Şehir: {cityName} için {hotelCount} otel kaydedildi.");
    }

    public override void Dispose()
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
    }
}
