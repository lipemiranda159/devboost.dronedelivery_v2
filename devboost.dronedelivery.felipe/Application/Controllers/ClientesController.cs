using devboost.dronedelivery.felipe.DTO.Models;
using devboost.dronedelivery.felipe.Security.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace devboost.dronedelivery.felipe.Controllers
{
    [Authorize(Roles = Roles.ROLE_API_DRONE)]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly UserManager<Cliente> _userManager;
        private readonly ISecurityClientProvider _securityClientProvider;
        public ClientesController(UserManager<Cliente> userManager, ISecurityClientProvider securityClientProvider)
        {
            _userManager = userManager;
            _securityClientProvider = securityClientProvider;
        }

        // POST api/<ClientesController>
        [HttpPost]
        public async Task Post([FromBody] Cliente cliente)
        {
            await _securityClientProvider.CreateUser(cliente, cliente.Password);
        }
    }
}
