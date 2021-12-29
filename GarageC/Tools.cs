using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace GarageC
{
    internal static class Tools
    {
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
         * The current version does not support optional text RegNo(s).
         * At least in Sweden, one could buy an unigue optional text as a regNo.
         * Basicly they can contain any text (don't know the exact roles).
         * An idea: One could have regNo and specialRegNo fields
         * If the text failes, the program then ask if one want to give a speical regNo instead
         * and then allow optional regNo
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
        /// Clean up possibleDoubleString by ref to be parseable as double and return true.
        /// <para> • after removing white space characters the rest must be valid as double.</para> 
        /// <para> • number will return the converted possibleDoubleString.</para> 
        /// /// </summary>
        /// <param name="possibleDoubleString"></param>
        /// <param name="number"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public static bool DoubleString(ref string? possibleDoubleString, ref double number, double min = double.MinValue, double max = double.MaxValue)
        {
            possibleDoubleString = RemoveWhiteSpace(possibleDoubleString);
            try
            {
                number = double.Parse(possibleDoubleString);
                if (!WithinRange(number, min, max)) return false;
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Clean up possibleIntegerString by ref to be parseable as integer and return true.
        /// <para> • after removing white space characters the rest must be valid as int.</para> 
        /// <para> • number will return the converted possibleIntString.</para> 
        /// </summary>
        /// <param name="possibleIntString"></param>
        /// <param name="number"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public static bool IntString(ref string possibleIntString, ref int number, int min = int.MinValue, int max = int.MaxValue)
        {
            possibleIntString = RemoveWhiteSpace(possibleIntString);
            try
            {
                number = int.Parse(possibleIntString);
                if (!WithinRange(number, min, max)) return false;
                return true;
            }
            catch { return false; }
        }
        /// <summary>
        /// Return str without WhiteSpace-characters.
        /// </summary>
        /// <param name="str"></param>
        public static string RemoveWhiteSpace(string? str)
        {
            if (str == null) return "";
            StringBuilder sb = new();
            foreach (char ch in str) if (!char.IsWhiteSpace(ch)) sb.Append(ch);
            return sb.ToString();
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
            if (text == null) return "";
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
                return "";
        }

        // ToDo
        // Bör skrivas om med char.IsLetter (har upptäckt att den godkänner svenska bokstäver)
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
        //    /// • If nameIn is null or doesn't contain any valid characters, an empty string ("") is returned.
        //    /// </summary>
        //    /// <param name="nameIn"></param>
        //    /// <returns></returns>
        //    public static string ToValidName(string nameIn)

        //    {
        //        if (nameIn == null) return "";

        //        const int SPACE_ASCII = 32, HYPHEN_ASCII = 45;

        //        int ascii = 0, pos = 0;

        //        // only one space or hyphen between the names
        //        int spaces = 0, hyphens = 0;
        //        bool prevHyphen = false, prevSpace = false;

        //        // make sure there is something to check
        //        string unvalidatedName = nameIn.Trim();
        //        if (unvalidatedName.Length == 0) return "";

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
        //            return ""; // nothing valid to return

        //        else if (validName.Substring(pos, 1).Equals("-"))
        //            return validName[..pos]; // exclude the concluding hyphen

        //        else return validName;
        //    }
    }
}
