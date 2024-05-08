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

  public class DBservicesExtendedStudent
  {
    public DBservicesExtendedStudent()
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
    public List<ExtendedStudent> GetExtendedStudent()
    {
        SqlConnection con;
        SqlCommand cmd;
        List<ExtendedStudent> extendedstudentList = new List<ExtendedStudent>();

        try
        {
            con = connect("myProjDB");
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        cmd = CreateCommandWithSPWithoutParametersExStu("SPproj_GetExtendedStudent", con);

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                ExtendedStudent extendedstu = new ExtendedStudent();
                
                //add student to complexed object
                extendedstu.Student.Stu_school= dataReader["stu_school"].ToString();
                extendedstu.Student.Stu_fullName= dataReader["stu_fullName"].ToString();
                extendedstu.Student.Stu_id= dataReader["stu_id"].ToString();
                extendedstu.Student.Stu_dateOfPlacement= Convert.ToDateTime(dataReader["Stu_dateOfPlacement"]);
                extendedstu.Student.Stu_dateofbirth= Convert.ToDateTime(dataReader["stu_dateofbirth"]);
                extendedstu.Student.Stu_disability= dataReader["stu_disability"].ToString();
                extendedstu.Student.Stu_comments= dataReader["stu_comments"].ToString();
                extendedstu.Student.Stu_grade= dataReader["stu_grade"].ToString();

                //add parent/s 
                Parent p1 = new Parent();
                Parent p2 = new Parent();
                
                p1.Stu_parentCell= dataReader["cellphone"].ToString();
                p1.Stu_parentName= dataReader["fullname"].ToString();
                p1.Stu_parentCity = dataReader["city"].ToString();
                p1.Stu_parentStreet = dataReader["street"].ToString();
                p1.Stu_parentHomeNum = Convert.ToInt32(dataReader["house_number"]);
                extendedstu.Parents.Add(p1);
                if (dataReader["p2_cellphone"].ToString()!=null)
                {
                    p2.Stu_parentCell = dataReader["p2_cellphone"].ToString();
                    p2.Stu_parentName = dataReader["p2_fullname"].ToString();
                    p2.Stu_parentCity = dataReader["p2_city"].ToString();
                    p2.Stu_parentStreet = dataReader["p2_street"].ToString();
                    p2.Stu_parentHomeNum = Convert.ToInt32(dataReader["p2_house_number"]);
                    extendedstu.Parents.Add(p2);
                }
        

                extendedstudentList.Add(extendedstu);
            }
            return extendedstudentList;
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
    private SqlCommand CreateCommandWithSPWithoutParametersExStu(String spName, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        return cmd;
    }

}    

