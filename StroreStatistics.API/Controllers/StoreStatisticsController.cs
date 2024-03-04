using Microsoft.AspNetCore.Mvc;

namespace StoreStatistics.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class StoreStatisticsController : ControllerBase
    {
        [HttpGet("ads")]
        public IActionResult CalculateADS(int id, int countDay)
        {
            var res = BLL.Calculation.Ads(id, countDay);
            return Ok(res);
        }
        [HttpGet("prediction")]
        public IActionResult CalculateSalesPrediction(int id, int countDay)
        {
            var res = BLL.Calculation.SalesPrediction(id, countDay);
            return Ok(res);
        }
        [HttpGet("demand")]
        public IActionResult CalculateDemand(int id, int countDay)
        {
            var res = BLL.Calculation.Demand(id, countDay);
            return Ok(res);
        }
    }
}
