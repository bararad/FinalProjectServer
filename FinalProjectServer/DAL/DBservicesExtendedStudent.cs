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

  public class DBservicesExtendedStudent: GeneralDBservices
{
    public DBservicesExtendedStudent():base()
    {
        
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

        cmd = CreateCommandWithSPWithoutParameters("SPproj_GetExtendedStudent", con);

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
   

}    

