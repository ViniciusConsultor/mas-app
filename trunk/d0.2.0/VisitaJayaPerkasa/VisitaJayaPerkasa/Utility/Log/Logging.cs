using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisitaJayaPerkasa.Utility.Log
{
    public class Logging
    {
        public static void Error(String text) {
            Console.WriteLine("Error - " + text);
        }

        public static void Information(String text) {
            Console.WriteLine("Information - " + text);
        }

        public static void Warning(String text) {
            Console.WriteLine("Warning - " + text);
        }
    }
}
