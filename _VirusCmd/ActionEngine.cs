using System;
using System.Text;
using _VirusCmd.Classes;
// Класс дейсвтий с странами и НП
public class ActionEngine
{

    bool isDangerous = false;
    float estimatedValue = 0;
    // Метод проверки эпидемологической ситуации в стране
    string CheckEpidemy(Country country)
    {
        Console.Clear();
        var a = ConsoleMarkupTools.SpaceGen(3);
        var b = ConsoleMarkupTools.SpaceGen(6);
        StringBuilder stringBuilder = new($"Эпидемологическая картина страны {country.Name}\n");
        foreach (Locality locality in country.Localities)
        {
            stringBuilder.AppendLine($"{a}{ConsoleMarkupTools.DefineLocalityType(locality.GetType().Name)} {locality.Name}");
            stringBuilder.AppendLine($"{b}Население - {locality.Population}");
            stringBuilder.AppendLine($"{b}Заболевших - {locality.Infected}");
            stringBuilder.AppendLine($"{b}Вакцинированных - {locality.Vaccinated}");
            stringBuilder.AppendLine($"{b}Соотношение заболевшие/население - {locality.CountInfectedPercentage()}");
            stringBuilder.AppendLine(ProceedLocality(locality));
        }
        return stringBuilder.ToString();
    }
    // Метод обработки НП
    string ProceedLocality(Locality locality)
    {
        Console.Clear();
        var a = ConsoleMarkupTools.SpaceGen(3);
        var b = ConsoleMarkupTools.SpaceGen(6);
        bool IsEpidemy = locality.CountInfectedPercentage() >= 45;
        StringBuilder stringBuilder = new($"{ConsoleMarkupTools.DefineLocalityType(locality.GetType().Name)} {locality.Name}\n");
        if (!IsEpidemy)
        {
            stringBuilder.AppendLine("Эпидемии в данном НП нет. Действий не требуется");
        }
        else 
        {
            isDangerous = true;
            estimatedValue += (locality.Population - locality.Vaccinated - locality.Infected) * 140;
            stringBuilder.AppendLine("Замечена эпидемия. Требуется действие");
        }
        return stringBuilder.ToString();
    }
    // Снижение уровня эпидемии в НПах страны
    void EpidemyReducer(ref Country country)
    {
        foreach(Locality locality in country.Localities)
        {
            if(locality.CountInfectedPercentage() >= 45)
            {
                locality.Infected -= (int)(locality.Infected * 0.5);
            }
        }
    }

    // Берет на вход ссылку на страну. Обработчик страны
    public void ProceedCountry(ref Country country)
    {
        // Сначала отчетные данные
        Console.WriteLine(CheckEpidemy(country));
        if(isDangerous)
        {
            Console.WriteLine($"Обнаружены эпидемии. Расчетная стоимость для принятия мер: {estimatedValue}");
            if(country.Budget < estimatedValue)
            {
                Console.WriteLine("Бюджета страны будет недостаточно!");
            }
            else
            {
                Console.WriteLine("Принять меры?\n1 - Да\n2 - Нет");
                var selector = Convert.ToInt32(Console.ReadLine());
                switch(selector)
                {
                    case 1:
                        EpidemyReducer(ref country);
                        country.Budget -= estimatedValue;
                        break;
                    case 2: return;
                }
            }
        }
        else
        {
            Console.WriteLine("Эпидемий не замечено");
        }
    }
}

