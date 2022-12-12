using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace ClockTest
{

    public partial class MainForm : Form
    {
        public static readonly Timer TimeoutTimer = new Timer();
        string answerstring = "";
        public static string questionText = "";
        public static int data;
        int time = 6000;
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void StartBtn_Click(object sender, EventArgs e)
        {

            int data = GenRandQuestion();
            Message.AutoCloseMsb(questionText, "Question", 3000);

            canvas.Show();

            answerBox.Text = "";
            try
            {
                TimeoutTimer.Stop();
            }
            catch
            {

            }
            var bitmap = new Bitmap(canvas.Width, canvas.Height);
            //Graphics myCanvas = canvas.CreateGraphics();
            var myCanvas = Graphics.FromImage(bitmap);
            myCanvas.Clear(Color.White);





            string[] files = Directory.GetFiles(@"Images");
            DirectoryInfo di = new DirectoryInfo(@"Images");
            FileInfo[] filenames = di.GetFiles("*.png");

            int[] number = new int[6] {
                GenRandNumber(),
                GenRandNumber(),
                GenRandNumber(),
                GenRandNumber(),
                GenRandNumber(),
                GenRandNumber()
            };

            Image i = Image.FromFile(files[number[0]]);
            Image i2 = Image.FromFile(files[number[1]]);
            Image i3 = Image.FromFile(files[number[2]]);
            Image i4 = Image.FromFile(files[number[3]]);
            Image i5 = Image.FromFile(files[number[4]]);
            Image i6 = Image.FromFile(files[number[5]]);


            //
            myCanvas.DrawImage(i, 50, 50);
            myCanvas.DrawImage(i2, 250, 50);
            myCanvas.DrawImage(i3, 450, 50);
            myCanvas.DrawImage(i4, 150, 250);
            myCanvas.DrawImage(i5, 350, 250);
            myCanvas.DrawImage(i6, 250, 450);

            answerstring = GenerateAnswer(data, filenames, number);


            canvas.Image = bitmap;

            // Timer

            TimeoutTimer.Interval = time;
            TimeoutTimer.Tick += new EventHandler(Timeout_Tick);
            TimeoutTimer.Start();
        }

        private void Timeout_Tick(object sender, EventArgs e)
        {
            Invoke(new Action(() =>
            {
                canvas.Hide();
                TimeoutTimer.Stop();
            }));
        }



        private string GenerateAnswer(int questionID, FileInfo[] files, int[] number)
        {
            string answerfull = "";
            switch (questionID)
            {
                case 0:
                    for (int i = 0; i < 6; i++)
                    {
                        if (files[number[i]].Name[0] == '0')
                        {
                            answerfull += files[number[i]].Name[2];
                        }

                        else
                        {
                            answerfull += '0';
                        }
                    }
                    break;

                case 1:
                    for (int i = 0; i < 6; i++)
                    {
                        if (files[number[i]].Name[0] == '0' && files[number[i]].Name[1] == '0')
                        {
                            answerfull += files[number[i]].Name[2];
                        }
                        else
                        {
                            answerfull += '0';
                        }
                    }
                    break;

                case 2:
                    for (int i = 0; i < 6; i++)
                    {
                        if (files[number[i]].Name[0] == '0' && files[number[i]].Name[1] == '1')
                        {
                            answerfull += files[number[i]].Name[2];
                        }
                        else
                        {
                            answerfull += '0';
                        }
                    }
                    break;

                case 3:
                    for (int i = 0; i < 6; i++)
                    {
                        if (files[number[i]].Name[0] == '1')
                        {
                            answerfull += files[number[i]].Name[2];
                        }

                        else
                        {
                            answerfull += '0';
                        }
                    }
                    break;

                case 4:
                    for (int i = 0; i < 6; i++)
                    {
                        if (files[number[i]].Name[0] == '1' && files[number[i]].Name[1] == '0')
                        {
                            answerfull += files[number[i]].Name[2];
                        }
                        else
                        {
                            answerfull += '0';
                        }
                    }
                    break;

                case 5:
                    for (int i = 0; i < 6; i++)
                    {
                        if (files[number[i]].Name[0] == '1' && files[number[i]].Name[1] == '1')
                        {
                            answerfull += files[number[i]].Name[2];
                        }
                        else
                        {
                            answerfull += '0';
                        }
                    }
                    break;


            }

            return answerfull;
        }

        private int GenRandNumber()
        {
            Random random = new Random();
            Thread.Sleep(2);
            return random.Next(40);
        }

        private int GenRandQuestion()
        {
            //0 is white, 1 is white circle, 2 is white square, 3 is red, 4 is red circle, 5 is red square
            Random rand = new Random();
            int result = rand.Next(6);

            switch (result)
            {
                case 0:
                    //qstLabel.Text = "Calculate white instruments";
                    questionText = "Calculate white instruments";
                    break;
                case 1:
                    //qstLabel.Text = "Calculate white instruments with circles";
                    questionText = "Calculate white instruments with circles";
                    break;
                case 2:
                    //qstLabel.Text = "Calculate white instruments with squares";
                    questionText = "Calculate white instruments with squares";
                    break;
                case 3:
                    //qstLabel.Text = "Calculate red instruments";
                    questionText = "Calculate red instruments";
                    break;
                case 4:
                    //qstLabel.Text = "Calculate red instruments with circles";
                    questionText = "Calculate red instruments with circles";
                    break;
                case 5:
                    //qstLabel.Text = "Calculate red instruments with squares";
                    questionText = "Calculate red instruments with squares";
                    break;
            }
            return result;
        }

        private void AnswerBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Correct answer is: " + answerstring);
            if (answerstring == answerBox.Text)
            {
                MessageBox.Show("Correct. Love you.");
            }
            else
            {
                MessageBox.Show("You were wrong. I still love you :D");
            }
        }

        private void SuperEasyBtn_Click(object sender, EventArgs e)
        {
            time = 10000;
        }

        private void EasyBtn_Click(object sender, EventArgs e)
        {
            time = 8000;
        }

        private void NormalBtn_Click(object sender, EventArgs e)
        {
            time = 6000;
        }

        private void FastBtn_Click(object sender, EventArgs e)
        {
            time = 4000;
        }

        private void ShowBtn_Click(object sender, EventArgs e)
        {
            canvas.Show();
        }

        private void AnswerBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AnswerBtn_Click(new object(), new EventArgs());
            }
        }
    }
}


