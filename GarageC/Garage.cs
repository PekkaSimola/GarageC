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
         * https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/recommended-tags
         * # or hash sign (even number sign or Pound sign)
         * () Parentheses
         * [] Brackets or square brackets
         * {} Braces or curly brackets.
         * 
         * Use <para> ... </para> for extra lines within <summary> blocks
         * REMARK: You MUST put a SPACE after the last >
         * 
         * These codes replace reserved chars within text in <summary> blocks
         * NOTE: Type EXACTLY as given in the first column (&...;)
         * &lt; 	< 	less than
         * &gt; 	> 	greater than
         * &amp;	& 	ampersand 
         * &apos;	' 	apostrophe
         * &quot;	" 	quotation mark
         * 
         * Garage<T> : IEnumerable<T> where T : Vechicle
         * T[] vehicles
         * 
         * 
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

        public string ClassTitle { get => "GARAGE"; }

        // required properties
        private string name;
        private int amountParkingLots = Consts.MAX_AMOUNT_PARKING_LOTS;

        // ToDo: to keep the test menu size down, these values are excluded in the test menu
        private string description = string.Empty;
        private string streetAddress = string.Empty;
        private string zipcode = string.Empty;
        private string city = string.Empty;
        private string country = string.Empty;

        public string Name { get => name; set => name = Tool.TextToSentence(value); }
        public string Description { get => description; set => description = Tool.TextToSentence(value); }
        public string StreetAddress { get => streetAddress; set => streetAddress = Tool.TextToSentence(value); }
        public string Zipcode { get => zipcode; set => zipcode = Tool.TextToSentence(value); }
        public string City { get => city; set => city = Tool.TextToSentence(value); }
        public string Country { get => country; set => country = Tool.TextToSentence(value); }

        // ToDo  Remove late: Used for testing; sets all the optional parameters
        public Garage(string name, string streetAddress, string zipcode, string city, string country)
        {
            this.name = name;
            this.streetAddress = streetAddress;
            this.zipcode = zipcode;
            this.city = city;
            this.country = country;
            amountGarages++;
        }

        public Garage(string name, int amountParkingLots)
        {
            if (string.IsNullOrWhiteSpace(name))
                this.name = "#NAME_MISSING!";
            else
                this.name = Tool.TextToSentence(name);

            if (!Tool.WithinRange(amountParkingLots, 1, Consts.MAX_AMOUNT_PARKING_LOTS))
                // the default of MAX_AMOUNT_PARKING_LOTS is kept
                throw new ArgumentException("The caller should check Garage.MAX_AMOUNT_PARKING_LOTS to prevent this exception!");
            else
                this.amountParkingLots = amountParkingLots;

            amountGarages++;
        }

        /// <summary>
        /// determines if regNo is used by any parked vehicle.
        /// <para> • intended for the Menu-system to forbid/warn regNo-doublets.</para> 
        /// </summary>
        /// <param name="regNo"></param>
        public bool UsedRegNo(string regNo)
        {
            foreach (Vehicle v in vehicles)
            {
                if (string.Equals(regNo, v.RegNo, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
        
        /// <summary>
        /// returns a nicely formated string of all the Garage properties.
        /// </summary>
        public string ToText()
        {
            StringBuilder s = new(ConsoleUI.ToTextTitle($"GARAGE : {name}"));

            s.AppendLine($"Number of Parking Lots: {amountParkingLots} lots");
            if (description.Length > 0) s.AppendLine($"Description: {description}");

            int[] len = { streetAddress.Length, zipcode.Length, city.Length, country.Length };
            if (len.Sum() > 0)
            {
                s.AppendLine("\nPOSTAL ADDRESS:");
                if (len[0] > 0) s.AppendLine($"Street: {streetAddress}");
                if (len[1] > 0 && len[2] > 0)
                    s.AppendLine($"Place: {zipcode} {city}");
                else
                {
                    if (len[1] > 0) s.AppendLine($"Zipcode: {zipcode}");
                    if (len[2] > 0) s.AppendLine($"City: {city}");
                }
                if (len[3] > 0) s.AppendLine($"Country: {country}");
            }

            s.AppendLine("\nGENERAL VEHICLE LIMITATIONS:");
            s.AppendLine($"Max Vehicle Lenght: {Math.Round(Consts.MAX_GARAGE_LOT_LENGHT_meters, 2)} meters");
            s.AppendLine($"Max Vehicle Height: {Math.Round(Consts.MAX_GARAGE_CEILING_HEIGHT_meters, 2)} meters");
            s.AppendLine($"Max Vehicle Weight: {Math.Round(Consts.MAX_GARAGE_VEHICLE_WEIGHT_kg, 2)} kilograms");

            return s.ToString();
        }
    }
}
