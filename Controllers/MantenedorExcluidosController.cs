using Microsoft.AspNetCore.Mvc;
using ProyectoADESS.Models;
using ProyectoADESS.SQL;

namespace ProyectoADESS.Controllers
{
    public class MantenedorExcluidosController : Controller
    {
        ContactoExcluidos _contactoExcluidos = new ContactoExcluidos();

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

