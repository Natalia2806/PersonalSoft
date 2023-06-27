using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using PruebaPersonalSoft.Models;

using PruebaPersonalSoft.Repositories.Usuarios;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace PruebaPersonalSoft.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private IUsuarioCollection usuarioCollection = new UsuarioCollection();
        public IConfiguration _configuration;

        public UsuarioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("api/login")]
        public async Task<IActionResult> IniciarSesion([FromBody] UsuarioRequest usuarioData)
        {
            try
            {
                string user = usuarioData.Correo.ToString();
                string password = usuarioData.Password.ToString();

                Usuario usuario = await usuarioCollection.IniciarSesion(user, password);

                if(usuario == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Credenciales incorrectas",
                        result = ""
                    });
                }

                var jwt = _configuration.GetSection("Jwt").Get<Jwt>();
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("nombre", usuario.Nombre),
                    new Claim("correo", usuario.Correo)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
                var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    jwt.Issuer,
                    jwt.Audience,
                    claims,
                    signingCredentials: singIn
                   );
               
                return Ok(
                    new { 
                        success = true,
                        message = "Inicio de sesion exitoso",
                        token = new JwtSecurityTokenHandler().WriteToken(token)
                    });
            }
            catch (Exception e)
            {
                return BadRequest($"Ha ocurrido un error: {e.Message}");
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateUsuario([FromBody] Usuario usuario)
        {
            try
            {
                    await usuarioCollection.InsertUsuario(usuario);
                    return Created("Usuario creado corrrectamente", true);
            }
            catch (Exception e)
            {
                return BadRequest($"Ha ocurrido un error: {e.Message}");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllUsuario()
        {
            try
            {
                return
                    Ok(
                    new
                    {
                        success = true,
                        message = "Usuarios encontrados",
                        result = await usuarioCollection.GetAllUsuario()
                    });
            }
            catch (Exception e)
            {
                return BadRequest($"Ha ocurrido un error: {e.Message}");
            }

        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUsuario([FromBody] Usuario usuario, string id)
        {
            try
            {
                if (usuario == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "No se encontro informacion del usuario",
                        result = ""
                    });
                }

                usuario.Id = new ObjectId(id);
                await usuarioCollection.UpdateUsuario(usuario);

                return Created("Usuario actualizado correctamente", true);
            }
            catch (Exception e)
            {
                return BadRequest($"Ha ocurrido un error: {e.Message}");
            }

        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUsuario(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Debe ingresar el id del usuario",
                        result = ""
                    });
                }

                await usuarioCollection.DeleteUsuario(id);
               return Ok(
                    new
                    {
                        success = true,
                        message = "Usuario eliminado correctamente",
                    });
            }
            catch (Exception e)
            {
                return BadRequest($"Ha ocurrido un error: {e.Message}");
            }

        }
    }
}
