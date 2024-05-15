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
using System.Text.Json;

public class DBservicesTransportation_Line : GeneralDBservices
{
    public DBservicesTransportation_Line() : base()
    {

    }

    //--------------------------------------------------------------------------------------------------
    // This method Inserts a TransportationLine to the TransportationLine table 
    //--------------------------------------------------------------------------------------------------
    public int InsertTransportationLine(TransportationLine tl)
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

        cmd = CreateTransportation_LineInsertCommandWithStoredProcedure("SPproj_InsertTransportationLine", con, tl);        // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
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


    public int InsertStudentsToLine(string ids, int linecode)
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

        cmd = StudentsinLineInsertionCommandWithStoredProcedure("SPproj_InsertStudentToLine", con, ids, linecode);// create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
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


    //in this function there is a use of AdHoc objects to store the location points of the students in the line 
    public List<Point> GetAdressfromParent(int linecode)
    {
        SqlConnection con;
        SqlCommand cmd;
        List<Point> waypoints = new List<Point>();

        try
        {
            con = connect("myProjDB");
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        cmd = getlocationparentCommandWithStoredProcedure("SPproj_GetParentasdreespoin", con, linecode);
        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            Point p = new Point();

            while (dataReader.Read())
            {
                p.latitude=Convert.ToDouble(dataReader["lat"]);
                p.longitude = Convert.ToDouble(dataReader["lng"]);
                
                waypoints.Add(p);
            }
            //create a List that contains the locations array of the students in the line 

            return waypoints;
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

    public int UpdateTransportationLine(TransportationLine tl)
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

        cmd = UpdateTransportationLineCommandWithStoredProcedure("SPproj_UpdateTransportationLine", con, tl);        // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
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

    private SqlCommand CreateTransportation_LineInsertCommandWithStoredProcedure(String spName, SqlConnection con, TransportationLine tl)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@line_code", tl.Line_code);
        cmd.Parameters.AddWithValue("@date_of_creation", tl.Definition_date);
        cmd.Parameters.AddWithValue("@car_type", tl.Line_car);
        cmd.Parameters.AddWithValue("@number_of_seats", tl.Number_of_seats);
        cmd.Parameters.AddWithValue("@school_ofline", tl.School_of_line);
        cmd.Parameters.AddWithValue("@station_defination", tl.Station_definition);
        cmd.Parameters.AddWithValue("@responsible_escort", tl.Escort_incharge);
        cmd.Parameters.AddWithValue("@transportation_company", tl.Transportation_company);
        cmd.Parameters.AddWithValue("@time_of_line", tl.Time_of_line);
        cmd.Parameters.AddWithValue("@comments", tl.Comments);

        return cmd;

    }

    private SqlCommand getlocationparentCommandWithStoredProcedure(String spName, SqlConnection con, int linecode)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@lineCode", linecode);
        return cmd;

    }

    private SqlCommand StudentsinLineInsertionCommandWithStoredProcedure(String spName, SqlConnection con, string ids, int linecode)
    {
        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@linecode", linecode);
        cmd.Parameters.AddWithValue("@ids", ids);


        return cmd;
    }

    private SqlCommand UpdateTransportationLineCommandWithStoredProcedure(String spName, SqlConnection con, TransportationLine tl)
    {
        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@line_code", tl.Line_code);
        cmd.Parameters.AddWithValue("@date_of_creation", tl.Definition_date);
        cmd.Parameters.AddWithValue("@car_type", tl.Line_car);
        cmd.Parameters.AddWithValue("@number_of_seats", tl.Number_of_seats);
        cmd.Parameters.AddWithValue("@school_ofline", tl.School_of_line);
        cmd.Parameters.AddWithValue("@station_defination", tl.Station_definition);
        cmd.Parameters.AddWithValue("@responsible_escort", tl.Escort_incharge);
        cmd.Parameters.AddWithValue("@transportation_company", tl.Transportation_company);
        cmd.Parameters.AddWithValue("@time_of_line", tl.Time_of_line);
        cmd.Parameters.AddWithValue("@comments", tl.Comments);
        return cmd;
    }


}