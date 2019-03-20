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
    public partial class Option6Form : Form
    {

        String[] lines;
        String[] chars;
        List<String> strings = new List<String>();
        List<String> totalStrings = new List<String>();
        List<String> totalStrings1 = new List<String>();
        List<Token> tokens = new List<Token>();
        Dictionary<string, double> stringCorr = new Dictionary<string, double>();
        //Dictionary <string,int> StringDict;
        //Dictionary<string, int> StringDict1;
        int counter=0;

        public Option6Form()
        {
            chars = new String[] { ".", ",", "!", "@", "#", "$", "%", "^", "&", "*","/", "\\", "(", ")", "?","=", "-", "’", " = ", "_", "+", ";", ":","—", "\"","“","”", "|", "[", "]", ">", "<", "?" };
            InitializeComponent();
            openFileDialog1.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = true;

        }

        /// <summary>
        /// The action to the first button which prints out all of the 
        /// correlations in the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {

           /// Dictionary of the political word correlations. The string is the name of the string, and the double is its correlation.
            Dictionary<string, double> stringCorr1 = new Dictionary<string, double>();
            //Dictionary of the religious word correlations. The string is the name of the string, and the double is its correlation.
            stringCorr = new Dictionary<string, double>();
            //For each data row in the database.
            foreach(DataRow dr in Program.dt.Rows)
            {
                //Save how many times the word appears in A and how many times the word appears in B
                int rt = Convert.ToInt16(dr["A_Count"].ToString());
                int pt = Convert.ToInt16(dr["B_Count"].ToString());
                //Calculate the bayesian correaltion between the two groups A and B in respect to A.
                double correlation = (rt - pt) / (Math.Sqrt(rt + pt) * Math.Sqrt(1000 - rt - pt));
                //Add the word and its correlation to group A's dictionary.
                stringCorr.Add(dr["Token_Name"].ToString(), correlation);
            }

            //For each row in the database again... (This part is redundant, move the correlation in the above function).
            foreach (DataRow dr in Program.dt.Rows)
            {
                int rt = Convert.ToInt16(dr["A_Count"].ToString());
                int pt = Convert.ToInt16(dr["B_Count"].ToString());
                double correlation = (pt - rt) / (Math.Sqrt(pt + rt) * Math.Sqrt(1000 - pt - rt));
                stringCorr1.Add(dr["Token_Name"].ToString(), correlation);
            }
            Console.WriteLine("yes");
            //Query that orders all the words by descending order of their correlation. (GROUP A)
            var CountQuery = from x in stringCorr
                             orderby x.Value descending
                             select new { Token = x.Key, Correlation = Math.Round(x.Value,5) };
            //Query that orders all the words by descending order of their correlation. (GROUP B)
            var CountQuery1 = from x in stringCorr1
                             orderby x.Value descending
                             select new { Token = x.Key, Correlation = Math.Round(x.Value,5) };
            textBox2.Text = "";
            //String Builders for both the text boxes.
            StringBuilder str = new StringBuilder("");
            StringBuilder str1 = new StringBuilder("");
            int count = 0;
            //Append all of the religious correlations
            str.Append("Religion correlations: \r\n");
            //For each correlation in the countQuery regarding Group A:
            foreach (var corr in CountQuery)
            {
                //If the Correlation is greater than 0 then don't do anything. 
                if(corr.Correlation > 0)
                //If the token's length is less than sixteen then don't include it
                if(corr.Token.Length<=16)
                    str.Append(("Token: " + corr.Token).PadRight(80) + ("\r\t\tCorrelation: " + corr.Correlation).PadLeft(5) + "\r\n");
                else
                    str.Append(("Token: " + corr.Token).PadRight(80) + ("\r\tCorrelation: " + corr.Correlation).PadLeft(5) + "\r\n");
                count++;
                //if (count >= 21)
                 //   break;
            }
            count = 0;
            //Do the same as above with the political correlations in Group B:
            str1.Append("\r\nPolitical correlations: \r\n");
            foreach (var corr in CountQuery1)
            {
                if(corr.Correlation >0)
                if (corr.Token.Length <= 16)
                    str1.Append(("Token: " + corr.Token).PadRight(80) + ("\r\t\tCorrelation: " + corr.Correlation).PadLeft(5) + "\r\n");
               else
                    str1.Append(("Token: " + corr.Token).PadRight(80) + ("\r\tCorrelation: " + corr.Correlation).PadLeft(5) + "\r\n");
               count++;
                //if (count >= 21)
                //    break;
            }

            Console.WriteLine("yes2");
 
            textBox2.Text = str.ToString();
            textBox1.Text = str1.ToString();
            //textBox2.Text = "";
            //string fileName;
            //openFileDialog1.ShowDialog();
            //fileName = openFileDialog1.FileName;
            //textBox1.Text = fileName;
            //Code for Part 4: 
            //readFile(fileName);
            //readFileDirectory();


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
                
                Console.WriteLine(lines[i]);
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
            if (counter <4)
            {
                totalStrings.AddRange(stringset);
            }
            //else
            //{
            //    StringDict1 = new Dictionary<string, int>();
            //    foreach (var x in CountQuery)
            //    {
            //        //Console.WriteLine("V: " + x.Value + "  K: " + x.Count);
            //        StringDict1.Add(x.Value, x.Count);
            //    }
            //    totalStrings1.AddRange(strings);
            //}
            //counter++;
        }
        private void readFileDirectory()
        {
            int counter = 0;
            string[] files = { "C:\\Users\\philp\\Documents\\Visual Studio 2015\\Projects\\CECS 328 Asignment 2\\CECS 328 Asignment 2\\Class A Files\\" ,
            "C:\\Users\\philp\\Documents\\Visual Studio 2015\\Projects\\CECS 328 Asignment 2\\CECS 328 Asignment 2\\Class B Files\\"};
            foreach (string file in files) { 
                counter = 0;
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
                    if (fileName.Contains("Class A Files"))
                        totalStrings.AddRange(stringset);
                    if (fileName.Contains("Class B Files"))
                        totalStrings1.AddRange(stringset);

                    counter++;
                    Console.WriteLine(counter);
                }
                        
            }
        }
        

        private void initializeDatabase()
        {
            TokenDatabase db = new TokenDatabase(@"c:\linq\northwnd.mdf");
            db.CreateDatabase();
            Console.WriteLine(db.DatabaseExists());
            //Table<Token> tokens = db.GetTable<Token>();
            
        }

        /**
         * The following code should be used to send the INITIAL values from each class folder into the database. 
         * It loads all the files, and then adds the word values into the database. Uncomment the code above in button2_Click
         * to activate this method
         **/
        private void sendIntoDatabase()
        {
            //First Query, the A_count and string value
            var CountQuery = from x in totalStrings
                             group x by x into g
                             let count = g.Count()
                             orderby count descending
                             select new { Value = g.Key, ACount = count };
            //Second Query, the B_count and string value
            var CountQuery1 = from x in totalStrings1
                             group x by x into g
                             let count = g.Count()
                             orderby count descending
                             select new { Value = g.Key, BCount = count };
            //An inner join on the string value, the new query will have string value, A_count, and B_count. If there is no B.Count, then it will be set to 0
            var resultQuery = from x in CountQuery
                              join y in CountQuery1 on x.Value equals y.Value into gj
                              from subquery in gj.DefaultIfEmpty() 
                              select new { Value = x.Value, ACount = x.ACount, BCount = subquery?.BCount ?? 0};
            //An inner join on the string value, the new query will have string value, A_count, and B_count. If there is no A.Count, then it will be set to 0
            var resultQuery1 = from x in CountQuery1
                               join y in CountQuery on x.Value equals y.Value
                               into gj
                               from subquery in gj.DefaultIfEmpty()
                               select new { Value = x.Value, ACount = subquery?.ACount ?? 0, BCount = x.BCount };
          
                         

                         
                         
            //Add each of the queries to the token list(incase you want to print access them with code in the same loop
            //And most importantly, add them to the database
            foreach(var tok in resultQuery)
            {
                Console.WriteLine(tok.Value + " " + tok.ACount + " " + tok.BCount);
                //Program.tokens.Add(new Token(tok.Value, tok.ACount, tok.BCount));
                Program.tabletableAdapter.Insert(tok.Value, tok.ACount, tok.BCount);
            }
            //All the tokens added to the database via resultQuery all have valid A_Count. That means if you add the 
            //tokens from resultQuerey1 that have an A_Count = 0, then they do not exist in the resultQuery. If they do have an A_Count that isn't 0,
            //then they've already been added
            foreach(var tok in resultQuery1)
            {
                Console.WriteLine(tok.Value + " " + tok.ACount + " " + tok.BCount);
                Program.tokens.Add(new Token(tok.Value, tok.ACount, tok.BCount));
                if(tok.ACount==0)
                    Program.tabletableAdapter.Insert(tok.Value, tok.ACount, tok.BCount);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            readFileDirectory();
            sendIntoDatabase();
        }
    }
}
