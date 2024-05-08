namespace final_proj.BL
{
    public class ExtendedStudent
    {
        Student student;
        Parent[] parents;

        public ExtendedStudent()
        {

        }
        public ExtendedStudent(Student student, Parent[] parents)
        {
            Student = student;
            Parents = parents;
        }

        public Student Student { get => student; set => student = value; }
        public Parent[] Parents { get => parents; set => parents = value; }

        public int Insert()
        {

            int res = Student.Insert();
            foreach (Parent p in parents)
            {
                res += p.Insert();
            }
            return res;

        }


        //public static List<Parent> Read()
        //{
    
        //}

        //public int Update()
        //{
           
        //}


    }
}
