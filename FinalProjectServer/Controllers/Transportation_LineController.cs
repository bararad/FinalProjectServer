using final_proj.BL;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace final_proj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Transportation_LineController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
       

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public ActionResult<int> Post([FromBody] TransportationLine tl)
        {
            try
            {
                var result = tl.Insert();
                return Ok(1);
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                // Handle the case where the foreign key constraint is violated
                // due to the absence of a matching value in the referenced table
                return BadRequest("Foreign key constraint violation: transportation company or escort not found.");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut()]
        public ActionResult<int> Put([FromBody] TransportationLine tl)
        {
            try
            {
                var result = tl.Update();
                return Ok(1);
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                // Handle the case where the foreign key constraint is violated
                // due to the absence of a matching value in the referenced table
                return BadRequest("Foreign key constraint violation: transportation company not found.");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(500, ex.Message);
            }
        }



        // PUT api/<Transportation_LineController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Transportation_LineController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}