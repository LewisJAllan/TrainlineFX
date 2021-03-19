/*
 * Trainline Copyright2021
 * Software Engineer Candidate: Lewis Allan
 * 
 * Description: Model for the targetted currency and amount
 * 19/03/2021 LA Initial version
 * 
 */

namespace TrainlineFX.Models
{
    public class ResponseFX
    {
        public string TargetCurrency { get; set; }

        public double Amount { get; set; }
    }
}
