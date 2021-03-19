/*
 * Trainline Copyright2021
 * Software Engineer Candidate: Lewis Allan
 * 
 * Description: Model for mapping the requested currency fx
 * 19/03/2021 LA Initial version
 * 
 */

namespace TrainlineFX.Models
{
    public class RequestFX
    {
        public string SourceCurrency { get; set; }

        public string TargetCurrency { get; set; }

        public string Amount { get; set; }
    }
}