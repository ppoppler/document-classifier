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
    public partial class Form1 : Form
    {


        List<Image> tweets = new List<Image>();
        Timer timer1 = new Timer();
        int counter = 0;
        int secondsToWait = 4;
        int speed1 = 20;           // tick speed ms
        int speed2 = 5;            // alpha (0-255) change speed
        int dir = 8;               // direction >0 : fade-in..
        int index = 1;
        //global::CECS_328_Asignment_2.Properties.Resources.ebert_tweet,
        //     global::CECS_328_Asignment_2.Properties.Resources.ebert_tweet1,
        //  global::CECS_328_Asignment_2.Properties.Resources.ebert_tweet2,
        //global::CECS_328_Asignment_2.Properties.Resources.ebert_tweet3,
        // global::CECS_328_Asignment_2.Properties.Resources.ebert_tweet4,
        //global::CECS_328_Asignment_2.Properties.Resources.ebert_tweet5
        //};
        public Form1()
        {
            InitializeComponent();

         
            
        }

    

        

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Input a string, then receive the key polynomial of the string.", this.button1);
        }
        
        private void button2_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Input a size for a table along with a list of words. \nThe program will return the table.", this.button2);
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Input a prime-number size for a quadratic probing hash table, \nalong with words to insert into the table. Prints the table.", this.button3);
        }

        private void button4_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Input a name of a text document. Reads and \nprints a clean version of the document.", this.button4);
        }

        private void button5_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Input a name of a text document. Reads it, \ncleans it, and keeps track of the tokens by printing them by frequency.", this.button5);
        }

        private void button6_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Input a name of a text document. Determines if the document \nis classified as a Class-A or Class-B document.", this.button6);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Option1Form option1 = new Option1Form();
            option1.Tag = this;
            option1.Show(this);
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Option2Form option2 = new Option2Form();
            option2.Tag = this;
            option2.Show(this);
            Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Option3Form option2 = new Option3Form();
            option2.Tag = this;
            option2.Show(this);
            Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Option4Form option4 = new Option4Form();
            option4.Tag = this;
            option4.Show(this);
            Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Option5Form option5 = new Option5Form();
            option5.Tag = this;
            option5.Show(this);
            Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Option6Form option6= new Option6Form();
            option6.Tag = this;
            option6.Show(this);
            Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Option7Form option7 = new Option7Form();
            option7.Tag = this;
            option7.Show(this);
            Hide();
        }
    }
}
