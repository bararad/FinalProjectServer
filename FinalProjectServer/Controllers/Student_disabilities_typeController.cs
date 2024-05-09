using final_proj.BL;
using Microsoft.AspNetCore.Mvc;


namespace final_proj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Student_disabilities_typeController : ControllerBase
    {
        // GET: api/<Student_disabilities_typeController>
        [HttpGet]
        public IEnumerable<Student_disabilities_type> Get()
        {
            return Student_disabilities_type.Read();
        }
        // GET api/<Student_disabilities_typeController>/5
     

    }
}
