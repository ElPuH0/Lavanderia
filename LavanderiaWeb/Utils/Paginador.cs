using System.Collections.Generic;

namespace LavanderiaWeb.Utils
{
    public class Paginador<T> where T:class
    {
        public int PaginaActual { get; set; }
        public string BusquedaActual { get; set; }
        public int RegistrosPorPagina { get; set; }
        public int TotalRegistros { get; set; }
        public int TotalPaginas { get; set; }
        public IEnumerable<T> Resultado { get; set; }
    }
}