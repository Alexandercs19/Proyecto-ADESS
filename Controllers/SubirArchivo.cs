using Microsoft.AspNetCore.Mvc;
using ProyectoADESS.Models;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using Microsoft.Win32;
using ProyectoADESS.SQL;

namespace ProyectoADESS.Controllers
{
    public class SubirArchivo : Controller
    {

        [HttpGet]
        public IActionResult listar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult listar(IFormFile Archivo)
        {
            List<ClassAdd> Registros = new List<ClassAdd>();
            if (Archivo != null)
            {
                using (StreamReader reader = new StreamReader(Archivo.OpenReadStream()))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Registros.Add(GetRegistro(line));
                    }
                }
            }

            return View(Registros);
        }
        [HttpPost]
        public IActionResult GetPersonasList(IFormFile ArchivoTxT)
        {
            List<ClassAdd> Registros = new();
            if (ArchivoTxT != null)
            {
                using (StreamReader reader = new StreamReader(ArchivoTxT.OpenReadStream()))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Registros.Add(GetRegistro(line));

                    }
                }
            }

            return Ok(Registros);
        }
        [HttpPost]
        public IActionResult GuardarRegistros([FromBody] List<ClassAdd> registros)
        {
            Contacto contacto = new Contacto();
            if (registros != null && registros.Count > 0)
            {
                registros.ForEach(x => contacto.Guardar(x));
                return Ok(registros);
            }
            else
            {
                return BadRequest("Error ");
            }


        }


        public IActionResult privacy()
        {
            return View();
        }

        private ClassAdd GetRegistro(string line)
        {
            var registro = new ClassAdd
            {
                Cedula_add = line.Substring(0, 11),
                Apellido = line.Substring(11, 30),
                Nombre = line.Substring(41, 30),
                Sub = line.Substring(71, 4),
                Monto = line.Substring(75, 9),
                Fecha_add = line.Substring(84)
            };

            return registro;
        }
    }
}
