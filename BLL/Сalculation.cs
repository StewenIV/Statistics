using Dal;

namespace BLL
{
    public class Calculation
    {
        private static List<SaleData> saleDatas = ReceivingData.ReceivingSalesData();
        private static List<SeasonalityCoefficient> seasonalityCoefficients = ReceivingData.ReceivingSeasonalityCoefficient();
        public static float Ads(int id,int countDay)
        {
            try
            {
                float sum = 0;
                foreach (SaleData s in saleDatas)
                {
                    if (s.Id != id)
                        break;
                    if (s.Stock != 0)
                    {
                        sum += s.Sales;
                    }
                }
                return sum / countDay;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Что-то пошло не так в Ads: {ex.Message} ");
                return float.E;
            }
        }
        public static float SalesPrediction(int id, int countDay)
        {
            try
            {
                float Coefficient = 0;
                float temp = Ads(id, countDay);
                foreach (SeasonalityCoefficient s in seasonalityCoefficients)
                {
                    if (s.Id == id)
                    {
                        Coefficient = s.Coefficient;
                    }
                }
                return temp * countDay * Coefficient;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Что-то пошло не так в SalesPrediction: {ex.Message} ");
                return float.E;
            }
        }
        public static float Demand(int id, int countDay)
        {
            try
            {
                float res = 0;
                float temp = SalesPrediction(id,countDay);
                foreach (SaleData s in saleDatas)
                {
                    if (s.Id == id)
                    {
                        res = temp - s.Stock;
                    }
                }
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Что-то пошло не так в Demand: {ex.Message} ");
                return float.E;
            }
        }
        public static bool CheckID(string str)
        {
            try
            {
                foreach (SaleData s in saleDatas)
                {
                    if (s.Id == int.Parse(str))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Что-то пошло не так про проверке ID: {ex.Message} ");
                return false;
            }
        }
    }
}
