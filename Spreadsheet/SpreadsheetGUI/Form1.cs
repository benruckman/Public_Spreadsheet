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

        private void OpenFileMenuButton_Click(object sender, EventArgs e)
        {
            controller.OpenFileButtonHandler(spreadsheetPanel);
        }

        private void SaveFileMenuButton_Click(object sender, EventArgs e)
        {
            controller.SaveFileButtonHandler();
        }

        private void NewFileMenuButton_Click(object sender, EventArgs e)
        {
            DemoApplicationContext.getAppContext().RunForm(new Form1());
        }

        private void QuitMenuButton_Click(object sender, EventArgs e)
        {
            if (controller.QuitFileButtonHandler())
                Close();
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {

        }
    }
}
