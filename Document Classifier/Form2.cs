using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

using System.Windows.Forms;

namespace CECS_328_Asignment_2
{
    public partial class Form2 : Form
    {

        private DrawPanel pan_image;
        private DrawPanel pan_layer;
        Timer timer1 = new Timer();
        List<Image> imageFiles = new List<Image>();
        int counter = 0;
        int secondsToWait = 4;
        int speed1 = 20;           // tick speed ms
        int speed2 = 5;            // alpha (0-255) change speed
        int dir = 8;               // direction >0 : fade-in..
        int index = 1;
        public Form2()
        {
            InitializeComponent();

            pan_image.Parent = this;
            pan_layer.Parent = this;
            pan_image.BackgroundImage = imageFiles[0];
            pan_image.Dock = DockStyle.Fill;
            pan_layer.Parent = pan_image;
            pan_layer.BackColor = pan_image.BackColor;
            pan_layer.Dock = DockStyle.Fill;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

           
            

            pan_image.BackgroundImage = imageFiles[0];
            pan_image.Dock = DockStyle.Fill;
            pan_layer.Parent = pan_image;
            pan_layer.BackColor = pan_image.BackColor;
            pan_layer.Dock = DockStyle.Fill;

            pan_image.BackgroundImageLayout = ImageLayout.Zoom;
            pan_layer.BackgroundImageLayout = ImageLayout.Zoom;

            timer1.Tick += timer1_Tick;
            timer1.Interval = speed1;
            timer1.Start();
        }

        bool changeImage()
        {
            if (pan_image.BackgroundImage != null)
            {
                var img = pan_image.BackgroundImage;
                pan_image.BackgroundImage = null;
                img.Dispose();
            }
            index = index++ % imageFiles.Count;
            pan_image.BackgroundImage = imageFiles[index];
            return index < imageFiles.Count;
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            // we have just waited and now we fade-out:
            if (dir == 0)
            {
                timer1.Stop(); 
                dir = -speed2;
                counter = 254;
                timer1.Interval = speed2;
                timer1.Start();
            }

            // the next alpha value:
            int alpha =  Math.Min(Math.Max(0, counter+= dir), 255);

            //button1.Text = dir > 0 ? "Fade In" : "Fade Out";

            // fully faded-in: set up the long wait:
            if (counter >= 255)
            { 
                timer1.Stop(); 
                //button1.Text = "Wait";
                timer1.Interval = secondsToWait * 1000;
                dir = 0;
                timer1.Start();
            }
            // fully faded-out: try to load a new image and set direction to fade-in or stop
            else if (counter <= 0)
            {
                if ( !changeImage() )  timer1.Stop(); 
                dir = speed2;
            }
            // create the new, semi-transparent color:
            Color col = Color.FromArgb(255 - alpha, pan_image.BackColor);
            // display the layer:
            pan_layer.BackColor = col;
            pan_layer.Refresh();
        }

        private void InitializeComponent()
        {
            this.pan_image = new CECS_328_Asignment_2.DrawPanel();
            this.pan_layer = new CECS_328_Asignment_2.DrawPanel();
            this.SuspendLayout();
            // 
            // pan_image
            // 
            this.pan_image.Location = new System.Drawing.Point(0, 0);
            this.pan_image.Name = "pan_image";
            this.pan_image.Size = new System.Drawing.Size(200, 100);
            this.pan_image.TabIndex = 0;
            // 
            // pan_layer
            // 
            this.pan_layer.Location = new System.Drawing.Point(168, 161);
            this.pan_layer.Name = "pan_layer";
            this.pan_layer.Size = new System.Drawing.Size(415, 180);
            this.pan_layer.TabIndex = 0;
            // 
            // Form2
            // 
            this.ClientSize = new System.Drawing.Size(619, 431);
            this.Controls.Add(this.pan_layer);
            this.Name = "Form2";
            this.ResumeLayout(false);

            imageFiles = new List<Image>();
           



            pan_image.BackgroundImage = imageFiles[0];
            pan_image.Dock = DockStyle.Fill;
            pan_layer.Parent = pan_image;
            pan_layer.BackColor = pan_image.BackColor;
            pan_layer.Dock = DockStyle.Fill;

            pan_image.BackgroundImageLayout = ImageLayout.Zoom;
            pan_layer.BackgroundImageLayout = ImageLayout.Zoom;
            
            timer1.Tick += timer1_Tick;
            timer1.Interval = speed1;
            timer1.Start();

        }
    }

    
}
