namespace Dal
{
    public class Calculation
    {
        private static List<SaleData> saleDatas = ReceivingData.ReceivingSalesData();
        private static List<SeasonalityCoefficient> seasonalityCoefficients = ReceivingData.ReceivingSeasonalityCoefficient();
        public static float Ads(int id,int countDay)
        {
            float sum = 0;
            foreach (SaleData s in saleDatas)
            {
                if(s.Id != id && s.Date.Day > countDay)
                    break;
                if(s.Stock != 0)
                {
                    sum += s.Sales;
                }
            }
            return sum/countDay;
        }
        public static float SalesPrediction(int id,int countDay)
        {
            float Coefficient = 0;
            float temp = Ads(countDay, id);
            foreach (SeasonalityCoefficient s in seasonalityCoefficients) 
            {
                if(s.Id == id)
                {
                    Coefficient = s.Coefficient;
                }
            }
            return temp*countDay*Coefficient;
        }
        public static float Demand(int id,int countDay)
        {
            float res = 0;
            float temp = SalesPrediction(countDay, id);
            foreach (SaleData s in saleDatas)
            {
                if (s.Id == id && s.Date.Day > countDay)
                {
                    res = temp - s.Stock;
                }
            }
            return res;
        }
        public static bool CheckID(string str)
        {
            foreach(SaleData s in saleDatas)
            {
                if (s.Id == int.Parse(str))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
