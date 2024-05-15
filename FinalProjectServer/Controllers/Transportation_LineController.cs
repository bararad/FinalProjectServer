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
        [HttpGet]
        //this function gets line code from the client and return with Ad Hoc the Latitude and Longitude of all the stations.
        public object getlocat(int linecod)
        {
            TransportationLine tr = new TransportationLine();
            return tr.GetLinelocation(linecod);
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
                // Store the student IDs in the database
                string ids = "212042422,314649344,205050555";
                //לשלוח את המידע למסד הנתונים ולשמור אותו בטבלת קשר תלמיד בקו

                //לקבל בחזרה את רשימת הכתובות של כל התלמידים ששוייכו לקו
                //לפנות לטומטום וליצור מסלול
                int linecode = 19;
                // Get the transportation line location data
                TransportationLine tr = new TransportationLine();
                var lineData= tr.GetLinelocation(linecode);
                List<object> locationsArr = lineData.locationsArr;

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