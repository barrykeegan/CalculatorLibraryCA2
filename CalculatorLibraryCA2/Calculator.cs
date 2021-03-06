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

        public static double BaseToExponent(double x, double y)
        {
            /*******************************************
             * To explain below original version, before
             * beginning development of BaseToExponent I
             * believed Math.Pow(0, 0) would result in 
             * NaN, but it returns 1, which caused an 
             * error in the test, so that had to be
             * modified also.
             *******************************************/
            //return double.NaN; Original
            return Math.Pow(x, y);
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

        //Factorial implemented to throw error, just as an exercise
        //in error handling during testing. could easily have just returned NaN.
        public static double Factorial(double x)
        {
            //return 1; original
            if (x % 1 == 0)
            {
                if (x > -1)
                {
                    double fact = 1;
                    for (double i = x; i > 0; i--)
                    {
                        fact *= i;
                    }
                    return fact;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Number cannot be negative.");
                }
            }
            else
            {
                throw new ArgumentException("Number must be an integer value.");
            }
        }

        public static double Invert(double x)
        {
            //original return 1;
            return 1 / x;
        }

        public static double Multiply(double n1, double n2)
        {
            //return 0.0; //Original
            return n1 * n2; //Version 2
        }

        public static double PlusMinus(double x)
        {
            return x * -1;
        }

        public static double Square(double x)
        {
            return x * x;
        }

        public static double SquareRoot(double x)
        {
            //return 1; original
            return Math.Sqrt(x);
        }

        public static double Subtract(double n1, double n2)
        {
            //return 0.0; //Original
            return n1 - n2; //Version 2
        }
    }
}
