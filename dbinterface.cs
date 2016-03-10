using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plot
{
    class dbinterface
    {
        public DataTable executeRead( string connect, string cmdstring)
        {
            var dataTable = new DataTable();          
            string connstring =connect;
            using (SqlConnection openCon = new SqlConnection(connstring))
            {
                openCon.Open();
                SqlCommand myCommand = new SqlCommand(cmdstring, openCon);
                SqlDataReader myReader = myCommand.ExecuteReader();
                dataTable.Load(myReader);           
                openCon.Close();
                return dataTable;
            }
        }

        public void excecuteWrite(string cmdstring)
        {
            string connstring = @"Data Source=HRWDTDEV00;Initial Catalog=Test;User ID=sa;Password=HR*ware2012";
            using (SqlConnection openCon = new SqlConnection(connstring))
            {
                openCon.Open();
                SqlCommand myCommand = new SqlCommand(cmdstring, openCon);
                SqlDataReader myReader = myCommand.ExecuteReader();
                openCon.Close();              
            }

        }

    }
}
