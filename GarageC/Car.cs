using System;
using System.Text;

namespace GarageC
{
    internal class Car : Vehicle
    {
        private CarType carType = CarType.NotAvailable;
        internal CarType CarType { get => carType; set => carType = value; }
        public Car(string regNo, string brand) : base(regNo, brand) { }
        /// <summary>
        /// returns a nicely formated string of all the Car properties.
        /// </summary>
        public override string ToText()
        {
            StringBuilder local = new();
            if (!carType.Equals(CarType.NotAvailable)) local.AppendLine($"Car Type: {carType}");
            return ConcatToText($"CAR: {RegNo}", local.ToString());
        }
    }
}
