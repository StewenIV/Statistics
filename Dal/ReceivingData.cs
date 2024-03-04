using System.Globalization;

namespace Dal
{
    public class ReceivingData
    {
        private const string SalesFilePath = "../InputData/SalesHistory.txt";
        private const string SeasonalityFilePath = "../InputData/Coef.txt";

        public static List<SaleData> ReceivingSalesData()
        {
            return ReadSalesData(SalesFilePath);
        }
        public static List<SeasonalityCoefficient> ReceivingSeasonalityCoefficient()
        {
            return ReadSeasonalityData(SeasonalityFilePath);
        }
        private static List<SaleData> ReadSalesData(string filePath)
        {
            try
            {
                List<SaleData> salesData = File.ReadAllLines(filePath)
                    .Skip(1)
                    .Select(line =>
                    {
                        string[] field = line.Split(",");
                        for (int i = 0; i < field.Length; i++)
                        {
                            field[i] = field[i].Trim();
                        }
                        return field;
                    })
                    .Select(fields => new SaleData
                    {
                        Id = int.Parse(fields[0], CultureInfo.InvariantCulture),
                        Date = DateTime.Parse(fields[1], CultureInfo.InvariantCulture),
                        Sales = int.Parse(fields[2], CultureInfo.InvariantCulture),
                        Stock = int.Parse(fields[3], CultureInfo.InvariantCulture)
                    })
                    .ToList();

                return salesData;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Ошибка работы с файлами: {ex.Message}");
                return new List<SaleData>();
            }
        }
        private static List<SeasonalityCoefficient> ReadSeasonalityData(string filePath)
        {
            try
            {
                List<SeasonalityCoefficient> seasonalityData = File.ReadAllLines(filePath)
                    .Skip(1)
                    .Select(line =>
                    {
                        string[] field = line.Split(",");
                        for (int i = 0; i < field.Length; i++)
                        {
                            field[i] = field[i].Trim();
                        }
                        return field;
                    })
                    .Select(fields => new SeasonalityCoefficient
                    {
                        Id = int.Parse(fields[0]),
                        Month = int.Parse(fields[1]),
                        Coefficient = float.Parse(fields[2], CultureInfo.InvariantCulture)
                    })
                    .ToList();

                return seasonalityData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка работы с файлами: {ex.Message}");
                return new List<SeasonalityCoefficient>();
            }
        }
    }

   public class SaleData
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Sales { get; set; }
        public int Stock { get; set; }
    }

    public class SeasonalityCoefficient
    {
        public int Id { get; set; }
        public int Month { get; set; }
        public float Coefficient { get; set; }
    }
}
