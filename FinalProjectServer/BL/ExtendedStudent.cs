namespace final_proj.BL
{
    public class ExtendedStudent
    {
        Student student;
        List<Parent> parents=new List<Parent>();

        public ExtendedStudent()
        {

        }

        public ExtendedStudent(Student student, List<Parent> parents)
        {
            Student = student;
            Parents = parents;
        }

        public Student Student { get => student; set => student = value; }
        
        public List<Parent> Parents { get => parents; set => parents = value; }

        public int Insert()
        {

            int res = Student.Insert();
            foreach (Parent p in Parents)
            {
               
            }
            return res;

        }


        public static List<ExtendedStudent> Read()
        {
            DBservicesExtendedStudent dbs = new DBservicesExtendedStudent();
            return dbs.GetExtendedStudent();
        }

        //public int Update()
        //{

        //}


    }
}
