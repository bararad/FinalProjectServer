using final_proj.BL;
using Microsoft.AspNetCore.Mvc;


namespace final_proj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationalController : ControllerBase
    {
        // GET: api/<EducationalController>
        [HttpGet]
        public IEnumerable<EducationalInstitution> Get()
        {
            return EducationalInstitution.Read();
        }
        //


        // POST api/<EducationalController>
        [HttpPost]
        public int Post([FromBody] EducationalInstitution ed)
        {
            return ed.Insert();
        }


        [HttpPut()]
        public int Put([FromBody] EducationalInstitution ed)
        {
            return ed.UpdateEducation();
        }

        // DELETE api/<EducationalController>/5
        [HttpDelete("{id}")]
        public int Delete(string id)
        {
            EducationalInstitution u = new EducationalInstitution();
            return u.DeleteEducation(id);

        }
    }
}

