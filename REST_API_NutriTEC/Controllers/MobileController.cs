using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REST_API_NutriTEC.Models;
using REST_API_NutriTEC.Resources;

namespace REST_API_NutriTEC.Controllers
{
    [ApiController]
    [Route("api/")]
    public class MobileController : ControllerBase
    {
        private readonly Proyecto2Context _context;

        public MobileController(Proyecto2Context context)
        {
            _context = context;
        }

        [HttpGet("auth_client_mobile/{email}/{password}")]
        public async Task<ActionResult<JSON_Object>> AuthClient(string email, string password)
        {
            JSON_Object json = new JSON_Object("error", null);
            var result = _context.LoginClients.FromSqlInterpolated($"select * from loginclient({email},{Encryption.encrypt_password(password)})");
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

        [HttpGet("add_client_mobile/{name}/{lastname1}/{lastname2}/{date}/{weight}/{height}/{email}/{password}/{country}/{calorie_goal}")]
        public async Task<ActionResult<JSON_Object>> AddClient(string name, string lastname1, string lastname2, string date, System.Double weight, System.Double height, string email, string password, string country, int calorie_goal)
        {
            DateTime dateTime = Convert.ToDateTime(date);
            DateOnly dateOnly = DateOnly.FromDateTime(dateTime);
            string dbDate = dateOnly.ToString("yyyy-MM-dd");
            Console.WriteLine("1) " + dbDate);
            DateOnly dateOnly1 = DateOnly.ParseExact(dbDate, "yyyy-MM-dd");

            JSON_Object json = new JSON_Object("error", null);
            var result = _context.AddNewClients.FromSqlInterpolated($"select * from addclient({name},{lastname1},{lastname2},{dateOnly1},{weight},{height},{email},{Encryption.encrypt_password(password)},{country},{calorie_goal})");
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

        [HttpGet("add_measurement_mobile/{email}/{date}/{weight}/{waist}/{neck}/{hip}/{muscle_percentage}/{fat_percentage}")]
        public async Task<ActionResult<JSON_Object>> AddMeasurement(string email, string date, System.Double weight, System.Double waist, System.Double neck, System.Double hip, string muscle_percentage, string fat_percentage)
        {
            DateTime dateTime = Convert.ToDateTime(date);
            DateOnly dateOnly = DateOnly.FromDateTime(dateTime);
            string dbDate = dateOnly.ToString("yyyy-MM-dd");
            Console.WriteLine("1) " + dbDate);
            DateOnly dateOnly1 = DateOnly.ParseExact(dbDate, "yyyy-MM-dd");

            JSON_Object json = new JSON_Object("error", null);
            var result = _context.AddMeasurements.FromSqlInterpolated($"select * from addmeasurement({email},{dateOnly1},{weight},{waist},{neck},{hip},{muscle_percentage},{fat_percentage})");
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

        [HttpGet("add_daily_intake_mobile/{email}/{product}/{date}/{food_time}/{size}")]
        public async Task<ActionResult<JSON_Object>> AddDailyIntake(string email, string product, string date, string food_time, int size)
        {
            DateTime dateTime = Convert.ToDateTime(date);
            DateOnly dateOnly = DateOnly.FromDateTime(dateTime);
            string dbDate = dateOnly.ToString("yyyy-MM-dd");
            Console.WriteLine("1) " + dbDate);
            DateOnly dateOnly1 = DateOnly.ParseExact(dbDate, "yyyy-MM-dd");

            JSON_Object json = new JSON_Object("error", null);

            
            var result = _context.AddDailyIntakes.FromSqlInterpolated($"select * from add_daily_intake({email},{product},{dateOnly1},{food_time},{size})");
            var db_result = result.ToList();
            if (db_result[0].add_daily_intake == 1)
            {
                json.status = "ok";

            }

            
            if (json.status == "ok")
            {
                return Ok(json);
            }
            else
            {
                return BadRequest(json);
            }
        }


        [HttpGet("search_dish_mobile/{product}")]
        public async Task<ActionResult<JSON_Object>> Search_Dish(string product)
        {
            JSON_Object json = new JSON_Object("error", null);

            var result = _context.Search_products.FromSqlInterpolated($"select * from search_product({product})");
            var db_result = result.ToList();

            //Checa si se ejecuto exitosamente el query de la función
            if (db_result.Count == 0)
            {
                return BadRequest(json);
            }
            else
            {
                json.status = "ok";
                json.result = db_result;
                return Ok(json);
            }
        }

        [HttpGet("add_product_mobile/{name}/{size}/{calcium}/{sodium}/{carbs}/{fat}/{calories}/{iron}/{protein}")]
        public async Task<ActionResult<JSON_Object>> Add_Product(string name, int size, System.Double calcium, System.Double sodium, System.Double carbs, System.Double fat, int calories, System.Double iron, System.Double protein)
        {
            JSON_Object json = new JSON_Object("error", null);
            var result = _context.Create_products.FromSqlInterpolated($"select * from createproduct({name},{size},{calcium},{sodium},{carbs},{fat},{calories},{iron},{protein})");
            var db_result = result.ToList();
            //Retorno de una tabla se valida de esta forma
            if (db_result[0].createproduct == 1)
            {
                json.status = "ok";
                return Ok(json);
            }
            else
            {
                return BadRequest(json);
            }
        }

        

    }
}
