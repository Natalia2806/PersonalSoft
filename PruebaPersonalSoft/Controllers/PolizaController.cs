using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using PruebaPersonalSoft.Models;
using PruebaPersonalSoft.Repositories.Polizas;
using System.IdentityModel.Tokens.Jwt;

namespace PruebaPersonalSoft.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
  
    public class PolizaController : Controller
    {
        private IPolizaCollection polizaCollection = new PolizaCollection();

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllPoliza()
        {
            try
            {
                return Ok(
                 new
                 {
                     success = true,
                     message = "Polizas encontrada exitosamente",
                     result = await polizaCollection.GetAllPoliza()
                 });
            }
            catch (Exception e)
            {
                return BadRequest($"Ha ocurrido un error: {e.Message}");
            }
         
        }
                                                                                                                              
        [HttpGet("{placaVehiculo?}/{numPoliza?}")]
        [Authorize]
        public async Task<IActionResult> GetPolizaDetails(string placaVehiculo,  string numPoliza)
        {
            try
            {
                return Ok(
                    new
                    {
                        success = true,
                        message = "Poliza encontrada exitosamente",
                        result = await polizaCollection.GetPolizaByParameters(placaVehiculo, numPoliza)
                    });
            }
            catch (Exception e)
            {
                return BadRequest($"Ha ocurrido un error: {e.Message}");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePoliza([FromBody] Poliza poliza)
        {
            try
            {
                DateTime fechaActual = DateTime.UtcNow;

                if (fechaActual >= poliza.FechaInicioVigencia && fechaActual <= poliza.FechaFinVigencia)
                {
                    await polizaCollection.InsertPoliza(poliza);
                    return Created("Poliza creada corrrectamente", true);
                }
                else
                {
                     return BadRequest(new
                    {
                        success = false,
                        message = "Lo sentimos, la poliza no se encuentra vigente",
                        result = ""
                    });
                }

            }
            catch (Exception e)
            {
                return BadRequest($"Ha ocurrido un error: {e.Message}");
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdatePoliza([FromBody] Poliza poliza, string id)
        {
            try
            {
                if (poliza == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Por favor ingrese la informacion de la poliza que desee actualizar",
                        result = ""
                    });
                }

                poliza.Id = new ObjectId(id);
                await polizaCollection.UpdatePoliza(poliza);

                return Created("Poliza actualizada correctamente", true);
            }
            catch(Exception e)
            {
                return BadRequest($"Ha ocurrido un error: {e.Message}");
            }
            

        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePoliza(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Debe ingresar el id de la poliza",
                        result = ""
                    });
                }

                await polizaCollection.DeletePoliza(id);
                return Ok(
                    new
                    {
                        success = true,
                        message = "Poliza eliminada correctamente",
                    });
            }
            catch (Exception e)
            {
                return BadRequest($"Ha ocurrido un error: {e.Message}");
            }
           
        }


    }
}
