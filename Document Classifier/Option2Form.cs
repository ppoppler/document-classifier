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
    public partial class Option2Form : Form
    {

        ChainHashTable table;
        public Option2Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int size = Convert.ToInt16(numericUpDown1.Value);
            table = new ChainHashTable(size);
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
    }
}
