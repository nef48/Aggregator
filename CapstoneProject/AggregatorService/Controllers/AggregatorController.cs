using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AggregatorService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AggregatorController : ControllerBase
    {
        private readonly ILogger<AggregatorController> _logger;

        public AggregatorController(ILogger<AggregatorController> logger)
        {
            _logger = logger;
        }
    }
}
