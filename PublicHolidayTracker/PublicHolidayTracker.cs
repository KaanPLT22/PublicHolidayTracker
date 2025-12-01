using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

class Holiday
{
    public string date { get; set; }
    public string localName { get; set; }
    public string name { get; set; }
    public string countryCode { get; set; }

    [JsonPropertyName("fixed")]
    public bool IsFixed { get; set; } // C# keyword çakışmasını önledik

    public bool global { get; set; }
}

class PublicHolidayTracker
{
    static Dictionary<int, List<Holiday>> holidayCache = new Dictionary<int, List<Holiday>>();

    static async Task Main(string[] args)
    {
        await LoadHolidays();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== PublicHolidayTracker =====");
            Console.WriteLine("1. Tatil listesini göster (yıl seçmeli)");
            Console.WriteLine("2. Tarihe göre tatil ara (gg-aa formatı)");
            Console.WriteLine("3. İsme göre tatil ara");
            Console.WriteLine("4. Tüm tatilleri 3 yıl boyunca göster (2023–2025)");
            Console.WriteLine("5. Çıkış");
            Console.Write("Seçiminiz: ");

            string secim = Console.ReadLine();

            switch (secim)
            {
                case "1":
                    Menu_ShowHolidayList();
                    break;
                case "2":
                    Menu_SearchByDate();
                    break;
                case "3":
                    Menu_SearchByName();
                    break;
                case "4":
                    Menu_ShowAllYears();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Geçersiz seçim!");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static async Task LoadHolidays()
    {
        int[] years = { 2023, 2024, 2025 };
        HttpClient client = new HttpClient();

        foreach (int year in years)
        {
            string url = $"https://date.nager.at/api/v3/PublicHolidays/{year}/TR";

            try
            {
                var holidays = await client.GetFromJsonAsync<List<Holiday>>(url);
                holidayCache[year] = holidays;

                Console.WriteLine($"API {year} yılı için okundu.");
            }
            catch
            {
                Console.WriteLine($"API {year} yılı için okunamadı!");
            }
        }

        Console.WriteLine("Tüm API verileri yüklendi.");

    }

    static void Menu_ShowHolidayList()
    {
        Console.Write("Yıl giriniz (2023-2025): ");
        if (int.TryParse(Console.ReadLine(), out int year) && holidayCache.ContainsKey(year))
        {
            Console.Clear();
            Console.WriteLine($"=== {year} Tatil Listesi ===");

            foreach (var h in holidayCache[year])
                Console.WriteLine($"{h.date} - {h.localName} ({h.name})");

            Console.WriteLine("Geri dönmek için ENTER tuşuna basınız.");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("Geçersiz yıl!");
            Console.ReadKey();
        }
    }

    static void Menu_SearchByDate()
    {
        Console.Write("Tarih (gg-aa): ");
        string input = Console.ReadLine();
        bool found = false;

        Console.Clear();

        foreach (var year in holidayCache.Keys)
        {
            foreach (var h in holidayCache[year])
            {
                DateTime dt = DateTime.Parse(h.date);
                if (dt.ToString("dd-MM") == input)
                {
                    Console.WriteLine($"{h.date} - {h.localName} ({h.name})");
                    found = true;
                }
            }
        }

        if (!found)
            Console.WriteLine("Bu tarihte bir tatil bulunamadı.");

        Console.WriteLine("Geri dönmek için ENTER tuşuna basınız.");
        Console.ReadKey();
    }

    static void Menu_SearchByName()
    {
        Console.Write("Aranacak isim: ");
        string name = Console.ReadLine().ToLower();
        bool found = false;

        Console.Clear();

        foreach (var year in holidayCache.Keys)
        {
            foreach (var h in holidayCache[year])
            {
                if (h.localName.ToLower().Contains(name) || h.name.ToLower().Contains(name))
                {
                    Console.WriteLine($"{h.date} - {h.localName} ({h.name})");
                    found = true;
                }
            }
        }

        if (!found)
            Console.WriteLine("Bu isimle eşleşen bir tatil bulunamadı.");

        Console.WriteLine("Geri dönmek için ENTER tuşuna basınız.");
        Console.ReadKey();
    }

    static void Menu_ShowAllYears()
    {
        Console.Clear();
        Console.WriteLine("=== 2023–2025 Tüm Tatiller ===");

        foreach (var year in holidayCache.Keys)
        {
            Console.WriteLine($"\n--- {year} ---");
            foreach (var h in holidayCache[year])
                Console.WriteLine($"{h.date} - {h.localName} ({h.name})");
        }

        Console.WriteLine("Geri dönmek için ENTER tuşuna basınız.");
        Console.ReadKey();
    }
}
