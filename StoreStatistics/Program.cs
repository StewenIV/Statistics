using System.Linq.Expressions;

namespace Dal
{
    class Program
    {
        public static EnumStatistics.Statistics Statistics;
        private static string Str;
        private static List<string> StrList;
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Введите данные в таком формате: < что_рассчитать > < ID товара > [количество_дней]");
                Str = Console.ReadLine();
                StrList = ConvertToArray(Str).Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
                if (StrList.Count == 3)
                {
                    bool flag = CheckFormat(StrList[0], out Statistics);
                    if (!flag)
                    {
                        Console.WriteLine("Введите команду правильно!");
                        if (!CheckID(StrList[1]))
                        {
                            Console.WriteLine("Такого id нет");
                        }
                        if (int.Parse(StrList[2]) > 30)
                        {
                            Console.WriteLine("Нет статистики больше 30 дней");
                        }
                        continue;
                    }
                    Console.WriteLine("Все прошло успешно");
                   
                    switch (Statistics)
                    {
                        case EnumStatistics.Statistics.ads:
                            Console.WriteLine(Calculation.Ads(int.Parse(StrList[1]), int.Parse(StrList[2])));
                            break;
                        case EnumStatistics.Statistics.prediction:
                            Console.WriteLine(Calculation.SalesPrediction(int.Parse(StrList[1]), int.Parse(StrList[2])));
                            break;
                        case EnumStatistics.Statistics.demand:
                            Console.WriteLine(Calculation.Demand(int.Parse(StrList[1]), int.Parse(StrList[2])));
                            break;
                    }
                }
                else { Console.WriteLine("Введите корректно данные"); }
            }
        }
        private static bool CheckID(string str)
        {
           return Calculation.CheckID(str);
        }
        private static string[] ConvertToArray(string str)
        {
             return str.Split(" ");
        }
        private static bool CheckFormat(string str, out EnumStatistics.Statistics result)
        {
            result = EnumStatistics.Statistics.none;
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            if (Enum.TryParse(str, true, out EnumStatistics.Statistics parsedValue) && Enum.IsDefined(typeof(EnumStatistics.Statistics), parsedValue))
            {
                result = parsedValue;
                return true;
            }
            return false;
        }
    }
}
