﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorLibraryCA2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLibraryCA2.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void AddTest()
        {
            Assert.AreEqual(0, Calculator.Add(0, 0));
            Assert.AreEqual(1, Calculator.Add(1, 0));
            Assert.AreEqual(1.5, Calculator.Add(1, 0.5));
            Assert.AreEqual(-1, Calculator.Add(0, -1));
            Assert.AreEqual(4, Calculator.Add(2, 2));
            Assert.AreEqual(5, Calculator.Add(2, 3));
        }

        [TestMethod]
        public void BaseToExponentTest()
        {
            Assert.AreEqual(1, Calculator.BaseToExponent(0, 0));
            Assert.AreEqual(1, Calculator.BaseToExponent(10, 0));
            Assert.AreEqual(0.25, Calculator.BaseToExponent(4, -1));
            Assert.AreEqual(-27, Calculator.BaseToExponent(-3, 3));
            Assert.AreEqual(10000000000, Calculator.BaseToExponent(10, 10));
        }

        [TestMethod]
        public void CubedTest()
        {
            Assert.AreEqual(0, Calculator.Cube(0));
            Assert.AreEqual(1, Calculator.Cube(1));
            Assert.AreEqual(27, Calculator.Cube(3));
            Assert.AreEqual(-27, Calculator.Cube(-3));
            Assert.AreEqual(0.125, Calculator.Cube(0.5));
            Assert.AreEqual(3.375, Calculator.Cube(1.5));
        }

        [TestMethod]
        public void DivideByZeroGivesInfinityTest()
        {
            Assert.AreEqual(double.PositiveInfinity, Calculator.Divide(1.5, 0));
            Assert.AreEqual(double.NegativeInfinity, Calculator.Divide(-1.5, 0));
            Assert.AreEqual(double.PositiveInfinity, Calculator.Divide(1, 0));
        }

        [TestMethod]
        public void DivideTest()
        {
            Assert.AreEqual(2, Calculator.Divide(1, 0.5));
            Assert.AreEqual(0, Calculator.Divide(0, -1));
            Assert.AreEqual(1, Calculator.Divide(2, 2));
            Assert.AreEqual(1.5, Calculator.Divide(3, 2));
            Assert.AreEqual(-1.5, Calculator.Divide(-3, 2));
            Assert.AreEqual(1.5, Calculator.Divide(-3, -2));
        }

        [TestMethod]
        public void FactorialTest()
        {
            Assert.AreEqual(1, Calculator.Factorial(1));
            Assert.AreEqual(2, Calculator.Factorial(2));
            Assert.AreEqual(6, Calculator.Factorial(3));
            Assert.AreEqual(24, Calculator.Factorial(4));
            Assert.AreEqual(120, Calculator.Factorial(5));

            try
            {
                Calculator.Factorial(3.6);
                //Execution should not make it to next line as exception should be thrown
                //If it does, test should fail
                Assert.Fail("No exception was thrown");
            }
            catch (ArgumentException e)
            {
                //Catches appropriate exception and test passes
                StringAssert.Contains(e.Message, "Number must be an integer value.");
            }
            catch (Exception)
            {
                //Catch base exception in case exception other than expected is thrown
                //test fails in that case
                Assert.Fail("Unexpected exception was thrown");
            }

            try
            {
                Calculator.Factorial(-1);
                //Execution should not make it to next line as exception should be thrown
                //If it does, test should fail
                Assert.Fail("No exception was thrown");
            }
            catch (ArgumentOutOfRangeException e)
            {
                //Catches appropriate exception and test passes
                StringAssert.Contains(e.Message, "Number cannot be negative.");
            }
            catch (Exception)
            {
                //Catch base exception in case exception other than expected is thrown
                //test fails in that case
                Assert.Fail("Unexpected exception was thrown");
            }
        }

        [TestMethod]
        public void InvertTest()
        {
            Assert.AreEqual(1, Calculator.Invert(1));
            Assert.AreEqual(-1, Calculator.Invert(-1));
            Assert.AreEqual(0.5, Calculator.Invert(2));
            Assert.AreEqual(-0.5, Calculator.Invert(-2));
            Assert.AreEqual(0.1, Calculator.Invert(10));
            Assert.AreEqual(0.190476190476, Math.Round(Calculator.Invert(5.25), 12));
            Assert.AreEqual(double.PositiveInfinity, Calculator.Invert(0));
        }

        [TestMethod]
        public void MultiplyTest()
        {
            Assert.AreEqual(0, Calculator.Multiply(0, 0));
            Assert.AreEqual(0, Calculator.Multiply(1, 0));
            Assert.AreEqual(0.5, Calculator.Multiply(1, 0.5));
            Assert.AreEqual(0, Calculator.Multiply(0, -1));
            Assert.AreEqual(4, Calculator.Multiply(2, 2));
            Assert.AreEqual(6, Calculator.Multiply(2, 3));
            Assert.AreEqual(-6, Calculator.Multiply(2, -3));
            Assert.AreEqual(-6, Calculator.Multiply(-2, 3));
            Assert.AreEqual(6, Calculator.Multiply(-2, -3));
        }

        [TestMethod]
        public void PlusMinusTest()
        {
            Assert.AreEqual(0, Calculator.PlusMinus(0));
            Assert.AreEqual(1, Calculator.PlusMinus(-1));
            Assert.AreEqual(-1, Calculator.PlusMinus(1));
            Assert.AreEqual(1.5, Calculator.PlusMinus(-1.5));
            Assert.AreEqual(-1.5, Calculator.PlusMinus(1.5));
        }

        [TestMethod]
        public void SquaredTest()
        {
            Assert.AreEqual(0, Calculator.Square(0));
            Assert.AreEqual(1, Calculator.Square(1));
            Assert.AreEqual(4, Calculator.Square(2));
            Assert.AreEqual(16, Calculator.Square(4));
            Assert.AreEqual(16, Calculator.Square(-4));
            Assert.AreEqual(0.25, Calculator.Square(0.5));
            Assert.AreEqual(2.25, Calculator.Square(1.5));
        }

        [TestMethod]
        public void SquareRootTest()
        {
            Assert.AreEqual(1, Calculator.SquareRoot(1));
            Assert.AreEqual(2, Calculator.SquareRoot(4));
            Assert.AreEqual(double.NaN, Calculator.SquareRoot(-4));
            Assert.AreEqual(1.73205080756888, Math.Round(Calculator.SquareRoot(3), 14));
            Assert.AreEqual(3, Calculator.SquareRoot(9));
            Assert.AreEqual(4, Calculator.SquareRoot(16));
            Assert.AreEqual(0, Calculator.SquareRoot(0));
        }

        [TestMethod]
        public void SubtractTest()
        {
            Assert.AreEqual(0, Calculator.Subtract(0, 0));
            Assert.AreEqual(1, Calculator.Subtract(1, 0));
            Assert.AreEqual(0.5, Calculator.Subtract(1, 0.5));
            Assert.AreEqual(1, Calculator.Subtract(0, -1));
            Assert.AreEqual(0, Calculator.Subtract(2, 2));
            Assert.AreEqual(-1, Calculator.Subtract(2, 3));
        }
    }
}