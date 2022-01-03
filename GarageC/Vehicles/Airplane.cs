using System;
using System.Collections.Generic;
using System.Text;

namespace GarageC
{
    internal class Airplane : Vehicle, IVehicle 
    {
        public override string ClassTitle { get => "AIRPLANE"; }

        private AirplaneType airplaneType = AirplaneType.NotAvailable;
        private int numberOfEngines = 0;

        internal AirplaneType AirplaneType { get => airplaneType; set => airplaneType = value; }
        public int NumberOfEngines
        {
            get => numberOfEngines;
            set
            {
                if (!Tool.WithinRange(value, 0))
                    throw new ArgumentException("The caller should check that value is not negative to prevent this exception!");
                else
                    numberOfEngines = value;
            }
        }

        internal Airplane(string regNo, int lotNo) : base(regNo, lotNo) { }

        public override string TitleText() => ConsoleUI.ToTextTitle(this);

        /// <summary>
        /// Creates an array suitable for padding and colored outputs.
        /// </summary>
        /// <param name="subArray">ignored here; created locally</param>
        /// <returns>>an array of titles and values</returns>
        public override string[] BodyToDisplay(string[] ignore1 = null, string ignore2 = "")
        {
            List<string> list = new();
            if (!airplaneType.Equals(AirplaneType.NotAvailable))
            {
                list.Add("Airplane Type: ");
                list.Add($"{airplaneType}");
            }
            if (numberOfEngines > 0)
            {
                list.Add("Number of Engines: ");
                list.Add($"{numberOfEngines}");
            }

            // puts together a mix of base and local properties in a wished order
            return base.BodyToDisplay(list.ToArray(), "(" + Tool.ParkingCost(this) + " " + Consts.CURRENCY_SIGN + ")");
        }
    }
}
