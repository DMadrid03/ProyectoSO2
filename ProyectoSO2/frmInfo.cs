using System.Data;
namespace ProyectoSO2
{
    public partial class frmInfo : Form
    {
        DataTable tabParticiones;
        int tamanoTotal;
        
        public frmInfo()
        {
            InitializeComponent();
            tabParticiones = new DataTable();
            tabParticiones.Columns.Add("Numero"); tabParticiones.Columns.Add("Tamaño");
            tabParticiones.Columns.Add("Ocupado");
            dgvParticiones.DataSource = tabParticiones;
        }
        private void LimitarATextoNumerico(TextBox textBox, KeyPressEventArgs e)
        {
            // Verificar si la tecla presionada es un número o una tecla de control
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignorar la entrada de caracteres no numéricos
            }
        }

        private void txtTamanoTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            LimitarATextoNumerico(txtTamanoTotal, e);
        }
        private void txtNParticiones_keyPress(object sender, KeyPressEventArgs e)
        {
            LimitarATextoNumerico(txtNParticiones, e);
        }
        private void txtMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            LimitarATextoNumerico(txtMin, e);
        }
        private void txtMax_KeyPress(object sender, KeyPressEventArgs e)
        {
            LimitarATextoNumerico(txtMax, e);
        }

        private void frmInfo_Load(object sender, EventArgs e)
        {
            cmbPolitica.SelectedIndex= 0;
            txtNParticiones.Text = "2";
            btnSiguiente.Visible = false;
            btnValidar.Visible = false;
            dgvParticiones.Columns["Numero"].ReadOnly = true;
            dgvParticiones.Columns["Tamaño"].ReadOnly = true;
            dgvParticiones.Columns["ocupado"].Visible = false;
            dgvParticiones.AllowUserToAddRows = false;
            dgvParticiones.AllowUserToDeleteRows = false;

        }

        private void txtNParticiones_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNParticiones_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtNParticiones.Text) > 10)
            {
                MessageBox.Show("Muchas particiones para mostrar, el máximo son 10", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNParticiones.Text = "10";
                return;
            }
            if (int.Parse(txtNParticiones.Text) > int.Parse(txtTamanoTotal.Text) / 10)
            {
                MessageBox.Show("Demasiadas particiones, el tamaño máximo es: " + (int.Parse(txtTamanoTotal.Text)/10) + " (10% del tamaño total) ", "Error, pruebe aumentar el tamaño total de memoria", MessageBoxButtons.OK, MessageBoxIcon.Error);                
                return;
            }            
            if (txtTamanoTotal.Text.Length == 0 || txtTamanoTotal.Text == " " || int.Parse(txtNParticiones.Text)==0)
            {
                MessageBox.Show("Error, ingrese antes el tamaño total de la memoria y la cantidad de particiones mayor a cero");
                return;
            }
            else
            {
                try
                {
                    tamanoTotal = int.Parse(txtTamanoTotal.Text);
                    int n = int.Parse(txtNParticiones.Text);
                    for (int i = 1; i <= n; i++)
                    {
                        tabParticiones.Rows.Add(i, tamanoTotal/n,false);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("error ");
                }

                btnCrear.Enabled = false;
                txtTamanoTotal.Enabled = false;
                dgvParticiones.Columns["Tamaño"].ReadOnly = false;
                btnValidar.Visible = true;
            }
        }

        private void dgvParticiones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("tamTot", typeof(float));
            dt.Columns.Add("politica", typeof(String));
            dt.Columns.Add("nParticiones", typeof(int));
            dt.Columns.Add("min", typeof(int));
            dt.Columns.Add("max", typeof(int));

            DataRow fila = dt.NewRow();
            fila["tamTot"] = tamanoTotal;
            fila["politica"] = cmbPolitica.SelectedItem.ToString();
            fila["nParticiones"] = int.Parse(txtNParticiones.Text);
            fila["min"] = int.Parse(txtMin.Text);
            fila["max"] = int.Parse(txtMax.Text);
            
            frmSimulacion frmSim = new frmSimulacion(fila,tabParticiones);
            frmSim.Show();            
        }

        private void dgvParticiones_Enter(object sender, EventArgs e)
        {

        }

        private void dgvParticiones_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == dgvParticiones.Columns["Tamaño"].Index)
            {
                // Obtén el valor de la celda que se está validando
                string valor = e.FormattedValue.ToString();

                // Intenta convertir el valor a un número
                int numero;
                if (!int.TryParse(valor, out numero))
                {
                    // Si no se puede convertir a número, muestra un mensaje de error
                    MessageBox.Show("Ingrese solo números en la columna 'Tamaño '.", "Error de validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Cancela la edición de la celda
                    dgvParticiones.CancelEdit();
                }
                if (numero < 0)
                {
                    DialogResult result = MessageBox.Show("El tamaño de la partición no puede ser negativo. ¿Desea cambiarlo a positivo?", "Error de datos de entrada", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    if (result == DialogResult.OK)
                    {
                        tabParticiones.Rows[e.RowIndex][e.ColumnIndex] = Math.Abs(numero);
                        dgvParticiones.Refresh();
                    }
                    else
                    {
                        dgvParticiones.CancelEdit();
                    }
                }
                
            }
        }


        private void dgvParticiones_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            if (cmbPolitica.SelectedIndex != -1 && txtMax.Text.Length != 0 && txtMin.Text.Length !=0)
            {
                int totalingresado = 0;
                //validar que la suma de las particiones sea = total memoria
                for (int i = 0; i < int.Parse(txtNParticiones.Text); i++)
                {
                    int n = int.Parse(tabParticiones.Rows[i]["Tamaño"].ToString());
                    if (n < 0)
                        n = n * -1;
                    totalingresado += +n;
                }
                
                if (totalingresado > tamanoTotal)
                {
                    MessageBox.Show("La suma del tamaño de las particiones excede el total de memoria por: " + (totalingresado - tamanoTotal), "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                if (totalingresado < tamanoTotal)
                {
                    MessageBox.Show("Debe agregar " + (tamanoTotal - totalingresado) + " espacio en las particiones", "Falta espacio para completar el total de memoria");
                    return;
                }
                if(int.Parse(txtMax.Text)<= int.Parse(txtMin.Text))
                {
                    MessageBox.Show("Error en el intervalo","Error",MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                //btnSiguiente_Click(sender, new EventArgs());
                DataTable dt = new DataTable();
                dt.Columns.Add("tamTot", typeof(float));
                dt.Columns.Add("politica", typeof(String));
                dt.Columns.Add("nParticiones", typeof(int));
                dt.Columns.Add("min", typeof(int));
                dt.Columns.Add("max", typeof(int));

                DataRow fila = dt.NewRow();
                fila["tamTot"] = tamanoTotal;
                fila["politica"] = cmbPolitica.SelectedItem.ToString();
                fila["nParticiones"] = int.Parse(txtNParticiones.Text);
                fila["min"] = int.Parse(txtMin.Text);
                fila["max"] = int.Parse(txtMax.Text);

                frmSimulacion frmSim = new frmSimulacion(fila, tabParticiones);
                dgvParticiones.DataSource = null;
                frmSim.Show();
                
            }
            else
            {
                MessageBox.Show("Faltan Datos","Error",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }           

        }
    }
}


//va haber un hilo y si hay un proceso que se 
//    se va a revisar si se puede sacr un hilo
//    después revisar si se puede meter otro

//    Actualizar estados