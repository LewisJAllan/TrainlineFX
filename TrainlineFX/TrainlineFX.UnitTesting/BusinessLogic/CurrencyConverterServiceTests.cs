namespace TrainlineFX.UnitTesting.BusinessLogic
{
    using Moq;
    using NUnit.Framework;
    using System.Collections.Generic;
    using TrainlineFX.BusinessLogic;
    using TrainlineFX.BusinessLogic.Interfaces;
    using TrainlineFX.Models;

    [TestFixture]
    public class CurrencyConverterServiceTests
    {
        private ICurrencyConverterService currencyConverter;
        private Mock<IExchangeRates> exchangeRates;
        private FXRates rates;
        
        [SetUp]
        public void Setup()
        {
            this.exchangeRates = new Mock<IExchangeRates>();
            this.currencyConverter = new CurrencyConverterService(this.exchangeRates.Object);

            this.rates = new FXRates();
            this.rates.Rates = new Dictionary<string, string>();
            this.rates.Rates.Add("GBP", "1.0");
            this.rates.Rates.Add("EUR", "1.16");
            this.rates.Rates.Add("MXN", "28.66");
        }

        [Test]
        public void WhenCallingCurrencyConverterWithValidParametersReturnSuccessful()
        {
            var request = new RequestFX()
            {
                Amount = "1.00",
                SourceCurrency = "GBP",
                TargetCurrency = "EUR"
            };

            this.exchangeRates.Setup(e => e.RetrieveLatestJsonRates(It.IsAny<string>())).ReturnsAsync(this.rates);
            var responseFx = new ResponseFX() { Amount = 1.16, TargetCurrency = "EUR" };

            var result = this.currencyConverter.ConvertCurrency(request);

            Assert.IsNotNull(result);
            Assert.AreEqual(responseFx.Amount, result.Amount);
            Assert.AreEqual(responseFx.TargetCurrency, result.TargetCurrency);
        }

        [Test]
        [TestCase("1.00","GBP","")]
        [TestCase("1.00", "", "")]
        [TestCase("", "", "")]
        public void WhenCallingCurrencyConverterWithInvalidParametersReturnNull(string amount, string source, string target)
        {
            var request = new RequestFX()
            {
                Amount = amount,
                SourceCurrency = source,
                TargetCurrency = target
            };

            var result = this.currencyConverter.ConvertCurrency(request);

            Assert.IsNull(result);
        }

        [Test]
        public void WhenCallingCurrencyConverterWithUnfoundTargetCurrencyReturnNull()
        {
            var request = new RequestFX()
            {
                Amount = "1.00",
                SourceCurrency = "GBP",
                TargetCurrency = "USD"
            };

            this.exchangeRates.Setup(e => e.RetrieveLatestJsonRates(It.IsAny<string>())).ReturnsAsync(this.rates);

            var result = this.currencyConverter.ConvertCurrency(request);

            Assert.IsNull(result);
        }
    }
}
