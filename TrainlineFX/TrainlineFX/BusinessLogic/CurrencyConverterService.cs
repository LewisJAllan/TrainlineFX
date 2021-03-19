/*
 * Trainline Copyright2021
 * Software Engineer Candidate: Lewis Allan
 * 
 * Description: Service class to call for fx rates and return fx rate
 * 19/03/2021 LA Initial version
 * 
 */

namespace TrainlineFX.BusinessLogic
{
    using System;
    using TrainlineFX.BusinessLogic.Interfaces;
    using TrainlineFX.Models;

    public class CurrencyConverterService : ICurrencyConverterService
    {
        private readonly IExchangeRates exchangeRates;

        public CurrencyConverterService(IExchangeRates exchangeRates)
        {
            this.exchangeRates = exchangeRates ?? throw new ArgumentNullException(nameof(exchangeRates));
        }

        public ResponseFX ConvertCurrency(RequestFX requestFx)
        {
            if (!this.ValidateRequest(requestFx))
            {
                return null;
            }

            var rates = this.exchangeRates.RetrieveLatestJsonRates(requestFx.SourceCurrency).Result.Rates;

            if (rates.ContainsKey(requestFx.TargetCurrency))
            {
                var convertedAmount = Convert.ToDouble(requestFx.Amount) * Convert.ToDouble(rates[requestFx.TargetCurrency]);
                return new ResponseFX() { TargetCurrency = requestFx.TargetCurrency, Amount = convertedAmount };
            }

            return null;
        }

        private bool ValidateRequest(RequestFX requestFx)
        {
            if (string.IsNullOrWhiteSpace(requestFx.Amount))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(requestFx.SourceCurrency))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(requestFx.TargetCurrency))
            {
                return false;
            }

            return true;
        }
    }
}
