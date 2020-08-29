using devboost.dronedelivery.felipe.DTO.Models;
using devboost.dronedelivery.felipe.Security.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace devboost.dronedelivery.felipe.Security
{
    public class SecurityClientProvider : ISecurityClientProvider
    {
        private readonly UserManager<Cliente> _userManager;
        public SecurityClientProvider(UserManager<Cliente> userManager)
        {
            _userManager = userManager;
        }
        public async Task CreateUser(
            Cliente cliente,
            string password,
            string initialRole = null)
        {
            var user = await _userManager.FindByNameAsync(cliente.UserName);
            if (user == null)
            {
                var resultado = await _userManager
                    .CreateAsync(cliente, password);

                if (resultado.Succeeded &&
                    !string.IsNullOrWhiteSpace(initialRole))
                {
                    await _userManager.AddToRoleAsync(cliente, initialRole);
                }
            }
        }

    }
}
