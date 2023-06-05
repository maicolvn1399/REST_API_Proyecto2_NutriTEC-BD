using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using REST_API_NutriTEC.Models;
using REST_API_NutriTEC.Resources;
using System.Linq.Expressions;

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
        [HttpPost("get_nutritionist_clients")]
        public async Task<ActionResult<JSON_Object>> GetNutriClients(NutriID _Entry)
        {
            JSON_Object json = new JSON_Object("error", null);
            var result = _context.Get_nutritionist_clientss.FromSqlInterpolated($"select * from get_nutritionist_clients({_Entry.nutritionist_id})");
            var db_result = result.ToList();
            //Retorno de una tabla se valida de esta forma
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
        [HttpPost("get_nutritionist_plans")]
        public async Task<ActionResult<JSON_Object>> GetNutriPlans(NutriID _Entry)
        {
            JSON_Object json = new JSON_Object("error", null);
            var result = _context.Get_nutritionist_planss.FromSqlInterpolated($"select * from get_nutritionist_plans({_Entry.nutritionist_id})");
            var db_result = result.ToList();
            //Retorno de una tabla se valida de esta forma
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
        [HttpPost("assign_plan")]
        public async Task<ActionResult<JSON_Object>> AssignPlan(PlanAssigner _Entry)
        {
            DateTime dateTime = Convert.ToDateTime(_Entry.start_date);
            DateOnly dateOnly = DateOnly.FromDateTime(dateTime);
            string dbDate = dateOnly.ToString("yyyy-MM-dd");
            DateOnly dateOnly1 = DateOnly.ParseExact(dbDate, "yyyy-MM-dd");

            DateTime dateTime1 = Convert.ToDateTime(_Entry.end_date);
            DateOnly dateOnly11 = DateOnly.FromDateTime(dateTime1);
            string dbDate1 = dateOnly11.ToString("yyyy-MM-dd");
            DateOnly dateOnly111 = DateOnly.ParseExact(dbDate1, "yyyy-MM-dd");


            JSON_Object json = new JSON_Object("error", null);
            var result = _context.Assign_plans.FromSqlInterpolated($"select * from assign_plan({_Entry.plan_name},{dateOnly1},{dateOnly111},{_Entry.email_client})");
            var db_result = result.ToList();
            //Retorno de una tabla se valida de esta forma
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
        [HttpPost("create_plan")]
        public async Task<ActionResult<JSON_Object>> CreatePlan(Plan_Create _Entry)
        {
            Console.WriteLine("executing.....");
            JSON_Object json = new JSON_Object("error", null);
            var result = _context.Create_plans.FromSqlInterpolated($"select * from create_plan({_Entry.plan_name},{_Entry.nutritionist_id})");
            var db_result = result.ToList();
            //Checa si se ejecuto exitosamente el query de la función
            if (db_result[0].create_plan == 1)
            {
                Add_Product_Plan(_Entry.plan, _Entry.plan_name);
                json.status = "ok";
                return Ok(json);
            }
            else
            {
                return BadRequest(json);
            }


        }
        [HttpPost("add_product_to_plan")]
        private async void Add_Product_Plan(List<Plan_dish> dishlist, string plan)
        {
            JSON_Object json = new JSON_Object("error", null);
            foreach (var item in dishlist)
            {
                Console.WriteLine(item);
                var dishresult = _context.Add_dish_to_plans.FromSqlInterpolated($"select * from add_dish_to_plan({plan},{item.product},{item.meal_time},{item.size})");
                var db_result = dishresult.ToList();
                Console.WriteLine(db_result[0].ToString());
            }
        }
        [HttpPost("search_dish")]
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
        [HttpPost("add_dish")]
        public async Task<ActionResult<JSON_Object>> Add_Recipe(New_recipe _Entry)
        {
            JSON_Object json = new JSON_Object("error", null);

            var result = _context.Createrecipes.FromSqlInterpolated($"select * from createrecipe({_Entry.dish_name})");
            var db_result = result.ToList();

            //Checa si se ejecuto exitosamente el query de la función
            if (db_result[0].createrecipe == 1)
            {
                Add_Product_Dish(_Entry.dish_name, _Entry.ingredients);
                json.status = "ok";
                return Ok(json);
            }
            else
            {
                return BadRequest(json);
            }


        }
        [HttpPost("add_product_to_dish")]
        private async void Add_Product_Dish(string name, List<Recipe_product> dishlist)
        {
            JSON_Object json = new JSON_Object("error", null);
            foreach (var item in dishlist)
            {
                Console.WriteLine(item);
                var dishresult = _context.Add_product_to_recipes.FromSqlInterpolated($"select * from add_product_to_recipe({name},{item.size},{item.product})");
                var db_result = dishresult.ToList();
                Console.WriteLine(db_result[0].ToString());
            }
            UpdateValues(name);
        }
        [HttpPost("update_nutri_values")]
        private async void UpdateValues(string name)
        {
            JSON_Object json = new JSON_Object("error", null);
                var dishresult = _context.Update_recipe_valuess.FromSqlInterpolated($"select * from update_recipe_values({name})");
                var db_result = dishresult.ToList();
                Console.WriteLine(db_result[0].ToString());
        }
    }
}