/*
 //insidde

            /*
            int[] colors = new int[6] {
                GenRandColor(),
                GenRandColor(),
                GenRandColor(),
                GenRandColor(),
                GenRandColor(),
                GenRandColor()
            };

            int[] number = new int[6] {
                GenRandNumber(),
                GenRandNumber(),
                GenRandNumber(),
                GenRandNumber(),
                GenRandNumber(),
                GenRandNumber()
            };

            int[] shape = new int[6] {
                GenRandShape(),
                GenRandShape(),
                GenRandShape(),
                GenRandShape(),
                GenRandShape(),
                GenRandShape()
            };

            circle(50, 50, 100, 100, myCanvas, colors[0], shape[0], number[0]);
            circle(150, 50, 100, 100, myCanvas, colors[1], shape[1], number[1]);
            circle(250, 50, 100, 100, myCanvas, colors[2], shape[2], number[2]);
            circle(100, 140, 100, 100, myCanvas, colors[3], shape[3], number[3]);
            circle(200, 140, 100, 100, myCanvas, colors[4], shape[4], number[4]);
            circle(150, 230, 100, 100, myCanvas, colors[5], shape[5], number[5]);
            
//


private int GenRandShape()
        {
            //1 is square, 0 is circle
            Random random = new Random();
            Thread.Sleep(1);
            return random.Next(2);
        }

        private int GenRandColor()
        {
            //1 is red, 0 is white
            Random random = new Random();
            Thread.Sleep(1);
            return random.Next(0, 2);
        }
 
         private static void circle(int x, int y, int width, int height, Graphics g, int back_color, int shape, int answer)
        {
            Thread.Sleep(1);
            Pen color = new Pen(Color.Black);
            Random rand = new Random();
            Rectangle circle = new Rectangle(x, y, width, height);
            g.DrawEllipse(color, circle);

            //Draw the color of the clock
            if (back_color == 1)
            {
                SolidBrush solidBrush = new SolidBrush(Color.FromArgb(200, 240, 25, 25));
                g.FillEllipse(solidBrush, circle);
            }

            else if (back_color == 0)
            {
                SolidBrush solidBrush = new SolidBrush(Color.FromArgb(0, 255, 55, 55));
                g.FillEllipse(solidBrush, circle);
            }

            //Draw the shape of the clock
            if (shape == 1)
            {
                Rectangle rect = new Rectangle(x + 42, y + 45, 15, 15);
                g.DrawRectangle(color, rect);
                SolidBrush solidBrush = new SolidBrush(Color.FromArgb(255, 0, 0, 0));
                g.FillRectangle(solidBrush, rect);
            }

            else if (shape == 0)
            {
                Rectangle internal_circle = new Rectangle(x + 42, y + 45, 15, 15);
                g.DrawEllipse(color, internal_circle);
                SolidBrush solidBrush = new SolidBrush(Color.FromArgb(255, 0, 0, 0));
                g.FillEllipse(solidBrush, internal_circle);
            }

            //Draw the number on it
            Font myFont = new Font("Arial", 10, FontStyle.Bold);
            int left = rand.Next(0, 2);
            Thread.Sleep(1);
            int up = rand.Next(0, 2);
            Thread.Sleep(1);
            int transposition = rand.Next(1, 4);
            Thread.Sleep(1);
            int drawNmbr = rand.Next(1, 5);
            SolidBrush nmbrBrush = new SolidBrush(Color.FromArgb(255, 0, 0, 0));
            Thread.Sleep(1);
            int position = rand.Next(0, 8);

            //MessageBox.Show(left.ToString() + " " + up.ToString());
            //MessageBox.Show(drawNmbr.ToString());
            switch (position)
            {
                case 0:
                    g.DrawString(drawNmbr.ToString(), myFont, nmbrBrush, x + 45, y - 1);
                    //MessageBox.Show(drawNmbr.ToString() + "0");
                    break;
                case 1:
                    g.DrawString(drawNmbr.ToString(), myFont, nmbrBrush, x + 65, y + 10);
                    //MessageBox.Show(drawNmbr.ToString() + "1");
                    break;
                case 2:
                    g.DrawString(drawNmbr.ToString(), myFont, nmbrBrush, x + 85, y + 40);
                    //MessageBox.Show(drawNmbr.ToString() + "2");
                    break;
                case 3:
                    g.DrawString(drawNmbr.ToString(), myFont, nmbrBrush, x + 65, y + 85);
                    //MessageBox.Show(drawNmbr.ToString() + "3");
                    break;
                case 4:
                    g.DrawString(drawNmbr.ToString(), myFont, nmbrBrush, x + 45, y + 85);
                    //MessageBox.Show(drawNmbr.ToString() + "4");
                    break;
                case 5:
                    g.DrawString(drawNmbr.ToString(), myFont, nmbrBrush, x + 6, y + 65);
                    //MessageBox.Show(drawNmbr.ToString() + "5");
                    break;
                case 6:
                    g.DrawString(drawNmbr.ToString(), myFont, nmbrBrush, x, y + 45);
                    //MessageBox.Show(drawNmbr.ToString() + "6");
                    break;
                case 7:
                    g.DrawString(drawNmbr.ToString(), myFont, nmbrBrush, x + 15, y + 10);
                    //MessageBox.Show(drawNmbr.ToString() + "7");
                    break;
            }
            //Draw the lines
            Pen colorPen = new Pen(Color.Blue, 2);
            int answer_line = transposition + position;
            if (answer_line > 7)
            {
                answer_line = answer_line - 8;
            }

            switch (answer_line)
            {
                case 0:
                    g.DrawLine(colorPen, x + 45, y - 1, x + 49, y + 50);
                    break;
                case 1:
                    g.DrawLine(colorPen, x, y, x + 49, y + 50);
                    break;
                case 2:
                    g.DrawLine(colorPen, x, y, x + 49, y + 50);
                    break;
                case 3:
                    g.DrawLine(colorPen, x, y, x + 49, y + 50);
                    break;
                case 4:
                    g.DrawLine(colorPen, x, y, x + 49, y + 50);
                    break;
                case 5:
                    g.DrawLine(colorPen, x, y, x + 49, y + 50);
                    break;
                case 6:
                    g.DrawLine(colorPen, x, y, x + 49, y + 50);
                    break;
                case 7:
                    g.DrawLine(colorPen, x, y, x + 49, y + 50);
                    break;
                case 8:
                    g.DrawLine(colorPen, x, y, x + 49, y + 50);
                    break;
                case 9:
                    g.DrawLine(colorPen, x, y, x + 49, y + 50);
                    break;
            }
        }
*/
