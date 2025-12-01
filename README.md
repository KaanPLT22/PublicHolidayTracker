# PublicHolidayTracker

## 🎯 Proje Hakkında
PublicHolidayTracker, C# ile geliştirilmiş bir konsol uygulamasıdır.  
Bu proje, Türkiye'deki resmi tatilleri API üzerinden alıp kullanıcıya farklı kriterlere göre listeleme ve arama imkanı sağlar.

Kullanıcılar, tatil listesini yıl bazında görüntüleyebilir, tarihe göre veya isme göre arama yapabilir ve üç yıllık tatilleri toplu olarak görebilir.

---

## 🚀 Özellikler
- Yıllık resmi tatil listesini görüntüleme (2023–2025)  
- Tarihe göre tatil arama (gg-aa formatı)  
- İsme göre tatil arama  
- Tüm tatilleri 3 yıl boyunca toplu listeleme  
- API bağlantısı ve veri çekme


### 💡 Çalışma Mantığı
1. Uygulama başlatıldığında `LoadHolidays` metodu çalışır ve belirtilen yıllar için API’ye istek atar.  
2. API’den dönen JSON verileri `Holiday` sınıfına deserialize edilir ve `holidayCache` isimli sözlükte saklanır.  
3. Kullanıcı konsol menüsünden bir seçim yapar:  
   - **Yıl Bazlı Listeleme:** Seçilen yılın tatilleri ekrana yazdırılır.  
   - **Tarihe Göre Arama:** Kullanıcı `gg-aa` formatında tarih girer ve eşleşen tatiller gösterilir.  
   - **İsme Göre Arama:** Kullanıcı bir tatil ismi girer ve eşleşen tatiller listelenir.  
   - **Tüm Tatilleri Göster:** 2023–2025 yıllarındaki tüm tatiller sıralanır.  
4. Kullanıcı menüden çıkış yapana kadar uygulama döngü içinde çalışmaya devam eder.  

---

## 🛠 Teknik Detaylar
- **Programlama Dili:** C#  
- **Platform:** .NET 8 (Konsol Uygulaması)  
- **Veri Kaynağı:** [Nager.Date API](https://date.nager.at)  
- **API Adresleri:**  
  - https://date.nager.at/api/v3/PublicHolidays/2023/TR  
  - https://date.nager.at/api/v3/PublicHolidays/2024/TR  
  - https://date.nager.at/api/v3/PublicHolidays/2025/TR  
- **JSON İşleme:** `System.Text.Json` kütüphanesi ile API’den alınan JSON verisi `Holiday` sınıfına dönüştürülüyor  
- **Veri Saklama:** Uygulama çalışırken bellek içi `Dictionary<int, List<Holiday>>` yapısında tutuluyor  
- **HTTP İstekleri:** `HttpClient` kullanılarak API’den veri çekiliyor  

