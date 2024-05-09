using final_proj.BL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace final_proj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EscortController : ControllerBase
    {
        // GET: api/<EscortController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<Escort> escorts = Escort.Read();
                return Ok(escorts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<EscortController>
        [HttpPost]
        public IActionResult Post([FromBody] Escort escort)
        {
            try
            {
                int result = escort.Insert();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<EscortController>
        [HttpPut()]
        public IActionResult Put([FromBody] Escort escort)
        {
            try
            {
                int result = escort.Update();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<EscortController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                Escort escort = new Escort();
                int result = escort.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
