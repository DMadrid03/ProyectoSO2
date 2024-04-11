using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using entornoPruebaClasesProyecto;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata.Ecma335;
using Microsoft.VisualBasic;

namespace ProyectoSO2
{
    public partial class frmSimulacion : Form
    {
        DataRow filaInfo;
        DataTable tabParticiones;
        DataTable tabProcesos;
        int particiones;
        int min;
        int max;
        Random random;
        List<Particion> particionesList;
        List<Proceso> procesosList;
        Gestor gestor;
        bool simulacion;
        Thread hiloPrincipal;
        public frmSimulacion(DataRow fila, DataTable tbPart)
        {
            InitializeComponent();
            simulacion = false;
            tabParticiones = tbPart;            
            filaInfo = fila;
            tabProcesos = new DataTable();
            gestor = new Gestor(tabProcesos, tabParticiones);


            particiones = int.Parse(filaInfo["nParticiones"].ToString()) + 1;
            min = int.Parse(filaInfo["min"].ToString());
            max = int.Parse(filaInfo["max"].ToString());
            random = new Random();

            panMemoria.BackColor = Color.Gray;

        }

        private void frmSimulacion_Load(object sender, EventArgs e)
        {            
            tabProcesos.Columns.Add("ID");
            tabProcesos.Columns.Add("Nombre");
            tabProcesos.Columns.Add("Duración");
            tabProcesos.Columns.Add("Memoria Requerida");
            tabProcesos.Columns.Add("Estado");
            tabProcesos.Columns.Add("Tiempo Transcurrido");
            tabProcesos.Columns.Add("particionID");
            tabProcesos.Columns.Add("Tiempo Inicio");
            dgvProcesos.DataSource = tabProcesos;

            dgvProcesos.Columns["ID"].ReadOnly = true; dgvProcesos.Columns["ID"].Width = 35;
            dgvProcesos.Columns["Duración"].ReadOnly = true; dgvProcesos.Columns["Memoria Requerida"].Width = 70;
            dgvProcesos.Columns["Estado"].ReadOnly = true; dgvProcesos.Columns["Estado"].Width = 50;
            dgvProcesos.Columns["Tiempo Transcurrido"].ReadOnly = true; dgvProcesos.Columns["Tiempo Transcurrido"].Width = 90;
            dgvProcesos.Columns["Nombre"].Width = 250; dgvProcesos.Columns["Duración"].Width = 70;
            dgvProcesos.Columns["Tiempo Inicio"].Visible = false;
            dgvProcesos.Columns["ParticionID"].ReadOnly = true;
            //agregar fila para el primer proceso
            DataRow fila = tabProcesos.NewRow();
            fila["ID"] = 1;
            fila["Duración"] = random.Next(min, max);
            fila["Estado"] = 2;
            fila["Tiempo Transcurrido"] = 0;
            fila["tiempo inicio"] = -1;
            fila["ParticionID"] = 0;
            tabProcesos.Rows.Add(fila);
            dibujarParticiones();            
        }
        private void dibujarParticiones()
        {
            panMemoria.Controls.Clear();

            // Crear y agregar los paneles internos
            for (int i = 0; i < particiones-1; i++)
            {
                Panel panelParticion = new Panel();
                panelParticion.BackColor = System.Drawing.Color.Green;
                panelParticion.Dock = DockStyle.Top;
                panelParticion.Height = panMemoria.Height / particiones;
                panelParticion.BorderStyle = BorderStyle.FixedSingle;
                panelParticion.Name = "particion" + i;


                // Crear y configurar el label dentro del panel interno
                Label lblTexto = new Label();
                lblTexto.Font = new Font("Verdana", 11, FontStyle.Regular);
                lblTexto.Text = panelParticion.Name + "      porcentaje usado:0%           tamaño: " + tabParticiones.Rows[i ]["tamaño"];
                lblTexto.AutoSize = true;
                lblTexto.Location = new Point(panelParticion.Width / 2, panelParticion.Height / 2);

                // Agregar el label al panel interno
                panelParticion.Controls.Add(lblTexto);

                // Agregar el panel interno al contenedor
                panMemoria.Controls.Add(panelParticion);
            }
        }
        private void actualizarGestor()
        {
            gestor = new Gestor(tabProcesos, tabParticiones);
            gestor.getParticionesList();
            gestor.getProcesosList();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {            
            DataRow fila;

            if (validarProceso())
            {
                fila = tabProcesos.NewRow();
                fila["ID"] = int.Parse(tabProcesos.Rows[tabProcesos.Rows.Count - 1]["ID"].ToString()) + 1;
                fila["Duración"] = random.Next(min, max);
                fila["Estado"] = 2;
                fila["Tiempo Transcurrido"] = 0;
                fila["particionID"] = 0;
                fila["tiempo inicio"] = -1;
                tabProcesos.Rows.Add(fila);
            }

        }


        private void dgvProcesos_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == dgvProcesos.Columns["Memoria Requerida"].Index)
            {
                // Obtén el valor de la celda que se está validando
                string valor = e.FormattedValue.ToString();

                // Intenta convertir el valor a un número
                int numero;
                if (!int.TryParse(valor, out numero))
                {
                    // Si no se puede convertir a número, muestra un mensaje de error
                    MessageBox.Show("Ingrese solo números en la columna 'Memoria Requerida'.", "Error de validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Cancela la edición de la celda
                    dgvProcesos.CancelEdit();
                }
            }
        }

