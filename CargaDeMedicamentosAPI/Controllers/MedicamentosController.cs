using System;
using System.Collections.Generic;
using CargaDeMedicamentosAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CargaDeMedicamentosAPI.Controllers
{
    [Route("api/medicamentos")]
    [ApiController]
    public class MedicamentosController : ControllerBase
    {
        public MedicamentosController(ILogger logger)
        {
            Logger = logger;
        }
        private ILogger Logger { get; }

        /// <summary>
        /// Se realiza la carga individual mediante el identificador de la sucursal y el codigo TFC.
        /// </summary>
        /// <param name="cargaIndividual"></param>
        /// <returns></returns>
        [HttpPatch()]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<IEnumerable<CargaIndividualOutput>> CargaIndividual([FromQuery] CargaIndividualInput cargaIndividual)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Se realiza la carga masiva indicando el identificador de la sucursal y el archivo binario de la carga.
        /// </summary>
        /// <param name="idSucursal"></param>
        /// <param name="cargaMasiva"></param>
        /// <returns></returns>
        [HttpPost()]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult CargaMasiva([FromQuery] string idSucursal, [FromBody] CargaMasivaInput cargaMasiva)
        {
            try
            {
                return Ok(cargaMasiva);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Se obtiene el estado de una carga de un archivo mediante su identificador.
        /// </summary>
        /// <param name="idCarga"></param>
        /// <returns></returns>
        [HttpGet("estado")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult EstadoCarga(string idCarga)
        {
            try
            {
                return Ok(idCarga);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Se obtienen los errores de carga de un archivo mediante su identificador.
        /// </summary>
        /// <param name="idCarga"></param>
        /// <returns></returns>
        [HttpGet("error")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult ErrorCarga(string idCarga)
        {
            try
            {
                return Ok(idCarga);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
