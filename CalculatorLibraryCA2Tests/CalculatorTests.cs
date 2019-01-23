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
            Assert.AreEqual(0, Calculator.Add(0,0) );
        }
    }
}