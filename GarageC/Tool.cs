using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace GarageC
{
    internal static class Tool
    {
        public static double ParkingCost(Vehicle v)
        {

            double[] prices = Consts.ParkingPrice(v);
            if (prices.Length < 6) return -1.0;

            Console.WriteLine("Prices[1]: " + prices[1]);
            Console.WriteLine("Prices[0]: " + prices[0]);
            
            //double[] prices = new();

            //switch (v.ClassTitle)
            //{
            //    case "CAR":
            //        prices = Consts.CAR_Min_Hour_Day_7_30_90;
            //        break;
            //    case "BUS":
            //        prices = Consts.CAR_Min_Hour_Day_7_30_90;
            //        break;
            //    case "BOAT":
            //        prices = Consts.CAR_Min_Hour_Day_7_30_90;
            //        break;
            //    case "MOTORCYCLE":
            //        double[] prices = Consts.CAR_Min_Hour_Day_7_30_90;
            //        break;
            //    case "AIRPLANE":
            //        double[] prices = Consts.CAR_Min_Hour_Day_7_30_90;
            //        break;
            //}

            return 0;
        }

        /// <summary>
        /// Convert TimeSpan between Now and startTime to a formated text. Zero values are excluded.
        /// <para> NOTE • Always returns a single trailing space.</para> 
        /// </summary>
        /// <param name="v">current vehicle</param>
        /// <returns>A string: [x days ][y hours ][i minutes]</returns>
        public static string ParkingTimeToString(Vehicle v)
        {
            (int days, int hours, int minutes) = ParkingTime(v);

            //if (days + hours + minutes == 0)
            //    return "0 hours";

            StringBuilder s = new();
            if (days > 0)
                if (days == 1)
                    s.Append($"{days} day ");
                else
                    s.Append($"{days} days ");

            if (hours > 0)
                if (hours == 1)
                    s.Append($"{hours} hour ");
                else
                    s.Append($"{hours} hours ");

            if (minutes > 0)
                if (minutes == 1)
                    s.Append($"{minutes} minute ");
                else
                    s.Append($"{minutes} minutes ");

            return s.ToString();
        }

        /// <summary>
        /// Determines current parking time as days, hours and minutes.
        /// </summary>
        /// <param name="startTime">creation time of the vehicle instance</param>
        /// <returns>3-tuple(days, hours, minutes)</returns>
        public static (int, int, int) ParkingTime(DateTime startTime)
        {
            TimeSpan t = DateTime.Now.Subtract(startTime);
            //if (t.TotalMinutes < 1)
            //    return (0, 0, 1);
            //else
            return (t.Days, t.Hours + 4, t.Minutes + 42);
        }
        public static (int, int, int) ParkingTime(Vehicle v)
        {
            return ParkingTime(v.StartTime);
        }

        /// <summary>
        /// Returns true if d is within min and max.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public static bool WithinRange(double d, double min = double.MinValue, double max = double.MaxValue)
        {
            try { return d >= min && d <= max; }
            catch { return false; }
        }

        /// <summary>
        /// Returns true if i is within min and max.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public static bool WithinRange(int i, int min = int.MinValue, int max = int.MaxValue)
        {
            try { return i >= min && i <= max; }
            catch { return false; }
        }

        // Leave this note for future ref:
        // Found out that char.IsLetter and char.IsDigit can replace Regex
        // private static readonly Regex regexALFANUMERIC = new("^[a-zA-Z0-9ÅÄÖåäö]*$");
        // private static readonly Regex regexDIGIT = new("^[0-9]*$");
        // private static readonly Regex regexUPPER_CASE_LETTER = new("^[A-ZÅÄÖ]*$");
        // use example: if (!regexALFANUMERIC.IsMatch(regNo)) return false;

        /* ToDo
         * The current version doesn't automatically support special RegNo(s).
         * At least in Sweden, one could buy an unigue optional text as a regNo.
         * The menu should automatically ask if one would like to input a special regNo
         * Vehicle.SetSpecialRegNo() can be used.
         * • The foreignRegistered property added
         * An idea:
         *   If the regNo-text failes, the program then ask if one want to give a special regNo instead
         *   and then skip FixRegNo()
         *   (it's always possible to input a temporary regNo and then Vehicle.SetSpecialRegNo() to change it.)
         *   2. One should mayby also ask in case the specialRegNo is foreign, and autoset the foreignRegistered
         * A warning of an existing doublet should be shown; Use Garage.UsedRegNo()
         */

        /// <summary>
        /// Clean up the regNo by ref, and return true at success.
        /// <para> • requires alfanumeric characters as ABC123.</para> 
        /// <para> • white space characters are removed.</para> 
        /// <para> • amount characters must match the length-parameters.</para> 
        /// </summary>
        /// <param name="regNo"></param>
        /// <param name="letterLength"></param>
        /// <param name="digitLength"></param>
        public static bool FixRegNo(ref string regNo, int letterLength = 3, int digitLength = 3)
        {
            StringBuilder s = new();
            int counter = 0;
            int maxLength = letterLength + digitLength;

            foreach (char ch in regNo)
            {
                if (!char.IsWhiteSpace(ch))
                {
                    counter++;
                    if (counter > maxLength) return false;
                    if (counter <= letterLength)
                    {
                        if (!char.IsLetter(ch)) return false;
                    }
                    else if (!char.IsDigit(ch))
                        return false;

                    s.Append(ch);
                }
            }

            regNo = s.ToString().ToUpper();
            return true;
        }

        /// <summary>
        /// Removes white characters, and parse the rest via number.
        /// <para> • the number is validated to lay between min and max parameters.</para> 
        /// </summary>
        public static bool TryParseInterval(string? intString, out int number, int min = int.MinValue, int max = int.MaxValue)
        {
            string s = RemoveWhiteSpace(intString);
            if (!int.TryParse(s, out number)) return false;
            return WithinRange(number, min, max);
        }

        /// <summary>
        /// Removes white characters, and parse the rest via number.
        /// <para> • the number is validated within interval of min and max parameters.</para> 
        /// </summary>
        public static bool TryParseInterval(string? doubleString, out double number, double min = double.MinValue, double max = double.MaxValue)
        {
            string s = RemoveWhiteSpace(doubleString);
            if (!double.TryParse(s, out number)) return false;
            return WithinRange(number, min, max);
        }

        /// <summary>
        /// Return str without WhiteSpace-characters.
        /// </summary>
        /// <param name="str"></param>
        public static string RemoveWhiteSpace(string? str)
        {
            if (string.IsNullOrWhiteSpace(str)) return string.Empty;
            StringBuilder sb = new();
            foreach (char ch in str) if (!char.IsWhiteSpace(ch)) sb.Append(ch);
            return sb.ToString();
        }

        /// <summary>
        /// converts false to no and true to yes.
        ///<para>• startWithUpperCase == true returns Yes or No.</para> 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="startWithUpperCase"></param>
        public static string BoolToWord(bool value, bool startWithUpperCase = true)
        {
            if (value)
            {
                if (startWithUpperCase) return "Yes";
                return "yes";
            }
            if (startWithUpperCase) return "No";
            return "no";
        }

        /// <summary>
        /// Returns a single space separated sentence.
        ///<para>• startSentenceWithUpperCase starts the sentence with upper case letter.</para> 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="startSentenceWithUpperCase"></param>
        /// <returns></returns>
        public static string TextToSentence(string? text, bool startSentenceWithUpperCase = true)
        {
            if (text == null) return string.Empty;
            StringBuilder sentence = new();
            string[] splited = text.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < splited.Length; i++)
            {
                if (i == 0)
                {
                    // Start sentence with upper case letter if startSentenceWithUpperCase is true
                    if (startSentenceWithUpperCase)
                    {
                        sentence.Append(splited[i][..1].ToUpper());
                        sentence.Append(splited[i][1..]);
                    }
                    else sentence.Append(splited[i]);
                }
                else
                {
                    sentence.Append(' ');
                    sentence.Append(splited[i]);
                }
            }

            if (sentence.Length > 0)
                return sentence.ToString();
            else
                return string.Empty;
        }

        // ToDo
        // ToValidName bör skrivas om med char.IsLetterOrDigit (har upptäckt att den godkänner svenska bokstäver)
        // OBS! Ett ev. problem är att den kanske hämtar landsinställningen från Windows inställningar,
        // vilket troligen betyder att icke-svensk Windows kanske inte godkänner svenska bokstäver.
        // DETTA BÖR DEFINITIVT UNDERSÖKAS INNAN EV. ÄNDRING!
        //
        // NOT USED IN THIS PROJECT, SO FAR...
        // ToValidName
        //    /// <summary>
        //    /// ToValidName returns a cleaned up nameIn-argument as follows:
        //    /// • It will keep the english letters, ÅÄÖ, single spaces and hyphens in both lower and upper cases.
        //    ///   Their ascii values are: A..Z = 65..90; a..z = 97..122; Å=197, å=229, Ä=196, ä=228, Ö=214 and ö=246
        //    ///   
        //    /// • A single space (ascii=32) or hyphen (ascii=45) is allowed between the words.
        //    /// 
        //    /// • Just the very first letter of the word(s) are captalized to allow f.ex. imput of McClean.
        //    /// 
        //    /// • If nameIn is null or doesn't contain any valid characters, string.Empty is returned.
        //    /// </summary>
        //    /// <param name="nameIn"></param>
        //    /// <returns></returns>
        //    internal static string ToValidName(string nameIn)

        //    {
        //        if (nameIn == null) return string.Empty;

        //        const int SPACE_ASCII = 32, HYPHEN_ASCII = 45;

        //        int ascii = 0, pos = 0;

        //        // only one space or hyphen between the names
        //        int spaces = 0, hyphens = 0;
        //        bool prevHyphen = false, prevSpace = false;

        //        // make sure there is something to check
        //        string unvalidatedName = nameIn.Trim();
        //        if (unvalidatedName.Length == 0) return string.Empty;

        //        // init for preceding hyphen
        //        if (unvalidatedName[..1].Equals("-"))
        //        {
        //            hyphens++;
        //            prevHyphen = true;
        //        }

        //        // characters to keep is added into charList
        //        StringBuilder charList = new();

        //        foreach (char ch in unvalidatedName)
        //        {
        //            pos++;
        //            ascii = (int)ch;

        //            if (ascii == SPACE_ASCII)
        //            {
        //                if (spaces == 0)
        //                {
        //                    if (prevHyphen == false)
        //                    {
        //                        charList.Append(ch);

        //                        // prepare for next round
        //                        prevSpace = true;
        //                        spaces++;
        //                    }
        //                }
        //            }
        //            else if (ascii == HYPHEN_ASCII)
        //            {
        //                if (hyphens == 0)
        //                {
        //                    if (prevSpace == true)
        //                    {
        //                        // remove the last space to prevent input of " -"
        //                        charList.Length--;
        //                        prevSpace = false;
        //                    }
        //                    charList.Append(ch);

        //                    // prepare for next round
        //                    hyphens++;
        //                    prevHyphen = true;
        //                    spaces = 0;
        //                }
        //            }
        //            // remaining valid letters: a..z, å, ä and ö in lower and upper cases
        //            else if ((ascii >= 65 && ascii <= 90) || (ascii >= 97 && ascii <= 122) || ascii == 196 || ascii == 197 || ascii == 214 || ascii == 228 || ascii == 229 || ascii == 246)
        //            {
        //                if ((spaces + hyphens) > 0 || pos == 1)
        //                    charList.Append(ch.ToString().ToUpper()); // capitalize the first letter of every word

        //                else charList.Append(ch);

        //                // prepare for next round
        //                spaces = 0;
        //                hyphens = 0;
        //                prevHyphen = false;
        //                prevSpace = false;
        //            }
        //        }

        //        /*
        //         * return a validated name-string; which is either a blank
        //         * string or a validated string without a concluding hyphen
        //         */
        //        string validName = charList.ToString();

        //        pos = validName.Length - 1; // position of the last character

        //        if (pos < 0)
        //            return string.Empty; // nothing valid to return

        //        else if (validName.Substring(pos, 1).Equals("-"))
        //            return validName[..pos]; // exclude the concluding hyphen

        //        else return validName;
        //    }
    }
}
