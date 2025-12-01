# PublicHolidayTracker

## Proje Hakkında
PublicHolidayTracker, C# ile geliştirilmiş bir konsol uygulamasıdır.  
Bu proje, Türkiye'deki resmi tatilleri API üzerinden alıp kullanıcıya farklı kriterlere göre listeleme ve arama imkanı sağlar.

Kullanıcılar, tatil listesini yıl bazında görüntüleyebilir, tarihe göre veya isme göre arama yapabilir ve üç yıllık tatilleri toplu olarak görebilir.

---

## Özellikler
- Yıllık resmi tatil listesini görüntüleme (2023–2025)  
- Tarihe göre tatil arama (gg-aa formatı)  
- İsme göre tatil arama  
- Tüm tatilleri 3 yıl boyunca toplu listeleme  
- API bağlantısı ve veri çekme

---

## Teknik Detaylar
- **Programlama Dili:** C#  
- **Platform:** .NET 8 (Konsol Uygulaması)  
- **Veri Kaynağı:** [Nager.Date API](https://date.nager.at)  
- **API Adresleri:**  
  - https://date.nager.at/api/v3/PublicHolidays/2023/TR  
  - https://date.nager.at/api/v3/PublicHolidays/2024/TR  
  - https://date.nager.at/api/v3/PublicHolidays/2025/TR  
- **JSON İşleme:** System.Text.Json kütüphanesi ile JSON verisi C# sınıfına dönüştürülüyor  
- **Veri Saklama:** Uygulama çalışırken bellek içi `Dictionary<int, List<Holiday>>` yapısında tutuluyor, dosyaya kaydetme yok  
- **HTTP İstekleri:** `HttpClient` kullanılarak API’den veri çekiliyor
