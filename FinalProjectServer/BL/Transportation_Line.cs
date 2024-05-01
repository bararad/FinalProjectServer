namespace final_proj.BL
{
    public class TransportationLine
    {
        private int line_code;
        private DateTime definition_date;
        private string line_car;
        private int number_of_seats;
        private string school_of_line;
        private string station_definition;
        private string escort_incharge;
        private string transportation_company;
        private DateTime time_of_line;
        private string comments;

        public TransportationLine(int line_code, DateTime definition_date, string line_car, int number_of_seats, string school_of_line, string station_definition, string escort_incharge, string transportation_company, DateTime time_of_line, string comments)
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
        }

        public int Line_code { get => line_code; set => line_code = value; }
        public DateTime Definition_date { get => definition_date; set => definition_date = value; }
        public string Line_car { get => line_car; set => line_car = value; }
        public int Number_of_seats { get => number_of_seats; set => number_of_seats = value; }
        public string School_of_line { get => school_of_line; set => school_of_line = value; }
        public string Station_definition { get => station_definition; set => station_definition = value; }
        public string Escort_incharge { get => escort_incharge; set => escort_incharge = value; }
        public string Transportation_company { get => transportation_company; set => transportation_company = value; }
        public DateTime Time_of_line { get => time_of_line; set => time_of_line = value; }
        public string Comments { get => comments; set => comments = value; }


        public int Insert()
        {
            DBservicesTransportation_Line dbs = new DBservicesTransportation_Line();
            return dbs.InsertTransportationCompany(this);
        }

        public int Update()
        {
            DBservicesTransportation_Line dbs = new DBservicesTransportation_Line();
            return dbs.UpdateTransportationLine(this);
        }






    }





}