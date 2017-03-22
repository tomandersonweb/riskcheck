using RiskCheck.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RiskCheck.Application
{
    public class OccupationCheckCalculation : IRiskCalculationRule
    {
        private readonly IOccupationService _occupationService;

        public OccupationCheckCalculation(IOccupationService occupationService)
        {
            _occupationService = occupationService;
        }

        public async Task<ActionEnum> CalculateRisk(Risk risk)
        {
            // Use the occupation lookup data provided and check the risks occupation and return the appropriate action.
            return await _occupationService.GetActionForOccupation(risk.Occupation);
        }
    }
}
