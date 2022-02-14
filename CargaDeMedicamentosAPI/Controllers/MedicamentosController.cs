using CargaDeMedicamentosAPI.Constants;
using CargaDeMedicamentosAPI.Context;
using CargaDeMedicamentosAPI.Entities;
using CargaDeMedicamentosAPI.Models;
using CargaDeMedicamentosAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CargaDeMedicamentosAPI.Controllers
{
    [Route(InternalRoutes.MEDIC)]
    [ApiController]
    [Produces(ConfigControllers.DEFAULT_OUTPUT_FORMAT)]
    public class MedicamentosController : ControllerBase
    {
        public MedicamentosController(ILogger<MedicamentosController> logger, AppDbContext context)
        {
            this.Logger = logger;
            this.Context = context;
        }
        private ILogger<MedicamentosController> Logger { get; }
        private AppDbContext Context { get; }

        [HttpGet("getAll")]
        public async Task<ActionResult> ObtenerMedicamentos()
        {
            var response = await Context.Emedicamentos.FindAsync();
            return Ok(response);
        }


        /// <summary>
        /// Se realiza la carga individual mediante el identificador de la sucursal y el codigo TFC.
        /// </summary>
        /// <param name="cargaIndividual"></param>
        /// <returns></returns>
        [HttpPatch()]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<CargaIndividualOutput>> CargaIndividual([FromBody] CargaIndividualInput cargaIndividual)
        {
            try
            {
                string uid = "26FB7924-7262-4151-8B0E-4DE09E205BFD";//GetUserId();

                var persona = await Context.People.Where(p => p.UserId == uid).FirstOrDefaultAsync();
                var medicamento = await Context.Emedicamentos.FindAsync(cargaIndividual.CodigoTFC);
                var precioFarmacia = await Context.EprecioFarmacia.FindAsync(cargaIndividual.CodigoTFC, cargaIndividual.IdSucursal);
                ServiceOutput DbResponse = MedicamentosService.UpdatePrecioFarmacia(cargaIndividual, precioFarmacia);

                // SI NO HAY ERRORES EN LA RESPUESTA, SE APLICAN LOS CAMBIOS EN LA BASE DE DATOS.
                if (!DbResponse.Error)
                {
                    await Context.SaveChangesAsync();
                }

                return Ok(DbResponse);
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
                Logger.LogError(ex.Message);
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
                Logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        private string GetUserId()
        {
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
            string uid = identity.FindFirst("uid").Value;
            return uid;
        }
    }
}
