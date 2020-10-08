namespace SpreadsheetGUI
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
            this.CellNameBox = new System.Windows.Forms.TextBox();
            this.CellValueBox = new System.Windows.Forms.TextBox();
            this.CellContentsBox = new System.Windows.Forms.TextBox();
            this.spreadsheetPanel = new SS.SpreadsheetPanel();
            this.CellNameLabel = new System.Windows.Forms.Label();
            this.CellContentsBoxLabel = new System.Windows.Forms.Label();
            this.CellValueBoxLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CellNameBox
            // 
            this.CellNameBox.BackColor = System.Drawing.SystemColors.Info;
            this.CellNameBox.Location = new System.Drawing.Point(28, 42);
            this.CellNameBox.Name = "CellNameBox";
            this.CellNameBox.ReadOnly = true;
            this.CellNameBox.Size = new System.Drawing.Size(28, 20);
            this.CellNameBox.TabIndex = 1;
            // 
            // CellValueBox
            // 
            this.CellValueBox.BackColor = System.Drawing.SystemColors.Info;
            this.CellValueBox.Location = new System.Drawing.Point(78, 42);
            this.CellValueBox.Name = "CellValueBox";
            this.CellValueBox.ReadOnly = true;
            this.CellValueBox.Size = new System.Drawing.Size(115, 20);
            this.CellValueBox.TabIndex = 2;
            // 
            // CellContentsBox
            // 
            this.CellContentsBox.BackColor = System.Drawing.SystemColors.Info;
            this.CellContentsBox.Location = new System.Drawing.Point(199, 42);
            this.CellContentsBox.Name = "CellContentsBox";
            this.CellContentsBox.Size = new System.Drawing.Size(244, 20);
            this.CellContentsBox.TabIndex = 3;
            this.CellContentsBox.TextChanged += new System.EventHandler(this.CellContentBox_TextChanged);
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
            // CellNameLabel
            // 
            this.CellNameLabel.AutoSize = true;
            this.CellNameLabel.Location = new System.Drawing.Point(12, 26);
            this.CellNameLabel.Name = "CellNameLabel";
            this.CellNameLabel.Size = new System.Drawing.Size(55, 13);
            this.CellNameLabel.TabIndex = 4;
            this.CellNameLabel.Text = "Cell Name";
            // 
            // CellContentsBoxLabel
            // 
            this.CellContentsBoxLabel.AutoSize = true;
            this.CellContentsBoxLabel.Location = new System.Drawing.Point(266, 26);
            this.CellContentsBoxLabel.Name = "CellContentsBoxLabel";
            this.CellContentsBoxLabel.Size = new System.Drawing.Size(90, 13);
            this.CellContentsBoxLabel.TabIndex = 5;
            this.CellContentsBoxLabel.Text = "Cell Contents Box";
            // 
            // CellValueBoxLabel
            // 
            this.CellValueBoxLabel.AutoSize = true;
            this.CellValueBoxLabel.Location = new System.Drawing.Point(97, 26);
            this.CellValueBoxLabel.Name = "CellValueBoxLabel";
            this.CellValueBoxLabel.Size = new System.Drawing.Size(75, 13);
            this.CellValueBoxLabel.TabIndex = 6;
            this.CellValueBoxLabel.Text = "Cell Value Box";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.CellValueBoxLabel);
            this.Controls.Add(this.CellContentsBoxLabel);
            this.Controls.Add(this.CellNameLabel);
            this.Controls.Add(this.CellContentsBox);
            this.Controls.Add(this.CellValueBox);
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
        private System.Windows.Forms.TextBox CellValueBox;
        private System.Windows.Forms.TextBox CellContentsBox;
        private System.Windows.Forms.Label CellNameLabel;
        private System.Windows.Forms.Label CellContentsBoxLabel;
        private System.Windows.Forms.Label CellValueBoxLabel;
    }
}

