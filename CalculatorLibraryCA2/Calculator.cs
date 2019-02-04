﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLibraryCA2
{
    public class Calculator
    {
        public static double Add(double n1, double n2)
        {
            //return 0.0; Original
            return n1 + n2; //Version 2
        }

        public static double Cube(double x)
        {
            //return 0; Original
            return Math.Pow(x, 3);
        }

        public static double Divide(double n1, double n2)
        {
            return n1 / n2;
        }

        public static double Multiply(double n1, double n2)
        {
            //return 0.0; //Original
            return n1 * n2; //Version 2
        }

        public static double Square(double x)
        {
            return x * x;
        }

        public static double Subtract(double n1, double n2)
        {
            //return 0.0; //Original
            return n1 - n2; //Version 2
        }
    }
}
