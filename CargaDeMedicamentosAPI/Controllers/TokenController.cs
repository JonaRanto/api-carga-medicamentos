using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CargaDeMedicamentosAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace CargaDeMedicamentosAPI.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public TokenController(IConfiguration configuration, ILogger<TokenController> logger)
        {
            Configuration = configuration;
            Logger = logger;
        }
        private IConfiguration Configuration { get; }
        private ILogger<TokenController> Logger { get; }


        /// <summary>
        /// Se obtiene un token de autenticación utilizando el uid del usuario.
        /// </summary>
        /// <returns></returns>
        [HttpPost("generate")]
        public ActionResult<IEnumerable<UserToken>> GetToken([FromQuery] string uid)
        {
            try
            {
                UserToken token = BuildToken(uid);
                return Ok(token);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        private UserToken BuildToken(string uid)
        {
            var claims = new List<Claim>
            {
                new Claim("uid", uid),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT_KEY"]));
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
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = expiration
            };
        }
    }
}
