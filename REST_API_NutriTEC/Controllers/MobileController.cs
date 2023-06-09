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
        /// <summary>
        /// Context attribute to call the class that manages the database
        /// </summary>
        private readonly Proyecto2Context _context;

        public MobileController(Proyecto2Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to authenticate the client using a mobile app 
        /// </summary>
        /// <param name="email"> refers to the email that the client uses to log in </param>
        /// <param name="password"> refers to the password that the client uses to log in </param>
        /// <returns> returns a JSON with information of the client </returns>
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
        /// <summary>
        /// Method that allows to enter a new client using a mobile app 
        /// </summary>
        /// <param name="name"> refers to client name </param>
        /// <param name="lastname1"> refers to client's lastname1 </param>
        /// <param name="lastname2"> refers to client's lastname2 </param>
        /// <param name="date"> refers to client's birth date </param>
        /// <param name="weight"> refers to client's weight  </param>
        /// <param name="height"> refers to client's height </param>
        /// <param name="email"> refers to client's email </param>
        /// <param name="password"> refers to client's password </param>
        /// <param name="country"> refers to the client's country </param>
        /// <param name="calorie_goal"> refers to client's calorie goal</param>
        /// <returns> returns a JSON confirming if the query was succesful </returns>
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

        /// <summary>
        /// Method to add measurements for a client using a mobile app 
        /// </summary>
        /// <param name="email"> refers to client's email that the measurement is going to be associated to </param>
        /// <param name="date"> refers to client's date of measurement </param>
        /// <param name="weight"> refers to client's weight measurement </param>
        /// <param name="waist"> refers to client's waist measurement </param>
        /// <param name="neck"> refers to client's neck measurement </param>
        /// <param name="hip"> refers to client's hip measurement </param>
        /// <param name="muscle_percentage"> refers to client's msucle percentage </param>
        /// <param name="fat_percentage"> refers to client's fat percentage </param>
        /// <returns></returns>
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

        /// <summary>
        /// Method to add a daily intake of food using a mobile app 
        /// </summary>
        /// <param name="email"> refers to client's email </param>
        /// <param name="product"> refers to the client's product intake </param>
        /// <param name="date"> refers to the date of intake </param>
        /// <param name="food_time">refers to the food time (breakfast, snack, lunch, dinner) </param>
        /// <param name="size"> refers to the serving of the food </param>
        /// <returns></returns>
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

        /// <summary>
        /// Method to search for a food 
        /// </summary>
        /// <param name="product"> refers to the name of the dish </param>
        /// <returns> returns a json with the requested information </returns>
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

        /// <summary>
        /// Method to add the nutritional information of a dish or product 
        /// </summary>
        /// <param name="name"> refers to the product's name </param>
        /// <param name="size"> refers to the product's serving </param>
        /// <param name="calcium"> refers to the calcium contained in the product </param>
        /// <param name="sodium"> refers to the sodium contained in the product </param>
        /// <param name="carbs"> refers to the carbs contained in the product </param>
        /// <param name="fat"> refers to the fat contained in the product</param>
        /// <param name="calories"> refers to the calories contained in the product</param>
        /// <param name="iron"> refers to the iron contained in the product</param>
        /// <param name="protein"> refers to the protein contained in the product</param>
        /// <returns></returns>
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
