using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plot
{
    public class logd
    {
        public int dx;
        public int sessionId;
        public string tag;
        public string dtime;
        public string drequest;
        public string token;
        public string taken;
        public string user;
    }
    public class iisdata
    {
        List<logd> data;
        int maxtime;
        int sessions;
        public List<logd> getData()
        {
            return data;
        }
        public int getMax()
        {
            return this.maxtime;
        }
        public int getSessionNum()
        {
            return this.sessions;
        }
        public void prepare()
        {
            //prepare X,Y    x time,  y  token increase
            readlogdb2();
            var first = data[0].dtime;
            var last = data[data.Count -1].dtime;

            var span = DateTime.Parse(last) - DateTime.Parse(first);
            int maxx = Convert.ToInt32( span.TotalSeconds);
            this.maxtime = maxx;
            int sequence = 0;
            IDictionary<string, int> dict = new Dictionary<string, int>();

            int x = 0;
            int y = 0;
          
            foreach (var log in data)
            {
                string key = log.token;
                if (key.Length < 5) continue;   // count how many tokens
                int outv = 0; 
                bool suc = dict.TryGetValue(key, out outv);
                if (suc)
                {
                    log.sessionId = outv;
                    log.dx = Convert.ToInt32((DateTime.Parse(log.dtime) - DateTime.Parse(first)).TotalSeconds);
                }
                else
                {
                    log.sessionId = sequence;
                    log.dx = Convert.ToInt32((DateTime.Parse(log.dtime) - DateTime.Parse(first)).TotalSeconds);
                    dict.Add(key, sequence++);
                }

            }

            this.sessions = sequence;

        }

        //public void readlogdb()
        //{
        //    data = new List<logd>();
        //    string connstring = @"Data Source=HRWDTDEV00;Initial Catalog=Test;User ID=sa;Password=HR*ware2012";
        //    using (SqlConnection openCon = new SqlConnection(connstring))
        //    {
        //        openCon.Open();
        //        SqlCommand myCommand = new SqlCommand("select * from [Test].[dbo].[iislog]  order by timeiis", openCon);
        //        SqlDataReader myReader = myCommand.ExecuteReader();
        //        while (myReader.Read())
        //        {                    
        //            data.Add(new logd { dtime= myReader["timeiis"].ToString()
        //                                , drequest = myReader["csuristem"].ToString()
        //                                , token = myReader["token"].ToString()
        //                              });
        //        }
        //        openCon.Close();
        //    }              
                         
        //}

        public void readlogdb2()
        {
            data = new List<logd>();
            dbinterface dbc = new dbinterface();
            string connstring = @"Data Source=HRWDTDEV00;Initial Catalog=Test;User ID=sa;Password=HR*ware2012";
            string readcmd = "select * from [Test].[dbo].[iislog]  order by timeiis";
            var table = dbc.executeRead(connstring,readcmd);
            foreach (DataRow row in table.Rows)
            {
                data.Add(new logd
                {
                    dtime = row["timeiis"].ToString() ,
                    drequest = row["csuristem"].ToString() ,
                    token = row["token"].ToString(),
                    taken = row["timetaken"].ToString(),
                    user = row["csusername"].ToString()
                });
            }               
           
        }
    }
}
