using devboost.dronedelivery.felipe.DTO.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace devboost.dronedelivery.felipe.Security
{
    public class AccessManager
    {
        private UserManager<Cliente> _userManager;
        private SignInManager<Cliente> _signInManager;
        private SigningConfigurations _signingConfigurations;
        private TokenConfigurations _tokenConfigurations;

        public AccessManager(
            UserManager<Cliente> userManager,
            SignInManager<Cliente> signInManager,
            SigningConfigurations signingConfigurations,
            TokenConfigurations tokenConfigurations)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
        }

        public async Task<bool> ValidateCredentialsAsync(Cliente cliente)
        {
            bool credenciaisValidas = false;
            if (cliente != null && !string.IsNullOrWhiteSpace(cliente.UserName))
            {
                var userIdentity = await _userManager.FindByNameAsync(cliente.UserName);
                if (userIdentity != null)
                {
                    var resultadoLogin = await _signInManager
                        .CheckPasswordSignInAsync(userIdentity, cliente.Password, false);
                    if (resultadoLogin.Succeeded)
                    {
                        credenciaisValidas = await  _userManager.IsInRoleAsync(
                            userIdentity, Roles.ROLE_API_DRONE);
                    }
                }
            }

            return credenciaisValidas;
        }

        public Token GenerateToken(Cliente user)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(user.UserName, "Login"),
                new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
                }
            );

            DateTime dataCriacao = DateTime.Now;
            DateTime dataExpiracao = dataCriacao +
                TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = dataCriacao,
                Expires = dataExpiracao
            });
            var token = handler.WriteToken(securityToken);

            return new Token()
            {
                Authenticated = true,
                Created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                Expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = token,
                Message = "OK"
            };
        }
    }
}
