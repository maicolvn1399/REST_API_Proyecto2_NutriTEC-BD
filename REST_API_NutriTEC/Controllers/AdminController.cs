using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REST_API_NutriTEC.Models;
using REST_API_NutriTEC.Resources;

namespace REST_API_NutriTEC.Controllers
{

    [ApiController]
    [Route("api/")]
    public class AdminController : ControllerBase

    {
        private readonly Proyecto2Context _context;

        public AdminController(Proyecto2Context context)
        {
            _context = context;
        }

        [HttpPost("auth_admin")]
        public async Task<ActionResult<JSON_Object>> AuthAdmin(Credentials admin_credentials)
        {
            JSON_Object json = new JSON_Object("error", null);
            var result = _context.LoginAdmins.FromSqlInterpolated($"select * from loginadmin({admin_credentials.email},{Encryption.encrypt_password(admin_credentials.password)})");
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
        [HttpGet("generate_admin_report")]
        public async Task<ActionResult<JSON_Object>> GenerateAdminReport()
        {
            JSON_Object json = new JSON_Object("error", null);
            var result = _context.Calculate_billings.FromSqlInterpolated($"select * from calculate_billing()");
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
        [HttpGet("get_unapproved_products")]
        public async Task<ActionResult<JSON_Object>> GetUnapprovedPducts()
        {
            JSON_Object json = new JSON_Object("error", null);
            var result = _context.GetUnapprovedProducts.FromSqlInterpolated($"select * from get_unapproved_products");
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
        [HttpPost("approve_product")]
        public async Task<ActionResult<JSON_Object>> ApproveProduct(Product_name _Name)
        {

            Console.WriteLine("executing.....");
            JSON_Object json = new JSON_Object("error", null);
            var result = _context.Approve_products.FromSqlInterpolated($"select approve_product({_Name.product_name})");
            var db_result = result.ToList();
            //Checa si se ejecuto exitosamente el query de la función
            if (db_result[0].approve_product == 1)
            {
                json.status = "ok";
                return Ok(json);
            }
            else
            {
                return BadRequest(json);
            }


        }
        [HttpPost("delete_product")]
        public async Task<ActionResult<JSON_Object>> DeleteProduct(Product_name _Name)
        {

            Console.WriteLine("executing.....");
            JSON_Object json = new JSON_Object("error", null);
            var result = _context.Delete_products.FromSqlInterpolated($"select delete_product({_Name.product_name})");
            var db_result = result.ToList();
            //Checa si se ejecuto exitosamente el query de la función
            if (db_result[0].delete_product == 1)
            {
                json.status = "ok";
                return Ok(json);
            }
            else
            {
                return BadRequest(json);
            }


        }
        [HttpPost("delete_plan")]
        public async Task<ActionResult<JSON_Object>> DeletePlan(Plan_name _Name)
        {

            Console.WriteLine("executing.....");
            JSON_Object json = new JSON_Object("error", null);
            var result = _context.Delete_plans.FromSqlInterpolated($"select delete_plan({_Name.plan_name})");
            var db_result = result.ToList();
            //Checa si se ejecuto exitosamente el query de la función
            if (db_result[0].delete_plan == 1)
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
