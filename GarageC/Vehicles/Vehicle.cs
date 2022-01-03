using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageC
{
    internal abstract class Vehicle : IVehicle
    {
        /*
         * ToDo
         * Because regNo could be used to uniquely identify vehicles,
         * The caller should use Garage.UsedRegNo() to prevent input of doublets.
         * I can think of doublets for swedish and foreign renNo(s)
         * The menu should contain an option to call Vehicle.SetSpecialRegNo()
         * • that resolve the problem to input "strange" reg numbers:
         *   1. add vehicle by using a temporary regNo
         *   2. call SetSpecialRegNo afterwards to change it.
         *   perhaps asking if one will input a special regNo instead?
         *   and set it directly.
         * • I'v also added the property foreignRegistrated
         */

        public virtual string ClassTitle { get => "VEHICLE"; }

        // Constructor set (more or less required) properties
        private string regNo; // unique vehicle key; can be changed to allow special regNo(s)
        private int lotNo;    // unique number; can be changed to allow moving between lots
        private DateTime startTime;  // required to determine parking cost; reset at paying
        private bool parked = false; // set to true by the constructer; ment for database saving of old data and/or testing

        // optional properties (have got default values and are menu-changeable)
        private string brand = string.Empty;
        private string model = string.Empty;
        private FuelType fuelType = FuelType.NotAvailable;
        private string color = string.Empty;
        private double lengthMeters = 0.0;
        private double heigthMeters = 0.0;
        private int weight_kg = 0;
        private string note = string.Empty;
        private bool foreignRegistered = false;

        internal string RegNo
        {
            get => regNo;
            set
            {
                if (!Tool.FixRegNo(ref value))
                    throw new ArgumentException("The caller should use FixRegNo() to prevent this exception!");

                regNo = value;
            }
        }
        public int LotNo { get => lotNo; set => lotNo = value; }
        internal DateTime StartTime { get => startTime; set => startTime = value; }
        internal bool ForeignRegistered { get => foreignRegistered; set => foreignRegistered = value; }
        internal bool Parked { get => parked; set => parked = value; }
        internal string Brand { get => brand; set => brand = Tool.TextToSentence(value); }
        internal string Model { get => model; set => model = Tool.TextToSentence(value); }
        internal FuelType FuelType { get => fuelType; set => fuelType = value; }
        internal string Color { get => color; set => color = Tool.TextToSentence(value); }
        internal int Weight_kg
        {
            get => weight_kg;
            set
            {
                if (!Tool.WithinRange(value, 0, Consts.MAX_GARAGE_VEHICLE_WEIGHT_kg))
                    throw new ArgumentException("The caller should check Garage.MAX_VEHICLE_WEIGHT_kg to prevent this exception!");
                else
                    weight_kg = value;
            }
        }
        internal double LengthMeters
        {
            get => lengthMeters;
            set
            {
                if (!Tool.WithinRange(value, 0, Consts.MAX_GARAGE_LOT_LENGHT_meters))
                    throw new ArgumentException("The caller should check Garage.MAX_LOT_LENGHT_meters to prevent this exception!");
                else
                    lengthMeters = value;
            }
        }
        internal double HeigthMeters
        {
            get => heigthMeters;
            set
            {
                if (!Tool.WithinRange(value, 0, Consts.MAX_GARAGE_CEILING_HEIGHT_meters))
                    throw new ArgumentException("The caller should check Garage.MAX_CEILING_HEIGHT_meters to prevent this exception!");
                else
                    heigthMeters = value;
            }
        }
        internal string Note { get => note; set => note = Tool.TextToSentence(value, false); }
        
        /// <summary>
        /// Resets startTime to Now.
        /// </summary>
        internal void SetStarttimeToNow() => startTime = DateTime.Now;

        /// <summary>
        /// Allow input of special regNo(s) like "Mr SuperMan".
        /// <para> • Property RegNo REQUIRES ABC123 format.</para> 
        /// <para> • Only Tools.TextToSentence(regNo, false) is used here.</para> 
        /// </summary>
        internal void SetSpecialRegNo(string regNo)
        {
            this.regNo = Tool.TextToSentence(regNo, false);
        }

        internal Vehicle(string regNo, int lotNo)
        {
            RegNo = regNo;
            LotNo = lotNo;

            parked = true;
            startTime = DateTime.Now;
        }

        /// <summary>
        /// Creates an array suitable for padding and colored outputs for ToText() method.
        /// </summary>
        /// <returns>an array with row-titles and nonBlank property-values to be displayed</returns>
        public virtual string TitleText() => ConsoleUI.ToTextTitle(this);

        /// <summary>
        /// Creates an array suitable for padding and colored outputs.
        /// <para> • Given subArray ends up between the more and the less important local fields.</para> 
        /// </summary>
        /// <param name="subArray">subclass specific titles and values OR ignored (default is null).</param>
        /// <returns>An array of title-value pairs</returns>
        public virtual string[] BodyToDisplay(string[] subArray = null, string parkingText = "")
        {
            List<string> list = new();

            // begin with these properties of this class
            list.Add("Parking Time: "); list.Add(Tool.ParkingTimeToString(this) + parkingText);

            list.Add("RegNo: "); list.Add(regNo);

            if (foreignRegistered) { list.Add("Foreign Registered: "); list.Add("Yes"); } // No is too obvious to display

            if (brand.Length > 0 && model.Length > 0) // Brand and model belongs together
            {
                list.Add("Brand: ");
                list.Add(brand + " " + model);
            }
            else
            {
                if (brand.Length > 0) { list.Add("Brand: "); list.Add(brand); }
                if (model.Length > 0) { list.Add("Model: "); list.Add(model); }
            }

            if (!parked) { list.Add("Parked: "); list.Add("No"); } // Yes is too obvious to display

            // insert subclass properties in the middle
            if (subArray != null)
                for (int i = 1; i < subArray.Length; i += 2)
                {
                    if (subArray[i].Length > 0)
                    {
                        list.Add(subArray[i - 1]);
                        list.Add(subArray[i]);
                    }
                }

            // trailing properties of this class
            if (!fuelType.Equals(FuelType.NotAvailable)) { list.Add("Fuel Type: "); list.Add(Enum.GetName(fuelType)); } // NotAvailable is equal to "blank"

            if (color.Length > 0) { list.Add("Color: "); list.Add(color); }

            if (lengthMeters > 0.0) { list.Add("Lenght: "); list.Add(Math.Round(lengthMeters, 2) + " meters"); }

            if (heigthMeters > 0.0) { list.Add("Height: "); list.Add(Math.Round(heigthMeters, 2) + " meters"); }

            if (weight_kg > 0) { list.Add("Weight: "); list.Add(weight_kg + " kilograms"); }

            if (note.Length > 0) { list.Add("Note: "); list.Add(note); }

            return list.ToArray();
        }

        /// <summary>
        /// Returns a nicely formated text of the "nonBlank" Vehicle properties.
        /// </summary>
        public string ToText()
        {
            string[] values = BodyToDisplay();
            int leftPadding = ConsoleUI.LongistTitle(values) + 1;

            StringBuilder s = new(TitleText());

            for (int i = 0; i < values.Length - 1; i += 2)
            {
                s.Append(values[i].PadLeft(leftPadding));
                s.AppendLine(values[i + 1]);
            }

            return s.ToString();
        }
    }
}
