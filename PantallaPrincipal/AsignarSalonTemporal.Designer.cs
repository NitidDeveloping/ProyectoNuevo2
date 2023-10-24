namespace Proyecto
{
    partial class AsignarSalonTemporal
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
            this.layout = new System.Windows.Forms.FlowLayoutPanel();
            this.plClase = new System.Windows.Forms.Panel();
            this.cbxClase = new System.Windows.Forms.ComboBox();
            this.lblSubClase = new System.Windows.Forms.Label();
            this.lblClase = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.bckgw = new System.ComponentModel.BackgroundWorker();
            this.layout.SuspendLayout();
            this.plClase.SuspendLayout();
            this.SuspendLayout();
            // 
            // layout
            // 
            this.layout.AutoScroll = true;
            this.layout.Controls.Add(this.plClase);
            this.layout.Location = new System.Drawing.Point(21, 21);
            this.layout.Name = "layout";
            this.layout.Size = new System.Drawing.Size(1202, 814);
            this.layout.TabIndex = 81;
            // 
            // plClase
            // 
            this.plClase.Controls.Add(this.cbxClase);
            this.plClase.Controls.Add(this.lblSubClase);
            this.plClase.Controls.Add(this.lblClase);
            this.plClase.Location = new System.Drawing.Point(7, 7);
            this.plClase.Margin = new System.Windows.Forms.Padding(7);
            this.plClase.Name = "plClase";
            this.plClase.Size = new System.Drawing.Size(384, 186);
            this.plClase.TabIndex = 77;
            // 
            // cbxClase
            // 
            this.cbxClase.DropDownHeight = 750;
            this.cbxClase.Font = new System.Drawing.Font("Verdana", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxClase.FormattingEnabled = true;
            this.cbxClase.IntegralHeight = false;
            this.cbxClase.Items.AddRange(new object[] {
            "Matutino",
            "Nocturno",
            "Vespertino"});
            this.cbxClase.Location = new System.Drawing.Point(22, 79);
            this.cbxClase.MaxDropDownItems = 25;
            this.cbxClase.Name = "cbxClase";
            this.cbxClase.Size = new System.Drawing.Size(339, 67);
            this.cbxClase.TabIndex = 74;
            this.cbxClase.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cbxClase_KeyUp);
            // 
            // lblSubClase
            // 
            this.lblSubClase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(8)))), ((int)(((byte)(55)))));
            this.lblSubClase.Location = new System.Drawing.Point(19, 151);
            this.lblSubClase.Name = "lblSubClase";
            this.lblSubClase.Size = new System.Drawing.Size(342, 5);
            this.lblSubClase.TabIndex = 58;
            // 
            // lblClase
            // 
            this.lblClase.AutoSize = true;
            this.lblClase.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClase.Location = new System.Drawing.Point(21, 8);
            this.lblClase.Name = "lblClase";
            this.lblClase.Size = new System.Drawing.Size(160, 55);
            this.lblClase.TabIndex = 55;
            this.lblClase.Text = "Salon:";
            // 
            // btnAceptar
            // 
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.Image = global::Proyecto.Properties.Resources.ACEOPTAR;
            this.btnAceptar.Location = new System.Drawing.Point(1362, 354);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(227, 90);
            this.btnAceptar.TabIndex = 33;
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Image = global::Proyecto.Properties.Resources.cancelar;
            this.btnCancelar.Location = new System.Drawing.Point(1362, 462);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(227, 90);
            this.btnCancelar.TabIndex = 34;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // bckgw
            // 
            this.bckgw.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bckgw_DoWork);
            this.bckgw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bckgw_RunWorkerCompleted);
            // 
            // AsignarSalonTemporal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1601, 919);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.layout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AsignarSalonTemporal";
            this.Text = "AgregarAlumnos";
            this.Load += new System.EventHandler(this.AsignarSalonTemporal_Load);
            this.layout.ResumeLayout(false);
            this.plClase.ResumeLayout(false);
            this.plClase.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.FlowLayoutPanel layout;
        private System.Windows.Forms.Panel plClase;
        private System.Windows.Forms.ComboBox cbxClase;
        private System.Windows.Forms.Label lblSubClase;
        private System.Windows.Forms.Label lblClase;
        private System.ComponentModel.BackgroundWorker bckgw;
    }
}