using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MultiplexerLib
{
    public class AgilentHelper
    {

        public static double GetPrefix(double value, out string prefix, out double multiplier)
        {
            int degree = (int)Math.Floor(Math.Log10(Math.Abs(value)) / 3);
            double scaled = value * Math.Pow(1000, -degree);

            string[] high = new[] { "k", "M", "G", "T", "P", "E", "Z", "Y" };
            string[] low = new[] { "m", "\u03bc", "n", "p", "f", "a", "z", "y" };

            if (degree >= 0)
            {
                prefix = high[degree - 1];
                multiplier = Math.Pow(1000, -degree);
                return scaled;
            }

            //  degree < 0
            prefix = low[-degree - 1];
            multiplier = Math.Pow(1000, -degree);
            return scaled;
        }

        public static string ToSI(double value, string unit)
        {
            string prefix;
            double multip;

            double scaled = GetPrefix(value, out prefix, out multip);
            return scaled.ToString("N3") + " " + prefix + unit;
        }


        public static string ImpedanceToString(Complex impedance)
        {
            string sr = impedance.Real.ToString("N4").Replace(',', '.');
            string sx = impedance.Imaginary.ToString("N4").Replace(',', '.');

            string s = string.Format("R = {0}; X = {1}",
                ToSI(impedance.Real, "Ω"),
                ToSI(impedance.Imaginary, "Ω"));
            return s;
        }


        public static string CapacitanceToString(Complex capacitance)
        {
            string d = capacitance.Imaginary.ToString("N4").Replace(',', '.');
            string s = string.Format("C = {0}; D = {1}", ToSI(capacitance.Real, "F"), d);
            return s;
        }

    }
}
