using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CECS_328_Asignment_2
{
    public partial class Option1Form : Form
    {
        public Option1Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form1 = (Form1)Tag;
            form1.Show();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String text = textBox1.Text;
            long keyPoly = calculateKeyPolynomial(text);
            label1.Text = "String Input: " + text;
            label2.Text = "Key Polynomial p(37): " + keyPoly;

        }

        private long calculateKeyPolynomial(String data)
        {
            long key = 0;
            int[] numbers = new int[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                numbers[i] = data[i];
            }
            key = (long)keyPoly(numbers, 37);
            //Console.WriteLine("Key of " + data + ": " + key);
            return key;
        }

        public double keyPoly(int[] numbers, double x)
        {
            double result = 0;

            for (int i = numbers.Length - 1; i >= 0; i--)
            {
                result = result * x + numbers[i];
            }

            return result;
        }
    }
}
