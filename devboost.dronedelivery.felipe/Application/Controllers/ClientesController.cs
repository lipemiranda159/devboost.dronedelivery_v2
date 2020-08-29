using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using devboost.dronedelivery.felipe.DTO.Models;
using devboost.dronedelivery.felipe.Security.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

        // PUT api/<ClientesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ClientesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
