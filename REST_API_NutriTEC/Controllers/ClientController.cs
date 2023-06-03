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
        [HttpPost("search_patient")]
        public async Task<ActionResult<JSON_Object>> PatientSearch(Client_name _Name)
        {
            JSON_Object json = new JSON_Object("error", null);
            var result = _context.searchclients.FromSqlInterpolated($"select * from searchclient({_Name.client_name})");
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
        [HttpPost("associate_client")]
        public async Task<ActionResult<JSON_Object>> AssociateClient(Associate_Client _Client)
        {
            JSON_Object json = new JSON_Object("error", null);
            var result = _context.Associateclients.FromSqlInterpolated($"select * from associateclient({_Client.client_email},{_Client.nutritionist_id})");
            var db_result = result.ToList();
            //Retorno de una tabla se valida de esta forma
            if (db_result[0].associateclient == 1)
            {
                json.status = "ok";
                return Ok(json);
            }
            else
            {
                return BadRequest(json);
            }
        }
        [HttpPost("add_product")]
        public async Task<ActionResult<JSON_Object>> Add_Product(AddProduct _Product)
        {
            JSON_Object json = new JSON_Object("error", null);
            var result = _context.Create_products.FromSqlInterpolated($"select * from createproduct({_Product.name},{_Product.size},{_Product.calcium},{_Product.sodium},{_Product.carbs},{_Product.fat},{_Product.calories},{_Product.iron},{_Product.protein})");
            var db_result = result.ToList();
            //Retorno de una tabla se valida de esta forma
            if (db_result[0].createproduct == 1)
            {
                Add_Vitamins(_Product.vitamins, _Product.name);
                json.status = "ok";
                return Ok(json);
            }
            else
            {
                return BadRequest(json);
            }
        }
        [HttpPost("add_vitamin")]
        private async void Add_Vitamins(List<string> vitaminlist, string p_name)
        {
            JSON_Object json = new JSON_Object("error", null);
            foreach (var item in vitaminlist)
            {
                Console.WriteLine(item);
                var vitaminsresult = _context.Add_Vitamin_Products.FromSqlInterpolated($"select * from add_vitamin_product({p_name},{item})");
                var db_result = vitaminsresult.ToList();
                Console.WriteLine(db_result[0].ToString());
            }
        }

        [HttpPost("get_daily_consumption")]
        public async Task<ActionResult<JSON_Object>> GetConsumption(Consumption_entry _Entry)
        {

            DateTime dateTime = Convert.ToDateTime(_Entry.date);
            DateOnly dateOnly = DateOnly.FromDateTime(dateTime);
            string dbDate = dateOnly.ToString("yyyy-MM-dd");
            Console.WriteLine("1) " + dbDate);
            DateOnly dateOnly1 = DateOnly.ParseExact(dbDate, "yyyy-MM-dd");
            Console.WriteLine("2) " + dateOnly1);

            JSON_Object json = new JSON_Object("error", null);
            var result = _context.Get_daily_consumptions.FromSqlInterpolated($"select * from get_daily_consumption({_Entry.client_email},{dateOnly1})");
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
    }
               
  
}
