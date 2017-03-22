using RiskCheck.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RiskCheck.Application
{
    public class RiskCheckerService : IRiskCheckerService
    {
        private List<ActionEnum> _results;
        private IEnumerable<IRiskCalculationRule> _riskCalcRules;

        public RiskCheckerService(IEnumerable<IRiskCalculationRule> riskCalcRules)
        {
            _riskCalcRules = riskCalcRules;
            _results = new List<ActionEnum>();
        }

        public ActionEnum GetOverallRisk(Risk risk)
        {
            foreach (var rule in _riskCalcRules)
                _results.Add(rule.CalculateRisk(risk).Result);

            if (_results.Any(x => x == ActionEnum.Decline))
                return ActionEnum.Decline;

            if (_results.Any(x => x == ActionEnum.Refer))
                return ActionEnum.Refer;

            return ActionEnum.Accept;
        }
    }
}
