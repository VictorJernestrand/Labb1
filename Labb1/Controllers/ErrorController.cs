using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Labb1.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult CustomErrorCode(int statusCode)
        {
            // Interface IStatusCodeReExecuteFeature gives path and querystring the user tried to access
            // var errorInfo = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            // Handle error codes
            if (statusCode == 404)
            {
                ViewBag.ErrorMessage = @$"Skitsidan du försöker komma åt finns inte.";
            }
            else
            {
                ViewBag.ErrorMessage = "Oops, något gick åt helvete. Kontakta support om felet kvarstår.";
            }

            ViewBag.StatusCode = statusCode;
            return View("PageNotFound");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}