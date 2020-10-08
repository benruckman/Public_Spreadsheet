using SS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpreadsheetGUI
{
    public partial class Form1 : Form
    {
        SpreadsheetController controller;

        public Form1()
        {
            InitializeComponent();

            AcceptButton = CalculateValuesButton;

            controller = new SpreadsheetController();

            spreadsheetPanel.SelectionChanged += selectionChanged;

        }

        public void selectionChanged(SpreadsheetPanel ssp)
        {
            controller.OnSelectionChanged(ssp, cellNameBox, CellContentBox, CellValueBox);
        }

        private void CalculateValuesButton_Click(object sender, EventArgs e)
        {
            controller.OnContentsChanged(spreadsheetPanel, CellContentBox, CellValueBox);
        }
    }
}
