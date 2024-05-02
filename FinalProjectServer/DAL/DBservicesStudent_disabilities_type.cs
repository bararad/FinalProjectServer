using final_proj.BL;
using System.Data;
using System.Data.SqlClient;

namespace final_proj.DAL
{
    public class DBservicesStudent_disabilities_type
    {
        public DBservicesStudent_disabilities_type()
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


        public List<Student_disabilities_type> GetStudent_disabilities_type()

        {
            SqlConnection con;
            SqlCommand cmd;
            List<Student_disabilities_type> disabilitiesTypeList = new List<Student_disabilities_type>();

            try
            {
                con = connect("myProjDB");
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            cmd = CreateCommandWithSPWithoutParametersEs("SPproj_GetStudentType", con);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    Student_disabilities_type disabilitiesType = new Student_disabilities_type();
                    disabilitiesType.TypeCode = dataReader["type_code"].ToString();
                    disabilitiesType.TypeDescription = dataReader["type_description"].ToString();

                    disabilitiesTypeList.Add(disabilitiesType);
                }
                return disabilitiesTypeList;
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
        private SqlCommand CreateCommandWithSPWithoutParametersEs(String spName, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            return cmd;
        }


    }
}
