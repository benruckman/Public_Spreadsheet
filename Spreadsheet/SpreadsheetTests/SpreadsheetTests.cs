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
            spreadsheet.SetContentsOfCell("A1", null);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void SetCellNullNameFormula()
        {
            Spreadsheet ss = new Spreadsheet();
            string name = null;
            ss.SetContentsOfCell(name, "=2 - 2");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void SetCellInvalidNameFormula()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetContentsOfCell("3f", "=2 + 2");
        }

        [TestMethod]
        public void SetCellContentsFormulaNoDependencies()
        {
            Spreadsheet ss = new Spreadsheet();
            Assert.AreEqual(1, ss.SetContentsOfCell("A1", "=2 + 2").Count());
        }

        [TestMethod]
        public void SetCellContentsFormulaSeperateChains1()
        {
            Spreadsheet ss = new Spreadsheet();
            Assert.AreEqual(1, ss.SetContentsOfCell("A1", "=2 + 2").Count());
            Assert.AreEqual(1, ss.SetContentsOfCell("B1", "=2 + 2").Count());
        }

        [TestMethod]
        public void SetCellContentsFormulaOneDependency()
        {
            Spreadsheet ss = new Spreadsheet();
            Assert.AreEqual(1, ss.SetContentsOfCell("A1", "=2 + 2").Count());
            Assert.AreEqual(1, ss.SetContentsOfCell("B1", "=A1").Count());
            Assert.AreEqual(2, ss.SetContentsOfCell("A1", "=5 - 7").Count());
        }

        [TestMethod]
        public void SetCellContentsChangingMultipleDependenciesToNone()
        {
            Spreadsheet ss = new Spreadsheet();
            Assert.AreEqual(1, ss.SetContentsOfCell("A1", "=1").Count());
            Assert.AreEqual(1, ss.SetContentsOfCell("B1", "=5").Count());
            Assert.AreEqual(1, ss.SetContentsOfCell("C1", "=A1 + B1").Count());
            Assert.AreEqual(2, ss.SetContentsOfCell("A1", "=5").Count());
            Assert.AreEqual(2, ss.SetContentsOfCell("B1", "=1").Count());
            Assert.AreEqual(1, ss.SetContentsOfCell("C1", "=5").Count());
            Assert.AreEqual(1, ss.SetContentsOfCell("A1", "=5").Count());
            Assert.AreEqual(1, ss.SetContentsOfCell("B1", "=1").Count());
        }

        [TestMethod]
        public void SetCellContentsString()
        {
            Spreadsheet ss = new Spreadsheet();
            Assert.AreEqual(1, ss.SetContentsOfCell("A1", "test").Count());
        }

        [TestMethod]
        public void SetCellContentsChangningFromDependentFormulaToString()
        {
            Spreadsheet ss = new Spreadsheet();
            Assert.IsTrue(ss.SetContentsOfCell("A1", "5").Contains("A1"));
            Assert.IsTrue(ss.SetContentsOfCell("B1", "=A1").Contains("B1"));
            Assert.IsTrue(ss.SetContentsOfCell("A1", "=5").Contains("A1"));
            Assert.IsTrue(ss.SetContentsOfCell("A1", "=5").Contains("B1"));
            Assert.IsTrue(ss.SetContentsOfCell("B1", "text").Contains("B1"));
            Assert.IsFalse(ss.SetContentsOfCell("A1", "=5").Contains("B1"));
        }

        [TestMethod]
        public void SimpleDoubleTest()
        {
            Spreadsheet ss = new Spreadsheet();
            Assert.IsTrue(ss.SetContentsOfCell("A1", "2.000").Contains("A1"));
            Assert.AreEqual(1, (ss.SetContentsOfCell("A1", "2.000").Count()));
        }

        [TestMethod]
        public void SetCellContentsChangningFromDependentFormulaToDouble()
        {
            Spreadsheet ss = new Spreadsheet();
            Assert.IsTrue(ss.SetContentsOfCell("A1", "=5").Contains("A1"));
            Assert.IsTrue(ss.SetContentsOfCell("B1", "=A1").Contains("B1"));
            Assert.IsTrue(ss.SetContentsOfCell("A1", "=5").Contains("A1"));
            Assert.IsTrue(ss.SetContentsOfCell("A1", "=5").Contains("B1"));
            Assert.IsTrue(ss.SetContentsOfCell("B1", "2.000").Contains("B1"));
            Assert.IsFalse(ss.SetContentsOfCell("A1", "=5").Contains("B1"));
        }

        [TestMethod]
        public void GetCellContentsTest()
        {
            Spreadsheet ss = new Spreadsheet();
            Assert.IsTrue(ss.SetContentsOfCell("A1", "text").Contains("A1"));
            Assert.IsTrue(ss.GetCellContents("A1").Equals("text"));
        }

        [TestMethod]
        public void GetEmptyCellContentsTest()
        {
            Spreadsheet ss = new Spreadsheet();
            Assert.IsTrue(ss.SetContentsOfCell("A1", "").Contains("A1"));
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
            ss.SetContentsOfCell("A1", "text");
            ss.SetContentsOfCell("B3", "text");
            ss.SetContentsOfCell("X2", "6.90");
            ss.SetContentsOfCell("G6", "=400 + 20");
            Assert.IsTrue(ss.GetNamesOfAllNonemptyCells().Contains("A1"));
            Assert.IsTrue(ss.GetNamesOfAllNonemptyCells().Contains("B3"));
            Assert.IsTrue(ss.GetNamesOfAllNonemptyCells().Contains("X2"));
            Assert.IsTrue(ss.GetNamesOfAllNonemptyCells().Contains("G6"));
        }

        [TestMethod]
        public void ReplaceCellToBeEmptyGetNamesOfAllNonEmptyCells()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetContentsOfCell("A1", "text");
            ss.SetContentsOfCell("B3", "text");
            ss.SetContentsOfCell("X2", "6.90");
            ss.SetContentsOfCell("G6", "=400 + 20");
            ss.SetContentsOfCell("B3", "");
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
            ss.SetContentsOfCell("A1", "2");
            ss.SetContentsOfCell("B1", "=A1");
            ss.SetContentsOfCell("A1", "=B1");
        }

        [TestMethod]
        public void CircularDependencyPreviousStateReverted()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetContentsOfCell("A1", "2");
            ss.SetContentsOfCell("B1", "=A1");
            try
            {
                ss.SetContentsOfCell("A1", "=B1");
            }
            catch (CircularException) { }

            Assert.IsTrue(ss.SetContentsOfCell("A1", "2").Contains("B1"));
        }

        [TestMethod]
        public void CircularDependencyPreviousStateRevertedWhenNewCellCausesCyclic()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetContentsOfCell("A1", "=B1");
            ss.SetContentsOfCell("B1", "=C1");
            try
            {
                ss.SetContentsOfCell("C1", "=A1");
            }
            catch (CircularException) { }

            Assert.IsTrue(ss.SetContentsOfCell("B1", "2").Contains("A1"));
        }

        //Tests added for PS5 are below

        [TestMethod]
        public void IsValidWorksCorrectlyTest()
        {
            Spreadsheet ss = new Spreadsheet(s => s == "A1", s => s, "default");
            Assert.IsTrue(ss.SetContentsOfCell("A1", "test").Contains("A1"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void IsValidFailsAnInvalidNameTest()
        {
            Spreadsheet ss = new Spreadsheet(s => s == "A1", s => s, "default");
            ss.SetContentsOfCell("B1", "test");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void InvalidNameTest1()
        {
            Spreadsheet ss = new Spreadsheet(s => s == "A1", s => s, "default");
            ss.SetContentsOfCell("B", "test");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void InvalidNameTest2()
        {
            Spreadsheet ss = new Spreadsheet(s => s == "A1", s => s, "default");
            ss.SetContentsOfCell("B2B", "test");
        }

        [TestMethod]
        public void SimpleNormalizeTest()
        {
            Spreadsheet ss = new Spreadsheet(s => true, s => s.ToUpper(), "default");
            Assert.IsTrue(ss.SetContentsOfCell("b2", "test").Contains("B2"));
            Assert.IsTrue(ss.SetContentsOfCell("ab12", "test").Contains("AB12"));
        }

        [TestMethod]
        public void HasNotChangedTest()
        {
            Spreadsheet ss = new Spreadsheet();
            Assert.IsFalse(ss.Changed);
        }

        [TestMethod]
        public void HasChangedTest()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetContentsOfCell("A1", "test");
            Assert.IsTrue(ss.Changed);
        }

        [TestMethod]
        public void HasNotChangedAfterSaveTest()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetContentsOfCell("A1", "test");
            ss.Save("testFile.xml");
            Assert.IsFalse(ss.Changed);
        }

        [TestMethod]
        public void HasChangedAfterSaveTest()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetContentsOfCell("A1", "test");
            ss.Save("testFile.xml");
            ss.SetContentsOfCell("B1", "testetstset");
            Assert.IsTrue(ss.Changed);
        }

        [TestMethod]
        public void VersionIsDefaultInEmptyConstructorTest()
        {
            Spreadsheet ss = new Spreadsheet();
            Assert.AreEqual("default", ss.Version);
        }

        [TestMethod]
        public void CorrectVersionWhenDefinedTest()
        {
            Spreadsheet ss = new Spreadsheet(s => true, s => s, "420");
            Assert.AreEqual("420", ss.Version);
        }
    }


}
