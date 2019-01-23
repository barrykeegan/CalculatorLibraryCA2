using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorLibraryCA2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorLibraryCA2.Tests
{
    [TestClass()]
    public class CalculatorTests
    {
        [TestMethod()]
        public void AddTest()
        {
            Assert.AreEqual(0, Calculator.Add(0, 0));
            Assert.AreEqual(1, Calculator.Add(1, 0));
            Assert.AreEqual(1.5, Calculator.Add(1, 0.5));
            Assert.AreEqual(-1, Calculator.Add(0, -1));
            Assert.AreEqual(4, Calculator.Add(2, 2));
            Assert.AreEqual(5, Calculator.Add(2, 3));
        }
    }
}