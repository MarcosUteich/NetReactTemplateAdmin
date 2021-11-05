using Business.Dashboard.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NetReactTemplateAdmin.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IOrderDashboardService _orderDashboardService;

        public DashboardController(
            ILogger<DashboardController> logger,
            IOrderDashboardService orderDashboardService)
        {
            _logger = logger;
            _orderDashboardService = orderDashboardService;
        }

        [HttpGet("getTotalEarning")]
        public string GetTotalEarning()
        {
            Thread.Sleep(TimeSpan.FromSeconds(7));
            return "7.556";
        }

        [HttpGet("getTotalOrderMes")]
        public string GetTotalOrderMes()
        {
            Thread.Sleep(TimeSpan.FromSeconds(2));
            return "113";
        }

        [HttpGet("getTotalOrderYear")]
        public string GetTotalOrderYear()
        {
            Thread.Sleep(TimeSpan.FromSeconds(1.5));
            return "556";
        }

        [HttpGet("getTotalIncome")]
        public string GetTotalIncome()
        {
            Thread.Sleep(TimeSpan.FromSeconds(0.6));
            return "124";
        }

        [HttpGet("getTotalGrowthBarChart")]
        public async Task<object> GetTotalGrowthBarChart()
        {
           return await _orderDashboardService.GetOrdersHistory();
        }

        [HttpGet("getDashboardStatus")]
        public object GetDashboardStatus()
        {
            return new[]
            {
                new { value = "today", label = "Today"},
                new { value = "month", label = "This Month"},
                new { value = "year", label = "This Year"},
            };
        }

        [HttpGet("getTotalGrowth")]
        public double GetTotalGrowth()
        {
            Thread.Sleep(TimeSpan.FromSeconds(3.8));

            return 458.00;
        }

        [HttpGet("getDashBajajArea")]
        public object GetDashBajajArea()
        {
            return new[] {
                new
                {
                    data = new int[] { 0, 15, 10, 50, 30, 40, 25 }
                }
            };
        }

        [HttpGet("GetTotalIncome2")]
        public double GetTotalIncome2()
        {
            return 783;
        }
    }
}
