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
            ss.SetCellContents("3f", new Formula("2 + 2", s => s, s => true));
        }

        //DELETE ME ONCE FINISIHED
        [TestMethod]
        public void test ()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetCellContents("A1", new Formula("B1 + 2", s => s, s => true));
        }
    }

    
}
