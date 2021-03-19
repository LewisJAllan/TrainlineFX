/*
 * Trainline Copyright2021
 * Software Engineer Candidate: Lewis Allan
 * 
 * Description: Interface for CurrencyConverterService
 * 19/03/2021 LA Initial version
 * 
 */

namespace TrainlineFX.BusinessLogic.Interfaces
{
    using TrainlineFX.Models;

    public interface ICurrencyConverterService
    {
        ResponseFX ConvertCurrency(RequestFX requestFx);
    }
}
