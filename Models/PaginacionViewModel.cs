namespace ProyectoADESS.Models
{
    public class PaginacionViewModel
    {
        public int Pagina { get; set; } = 1;
        private int recordsPorPagina { get; set; } = 3;
        private readonly int cantidadMaximaRecordsPorPagina = 2000;

        public int RecordsPorPagina
        {
            get
            {
                return recordsPorPagina;
            }
            set
            {
                recordsPorPagina = (value > cantidadMaximaRecordsPorPagina) ?
                                cantidadMaximaRecordsPorPagina : value;

            }
        }

        public int RecordsASaltar => recordsPorPagina * (Pagina - 1);

    }
}
