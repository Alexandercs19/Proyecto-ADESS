using Microsoft.AspNetCore.Mvc;
using ProyectoADESS.Models;
using System.Diagnostics;
using System.Reflection;
using System.IO;

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
                        Registros.Add(GetRegistros(line));
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
                        Registros.Add(GetRegistros(line));

                    }
                }
            }

            return Ok(Registros);
        }
        [HttpPost]
        public IActionResult GuardarRegistr([FromBody] List<ClassAdd> registros)
        {
            return Ok(registros);
        }
        public IActionResult privacy()
        {
            return View();
        }

        private ClassAdd GetRegistros(string line)
        {
            var lineaList = line.Split(" ").ToList();
            List<string> list = new List<string>();
            var registro = new ClassAdd();

            foreach (var linea in lineaList)
            {
                if (linea != "")
                {
                    if (list.Count() == 0)
                    {
                        string cedula = linea.Substring(0, 11);
                        string nombre = linea.Substring(11);

                        int currentNombre = 0;
                        string PrimerNombre = "";
                        string SegundoNombre = "";


                        for (int i = 0; i < nombre.Length; i++)
                        {
                            if (nombre[i].ToString() == nombre[i].ToString().ToUpper() && i == 0)
                            {
                                currentNombre = 1;
                            }
                            if (nombre[i].ToString() == nombre[i].ToString().ToUpper() && i > 1)
                            {
                                currentNombre = 2;
                            }
                            if(currentNombre == 1)
                            {
                                PrimerNombre += nombre[i];
                            }
                            else
                            {
                                SegundoNombre+= nombre[i];
                            }
                        }

                        list.Add(cedula);
                        list.Add(PrimerNombre + " " + SegundoNombre);
                    }
                    else
                    {
                        list.Add(linea);
                    }
                }
            };

            string monto, fecha, sub;
            sub = list[3].Substring(0, 4); 
            monto = list[3].Substring(5, 8);
            fecha = list[3].Substring(9);

            registro.Cedula_add = list[0];
            registro.Apellido = list[1];
            registro.Nombre = list[2];
            registro.Sub= sub;
            registro.Monto = monto.Substring(3);
            registro.Fecha_add = fecha.Substring(3);

            return registro;
        }
    }
}

