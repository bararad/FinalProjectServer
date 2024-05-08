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

public class DBservicesStudent
{
    public DBservicesStudent()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //--------------------------------------------------------------------------------------------------
    // This method creates a connection to the database according to the connectionString name in the web.config 
    //--------------------------------------------------------------------------------------------------
    public SqlConnection connect(String conString)
    {

        // read the connection string from the configuration file
        IConfigurationRoot configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json").Build();
        string cStr = configuration.GetConnectionString("myProjDB");
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }


    public int InsertStudent(Student student)
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

        cmd = CreateStudentInsertCommandWithStoredProcedure("SPproj_InsertStudent", con, student);

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
    public List<Student> GetStudent()
    {
        SqlConnection con;
        SqlCommand cmd;
        List<Student> studentList = new List<Student>();

        try
        {
            con = connect("myProjDB");
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        cmd = CreateCommandWithSPWithoutParametersStu("SPproj_GetStudent", con);

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Student student = new Student();
                student.Stu_fullName = dataReader["stu_fullName"].ToString();
                student.Stu_id = dataReader["stu_id"].ToString();
                student.Stu_dateofbirth = Convert.ToDateTime(dataReader["stu_dateofbirth"]);
                student.Stu_grade = dataReader["stu_grade"].ToString();
                student.Stu_school = dataReader["stu_school"].ToString();
                student.Stu_dateOfPlacement = Convert.ToDateTime(dataReader["Stu_dateOfPlacement"]);
                student.Stu_disability = dataReader["stu_disability"].ToString();
                student.Stu_comments = dataReader["stu_comments"].ToString();
                studentList.Add(student);
            }



            return studentList;
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

    public List<Parent> FindStudentParents(string stu_id)
    {
        SqlConnection con;
        SqlCommand cmd;
        List<Parent> parents = new List<Parent>();

        try
        {
            con = connect("myProjDB");
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        try
        {

            cmd = FindStudentParentProc("FindMyParents", con, stu_id);

            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Parent p1 = new Parent();
                p1.Stu_parentCell = dataReader["cellphone"].ToString();
                p1.Stu_parentName = dataReader["fullname"].ToString();
                p1.Stu_parentCity = dataReader["city"].ToString();
                p1.Stu_parentStreet = dataReader["street"].ToString();
                p1.Stu_parentHomeNum = Convert.ToInt32(dataReader["house_number"]);
                parents.Add(p1);
            }

            return parents;
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


    public int UpdateStudent(Student student)
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

        cmd = UpdateStudentCommandWithStoredProcedure("SPproj_UpdateStudent", con, student);

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
    private SqlCommand CreateCommandWithSPWithoutParametersStu(String spName, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        return cmd;
    }

    private SqlCommand UpdateStudentCommandWithStoredProcedure(string spName, SqlConnection con, Student student)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = spName;
        cmd.CommandTimeout = 10;
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@stu_fullName", student.Stu_fullName);
        cmd.Parameters.AddWithValue("@stu_id", student.Stu_id);
        cmd.Parameters.AddWithValue("@stu_dateofbirth", student.Stu_dateofbirth);
        cmd.Parameters.AddWithValue("@stu_grade", student.Stu_grade);
        cmd.Parameters.AddWithValue("@stu_school", student.Stu_school);
        cmd.Parameters.AddWithValue("@stu_dateOfPlacement", student.Stu_dateOfPlacement);
        cmd.Parameters.AddWithValue("@stu_disability", student.Stu_disability);
        cmd.Parameters.AddWithValue("@stu_comments", student.Stu_comments);
        return cmd;
    }

    private SqlCommand FindStudentParentProc(string spName, SqlConnection con, string id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = spName;
        cmd.CommandTimeout = 10;
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@stu_id", id);

        return cmd;
    }

    private SqlCommand CreateStudentInsertCommandWithStoredProcedure(string spName, SqlConnection con, Student student)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = spName;
        cmd.CommandTimeout = 10;
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@stu_fullName", student.Stu_fullName);
        cmd.Parameters.AddWithValue("@stu_id", student.Stu_id);
        cmd.Parameters.AddWithValue("@stu_dateofbirth", student.Stu_dateofbirth);
        cmd.Parameters.AddWithValue("@stu_grade", student.Stu_grade);
        cmd.Parameters.AddWithValue("@stu_school", student.Stu_school);
        cmd.Parameters.AddWithValue("@stu_dateOfPlacement", student.Stu_dateOfPlacement);
        cmd.Parameters.AddWithValue("@stu_disability", student.Stu_disability);
        cmd.Parameters.AddWithValue("@stu_comments", student.Stu_comments);
        return cmd;
    }


}
