using RiskCheck.Domain;

namespace RiskCheck.Application
{
    public interface IRiskCheckerService
    {
        ActionEnum GetOverallRisk(Risk risk);
    }
}