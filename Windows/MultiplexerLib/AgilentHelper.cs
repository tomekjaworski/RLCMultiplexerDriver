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
            if (degree > 8)
                degree = 8;
            if (degree < -8)
                degree = -8;

            double scaled = value * Math.Pow(1000, -degree);

            string[] high = new[] { "k", "M", "G", "T", "P", "E", "Z", "Y" };
            string[] low = new[] { "m", "\u03bc", "n", "p", "f", "a", "z", "y" };

            if (degree > 0)
            {
                prefix = high[degree - 1];
                multiplier = Math.Pow(1000, -degree);
                return scaled;
            }

            if (degree < 0)
            {
                prefix = low[-degree - 1];
                multiplier = Math.Pow(1000, -degree);
                return scaled;
            }

            // degree == 0
            prefix = "";
            multiplier = 1;
            return value;
        }

        public static string ToSI(double value, string unit)
        {
            string prefix;
            double multip;

            double scaled = GetPrefix(value, out prefix, out multip);
            return scaled.ToString("N3").Replace(',', '.') + " " + prefix + unit;
        }


        public static string ImpedanceToString(Complex impedance, bool only_resistance)
        {
            string sr = impedance.Real.ToString("N4").Replace(',', '.');
            string sx = impedance.Imaginary.ToString("N4").Replace(',', '.');

            if (only_resistance)
                return ToSI(impedance.Real, "Ω");

            return string.Format("R = {0}; X = {1}",
                ToSI(impedance.Real, "Ω"),
                ToSI(impedance.Imaginary, "Ω"));
        }


        public static string CapacitanceToString(Complex capacitance, bool only_capacitance)
        {
            if (only_capacitance)
                return ToSI(capacitance.Real, "F");

            string d = capacitance.Imaginary.ToString("N4").Replace(',', '.');
            return string.Format("C = {0}; D = {1}", ToSI(capacitance.Real, "F"), d);
        }

    }
}
