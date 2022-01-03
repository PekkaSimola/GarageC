using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageC
{
    internal class Bus : Vehicle, IVehicle
    {
        public override string ClassTitle { get => "BUS"; }

        private int numberOfSeats = 0;
        private bool isDoubleDecker = false;
        private bool isArticulated = false; // Ledad buss in Swedish: Length ~18 meters or 59 feet
        internal bool IsDoubleDecker { get => isDoubleDecker; set => isDoubleDecker = value; }
        internal bool IsArticulated { get => isArticulated; set => isArticulated = value; }
        internal int NumberOfSeats
        {
            get => numberOfSeats;
            set
            {
                if (!Tool.WithinRange(value, 0, Consts.MAX_BUS_SEATS))
                    throw new ArgumentException("The caller should check Bus.MAX_AMOUNT_SEATS to prevent this exception!");
                else
                    numberOfSeats = value;
            }
        }

        internal Bus(string regNo, int lotNo) : base(regNo, lotNo) { }

        public override string TitleText() => ConsoleUI.ToTextTitle(this);

        /// <summary>
        /// Creates an array suitable for padding and colored outputs.
        /// </summary>
        /// <param name="subArray">ignored here; created locally</param>
        /// <returns>>an array of titles and values</returns>
        public override string[] BodyToDisplay(string[] ignore1 = null, string ignore2 = "")
        {
            List<string> list = new();
            if (isDoubleDecker)
            {
                list.Add("Double Decker: ");
                list.Add("Yes");
            }
            if (isArticulated)
            {
                list.Add("Articulated Bus: ");
                list.Add("Yes");
            }
            if (numberOfSeats > 0)
            {
                list.Add("Amount Seats: ");
                list.Add($"{numberOfSeats}");
            }

            // puts together a mix of base and local properties in a wished order
            return base.BodyToDisplay(list.ToArray(), "(" + Tool.ParkingCost(this) + " " + Consts.CURRENCY_SIGN + ")");
        }
    }
}
