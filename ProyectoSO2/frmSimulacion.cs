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
        Gestor gestor;
        public frmSimulacion(DataRow fila, DataTable tbPart)
        {
            InitializeComponent();

            tabParticiones = tbPart;
            filaInfo = fila;
            tabProcesos = new DataTable();
            gestor = new Gestor(tabProcesos, tabParticiones);


            particiones = int.Parse(filaInfo["nParticiones"].ToString()) + 1;
            min = int.Parse(filaInfo["min"].ToString());
            max = int.Parse(filaInfo["max"].ToString());
            random = new Random();

            flowLayoutPanelMemoria.BackColor = Color.Gray;

            flowLayoutPanelMemoria.ControlAdded += flowLayoutPanelMemoria_ControlAdded;
            CrearParticiones();
        }

        private void frmSimulacion_Load(object sender, EventArgs e)
        {
            flowLayoutPanelMemoria.Resize += FlowLayoutPanelMemoria_Resize;

            tabProcesos.Columns.Add("ID");
            tabProcesos.Columns.Add("Nombre");
            tabProcesos.Columns.Add("Duración");
            tabProcesos.Columns.Add("Memoria Requerida");
            tabProcesos.Columns.Add("Estado");
            tabProcesos.Columns.Add("Tiempo Transcurrido");
            dgvProcesos.DataSource = tabProcesos;

            dgvProcesos.Columns["ID"].ReadOnly = true; dgvProcesos.Columns["ID"].Width = 35;
            dgvProcesos.Columns["Duración"].ReadOnly = true; dgvProcesos.Columns["Memoria Requerida"].Width = 70;
            dgvProcesos.Columns["Estado"].ReadOnly = true; dgvProcesos.Columns["Estado"].Width = 50;
            dgvProcesos.Columns["Tiempo Transcurrido"].ReadOnly = true; dgvProcesos.Columns["Tiempo Transcurrido"].Width = 90;
            dgvProcesos.Columns["Nombre"].Width = 250; dgvProcesos.Columns["Duración"].Width = 70;
            //agregar fila para el primer proceso
            DataRow fila = tabProcesos.NewRow();
            fila["ID"] = 1;
            fila["Duración"] = random.Next(min, max);
            fila["Estado"] = 0;
            fila["Tiempo Transcurrido"] = 0;

            tabProcesos.Rows.Add(fila);
        }
        private void actualizarGestor()
        {
            gestor = new Gestor(tabProcesos, tabParticiones);
            gestor.getParticionesList();
            gestor.getProcesosList();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            
            if (validarProceso())
            {
                DataRow fila = tabProcesos.NewRow();
                fila["ID"] = int.Parse(tabProcesos.Rows[tabProcesos.Rows.Count - 1]["ID"].ToString()) + 1;
                fila["Duración"] = random.Next(min, max);
                fila["Estado"] = 0;
                fila["Tiempo Transcurrido"] = 0;

                tabProcesos.Rows.Add(fila);
            }
            
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
            Particion p = gestor.particionMasGrande();
            if (int.Parse(f["Memoria Requerida"].ToString()) > p.size)
            {
                MessageBox.Show("El proceso requiere demasiada memoria y no podrá ser ejecutado","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                dgvProcesos.CurrentRow.Cells[3].Value = p.size;
                MessageBox.Show("Tamaño corregido al máximo permitido","Información",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return false;
            }
            return true;
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
            // Recalcular el ancho de las particiones cuando cambie el tamaño del panel
            RecalcularAnchoParticiones();
        }
        private void CrearParticiones()
        {
            // Eliminar cualquier control existente en el FlowLayoutPanel
            flowLayoutPanelMemoria.Controls.Clear();

            // Calcular el ancho de cada partición (división entera)
            int anchoParticion = flowLayoutPanelMemoria.Width / particiones;
            int anchoRestante = flowLayoutPanelMemoria.Width % particiones;

            // Crear y agregar las particiones al FlowLayoutPanel
            for (int i = 0; i < particiones; i++)
            {
                
                FlowLayoutPanel particion = new FlowLayoutPanel();
                particion.BackColor = System.Drawing.Color.Green;
                particion.BorderStyle = BorderStyle.FixedSingle;
                particion.Height = flowLayoutPanelMemoria.Height; // Mismo alto que el FlowLayoutPanel principal
                particion.Name = "particion_" + i;
                // Ajustar el ancho de la última partición para ocupar el espacio restante
                if (i == particiones - 1)
                {
                    anchoParticion += anchoRestante;
                }

                particion.Width = anchoParticion;


                // Crear y configurar el label
                Label lblInfoParticion = new Label();
                lblInfoParticion.Text = "Particion " + i + "\nTamaño = " + tabParticiones.Rows[0][1];                
                lblInfoParticion.AutoSize = false;
                lblInfoParticion.TextAlign = ContentAlignment.MiddleCenter;
                lblInfoParticion.Dock = DockStyle.Fill; // Para que se expanda y se centre dentro del FlowLayoutPanel
                lblInfoParticion.Font = new Font(lblInfoParticion.Font.FontFamily, 50, FontStyle.Bold);
                lblInfoParticion.Visible = true;
                // Agregar el label a la partición
                particion.Controls.Add(lblInfoParticion);
                
                

                // Agregar la partición al FlowLayoutPanel
                flowLayoutPanelMemoria.Controls.Add(particion);
            }          
        }

        private void RecalcularAnchoParticiones()
        {
            // Calcular el ancho de cada partición
            int anchoParticion = flowLayoutPanelMemoria.Width / particiones;

            // Ajustar el ancho de cada partición
            foreach (FlowLayoutPanel particion in flowLayoutPanelMemoria.Controls)
            {
                particion.Width = anchoParticion;
            }
        }

        private void flowLayoutPanelMemoria_ControlAdded(object sender, ControlEventArgs e)
        {
            // Verificar si todos los controles han sido agregados
            if (flowLayoutPanelMemoria.Controls.Count == particiones)
            {
                // Calcular el ancho de cada partición después de que todos los controles han sido agregados
                int anchoParticion = flowLayoutPanelMemoria.Width / particiones;

                // Ajustar el ancho de cada partición
                foreach (FlowLayoutPanel particion in flowLayoutPanelMemoria.Controls)
                {
                    particion.Width = anchoParticion;
                }
            }
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            actualizarGestor();
            

            if(gestor.verificarProcesoSaliente() || gestor.particionesLibres() > 0)
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

            btnAgregar.Visible = false;
            btnIniciar.Visible = false;
            //btnDetener.Visible = true;
            

        }
    }
}
