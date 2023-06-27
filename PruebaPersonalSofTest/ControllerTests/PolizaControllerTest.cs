using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using PruebaPersonalSoft.Controllers;
using PruebaPersonalSoft.Models;
using PruebaPersonalSoft.Repositories.Polizas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaPersonalSofTest.ControllerTests
{
    [TestFixture]
    public class PolizaControllerTest
    {
        private PolizaController _polizaController;
       
        [SetUp]
        public void Setup()
        {
;
            var configuration = new ConfigurationBuilder()
         .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
         .Build();

            _polizaController = new PolizaController(configuration);

        }

        [Test]
        public async Task GetPolizaDetails_PolizaExists_ReturnsOk()
        {
            // Arrange
            var placaVehiculo = "ASD-GHJ";
            var numPoliza = "123456";

            // Act
            var result = await _polizaController.GetPolizaDetails(placaVehiculo, numPoliza);
       
            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result is OkObjectResult);
            var okResult = (OkObjectResult)result;
            var success = (bool)okResult.Value.GetType().GetProperty("success").GetValue(okResult.Value);
            var poliza = (Poliza)okResult.Value.GetType().GetProperty("result").GetValue(okResult.Value);

            Assert.IsTrue(success is true);
            Assert.IsNotNull(poliza);
            Assert.AreEqual(200, okResult.StatusCode);

        }

        [Test]
        public async Task GetAllPoliza_ReturnsOk()
        {
            // Arrange

            // Act
            var result = await _polizaController.GetAllPoliza();


            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result is OkObjectResult);
            var okResult = (OkObjectResult)result;
            var success = (bool)okResult.Value.GetType().GetProperty("success").GetValue(okResult.Value);
            var polizas = (List<Poliza>)okResult.Value.GetType().GetProperty("result").GetValue(okResult.Value);

            Assert.IsTrue(okResult.Value is dynamic);
            var actualResult = (dynamic)okResult.Value;
            Assert.IsTrue(success);
            Assert.IsNotNull(polizas);
            Assert.AreEqual(200, okResult.StatusCode);
        }
    }
}
