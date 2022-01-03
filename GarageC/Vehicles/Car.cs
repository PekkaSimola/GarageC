using System;
using System.Collections.Generic;
using System.Text;

namespace GarageC
{
    internal class Car : Vehicle, IVehicle
    {
        public override string ClassTitle { get => "CAR"; }

        private CarType carType = CarType.NotAvailable;
        internal CarType CarType { get => carType; set => carType = value; }

        internal Car(string regNo, int lotNo) : base(regNo, lotNo) { }

        public override string TitleText() => ConsoleUI.ToTextTitle(this);

        /// <summary>
        /// Creates an array suitable for padding and colored outputs.
        /// </summary>
        /// <param name="subArray">ignored here; created locally</param>
        /// <returns>>an array of titles and values</returns>
        public override string[] BodyToDisplay(string[] ignore1 = null, string ignore2 = "")
        {
            List<string> list = new();
            if (!carType.Equals(CarType.NotAvailable))
            {
                list.Add("Car Type: ");
                list.Add($"{carType}");
            }

            // puts together a mix of base and local properties in a wished order
            return base.BodyToDisplay(list.ToArray(), "(" + Tool.ParkingCost(this) + " " + Consts.CURRENCY_SIGN + ")"); 
        }
    }
}
