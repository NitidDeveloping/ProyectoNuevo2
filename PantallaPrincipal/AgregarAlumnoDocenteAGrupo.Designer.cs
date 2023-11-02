namespace AulaGO
{
    partial class AgregarAlumnoDocenteAGrupo
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
            this.lblId = new System.Windows.Forms.Label();
            this.lblAuxId = new System.Windows.Forms.Label();
            this.lblCI = new System.Windows.Forms.Label();
            this.lblSubCI = new System.Windows.Forms.Label();
            this.txtCI = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblSubNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblApellido = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.lblAuxMateria = new System.Windows.Forms.Label();
            this.lblMateria = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
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
            // lblCI
            // 
            this.lblCI.AutoSize = true;
            this.lblCI.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCI.Location = new System.Drawing.Point(484, 229);
            this.lblCI.Name = "lblCI";
            this.lblCI.Size = new System.Drawing.Size(85, 55);
            this.lblCI.TabIndex = 55;
            this.lblCI.Text = "CI:";
            // 
            // lblSubCI
            // 
            this.lblSubCI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(8)))), ((int)(((byte)(55)))));
            this.lblSubCI.Location = new System.Drawing.Point(485, 351);
            this.lblSubCI.Name = "lblSubCI";
            this.lblSubCI.Size = new System.Drawing.Size(406, 5);
            this.lblSubCI.TabIndex = 58;
            // 
            // txtCI
            // 
            this.txtCI.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCI.Font = new System.Drawing.Font("Verdana", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCI.Location = new System.Drawing.Point(483, 287);
            this.txtCI.MaxLength = 8;
            this.txtCI.Name = "txtCI";
            this.txtCI.Size = new System.Drawing.Size(408, 59);
            this.txtCI.TabIndex = 59;
            this.txtCI.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCI.Enter += new System.EventHandler(this.txtCI_Enter);
            this.txtCI.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCI_KeyPress);
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.Location = new System.Drawing.Point(224, 450);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(209, 55);
            this.lblNombre.TabIndex = 63;
            this.lblNombre.Text = "Nombre:";
            // 
            // lblSubNombre
            // 
            this.lblSubNombre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(8)))), ((int)(((byte)(55)))));
            this.lblSubNombre.Location = new System.Drawing.Point(224, 571);
            this.lblSubNombre.Name = "lblSubNombre";
            this.lblSubNombre.Size = new System.Drawing.Size(371, 5);
            this.lblSubNombre.TabIndex = 64;
            // 
            // txtNombre
            // 
            this.txtNombre.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNombre.Enabled = false;
            this.txtNombre.Font = new System.Drawing.Font("Verdana", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.Location = new System.Drawing.Point(222, 508);
            this.txtNombre.MaxLength = 8;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(373, 59);
            this.txtNombre.TabIndex = 65;
            this.txtNombre.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblApellido
            // 
            this.lblApellido.AutoSize = true;
            this.lblApellido.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApellido.Location = new System.Drawing.Point(790, 450);
            this.lblApellido.Name = "lblApellido";
            this.lblApellido.Size = new System.Drawing.Size(207, 55);
            this.lblApellido.TabIndex = 66;
            this.lblApellido.Text = "Apellido:";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(8)))), ((int)(((byte)(55)))));
            this.label4.Location = new System.Drawing.Point(790, 571);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(348, 5);
            this.label4.TabIndex = 67;
            // 
            // txtApellido
            // 
            this.txtApellido.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtApellido.Enabled = false;
            this.txtApellido.Font = new System.Drawing.Font("Verdana", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtApellido.Location = new System.Drawing.Point(789, 508);
            this.txtApellido.MaxLength = 8;
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Size = new System.Drawing.Size(349, 59);
            this.txtApellido.TabIndex = 68;
            this.txtApellido.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblAuxMateria
            // 
            this.lblAuxMateria.AutoSize = true;
            this.lblAuxMateria.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAuxMateria.Location = new System.Drawing.Point(47, 110);
            this.lblAuxMateria.Name = "lblAuxMateria";
            this.lblAuxMateria.Size = new System.Drawing.Size(197, 55);
            this.lblAuxMateria.TabIndex = 70;
            this.lblAuxMateria.Text = "Materia:";
            this.lblAuxMateria.Visible = false;
            // 
            // lblMateria
            // 
            this.lblMateria.AutoSize = true;
            this.lblMateria.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMateria.Location = new System.Drawing.Point(250, 110);
            this.lblMateria.Name = "lblMateria";
            this.lblMateria.Size = new System.Drawing.Size(196, 55);
            this.lblMateria.TabIndex = 69;
            this.lblMateria.Text = "Nombre";
            this.lblMateria.Visible = false;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Enabled = false;
            this.btnAceptar.FlatAppearance.BorderSize = 0;
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.Image = global::AulaGO.Properties.Resources.ACEOPTAR;
            this.btnAceptar.Location = new System.Drawing.Point(1287, 362);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(233, 94);
            this.btnAceptar.TabIndex = 72;
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Image = global::AulaGO.Properties.Resources.cancelar;
            this.btnCancelar.Location = new System.Drawing.Point(1287, 462);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(233, 95);
            this.btnCancelar.TabIndex = 71;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // AgregarAlumnoDocenteAGrupo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1601, 919);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.lblAuxMateria);
            this.Controls.Add(this.lblMateria);
            this.Controls.Add(this.lblApellido);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtApellido);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.lblSubNombre);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lblCI);
            this.Controls.Add(this.lblSubCI);
            this.Controls.Add(this.txtCI);
            this.Controls.Add(this.lblAuxId);
            this.Controls.Add(this.lblId);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AgregarAlumnoDocenteAGrupo";
            this.Text = "Docentes";
            this.Load += new System.EventHandler(this.AgregarAlumnoDocenteAGrupo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblAuxId;
        private System.Windows.Forms.Label lblCI;
        private System.Windows.Forms.Label lblSubCI;
        private System.Windows.Forms.TextBox txtCI;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblSubNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblApellido;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label lblAuxMateria;
        private System.Windows.Forms.Label lblMateria;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
    }
}