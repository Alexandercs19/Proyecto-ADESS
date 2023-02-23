using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using ProyectoADESS.Models;
using System.Net.Mail;
using Microsoft.AspNetCore.SignalR;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ProyectoADESS.SQL;

namespace ProyectoADESS.Controllers
{
    public class SubirArchivoController : Controller
    {
        static string cadena = "Data Source=(Local); Initial Catalog=ADESS;Integrated Security=true";

        public ActionResult Index()
        {
            return View();
        }
        
        public IActionResult SubirArchivo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubirArchivo(ClassAdd classAdd)
        {
           
            string Extension = Path.GetExtension(classAdd.Archivo.FileName);

            MemoryStream ms = new MemoryStream();
            classAdd.Archivo.CopyTo(ms);
            byte[] data = ms.ToArray();
            //Attachment.CreateAttachmentFromString
            using (SqlConnection oconexion = new SqlConnection(cadena))

            { 
                SqlCommand cmd = new SqlCommand("SP_guardarsubidos", oconexion);
                cmd.Parameters.AddWithValue("@cedula_add", classAdd.Cedula_add);
                cmd.Parameters.AddWithValue("@apellido", classAdd.Apellido);
                cmd.Parameters.AddWithValue("@nombre", classAdd.Nombre);
                cmd.Parameters.AddWithValue("@sub", classAdd.Sub);
                cmd.Parameters.AddWithValue("@monto", classAdd.Monto);
                cmd.Parameters.AddWithValue("@fecha_add", classAdd.Fecha_add);
                cmd.Parameters.AddWithValue("@archivo", data);
                cmd.Parameters.AddWithValue("@extension", Extension);
                cmd.CommandType = CommandType.Text;
                oconexion.Open();
                cmd.ExecuteNonQuery();

            }
            return View();
        }

    }


}
