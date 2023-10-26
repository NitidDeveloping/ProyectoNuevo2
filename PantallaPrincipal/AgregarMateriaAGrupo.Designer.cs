namespace Proyecto
{
    partial class AgregarMateriaAGrupo
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
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.lblMateria = new System.Windows.Forms.Label();
            this.lblSubMateria = new System.Windows.Forms.Label();
            this.cbxMateria = new System.Windows.Forms.ComboBox();
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
            // btnCancelar
            // 
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Image = global::Proyecto.Properties.Resources.cancelar;
            this.btnCancelar.Location = new System.Drawing.Point(1320, 462);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(233, 95);
            this.btnCancelar.TabIndex = 48;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Enabled = false;
            this.btnAceptar.FlatAppearance.BorderSize = 0;
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.Image = global::Proyecto.Properties.Resources.ACEOPTAR;
            this.btnAceptar.Location = new System.Drawing.Point(1320, 362);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(233, 94);
            this.btnAceptar.TabIndex = 49;
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // lblMateria
            // 
            this.lblMateria.AutoSize = true;
            this.lblMateria.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMateria.Location = new System.Drawing.Point(287, 341);
            this.lblMateria.Name = "lblMateria";
            this.lblMateria.Size = new System.Drawing.Size(197, 55);
            this.lblMateria.TabIndex = 63;
            this.lblMateria.Text = "Materia:";
            // 
            // lblSubMateria
            // 
            this.lblSubMateria.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(8)))), ((int)(((byte)(55)))));
            this.lblSubMateria.Location = new System.Drawing.Point(298, 469);
            this.lblSubMateria.Name = "lblSubMateria";
            this.lblSubMateria.Size = new System.Drawing.Size(734, 5);
            this.lblSubMateria.TabIndex = 64;
            // 
            // cbxMateria
            // 
            this.cbxMateria.DropDownHeight = 750;
            this.cbxMateria.Font = new System.Drawing.Font("Verdana", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxMateria.FormattingEnabled = true;
            this.cbxMateria.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbxMateria.IntegralHeight = false;
            this.cbxMateria.Items.AddRange(new object[] {
            "Matutino",
            "Nocturno",
            "Vespertino"});
            this.cbxMateria.Location = new System.Drawing.Point(297, 399);
            this.cbxMateria.MaxDropDownItems = 25;
            this.cbxMateria.Name = "cbxMateria";
            this.cbxMateria.Size = new System.Drawing.Size(737, 67);
            this.cbxMateria.TabIndex = 75;
            this.cbxMateria.SelectedIndexChanged += new System.EventHandler(this.cbxMateria_SelectedIndexChanged);
            // 
            // AgregarMateriaAGrupo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1601, 919);
            this.Controls.Add(this.cbxMateria);
            this.Controls.Add(this.lblMateria);
            this.Controls.Add(this.lblSubMateria);
            this.Controls.Add(this.lblAuxId);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnCancelar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AgregarMateriaAGrupo";
            this.Text = "Docentes";
            this.Load += new System.EventHandler(this.AgregarMateriaAGrupo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblId;
        private System.ComponentModel.BackgroundWorker bckgAlumnos;
        private System.Windows.Forms.Label lblAuxId;
        private System.ComponentModel.BackgroundWorker bckgMateriasDocentes;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label lblMateria;
        private System.Windows.Forms.Label lblSubMateria;
        private System.Windows.Forms.ComboBox cbxMateria;
    }
}