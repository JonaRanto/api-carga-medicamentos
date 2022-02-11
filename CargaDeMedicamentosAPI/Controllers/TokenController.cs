using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CargaDeMedicamentosAPI.Constants;
using CargaDeMedicamentosAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace CargaDeMedicamentosAPI.Controllers
{
    [Route(InternalRoutes.TOKEN)]
    [ApiController]
    [Produces("application/json")]
    public class TokenController : ControllerBase
    {
        public TokenController(IConfiguration configuration, ILogger<TokenController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        private IConfiguration _configuration { get; }
        private ILogger<TokenController> _logger { get; }


        /// <summary>
        /// Se obtiene un token de autenticación utilizando el uid del usuario.
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [HttpPost(RoutesPaths.TOKEN_GENERATE)]
        public ActionResult<IEnumerable<UserToken>> GetToken([FromQuery] string uid)
        {
            try
            {
                UserToken token = BuildToken(uid);
                return Ok(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Recibe un uid para generar y retornar un token.
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        private UserToken BuildToken(string uid)
        {
            var claims = new List<Claim>
            {
                new Claim("uid", uid),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT_KEY"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(4);

            JwtSecurityToken token = new(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
                );

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
