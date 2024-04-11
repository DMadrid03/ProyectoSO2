using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
        public int procesosTerminados;

        public Gestor(DataTable tabProcesos,DataTable tabParticiones)
        {
            procesosTerminados = 0;
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
            if(tabProcesos.Rows.Count >0)tabProcesos.Rows.Clear();
            DataRow fila;
            foreach (Proceso p in ProcesosList)
            {
                fila = tabProcesos.NewRow();
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
            DataRow fila;
            foreach(Particion p in ParticionesList)
            {
                fila = tabParticiones.NewRow();
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
            int pt = 0;
            for(int i=0; i < ProcesosList.Count; i++)
            {
                proceso = ProcesosList[i];
                if(proceso.Estado ==1 && proceso.Duracion ==proceso.TiempoTranscurrido - proceso.TiempoInicio)
                {//proceso en ejecución, y ya paso su tiempo de ejecución
                    ParticionesList[proceso.ParticionID - 1].liberarParticion();
                    proceso.Estado = 3;
                    SalieronProcesos = true;
                    proceso.ParticionID = -1;//sacar proceso
                    ProcesosList[i] = proceso;                    
                }
                if(proceso.Estado != 3)
                {//el proceso está en memoria y se seguirá ejecutando
                    proceso.TiempoTranscurrido++;
                }
                if (proceso.ParticionID == -1)
                    pt++;//conteo de procesos terminados
            }
            procesosTerminados = pt;
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
                if(proc.Estado ==2){
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
                            proceso.Estado = 1;                            
                        }                       
                    }
                    if(particionIndex != -1)
                    {//ese proceso encontró una particion donde ubicarse
                        proc.ParticionID = proceso.ParticionID;//asignar el proceso a esa partición
                        proc.TiempoInicio = proceso.TiempoTranscurrido - 1;//guardar el segundo en que ese proceso entró a memoria
                        ParticionesList[particionIndex].ocupado = true; //marcar la partición como ocupada
                    }
                    
                }
            }
        }
        public void firstFit()
        {            
            foreach(Proceso pro in ProcesosList)
            {//asignar los procesos libres a los huecos libres de memoria usando primer ajuste
                if(pro.Estado == 2)
                {
                    foreach(Particion par in ParticionesList)
                    {
                        if(par.ocupado == false && pro.MemoriaRequerida <= par.size)
                        {
                            pro.ParticionID = par.id;//asignar directamente el proceso a la partición
                            pro.TiempoInicio = pro.TiempoTranscurrido;
                            pro.Estado = 1;//proceso en estado (ejecutando)
                            par.ocupado = true;
                            break;//dejar de buscar particiones para ese proceso
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
