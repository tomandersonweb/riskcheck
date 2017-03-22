using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RiskCheck.Domain;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RiskCheck.Web.ViewModels;
using System.Reflection;
using Omu.ValueInjecter;
using RiskCheck.Application;

namespace RiskCheck.Web.Controllers
{
    [Route("api/[controller]")]
    public class RiskController : Controller
    {
        private readonly IRiskCheckerService _riskCheckerService;

        public RiskController(IRiskCheckerService riskCheckerService)
        {
            _riskCheckerService = riskCheckerService;
        }

        [HttpPost, Produces("application/xml")]
        public IActionResult Post([FromBody]RiskViewModel riskViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var risk = new Risk();
            risk.InjectFrom(riskViewModel);
            risk.Address.InjectFrom(riskViewModel.Address);

            var resultViewModel = new Result();
            resultViewModel.Action = _riskCheckerService.GetOverallRisk(risk).ToString();

            return Ok(resultViewModel);
        }
    }
}
