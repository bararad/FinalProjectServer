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

public class DBservicesParent
{
    public DBservicesParent()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public SqlConnection connect(String conString)
    {
        // Read the connection string from the configuration file
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();
        string cStr = configuration.GetConnectionString("myProjDB");
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }

    public int InsertParent(Parent parent)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB");
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        cmd = CreateParentInsertCommandWithStoredProcedure("SPproj_InsertParent", con, parent);

        try
        {
            int numEffected = cmd.ExecuteNonQuery();
            return numEffected;
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public List<Parent> GetParent()
    {
        SqlConnection con;
        SqlCommand cmd;
        List<Parent> parentList = new List<Parent>();

        try
        {
            con = connect("myProjDB");
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        cmd = CreateCommandWithSPWithoutParametersPar("SPproj_GetParent", con);

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Parent parent = new Parent();
                parent.Stu_parentName = dataReader["fullname"].ToString();
                parent.Stu_parentCell = dataReader["cellphone"].ToString();
                parent.Stu_parentCity = dataReader["city"].ToString();
                parent.Stu_parentStreet = dataReader["street"].ToString();
                parent.Stu_parentHomeNum = Convert.ToInt32(dataReader["house_number"]);               

                parentList.Add(parent);
            }
            return parentList;
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }



    public int UpdateParent(Parent parent)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB");
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        cmd = UpdateParentCommandWithStoredProcedure("SPproj_UpdateParent", con, parent);

        try
        {
            int numEffected = cmd.ExecuteNonQuery();
            return numEffected;
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

  

    private SqlCommand CreateParentInsertCommandWithStoredProcedure(string spName, SqlConnection con, Parent parent)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = spName;
        cmd.CommandTimeout = 10;
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@cellphone", parent.Stu_parentCell);
        cmd.Parameters.AddWithValue("@fullname", parent.Stu_parentName);
        cmd.Parameters.AddWithValue("@street", parent.Stu_parentStreet);
        cmd.Parameters.AddWithValue("@house_number", parent.Stu_parentHomeNum);
        cmd.Parameters.AddWithValue("@city", parent.Stu_parentCity);
        cmd.Parameters.AddWithValue("@stu_id", parent.Stu_id);

        return cmd;
    }
    private SqlCommand CreateCommandWithSPWithoutParametersPar(String spName, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        return cmd;
    }

    private SqlCommand UpdateParentCommandWithStoredProcedure(string spName, SqlConnection con, Parent parent)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = spName;
        cmd.CommandTimeout = 10;
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@cellphone", parent.Stu_parentCell);
        cmd.Parameters.AddWithValue("@fullname", parent.Stu_parentName);
        cmd.Parameters.AddWithValue("@street", parent.Stu_parentStreet);
        cmd.Parameters.AddWithValue("@house_number", parent.Stu_parentHomeNum);
        cmd.Parameters.AddWithValue("@city", parent.Stu_parentCity);
        return cmd;
    }


}

