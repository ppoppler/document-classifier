using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace CECS_328_Asignment_2
{
    public partial class Option7Form : Form
    {

        String[] lines;
        String[] chars;
        List<String> strings = new List<String>();
        List<String> totalStrings = new List<String>();
        List<String> totalStrings1 = new List<String>();
        List<Token> tokens = new List<Token>();
        double thresholdValue=0;
        Dictionary<string, double> stringCorr1 = new Dictionary<string, double>();
        Dictionary<string, double> stringCorr = new Dictionary<string, double>();
        //Dictionary <string,int> StringDict;
        //Dictionary<string, int> StringDict1;
        double AScore;
        double BScore;
        int numCorrect = 0, numIncorrect = 0;
        public Option7Form()
        {
            chars = new String[] { ".", ",", "!", "@", "#", "$", "%", "^", "&", "*","/", "\\", "(", ")", "?","=", "-", "’", " = ", "_", "+", ";", ":","—", "\"","“","”", "|", "[", "]", ">", "<", "?" };
            InitializeComponent();
            openFileDialog1.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            stringCorr1 = new Dictionary<string, double>();
            stringCorr = new Dictionary<string, double>();
            foreach(DataRow dr in Program.dt.Rows)
            {
                int rt = Convert.ToInt16(dr["A_Count"].ToString());
                int pt = Convert.ToInt16(dr["B_Count"].ToString());
                double correlation = (rt - pt) / (Math.Sqrt(rt + pt) * Math.Sqrt(1000 - rt - pt));
                stringCorr.Add(dr["Token_Name"].ToString(), correlation);
            }
            foreach (DataRow dr in Program.dt.Rows)
            {
                int rt = Convert.ToInt16(dr["A_Count"].ToString());
                int pt = Convert.ToInt16(dr["B_Count"].ToString());
                double correlation = (pt - rt) / (Math.Sqrt(pt + rt) * Math.Sqrt(1000 - pt - rt));
                stringCorr1.Add(dr["Token_Name"].ToString(), correlation);
            }
            Console.WriteLine("yes");
            string fileName;
            openFileDialog1.ShowDialog();
            fileName = openFileDialog1.FileName;
            textBox1.Text = fileName;
            readFile(fileName);
            //textBox2.Text = "";
            //string fileName;
            //openFileDialog1.ShowDialog();
            //fileName = openFileDialog1.FileName;
            //textBox1.Text = fileName;
            //Code for Part 4: 
            //readFile(fileName);
            //readFileDirectory();


        }


        private void button3_Click(object sender, EventArgs e)
        {
            AScore = 0.0;
            BScore = 0.0;
            //Descending string correlations in Group A
            var CountQuery = from x in stringCorr
                             orderby x.Value descending
                             select new { Token = x.Key, Correlation = Math.Round(x.Value, 5) };
            //Descending string correlations in Group B
            var CountQuery1 = from x in stringCorr1
                              orderby x.Value descending
                              select new { Token = x.Key, Correlation = Math.Round(x.Value, 5) };
            //Joint query between Group A and B where the words are the same.
            var joinQuery = from x in CountQuery
                        join y in CountQuery1 on x.Token equals y.Token
                        select new { Token = x.Token, A_Correlation = x.Correlation, B_Correlation = y.Correlation };
            textBox2.Text = "";
            //From the Joint Query where the correlation score from Group A is greater than the correlation score from group B
            //and join in with a list of Tokens read from the file the user inputted and receives its counts.
            var ThreshQuery1 = from x in joinQuery
                             where (x.A_Correlation > thresholdValue)
                             join y in tokens on x.Token equals y.token
                             select new { Token = x.Token, A_Correlation = x.A_Correlation, B_Correlation = x.B_Correlation,Count = y.count };
            //From the Joint Query where the correlation score from Group B is greater than the correlation score from group A
            //and join in with a list of Tokens read from the file the user inputted and receives its counts.
            var ThreshQuery2 = from x in joinQuery
                               where (x.B_Correlation > thresholdValue)
                               join y in tokens on x.Token equals y.token
                               select new { Token = x.Token, A_Correlation = x.A_Correlation, B_Correlation = x.B_Correlation, Count = y.count };
            
            //foreach(var dm in ThreshQuery1)
            //{
            //    Console.WriteLine(dm.Token);
            //}

            //For each value in the first ThreshQuery Group A, take the correaltion score and multiply it by how many times the word appears.
            foreach(var discrm in ThreshQuery1)
            {
                AScore += (discrm.A_Correlation * discrm.Count);
            }
            //For each value in the second ThreshQuery Group B, take the correaltion score and multiply it by how many time the word appears.
            foreach(var discrm in ThreshQuery2)
            {
                BScore += (discrm.B_Correlation * discrm.Count);
            }

            //Print out 
            textBox2.Text += "Religion Document Score: " + AScore + "\r\n";
            textBox2.Text += "Politics Document Score: " + BScore + "\r\n";
            if (AScore > BScore)
                textBox2.Text += "Test Document is a Religion Document\r\n";
            if (BScore > AScore)
                textBox2.Text += "Test Document is a Politics Document\r\n";
            if ((textBox1.Text.Contains("Class A") && AScore > BScore) || (textBox1.Text.Contains("Class B") && BScore > AScore)){
                numCorrect++;
                textBox2.Text += "Correctly Classified Documents: " + numCorrect + "\r\n";
                textBox2.Text += "Incorrectly Classified Documents: " + numIncorrect + "\r\n";
            }
            else
            {
                numIncorrect++;
                textBox2.Text += "Correctly Classified Documents: " + numCorrect + "\r\n";
                textBox2.Text += "Incorrectly Classified Documents: " + numIncorrect + "\r\n";
            }
            
                


        }


        private void button2_Click(object sender, EventArgs e)
        {
            //sendIntoDatabase();
            var form1 = (Form1)Tag;
            form1.Show();
            Close();

        }
        //Reads a file
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
            tokens = new List<Token>();
            foreach(var tok in CountQuery)
            {
                tokens.Add(new Token(tok.Value, tok.Count));
            }
       
        }

        private void button5_Click(object sender, EventArgs e)
        {
            readFileDirectory();
        }

        private void readFileDirectory()
        {
            numCorrect = 0;
            numIncorrect = 0;
            //int counter = 0;
            string[] files = { "C:\\Users\\philp\\Documents\\Visual Studio 2015\\Projects\\CECS 328 Asignment 2\\CECS 328 Asignment 2\\Class A Files\\" ,
            "C:\\Users\\philp\\Documents\\Visual Studio 2015\\Projects\\CECS 328 Asignment 2\\CECS 328 Asignment 2\\Class B Files\\"};
            var CountQuery2 = from x in stringCorr
                              orderby x.Value descending
                              select new { Token = x.Key, Correlation = Math.Round(x.Value, 5) };
            var CountQuery1 = from x in stringCorr1
                              orderby x.Value descending
                              select new { Token = x.Key, Correlation = Math.Round(x.Value, 5) };
            var joinQuery = from x in CountQuery2
                            join y in CountQuery1 on x.Token equals y.Token
                            select new { Token = x.Token, A_Correlation = x.Correlation, B_Correlation = y.Correlation };

            foreach (string file in files)
            { 
                foreach (string fileName in Directory.EnumerateFiles(file))
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

                    }

                    for (int i = 0; i < lines.Length; i++)
                    {
                        strings.AddRange(lines[i].Split(null).Where(s => !string.IsNullOrWhiteSpace(s)));
                    }
                    HashSet<string> stringset = new HashSet<string>(strings);

                    var CountQuery = from x in stringset
                                     group x by x into g
                                     let count = g.Count()
                                     orderby count descending
                                     select new { Value = g.Key, Count = count };
                    tokens = new List<Token>();
                    foreach (var tok in CountQuery)
                    {
                        tokens.Add(new Token(tok.Value, tok.Count));
                    }
                    AScore = 0.0;
                    BScore = 0.0;
                    
                    var ThreshQuery1 = from x in joinQuery
                                       where (x.A_Correlation > thresholdValue)
                                       join y in tokens on x.Token equals y.token
                                       select new { Token = x.Token, A_Correlation = x.A_Correlation, B_Correlation = x.B_Correlation, Count = y.count };
                    var ThreshQuery2 = from x in joinQuery
                                       where (x.B_Correlation > thresholdValue)
                                       join y in tokens on x.Token equals y.token
                                       select new { Token = x.Token, A_Correlation = x.A_Correlation, B_Correlation = x.B_Correlation, Count = y.count };

                    foreach (var discrm in ThreshQuery1)
                    {
                        AScore += (discrm.A_Correlation * discrm.Count);
                    }
                    foreach (var discrm in ThreshQuery2)
                    {
                        BScore += (discrm.B_Correlation * discrm.Count);
                    }
                 
                    if ((fileName.Contains("Class A") && AScore > BScore) || (fileName.Contains("Class B") && BScore > AScore))
                    {
                        numCorrect++;
                        
                    }
                    else
                    {
                        numIncorrect++;
                        
                    }
                }
            }
            textBox2.Text = "";
            textBox2.Text += "Religion Document Score: " + AScore + "\r\n";
            textBox2.Text += "Politics Document Score: " + BScore + "\r\n";
            if (AScore > BScore)
                textBox2.Text += "Test Document is a Religion Document\r\n";
            if (BScore > AScore)
                textBox2.Text += "Test Document is a Politics Document\r\n";
            textBox2.Text += "Correctly Classified Documents: " + numCorrect + "\r\n";
            textBox2.Text += "Incorrectly Classified Documents: " + numIncorrect + "\r\n";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(thresholdValue<=1.0 && thresholdValue >=0)
                thresholdValue = Convert.ToDouble(numericUpDown1.Value);
        }
    }
}
