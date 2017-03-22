using RiskCheck.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RiskCheck.Application
{
    public interface IRiskCalculationRule
    {
        Task<ActionEnum> CalculateRisk(Risk risk);
    }
}
