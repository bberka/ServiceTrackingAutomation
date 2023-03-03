using ServiceTrackingAutomation.Domain.Enums;

namespace ServiceTrackingAutomation.Domain.Extensions;

public static class EnumExtensions
{
    public static string ToMessage(this ComplaintStatus status)
    {
        return status switch
        {
            ComplaintStatus.ReceivedFromCustomer => "Müşteriden Alındı",
            ComplaintStatus.WillBeSentToService => "Servise Yollanacak",
            ComplaintStatus.SentToService => "Servise Yollandı",
            ComplaintStatus.ReceivedFromService => "Servisten Alındı",
            ComplaintStatus.SentToCustomer => "Müşteriye Yollandı",
            _ => "Bilinmeyen Durum"
        };
    }
}