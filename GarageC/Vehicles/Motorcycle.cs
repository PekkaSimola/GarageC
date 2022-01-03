using System;
using System.Collections.Generic;
using System.Text;

namespace GarageC
{
    internal class Motorcycle : Vehicle, IVehicle 
    {
        public override string ClassTitle { get => "MOTORCYCLE"; }

        private int cylinderVolume = 0;
        internal int CylinderVolume
        {
            get => cylinderVolume;
            set
            {
                if (!Tool.WithinRange(value, 50, 2000))
                    throw new ArgumentException("Give a cylinder volume between 50 and 2000 cubic!");
                else
                    cylinderVolume = value;
            }
        }

        public Motorcycle(string regNo, int lotNo) : base(regNo, lotNo) { }
        public override string TitleText() => ConsoleUI.ToTextTitle(this);

        /// <summary>
        /// Creates an array suitable for padding and colored outputs.
        /// </summary>
        /// <param name="subArray">ignored here; created locally by sub classes</param>
        /// <returns>>an array of titles and values</returns>
        public override string[] BodyToDisplay(string[] ignore1 = null, string ignore2 = "")
        {
            List<string> list = new();
            if (cylinderVolume > 0)
            {
                list.Add("Cylinder Volume: ");
                list.Add($"{cylinderVolume }");
            }

            // puts together a mix of base and local properties in a wished order
            return base.BodyToDisplay(list.ToArray(), "(" + Tool.ParkingCost(this) + " " + Consts.CURRENCY_SIGN + ")");
        }
    }
}
