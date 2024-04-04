using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entornoPruebaClasesProyecto
{
    internal class Gestor
    {
        public Particion particion { get; set; }
        public Proceso proceso { get; set; }
        public List<Proceso> ProcesosList;
        public List<Particion> ParticionesList;
        private DataTable tabProcesos, tabParticiones;

        public Gestor(DataTable tabProcesos,DataTable tabParticiones)
        {
           this.tabProcesos = tabProcesos;
           this.tabParticiones = tabParticiones;
            ProcesosList = new List<Proceso>();
            ParticionesList = new List<Particion>();

        }
        public List<Proceso> getProcesosList()
        {
            ProcesosList.Clear();
            for(int i =0; i<tabProcesos.Rows.Count;i++)
            {
                Proceso proceso = new Proceso(tabProcesos.Rows[i]);
                ProcesosList.Add(proceso);
            }
            return ProcesosList;
        }
        public List<Particion> getParticionesList()
        {
            ParticionesList.Clear();
            //crear lista de particiones            
            for (int i = 0; i < tabParticiones.Rows.Count; i++)
            {
                Particion particion = new Particion(tabParticiones.Rows[i]);
                ParticionesList.Add(particion);
            }            
            return ParticionesList;
        }
        public DataTable getTableProcesos()
        {
            tabProcesos.Rows.Clear();
            DataRow fila =  tabProcesos.NewRow();
            foreach (Proceso p in ProcesosList)
            {
                fila["ID"] = p.id;
                fila["nombre"] = p.Nombre;
                fila["Duración"] = p.Duracion;
                fila["Memoria Requerida"] = p.MemoriaRequerida;
                fila["Estado"] = p.Estado;
                fila["Tiempo Transcurrido"] = p.TiempoTranscurrido;
                fila["particionID"] = p.ParticionID;
                fila["Tiempo Inicio"] = p.TiempoInicio;
                tabProcesos.Rows.Add(fila);
            }
            return tabProcesos;
        }
        public DataTable getTableParticiones()
        {
            tabParticiones.Rows.Clear();
            DataRow fila = tabParticiones.NewRow();
            foreach(Particion p in ParticionesList)
            {
                fila["Numero"] = p.id;
                fila["Tamaño"] = p.size;
                fila["Ocupado"] = p.ocupado;
                tabParticiones.Rows.Add(fila);
            }
            
            return tabParticiones;
        }
        public bool verificarProcesoSaliente()
        {
            bool SalieronProcesos = false;
            for(int i=1; i < ProcesosList.Count; i++)
            {
                proceso = ProcesosList[i];
                if(proceso.Estado ==2 && proceso.Duracion ==proceso.TiempoTranscurrido - proceso.TiempoInicio)
                {
                    proceso.Estado = 3;
                    SalieronProcesos = true;
                    proceso.ParticionID = -1;
                    ProcesosList[i] = proceso;
                }
            }
            return SalieronProcesos;
        }
        public int particionesLibres()
        {
            int pl = 0;
            foreach(Particion p in ParticionesList)
            {
                if (p.ocupado == false)
                    pl++;
            }
            return pl;
        }
        public void BestFit()
        {//va a asignar el/los procesos libres en las particiones de memoria que estén libres usando mejor ajuste            
            foreach(Proceso proc in ProcesosList)
            {
                if(proc.Estado ==1){
                    proceso = proc;
                    int diferencia = 100000000;
                    int particionIndex = -1;
                    for (int i=0; i<ParticionesList.Count;i++)
                    {
                        particion = ParticionesList[i];
                        if ((!particion.ocupado) && particion.size >= proceso.MemoriaRequerida)
                        {
                            if (particion.size - proceso.MemoriaRequerida < diferencia)
                            {//definir la partición mas óptima para ese proceso
                                diferencia = particion.size - proceso.MemoriaRequerida;
                                proceso.ParticionID = particion.id;
                                particionIndex = i;
                            }
                            proceso.Estado = 2;
                        }                       
                    }
                    proc.ParticionID = proceso.ParticionID;//asignar el proceso a esa partición
                    ParticionesList[particionIndex].ocupado = true; //marcar la partición como ocupada
                    
                }
            }
        }
        public void firstFit()
        {            
            foreach(Proceso pro in ProcesosList)
            {//asignar los procesos libres a los huecos libres de memoria usando primer ajuste
                if(pro.Estado == 1)
                {
                    foreach(Particion par in ParticionesList)
                    {
                        if(par.ocupado == false && pro.MemoriaRequerida <= par.size)
                        {
                            pro.ParticionID = par.id;
                            pro.Estado = 2;
                            par.ocupado = true;
                            break;
                        }
                    }
                }
            }
        }
        public Particion particionMasGrande()
        {
            
            int size=0,index=0;
            for (int i = 0; i < ParticionesList.Count ; i++)
            {
                if (ParticionesList[i].size > size)
                {
                    index = i;
                    size = ParticionesList[i].size;
                }
            }
            return ParticionesList[index];
        }
    }
}
