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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TestData data = new TestData();
            data.readin();
            int count = data.begins.Count;
            for (int i = 0; i < count; i++)
            {
                chart1.Series["Series1"].Points.AddXY(data.begins[i].ToShortTimeString(), data.response[i]);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int fromline = Convert.ToInt32(textBox1.Text);
            int bufsize = Convert.ToInt32(textBox2.Text);
            iislog logs = new iislog();
            logs.readlog(fromline,bufsize);

            label1.Text = "finished";
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //
            this.Visible = false;
            Form2 form2 = new Form2();
            form2.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            iislog logs = new iislog();
            logs.dellog();
        }
    }
}
