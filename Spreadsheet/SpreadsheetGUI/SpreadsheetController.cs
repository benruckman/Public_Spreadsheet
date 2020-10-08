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
        public SpreadsheetController ()
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
        public void OnSelectionChanged (SpreadsheetPanel ssp, TextBox cellNameBox, TextBox cellContentBox, TextBox cellValueBox)
        {
            cellContentBox.Clear();
            cellContentBox.Focus();
            ssp.GetSelection(out int col, out int row);
            cellNameBox.Text = convertIntToName(col, row);
        }

        /// <summary>
        /// controller method to handle when the contents of a cell have been changed
        /// </summary>
        /// <param name="ssp"></param>
        /// <param name="cellContentBox"></param>
        /// <param name="cellValueBox"></param>
        public void OnContentsChanged (SpreadsheetPanel ssp, TextBox cellContentBox, TextBox cellValueBox)
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
        }

        /// <summary>
        /// helper method to convert a spreadsheet panel coordinate from int values to a cell name as a string
        /// NOTE: this method should handle incrementing them, provide the row and col as they are given by spreadsheetPanel
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private string convertIntToName (int col, int row)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// helper method to convert a cell name to 0 based spreadsheet panel coordinates
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <param name="name"></param>
        private void convertNameToInt (out int col, out int row, string name)
        {
            throw new NotImplementedException();
        }
         

    }
}
