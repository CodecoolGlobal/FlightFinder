using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Data.SqlClient;

namespace FlightFinder.Models
{
    public class DataBase
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-TL9U1P9;Initial Catalog=Movies;Integrated Security=True;Pooling=False");
        public int LoginCheck(AdminLogin adminLogin)
        {
            SqlCommand sqlCommand = new SqlCommand("Sp_Login", con);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Admin_name", adminLogin.AdminName);
            sqlCommand.Parameters.AddWithValue("@Admin_password", adminLogin.AdminPassword);
            SqlParameter obLogin = new SqlParameter();
            obLogin.ParameterName = "@Isvalid";
            obLogin.SqlDbType = SqlDbType.Bit;
            obLogin.Direction = ParameterDirection.Output;
            sqlCommand.Parameters.Add(obLogin);
            con.Open();
            sqlCommand.ExecuteNonQuery();
            int response = Convert.ToInt32(obLogin.Value);
            con.Close();
            return response;


        }
    }
}
