using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SS;
using SpreadsheetGUI;

namespace SpreadsheetGUI
{
    public partial class Form1 : Form
    {
        Spreadsheet ss = new Spreadsheet();

        public Form1()
        {
            InitializeComponent();
        }

        private void CellContentsBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void spreadsheetPanel1_MouseClick(object sender, MouseEventArgs e)
        {

            OnMouseClick(e);
            int col;
            int row;
            /*GetSelection(out col, out row);*/
        }
    }
}
