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
    public partial class Option3Form : Form
    {

        QuadProbeHashTable table;
        public Option3Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int size = Convert.ToInt16(numericUpDown1.Value);
            table = new QuadProbeHashTable(size);
            button1.Enabled = false;
            numericUpDown1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            table.insert(textBox1.Text);
            textBox1.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            numericUpDown1.Enabled = true;
            label4.Text = "Hash Table Contents: \n" + table.print();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            var form1 = (Form1)Tag;
            form1.Show();
            Close();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (IsPrime((int)numericUpDown1.Value))
                button1.Enabled = true;
            else
                button1.Enabled = false;

        }

        private bool IsPrime(int number)
        {
            if (number == 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;
            var bound = (int)Math.Floor(Math.Sqrt(number));
            for (int i = 3; i <= bound; i += 2)
            {
                if (number % i == 0) return false;
            }
            return true;
        }
    }
}
