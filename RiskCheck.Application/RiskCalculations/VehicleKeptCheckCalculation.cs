using RiskCheck.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace RiskCheck.Application
{
    public class VehicleKeptCheckCalculation : IRiskCalculationRule
    {
        private readonly IPostCodeDistanceService _postCodeService;

        public VehicleKeptCheckCalculation(IPostCodeDistanceService postCodeService)
        {
            _postCodeService = postCodeService;
        }

        public async Task<ActionEnum> CalculateRisk(Risk risk)
        {
            var keptPostCode = string.Concat(risk?.KeptPostcode?.Where(x => char.IsLetterOrDigit(x)))?.ToLower() ?? string.Empty;
            var riskPostCode = string.Concat(risk?.Address?.Postcode?.Where(x => char.IsLetterOrDigit(x)))?.ToLower() ?? string.Empty;

            // If no kept postcode is provided it is assumed to be the same as the risks postcode.
            if (string.IsNullOrEmpty(keptPostCode) || keptPostCode == riskPostCode)
                return ActionEnum.Accept;

            // You should take the risks postcode and calculate the distance to the kept postcode.
            var result = await _postCodeService.GetDistance(riskPostCode, keptPostCode);

            // If the distance is less than 10 meters you should accept the risk
            if (result < 10)
                return ActionEnum.Accept;

            // If the distance is more than 100 meters you should decline the risk.
            if (result > 100)
                return ActionEnum.Decline;

            // If this is more than 10 meters away you should refer the disk.
            return ActionEnum.Refer;
        }
    }
}
