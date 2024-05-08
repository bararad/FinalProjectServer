namespace final_proj.BL
{
    public class Parent
    {
        private string stu_parentName;
        private string stu_parentCell;
        private string stu_parentCity;
        private string stu_parentStreet;
        private int stu_parentHomeNum;
        private string stu_id;


        public Parent() { }
        public Parent(string stu_parentName, string stu_parentCell, string stu_parentCity, string stu_parentStreet, int stu_parentHomeNum, string stu_id)
        {
            Stu_parentName = stu_parentName;
            Stu_parentCell = stu_parentCell;
            Stu_parentCity = stu_parentCity;
            Stu_parentStreet = stu_parentStreet;
            Stu_parentHomeNum = stu_parentHomeNum;
            Stu_id = stu_id;
        }



        public string Stu_parentName { get => stu_parentName; set => stu_parentName = value; }
        public string Stu_parentCell { get => stu_parentCell; set => stu_parentCell = value; }
        public string Stu_parentCity { get => stu_parentCity; set => stu_parentCity = value; }
        public string Stu_parentStreet { get => stu_parentStreet; set => stu_parentStreet = value; }
        public int Stu_parentHomeNum { get => stu_parentHomeNum; set => stu_parentHomeNum = value; }
        public string Stu_id { get => stu_id; set => stu_id = value; }

        public int Insert()
        {
            DBservicesParent dbs = new DBservicesParent();
            return dbs.InsertParent(this);
        }

        public static List<Parent> Read()
        {
            DBservicesParent dbs = new DBservicesParent();
            return dbs.GetParent();
        }

        public int Update()
        {
            DBservicesParent dbs = new DBservicesParent();
            return dbs.UpdateParent(this);
        }
    }
}
