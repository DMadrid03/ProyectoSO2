using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoSO2
{
    public partial class frmRegistrarProcesos : Form
    {
        public frmRegistrarProcesos()
        {
           // InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            //verificar datos en los txt y limpiarlos despues de agregar
        }

        private void txtMemoria_keyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void frmRegistrarProcesos_Load(object sender, EventArgs e)
        {
            
        }
    }
}
