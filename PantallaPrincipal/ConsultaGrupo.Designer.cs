namespace AulaGO
{
    partial class ConsultaGrupo
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DGVMateriasDocentes = new System.Windows.Forms.DataGridView();
            this.lblId = new System.Windows.Forms.Label();
            this.bckgAlumnos = new System.ComponentModel.BackgroundWorker();
            this.btnAgregarAlumno = new System.Windows.Forms.Button();
            this.btnEliminarAlumno = new System.Windows.Forms.Button();
            this.lblMateriasDocentes = new System.Windows.Forms.Label();
            this.lblAlumnos = new System.Windows.Forms.Label();
            this.DGVAlumnos = new System.Windows.Forms.DataGridView();
            this.btnAsignarDocenteAMateria = new System.Windows.Forms.Button();
            this.btnDesasignarDocenteAMateria = new System.Windows.Forms.Button();
            this.btnDesasignarMateria = new System.Windows.Forms.Button();
            this.btnAsignarMateria = new System.Windows.Forms.Button();
            this.lblAuxId = new System.Windows.Forms.Label();
            this.lblAuxOrientacion = new System.Windows.Forms.Label();
            this.lblOrientacion = new System.Windows.Forms.Label();
            this.lblAuxAnio = new System.Windows.Forms.Label();
            this.lblAnio = new System.Windows.Forms.Label();
            this.bckgMateriasDocentes = new System.ComponentModel.BackgroundWorker();
            this.btnVolver = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGVMateriasDocentes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVAlumnos)).BeginInit();
            this.SuspendLayout();
            // 
            // DGVMateriasDocentes
            // 
            this.DGVMateriasDocentes.AllowUserToAddRows = false;
            this.DGVMateriasDocentes.AllowUserToDeleteRows = false;
            this.DGVMateriasDocentes.AllowUserToOrderColumns = true;
            this.DGVMateriasDocentes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGVMateriasDocentes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVMateriasDocentes.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVMateriasDocentes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGVMateriasDocentes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGVMateriasDocentes.DefaultCellStyle = dataGridViewCellStyle2;
            this.DGVMateriasDocentes.Location = new System.Drawing.Point(29, 223);
            this.DGVMateriasDocentes.MultiSelect = false;
            this.DGVMateriasDocentes.Name = "DGVMateriasDocentes";
            this.DGVMateriasDocentes.ReadOnly = true;
            this.DGVMateriasDocentes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGVMateriasDocentes.Size = new System.Drawing.Size(598, 618);
            this.DGVMateriasDocentes.TabIndex = 50;
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblId.Location = new System.Drawing.Point(224, 34);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(64, 55);
            this.lblId.TabIndex = 51;
            this.lblId.Text = "Id";
            // 
            // bckgAlumnos
            // 
            this.bckgAlumnos.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bckgAlumnos_DoWork);
            this.bckgAlumnos.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bckgAlumnos_RunWorkerCompleted);
            // 
            // btnAgregarAlumno
            // 
            this.btnAgregarAlumno.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarAlumno.Location = new System.Drawing.Point(1356, 222);
            this.btnAgregarAlumno.Name = "btnAgregarAlumno";
            this.btnAgregarAlumno.Size = new System.Drawing.Size(233, 41);
            this.btnAgregarAlumno.TabIndex = 49;
            this.btnAgregarAlumno.Text = "Agregar Alumno";
            this.btnAgregarAlumno.UseVisualStyleBackColor = true;
            this.btnAgregarAlumno.Click += new System.EventHandler(this.btnAgregarAlumno_Click);
            // 
            // btnEliminarAlumno
            // 
            this.btnEliminarAlumno.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarAlumno.Location = new System.Drawing.Point(1356, 275);
            this.btnEliminarAlumno.Name = "btnEliminarAlumno";
            this.btnEliminarAlumno.Size = new System.Drawing.Size(233, 45);
            this.btnEliminarAlumno.TabIndex = 48;
            this.btnEliminarAlumno.Text = "Eliminar Alumno";
            this.btnEliminarAlumno.UseVisualStyleBackColor = true;
            this.btnEliminarAlumno.Click += new System.EventHandler(this.btnEliminarAlumno_Click);
            // 
            // lblMateriasDocentes
            // 
            this.lblMateriasDocentes.AutoSize = true;
            this.lblMateriasDocentes.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMateriasDocentes.Location = new System.Drawing.Point(19, 152);
            this.lblMateriasDocentes.Name = "lblMateriasDocentes";
            this.lblMateriasDocentes.Size = new System.Drawing.Size(462, 55);
            this.lblMateriasDocentes.TabIndex = 52;
            this.lblMateriasDocentes.Text = "Materias y Docentes";
            // 
            // lblAlumnos
            // 
            this.lblAlumnos.AutoSize = true;
            this.lblAlumnos.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlumnos.Location = new System.Drawing.Point(875, 152);
            this.lblAlumnos.Name = "lblAlumnos";
            this.lblAlumnos.Size = new System.Drawing.Size(211, 55);
            this.lblAlumnos.TabIndex = 54;
            this.lblAlumnos.Text = "Alumnos";
            // 
            // DGVAlumnos
            // 
            this.DGVAlumnos.AllowUserToAddRows = false;
            this.DGVAlumnos.AllowUserToDeleteRows = false;
            this.DGVAlumnos.AllowUserToOrderColumns = true;
            this.DGVAlumnos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGVAlumnos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVAlumnos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedHorizontal;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVAlumnos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.DGVAlumnos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGVAlumnos.DefaultCellStyle = dataGridViewCellStyle4;
            this.DGVAlumnos.Location = new System.Drawing.Point(885, 223);
            this.DGVAlumnos.MultiSelect = false;
            this.DGVAlumnos.Name = "DGVAlumnos";
            this.DGVAlumnos.ReadOnly = true;
            this.DGVAlumnos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGVAlumnos.Size = new System.Drawing.Size(465, 618);
            this.DGVAlumnos.TabIndex = 53;
            // 
            // btnAsignarDocenteAMateria
            // 
            this.btnAsignarDocenteAMateria.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAsignarDocenteAMateria.Location = new System.Drawing.Point(640, 353);
            this.btnAsignarDocenteAMateria.Name = "btnAsignarDocenteAMateria";
            this.btnAsignarDocenteAMateria.Size = new System.Drawing.Size(233, 41);
            this.btnAsignarDocenteAMateria.TabIndex = 56;
            this.btnAsignarDocenteAMateria.Text = "Asignar docente";
            this.btnAsignarDocenteAMateria.UseVisualStyleBackColor = true;
            this.btnAsignarDocenteAMateria.Click += new System.EventHandler(this.btnAsignarDocenteAMateria_Click);
            // 
            // btnDesasignarDocenteAMateria
            // 
            this.btnDesasignarDocenteAMateria.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDesasignarDocenteAMateria.Location = new System.Drawing.Point(640, 400);
            this.btnDesasignarDocenteAMateria.Name = "btnDesasignarDocenteAMateria";
            this.btnDesasignarDocenteAMateria.Size = new System.Drawing.Size(233, 45);
            this.btnDesasignarDocenteAMateria.TabIndex = 55;
            this.btnDesasignarDocenteAMateria.Text = "Desasignar Docente";
            this.btnDesasignarDocenteAMateria.UseVisualStyleBackColor = true;
            this.btnDesasignarDocenteAMateria.Click += new System.EventHandler(this.btnDesasignarDocenteAMateria_Click);
            // 
            // btnDesasignarMateria
            // 
            this.btnDesasignarMateria.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDesasignarMateria.Location = new System.Drawing.Point(640, 267);
            this.btnDesasignarMateria.Name = "btnDesasignarMateria";
            this.btnDesasignarMateria.Size = new System.Drawing.Size(233, 45);
            this.btnDesasignarMateria.TabIndex = 55;
            this.btnDesasignarMateria.Text = "Desasignar Materia";
            this.btnDesasignarMateria.UseVisualStyleBackColor = true;
            this.btnDesasignarMateria.Click += new System.EventHandler(this.btnDesasignarMateria_Click);
            // 
            // btnAsignarMateria
            // 
            this.btnAsignarMateria.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAsignarMateria.Location = new System.Drawing.Point(640, 223);
            this.btnAsignarMateria.Name = "btnAsignarMateria";
            this.btnAsignarMateria.Size = new System.Drawing.Size(233, 41);
            this.btnAsignarMateria.TabIndex = 56;
            this.btnAsignarMateria.Text = "Asignar Materia";
            this.btnAsignarMateria.UseVisualStyleBackColor = true;
            this.btnAsignarMateria.Click += new System.EventHandler(this.btnAsignarMateria_Click);
            // 
            // lblAuxId
            // 
            this.lblAuxId.AutoSize = true;
            this.lblAuxId.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAuxId.Location = new System.Drawing.Point(47, 34);
            this.lblAuxId.Name = "lblAuxId";
            this.lblAuxId.Size = new System.Drawing.Size(171, 55);
            this.lblAuxId.TabIndex = 57;
            this.lblAuxId.Text = "Grupo:";
            // 
            // lblAuxOrientacion
            // 
            this.lblAuxOrientacion.AutoSize = true;
            this.lblAuxOrientacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAuxOrientacion.Location = new System.Drawing.Point(439, 34);
            this.lblAuxOrientacion.Name = "lblAuxOrientacion";
            this.lblAuxOrientacion.Size = new System.Drawing.Size(282, 55);
            this.lblAuxOrientacion.TabIndex = 59;
            this.lblAuxOrientacion.Text = "Orientacion:";
            // 
            // lblOrientacion
            // 
            this.lblOrientacion.AutoSize = true;
            this.lblOrientacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrientacion.Location = new System.Drawing.Point(741, 34);
            this.lblOrientacion.Name = "lblOrientacion";
            this.lblOrientacion.Size = new System.Drawing.Size(63, 55);
            this.lblOrientacion.TabIndex = 58;
            this.lblOrientacion.Text = "...";
            // 
            // lblAuxAnio
            // 
            this.lblAuxAnio.AutoSize = true;
            this.lblAuxAnio.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAuxAnio.Location = new System.Drawing.Point(1089, 34);
            this.lblAuxAnio.Name = "lblAuxAnio";
            this.lblAuxAnio.Size = new System.Drawing.Size(123, 55);
            this.lblAuxAnio.TabIndex = 61;
            this.lblAuxAnio.Text = "Año:";
            // 
            // lblAnio
            // 
            this.lblAnio.AutoSize = true;
            this.lblAnio.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAnio.Location = new System.Drawing.Point(1237, 34);
            this.lblAnio.Name = "lblAnio";
            this.lblAnio.Size = new System.Drawing.Size(63, 55);
            this.lblAnio.TabIndex = 60;
            this.lblAnio.Text = "...";
            // 
            // bckgMateriasDocentes
            // 
            this.bckgMateriasDocentes.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bckgMateriasDocentes_DoWork);
            this.bckgMateriasDocentes.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bckgMateriasDocentes_RunWorkerCompleted);
            // 
            // btnVolver
            // 
            this.btnVolver.BackColor = System.Drawing.Color.Transparent;
            this.btnVolver.FlatAppearance.BorderSize = 0;
            this.btnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolver.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.btnVolver.Image = global::AulaGO.Properties.Resources.VOLVER;
            this.btnVolver.Location = new System.Drawing.Point(1356, 798);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(233, 95);
            this.btnVolver.TabIndex = 62;
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // ConsultaGrupo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1601, 919);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.lblAuxAnio);
            this.Controls.Add(this.lblAnio);
            this.Controls.Add(this.lblAuxOrientacion);
            this.Controls.Add(this.lblOrientacion);
            this.Controls.Add(this.lblAuxId);
            this.Controls.Add(this.btnAsignarMateria);
            this.Controls.Add(this.btnDesasignarMateria);
            this.Controls.Add(this.btnAsignarDocenteAMateria);
            this.Controls.Add(this.btnDesasignarDocenteAMateria);
            this.Controls.Add(this.lblAlumnos);
            this.Controls.Add(this.DGVAlumnos);
            this.Controls.Add(this.lblMateriasDocentes);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.DGVMateriasDocentes);
            this.Controls.Add(this.btnAgregarAlumno);
            this.Controls.Add(this.btnEliminarAlumno);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ConsultaGrupo";
            this.Text = "Docentes";
            this.Load += new System.EventHandler(this.ConsultaGrupo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVMateriasDocentes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVAlumnos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEliminarAlumno;
        private System.Windows.Forms.Button btnAgregarAlumno;
        private System.Windows.Forms.Label lblId;
        private System.ComponentModel.BackgroundWorker bckgAlumnos;
        private System.Windows.Forms.DataGridView DGVMateriasDocentes;
        private System.Windows.Forms.Label lblMateriasDocentes;
        private System.Windows.Forms.Label lblAlumnos;
        private System.Windows.Forms.DataGridView DGVAlumnos;
        private System.Windows.Forms.Button btnAsignarDocenteAMateria;
        private System.Windows.Forms.Button btnDesasignarDocenteAMateria;
        private System.Windows.Forms.Button btnDesasignarMateria;
        private System.Windows.Forms.Button btnAsignarMateria;
        private System.Windows.Forms.Label lblAuxId;
        private System.Windows.Forms.Label lblAuxOrientacion;
        private System.Windows.Forms.Label lblOrientacion;
        private System.Windows.Forms.Label lblAuxAnio;
        private System.Windows.Forms.Label lblAnio;
        private System.ComponentModel.BackgroundWorker bckgMateriasDocentes;
        private System.Windows.Forms.Button btnVolver;
    }
}