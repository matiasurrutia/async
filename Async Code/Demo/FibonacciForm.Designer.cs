namespace Responsivenes
{
    partial class FibonacciForm
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
            this.cmdCalculate = new System.Windows.Forms.Button();
            this.nText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.resultsText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cmdCalculate
            // 
            this.cmdCalculate.Location = new System.Drawing.Point(37, 48);
            this.cmdCalculate.Name = "cmdCalculate";
            this.cmdCalculate.Size = new System.Drawing.Size(75, 23);
            this.cmdCalculate.TabIndex = 0;
            this.cmdCalculate.Text = "Calculate";
            this.cmdCalculate.UseVisualStyleBackColor = true;
            this.cmdCalculate.Click += new System.EventHandler(this.CalculateClick);
            // 
            // nText
            // 
            this.nText.Location = new System.Drawing.Point(37, 22);
            this.nText.Name = "nText";
            this.nText.Size = new System.Drawing.Size(75, 20);
            this.nText.TabIndex = 1;
            this.nText.Text = "40";
            this.nText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "N";
            // 
            // resultsText
            // 
            this.resultsText.Location = new System.Drawing.Point(140, 22);
            this.resultsText.Multiline = true;
            this.resultsText.Name = "resultsText";
            this.resultsText.Size = new System.Drawing.Size(224, 216);
            this.resultsText.TabIndex = 3;
            // 
            // FibonacciForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 261);
            this.Controls.Add(this.resultsText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nText);
            this.Controls.Add(this.cmdCalculate);
            this.Name = "FibonacciForm";
            this.Text = "Fibonacci";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdCalculate;
        private System.Windows.Forms.TextBox nText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox resultsText;
    }
}

