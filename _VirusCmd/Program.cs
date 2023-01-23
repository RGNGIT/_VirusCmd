using _VirusCmd.Classes;

enum ESeason
{
    Winter = 0,
    Spring = 1,
    Summer = 2,
    Fall = 3
}

enum ELocalityType
{
    Megapolis = 1,
    City = 2,
    Town = 3
}

public static class Program
{
    static List<Country> Storage = new();
    // Хранилище для текущего времени года
    static SeasonSingleton seasonSingleton = new();
    // Словарь времен года
    static Dictionary<int, string> SeasonsDictionary = new()
    {
        {0, "Зима"},
        {1, "Весна"},
        {2, "Лето"},
        {3, "Осень"}
    };

    static void SetSeasonAndYear(int Year, int SeasonKey)
    {
        seasonSingleton.Year = Year;
        seasonSingleton.Season = SeasonsDictionary[SeasonKey];
    }

    static void SeasonMenuOption()
    {
        Console.Clear();
        Console.WriteLine("Назначить сезон и год\nГод:");
        var Year = Console.ReadLine();
        Console.WriteLine("Сезон:\n1 - Зима\n2 - Весна\n3 - Лето\n4 - Осень");
        RetrySeason:
        var Season = Console.ReadLine();
        switch(int.Parse(Season))
        {
            case 1: SetSeasonAndYear(int.Parse(Year), (int)ESeason.Winter); break;
            case 2: SetSeasonAndYear(int.Parse(Year), (int)ESeason.Spring); break;
            case 3: SetSeasonAndYear(int.Parse(Year), (int)ESeason.Summer); break;
            case 4: SetSeasonAndYear(int.Parse(Year), (int)ESeason.Fall); break;
            default: { Console.WriteLine("Чет такого сезона не знает мир. Еще разок"); goto RetrySeason; }
        }
    }

    static void AddNewLocality(int countryKey)
    {
        Console.Clear();
        Console.WriteLine("Выберите тип:\n1 - Мегаполис\n2 - Город\n3 - Поселок");
        var Type = Console.ReadLine();
        if(int.Parse(Type) < 1 || int.Parse(Type) > 3)
        {
            Console.WriteLine("Такого типа НП нет");
            Console.ReadKey();
            AddNewLocality(countryKey);
        }
        Locality locality = null;
        Console.WriteLine("Численность населения:");
        var Population = Console.ReadLine();
        Console.WriteLine("Численность заболевших:");
        var Infected = Console.ReadLine();
        Console.WriteLine("Численность вакцинированных:");
        var Vaccinated = Console.ReadLine();
        Console.WriteLine("Плотность населения (дробное от 0 до 1):");
        var Density = Console.ReadLine();
        switch (int.Parse(Type))
        {
            case (int)ELocalityType.Megapolis:
                Console.WriteLine("Количество городов, поглощенных мегаполисом:");
                var MergedAmount = Console.ReadLine();
                locality = new Megapolis(
                    int.Parse(Population),
                    int.Parse(Infected),
                    int.Parse(Vaccinated),
                    float.Parse(Density),
                    int.Parse(MergedAmount)
                    );
                break;
            case (int)ELocalityType.City:
                Console.WriteLine("Количество торговых центров:");
                var TCAmount = Console.ReadLine();
                Console.WriteLine("Процент посещаемости (дробное от 0 до 1):");
                var TCAttendPercentage = Console.ReadLine();
                locality = new City(
                    int.Parse(Population),
                    int.Parse(Infected),
                    int.Parse(Vaccinated),
                    float.Parse(Density),
                    int.Parse(TCAmount),
                    float.Parse(TCAttendPercentage)
                    );
                break;
            case (int)ELocalityType.Town:
                Console.WriteLine("ФИО старейшины поселка:");
                var Elder = Console.ReadLine();
                Console.WriteLine("Количество домашних хозяйств:");
                var HouseholdAmount = Console.ReadLine();
                locality = new Town(
                    int.Parse(Population),
                    int.Parse(Infected),
                    int.Parse(Vaccinated),
                    float.Parse(Density),
                    Elder.ToString(),
                    int.Parse(HouseholdAmount)
                    );
                break;
        }
        if(locality != null)
        {
            Storage[countryKey].Localities.Add(locality);
        }
        else
        {
            Console.WriteLine("Экземпляр не инициализирован");
            Console.ReadKey();
        }
    }

    static void LocalityMenuOption()
    {
        Console.Clear();
        try
        {
            if(Storage.Count == 0)
            {
                Console.WriteLine("Стран не добавлено");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Выберите страну, куда добавить НП:");
            for(int i = 0; i < Storage.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {Storage[i].Name}");
            }
            var Key = Console.ReadLine();
            AddNewLocality(int.Parse(Key) - 1);
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            Console.ReadKey();
        }
    }

    static void CountryMenuOption()
    {
        Console.Clear();
        Console.WriteLine("Добавить страну:\nНавзвание:");
        var Name = Console.ReadLine();
        Console.WriteLine("Бюджет:");
        var Budget = Console.ReadLine();
        Storage.Add(new Country(Name, float.Parse(Budget)));
    }

    public static void Main()
    {
        Console.Clear();
        if(seasonSingleton.Season != null && seasonSingleton.Year != null)
        {
            Console.WriteLine($"Год: {seasonSingleton.Year}. Сезон: {seasonSingleton.Season}\n{seasonSingleton.VerbalPredict[seasonSingleton.Season]}");
        }
        else
        {
            Console.WriteLine("Год и сезон не назначены");
        }
        Console.WriteLine("Главное меню\n1 - Назначить сезон и год\n2 - Внести новый НП\n3 - Добавить страну");
        var Selector = Console.ReadLine();
        switch(int.Parse(Selector))
        {
            case 1: SeasonMenuOption(); break;
            case 2: LocalityMenuOption(); break;
            case 3: CountryMenuOption(); break;
        }
        Main();
    }
}