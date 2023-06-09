using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using REST_API_NutriTEC.Models;
using REST_API_NutriTEC.Resources;
using System;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace REST_API_NutriTEC.Controllers
{

    [ApiController]
    [Route("api/")]
    public class AdminController : ControllerBase

    {
        /// <summary>
        /// Context attribute to call the class that manages the database
        /// </summary>
        private readonly Proyecto2Context _context;

        public AdminController(Proyecto2Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to authenticate the admin
        /// </summary>
        /// <param name="admin_credentials"> this parameter refers to a json containing the email and password to authenticate the admin </param>
        /// <returns> returns a JSON with the information of the admin </returns>
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

        /// <summary>
        /// Method to generate the admin report and be displayed in the web app
        /// </summary>
        /// <returns> returns a JSON with the information of the report</returns>
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

        
        /// <summary>
        /// Method to get all the unnaproved products in the database
        /// </summary>
        /// <returns>returns a JSON with the corresponding information </returns>
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
        /// <summary>
        /// Method to approve a product 
        /// </summary>
        /// <param name="_Name"> name of the product that has to be approved in the database</param>
        /// <returns> returns a JSON indicating if the approval was succesful or not </returns>
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

        /// <summary>
        /// Method to delete a product 
        /// </summary>
        /// <param name="_Name"> returns a json indicating if the delete was succesful or not </param>
        /// <returns></returns>
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
        /// <summary>
        /// Method to delete a plan
        /// </summary>
        /// <param name="_Name"> Name of plan to delete </param>
        /// <returns> returns JSON indicating </returns>
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

        /// <summary>
        /// Creates a PDF File for the report of administrator
        /// </summary>
        /// <returns> PDF file with the report </returns>
        [HttpGet("generate_admin_report_pdf")]
        public async Task<ActionResult<JSON_Object>> GenerateAdminReportPDF()
        {

            JSON_Object json = new JSON_Object("error", null);
            try
            {
                var result = _context.Calculate_billings.FromSqlInterpolated($"select * from calculate_billing()");
                var db_result = result.ToList();

                var document = new PdfDocument();


                string HTMLContent = "<div style='width:100%; text-align:center'>";
                HTMLContent += "<h2> Your Report From NutriTec </h2>";


                HTMLContent += $"<h3> Administrator report </h3>";
                HTMLContent += "<div>";

                HTMLContent += "<table style ='width:100%; border: 1px solid #000'>";
                HTMLContent += "<thead style='font-weight:bold'>";
                HTMLContent += "<tr>";
                HTMLContent += "<td style='border:1px solid #000'> Billing Type </td>";
                HTMLContent += "<td style='border:1px solid #000'> Nutririonist Email </td>";
                HTMLContent += "<td style='border:1px solid #000'> Nutritiionist Name </td>";
                HTMLContent += "<td style='border:1px solid #000'> Credit Card </td >";
                HTMLContent += "<td style='border:1px solid #000'> Total </td>";
                HTMLContent += "<td style='border:1px solid #000'> Discount </td>";
                HTMLContent += "<td style='border:1px solid #000'> Payment </td>";
                HTMLContent += "</tr>";
                HTMLContent += "</thead >";

                HTMLContent += "<tbody>";

                foreach (var item in db_result)
                {
                    HTMLContent += "<tr>";
                    HTMLContent += "<td>" + item.billing_type + "</td>";
                    HTMLContent += "<td>" + item.nutri_email + "</td>";
                    HTMLContent += "<td>" + item.nutri_fullname + "</td >";
                    HTMLContent += "<td>" + item.credit_card + "</td >";
                    HTMLContent += "<td>" + item.total + "</td>";
                    HTMLContent += "<td> " + item.discount + "</td >";
                    HTMLContent += "<td> " + item.payment + "</td >";
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
                string filename = $"{"admin_report_"}.pdf";
                return File(response, "application/pdf", filename);
            }catch(Exception ex)
            {
                return BadRequest(json);
            }
            

        }


    }
    
}
