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

        private void HelpButton_Click(object sender, EventArgs e)
        {
            controller.HelpButtonHandler();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

        }


        //This code is inspired by stackOverflow
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Up)
            {
                controller.createColumnSum(spreadsheetPanel, CellContentBox, CellValueBox);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
