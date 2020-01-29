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

        SqlConnection con = new SqlConnection("YOUR CONNECTION STRING");
        public int LoginCheck(Login login)
        {
            SqlCommand sqlCommand = new SqlCommand("Sp_Login", con);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@User_name", login.UserName);
            sqlCommand.Parameters.AddWithValue("@User_password", login.UserPassword);
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
        public void Register(Register register)
        {
            SqlCommand sqlCommand = new SqlCommand("Sp_Register", con);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@User_name", register.UserName);
            sqlCommand.Parameters.AddWithValue("@User_password", register.UserPassword);
            SqlParameter obLogin = new SqlParameter();
            con.Open();
            sqlCommand.ExecuteNonQuery();
            int response = Convert.ToInt32(obLogin.Value);
            con.Close();
        }

    }
}
