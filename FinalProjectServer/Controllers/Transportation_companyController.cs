using final_proj.BL;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace final_proj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportationCompanyController : ControllerBase
    {
        // GET: api/TransportationCompany
        [HttpGet]
        public IEnumerable<TransportationCompany> Get()
        {
            return TransportationCompany.Read();
        }

        [HttpPost]
        public ActionResult<int> Post([FromBody] TransportationCompany company)
        {
            
            var result = company.Insert();
            if (result == 0)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPut()]
        public int Put([FromBody] TransportationCompany ed)
        {
            return ed.Update();
        }

        [HttpDelete("{id}")]
        public int Delete(string id)
        {
            TransportationCompany u = new TransportationCompany();
            return u.Delete(id);

        }
    }
}