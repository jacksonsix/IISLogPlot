using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plot
{
    class logbuf
    {
        int unread;

    }
    class iislog
    {
        // read 5 lines

        public void dellog()
        {
            string connstring = @"Data Source=HRWDTDEV00;Initial Catalog=Test;User ID=sa;Password=HR*ware2012";

            using (SqlConnection openCon = new SqlConnection(connstring))
            {
                string saveStaff = @"truncate table iislog ";
                openCon.Open();

                using (SqlCommand delcommand = new SqlCommand(saveStaff))
                {
                    delcommand.Connection = openCon;
                    delcommand.ExecuteNonQuery();
                }
                openCon.Close();
            }
        }
        public void readlog(int begin,int bufsize)
        {
            string path = @"C:\Tools\iislog.txt";
            string[] files = System.IO.File.ReadAllLines(path);
            
            string[] lines = new string[bufsize] ;
            int total = (files.Length - begin) < bufsize ? (files.Length - begin) : bufsize;

            Array.Copy(files, begin, lines, 0, total);     
            connect(lines,total);
        }

        public void connect(string[] lines,int total)
        {
            string connstring = @"Data Source=HRWDTDEV00;Initial Catalog=Test;User ID=sa;Password=HR*ware2012";         

            using (SqlConnection openCon = new SqlConnection(connstring))
            {
                string saveStaff = @"INSERT into iislog  ([dateiis]
                                   ,[timeiis]
                                   ,[sip]
                                   ,[csmethod]
                                   ,[csuristem]
                                   ,[csuriquery]
                                   ,[sport]
                                   ,[csusername]
                                   ,[cip]
                                   ,[csUserAgent]
                                   ,[csSessionId]
                                   ,[token]
                                   ,[csReferer]
                                   ,[scstatus]
                                   ,[scsubstatus]
                                   ,[scwin32status]
                                   ,[timetaken])   
                                   VALUES (@v1,@v2,@v3,@v4,@v5,@v6,@v7,@v8,@v9,@v10,@v11,@v12,@v13,@v14,@v15,@v16,@v17)";
                openCon.Open();
                int max = total;
                for (int i=0;i< max;i++)
                {
                    string exp = lines[i];
                    if (exp.IndexOf("#") != -1) continue;
                    var records = exp.Split(new char[] { ' ' });
                    using (SqlCommand querySaveStaff = new SqlCommand(saveStaff))
                    {
                        querySaveStaff.Connection = openCon;
                        querySaveStaff.Parameters.Add("@v1", SqlDbType.VarChar, 50).Value = records[0];
                        querySaveStaff.Parameters.Add("@v2", SqlDbType.VarChar, 100).Value = records[1];
                        querySaveStaff.Parameters.Add("@v3", SqlDbType.VarChar, 100).Value = records[2];
                        querySaveStaff.Parameters.Add("@v4", SqlDbType.VarChar, 100).Value = records[3];
                        querySaveStaff.Parameters.Add("@v5", SqlDbType.VarChar, 100).Value = records[4];
                        querySaveStaff.Parameters.Add("@v6", SqlDbType.VarChar, 30).Value = records[5];
                        querySaveStaff.Parameters.Add("@v7", SqlDbType.VarChar, 30).Value = records[6];
                        querySaveStaff.Parameters.Add("@v8", SqlDbType.VarChar, 30).Value = records[7];
                        querySaveStaff.Parameters.Add("@v9", SqlDbType.VarChar, 30).Value = records[8];
                        querySaveStaff.Parameters.Add("@v10", SqlDbType.VarChar, 30).Value = records[9];
                        try {
                            string[] rs = records[10].Split(new char[] { ';' });
                            string sessionid = rs[3].Substring(rs[3].IndexOf('=') + 1);
                            string token = rs[4].Substring(rs[4].IndexOf('=') + 1);
                            querySaveStaff.Parameters.Add("@v11", SqlDbType.VarChar, 40).Value = sessionid;
                            querySaveStaff.Parameters.Add("@v12", SqlDbType.VarChar, 100).Value = token;
                        }
                        catch (Exception e) {
                            querySaveStaff.Parameters.Add("@v11", SqlDbType.VarChar, 40).Value = "";
                            querySaveStaff.Parameters.Add("@v12", SqlDbType.VarChar, 100).Value = "";
                        }                      


                        querySaveStaff.Parameters.Add("@v13", SqlDbType.VarChar, 30).Value = records[11];
                        querySaveStaff.Parameters.Add("@v14", SqlDbType.VarChar, 30).Value = records[12];
                        querySaveStaff.Parameters.Add("@v15", SqlDbType.VarChar, 30).Value = records[13];
                        querySaveStaff.Parameters.Add("@v16", SqlDbType.VarChar, 30).Value = records[14];
                        querySaveStaff.Parameters.Add("@v17", SqlDbType.VarChar, 30).Value = records[15];
                        querySaveStaff.ExecuteNonQuery();
                    }
                }              
                openCon.Close();
            }


        } 
    }
}
