using final_proj.BL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Student_disabilities_typeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Student_disabilities_typeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Student_disabilities_typeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
