using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grande.POJOS
{
    public class Producto
    {
        public string Clave { get; set; }

        public int Cantidad { get; set; }

        public int CantidadMinima { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public decimal Precio { get; set; }

        public string activo { get; set; }

    }
}
