﻿
using SS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpreadsheetGUI
{
    class SpreadsheetController
    {
        Spreadsheet ss;
        public SpreadsheetController()
        {
            ss = new Spreadsheet();
        }

        /// <summary>
        /// controller method to handle the event of seleciton being changed in SpreadsheetPanel
        /// </summary>
        /// <param name="ssp"></param>
        /// <param name="cellNameBox"></param>
        /// <param name="cellContentBox"></param>
        /// <param name="cellValueBox"></param>
        public void OnSelectionChanged(SpreadsheetPanel ssp, TextBox cellNameBox, TextBox cellContentBox, TextBox cellValueBox)
        {
            cellContentBox.Clear();
            cellContentBox.Focus();
            ssp.GetSelection(out int col, out int row);
            cellNameBox.Text = convertIntToName(col, row);
            cellValueBox.Text = ss.GetCellValue(convertIntToName(col, row)).ToString();
            if (ss.GetCellContents(convertIntToName(col, row)) is double || ss.GetCellContents(convertIntToName(col, row)) is string)
            {
                cellContentBox.Text = ss.GetCellContents(convertIntToName(col, row)).ToString();
            }
            else
            {
                cellContentBox.Text = "=" + ss.GetCellContents(convertIntToName(col, row)).ToString();
            }

        }

        /// <summary>
        /// controller method to handle when the contents of a cell have been changed
        /// </summary>
        /// <param name="ssp"></param>
        /// <param name="cellContentBox"></param>
        /// <param name="cellValueBox"></param>
        public void OnContentsChanged(SpreadsheetPanel ssp, TextBox cellContentBox, TextBox cellValueBox)
        {
            ssp.GetSelection(out int col, out int row);
            IList<string> updatedValueList = ss.SetContentsOfCell(convertIntToName(col, row), cellContentBox.Text);
            int updateCol;
            int updateRow;
            foreach (string name in updatedValueList)
            {
                convertNameToInt(out updateCol, out updateRow, name);
                ssp.SetValue(updateCol, updateRow, ss.GetCellValue(name).ToString());
            }
            cellValueBox.Text = ss.GetCellValue(convertIntToName(col, row)).ToString();
        }

        /// <summary>
        /// helper method to convert a spreadsheet panel coordinate from int values to a cell name as a string
        /// NOTE: this method should handle incrementing them, provide the row and col as they are given by spreadsheetPanel
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private string convertIntToName(int col, int row)
        {
            return "" + ((char)(col + 65)).ToString() + (row + 1).ToString();
        }

        /// <summary>
        /// helper method to convert a cell name to 0 based spreadsheet panel coordinates
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <param name="name"></param>
        private void convertNameToInt(out int col, out int row, string name)
        {
            string letter = name.Substring(0, 1);
            letter = letter.ToUpper();
            col = char.Parse(letter) - 'A';

            row = int.Parse(name.Substring(1)) - 1;
        }




        public void OpenFileButtonHandler()
        {
            if (ss.Changed)
                SaveChangeErrorMessageHelper();
        }


        public void SaveFileButtonHandler()
        {
            ss.Save("ps6");//To implement to get the filename to save down as.
        }


        public void NewFileButtonHandler()
        {
            if (ss.Changed)
                SaveChangeErrorMessageHelper();
        }

        public void QuitFileButtonHandler()
        {
            if (ss.Changed)
                SaveChangeErrorMessageHelper();
            Quit();
        }

        private void SaveChangeErrorMessageHelper()
        {
            DialogResult res = MessageBox.Show("Your data isn't saved, if you quit you will lose your changes", "Changes not saved", MessageBoxButtons.OKCancel);
            if (res.ToString().Equals("OK"))
            {
                Quit();
            }
        }

        private void Quit()
        {
            
            /*throw new NotImplementedException();*/
        }
    }
}
