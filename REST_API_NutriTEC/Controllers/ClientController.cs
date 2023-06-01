using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            if(db_result.Count == 0 )
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

       


    }
}
