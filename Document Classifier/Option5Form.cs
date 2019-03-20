using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CECS_328_Asignment_2
{
    public partial class Option5Form : Form
    {

        string[] lines;
        String[] chars;
        List<String> strings = new List<String>();
        List<String> totalStrings = new List<String>();
        
        QuadProbeHashTableTok qtable;
        List<int> primes;

        List<Token> stringList;
        public Option5Form()
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

            //foreach(String str in strings)
            //{
             //   stringList.Add(new Token(str, 1));
            //}
            

            //for (int i = 0; i < lines.Length; i++)
            //{
            //    strings.AddRange(lines[i].Split(null).Where(s => !string.IsNullOrWhiteSpace(s)));
            //}
            
            //Console.WriteLine(strings.Count);
            foreach (var str in strings)
            {
                //Console.Write(str + " ");
            }
            
            
            
            //totalStrings.AddRange(strings);
        }

        private static bool isEqual(Token s, Token t)
        {
            if (s.token == t.token)
                return true;
            return false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int index;
            StringBuilder text = new StringBuilder();
            HashSet<String> set = new HashSet<String>();
            stringList = new List<Token>();
            int count = 0;
            Stopwatch timer = new Stopwatch();
            timer.Start();
            //var counts = strings.GroupBy(x => x);
           
            //foreach (var str in counts)
            //{ 
            //    Token tok = new Token(str.Key, (double)str.Count());
            //    stringList.Add(tok);
            //}

            foreach (String str in strings)
            {
                Token tok = new Token(str, 1);

                //index = stringList.FindIndex(token => token.token == tok.token);
                if (!set.Contains(str))
                {
                    stringList.Add(tok);
                    set.Add(str);
                    count++;
                }
                else
                {
                    index = stringList.FindIndex(token => token.token == tok.token);
                    stringList[index].frequency++;
                }
            }
            stringList.Sort();
            Console.WriteLine(count);
            textBox2.Text = "";

            foreach (var str in stringList)
                text.Append("Value: " + str.token + " | Frequency: " + str.frequency + "\r\n");
            textBox2.Text = text.ToString();
            timer.Stop();
            long runtime = timer.ElapsedMilliseconds;
            if(runtime >= 1000)
                label1.Text = "Runtime: " + timer.ElapsedMilliseconds/1000.0 +" s";
            else
                label1.Text = "Runtime: " + timer.ElapsedMilliseconds + " ms";
            //Console.WriteLine(strings.Count());


            ///for(int i = 0; i<ctable.Size(); i++)
            //  if(ctable.retrieve(i)!=null)
            //    textBox2.Text += "Value: " + ctable.retrieve(i).token + " | Frequency: " + ctable.retrieve(i).frequency + "\r\n";
        }


        private void button4_Click(object sender, EventArgs e)
        {
            //Create a StringBuilder to make sure the string can be read in a fast time 
            StringBuilder text = new StringBuilder();
            //Instantiate Chain Hash Table
            ChainHashTableTok ctable = new ChainHashTableTok(strings.Count());
            HashSet<String> set = new HashSet<String>();
            int count = 0;
            //Start Timer
            Stopwatch timer = new Stopwatch();
            timer.Start();
            //For each string in the list of all the strings (includes duplicates)
            foreach (String str in strings)
            {
                //Tok is a new token with a count of 1
                Token tok = new Token(str, 1);
                //If the token isn't in the table already...
                //Retreieve(int hash), I've made a function that calculates the hashCode for Tokens
                if (!tok.Equals(ctable.retrieve(tok.GetLongHashCode())))
                {
                    ctable.insert(tok);
                    count++;
                }
                //If the token IS in the table, meaning the token was at that hash, increment the token by one 
                //(i've created a function that does this).
                else
                {
                    ctable.incrementToken(tok.GetLongHashCode());

                }
            }
            Console.WriteLine(count);
            List<Token> list = ctable.ToList();
            
            foreach (var tok in list) {
                //Console.WriteLine(tok.token);
                text.Append("Value: " + tok.token + " | Frequency: " + tok.frequency + "\r\n");
            }
            textBox2.Text = text.ToString();
            timer.Stop();
            long runtime = timer.ElapsedMilliseconds;
            if (runtime >= 1000)
                label2.Text = "Runtime: " + timer.ElapsedMilliseconds / 1000.0 + " s";
            else
                label2.Text = "Runtime: " + timer.ElapsedMilliseconds + " ms";
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            StringBuilder text = new StringBuilder();
            int givensize = (strings.Count());
            int count = 0;
            qtable = new QuadProbeHashTableTok(givensize);
            Stopwatch timer = new Stopwatch();
            timer.Start();
            foreach (String str in strings)
            {
                Token tok = new Token(str, 1);
                //If the token isn't in the table already...
                //Retreieve(int hash), I've made a function that calculates the hashCode for Tokens
                if (!tok.Equals(qtable.retrieve(tok.GetLongHashCode())))
                {
                    qtable.insert(tok);
                    count++;
                }
                //If the token IS in the table, meaning the token was at that hash, increment the token by one 
                //(i've created a function that does this).
                else
                {
                    qtable.incrementToken(tok.GetLongHashCode());
                }
                
            }
            Console.WriteLine(count);
            List<Token> list = qtable.ToList();
            textBox2.Text = "";
            foreach (var tok in list)
            {
                //Console.WriteLine(tok.token);
                text.Append("Value: " + tok.token + " | Frequency: " + tok.frequency + "\r\n");
            }
            textBox2.Text = text.ToString();
            timer.Stop();
            long runtime = timer.ElapsedMilliseconds;
            if (runtime >= 1000)
                label3.Text = "Runtime: " + timer.ElapsedMilliseconds / 1000.0 + " s";
            else
                label3.Text = "Runtime: " + timer.ElapsedMilliseconds + " ms";
            //Console.WriteLine(strings.Count());
        }

        private void CalculatePrime()
        {
            HashHelpers.GetPrime(2000);
            HashHelpers.GetPrime(500);
        }

       
       

    }

    
}
