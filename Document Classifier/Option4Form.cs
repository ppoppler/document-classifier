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
    public partial class Option4Form : Form
    {

        string[] lines;
        String[] chars;
        List<String> strings = new List<String>();
        List<String> totalStrings = new List<String>();
        Dictionary<string,int> StringDict;
        public Option4Form()
        {
            chars = new String[] { ".", ",", "!", "@", "#", "$", "%", "^", "&", "*", "/", "\\", "(", ")", "?", "=", "-", "’", " = ", "_", "+", ";", ":", "—", "\"", "“", "”", "|", "[", "]", ">", "<", "?" };
            InitializeComponent();
            openFileDialog1.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            string fileName;
            openFileDialog1.ShowDialog();
            fileName = openFileDialog1.FileName;
            textBox1.Text = fileName;
            //Code for Part 4: 
            readFile(fileName);
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var form1 = (Form1)Tag;
            form1.Show();
            Close();

        }

        private void readFile(String fileName)
        {
            strings = new List<String>();
            lines = System.IO.File.ReadAllLines(fileName);
            for (int i = 0; i < lines.Length; i++)
            {
                foreach (var c in chars)
                {
                    lines[i] = lines[i].Replace(c, " ");
                    lines[i] = lines[i].ToLower();

                }
                 strings.AddRange(lines[i].Split(null).Where(s => !string.IsNullOrWhiteSpace(s)));
                //Console.WriteLine(lines[i]);
            }

            for (int i = 0; i < lines.Length; i++)
            {
                strings.AddRange(lines[i].Split(null).Where(s => !string.IsNullOrWhiteSpace(s)));
            }
            //Console.WriteLine(strings.Count);
            foreach (var str in strings)
            {
                //Console.Write(str + " ");
            }
            var CountQuery = from x in strings
                             group x by x into g
                             let count = g.Count()
                             orderby count descending
                             select new { Value = g.Key, Count = count };
            StringDict = new Dictionary<string, int>();
            foreach (var x in CountQuery)
            {
                //Console.WriteLine("V: " + x.Value + "  K: " + x.Count);
                StringDict.Add(x.Value, x.Count);
            }
            totalStrings.AddRange(strings);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StringBuilder str1 = new StringBuilder();
            textBox2.Text = "";
            foreach (var str in strings) {
                str1.Append(str + " ");
            }
            textBox2.Text += str1;

        }
    }
}
