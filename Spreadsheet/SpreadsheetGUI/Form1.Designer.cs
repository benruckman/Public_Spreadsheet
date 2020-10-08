﻿namespace SpreadsheetGUI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.spreadsheetPanel = new SS.SpreadsheetPanel();
            this.CellNameBox = new System.Windows.Forms.TextBox();
            this.CellContentBox = new System.Windows.Forms.TextBox();
            this.CellValueBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // spreadsheetPanel
            // 
            this.spreadsheetPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spreadsheetPanel.Location = new System.Drawing.Point(12, 68);
            this.spreadsheetPanel.Name = "spreadsheetPanel";
            this.spreadsheetPanel.Size = new System.Drawing.Size(776, 370);
            this.spreadsheetPanel.TabIndex = 0;
            // 
            // CellNameBox
            // 
            this.CellNameBox.BackColor = System.Drawing.SystemColors.Info;
            this.CellNameBox.Location = new System.Drawing.Point(44, 42);
            this.CellNameBox.Name = "CellNameBox";
            this.CellNameBox.ReadOnly = true;
            this.CellNameBox.Size = new System.Drawing.Size(28, 20);
            this.CellNameBox.TabIndex = 1;
            // 
            // CellContentBox
            // 
            this.CellContentBox.BackColor = System.Drawing.SystemColors.Info;
            this.CellContentBox.Location = new System.Drawing.Point(78, 42);
            this.CellContentBox.Name = "CellContentBox";
            this.CellContentBox.ReadOnly = true;
            this.CellContentBox.Size = new System.Drawing.Size(115, 20);
            this.CellContentBox.TabIndex = 2;
            // 
            // CellValueBox
            // 
            this.CellValueBox.BackColor = System.Drawing.SystemColors.Info;
            this.CellValueBox.Location = new System.Drawing.Point(199, 42);
            this.CellValueBox.Name = "CellValueBox";
            this.CellValueBox.Size = new System.Drawing.Size(244, 20);
            this.CellValueBox.TabIndex = 3;
            this.CellValueBox.TextChanged += new System.EventHandler(this.CellValueBox_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.CellValueBox);
            this.Controls.Add(this.CellContentBox);
            this.Controls.Add(this.CellNameBox);
            this.Controls.Add(this.spreadsheetPanel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SS.SpreadsheetPanel spreadsheetPanel;
        private System.Windows.Forms.TextBox CellNameBox;
        private System.Windows.Forms.TextBox CellContentBox;
        private System.Windows.Forms.TextBox CellValueBox;
    }
}

