using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using REST_API_NutriTEC.Models;
using REST_API_NutriTEC.Resources;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using System.Reflection.PortableExecutable;

namespace REST_API_NutriTEC.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ClientController : ControllerBase
    {
        /// <summary>
        /// Context attribute to call the class that manages the database
        /// </summary>
        private readonly Proyecto2Context _context;

        public ClientController(Proyecto2Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to authenticate the client 
        /// </summary>
        /// <param name="client_credentials"> JSON with email and password to authenticate the client </param>
        /// <returns> JSON with the information for the client </returns>
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
        /// <summary>
        /// Method to search patient 
        /// </summary>
        /// <param name="_Name"> name of the person that someone wants to find </param>
        /// <returns></returns>
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
        /// <summary>
        /// Method to associate client to a nutritionist 
        /// </summary>
        /// <param name="_Client"> client to associate with a nutritionist </param>
        /// <returns> returns a json confirming if the query was succesful </returns>
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

        /// <summary>
        /// Method to add a product to the database
        /// </summary>
        /// <param name="_Product"> model that refers to the product that has to be inserted into the database</param>
        /// <returns> JSON confirming if the query was succesful </returns>
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
        /// <summary>
        /// Method to add vitamins to a certain product
        /// </summary>
        /// <param name="vitaminlist"> vitamins list of a product </param>
        /// <param name="p_name"> name of the product </param>
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

        /// <summary>
        /// Method to get the daily consumption of a client
        /// </summary>
        /// <param name="_Entry"> model with the specific attributes to get the consumption of a client </param>
        /// <returns> returns a JSON with the information requested</returns>

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

        /// <summary>
        /// Method to add a client to the database 
        /// </summary>
        /// <param name="new_client"> model of the attributes of a new client that has to be inserted into the database </param>
        /// <returns></returns>
        [HttpPost("add_client")]
        public async Task<ActionResult<JSON_Object>> AddClient(NewClient new_client)
        {
            DateTime dateTime = Convert.ToDateTime(new_client.birth_date);
            DateOnly dateOnly = DateOnly.FromDateTime(dateTime);
            string dbDate = dateOnly.ToString("yyyy-MM-dd");
            Console.WriteLine("1) " + dbDate);
            DateOnly dateOnly1 = DateOnly.ParseExact(dbDate, "yyyy-MM-dd");

            JSON_Object json = new JSON_Object("error", null);
            var result = _context.AddNewClients.FromSqlInterpolated($"select * from addclient({new_client.name},{new_client.lastname1},{new_client.lastname2},{dateOnly1},{new_client.weight},{new_client.height},{new_client.email},{Encryption.encrypt_password(new_client.password)},{new_client.country},{new_client.calorie_goal})");
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
        /// method to add measurements for a client 
        /// </summary>
        /// <param name="newMeasurement"> model with the attributes of measurements for a certain client </param>
        /// <returns> returns a json indicating if the query was succesdful </returns>
        [HttpPost("add_measurement")]
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

        /// <summary>
        /// Method to add the daily intake of a client 
        /// </summary>
        /// <param name="newDailyIntake"> model with the attributes for a client to enter their daily intake of food</param>
        /// <returns> returns a JSON indicating if the query was succesful </returns>
        [HttpPost("add_daily_intake")]
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

        /// <summary>
        /// Method to generate a report that will be displayed in the web app 
        /// </summary>
        /// <param name="report_getter"> model with the attributes to get the report for a certain client </param>
        /// <returns> returns a JSON with the information requested </returns>
        [HttpPost("generate_report")]
        public async Task<ActionResult<JSON_Object>> GenerateReport(ReportGetter report_getter)
        {
            DateTime dateTime = Convert.ToDateTime(report_getter.start_date);
            DateOnly dateOnly = DateOnly.FromDateTime(dateTime);
            string dbDate = dateOnly.ToString("yyyy-MM-dd");
            Console.WriteLine("1) " + dbDate);
            DateOnly dateOnly1 = DateOnly.ParseExact(dbDate, "yyyy-MM-dd");

            DateTime dateTime2 = Convert.ToDateTime(report_getter.end_date);
            DateOnly dateOnly2 = DateOnly.FromDateTime(dateTime2);
            string dbDate2 = dateOnly2.ToString("yyyy-MM-dd");
            Console.WriteLine("1) " + dbDate2);
            DateOnly dateOnly3 = DateOnly.ParseExact(dbDate2, "yyyy-MM-dd");

            JSON_Object json = new JSON_Object("error", null);

            var result = _context.GenerateReports.FromSqlInterpolated($"select * from generate_report({report_getter.client_id},{dateOnly1},{dateOnly3})");
            var db_result = result.ToList();

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
        ///  Method to get the plan that a client is associated to 
        /// </summary>
        /// <param name="client_id"> model to refer to a client </param>
        /// <returns> returns a JSON with the requested information </returns>
        [HttpPost("get_client_plan")]
        public async Task<ActionResult<JSON_Object>> GetClientPlan(ClientIdentifier client_id)
        {
            JSON_Object json = new JSON_Object("error", null);
            var result = _context.ClientPlans.FromSqlInterpolated($"select * from load_plan({client_id.client_id})");
            var db_result = result.ToList();
           

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
        /// Method to get a PDF file of a report of the client's measurements 
        /// </summary>
        /// <param name="email"> refers to the email of client </param>
        /// <param name="start_date"> refers to a the starting date of the set of measurements provided by the client</param>
        /// <param name="end_date"> refers to the end_date of the set of measurements provided by the client </param>
        /// <returns> returns a PDF file if succesful, if not it returns a JSON indicating error </returns>
        [HttpGet("create_pdf_report/{email}/{start_date}/{end_date}")]
        public async Task<ActionResult<JSON_Object>> CreatePDFReport(string email, string start_date, string end_date)
        {
            JSON_Object json = new JSON_Object("error", null);
            try
            {
                DateTime dateTime = Convert.ToDateTime(start_date);
                DateOnly dateOnly = DateOnly.FromDateTime(dateTime);
                string dbDate = dateOnly.ToString("yyyy-MM-dd");
                Console.WriteLine("1) " + dbDate);
                DateOnly dateOnly1 = DateOnly.ParseExact(dbDate, "yyyy-MM-dd");

                DateTime dateTime2 = Convert.ToDateTime(end_date);
                DateOnly dateOnly2 = DateOnly.FromDateTime(dateTime2);
                string dbDate2 = dateOnly2.ToString("yyyy-MM-dd");
                Console.WriteLine("1) " + dbDate2);
                DateOnly dateOnly3 = DateOnly.ParseExact(dbDate2, "yyyy-MM-dd");

                

                var result = _context.GenerateReports.FromSqlInterpolated($"select * from generate_report({email},{dateOnly1},{dateOnly3})");
                var db_result = result.ToList();

                var document = new PdfDocument();


                string HTMLContent = "<div style='width:100%; text-align:center'>";
                HTMLContent += "<h1> Your Report From NutriTec </h1>";



                HTMLContent += $"<h3> Client email : {email} </h3>";
                HTMLContent += "<div>";

                HTMLContent += "<table style ='width:100%; border: 1px solid #000'>";
                HTMLContent += "<thead style='font-weight:bold'>";
                HTMLContent += "<tr>";
                HTMLContent += "<td style='border:1px solid #000'> Date </td>";
                HTMLContent += "<td style='border:1px solid #000'> Weight (kg) </td>";
                HTMLContent += "<td style='border:1px solid #000'> Waist (cm) </td>";
                HTMLContent += "<td style='border:1px solid #000'> Neck (cm) </td >";
                HTMLContent += "<td style='border:1px solid #000'> Hip (cm) </td>";
                HTMLContent += "<td style='border:1px solid #000'> Muscle Percentage </td>";
                HTMLContent += "<td style='border:1px solid #000'> Fat Percentage </td>";
                HTMLContent += "</tr>";
                HTMLContent += "</thead >";

                HTMLContent += "<tbody>";

                foreach (var item in db_result)
                {
                    HTMLContent += "<tr>";
                    HTMLContent += "<td>" + item.date + "</td>";
                    HTMLContent += "<td>" + item.weight + "</td>";
                    HTMLContent += "<td>" + item.waist + "</td >";
                    HTMLContent += "<td>" + item.neck + "</td >";
                    HTMLContent += "<td>" + item.hip + "</td>";
                    HTMLContent += "<td> " + item.muscle_percentage + "</td >";
                    HTMLContent += "<td> " + item.fat_percentage + "</td >";
                    HTMLContent += "</tr>";
                }
                HTMLContent += "</tbody>";

                PdfGenerator.AddPdfPages(document, HTMLContent, PageSize.A3);
                byte[]? response = null;
                using (MemoryStream stream = new MemoryStream())
                {
                    document.Save(stream);
                    response = stream.ToArray();
                }
                string filename = $"report {email} - {dateOnly1} - {dateOnly3}.pdf";
                return File(response, "application/pdf", filename);
            } catch (Exception ex)
            {
                return BadRequest(json);
            }

            




        }

       

    }


}
