using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageC
{
    internal class Boat : Vehicle, IVehicle 
    {
        public override string ClassTitle { get => "BOAT"; }

        private BoatType boatType = BoatType.NotAvailable;
        internal BoatType BoatType { get => boatType; set => boatType = value; }

        internal Boat(string regNo, int lotNo) : base(regNo, lotNo) { }

        public override string TitleText() => ConsoleUI.ToTextTitle(this);

        /// <summary>
        /// Creates an array suitable for padding and colored outputs.
        /// </summary>
        /// <param name="subArray">ignored here; created locally</param>
        /// <returns>>an array of titles and values</returns>
        public override string[] BodyToDisplay(string[] ignore1 = null, string ignore2 = "")
        {
            List<string> list = new();
            if (!boatType.Equals(BoatType.NotAvailable))
            {
                list.Add("Boat Type: ");
                list.Add($"{boatType}");
            }

            // puts together a mix of base and local properties in a wished order
            return base.BodyToDisplay(list.ToArray(), "(" + Tool.ParkingCost(this) + " " + Consts.CURRENCY_SIGN + ")");
        }
    }
}
