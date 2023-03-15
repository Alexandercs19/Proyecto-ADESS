using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ProyectoADESS.Models
{
    public class ClassAdd
    {

        [Required(ErrorMessage ="Este campo es obligatorio")]
        public string Cedula_add { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Sub { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Monto { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Fecha_add { get; set; }

        public int Id_add { get; set; }

        public IFormFile Archivo { get; set; }
        public string Extension { get; set; }


    }
}

