using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageC
{
    internal class ConsoleUI : IUI
    {
        /// <summary>
        /// Displays: Press a key &lt;lastWord&gt; »
        /// </summary>
        /// <param name="lastWord">default => "continue"</param>
        public static void PressKey(string lastWord = "continue")
        {
            string text = " Press a key to " + lastWord + " >";
            int lineLen = text.Length - 1;
            
            DrawLine(lineLen);
            Console.WriteLine(text);
            DrawLine(lineLen,'¯');

            Console.SetCursorPosition(Console.CursorLeft = lineLen +2, Console.CursorTop -= 2);

            Console.ReadKey(true);
            Console.SetCursorPosition(0, Console.CursorTop += 2);
        }
        /// <summary>
        /// Draw a line with length-amount characters via Console.WriteLIne.
        /// </summary>
        /// <param name="length">line length</param>
        /// <param name="character">line character</param>
        public static void DrawLine(int length, char character = '_') { Console.WriteLine(new string(character, length)); }

        /// <summary>
        /// Returns the longist title within values[even].
        /// </summary>
        /// <param name="values">x-amount title-value pairs</param>
        internal static int LongistTitle(string[] values)
        {
            int longist = 0, len;
            for (int i = 0; i < values.Length - 1; i += 2)
            {
                len = values[i].Length;
                if (len > longist) longist = len;
            }
            return longist;
        }

        /// <summary>
        /// Writes current vehicle properties nicely formated into the Console.
        /// </summary>
        /// <param name="v">current vehicle instance</param>
        /// <param name="titleColor">title text color; default by Const-class</param>
        /// <param name="bodytextColor">property text color; default by Const-class</param>
        internal static void DisplayVehicle(Vehicle v, ConsoleColor titleColor = Consts.TitleColor, ConsoleColor bodytextColor = Consts.BodytextColor)
        {
            ConsoleColor savedColor = Console.ForegroundColor;

            string rn = v.RegNo;
            string ct = v.ClassTitle;
            int lineLen = ct.Length + rn.Length + 3;

            Console.ForegroundColor = titleColor;
            Console.WriteLine(new string('_', lineLen));
            Console.Write(" " + ct);

            Console.ForegroundColor = bodytextColor;
            Console.WriteLine(" " + rn);

            Console.ForegroundColor = titleColor;
            Console.WriteLine(new string('¯', lineLen));

            string[] values = v.BodyToDisplay();
            int leftPadding = LongistTitle(values) + 1;

            for (int i = 0; i < values.Length - 1; i += 2)
            {
                Console.ForegroundColor = titleColor;
                Console.Write(values[i].PadLeft(leftPadding));
                Console.ForegroundColor = bodytextColor;
                Console.WriteLine(values[i + 1]);
            }

            Console.ForegroundColor = savedColor;
        }

        /// <summary>
        /// Add lines obave and below the title text.
        /// </summary>
        /// <param name="titleText">either title text, or Vehicle instance</param>
        /// <returns>Nicely formated titleText suitable for ToText() method</returns>
        internal static string ToTextTitle(string titleText)
        {
            int lineLen = titleText.Length + 2;
            StringBuilder s = new(new string('_', lineLen));
            s.AppendLine("\n " + titleText);
            s.AppendLine(new string('¯', lineLen));
            return s.ToString();
        }
        internal static string ToTextTitle(Vehicle v)
        {
            int lineLen = v.ClassTitle.Length + v.RegNo.Length + 3;
            StringBuilder s = new(new string('_', lineLen));
            s.AppendLine("\n " + v.ClassTitle + " " + v.RegNo);
            s.AppendLine(new string('¯', lineLen));
            return s.ToString();
        }

        /// <summary>
        /// Takes an input via Console, validates it, and returns a tuple (success, returnInt, consoleInput)
        /// <para>user cancels by Enter with the returnTuple (true, cancelInt, string.Empty)</para> 
        /// <para>NOTE: prompt should for instance end with &lt;Enter cancels&gt;:</para> 
        /// </summary>
        public (bool, int, string?) GetConsoleInt(string prompt, int min = int.MinValue, int max = int.MaxValue, int cancelInt = -1)
        {
            string? consoleInput;
            Console.Write(prompt);
            consoleInput = Console.ReadLine(); // never null

            if (consoleInput?.Trim().Length == 0) return (true, cancelInt, string.Empty); // user canceled

            return (Tool.TryParseInterval(consoleInput, out int number, min, max), number, consoleInput);
        }

        /// <summary>
        /// Takes an input via Console, validates it, and returns a tuple (success, returnDouble, consoleInput)
        /// <para>user cancels by Enter with the returnTuple (true, cancelDouble, string.Empty)</para> 
        /// <para>NOTE: prompt should for instance end with &lt;Enter cancels&gt;:</para> 
        /// </summary>
        public (bool, double, string?) GetConsoleDouble(string prompt, double min = double.MinValue, double max = double.MaxValue, double cancelDouble = -1.0)
        {
            string? consoleInput;
            Console.Write(prompt);
            consoleInput = Console.ReadLine(); // never null

            if (consoleInput?.Trim().Length == 0) return (true, cancelDouble, string.Empty); // user canceled

            return (Tool.TryParseInterval(consoleInput, out double number, min, max), number, consoleInput);
        }

        /// <summary>
        /// Takes an input via Console, validates its length, and returns a tuple (success, consoleSentence)
        /// <para>user cancels by Enter with the returnTuple (true, cancelString)</para> 
        /// <para>NOTE: prompt should for instance end with &lt;Enter cancels&gt;:</para> 
        /// </summary>
        public (bool, string?) GetConsoleSentence(string prompt, uint minLength = 1, uint maxLength = 20, bool startSentenceWithUpperCase = true, string cancelString = "#Cancel!")
        {
            string? consoleSentence;
            Console.Write(prompt);
            consoleSentence = Tool.TextToSentence(Console.ReadLine(), startSentenceWithUpperCase); // never null

            int len = consoleSentence.Length;
            if (len == 0) return (true, cancelString);

            return (!(len < minLength || len > maxLength), consoleSentence);
        }
    }
}
