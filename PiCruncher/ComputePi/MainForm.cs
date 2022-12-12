using System;
using System.Windows.Forms;

namespace ComputePi
{


    public partial class MainForm : Form
    {

        int digit = 0;
        int progress = 0;
        string final;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public static string CalculatePi(int digits)
        {
            digits++;

            uint[] x = new uint[digits * 10 / 3 + 2];
            uint[] r = new uint[digits * 10 / 3 + 2];

            uint[] pi = new uint[digits];

            for (int j = 0; j < x.Length; j++)
                x[j] = 20;

            for (int i = 0; i < digits; i++)
            {
                uint carry = 0;
                for (int j = 0; j < x.Length; j++)
                {
                    uint num = (uint)(x.Length - j - 1);
                    uint dem = num * 2 + 1;

                    x[j] += carry;

                    uint q = x[j] / dem;
                    r[j] = x[j] % dem;

                    carry = q * num;

                }

                pi[i] = (x[x.Length - 1] / 10);


                r[x.Length - 1] = x[x.Length - 1] % 10; ;

                for (int j = 0; j < x.Length; j++)
                    x[j] = r[j] * 10;
            }

            var result = "";

            uint c = 0;

            for (int i = pi.Length - 1; i >= 0; i--)
            {
                pi[i] += c;
                c = pi[i] / 10;

                result = (pi[i] % 10).ToString() + result;

            }

            return result;
        }


        private void Compute_Click(object sender, EventArgs e)
        {

            if (digit > 10)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                final = CalculatePi(digit);
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                final = final.Substring(final.Length - 10);
                ResultLabel.Text = (elapsedMs/1000).ToString();
                Result.Text = final;
                
            }

            else {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                final = CalculatePi(digit);
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                ResultLabel.Text = (elapsedMs/1000).ToString();
                Result.Text = final;

            }      
        }

        private void NoOfDigits_TextChanged(object sender, EventArgs e)
        {
            digit = Convert.ToInt32(NoOfDigits.Text);
        }
    }
}
