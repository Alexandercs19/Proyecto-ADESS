using Microsoft.AspNetCore.Mvc;
using ProyectoADESS.Models;
using ProyectoADESS.SQL;

namespace ProyectoADESS.Controllers
{
    public class MantenedorExcluidosController : Controller
    {
        ContactoExcluidos _contactoExcluidos = new ContactoExcluidos();

        //static string cadena_bien = "data source(laptop-9qit7t5h); initial catalog =excluidosdb; integrated security=true";
        public IActionResult Listar()
        {
            //Listado de los Usuarios
            var oListaExcluidos = _contactoExcluidos.Listar();
            return View(oListaExcluidos);
        }



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

        [HttpPost]

        public IActionResult Editar(int IdExcluidos)
        {
            var ocontacto = _contactoExcluidos.Obtener(IdExcluidos);
            return View(ocontacto);


        }
        public IActionResult Editar(Excluidos oid_add)
        {
            var respuesta = _contactoExcluidos.Editar(oid_add);

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

