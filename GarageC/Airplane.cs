using System;
using System.Text;

namespace GarageC
{
    internal class Airplane : Vehicle
    {
        private AirplaneType airplaneType = AirplaneType.NotAvailable;
        private int numberOfEngines = 0;
        internal AirplaneType AirplaneType { get => airplaneType; set => airplaneType = value; }
        public int NumberOfEngines
        {
            get => numberOfEngines;
            set
            {
                if (!Tools.WithinRange(value, 0))
                    throw new ArgumentException("The caller should check that value is not negative to prevent this exception!");
                else
                    numberOfEngines = value;
            }
        }
        public Airplane(string regNo, string brand) : base(regNo, brand) { }
        /// <summary>
        /// returns a nicely formated string of all the Airplane properties.
        /// </summary>
        public override string ToText()
        {
            StringBuilder local = new();
            if (!airplaneType.Equals(AirplaneType.NotAvailable)) local.AppendLine($"Airplane Type: {airplaneType}");
            if (numberOfEngines>0) local.AppendLine($"Number of Engines: {numberOfEngines}");
            return ConcatToText($"AIRPLANE: {RegNo}", local.ToString());
        }
    }
}
