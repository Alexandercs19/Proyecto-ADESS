using Microsoft.AspNetCore.Mvc;

using ProyectoADESS.SQL;
using ProyectoADESS.Models;


namespace ProyectoADESS.Controllers
{
    public class MantenedorIncluidosController : Controller
    {

        Contacto _Contacto = new Contacto();

        public IActionResult Listar()
        {
            //Listado de los Usuarios
            var oLista = _Contacto.Listar();
            return View(oLista);
        }
        public IActionResult Guardar()
        {
            //Para devolver metodo en la vista
            return View();
        }
        [HttpPost]
        public IActionResult Guardar(ClassAdd oContacto)
        {
            //Para guardar datos en la Base de Datos

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
        [HttpPost]

        public IActionResult Editar (int Id_add)
        {
            var ocontacto = _Contacto.Obtener(Id_add);
            return View(ocontacto);


        }
        public IActionResult Editar(ClassAdd oid_add)
        {
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
    }
}
