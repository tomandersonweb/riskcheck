using System.Threading.Tasks;

namespace RiskCheck.Application
{
    public interface IPostCodeDistanceService
    {
        Task<int> GetDistance(string riskPostCode, string keptPostCode);
    }
}