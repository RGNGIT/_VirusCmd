using _VirusCmd.Classes;
// Перечень айдишников времен года
enum ESeason
{
    Winter = 0,
    Spring = 1,
    Summer = 2,
    Fall = 3
}
// Перечень айдишников населенных пунктов
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
    // Установка года и времени года
    static void SetSeasonAndYear(int Year, int SeasonKey)
    {
        seasonSingleton.Year = Year;
        seasonSingleton.Season = SeasonsDictionary[SeasonKey];
    }
    // Метод назначения времени года
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
    // Добавление заболевания в НП
    static void EditDisease(ref Locality locality)
    {
        Console.WriteLine("Назначьте заболевание в данном НП:\n1 - Вирус\n2 - Бактериальное заболевание");
        var selector = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Название:");
        string? Name = Console.ReadLine();
        Console.WriteLine("Описание:");
        string? Description = Console.ReadLine();
        switch(selector) 
        {
            case 1:
                Console.WriteLine("Штамм:");
                string? Shtamm = Console.ReadLine();
                Console.WriteLine("Инкубационный период:");
                string? IncubationPeriod  = Console.ReadLine();
                locality.Disease = new Virus(Name!, Description!, Shtamm!, IncubationPeriod!);
                break;
            case 2:
                Console.WriteLine("Зона распространения:");
                string? Zone = Console.ReadLine();
                locality.Disease = new Bacteria(Name!, Description!, Zone!);
                break;
        }
    }
    // Добавление нового НП
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
            EditDisease(ref locality);
            Storage[countryKey].Localities.Add(locality);
        }
        else
        {
            Console.WriteLine("Экземпляр не инициализирован");
            Console.ReadKey();
        }
    }
    // Выбор в какую страну добавить НП
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
    // Добавление страны
    static void CountryMenuOption()
    {
        Console.Clear();
        Console.WriteLine("Добавить страну:\nНавзвание:");
        var Name = Console.ReadLine();
        Console.WriteLine("Бюджет:");
        var Budget = Console.ReadLine();
        Storage.Add(new Country(Name != null ? Name.ToString() : "Не назначено", Convert.ToSingle(Budget)));
    }
    // Удаление страны
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
    // Удаление НП
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
    // Выбор что удалить
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
    // Вызов меню действий
    static void Actions()
    {
        Console.Clear();
        Console.WriteLine("Выберите страну для действия");
        for(int i = 0; i < Storage.Count; i++)
        {
            Console.WriteLine($"{i + 1} - {Storage[i].Name}");
        }
        var selector = Convert.ToInt32(Console.ReadLine());
        ActionEngine engine = new();
        Country country = Storage[selector - 1];
        engine.ProceedCountry(ref country);
        Storage[selector - 1] = country;
        Console.ReadKey();
    }
    // Показать все данные
    static void ShowAll()
    {
        Console.Clear();
        var a = ConsoleMarkupTools.SpaceGen(3); 
        var b = ConsoleMarkupTools.SpaceGen(6);
        var c = ConsoleMarkupTools.SpaceGen(9);
        foreach (Country country in Storage) 
        {
            Console.WriteLine($"Страна: {country.Name}\nБюджет: {country.Budget}\nНаселенные пункты:");
            foreach(Locality locality in country.Localities) 
            {
                Console.WriteLine($"{a}{ConsoleMarkupTools.DefineLocalityType(locality.GetType().Name)} {locality.Name}");
                Console.WriteLine($"{b}Население: {locality.Population}");
                Console.WriteLine($"{b}Заболевание: {ConsoleMarkupTools.DefineDiseaseType(locality.Disease!.GetType().Name)}, {locality.Disease.Name}, {locality.Disease.Description}");
                switch(locality.Disease!.GetType().Name)
                {
                    case "Virus":
                        Console.WriteLine($"{c}Штамм: {((Virus)locality.Disease).Shtamm}");
                        Console.WriteLine($"{c}Инкубационный период: {((Virus)locality.Disease).IncubationPeriod}");
                        break;
                    case "Bacteria":
                        Console.WriteLine($"{c}Зона поражения: {((Bacteria)locality.Disease).Zone}");
                        break;
                }
                Console.WriteLine($"{b}Заболевшие: {locality.Infected}");
                Console.WriteLine($"{b}Вакцинированные: {locality.Vaccinated}");
                Console.WriteLine($"{b}Плотность трафика: {locality.Density}");
                switch(locality.GetType().Name) 
                {
                    case "Megapolis":
                    Console.WriteLine($"{b}Городов в мегаполисе: {((Megapolis)locality).CitiesMerged}");
                    break;
                    case "City":
                    Console.WriteLine($"{b}Количество ТЦ: {((City)locality).TCAmount}");
                    Console.WriteLine($"{b}Процент посещения ТЦ: {((City)locality).TCAttendPercentage}");
                    break;
                    case "Town":
                    Console.WriteLine($"{b}ФИО старейшины поселка: {((Town)locality).Elder}");
                    Console.WriteLine($"{b}Количество хозяйств: {((Town)locality).HouseholdAmount}");
                    break;
                }
            }
        }
        Console.ReadKey();
    }
    // Сохранение данных
    public static void SaveData() 
    {
        Console.Clear();
        _VirusCmd.DataManager.Save(Storage, seasonSingleton);
        Console.WriteLine("Данные сохранены");
        Console.ReadKey();
    }
    // Загрузка данных
    public static void LoadData() 
    {
        Console.Clear();
        dynamic container = _VirusCmd.DataManager.Read();
        Storage = ((_VirusCmd.DataManager.SerializableContainer)container).countries!;
        seasonSingleton = ((_VirusCmd.DataManager.SerializableContainer)container).season!;
        Console.WriteLine("Данные загружены");
        Console.ReadKey();
    }
    // Точка входа программы
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
        Console.WriteLine("Главное меню\n1 - Назначить сезон и год\n2 - Внести новый НП\n3 - Добавить страну\n4 - Удаление\n5 - Действия\n6 - Вывести все данные\n7 - Сохранить данные\n8 - Загрузить данные");
        var Selector = Console.ReadLine();
        switch(Convert.ToInt32(Selector))
        {
            case 1: SeasonMenuOption(); break;
            case 2: LocalityMenuOption(); break;
            case 3: CountryMenuOption(); break;
            case 4: EditorMenuOption(); break;
            case 5: Actions(); break;
            case 6: ShowAll(); break;
            case 7: SaveData(); break;
            case 8: LoadData(); break;
        }
        Main();
    }
}