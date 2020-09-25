using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGet.Frameworks;
using SpreadsheetUtilities;
using SS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpreadsheetTests
{
    [TestClass]
    public class SpreadsheetTests
    {
        [TestMethod]
        public void EmptyContructorMethod()
        {
            Spreadsheet spreadsheet = new Spreadsheet();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetCellWithNullFormula()
        {
            Spreadsheet spreadsheet = new Spreadsheet();
            Formula formula = null;
            spreadsheet.SetCellContents("A1", formula);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void SetCellNullNameFormula()
        {
            Spreadsheet ss = new Spreadsheet();
            string name = null;
            ss.SetCellContents(name, new Formula("2 - 2"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void SetCellInvalidNameFormula()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetCellContents("3f", new Formula("2 + 2"));
        }

        [TestMethod]
        public void SetCellContentsFormulaNoDependencies()
        {
            Spreadsheet ss = new Spreadsheet();
            Assert.AreEqual(1, ss.SetCellContents("A1", new Formula("2 + 2")).Count());
        }

        [TestMethod]
        public void SetCellContentsFormulaSeperateChains1()
        {
            Spreadsheet ss = new Spreadsheet();
            Assert.AreEqual(1, ss.SetCellContents("A1", new Formula("2 + 2")).Count());
            Assert.AreEqual(1, ss.SetCellContents("B1", new Formula("2 + 2")).Count());
        }

        [TestMethod]
        public void SetCellContentsFormulaOneDependency()
        {
            Spreadsheet ss = new Spreadsheet();
            Assert.AreEqual(1, ss.SetCellContents("A1", new Formula("2 + 2")).Count());
            Assert.AreEqual(1, ss.SetCellContents("B1", new Formula("A1")).Count());
            Assert.AreEqual(2, ss.SetCellContents("A1", new Formula("5 - 7")).Count());
        }

        [TestMethod]
        public void SetCellContentsChangingMultipleDependenciesToNone()
        {
            Spreadsheet ss = new Spreadsheet();
            Assert.AreEqual(1, ss.SetCellContents("A1", new Formula("1")).Count());
            Assert.AreEqual(1, ss.SetCellContents("B1", new Formula("5")).Count());
            Assert.AreEqual(1, ss.SetCellContents("C1", new Formula("A1 + B1")).Count());
            Assert.AreEqual(2, ss.SetCellContents("A1", new Formula("5")).Count());
            Assert.AreEqual(2, ss.SetCellContents("B1", new Formula("1")).Count());
            Assert.AreEqual(1, ss.SetCellContents("C1", new Formula("5")).Count());
            Assert.AreEqual(1, ss.SetCellContents("A1", new Formula("5")).Count());
            Assert.AreEqual(1, ss.SetCellContents("B1", new Formula("1")).Count());
        }

        [TestMethod]
        public void SetCellContentsString()
        {
            Spreadsheet ss = new Spreadsheet();
            Assert.AreEqual(1, ss.SetCellContents("A1", "test").Count());
        }

        [TestMethod]
        public void SetCellContentsChangningFromDependentFormula()
        {
            Spreadsheet ss = new Spreadsheet();
            Assert.IsTrue(ss.SetCellContents("A1", new Formula("5")).Contains("A1"));
            Assert.IsTrue(ss.SetCellContents("B1", new Formula("A1")).Contains("B1"));
            Assert.IsTrue(ss.SetCellContents("A1", new Formula("5")).Contains("A1"));
            Assert.IsTrue(ss.SetCellContents("A1", new Formula("5")).Contains("B1"));
        }
    }

    
}
