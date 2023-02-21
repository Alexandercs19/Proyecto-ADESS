using System.ComponentModel.DataAnnotations;
namespace ProyectoADESS.Models
{
    public class ClassAdd
    {

        [Required(ErrorMessage ="El campo Cedula es requerida")]
        public string Cedula_add { get; set; }
        [Required(ErrorMessage ="El campo Apellido es requerido")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "El campo Nombre es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo SUB requerido")]
        public string Sub { get; set; }
        [Required(ErrorMessage = "El campo Apellido es requerido")]
        public string Monto { get; set;}
        [Required(ErrorMessage = "El campo Apellido es requerido")]
        public string Fecha_add { get; set; }

        public int Id_add { get; set; }
    }
}

