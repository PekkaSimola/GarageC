using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageC
{
    internal class ConsoleUI : IUI
    {
        public const int ESC_INT = -1;
        public const double ESC_DOUBLE = ESC_INT;

        public bool GetConsoleInt(string inputMsg, ref int number, int min = int.MinValue, int max = int.MaxValue, int escInt = ESC_INT)
        {
            string? consoleString;
            Console.Write(inputMsg);
            consoleString = Console.ReadLine();
            if (consoleString?.Trim().Length == 0)
            {
                number = escInt;
                return true;
            }

            if (consoleString == null) return false;

            if (Tools.IntString(ref consoleString, ref number, min, max))
                return true;

            return false;
        }

        public bool GetConsoleDouble(string inputMsg, ref double number, double min = double.MinValue, double max = double.MaxValue, double escDouble = ESC_DOUBLE)
        {
            string? consoleString;
            Console.Write(inputMsg);
            consoleString = Console.ReadLine();
            if (consoleString?.Trim().Length == 0)
            {
                number = escDouble;
                return true;
            }

            if (consoleString == null) return false;

            if (Tools.DoubleString(ref consoleString, ref number, min, max))
                return true;

            return false;
        }

        public bool GetConsoleSentence(string inputMsg, ref string sentence, uint minLength = 1, uint maxLength = 20, bool startSentenceWithUpperCase = true)
        {
            string? consoleString;
            Console.Write(inputMsg);
            consoleString = Tools.TextToSentence(Console.ReadLine(), startSentenceWithUpperCase);
            int len = consoleString.Length;

            if (len == 0)
            {
                sentence = ""; // user canceled
                return true;
            }

            sentence = consoleString;

            if (len < minLength || len > maxLength) return false;

            return true;
        }

    }
}
