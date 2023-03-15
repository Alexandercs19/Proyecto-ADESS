using Microsoft.AspNetCore.Mvc;
using ProyectoADESS.SQL;
using ProyectoADESS.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ProyectoADESS.Controllers
{
    public class MantenedorIncluidosController : Controller
    {

        Contacto _Contacto = new Contacto();
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MantenedorIncluidosController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult GenerarArchivo()
        {
            string nombre = $"Reporte-De-{DateTime.Today.Date.ToString().Replace("/", "-").Replace(" ","-").Replace(":", "-")}";
    
            string archivoRuta = $"{_webHostEnvironment.WebRootPath}/{nombre}.txt";
            // Obtiene los productos desde la base de datos
            var usuarios = from Contacto in _Contacto.Listar() select Contacto;
            
            //Creamos el contenido del archivo 
            using (var writer = new StreamWriter(archivoRuta))
            {
                foreach (var items in usuarios)
                {
                    writer.WriteLine("{0}{1}                              {2}                              {3}{4}{5}", items.Cedula_add, items.Apellido, items.Nombre, items.Sub, items.Monto, items.Fecha_add.ToUpper());
                }
            }
            // Nombre del archivo

            if (System.IO.File.Exists(archivoRuta))
            {
                FileStream fs = new FileStream(archivoRuta, FileMode.Open);
                    return File(fs,"application/octet-stream", $"{nombre}.txt");
            }
            else
            {
                ViewBag.ErrorArchivo = "Ocurrio un error al generar el archivo";
                return View("Listar", "MantenedorIncluidos");
            }

        }
        public IActionResult Buscar(string cedula, string nombre, string apellido)
        {
            var usuarios = from Contacto in _Contacto.Listar() select Contacto;

            if (!String.IsNullOrEmpty(cedula))
            {
                usuarios = usuarios.Where(s => s.Cedula_add!.Contains(cedula.ToLower()));
            }
            if (!String.IsNullOrEmpty(nombre))
            {
                usuarios = usuarios.Where(s => s.Nombre!.ToLower().Contains(nombre.ToLower()));
            }
            if (!String.IsNullOrEmpty(apellido))
            {
                usuarios = usuarios.Where(s => s.Apellido!.ToLower().Contains(apellido.ToLower()));
            }

            return View(usuarios.ToList());

        }
        public IActionResult Listar(PaginacionRespuesta paginacionViewModel)
        {
            var oLista = _Contacto.Listar();
            //var respuestaVM = new PaginacionRespuesta<ClassAdd>
            //{
            //    Elementos = oLista,
            //    Pagina = paginacionViewModel.Pagina,
            //    RecordsPorPagina = paginacionViewModel.RecordsPorPagina,
            //    //CantidadTotalRecords = cmd;
            //    BaseUrl = Url.Action()
            //};
            return View(oLista);
        }
        public JsonResult Paginar()
        {
            var lista = new List<ClassAdd>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_PaginarIncluidos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new ClassAdd()
                        {
                            Cedula_add = dr["cedula_add"].ToString(),
                            Apellido = dr["apellido"].ToString(),
                            Nombre = dr["nombre"].ToString(),
                            Sub = dr["sub"].ToString(),
                            Monto = dr["monto"].ToString(),
                            Fecha_add = dr["fecha_add"].ToString(),
                            Id_add = Convert.ToInt32(dr["id_add"])
                        });
                    }
                }
            }
            return Json(lista);
        }
        [HttpGet]
        public IActionResult Guardar()
        {
            //Para devolver metodo en la vista
            return View();
        }

        [HttpPost]
        public IActionResult Guardar(ClassAdd oContacto)
        {
            //Para guardar datos en la Base de Datos
            if (!ModelState.IsValid)
                return View();

            var respuesta = _Contacto.Guardar(oContacto);

            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
            {
                return View();
            }

        }
        [HttpGet]
        public IActionResult Editar(int id_add)
        {
            //Para devolver metodo a la vista

            var ocontacto = _Contacto.Obtener(id_add);
            return View(ocontacto);


        }
        [HttpPost]
        public IActionResult Editar(ClassAdd oid_add)
        {
            //Para editar los datos tanto como en la BD como en la vista
            var respuesta = _Contacto.Editar(oid_add);

            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
            {
                return View();
            }
        }

        public IActionResult EditarFecha(ClassAdd afecha_add)
        {
            var fechaeditada = _Contacto.EditarFecha(afecha_add);

                if (fechaeditada)
            {
                return RedirectToAction("Listar");
            }
            else
            {
                return View();
            }
        }
        public IActionResult Eliminar(int Id_add)
        {
            //Para devolver el metodo a la vista
            var ocontacto = _Contacto.Obtener(Id_add);
            return View(ocontacto);

        }
        [HttpPost]
        public IActionResult Eliminar(ClassAdd oid_add)
        {
            //Para eliminar los datos tanto como en la BD como en la vista
            var respuesta = _Contacto.Eliminar(oid_add.Id_add);

            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Limpiar()
        {
            _Contacto.Limpiar();
            return RedirectToAction("Listar");
        }

        [HttpGet]
        public IActionResult VerficarCedula(string cedula)
        {
            var ced_exist = _Contacto.ObtenerCedula(cedula);
            if (ced_exist != null)
            {
                return Json("Esa cédula ya existe");
            }
            return Json(true);
        }
    }
}
