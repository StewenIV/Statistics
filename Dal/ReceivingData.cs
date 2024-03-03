using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class ReceivingData
    {
        static string salesFilePath = "C:\\Users\\077764842\\source\\repos\\BLL\\Dal\\InputData\\SalesHistory.txt";
        static string seasonalityFilePath = "C:\\Users\\077764842\\source\\repos\\BLL\\Dal\\InputData\\Coef.txt";

        public static List<SaleData> ReceivingSalesData()
        {
            return ReadSalesData(salesFilePath);
        }
        public static List<SeasonalityCoefficient> ReceivingSeasonalityCoefficient()
        {
            return ReadSeasonalityData(seasonalityFilePath);
        }
        private static List<SaleData> ReadSalesData(string filePath)
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
        private static List<SeasonalityCoefficient> ReadSeasonalityData(string filePath)
        {
            List<SeasonalityCoefficient> seasonalityData = File.ReadAllLines(filePath)
                .Skip(1) 
                .Select(line =>
                {
                    string[] field = line.Split(",");
                    for(int i = 0; i<field.Length; i++)
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
