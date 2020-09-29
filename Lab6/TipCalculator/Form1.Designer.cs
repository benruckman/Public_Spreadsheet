namespace TipCalculator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.totalBillLabel = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.calculateTipLabel = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // totalBillLabel
            // 
            this.totalBillLabel.Location = new System.Drawing.Point(127, 80);
            this.totalBillLabel.Name = "totalBillLabel";
            this.totalBillLabel.Size = new System.Drawing.Size(92, 23);
            this.totalBillLabel.TabIndex = 0;
            this.totalBillLabel.Text = "Enter Total Bill";
            this.totalBillLabel.UseVisualStyleBackColor = true;
            this.totalBillLabel.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(225, 79);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 23);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // calculateTipLabel
            // 
            this.calculateTipLabel.Location = new System.Drawing.Point(127, 119);
            this.calculateTipLabel.Name = "calculateTipLabel";
            this.calculateTipLabel.Size = new System.Drawing.Size(92, 23);
            this.calculateTipLabel.TabIndex = 2;
            this.calculateTipLabel.Text = "Caculate Tip";
            this.calculateTipLabel.UseVisualStyleBackColor = true;
            this.calculateTipLabel.Click += new System.EventHandler(this.calculateTipLabel_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(225, 118);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 23);
            this.textBox2.TabIndex = 3;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(225, 50);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 23);
            this.textBox3.TabIndex = 4;
            this.textBox3.Text = "Enter Tip %";
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.calculateTipLabel);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.totalBillLabel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button totalBillLabel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button calculateTipLabel;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
    }
}

