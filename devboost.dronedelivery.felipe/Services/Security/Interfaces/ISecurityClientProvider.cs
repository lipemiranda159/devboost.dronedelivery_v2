using devboost.dronedelivery.felipe.DTO.Models;
using System.Threading.Tasks;

namespace devboost.dronedelivery.felipe.Security.Interfaces
{
    public interface ISecurityClientProvider
    {
        Task CreateUser(
            Cliente cliente,
            string password,
            string initialRole = null);
    }
}