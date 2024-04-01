using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entornoPruebaClasesProyecto
{
    internal class Particion
    {
        public int id { get; set; }
        public int size { get; set; }
        public bool ocupado { get; set; }

        public Particion(DataRow fila)
        {
            id = int.Parse(fila["Numero"].ToString());
            size = int.Parse(fila["tamaño"].ToString());
            ocupado = false;
        }
        public void liberarParticion()
        {
            this.ocupado = false;
        }


    }
}
