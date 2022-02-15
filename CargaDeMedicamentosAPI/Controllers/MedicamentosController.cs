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

        /// <summary>
        /// Se realiza la carga individual mediante el identificador de la sucursal y el codigo TFC.
        /// </summary>
        /// <param name="sucursal_id"></param>
        /// <param name="codigoTFC"></param>
        /// <param name="cargaIndividual"></param>
        /// <returns></returns>
        [HttpPatch(RoutesPaths.MEDIC_INDIVIDUAL_LOAD)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ServiceOutput>> CargaIndividual(string sucursal_id, string codigoTFC, [FromBody] CargaIndividualInput cargaIndividual)
        {
            try
            {
                string uid = "0E6AB68E-E60E-4EBA-80AB-F7EF84AB350B";//GetUserId();

                Person persona = Context.People.Where(p => p.UserId == uid).FirstOrDefault();
                Emedicamento medicamento = await Context.Emedicamentos.FindAsync(codigoTFC);
                EprecioFarmacium precioFarmacia = await Context.EprecioFarmacia.FindAsync(codigoTFC, sucursal_id);
                ServiceOutput DbResponse = MedicamentosService.UpdatePrecioFarmacia(cargaIndividual, precioFarmacia);

                // SI NO HAY ERRORES EN LA RESPUESTA, SE APLICAN LOS CAMBIOS EN LA BASE DE DATOS.
                if (!DbResponse.Error)
                {
                    UpdateHistorialPrecio(precioFarmacia, medicamento, persona);
                    await Context.SaveChangesAsync();
                    return Ok(DbResponse);
                }
                else
                {
                    return BadRequest(DbResponse);
                }
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
        /// <param name="sucursal_id"></param>
        /// <param name="cargaMasiva"></param>
        /// <returns></returns>
        [HttpPost(RoutesPaths.MEDIC_MASIVE_LOAD)]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ServiceOutput>> CargaMasiva(string sucursal_id, [FromForm] CargaMasivaInput cargaMasiva)
        {
            try
            {
                ServiceOutput readedFile = MedicamentosService.ReadCSVFile(cargaMasiva);
                List<DTOPrecioFarmacia> dtoPreciosFarmacia =
                    JsonSerializer.Deserialize<List<DTOPrecioFarmacia>>(readedFile.Data);
                if (readedFile.Error) return BadRequest(readedFile.Message);

                string uid = "0E6AB68E-E60E-4EBA-80AB-F7EF84AB350B";//GetUserId();

                Person person = Context.People.Where(p => p.UserId == uid).FirstOrDefault();

                List<DTOEstadoImportacion> estadosImportacion = new();

                int wrongImport = 0;
                int successImport = 0;

                foreach (var item in dtoPreciosFarmacia)
                {
                    EprecioFarmacium precioFarmacia = Context.EprecioFarmacia.FirstOrDefault(p =>
                    p.CodTfc == item.CodigoTFC && p.CodFarmacia == sucursal_id);
                    Emedicamento medicamento = await Context.Emedicamentos.FindAsync(item.CodigoTFC);

                    if (precioFarmacia == null)
                    {
                        DTOEstadoImportacion estadoImportacion = new()
                        {
                            Success = false,
                            Message = CargaMasivaMessages.NOT_FOUND,
                            dtoPrecioFarmacia = item
                        };
                        estadosImportacion.Add(estadoImportacion);
                        wrongImport++;
                    }
                    else
                    {
                        precioFarmacia.Precio = Convert.ToDecimal(item.NuevoPrecio);
                        precioFarmacia.Stock = Convert.ToInt32(item.StockActual);
                        precioFarmacia.CodigoBarraFramacia = item.CodigoBarra;
                        precioFarmacia.DescripcionInternaFarmacia = item.DescripcionInterna;

                        // ESTABLECER EL ESTADO DE LA ENTIDAD COMO MODIFICADO
                        Context.Entry(precioFarmacia).State = EntityState.Modified;

                        try
                        {
                            await Context.SaveChangesAsync();
                            UpdateHistorialPrecio(precioFarmacia, medicamento, person);
                            DTOEstadoImportacion estadoImportacion = new()
                            {
                                Success = true,
                                Message = CargaMasivaMessages.CARGA_SUCCESSFUL,
                                dtoPrecioFarmacia = item
                            };
                            estadosImportacion.Add(estadoImportacion);
                            successImport++;
                        }
                        catch (Exception ex)
                        {
                            DTOEstadoImportacion estadoImportacion = new()
                            {
                                Success = false,
                                Message = ex.Message,
                                dtoPrecioFarmacia = item
                            };
                            estadosImportacion.Add(estadoImportacion);
                            wrongImport++;
                        }
                    }
                }

                //// Almacena la carga en base de datos
                //ECarga eCarga = CargaMasivaService.GenerateCargaMasiva(farmacia, precioFarmaciasDTO.Count, successItems, wrongItems, person);
                //_context.Ecarga.Add(eCarga);
                //await _context.SaveChangesAsync();


                //// Almacena los registros con error ASYNC
                //CargaMasivaService.SaveRegistrosFallidos(eCarga.Id, importacionFinalizacionDTOs, _context);
                //await _context.SaveChangesAsync();

                //return Ok(importacionFinalizacionDTOs);

                return Ok(estadosImportacion);
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
            // SE OBTIENE EL UID DE LA IDENTIDAD EN EL CONTEXTO HTTP
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
            string uid = identity.FindFirst("uid").Value;
            return uid;
        }

        private void UpdateHistorialPrecio(EprecioFarmacium eprecioFarmacium, Emedicamento emedicamento, Person person)
        {
            EhistorialPrecio ehistorialPrecio = new()
            {
                CodTfc = eprecioFarmacium.CodTfc,
                CodFarmacia = eprecioFarmacium.CodFarmacia,
                CodIsp = emedicamento.CodIsp,
                CodGtin = eprecioFarmacium.CodigoBarraFramacia,
                FechaActualizacion = eprecioFarmacium.FechaActualizacion,
                Precio = eprecioFarmacium.Precio,
                PrecioUnidad = eprecioFarmacium.Precio,
                Stock = eprecioFarmacium.Stock,
                StockMin = eprecioFarmacium.StockMin,
                StockMax = eprecioFarmacium.StockMax,
                TipoActualizacion = null,
                PersonId = person.Id
            };
            Context.EhistorialPrecios.Add(ehistorialPrecio);
        }
    }
}
