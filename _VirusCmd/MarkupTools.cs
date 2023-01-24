public static class ConsoleMarkupTools
{
    public static string SpaceGen(int degree)
    {
        string temp = "";
        for (int i = 0; i < degree; i++)
        {
            temp += " ";
        }
        return temp;
    }
    public static string DefineType(string className)
    {
        switch (className)
        {
            case "Megapolis": return "Мегаполис";
            case "City": return "Город";
            case "Town": return "Поселок";
            default: return "Нечто";
        }
    }
}