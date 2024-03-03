using BLL;

namespace StoreStatistics
{
    class Program
    {
        private static Statistics Statistics;
        private static string? Str;
        private static List<string>? StrList;
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Введите данные в таком формате: < что_рассчитать > < ID товара > [количество_дней]");
                Str = Console.ReadLine() ?? string.Empty;
                if (Str != string.Empty)
                {
                    StrList = Str.Split(" ").Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
                    if (StrList.Count == 3)
                    {
                        bool flag = CheckFormat(StrList[0], out Statistics);
                        if (!flag)
                        {
                            Console.WriteLine("Введите комунду рассчета верно!");
                            continue;
                        }
                        if (!CheckID(StrList[1]))
                        {
                            Console.WriteLine("Такого id нет!");
                            continue;
                        }
                        if (int.Parse(StrList[2]) > 30 || int.Parse(StrList[2]) < 0)
                        {
                            Console.WriteLine("Дни указаны неправильно!");
                            continue;
                        }
                        Console.WriteLine("Данные введены верно!");
                        switch (Statistics)
                        {
                            case Statistics.Ads:
                                float str = Calculation.Ads(int.Parse(StrList[1]), int.Parse(StrList[2]));
                                Console.WriteLine(str == float.E ? "" : str) ;
                                break;
                            case Statistics.Prediction:
                                str = Calculation.SalesPrediction(int.Parse(StrList[1]), int.Parse(StrList[2]));
                                Console.WriteLine(str == float.E ? "" : str);
                                break;
                            case Statistics.Demand:
                                str = Calculation.Demand(int.Parse(StrList[1]), int.Parse(StrList[2]));
                                Console.WriteLine(str == float.E ? "" : str);
                                break;
                            default:
                                Console.WriteLine("Такой команды не существует");
                                break;
                        }
                    }
                    else { Console.WriteLine("Введите корректно данные"); }
                }
                else { Console.WriteLine("Введите корректно данные"); }
            }
        }
        private static bool CheckID(string str)
        {
           return Calculation.CheckID(str);
        }
        private static bool CheckFormat(string str, out Statistics result)
        {
            result = Statistics.None;
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            if (Enum.TryParse(str, true, out Statistics parsedValue) && Enum.IsDefined(typeof(Statistics), parsedValue))
            {
                result = parsedValue;
                return true;
            }
            return false;
        }
    }
}
