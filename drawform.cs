using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plot
{
    class drawform : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Button button1;
        iisvm _vm;
        public drawform(iisvm vm)
        {
            _vm =  vm;
            InitializeComponent();
            this.AutoScroll = true;
            this.AutoScrollMinSize = new Size(1000, 1000);
            this.ResizeRedraw = true;
        }

        public  void drawX(Graphics canvas,Font drawFont,SolidBrush drawBrush, StringFormat drawFormat)
        {
            int xscale = 20;
            int timeSeconds = _vm.totalruntime;
            for (int t = 0; t < timeSeconds; t++)
            {
                string drawString = t.ToString();
                canvas.DrawString(drawString, drawFont, drawBrush, t * xscale, 0, drawFormat);
            }
        }

        public void drawY(Graphics canvas, Font drawFont, SolidBrush drawBrush, StringFormat drawFormat)
        {
            for (int s = 0; s < _vm.totolusers; s++)
            {
                if (s % 10 == 0)
                {
                    string drawString = s.ToString();
                    canvas.DrawString(drawString, drawFont, drawBrush, 0, s * 10, drawFormat);
                }
            }
        }

        public void drawUnit(Graphics canvas, Font drawFont, SolidBrush drawBrush, StringFormat drawFormat,int x,int y,float width,int height,int userId, int reqId)
        {
            // scale to form pixels
            int xscale = 20;
            int yscale = 7;

            int len = Convert.ToInt32( width * xscale);
            int startreq = x * xscale - len;
                      
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(
                                                                    startreq, y*yscale, len, height*yscale);
            if (userId == 0)
                canvas.DrawRectangle(System.Drawing.Pens.Red, rectangle);
            else if (userId == 1)
                canvas.DrawRectangle(System.Drawing.Pens.Green, rectangle);
            else if (userId == 2)
                canvas.DrawRectangle(System.Drawing.Pens.Purple, rectangle);
            else if (userId == 3)
                canvas.DrawRectangle(System.Drawing.Pens.Red, rectangle);
            else if (userId == 4)
                canvas.DrawRectangle(System.Drawing.Pens.Green, rectangle);
            else if (userId == 5)
                canvas.DrawRectangle(System.Drawing.Pens.Purple, rectangle);
            else
                canvas.DrawRectangle(System.Drawing.Pens.Blue, rectangle);
           
            canvas.DrawString(reqId.ToString(), drawFont, drawBrush, startreq - 12, y*yscale, drawFormat);
        }

        private void drawUserSession(Graphics canvas, Font drawFont, SolidBrush drawBrush, StringFormat drawFormat,int yPos,int sesionId)
        {  // in x, y unit .
            var data = _vm.useractions[sesionId];
            int x, y, height;
            for (int i = 0; i < data.requests.Count; i++)
            {
                x = data.requests[i].start;
                y = yPos + i;
                float width = data.requests[i].duration;
                height = 1;
                drawUnit(canvas, drawFont, drawBrush, drawFormat, x, y, width, height,data.userId,data.requests[i].requestId);
            }
           
        }
        public void draw()
        {          

            System.Drawing.Graphics graphics = this.CreateGraphics();
            System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 7);
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);           
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();

            iisdata read = new iisdata();
            read.prepare();
            var data = read.getData();

            drawX(graphics,drawFont,drawBrush,drawFormat);
            drawY(graphics, drawFont, drawBrush, drawFormat);

            for (int i = 0; i < 7; i++)
            {
                drawUserSession(graphics, drawFont, drawBrush, drawFormat, i * 12, i);
            }
            //drawUserSession(graphics, drawFont, drawBrush, drawFormat, 0 * 30, 1);
            // drawUserSession(graphics, drawFont, drawBrush, drawFormat, 1 * 30, 0);
            // drawUserSession(graphics, drawFont, drawBrush, drawFormat, 2 * 30, 2);

            drawFont.Dispose();
            drawBrush.Dispose();
        }

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(463, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // drawform
            // 
            this.ClientSize = new System.Drawing.Size(550, 332);
            this.Controls.Add(this.button1);
            this.Name = "drawform";
            this.ResumeLayout(false);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            draw();
        }
    }
}
