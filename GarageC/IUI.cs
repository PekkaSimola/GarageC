using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageC
{
    internal interface IUI
    {
        public abstract bool GetConsoleInt(string prompt, ref int number, int min = int.MinValue, int max = int.MaxValue, int escInt = -1);
        public abstract bool GetConsoleDouble(string prompt, ref double number, double min = double.MinValue, double max = double.MaxValue, double escDouble = -1.0);
        public abstract bool GetConsoleSentence(string inputMsg, ref string sentence, uint minLength = uint.MinValue, uint maxLength = uint.MaxValue, bool startSentenceWithUpperCase = true);

    }
}
