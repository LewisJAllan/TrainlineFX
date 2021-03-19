/*
 * Trainline Copyright2021
 * Software Engineer Candidate: Lewis Allan
 * 
 * Description: Dictionary to map latest fx rates to for quick lookup
 * 19/03/2021 LA Initial version
 * 
 */

namespace TrainlineFX.Models
{
    using System.Collections.Generic;

    public class FXRates
    {
        public IDictionary<string, string> Rates { get; set; }
    }
}
