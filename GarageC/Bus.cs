using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageC
{
    internal class Bus : Vehicle
    {
        public const int MAX_AMOUNT_SEATS = 100;

        private int numberOfSeats = 0;
        private bool isDoubleDecker = false;
        private bool isHeaded = false; // Swedish: Ledad buss
        public bool IsDoubleDecker { get => isDoubleDecker; set => isDoubleDecker = value; }
        public bool IsHeaded { get => isHeaded; set => isHeaded = value; }
        public int NumberOfSeats
        {
            get => numberOfSeats;
            set
            {
                if (!Tools.WithinRange(value, 0, MAX_AMOUNT_SEATS))
                    throw new ArgumentException("The caller should check Bus.MAX_AMOUNT_SEATS to prevent this exception!");
                else
                    numberOfSeats = value;
            }
        }
        public Bus(string regNo, string brand) : base(regNo, brand) { }
        /// <summary>
        /// returns a nicely formated string of all the Bus properties.
        /// </summary>
        public override string ToText()
        {
            StringBuilder local = new();
            if (isDoubleDecker) local.AppendLine("Double Decker: True");
            if (isHeaded) local.AppendLine("Headed Bus: True");
            if (numberOfSeats > 0) local.AppendLine($"Amount Seats: {numberOfSeats}");

            return ConcatToText($"BUS: {RegNo}", local.ToString());
        }
    }
}
