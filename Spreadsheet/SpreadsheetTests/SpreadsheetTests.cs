using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGet.Frameworks;
using SpreadsheetUtilities;
using SS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

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
        public void SetCellContentsChangningFromDependentFormulaToString()
        {
            Spreadsheet ss = new Spreadsheet();
            Assert.IsTrue(ss.SetCellContents("A1", new Formula("5")).Contains("A1"));
            Assert.IsTrue(ss.SetCellContents("B1", new Formula("A1")).Contains("B1"));
            Assert.IsTrue(ss.SetCellContents("A1", new Formula("5")).Contains("A1"));
            Assert.IsTrue(ss.SetCellContents("A1", new Formula("5")).Contains("B1"));
            Assert.IsTrue(ss.SetCellContents("B1", "text").Contains("B1"));
            Assert.IsFalse(ss.SetCellContents("A1", new Formula("5")).Contains("B1"));
        }

        [TestMethod]
        public void SimpleDoubleTest()
        {
            Spreadsheet ss = new Spreadsheet();
            Assert.IsTrue(ss.SetCellContents("A1", Double.Parse("2.000")).Contains("A1"));
            Assert.AreEqual(1, (ss.SetCellContents("A1", Double.Parse("2.000")).Count()));
        }

        [TestMethod]
        public void SetCellContentsChangningFromDependentFormulaToDouble()
        {
            Spreadsheet ss = new Spreadsheet();
            Assert.IsTrue(ss.SetCellContents("A1", new Formula("5")).Contains("A1"));
            Assert.IsTrue(ss.SetCellContents("B1", new Formula("A1")).Contains("B1"));
            Assert.IsTrue(ss.SetCellContents("A1", new Formula("5")).Contains("A1"));
            Assert.IsTrue(ss.SetCellContents("A1", new Formula("5")).Contains("B1"));
            Assert.IsTrue(ss.SetCellContents("B1", Double.Parse("2.000")).Contains("B1"));
            Assert.IsFalse(ss.SetCellContents("A1", new Formula("5")).Contains("B1"));
        }

        [TestMethod]
        public void GetCellContentsTest()
        {
            Spreadsheet ss = new Spreadsheet();
            Assert.IsTrue(ss.SetCellContents("A1", "text").Contains("A1"));
            Assert.IsTrue(ss.GetCellContents("A1").Equals("text"));
        }

        [TestMethod]
        public void GetEmptyCellContentsTest()
        {
            Spreadsheet ss = new Spreadsheet();
            Assert.IsTrue(ss.SetCellContents("A1", "").Contains("A1"));
            Assert.IsTrue(ss.GetCellContents("A1").Equals(""));
        }

        [TestMethod]
        public void GetUnamedCellContentsTest()
        {
            Spreadsheet ss = new Spreadsheet();
            Assert.IsTrue(ss.GetCellContents("A1").Equals(""));
        }

        [TestMethod]
        public void SimpleGetNamesOfAllNonEmptyCells()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetCellContents("A1", "text");
            ss.SetCellContents("B3", "text");
            ss.SetCellContents("X2", Double.Parse("6.90"));
            ss.SetCellContents("G6", new Formula("400 + 20"));
            Assert.IsTrue(ss.GetNamesOfAllNonemptyCells().Contains("A1"));
            Assert.IsTrue(ss.GetNamesOfAllNonemptyCells().Contains("B3"));
            Assert.IsTrue(ss.GetNamesOfAllNonemptyCells().Contains("X2"));
            Assert.IsTrue(ss.GetNamesOfAllNonemptyCells().Contains("G6"));
        }

        [TestMethod]
        public void ReplaceCellToBeEmptyGetNamesOfAllNonEmptyCells()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetCellContents("A1", "text");
            ss.SetCellContents("B3", "text");
            ss.SetCellContents("X2", Double.Parse("6.90"));
            ss.SetCellContents("G6", new Formula("400 + 20"));
            ss.SetCellContents("B3", "");
            Assert.IsTrue(ss.GetNamesOfAllNonemptyCells().Contains("A1"));
            Assert.IsFalse(ss.GetNamesOfAllNonemptyCells().Contains("B3"));
            Assert.IsTrue(ss.GetNamesOfAllNonemptyCells().Contains("X2"));
            Assert.IsTrue(ss.GetNamesOfAllNonemptyCells().Contains("G6"));
        }

        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void CircularDependency()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetCellContents("A1", "2");
            ss.SetCellContents("B1", new Formula("A1"));
            ss.SetCellContents("A1", new Formula("B1"));
        }

        [TestMethod]
        public void CircularDependencyPreviousStateReverted()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetCellContents("A1", "2");
            ss.SetCellContents("B1", new Formula("A1"));
            try
            {
                ss.SetCellContents("A1", new Formula("B1"));
            }
            catch (CircularException) { }

            Assert.IsTrue(ss.SetCellContents("A1", "2").Contains("B1"));
        }

        [TestMethod]
        public void CircularDependencyPreviousStateRevertedWhenNewCellCausesCyclic()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetCellContents("A1", new Formula("B1"));
            ss.SetCellContents("B1", new Formula("C1"));
            try
            {
                ss.SetCellContents("C1", new Formula("A1"));
            }
            catch (CircularException) { }

            Assert.IsTrue(ss.SetCellContents("B1", "2").Contains("A1"));
        }

    }


}
