using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using REST_API_NutriTEC.Models;
using REST_API_NutriTEC.Resources;

namespace REST_API_NutriTEC.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ClientController : ControllerBase
    {

        private readonly Proyecto2Context _context;

        public ClientController(Proyecto2Context context)
        {
            _context = context;
        }

        [HttpPost("auth_client")]
        public async Task<ActionResult<JSON_Object>> AuthClient(Credentials client_credentials)
        {
            JSON_Object json = new JSON_Object("error", null);
            var result = _context.LoginClients.FromSqlInterpolated($"select * from loginclient({client_credentials.email},{Encryption.encrypt_password(client_credentials.password)})");
            var db_result = result.ToList();
            if (db_result.Count == 0)
            {
                return BadRequest(json);
            }
            else
            {
                json.status = "ok";
                json.result = db_result[0];
                return Ok(json);
            }
        }

        [HttpPost("add_client")]
        public async Task<ActionResult<JSON_Object>> AddClient(NewClient new_client)
        {
            DateTime dateTime = Convert.ToDateTime(new_client.birth_date);
            DateOnly dateOnly = DateOnly.FromDateTime(dateTime);
            string dbDate = dateOnly.ToString("yyyy-MM-dd");
            Console.WriteLine("1) " + dbDate);
            DateOnly dateOnly1 = DateOnly.ParseExact(dbDate, "yyyy-MM-dd");

            JSON_Object json = new JSON_Object("error", null);
            var result = _context.AddNewClients.FromSqlInterpolated($"select * from addclient({new_client.name},{new_client.lastname1},{new_client.lastname2},{dateOnly1},{new_client.weight},{new_client.height},{new_client.email},{Encryption.encrypt_password(new_client.password)},{new_client.country},{new_client.calorie})");
            var db_result = result.ToList();
            if (db_result[0].addclient == 1)
            {
                json.status = "ok";
                return Ok(json);
            }
            else
            {
                return BadRequest(json);
            }
        }

        [HttpPost("add_measurement")]
        public async Task<ActionResult<JSON_Object>> AddMeasurement(NewMeasurement newMeasurement)
        {
            DateTime dateTime = Convert.ToDateTime(newMeasurement.date);
            DateOnly dateOnly = DateOnly.FromDateTime(dateTime);
            string dbDate = dateOnly.ToString("yyyy-MM-dd");
            Console.WriteLine("1) " + dbDate);
            DateOnly dateOnly1 = DateOnly.ParseExact(dbDate, "yyyy-MM-dd");

            JSON_Object json = new JSON_Object("error", null);
            var result = _context.AddMeasurements.FromSqlInterpolated($"select * from addmeasurement({newMeasurement.client_id},{dateOnly1},{newMeasurement.weight},{newMeasurement.waist},{newMeasurement.neck},{newMeasurement.hip},{newMeasurement.muscle_percentage},{newMeasurement.fat_percentage})");
            var db_result = result.ToList();
            if (db_result[0].addmeasurement == 1)
            {
                json.status = "ok";
                return Ok(json);
            }
            else
            {
                return BadRequest(json);
            }
        }

        [HttpPost("add_daily_intake")]
        public async Task<ActionResult<JSON_Object>> AddDailyIntake(NewDailyIntake newDailyIntake)
        {
            DateTime dateTime = Convert.ToDateTime(newDailyIntake.date);
            DateOnly dateOnly = DateOnly.FromDateTime(dateTime);
            string dbDate = dateOnly.ToString("yyyy-MM-dd");
            Console.WriteLine("1) " + dbDate);
            DateOnly dateOnly1 = DateOnly.ParseExact(dbDate, "yyyy-MM-dd");

            JSON_Object json = new JSON_Object("error", null);

            foreach (var item in newDailyIntake.consumption)
            {
                var result = _context.AddDailyIntakes.FromSqlInterpolated($"select * from add_daily_intake({newDailyIntake.client_id},{item.dish_name},{dateOnly1},{item.food_time},{item.serving})");
                var db_result = result.ToList() ;
                if (db_result[0].add_daily_intake == 1)
                {
                    json.status = "ok";
                    
                }
                
            }
            if(json.status == "ok")
            {
                return Ok(json);
            }
            else
            {
                return BadRequest(json);
            }

           

        }

       


    }
}
