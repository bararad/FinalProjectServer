using final_proj.BL;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.Json;

namespace final_proj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Transportation_LineController : ControllerBase
    {

        [HttpGet("specificLineData")]
        //this function gets line code from the client and return with Ad Hoc the Latitude and Longitude of all the stations.
        public object getlocat(int linecod)
        {
            TransportationLine tr = new TransportationLine();
            return tr.ReadByLineCode(linecod);
        }




        [HttpGet("LineRouteInfo")]
        //this function gets line code from the client and return with Ad Hoc the Latitude and Longitude of all the stations.
        public List<object> Routeinfo(int linecod)
        {
            TransportationLine tr = new TransportationLine();
            return tr.ReadRouteinfo(linecod);
        }




        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<TransportationLine> lines = TransportationLine.Read();
                return Ok(lines);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

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
                return BadRequest("Foreign key constraint violation: transportation company not found.");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(500, ex.Message);
            }
        }



        [HttpPost("CreateRoute")]
        public ActionResult<int> AddStudentsAndCreateRoute([FromBody] JsonElement data)
        {
            try
            {

                string students = data.GetProperty("students").GetString();
                int linecode = Convert.ToInt32(data.GetProperty("linecode").GetInt32());

                TransportationLine tr = new TransportationLine();
                tr.InsertStudentsAndGetOptimalRoute(students, linecode);




                return Ok(1);
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                // Handle the case where the foreign key constraint is violated
                return BadRequest("Foreign key constraint violation: transportation company or escort not found.");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(500, ex.Message);
            }
        }


    }
}