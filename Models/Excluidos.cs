using System.ComponentModel.DataAnnotations;

namespace ProyectoADESS.Models
{
	public class Excluidos
	{
		public int IdExcluidos { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]

        public string Cedula { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]

        public string Motivo { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]

        public string Fecha { get; set; }


	}
}

