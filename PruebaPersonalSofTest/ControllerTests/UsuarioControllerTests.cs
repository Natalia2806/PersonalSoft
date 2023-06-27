using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PruebaPersonalSoft.Controllers;
using PruebaPersonalSoft.Models;


namespace PruebaPersonalSofTest.ControllerTests
{
    [TestFixture]
    public class UsuarioControllerTests
    {
        private UsuarioController _usuarioController;

        [SetUp]
        public void Setup()
        {

            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            _usuarioController = new UsuarioController(configuration);
        }

        [Test]
        public async Task IniciarSesion_ValidCredentials_ReturnsOk()
        {
            // Arrange
            var usuarioData = new UsuarioRequest
            {
                Correo = "tuirannatalia@gmail.com",
                Password = "1234"
            };

            // Act
            var result = await _usuarioController.IniciarSesion(usuarioData);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result is OkObjectResult);
            var okResult = (OkObjectResult)result;
            var token = okResult.Value.ToString();
            Assert.IsFalse(string.IsNullOrEmpty(token));
        
        }
    }
}