        private void dgvProcesos_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

        }

        private void dgvProcesos_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void FlowLayoutPanelMemoria_Resize(object sender, EventArgs e)
        {
        }
        private void flowLayoutPanelMemoria_ControlAdded(object sender, ControlEventArgs e)
        {

        }

        private int procesosNoVacios()
        {
            bool procesosVacios = false;
            //verificar que los datos que el usuario debe ingresar no estén vacíos            

            foreach (DataRow row in tabProcesos.Rows)
            {
                if (row["Nombre"].ToString().Length == 0 || row["Memoria requerida"].ToString().Length == 0)
                    return -1;//significa que faltan datos

                if (int.Parse(row["Memoria requerida"].ToString()) == 0)
                    return -2;
            }
            return 0;
        }
        private bool validarProceso()
        {
            DataRow f = tabProcesos.NewRow();
            f = tabProcesos.Rows[tabProcesos.Rows.Count - 1];


            if (f["Nombre"].ToString().Length == 0 || f["Memoria Requerida"].ToString().Length == 0)
            {//verificar que al proceso anterior no le falten datos y que el tamaño no sea mayor que la partición más grande
                MessageBox.Show("Complete los datos del último proceso antes de agregar otro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            actualizarGestor();//reinicializar el gestor con los procesos agregados
            //hasta el momento (para que el procesosList esté actualizado)
            Particion p = gestor.particionMasGrande();///REVISAAAARRR FUNCION PARTICION MAS GRANDE
            if (int.Parse(f["Memoria Requerida"].ToString()) > p.size)
            {
                MessageBox.Show("El proceso requiere demasiada memoria y no podrá ser ejecutado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvProcesos.CurrentRow.Cells[3].Value = p.size;
                MessageBox.Show("Tamaño corregido al máximo permitido", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }
        private void btnIniciar_Click(object sender, EventArgs e)
        {
            int bandera = procesosNoVacios();
            if (bandera == -1)
            {
                MessageBox.Show("Faltan datos de los procesos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (bandera == -2)
            {
                MessageBox.Show("La cantidad de memoria requerida de un proceso no puede ser cero", "Error con la memoria requerida", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else
            {
                if (validarProceso())
                    simulacion = true;
                else
                    return;
                btnAgregar.Visible = false;
                btnIniciar.Visible = false;
                dgvProcesos.ReadOnly = true;
                btnDetener.Visible = true;
                hiloPrincipal = new Thread(new ThreadStart(accionesPeriodicas));
                hiloPrincipal.Start();
            }


        }
        public void accionesPeriodicas()
        {
            while (simulacion)
            {             
                actualizarGestor();

                    if (gestor.verificarProcesoSaliente() || gestor.particionesLibres() > 0)
                    {
                        if (filaInfo["Politica"].Equals("Primer Ajuste"))
                        {
                            gestor.firstFit();
                        }
                        else
                        {
                            gestor.BestFit();
                        }
                    }

                particionesList = gestor.ParticionesList;
                procesosList = gestor.ProcesosList;  

                Thread.Sleep(1000);

                dgvProcesos.Invoke((MethodInvoker)delegate
                {                    
                    dgvProcesos.DataSource = null;
                    tabProcesos.Rows.Clear();
                    tabProcesos = gestor.getTableProcesos();
                    dgvProcesos.DataSource = tabProcesos;
                    dgvProcesos.Refresh();
                    
                });
                tabParticiones.Rows.Clear();
                tabParticiones = gestor.getTableParticiones();


                //recorrer los procesos y llamar a la función de actualizar paneles para la particionid que ocupan
                foreach(Proceso proceso in procesosList)
                {
                    if(proceso.ParticionID != -1)
                    {
                        if(proceso.ParticionID >0 )
                        {
                            actualizarPanel(proceso.ParticionID - 1, proceso);
                        }
                    }
                }

                panelesLibres();
               
                
                if (gestor.procesosTerminados == procesosList.Count) //verificar que ya no hayan procesos para ejecutar
                    simulacion = false;


                                                
            }
        }
        private void actualizarPanel(int particionID, Proceso proceso)
        {
            panMemoria.Invoke((MethodInvoker)delegate
            {
                panMemoria.Controls[particionID].BackColor = Color.Yellow;
                panMemoria.Controls[particionID].Text = "Particion " + particionID + "      " +
                "porcentaje usado: " + particionesList[particionID].size / proceso.MemoriaRequerida + 
                "%          tamaño: " + particionesList[particionID].size;
            });
            
        }
        private void panelesLibres()
        {
            //pintar paneles libres
            for (int i =0;i<particionesList.Count;i++) 
            {
                if (!particionesList[i].ocupado)
                {
                    panMemoria.Invoke((MethodInvoker)delegate
                    {
                        panMemoria.Controls[i].BackColor = Color.Green;
                        panMemoria.Controls[i].Text = "Particion Libre";
                    });
                }
            }
        }
        private void btnDetener_Click(object sender, EventArgs e)
        {
            particionesList = gestor.ParticionesList;
            procesosList = gestor.ProcesosList;
            simulacion = false;
        }
    }
}
