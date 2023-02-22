using System.ComponentModel.DataAnnotations;

namespace ProyectoADESS.Models
{
    public class ClassAdd
    {

        [Required(ErrorMessage =" El campo Cedula es requerido.")]
        public string Cedula_add { get; set; }

        [Required(ErrorMessage = " El campo Apellido es requerido.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = " El campo Nombre es requerido.")]

        public string Nombre { get; set; }

        [Required(ErrorMessage = " El campo Sub es requerido.")]

        public string Sub { get; set; }

        [Required(ErrorMessage = " El campo Monto es requerido.")]

        public string Monto { get; set;}

        [Required(ErrorMessage = " El campo Fecha es requerido.")]

        public string Fecha_add { get; set; }
        public int Id_add { get; set; }

        public string Message { get; set; }
        public bool Subido { get; set; }
        public bool IsResponse { get; set; }

        public string Nombrearchivo { get; set; }
        public IFormFile archivo { get; set; }
    }
}

