using System.Text;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Application.Report;

public class ReportService : IReportService
{
  
    private readonly HotelDbContext _context;
    private readonly ReportDetailProducer _reportDetailProducer;
    private readonly HttpClient _httpClient;
    
   // private readonly IMessageQueueProducer _producer;
    
    public ReportService(HotelDbContext hotelRepository,ReportDetailProducer reportDetailProducer, HttpClient httpClient)
    {
        _context = hotelRepository;
        _reportDetailProducer = reportDetailProducer;
        _httpClient = httpClient;
    }

    public async Task PrepareReportAsync(Guid reportId, string cityName)
    {
        Console.WriteLine($"Rapor hazırlanmaya başlandı Rapor ID: {reportId}, Şehir: {cityName}");
        
        var hotelsInCity = await _context.Hotels
            .Where(h => h.City == cityName)
            .Include(x=>x.ContactInformations)
            .ToListAsync();

       
        var hotelCount = hotelsInCity.Count();

        
        var phoneNumberCount = hotelsInCity
            .SelectMany(h => h.ContactInformations)
            .Count(c => c.InfoType == "Phone");
     
        Console.WriteLine($"Şehir: {cityName}, Otel Sayısı: {hotelCount}");
        Console.WriteLine($"Otel Telefon Numarası Sayısı: {phoneNumberCount}");
        
        await Task.Delay(2000);
        await _reportDetailProducer.PublishMessage(new ReportDetailRequest
        {
            ReportId = reportId,
            City = cityName,
            hotelCount = hotelCount,
            phoneNumberCount = 1
        });
     
    }
}