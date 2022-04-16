using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huboo {
    internal class VehicleVO {

        public string Make { get; }
        public string Model { get; }
        public string Colour { get; }
        public DateTime ExpiryDate { get; }
        public uint FailedMotsTotal { get; }


        public bool ExpiryDateValid { get { return !ExpiryDate.Equals(DateTime.MinValue); } }

        /**
            Make 
            Model 
            Colour 
            MOT expiry date 
            The number of failed MOTs in the vehicle’s history 
        */
        public VehicleVO(string make, string model, string colour, DateTime expiryDate, uint failedMotsTotal) {
            Make            = make;
            Model           = model;
            Colour          = colour;
            ExpiryDate      = expiryDate;
            FailedMotsTotal = failedMotsTotal;
        }


    }
}
