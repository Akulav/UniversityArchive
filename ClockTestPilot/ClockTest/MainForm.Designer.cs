namespace ClockTest
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
            this.canvas = new System.Windows.Forms.PictureBox();
            this.startBtn = new System.Windows.Forms.Button();
            this.qstLabel = new System.Windows.Forms.Label();
            this.answerBox = new System.Windows.Forms.TextBox();
            this.answerBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.superEasyBtn = new System.Windows.Forms.Button();
            this.easyBtn = new System.Windows.Forms.Button();
            this.normalBtn = new System.Windows.Forms.Button();
            this.fastBtn = new System.Windows.Forms.Button();
            this.showBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.Location = new System.Drawing.Point(12, 12);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(900, 778);
            this.canvas.TabIndex = 0;
            this.canvas.TabStop = false;
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(918, 12);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(249, 81);
            this.startBtn.TabIndex = 1;
            this.startBtn.Text = "New Game";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // qstLabel
            // 
            this.qstLabel.AutoSize = true;
            this.qstLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.qstLabel.Location = new System.Drawing.Point(12, 802);
            this.qstLabel.Name = "qstLabel";
            this.qstLabel.Size = new System.Drawing.Size(154, 37);
            this.qstLabel.TabIndex = 2;
            this.qstLabel.Text = "Question:";
            // 
            // answerBox
            // 
            this.answerBox.Location = new System.Drawing.Point(918, 99);
            this.answerBox.Name = "answerBox";
            this.answerBox.Size = new System.Drawing.Size(249, 20);
            this.answerBox.TabIndex = 3;
            this.answerBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AnswerBox_KeyDown);
            // 
            // answerBtn
            // 
            this.answerBtn.Location = new System.Drawing.Point(918, 125);
            this.answerBtn.Name = "answerBtn";
            this.answerBtn.Size = new System.Drawing.Size(249, 27);
            this.answerBtn.TabIndex = 4;
            this.answerBtn.Text = "Answer";
            this.answerBtn.UseVisualStyleBackColor = true;
            this.answerBtn.Click += new System.EventHandler(this.AnswerBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(990, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "-------SETTINGS-------";
            // 
            // superEasyBtn
            // 
            this.superEasyBtn.Location = new System.Drawing.Point(966, 171);
            this.superEasyBtn.Name = "superEasyBtn";
            this.superEasyBtn.Size = new System.Drawing.Size(153, 23);
            this.superEasyBtn.TabIndex = 6;
            this.superEasyBtn.Text = "Speed:Super Easy";
            this.superEasyBtn.UseVisualStyleBackColor = true;
            this.superEasyBtn.Click += new System.EventHandler(this.SuperEasyBtn_Click);
            // 
            // easyBtn
            // 
            this.easyBtn.Location = new System.Drawing.Point(966, 200);
            this.easyBtn.Name = "easyBtn";
            this.easyBtn.Size = new System.Drawing.Size(153, 23);
            this.easyBtn.TabIndex = 7;
            this.easyBtn.Text = "Speed: Easy";
            this.easyBtn.UseVisualStyleBackColor = true;
            this.easyBtn.Click += new System.EventHandler(this.EasyBtn_Click);
            // 
            // normalBtn
            // 
            this.normalBtn.Location = new System.Drawing.Point(966, 229);
            this.normalBtn.Name = "normalBtn";
            this.normalBtn.Size = new System.Drawing.Size(153, 23);
            this.normalBtn.TabIndex = 8;
            this.normalBtn.Text = "Speed: Standart";
            this.normalBtn.UseVisualStyleBackColor = true;
            this.normalBtn.Click += new System.EventHandler(this.NormalBtn_Click);
            // 
            // fastBtn
            // 
            this.fastBtn.Location = new System.Drawing.Point(966, 258);
            this.fastBtn.Name = "fastBtn";
            this.fastBtn.Size = new System.Drawing.Size(153, 23);
            this.fastBtn.TabIndex = 9;
            this.fastBtn.Text = "Speed: Fast";
            this.fastBtn.UseVisualStyleBackColor = true;
            this.fastBtn.Click += new System.EventHandler(this.FastBtn_Click);
            // 
            // showBtn
            // 
            this.showBtn.Location = new System.Drawing.Point(918, 287);
            this.showBtn.Name = "showBtn";
            this.showBtn.Size = new System.Drawing.Size(249, 33);
            this.showBtn.TabIndex = 10;
            this.showBtn.Text = "Show Again";
            this.showBtn.UseVisualStyleBackColor = true;
            this.showBtn.Click += new System.EventHandler(this.ShowBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1175, 866);
            this.Controls.Add(this.showBtn);
            this.Controls.Add(this.fastBtn);
            this.Controls.Add(this.normalBtn);
            this.Controls.Add(this.easyBtn);
            this.Controls.Add(this.superEasyBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.answerBtn);
            this.Controls.Add(this.answerBox);
            this.Controls.Add(this.qstLabel);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.canvas);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Michelle = Puiu ♥";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Label qstLabel;
        private System.Windows.Forms.TextBox answerBox;
        private System.Windows.Forms.Button answerBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button superEasyBtn;
        private System.Windows.Forms.Button easyBtn;
        private System.Windows.Forms.Button normalBtn;
        private System.Windows.Forms.Button fastBtn;
        private System.Windows.Forms.Button showBtn;
    }
}

