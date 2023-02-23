using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.IO;
using System.Data.SqlClient;
using System.Data;
namespace ProyectoADESS.Controllers
{
    public class SubirArchivoController : Controller
    {
        static string cadena = "Data Source=(Local);Initial Catalog=ADESS;Integrated Security=true";

        public IActionResult Subir()
        {
            return View();
        }

        public ActionResult index() 
        {
            return View();
        }
        
        
    }
}
