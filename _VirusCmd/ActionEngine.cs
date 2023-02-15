using System;
using System.Text;
using _VirusCmd.Classes;

public class ActionEngine
{
    string CheckEpidemy(Country country)
    {
        Console.Clear();
        var a = ConsoleMarkupTools.SpaceGen(3);
        var b = ConsoleMarkupTools.SpaceGen(6);
        StringBuilder stringBuilder = new($"Эпидемологическая картина страны {country.Name}");
        foreach (Locality locality in country.Localities)
        {
            stringBuilder.Append($"{a}{ConsoleMarkupTools.DefineLocalityType(locality.GetType().Name)} {locality.Name}");
            stringBuilder.Append($"{b}Население - {locality.Population}");
            stringBuilder.Append($"{b}Заболевших - {locality.Infected}");
            stringBuilder.Append($"{b}Вакцинированных - {locality.Vaccinated}");
            stringBuilder.Append($"{b}Соотношение заболевшие/население - {locality.CountInfectedPercentage()}");
            stringBuilder.Append(locality.CountInfectedPercentage() >= 45 ? "Внимание! Эпидемия!" : "ОК");
        }
        return stringBuilder.ToString();
    }
    string ProceedLocality(Locality locality)
    {
        Console.Clear();
        var a = ConsoleMarkupTools.SpaceGen(3);
        var b = ConsoleMarkupTools.SpaceGen(6);
        bool IsEpidemy = locality.CountInfectedPercentage() >= 45;
        StringBuilder stringBuilder = new($"{ConsoleMarkupTools.DefineLocalityType(locality.GetType().Name)} {locality.Name}");
        if (!IsEpidemy)
        {
            stringBuilder.Append("Эпидемии в данном НП нет. Действий не требуется");
        }
        else 
        {
            stringBuilder.Append("Замечена эпидемия. Требуется действие");
        }
        return stringBuilder.ToString();
    }
    // Берет на вход ссылку на страну
    public void ProceedCountry(ref Country country)
    {
        // Сначала отчетные данные
        Console.WriteLine(CheckEpidemy(country));

    }
}

