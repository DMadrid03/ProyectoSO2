namespace ProyectoSO2
{
    partial class frmSimulacion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            dgvProcesos = new DataGridView();
            btnAgregar = new Button();
            label1 = new Label();
            label2 = new Label();
            btnIniciar = new Button();
            flowLayoutPanelMemoria = new FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)dgvProcesos).BeginInit();
            SuspendLayout();
            // 
            // dgvProcesos
            // 
            dgvProcesos.AllowUserToAddRows = false;
            dgvProcesos.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(192, 192, 255);
            dataGridViewCellStyle1.Font = new Font("Nirmala UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dgvProcesos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvProcesos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProcesos.GridColor = SystemColors.InactiveCaption;
            dgvProcesos.Location = new Point(26, 57);
            dgvProcesos.Name = "dgvProcesos";
            dgvProcesos.Size = new Size(626, 150);
            dgvProcesos.TabIndex = 1;
            dgvProcesos.CellValidating += dgvProcesos_CellValidating;
            dgvProcesos.EditingControlShowing += dgvProcesos_EditingControlShowing;
            dgvProcesos.KeyPress += dgvProcesos_KeyPress;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(658, 57);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(133, 29);
            btnAgregar.TabIndex = 2;
            btnAgregar.Text = "Nuevo Proceso";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold);
            label1.Location = new Point(26, 30);
            label1.Name = "label1";
            label1.Size = new Size(129, 20);
            label1.TabIndex = 3;
            label1.Text = "Agregar procesos";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(26, 231);
            label2.Name = "label2";
            label2.Size = new Size(84, 20);
            label2.TabIndex = 4;
            label2.Text = "Simulación";
            // 
            // btnIniciar
            // 
            btnIniciar.Location = new Point(658, 156);
            btnIniciar.Name = "btnIniciar";
            btnIniciar.Size = new Size(133, 51);
            btnIniciar.TabIndex = 5;
            btnIniciar.Text = "Iniciar Simulación";
            btnIniciar.UseVisualStyleBackColor = true;
            btnIniciar.Click += btnIniciar_Click;
            // 
            // flowLayoutPanelMemoria
            // 
            flowLayoutPanelMemoria.Location = new Point(26, 271);
            flowLayoutPanelMemoria.Name = "flowLayoutPanelMemoria";
            flowLayoutPanelMemoria.Size = new Size(765, 265);
            flowLayoutPanelMemoria.TabIndex = 6;
            flowLayoutPanelMemoria.ControlAdded += flowLayoutPanelMemoria_ControlAdded;
            // 
            // frmSimulacion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(812, 584);
            Controls.Add(flowLayoutPanelMemoria);
            Controls.Add(btnIniciar);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnAgregar);
            Controls.Add(dgvProcesos);
            Name = "frmSimulacion";
            Text = "frmSimulacion";
            Load += frmSimulacion_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProcesos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView dgvProcesos;
        private Button btnAgregar;
        private Label label1;
        private Label label2;
        private Button btnIniciar;
        private FlowLayoutPanel flowLayoutPanelMemoria;
    }
}