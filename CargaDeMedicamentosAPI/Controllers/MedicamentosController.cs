using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CargaDeMedicamentosAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CargaDeMedicamentosAPI.Controllers
{
    [Route("api/medicamentos")]
    [ApiController]
    public class MedicamentosController : ControllerBase
    {
        [HttpGet()]
        public ActionResult CargaIndividual()
        {
            return Ok();
        }

        [HttpPost()]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult CargaMavisa([FromQuery] string idSucursal, [FromBody] CargaMasiva cargaMasiva)
        {
            return Ok(idSucursal);
        }
    }
}
