namespace ComputePi
{
    partial class MainForm
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
            this.Compute = new System.Windows.Forms.Button();
            this.NoOfDigits = new System.Windows.Forms.TextBox();
            this.Instruction = new System.Windows.Forms.Label();
            this.ResultLabel = new System.Windows.Forms.Label();
            this.Result = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Compute
            // 
            this.Compute.Location = new System.Drawing.Point(12, 40);
            this.Compute.Name = "Compute";
            this.Compute.Size = new System.Drawing.Size(430, 32);
            this.Compute.TabIndex = 0;
            this.Compute.Text = "Compute";
            this.Compute.UseVisualStyleBackColor = true;
            this.Compute.Click += new System.EventHandler(this.Compute_Click);
            // 
            // NoOfDigits
            // 
            this.NoOfDigits.Location = new System.Drawing.Point(344, 12);
            this.NoOfDigits.Name = "NoOfDigits";
            this.NoOfDigits.Size = new System.Drawing.Size(98, 20);
            this.NoOfDigits.TabIndex = 1;
            this.NoOfDigits.TextChanged += new System.EventHandler(this.NoOfDigits_TextChanged);
            // 
            // Instruction
            // 
            this.Instruction.AutoSize = true;
            this.Instruction.Location = new System.Drawing.Point(9, 15);
            this.Instruction.Name = "Instruction";
            this.Instruction.Size = new System.Drawing.Size(329, 13);
            this.Instruction.TabIndex = 3;
            this.Instruction.Text = "Please Enter the number of digits up to which you want pi calculated";
            // 
            // ResultLabel
            // 
            this.ResultLabel.AutoSize = true;
            this.ResultLabel.Location = new System.Drawing.Point(12, 85);
            this.ResultLabel.Name = "ResultLabel";
            this.ResultLabel.Size = new System.Drawing.Size(40, 13);
            this.ResultLabel.TabIndex = 5;
            this.ResultLabel.Text = "Result:";
            // 
            // Result
            // 
            this.Result.Location = new System.Drawing.Point(69, 78);
            this.Result.Multiline = true;
            this.Result.Name = "Result";
            this.Result.Size = new System.Drawing.Size(373, 20);
            this.Result.TabIndex = 6;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 111);
            this.Controls.Add(this.Result);
            this.Controls.Add(this.ResultLabel);
            this.Controls.Add(this.Instruction);
            this.Controls.Add(this.NoOfDigits);
            this.Controls.Add(this.Compute);
            this.Name = "MainForm";
            this.Text = "Compute Pi - By Caty";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Compute;
        private System.Windows.Forms.TextBox NoOfDigits;
        private System.Windows.Forms.Label Instruction;
        private System.Windows.Forms.Label ResultLabel;
        private System.Windows.Forms.TextBox Result;
    }
}

