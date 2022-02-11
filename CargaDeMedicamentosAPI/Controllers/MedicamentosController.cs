using CargaDeMedicamentosAPI.Constants;
using CargaDeMedicamentosAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace CargaDeMedicamentosAPI.Controllers
{
    [Route(InternalRoutes.MEDIC)]
    [ApiController]
    [Produces(ConfigControllers.DEFAULT_OUTPUT_FORMAT)]
    public class MedicamentosController : ControllerBase
    {
        public MedicamentosController(ILogger<MedicamentosController> logger)
        {
            _logger = logger;
        }
        private ILogger<MedicamentosController> _logger { get; }

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
                return Ok(cargaIndividual);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
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
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Se obtiene el estado de una carga de un archivo mediante su identificador.
        /// </summary>
        /// <param name="idCarga"></param>
        /// <returns></returns>
        [HttpGet(RoutesPaths.MEDIC_STATE)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult EstadoCarga(string idCarga)
        {
            try
            {
                return Ok(idCarga);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Se obtienen los errores de carga de un archivo mediante su identificador.
        /// </summary>
        /// <param name="idCarga"></param>
        /// <returns></returns>
        [HttpGet(RoutesPaths.MEDIC_ERROR)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult ErrorCarga(string idCarga)
        {
            try
            {
                return Ok(idCarga);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
