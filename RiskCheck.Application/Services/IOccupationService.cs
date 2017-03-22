using RiskCheck.Domain;
using System.Threading.Tasks;

namespace RiskCheck.Application
{
    public interface IOccupationService
    {
        Task<ActionEnum> GetActionForOccupation(string occupation);
    }
}