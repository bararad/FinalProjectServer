using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using final_proj;
using final_proj.BL;
using final_proj.Controllers;


public class DBservicesUser: GeneralDBservices
{
    public DBservicesUser():base()
    {
        
    }


    //The users are created automaticly on SQL when using 'insertescort','insertparent'

    public User GetUserByDetails(User user)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithSPWithParameters("SPproj_GetUserByDetails", con, user);  // create the command


        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            User u = new User();
            while (dataReader.Read())
            {
                u.UserName = (dataReader["username_"]).ToString();
                u.Password = (dataReader["password_"]).ToString();
                u.Role = Convert.ToInt32(dataReader["Role"]);
            }
            return u;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    private SqlCommand CreateCommandWithSPWithParameters(String spName, SqlConnection con, User user)
    {
        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@username", user.UserName);

        cmd.Parameters.AddWithValue("@password", user.Password);

        return cmd;
    }

}

