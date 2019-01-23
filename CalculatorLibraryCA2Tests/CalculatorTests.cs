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
        public void DivideByZeroGivesInfinityTest()
        {
            Assert.AreEqual(double.PositiveInfinity, Calculator.Divide(1.5, 0));
            Assert.AreEqual(double.NegativeInfinity, Calculator.Divide(-1.5, 0));
            Assert.AreEqual(double.PositiveInfinity, Calculator.Divide(1, 0));
            //Assert.AreEqual(0, Calculator.Divide(1, 0));
            //Assert.AreEqual(0.5, Calculator.Divide(1, 0));
            //Assert.AreEqual(0, Calculator.Divide(0, 0));
            //Assert.AreEqual(4, Calculator.Divide(2, 0));
            //Assert.AreEqual(6, Calculator.Divide(2, 0));
            //Assert.AreEqual(-6, Calculator.Divide(2, 0));
            //Assert.AreEqual(-6, Calculator.Divide(-2, 0));
            //Assert.AreEqual(6, Calculator.Divide(-2, 0));
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