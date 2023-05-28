using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REST_API_NutriTEC.Models;

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
            var result = _context.LoginClients.FromSqlInterpolated($"select * from loginclient({client_credentials.email},{client_credentials.password})");
            var db_result = result.ToList();
            JSON_Object json = new JSON_Object("ok", db_result[0]);

            return Ok(json);

        }

        

     }
}
