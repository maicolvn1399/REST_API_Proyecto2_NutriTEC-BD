using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REST_API_NutriTEC.Models;
using REST_API_NutriTEC.Resources;

namespace REST_API_NutriTEC.Controllers
{
    [ApiController]
    [Route("api/")]
    public class NutritionistController : ControllerBase
    {

        private readonly Proyecto2Context _context;

        public NutritionistController(Proyecto2Context context)
        {
            _context = context;
        }

        [HttpPost("auth_nutritionist")]
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

        [HttpPost("add_nutritionist")]
        public async Task<ActionResult<JSON_Object>> AddNutritionist(NewNutritionist new_nutritionist)
        {
            DateTime dateTime = Convert.ToDateTime(new_nutritionist.BirthDate);
            DateOnly dateOnly = DateOnly.FromDateTime(dateTime);
            string dbDate = dateOnly.ToString("yyyy-MM-dd");
            Console.WriteLine("1) " + dbDate);
            DateOnly dateOnly1 = DateOnly.ParseExact(dbDate, "yyyy-MM-dd");
            Console.WriteLine("2) " + dateOnly1);
            


            Console.WriteLine("executing.....");
            JSON_Object json = new JSON_Object("error", null);
            Console.WriteLine($"select * from createnutritionist('{new_nutritionist.NutritionistId}','{new_nutritionist.NutritionistName}','{new_nutritionist.Lastname1}','{new_nutritionist.Lastname2}','{new_nutritionist.Address}','{new_nutritionist.Photo}','{new_nutritionist.CreditCard}',{new_nutritionist.Weight},{new_nutritionist.Height},'{new_nutritionist.Email}','{new_nutritionist.Pass}',{dateOnly1},'{new_nutritionist.BillingId}','{new_nutritionist.RoleId}')");
            var result = _context.AddNutritionists.FromSqlInterpolated($"select * from createnutritionist({new_nutritionist.NutritionistId},{new_nutritionist.NutritionistName},{new_nutritionist.Lastname1},{new_nutritionist.Lastname2},{new_nutritionist.Address},{new_nutritionist.Photo},{new_nutritionist.CreditCard},{new_nutritionist.Weight},{new_nutritionist.Height},{new_nutritionist.Email},{new_nutritionist.Pass},{dateOnly1},{new_nutritionist.BillingId},{new_nutritionist.RoleId})");
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
    }
}
