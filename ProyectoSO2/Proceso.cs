using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace entornoPruebaClasesProyecto
{
    internal class Proceso
    {
        public int id { get; set; }
        public String Nombre { get; set; }
        public int Duracion { get; set; }
        public int MemoriaRequerida { get; set; }
        public int Estado { get; set; }
        public int TiempoTranscurrido { get; set; }
        public int TiempoInicio { get; set; }
        public int ParticionID { get; set; }

        public Proceso(DataRow fila)
        {
            id = int.Parse(fila["id"].ToString());
            Nombre = fila["Nombre"].ToString();
            Duracion = int.Parse(fila["Duración"].ToString());
            MemoriaRequerida = int.Parse(fila["memoria Requerida"].ToString());
            Estado = 1;
            TiempoTranscurrido = 0;
            TiempoInicio = -1;
            ParticionID = 0;
        }

    }
}
