﻿using final_proj.BL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace final_proj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        // GET: api/<PartnerController>
        [HttpGet]
        public IEnumerable<Parent> Get()
        {
            return Parent.Read();
        }

        // GET api/<PartnerController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PartnerController>
        [HttpPost]////changed.....why do we need it at all?
        public int Post([FromBody] Parent parent)
        {
            return 0;
        }

        // PUT api/<PartnerController>/5
        [HttpPut()]
        public int Put([FromBody] Parent parent)
        {
            return parent.Update();
        }

        // DELETE api/<PartnerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
