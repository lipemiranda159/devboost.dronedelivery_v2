using devboost.dronedelivery.felipe.DTO.Models;
using devboost.dronedelivery.felipe.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace devboost.dronedelivery.felipe.Controllers
{

    /// <summary>
    /// Controller com operações referentes aos drones
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController
    {
        public LoginController()
        {
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<object> Post([FromBody] Cliente usuario, [FromServices] AccessManager accessManager)
        {
            if (await accessManager.ValidateCredentialsAsync(usuario))
            {
                return accessManager.GenerateToken(usuario);
            }
            else
            {
                return new
                {
                    Authenticated = false,
                    Message = "Falha ao autenticar"
                };
            }
        }


    }
}
