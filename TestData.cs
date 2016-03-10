using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plot
{
    public class TestData
    {
       public List<string> response;
       public List<DateTime> begins;
        public TestData()
        {
            response = new List<string>();
            begins = new List<DateTime>();
        }

        public void readin()
        {
            string file = @"C:\Tools\testScript\results.csv";
            var lines = System.IO.File.ReadAllLines(file);
            for (int i = 0; i < lines.Length; i++)
            {
                string[] fields = lines[i].Split(new char[] { ',' });
                response.Add(fields[1]);

                var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Math.Round(Convert.ToUInt64(fields[0]) / 1000d)).ToLocalTime();
                begins.Add(dt);
            }
        }

       


    }
}
