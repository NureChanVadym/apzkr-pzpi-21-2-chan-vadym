// ConsumptionCostController.cs

using EcoMeChan.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EcoMeChan.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsumptionCostController : ControllerBase
    {
        private readonly IConsumptionCostService _consumptionCostService;

        public ConsumptionCostController(IConsumptionCostService consumptionCostService)
        {
            _consumptionCostService = consumptionCostService;
        }

        [HttpGet("total-cost")]
        public async Task<IActionResult> GetTotalCost(int userId, DateTime startDate, DateTime endDate)
        {
            var totalCost = await _consumptionCostService.CalculateTotalCostAsync(userId, startDate, endDate);
            return Ok(new { TotalCost = totalCost });
        }
    }
}