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
        switch(Convert.ToInt32(Season))
        {
            case 1: SetSeasonAndYear(Convert.ToInt32(Year), (int)ESeason.Winter); break;
            case 2: SetSeasonAndYear(Convert.ToInt32(Year), (int)ESeason.Spring); break;
            case 3: SetSeasonAndYear(Convert.ToInt32(Year), (int)ESeason.Summer); break;
            case 4: SetSeasonAndYear(Convert.ToInt32(Year), (int)ESeason.Fall); break;
            default: { Console.WriteLine("Чет такого сезона не знает мир. Еще разок"); goto RetrySeason; }
        }
    }

    static void AddNewLocality(int countryKey)
    {
        Console.Clear();
        Console.WriteLine("Выберите тип:\n1 - Мегаполис\n2 - Город\n3 - Поселок");
        var Type = Console.ReadLine();
        if(Convert.ToInt32(Type) < 1 || Convert.ToInt32(Type) > 3)
        {
            Console.WriteLine("Такого типа НП нет");
            Console.ReadKey();
            AddNewLocality(countryKey);
        }
        Locality? locality = null;
        Console.WriteLine("Название:");
        var Name = Console.ReadLine();
        Console.WriteLine("Численность населения:");
        var Population = Console.ReadLine();
        Console.WriteLine("Численность заболевших:");
        var Infected = Console.ReadLine();
        Console.WriteLine("Численность вакцинированных:");
        var Vaccinated = Console.ReadLine();
        Console.WriteLine("Плотность населения (дробное от 0 до 1):");
        var Density = Console.ReadLine();
        switch (Convert.ToInt32(Type))
        {
            case (int)ELocalityType.Megapolis:
                Console.WriteLine("Количество городов, поглощенных мегаполисом:");
                var MergedAmount = Console.ReadLine();
                locality = new Megapolis(
                    Name != null ? Name : "Не назначено",
                    Convert.ToInt32(Population),
                    Convert.ToInt32(Infected),
                    Convert.ToInt32(Vaccinated),
                    Convert.ToSingle(Density),
                    Convert.ToInt32(MergedAmount)
                    );
                break;
            case (int)ELocalityType.City:
                Console.WriteLine("Количество торговых центров:");
                var TCAmount = Console.ReadLine();
                Console.WriteLine("Процент посещаемости (дробное от 0 до 1):");
                var TCAttendPercentage = Console.ReadLine();
                locality = new City(
                    Name != null ? Name : "Не назначено",
                    Convert.ToInt32(Population),
                    Convert.ToInt32(Infected),
                    Convert.ToInt32(Vaccinated),
                    Convert.ToSingle(Density),
                    Convert.ToInt32(TCAmount),
                    Convert.ToSingle(TCAttendPercentage)
                    );
                break;
            case (int)ELocalityType.Town:
                Console.WriteLine("ФИО старейшины поселка:");
                var Elder = Console.ReadLine();
                Console.WriteLine("Количество домашних хозяйств:");
                var HouseholdAmount = Console.ReadLine();
                locality = new Town(
                    Name != null ? Name : "Не назначено",
                    Convert.ToInt32(Population),
                    Convert.ToInt32(Infected),
                    Convert.ToInt32(Vaccinated),
                    Convert.ToSingle(Density),
                    Elder != null ? Elder.ToString() : "Не назначено",
                    Convert.ToInt32(HouseholdAmount)
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
            AddNewLocality(Convert.ToInt32(Key) - 1);
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
        Storage.Add(new Country(Name != null ? Name.ToString() : "Не назначено", Convert.ToSingle(Budget)));
    }

    static void DeleteCountry()
    {
        Console.Clear();
        if(Storage.Count == 0)
        {
            Console.WriteLine("Стран не добавлено");
            Console.ReadKey();
            return;
        }
        Console.WriteLine("Выберите страну для удаления");
        for(int i = 0; i < Storage.Count; i++)
        {
            Console.WriteLine($"{i + 1} - {Storage[i].Name}");
        }
        var Selector = Console.ReadLine();
        if (Convert.ToInt32(Selector) >= 1 && Convert.ToInt32(Selector) <= Storage.Count)
        {
            Storage.RemoveAt(Convert.ToInt32(Selector) - 1);
            Console.WriteLine("Удалено");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("Выбор невозможен");
            Console.ReadKey();
            DeleteCountry();
        }
    }

    static void DeleteLocality()
    {
        Console.Clear();
        if (Storage.Count == 0)
        {
            Console.WriteLine("Стран не добавлено. Населенных пунктов нет");
            Console.ReadKey();
            return;
        }
        Console.WriteLine("Выберите страну для удаления НП");
        for (int i = 0; i < Storage.Count; i++)
        {
            Console.WriteLine($"{i + 1} - {Storage[i].Name}");
        }
        var Selector = Console.ReadLine();
        if (Convert.ToInt32(Selector) >= 1 && Convert.ToInt32(Selector) <= Storage.Count)
        {
            RetryLocalityDelete:
            Console.Clear();
            Console.WriteLine($"Страна: {Storage[Convert.ToInt32(Selector) - 1].Name}\nВыберите НП для удаления");
            int CountryKey = Convert.ToInt32(Selector) - 1;
            int Count = Storage[CountryKey].Localities.Count;
            for (int i = 0; i < Count; i++)
            {
                Console.WriteLine($"{i + 1} - {Storage[CountryKey].Localities[i].Name}");
            }
            Selector = Console.ReadLine();
            if (Convert.ToInt32(Selector) >= 1 && Convert.ToInt32(Selector) <= Count)
            {
                Storage[CountryKey].Localities.RemoveAt(Convert.ToInt32(Selector) - 1);
                Console.WriteLine("Удалено");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Выбор невозможен");
                Console.ReadKey();
                goto RetryLocalityDelete;
            }
        }
        else
        {
            Console.WriteLine("Выбор невозможен");
            Console.ReadKey();
            DeleteCountry();
        }
    }

    static void EditorMenuOption()
    {
        Console.Clear();
        Console.WriteLine("1 - Удалить страну\n2 - Удалить НП");
        var Selector = Console.ReadLine();
        switch(Convert.ToInt32(Selector))
        {
            case 1: DeleteCountry(); break;
            case 2: DeleteLocality(); break;
        }
    }

    static void ReportGen()
    {
        Console.Clear();
        
    }

    static void ShowAll()
    {
        string DefineType(string className) 
        {
            switch(className) 
            {
                case "Megapolis": return "Мегаполис";
                case "City": return "Город";
                case "Town": return "Поселок";
                default: return "Не найдено определения класса";
            }
        }
        Console.Clear();
        foreach(Country country in Storage) 
        {
            Console.WriteLine($"Страна: {country.Name}\nБюджет: {country.Budget}\nНаселенные пункты:");
            foreach(Locality locality in country.Localities) 
            {
                Console.WriteLine($"   {locality.Name} // {DefineType(locality.GetType().Name)}");
                Console.WriteLine($"      Население: {locality.Population}");
                Console.WriteLine($"      Заболевшие: {locality.Infected}");
                Console.WriteLine($"      Вакцинированные: {locality.Vaccinated}");
                Console.WriteLine($"      Плотность трафика: {locality.Density}");
                switch(locality.GetType().Name) 
                {
                    case "Megapolis":
                    Console.WriteLine($"      Городов в мегаполисе: {((Megapolis)locality).CitiesMerged}");
                    break;
                    case "City":
                    Console.WriteLine($"      Количество ТЦ: {((City)locality).TCAmount}");
                    Console.WriteLine($"      Процент посещения ТЦ: {((City)locality).TCAttendPercentage}");
                    break;
                    case "Town":
                    Console.WriteLine($"      ФИО старейшины поселка: {((Town)locality).Elder}");
                    Console.WriteLine($"      Количество хозяйств: {((Town)locality).HouseholdAmount}");
                    break;
                }
            }
        }
        Console.ReadKey();
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
        Console.WriteLine("Главное меню\n1 - Назначить сезон и год\n2 - Внести новый НП\n3 - Добавить страну\n4 - Удаление\n5 - Формирование отчета\n6 - Вывести все данные");
        var Selector = Console.ReadLine();
        switch(Convert.ToInt32(Selector))
        {
            case 1: SeasonMenuOption(); break;
            case 2: LocalityMenuOption(); break;
            case 3: CountryMenuOption(); break;
            case 4: EditorMenuOption(); break;
            case 5: ReportGen(); break;
            case 6: ShowAll(); break;
        }
        Main();
    }
}