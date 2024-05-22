using System.Text.Json;
using System.Text.Json.Nodes;
using System.Globalization;
using System.Reflection;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace final_proj.BL
{
    public class TransportationLine
    {
        static readonly HttpClient client = new HttpClient();

        private int line_code;
        private DateTime definition_date;
        private string line_car;
        private int number_of_seats;
        private string school_of_line;
        private string station_definition;
        private string escort_incharge;
        private string transportation_company;
        private string time_of_line;
        private string comments;
        private List<string> studentsId;

        public TransportationLine()
        {
        }

        public TransportationLine(int line_code, DateTime definition_date, string line_car, int number_of_seats, string school_of_line, string station_definition, string escort_incharge, string transportation_company, string time_of_line, string comments)
        {
            this.line_code = line_code;
            this.definition_date = definition_date;
            this.line_car = line_car;
            this.number_of_seats = number_of_seats;
            this.school_of_line = school_of_line;
            this.station_definition = station_definition;
            this.escort_incharge = escort_incharge;
            this.transportation_company = transportation_company;
            this.time_of_line = time_of_line;
            this.comments = comments;
            StudentsId = new List<string>();
        }


        public int Line_code { get => line_code; set => line_code = value; }
        public DateTime Definition_date { get => definition_date; set => definition_date = value; }
        public string Line_car { get => line_car; set => line_car = value; }
        public int Number_of_seats { get => number_of_seats; set => number_of_seats = value; }
        public string School_of_line { get => school_of_line; set => school_of_line = value; }
        public string Station_definition { get => station_definition; set => station_definition = value; }
        public string Escort_incharge { get => escort_incharge; set => escort_incharge = value; }
        public string Transportation_company { get => transportation_company; set => transportation_company = value; }
        public string Time_of_line { get => time_of_line; set => time_of_line = value; }
        public string Comments { get => comments; set => comments = value; }
        public List<string> StudentsId { get => studentsId; set => studentsId = value; }

        public int Insert()
        {
            DBservicesTransportation_Line dbs = new DBservicesTransportation_Line();
            return dbs.InsertTransportationLine(this);
        }

        public int Update()
        {
            DBservicesTransportation_Line dbs = new DBservicesTransportation_Line();
            return dbs.UpdateTransportationLine(this);
        }

        public static List<TransportationLine> Read()
        {
            DBservicesTransportation_Line dbs = new DBservicesTransportation_Line();
            List<TransportationLine> lineList = dbs.GetLines();

            foreach (TransportationLine line in lineList)
            {
                line.StudentsId = dbs.GetStudentsInLine(line.Line_code);
            }

            return lineList;
        }

        public object ReadByLineCode(int linecode)
        {
            DBservicesTransportation_Line dbs = new DBservicesTransportation_Line();
            List<string> stuid = new List<string>();
            stuid = dbs.GetStudentsInLine(linecode);

            TransportationLine mytl = new TransportationLine();
            mytl = dbs.gettransportaiondetail(linecode);
            var studeentsofline = new { transportaionline = mytl, studentid = stuid };
            return studeentsofline;
        }


        public static async Task<List<Point>> CreateOptimalRoute(List<Point> waypoints)
        {
            try
            {

                string url = "https://api.tomtom.com/routing/waypointoptimization/1?key=pQ2wOkN7gW5AktUC12urg6Z2M8lkiIFH";

                JArray waypointsToSend = new JArray();

                foreach (Point point in waypoints)
                {
                    //defines a JSON object
                    JObject o = new JObject();
                    //creates a key value sturcture in JSON according to token
                    o.Add("point", JToken.FromObject(point));
                    waypointsToSend.Add(o);
                }


                JObject obj = new JObject();
                obj["waypoints"] = waypointsToSend;


                HttpClient client = new HttpClient();
                var response = await client.PostAsync(url, new StringContent(obj.ToString().Replace("{{", "{").Replace("}}", "}"), Encoding.UTF8, "application/json"));
                string responseBody = await response.Content.ReadAsStringAsync();

                //parse to JSON object to reach the exact field of the optimized route
                JObject res = JObject.Parse(responseBody);
                List<int> order = JsonConvert.DeserializeObject<List<int>>(res["optimizedOrder"].ToString());

                List<Point> optimizedPoints = new List<Point>();
                foreach (int location in order)
                {
                    optimizedPoints.Add(waypoints[location]);
                }
                
                return optimizedPoints;

                //add to the DB

            }
            catch (Exception)
            {

                throw;
            }

        }

        public Task<List<Point>> InsertStudentsAndGetOptimalRoute(string students, int linecode)
        {
            DBservicesTransportation_Line dbs = new DBservicesTransportation_Line();
            //add all students to line
            int numaffected = dbs.InsertStudentsToLine(students, linecode);

            //get all adresses of students in line
            List<Point> waypoints = dbs.GetAdressfromParent(linecode);
            return CreateOptimalRoute(waypoints);

        }






    }





}