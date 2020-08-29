using devboost.dronedelivery.felipe.DTO.Models;
using devboost.dronedelivery.felipe.EF.Data;
using devboost.dronedelivery.felipe.Security.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace devboost.dronedelivery.felipe.Security
{
    public class IdentityInitializer
    {
        private readonly ISecurityClientProvider _securityClientProvider;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityInitializer(
            ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            ISecurityClientProvider securityClientProvider)
        {
            _context = context;
            _roleManager = roleManager;
            _securityClientProvider = securityClientProvider;
        }

        public async Task Initialize()
        {
            if (_context.Database.EnsureCreated())
            {
                var roleExists = await _roleManager.RoleExistsAsync(Roles.ROLE_API_DRONE);
                if (!roleExists)
                {
                    var resultado = await _roleManager.CreateAsync(
                        new IdentityRole(Roles.ROLE_API_DRONE));
                    if (!resultado.Succeeded)
                    {
                        throw new Exception(
                            $"Erro durante a criação da role {Roles.ROLE_API_DRONE}.");
                    }
                }

                await _securityClientProvider.CreateUser(
                    new Cliente()
                    {
                        UserName = "admin_drone",
                        Email = "admin-apiprodutos@teste.com.br",
                        EmailConfirmed = true
                    }, "AdminAPIDrone01!", Roles.ROLE_API_DRONE);

                await _securityClientProvider.CreateUser(
                    new Cliente()
                    {
                        UserName = "usuario_drone",
                        Email = "usrinvalido-apiprodutos@teste.com.br",
                        EmailConfirmed = true
                    }, "UsrInvAPIDrone01!");
            }
        }
    }
}
