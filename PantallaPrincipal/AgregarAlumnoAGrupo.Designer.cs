﻿namespace Proyecto
{
    partial class AgregarAlumnoAGrupo
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
            this.bckgAlumnos = new System.ComponentModel.BackgroundWorker();
            this.lblAuxId = new System.Windows.Forms.Label();
            this.bckgMateriasDocentes = new System.ComponentModel.BackgroundWorker();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.lblCI = new System.Windows.Forms.Label();
            this.lblSubCI = new System.Windows.Forms.Label();
            this.txtCI = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblSubNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblApellido = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtApellido = new System.Windows.Forms.TextBox();
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
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(0, 0);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(75, 23);
            this.btnVolver.TabIndex = 69;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(1320, 371);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(233, 45);
            this.btnCancelar.TabIndex = 48;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Enabled = false;
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.Location = new System.Drawing.Point(1320, 286);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(233, 41);
            this.btnAceptar.TabIndex = 49;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
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
            this.lblSubCI.Location = new System.Drawing.Point(484, 344);
            this.lblSubCI.Name = "lblSubCI";
            this.lblSubCI.Size = new System.Drawing.Size(406, 10);
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
            this.txtCI.Leave += new System.EventHandler(this.txtCI_Leave);
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
            this.lblSubNombre.Location = new System.Drawing.Point(224, 565);
            this.lblSubNombre.Name = "lblSubNombre";
            this.lblSubNombre.Size = new System.Drawing.Size(371, 10);
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
            this.label4.Location = new System.Drawing.Point(790, 565);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(348, 10);
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
            // AgregarAlumnoAGrupo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1601, 919);
            this.Controls.Add(this.lblApellido);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtApellido);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.lblSubNombre);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lblCI);
            this.Controls.Add(this.lblSubCI);
            this.Controls.Add(this.txtCI);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.lblAuxId);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnCancelar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AgregarAlumnoAGrupo";
            this.Text = "Docentes";
            this.Load += new System.EventHandler(this.ConsultaGrupo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblId;
        private System.ComponentModel.BackgroundWorker bckgAlumnos;
        private System.Windows.Forms.Label lblAuxId;
        private System.ComponentModel.BackgroundWorker bckgMateriasDocentes;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label lblCI;
        private System.Windows.Forms.Label lblSubCI;
        private System.Windows.Forms.TextBox txtCI;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblSubNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblApellido;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtApellido;
    }
}