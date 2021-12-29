using System;
using System.Text;

namespace GarageC
{
    internal class Motorcycle : Vehicle
    {
        private int cylinderVolume = 0;
        public int CylinderVolume
        {
            get => cylinderVolume;
            set
            {
                if (!Tools.WithinRange(value, 50, 2000))
                    throw new ArgumentException("Give a cylinder volume between 50 and 2000 cubic!");
                else
                    cylinderVolume = value;
            }
        }
        public Motorcycle(string regNo, string brand) : base(regNo, brand) { }
        /// <summary>
        /// returns a nicely formated string of all the Motorcycle properties.
        /// </summary>
        public override string ToText()
        {
            StringBuilder local = new();
            if (cylinderVolume > 0) local.AppendLine($"Cylinder Volume: {cylinderVolume }");
            return ConcatToText($"MOTORCYCLE: {RegNo}", local.ToString());
        }
    }
}
