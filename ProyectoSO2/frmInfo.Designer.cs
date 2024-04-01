namespace ProyectoSO2
{
    partial class frmInfo
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            dgvParticiones = new DataGridView();
            txtNParticiones = new TextBox();
            label4 = new Label();
            label5 = new Label();
            cmbPolitica = new ComboBox();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            txtMin = new TextBox();
            txtMax = new TextBox();
            btnSiguiente = new Button();
            btnCrear = new Button();
            txtTamanoTotal = new TextBox();
            btnValidar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvParticiones).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Verdana", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(112, 31);
            label1.Name = "label1";
            label1.Size = new Size(432, 18);
            label1.TabIndex = 0;
            label1.Text = "Información inicial para el simulador de memoria";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(58, 90);
            label2.Name = "label2";
            label2.Size = new Size(124, 18);
            label2.TabIndex = 1;
            label2.Text = "Tamaño Total:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(366, 90);
            label3.Name = "label3";
            label3.Size = new Size(165, 18);
            label3.TabIndex = 2;
            label3.Text = "No. de particiones:";
            // 
            // dgvParticiones
            // 
            dgvParticiones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvParticiones.Location = new Point(366, 147);
            dgvParticiones.Name = "dgvParticiones";
            dgvParticiones.Size = new Size(271, 177);
            dgvParticiones.TabIndex = 3;
            dgvParticiones.CellContentClick += dgvParticiones_CellContentClick;
            dgvParticiones.CellValidating += dgvParticiones_CellValidating;
            dgvParticiones.CellValueChanged += dgvParticiones_CellValueChanged;
            dgvParticiones.Enter += dgvParticiones_Enter;
            // 
            // txtNParticiones
            // 
            txtNParticiones.Location = new Point(551, 90);
            txtNParticiones.Name = "txtNParticiones";
            txtNParticiones.Size = new Size(86, 23);
            txtNParticiones.TabIndex = 5;
            txtNParticiones.Text = "0";
            txtNParticiones.TextChanged += txtNParticiones_TextChanged;
            txtNParticiones.KeyPress += txtNParticiones_keyPress;
            txtNParticiones.KeyUp += txtNParticiones_KeyUp;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label4.Location = new Point(236, 319);
            label4.Name = "label4";
            label4.Size = new Size(73, 15);
            label4.TabIndex = 6;
            label4.Text = "En segundos";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(58, 129);
            label5.Name = "label5";
            label5.Size = new Size(75, 18);
            label5.TabIndex = 7;
            label5.Text = "Política:";
            // 
            // cmbPolitica
            // 
            cmbPolitica.FormattingEnabled = true;
            cmbPolitica.Items.AddRange(new object[] { "Primer Ajuste", "Mejor Ajuste" });
            cmbPolitica.Location = new Point(188, 129);
            cmbPolitica.Name = "cmbPolitica";
            cmbPolitica.Size = new Size(121, 23);
            cmbPolitica.TabIndex = 8;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(94, 192);
            label6.Name = "label6";
            label6.Size = new Size(180, 18);
            label6.TabIndex = 9;
            label6.Text = "Intervalos de tiempo";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(58, 242);
            label7.Name = "label7";
            label7.Size = new Size(79, 18);
            label7.TabIndex = 9;
            label7.Text = "Mínimo: ";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Verdana", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(58, 279);
            label8.Name = "label8";
            label8.Size = new Size(83, 18);
            label8.TabIndex = 9;
            label8.Text = "Máximo: ";
            // 
            // txtMin
            // 
            txtMin.Location = new Point(147, 242);
            txtMin.Name = "txtMin";
            txtMin.Size = new Size(162, 23);
            txtMin.TabIndex = 10;
            txtMin.Text = "15";
            txtMin.KeyPress += txtMin_KeyPress;
            // 
            // txtMax
            // 
            txtMax.Location = new Point(147, 279);
            txtMax.Name = "txtMax";
            txtMax.Size = new Size(162, 23);
            txtMax.TabIndex = 11;
            txtMax.Text = "60";
            txtMax.KeyPress += txtMax_KeyPress;
            // 
            // btnSiguiente
            // 
            btnSiguiente.Cursor = Cursors.Hand;
            btnSiguiente.Location = new Point(562, 366);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(75, 23);
            btnSiguiente.TabIndex = 12;
            btnSiguiente.Text = "Siguiente";
            btnSiguiente.UseVisualStyleBackColor = true;
            btnSiguiente.Click += btnSiguiente_Click;
            // 
            // btnCrear
            // 
            btnCrear.Location = new Point(562, 118);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(75, 23);
            btnCrear.TabIndex = 13;
            btnCrear.Text = "Crear";
            btnCrear.UseVisualStyleBackColor = true;
            btnCrear.Click += btnCrear_Click;
            // 
            // txtTamanoTotal
            // 
            txtTamanoTotal.Location = new Point(188, 90);
            txtTamanoTotal.Name = "txtTamanoTotal";
            txtTamanoTotal.Size = new Size(121, 23);
            txtTamanoTotal.TabIndex = 14;
            txtTamanoTotal.Text = "100";
            txtTamanoTotal.KeyPress += txtTamanoTotal_KeyPress;
            // 
            // btnValidar
            // 
            btnValidar.Location = new Point(562, 337);
            btnValidar.Name = "btnValidar";
            btnValidar.Size = new Size(75, 23);
            btnValidar.TabIndex = 15;
            btnValidar.Text = "Siguiente";
            btnValidar.UseVisualStyleBackColor = true;
            btnValidar.Click += btnValidar_Click;
            // 
            // frmInfo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(670, 401);
            Controls.Add(btnValidar);
            Controls.Add(txtTamanoTotal);
            Controls.Add(btnCrear);
            Controls.Add(btnSiguiente);
            Controls.Add(txtMax);
            Controls.Add(txtMin);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(cmbPolitica);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(txtNParticiones);
            Controls.Add(dgvParticiones);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "frmInfo";
            Text = "Form1";
            Load += frmInfo_Load;
            ((System.ComponentModel.ISupportInitialize)dgvParticiones).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private DataGridView dgvParticiones;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        public TextBox txtNParticiones;
        public TextBox txtMin;
        public TextBox txtMax;
        private Button btnSiguiente;
        private Button btnCrear;
        public ComboBox cmbPolitica;
        private TextBox txtTamanoTotal;
        private Button btnValidar;
    }
}
