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
        [HttpPost("auth_admin")]
        public async Task<ActionResult<JSON_Object>> AuthAdmin(Credentials admin_credentials)
        {
            
                return Ok();
            

        }
    }
}
