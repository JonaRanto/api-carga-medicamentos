﻿using CargaDeMedicamentosAPI.Constants;
using CargaDeMedicamentosAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CargaDeMedicamentosAPI.Controllers
{
    [Route(InternalRoutes.TOKEN)]
    [ApiController]
    [Produces(ConfigControllers.DEFAULT_OUTPUT_FORMAT)]
    public class TokenController : ControllerBase
    {
        public TokenController(IConfiguration configuration, ILogger<TokenController> logger)
        {
            this.Configuration = configuration;
            this.Logger = logger;
        }
        private IConfiguration Configuration { get; }
        private ILogger<TokenController> Logger { get; }


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
                Logger.LogError(ex.Message);
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
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
