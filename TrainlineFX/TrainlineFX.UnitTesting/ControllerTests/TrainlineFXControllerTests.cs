namespace TrainlineFX.UnitTesting.ControllerTests
{
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using NUnit.Framework;
    using System.Net;
    using TrainlineFX.BusinessLogic.Interfaces;
    using TrainlineFX.Controllers;
    using TrainlineFX.Models;

    [TestFixture]
    public class TrainlineFXControllerTests
    {
        private TrainlineFXController controller;
        private Mock<ICurrencyConverterService> converter;
        private ResponseFX response;

        [SetUp]
        public void Setup()
        {
            this.converter = new Mock<ICurrencyConverterService>();
            this.controller = new TrainlineFXController(this.converter.Object);
            this.response = new ResponseFX() { Amount = 2.5, TargetCurrency = "EUR" };
        }

        [Test]
        public void WhenCallingGetWithValidRequestReturnOK()
        {
            var request = new RequestFX()
            {
                Amount = "1.20",
                SourceCurrency = "GBP",
                TargetCurrency = "EUR"
            };

            this.converter.Setup(c => c.ConvertCurrency(request)).Returns(this.response);

            var result = this.controller.Get(request) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(this.response, result.Value);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void WhenCallingGetWithInvalidRequestReturnBadRequest()
        {
            var request = new RequestFX()
            {
                Amount = "1.20",
                SourceCurrency = "GBP",
                TargetCurrency = "EUR"
            };

            this.converter.Setup(c => c.ConvertCurrency(request)).Returns((ResponseFX)null);

            var result = this.controller.Get(request) as BadRequestResult;

            Assert.AreEqual(400, result.StatusCode);
        }
    }
}
