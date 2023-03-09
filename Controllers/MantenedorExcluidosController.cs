using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ProyectoADESS.Models;
using ProyectoADESS.SQL;
using System.Data;

namespace ProyectoADESS.Controllers
{
    public class MantenedorExcluidosController : Controller
    {
        ContactoExcluidos _contactoExcluidos = new ContactoExcluidos();
        private readonly IWebHostEnvironment _webHostEnvironment;

        public IActionResult Buscar(string cedula, string motivo, string fecha)
        {
            var usuarios = from Contacto in _contactoExcluidos.Listar() select Contacto;

            if (!String.IsNullOrEmpty(cedula))
            {
                usuarios = usuarios.Where(s => s.Cedula!.ToLower().Contains(cedula.ToLower()));
            }
            if (!String.IsNullOrEmpty(motivo))
            {
                usuarios = usuarios.Where(s => s.Motivo!.ToLower().Contains(motivo.ToLower()));
            }
            if (!String.IsNullOrEmpty(fecha))
            {
                usuarios = usuarios.Where(s => s.Fecha!.ToLower().Contains(fecha.ToLower()));
            }

            return View(usuarios.ToList());

        }

        public IActionResult Listar()
        {
            //Listado de los Usuarios
            var oListaExcluidos = _contactoExcluidos.Listar();
            return View(oListaExcluidos);
        }

        public JsonResult Paginar()
        {
            List<Excluidos> lista = new List<Excluidos>();
            var cn = new Conexion();

            using (var conexionExcluidos = new SqlConnection(cn.getCadenaSQL()))
            {
                conexionExcluidos.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarExcluidos", conexionExcluidos);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Excluidos()
                        {
                            
                            Cedula = dr["Cedula"].ToString(),
                            Motivo = dr["Motivo"].ToString(),
                            Fecha = dr["Fecha"].ToString(),

                        });
                    }
                }
            }
            return Json(new { data = lista });
        }

        [HttpGet]
        public IActionResult Guardar()
        {
            //Para devolver metodo en la vista
            return View();
        }
        [HttpPost]


        public IActionResult Guardar(Excluidos oContacto)
        {
            //Para guardar datos en la Base de Datos

            var respuesta = _contactoExcluidos.Guardar(oContacto);

            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
            {
                return View();
            }

        }

        public MantenedorExcluidosController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult GenerarArchivo()
        {
            string archivoRuta = $"{_webHostEnvironment.WebRootPath}/{Guid.NewGuid()}.txt";
            // Obtiene los productos desde la base de datos
            var usuarios = from ContactoExcluidos in _contactoExcluidos.Listar() select ContactoExcluidos;
            var writer = new StreamWriter(archivoRuta);
            //Creamos el contenido del archivo 
            using (writer)
            {
                foreach (var items in usuarios)
                {
                    writer.WriteLine("{0}                               {1}                         {2}", items.Cedula, items.Motivo, items.Fecha.ToUpper());
                }
            }
            // Nombre del archivo
            string nombreArchivo = "archivo.txt";

            // Tipo MIME
            string tipoMime = "text/plain";

            // Genera el archivo y lo devuelve como un FileResult
            return File(new byte[0], tipoMime, nombreArchivo);
        }

        public IActionResult Editar(int IdExcluidos)
        {
            //Para devolver metodo a la vista
            var ocontacto = _contactoExcluidos.Obtener(IdExcluidos);
            return View(ocontacto);


        }
        [HttpPost]
        public IActionResult Editar(Excluidos oid_ad)
        {
            //Para editar los datos tanto como en la BD como en la vista
            var respuesta = _contactoExcluidos.Editar(oid_ad);

            if (respuesta)
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
            var ocontacto = _contactoExcluidos.Obtener(Id_add);
            return View(ocontacto);


        }
        [HttpPost]
        public IActionResult Eliminar(Excluidos oid_add)
        {
            //Para eliminar los datos tanto como en la BD como en la vista
            var respuesta = _contactoExcluidos.Eliminar(oid_add.IdExcluidos);

            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
            {
                return View();
            }
        }
    }
}

