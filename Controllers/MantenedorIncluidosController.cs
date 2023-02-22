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
            unicoArchivo model = new unicoArchivo();
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
        public IActionResult Editar(int Id_add)
        {
            //Para devolver metodo a la vista
            var ocontacto = _Contacto.Obtener(Id_add);
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

        

    }
}
