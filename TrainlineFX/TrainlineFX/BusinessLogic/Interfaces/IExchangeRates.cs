/*
 * Trainline Copyright2021
 * Software Engineer Candidate: Lewis Allan
 * 
 * Description: Interface for Retrieving ExchangeRates
 * 19/03/2021 LA Initial version
 * 
 */

namespace TrainlineFX.BusinessLogic.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TrainlineFX.Models;

    public interface IExchangeRates
    {
        Task<FXRates> RetrieveLatestJsonRates(string sourceCurrency);
    }
}
