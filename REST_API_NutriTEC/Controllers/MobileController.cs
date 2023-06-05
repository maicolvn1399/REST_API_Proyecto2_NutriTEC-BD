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

        [HttpPut("auth_nutritionist_mobile")]
        public async Task<ActionResult<JSON_Object>> AuthNutritionist(Credentials nutritionist_credentials)
        {
            JSON_Object json = new JSON_Object("error", null);
            var result = _context.LoginNutritionists.FromSqlInterpolated($"select * from loginnutritionist({nutritionist_credentials.email},{Encryption.encrypt_password(nutritionist_credentials.password)})");
            var db_result = result.ToList();
            //Retorno de una tabla se valida de esta forma
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

        [HttpPut("add_nutritionist_mobile")]
        public async Task<ActionResult<JSON_Object>> AddNutritionist(NewNutritionist new_nutritionist)
        {
            DateTime dateTime = Convert.ToDateTime(new_nutritionist.birth_date);
            DateOnly dateOnly = DateOnly.FromDateTime(dateTime);
            string dbDate = dateOnly.ToString("yyyy-MM-dd");
            Console.WriteLine("1) " + dbDate);
            DateOnly dateOnly1 = DateOnly.ParseExact(dbDate, "yyyy-MM-dd");
            Console.WriteLine("2) " + dateOnly1);



            Console.WriteLine("executing.....");
            JSON_Object json = new JSON_Object("error", null);
            string rol = "Nutritionist";
            //Console.WriteLine($"select * from createnutritionist('{new_nutritionist.id}','{new_nutritionist.name}','{new_nutritionist.Lastname1}','{new_nutritionist.Lastname2}','{new_nutritionist.Address}','{new_nutritionist.Photo}','{new_nutritionist.CreditCard}',{new_nutritionist.Weight},{new_nutritionist.Height},'{new_nutritionist.Email}','{new_nutritionist.Pass}',{dateOnly1},'{new_nutritionist.BillingId}','{new_nutritionist.RoleId}')");
            var result = _context.AddNutritionists.FromSqlInterpolated($"select * from createnutritionist({new_nutritionist.id},{new_nutritionist.name},{new_nutritionist.lastname_1},{new_nutritionist.lastname_2},{new_nutritionist.address},{new_nutritionist.photo},{new_nutritionist.credit_card},{new_nutritionist.weight},{new_nutritionist.height},{new_nutritionist.email},{Encryption.encrypt_password(new_nutritionist.password)},{dateOnly1},{new_nutritionist.payment_type},{rol})");
            var db_result = result.ToList();
            //Checa si se ejecuto exitosamente el query de la función
            if (db_result[0].createnutritionist == 1)
            {
                json.status = "ok";
                return Ok(json);
            }
            else
            {
                return BadRequest(json);
            }

        }

        [HttpPut("add_measurement_mobile")]
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

        [HttpPut("add_daily_intake_mobile")]
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
                var result = _context.AddDailyIntakes.FromSqlInterpolated($"select * from add_daily_intake({newDailyIntake.client_id},{item.product},{dateOnly1},{item.food_time},{item.size})");
                var db_result = result.ToList();
                if (db_result[0].add_daily_intake == 1)
                {
                    json.status = "ok";

                }

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


        [HttpPut("search_dish")]
        public async Task<ActionResult<JSON_Object>> Search_Dish(Product_ID _Entry)
        {
            JSON_Object json = new JSON_Object("error", null);

            var result = _context.Search_products.FromSqlInterpolated($"select * from search_product({_Entry.product})");
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
}
