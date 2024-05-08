using final_proj.BL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace final_proj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        // GET: api/<StudentController>
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return Student.Read();
        }

        // POST api/<StudentController>
        [HttpPost]
        public int Post([FromBody] Student student)
        {
            return student.Insert();
        }

        // PUT api/<StudentController>/5
        [HttpPut()]
        public int Put([FromBody] Student student)
        {
            return student.Update();
        }


        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
