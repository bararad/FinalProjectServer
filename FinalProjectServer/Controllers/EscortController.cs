using final_proj.BL;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace final_proj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EscortController : ControllerBase
    {
        // GET: api/<EscortController>
        [HttpGet]
        public IEnumerable<Escort> Get()
        {
            return Escort.Read();
        }



        // POST api/<EscortController>
        [HttpPost]
        public int Post([FromBody] Escort escort)
        {
            return escort.Insert();
        }




        // PUT api/<EscortController>
        [HttpPut()]
        public int Put([FromBody] Escort escort)
        {
            return escort.Update();
        }

        // DELETE api/<EscortController>/5
        [HttpDelete("{id}")]
        public int Delete(string id)
        {
            Escort escort = new Escort();
            return escort.Delete(id);
        }
    }
}