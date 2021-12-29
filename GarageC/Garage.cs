using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageC
{
    internal class Garage
    {
        /* 
         * ToDo1
         *  IMPLEMENTED LATER for the very first main-menu
         *  Requires saving the created garages in a file or database.
         *  (amountGarages could be used as a flag as follows):
         * 
         *    MyGarages
         *    1. Open Garage ( check amountGarages > 0 )
         *    2. Create Garage
         *    3. Delete Garage  ( check amountGarages > 0 )
         *    4. Quit
         *    5. • Create Test-garage ( temporary test option )
         *   
         * ToDo2
         *  AutoSave of previously parked vehicles for later reference
         *  Requires saving into file(s) or database.
         *  
         * ToDo3
         *  [auto]deletion of previously saved data for instance by date
         */
        private static uint amountGarages; // increased by the constructor

        // ToDo
        // • used for simple menu testing; the final version should use Garage<T> and an Array
        List<Vehicle> vehicles = new();

        public const string CODE_VERSION_ID = "1.00N"; // N stands for Nordic 

        // default constants based on the building
        // should be used for validation of code
        public const double MAX_CEILING_HEIGHT_meters = 7d;
        public const double MAX_LOT_LENGHT_meters = 12d;
        public const double MAX_VEHICLE_WEIGHT_kg = 25000;
        public const int MAX_AMOUNT_PARKING_LOTS = 40;

        // required properties
        private string name;
        private int amountParkingLots = MAX_AMOUNT_PARKING_LOTS;

        // ToDo: to keep the test menu size down, these values are excluded
        private string description = "";
        private string streetAddress = "";
        private string zipCode = "";
        private string city = "";
        private string country = "";

        public string Name { get => name; set => name = Tools.TextToSentence(value); }
        public string Description { get => description; set => description = Tools.TextToSentence(value); }
        public string StreetAddress { get => streetAddress; set => streetAddress = Tools.TextToSentence(value); }
        public string ZipCode { get => zipCode; set => zipCode = Tools.TextToSentence(value); }
        public string City { get => city; set => city = Tools.TextToSentence(value); }
        public string Country { get => country; set => country = Tools.TextToSentence(value); }

        // ToDo used for testing; optional parameters except the name
        public Garage(string name, string streeetAddress, string zipCode, string city, string country)
        {
            this.name = name;
            this.streetAddress = streeetAddress;
            this.zipCode = zipCode;
            this.city = city;
            this.country = country;
            amountGarages++;
        }

        public Garage(string name, int amountParkingLots)
        {
            if (string.IsNullOrWhiteSpace(name))
                this.name = "#NAME_MISSING!";
            else
                this.name = Tools.TextToSentence(name);

            if (!Tools.WithinRange(amountParkingLots, 1, MAX_AMOUNT_PARKING_LOTS))
                // the default of MAX_AMOUNT_PARKING_LOTS is kept
                throw new ArgumentException("The caller should check Garage.MAX_AMOUNT_PARKING_LOTS to prevent this exception!");
            else
                this.amountParkingLots = amountParkingLots;

            amountGarages++;
        }

        public bool RegNoExist(string regNo)
        {
            foreach(Vehicle v in vehicles)
            {
                if (string.Equals(regNo, v.RegNo, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        public string ToText()
        {
            StringBuilder s = new();
            s.AppendLine($"GARAGE {name}");
            s.AppendLine($"Number of Parking Lots: {amountParkingLots} pcs.");

            //if (model.Length > 0)
            //    s.AppendLine($"Brand: {brand} {model}");
            //else
            //    s.AppendLine($"Brand: {brand}");

            //s.AppendLine($"Parked: {parked}");

            //if (!fuelType.Equals(FuelType.NotAvailable)) s.AppendLine($"Fuel Type: {fuelType}");

            //if (color.Length > 0) s.AppendLine($"Color: {color}");
            //if (lengthMeters > 0) s.AppendLine($"Lenght: {Math.Round(lengthMeters, 2)} meters");
            //if (heigthMeters > 0) s.AppendLine($"Height: {Math.Round(heigthMeters, 2)} meters");
            //if (weight_kg > 0) s.AppendLine($"Weight: {weight_kg} kilograms");

            //if (note.Length > 0) s.AppendLine($"Note: {note}");

            //this.name = name;
            //this.streetAddress = streeetAddress;
            //this.zipCode = zipCode;
            //this.city = city;
            //this.country = country;

            return s.ToString();
        }

    }
}
