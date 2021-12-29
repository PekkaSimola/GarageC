using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageC
{
    internal class Boat : Vehicle
    {
        private BoatType boatType = BoatType.NotAvailable;
        internal BoatType BoatType { get => boatType; set => boatType = value; }
        public Boat(string regNo, string brand) : base(regNo, brand) { }
        /// <summary>
        /// returns a nicely formated string of all the Boat properties.
        /// </summary>
        public override string ToText()
        {
            StringBuilder local = new();
            if (!boatType.Equals(BoatType.NotAvailable)) local.AppendLine($"Boat Type: {boatType}");
            return ConcatToText($"BOAT: {RegNo}", local.ToString());
        }
    }
}
