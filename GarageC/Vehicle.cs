using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageC
{
    internal abstract class Vehicle : IVehicle
    {
        public const string CODE_VERSION_ID = "1.00N"; // N stands for Nordic      

        // required properties
        private string regNo = "";
        private string brand = "";

        // optional properties to default
        private string model = "";
        private bool parked = false; // set to true by the constructer
        private FuelType fuelType = FuelType.NotAvailable;
        private string color = "";
        private double lengthMeters = 0.0;
        private double heigthMeters = 0.0;
        private int weight_kg = 0;
        private string note = "";

        public string RegNo
        {
            get => regNo;
            set
            {
                if (!Tools.FixRegNo(ref value))
                    throw new ArgumentException("The caller should use FixRegNo() to prevent this exception!");

                regNo = value;
            }
        }
        public string Brand { get => brand; set => brand = Tools.TextToSentence(value); }
        public string Model { get => model; set => model = Tools.TextToSentence(value); }
        public bool Parked { get => parked; set => parked = value; }
        internal FuelType FuelType { get => fuelType; set => fuelType = value; }
        public string Color { get => color; set => color = Tools.TextToSentence(value); }
        public int Weight_kg
        {
            get => weight_kg;
            set
            {
                if (!Tools.WithinRange(value, 0, Garage.MAX_VEHICLE_WEIGHT_kg))
                    throw new ArgumentException("The caller should check Garage.MAX_VEHICLE_WEIGHT_kg to prevent this exception!");
                else
                    weight_kg = value;
            }
        }
        public double LengthMeters
        {
            get => lengthMeters;
            set
            {
                if (!Tools.WithinRange(value, 0, Garage.MAX_LOT_LENGHT_meters))
                    throw new ArgumentException("The caller should check Garage.MAX_LOT_LENGHT_meters to prevent this exception!");
                else
                    lengthMeters = value;
            }
        }
        public double HeigthMeters
        {
            get => heigthMeters;
            set
            {
                if (!Tools.WithinRange(value, 0, Garage.MAX_CEILING_HEIGHT_meters))
                    throw new ArgumentException("The caller should check Garage.MAX_CEILING_HEIGHT_meters to prevent this exception!");
                else
                    heigthMeters = value;
            }
        }
        public string Note { get => note; set => note = Tools.TextToSentence(value, false); }

        /// <summary>
        /// Allow input of special regNo(s) like "Mr SuperMan".
        /// <para> • Set of RegNo property REQUIRES ABC123 format.</para> 
        /// <para> • Only Tools.TextToSentence(regNo, false) is used here.</para> 
        /// </summary>
        /// <param name="regNo"></param>
        public void SetSpecialRegNo(string regNo)
        {
            this.regNo = Tools.TextToSentence(regNo, false);
        }
        public Vehicle(string regNo, string brand)
        {
            RegNo = regNo;
            Brand = brand;
            parked = true;
        }
        /// <summary>
        /// returns a text as: titleText + required properties + subText + optional properties
        /// <para> • used by ToText() method of this.class and *child* classes.</para> 
        /// </summary>
        /// <param name="titleText"></param>
        /// <param name="subText"></param>
        public string ConcatToText(string titleText, string subText)
        {
            // given title
            StringBuilder s = new("\n");
            s.AppendLine(titleText);
            int len = s.Length - 3;
            s.Insert(0, "_", len);
            s.AppendLine(new string('¯', len));

            // required common properties
            s.AppendLine($"RegNo: {regNo}");
            if (model.Length > 0)
                s.AppendLine($"Brand: {brand} {model}");
            else
                s.AppendLine($"Brand: {brand}");
            s.AppendLine($"Parked: {parked}");

            // *child* specific properties
            if (subText.Length > 0) s.Append(subText);

            // optional common properties
            if (!fuelType.Equals(FuelType.NotAvailable)) s.AppendLine($"Fuel Type: {fuelType}");
            if (color.Length > 0) s.AppendLine($"Color: {color}");
            if (lengthMeters > 0) s.AppendLine($"Lenght: {Math.Round(lengthMeters, 2)} meters");
            if (heigthMeters > 0) s.AppendLine($"Height: {Math.Round(heigthMeters, 2)} meters");
            if (weight_kg > 0) s.AppendLine($"Weight: {weight_kg} kilograms");
            if (note.Length > 0) s.AppendLine($"Note: {note}");

            return s.ToString();
        }
        /// <summary>
        /// Returns a nicely formated text of all the Vehicle properties.
        /// <para> OBS! abstract class doesn't allow instanciating; a *child* class can call by base.ToText().</para> 
        /// </summary>
        public virtual string ToText() => ConcatToText($"VEHICLE: {RegNo}", "");
    }
}
