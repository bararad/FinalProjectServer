using Microsoft.AspNetCore.Mvc;
using final_proj.BL;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace final_proj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtendedStudentController : ControllerBase
    {
        // GET: api/<ExtendedStudentController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ExtendedStudentController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ExtendedStudentController>
        [HttpPost]
        public int Post([FromBody] ExtendedStudent extendedstudent)
        {
            return extendedstudent.Insert();
        }

        // PUT api/<ExtendedStudentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ExtendedStudentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
