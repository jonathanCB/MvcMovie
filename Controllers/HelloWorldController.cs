using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MvcMovie.Controllers
{
    public class HelloWorldController : Controller
    {
        // 
        // GET: /HelloWorld/

        public IActionResult Index()
            {
                return View();
            }

        // 
        // GET: /HelloWorld/Welcome/ 

        /*  --------------------- ViewData --------------------- 
            Os dados são extraídos da URL e passados para o HelloWorldController.
            O controlador empacota os dados em um dicionário ViewData e passa esse objeto para a View.
            Em seguida, a View renderiza os dados como HTML para o navegador.
        */
        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;

            return View();
        }
        //O ViewData Dictionary foi usado para passar dados do Controller para uma View. 
    }
}