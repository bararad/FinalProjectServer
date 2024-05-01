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

        //// GET api/TransportationCompany/5
        //[HttpGet("{companyCode}")]
        //public ActionResult<TransportationCompany> Get(string companyCode)
        //{
        //    var transportationCompany = TransportationCompany.GetByCompanyCode(companyCode);
        //    if (transportationCompany == null)
        //    {
        //        return NotFound();
        //    }
        //    return transportationCompany;
        //}

        // POST api/TransportationCompany
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

        // DELETE api/<EducationalController>/5
        [HttpDelete("{id}")]
        public int Delete(string id)
        {
            TransportationCompany u = new TransportationCompany();
            return u.Delete(id);

        }
    }
}