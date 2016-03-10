using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace plot
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.AutoScroll = true;
            this.AutoScrollMinSize = new Size(1000, 1000);
            this.ResizeRedraw = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // read txt file
           string path = @"C:\Users\JLiang\Documents\Visual Studio 2015\Projects\ConsoleApplication1\plot\rquests.txt";
           string[] rquests =  System.IO.File.ReadAllLines(path);

            System.Drawing.Graphics graphics = this.CreateGraphics();        

            iisdata read = new iisdata();
            read.prepare();
            var data = read.getData();

           
            System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 7);
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            float x = 150.0F;
            float y = 50.0F;
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
            for (int t = 0; t < read.getMax(); t++)
            {               
                string drawString = t.ToString();
                graphics.DrawString(drawString, drawFont, drawBrush, t*30, 0, drawFormat);          
            }

            for (int s = 0; s < read.getSessionNum(); s++)
            {
                if (s % 10 == 0)
                {
                    string drawString = s.ToString();
                    graphics.DrawString(drawString, drawFont, drawBrush, 0, s * 5, drawFormat);
                }
            }
           

            for (int i = 0; i < data.Count; i++)
            {
                int len = Convert.ToInt32((Convert.ToInt32(data[i].taken) * 0.03));
                int startreq = data[i].dx * 30 - len;
                string drawString = data[i].user;
                System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(
                                                                        startreq, i *8, len, 8);
                if (drawString.Equals("Abrams"))
                    graphics.DrawRectangle(System.Drawing.Pens.Red, rectangle);
                else if (drawString.Equals("LArmstrong"))
                    graphics.DrawRectangle(System.Drawing.Pens.Green, rectangle);
                else if (drawString.Equals("SBaker"))
                    graphics.DrawRectangle(System.Drawing.Pens.Purple, rectangle);
                else
                    graphics.DrawRectangle(System.Drawing.Pens.Blue, rectangle);

                int reqId = Array.IndexOf(rquests, data[i].drequest);
                graphics.DrawString(reqId.ToString(), drawFont, drawBrush, startreq - 12, i*8, drawFormat);
            }

            drawFont.Dispose();
            drawBrush.Dispose();
        }

        //protected override void OnResize(EventArgs e)
        //{
        //    base.OnResize(e);
        //    System.Drawing.Graphics graphics = this.CreateGraphics();

        //    iisdata read = new iisdata();
        //    read.prepare();
        //    var data = read.getData();


        //    System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 6);
        //    System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
        //    float x = 150.0F;
        //    float y = 50.0F;
        //    System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
        //    for (int t = 0; t < read.getMax(); t++)
        //    {
        //        //System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(
        //        //t * 10, 0, 3, 3);
        //        //graphics.DrawEllipse(System.Drawing.Pens.Red, rectangle);
        //        string drawString = t.ToString();
        //        graphics.DrawString(drawString, drawFont, drawBrush, t * 15, 0, drawFormat);

        //    }
        //    drawFont.Dispose();
        //    drawBrush.Dispose();

        //    for (int i = 0; i < data.Count; i++)
        //    {
        //        System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(
        //        data[i].dx * 15, data[i].dy * 5, 3, 3);
        //        graphics.DrawEllipse(System.Drawing.Pens.Red, rectangle);
        //    }
        //}
        private void button2_Click(object sender, EventArgs e)
        {
           
            this.Visible = false;
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            iisdata read = new iisdata();
            read.prepare();
            iisvm vm = new iisvm(read);
            var form3 = new drawform(vm);
            form3.Show();
          
        }
    }
}
