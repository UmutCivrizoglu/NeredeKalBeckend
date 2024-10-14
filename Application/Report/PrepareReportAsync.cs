using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Report;

public class PrepareReportAsync : IReportService
{
  
    private readonly HotelDbContext _context;
   // private readonly IMessageQueueProducer _producer;
    
    public PrepareReportAsync(HotelDbContext hotelRepository)
    {
        _context = hotelRepository;
       // _producer = producer;
    }

    async Task IReportService.PrepareReportAsync(Guid reportId, string cityName)
    {
        Console.WriteLine($"Rapor hazırlanmaya başlandı. Rapor ID: {reportId}, Şehir: {cityName}");
        
        var hotelsInCity = await _context.Hotels
            .Where(h => h.City == cityName)
            .ToListAsync();

       
        var hotelCount = hotelsInCity.Count();

        
        var phoneNumberCount = hotelsInCity
            .SelectMany(h => h.ContactInformations)
            .Count(c => c.InfoType == "Phone");
     
        Console.WriteLine($"Şehir: {cityName}, Otel Sayısı: {hotelCount}");
        Console.WriteLine($"Otel Telefon Numarası Sayısı: {phoneNumberCount}");
        
        await Task.Delay(2000);
      //  _producer.PublishReportResult(reportId,cityName,hotelCount, phoneNumberCount);
        Console.WriteLine($"Rapor hazırlandı: Rapor ID: {reportId}, Şehir: {cityName} için {hotelCount} otel kaydedildi.");
    }
}