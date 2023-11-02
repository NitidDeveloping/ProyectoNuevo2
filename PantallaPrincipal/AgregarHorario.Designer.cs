namespace AulaGO
{
    partial class AgregarHorario
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
            this.plDocente = new System.Windows.Forms.Panel();
            this.txtDocente = new System.Windows.Forms.TextBox();
            this.lblSubDocente = new System.Windows.Forms.Label();
            this.lblDocente = new System.Windows.Forms.Label();
            this.plGrupo = new System.Windows.Forms.Panel();
            this.cbxGrupo = new System.Windows.Forms.ComboBox();
            this.lblSubGrupo = new System.Windows.Forms.Label();
            this.lblGrupo = new System.Windows.Forms.Label();
            this.plMateria = new System.Windows.Forms.Panel();
            this.cbxMateria = new System.Windows.Forms.ComboBox();
            this.lblSubMateria = new System.Windows.Forms.Label();
            this.lblMateria = new System.Windows.Forms.Label();
            this.plTurno = new System.Windows.Forms.Panel();
            this.cbxTurno = new System.Windows.Forms.ComboBox();
            this.lblSubTurno = new System.Windows.Forms.Label();
            this.lblTurno = new System.Windows.Forms.Label();
            this.layout = new System.Windows.Forms.FlowLayoutPanel();
            this.plDia = new System.Windows.Forms.Panel();
            this.cbxDia = new System.Windows.Forms.ComboBox();
            this.lblSubDia = new System.Windows.Forms.Label();
            this.lblDia = new System.Windows.Forms.Label();
            this.plHoras = new System.Windows.Forms.Panel();
            this.lblSubHoras = new System.Windows.Forms.Label();
            this.lblHoras = new System.Windows.Forms.Label();
            this.chlbHoras = new System.Windows.Forms.CheckedListBox();
            this.plClase = new System.Windows.Forms.Panel();
            this.cbxClase = new System.Windows.Forms.ComboBox();
            this.lblSubClase = new System.Windows.Forms.Label();
            this.lblClase = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.bckgw = new System.ComponentModel.BackgroundWorker();
            this.plDocente.SuspendLayout();
            this.plGrupo.SuspendLayout();
            this.plMateria.SuspendLayout();
            this.plTurno.SuspendLayout();
            this.layout.SuspendLayout();
            this.plDia.SuspendLayout();
            this.plHoras.SuspendLayout();
            this.plClase.SuspendLayout();
            this.SuspendLayout();
            // 
            // plDocente
            // 
            this.plDocente.Controls.Add(this.txtDocente);
            this.plDocente.Controls.Add(this.lblSubDocente);
            this.plDocente.Controls.Add(this.lblDocente);
            this.plDocente.Enabled = false;
            this.plDocente.Location = new System.Drawing.Point(7, 207);
            this.plDocente.Margin = new System.Windows.Forms.Padding(7);
            this.plDocente.Name = "plDocente";
            this.plDocente.Size = new System.Drawing.Size(384, 186);
            this.plDocente.TabIndex = 73;
            // 
            // txtDocente
            // 
            this.txtDocente.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDocente.Enabled = false;
            this.txtDocente.Font = new System.Drawing.Font("Verdana", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDocente.Location = new System.Drawing.Point(21, 82);
            this.txtDocente.MaxLength = 50;
            this.txtDocente.Name = "txtDocente";
            this.txtDocente.Size = new System.Drawing.Size(339, 59);
            this.txtDocente.TabIndex = 59;
            this.txtDocente.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblSubDocente
            // 
            this.lblSubDocente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(8)))), ((int)(((byte)(55)))));
            this.lblSubDocente.Location = new System.Drawing.Point(19, 151);
            this.lblSubDocente.Name = "lblSubDocente";
            this.lblSubDocente.Size = new System.Drawing.Size(342, 5);
            this.lblSubDocente.TabIndex = 58;
            // 
            // lblDocente
            // 
            this.lblDocente.AutoSize = true;
            this.lblDocente.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocente.Location = new System.Drawing.Point(21, 8);
            this.lblDocente.Name = "lblDocente";
            this.lblDocente.Size = new System.Drawing.Size(217, 55);
            this.lblDocente.TabIndex = 55;
            this.lblDocente.Text = "Docente:";
            // 
            // plGrupo
            // 
            this.plGrupo.Controls.Add(this.cbxGrupo);
            this.plGrupo.Controls.Add(this.lblSubGrupo);
            this.plGrupo.Controls.Add(this.lblGrupo);
            this.plGrupo.Location = new System.Drawing.Point(7, 7);
            this.plGrupo.Margin = new System.Windows.Forms.Padding(7);
            this.plGrupo.Name = "plGrupo";
            this.plGrupo.Size = new System.Drawing.Size(384, 186);
            this.plGrupo.TabIndex = 73;
            // 
            // cbxGrupo
            // 
            this.cbxGrupo.DropDownHeight = 750;
            this.cbxGrupo.Font = new System.Drawing.Font("Verdana", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxGrupo.FormattingEnabled = true;
            this.cbxGrupo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbxGrupo.IntegralHeight = false;
            this.cbxGrupo.Items.AddRange(new object[] {
            "Matutino",
            "Nocturno",
            "Vespertino"});
            this.cbxGrupo.Location = new System.Drawing.Point(22, 79);
            this.cbxGrupo.MaxDropDownItems = 20;
            this.cbxGrupo.Name = "cbxGrupo";
            this.cbxGrupo.Size = new System.Drawing.Size(339, 67);
            this.cbxGrupo.TabIndex = 74;
            this.cbxGrupo.SelectedIndexChanged += new System.EventHandler(this.cbxGrupo_SelectedIndexChanged);
            this.cbxGrupo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cbxGrupo_KeyUp);
            // 
            // lblSubGrupo
            // 
            this.lblSubGrupo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(8)))), ((int)(((byte)(55)))));
            this.lblSubGrupo.Location = new System.Drawing.Point(19, 151);
            this.lblSubGrupo.Name = "lblSubGrupo";
            this.lblSubGrupo.Size = new System.Drawing.Size(342, 5);
            this.lblSubGrupo.TabIndex = 58;
            // 
            // lblGrupo
            // 
            this.lblGrupo.AutoSize = true;
            this.lblGrupo.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrupo.Location = new System.Drawing.Point(21, 8);
            this.lblGrupo.Name = "lblGrupo";
            this.lblGrupo.Size = new System.Drawing.Size(190, 55);
            this.lblGrupo.TabIndex = 55;
            this.lblGrupo.Text = "Grupo*:";
            // 
            // plMateria
            // 
            this.plMateria.Controls.Add(this.cbxMateria);
            this.plMateria.Controls.Add(this.lblSubMateria);
            this.plMateria.Controls.Add(this.lblMateria);
            this.plMateria.Enabled = false;
            this.plMateria.Location = new System.Drawing.Point(803, 7);
            this.plMateria.Margin = new System.Windows.Forms.Padding(7);
            this.plMateria.Name = "plMateria";
            this.plMateria.Size = new System.Drawing.Size(384, 186);
            this.plMateria.TabIndex = 75;
            // 
            // cbxMateria
            // 
            this.cbxMateria.DropDownHeight = 750;
            this.cbxMateria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMateria.Font = new System.Drawing.Font("Verdana", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxMateria.FormattingEnabled = true;
            this.cbxMateria.IntegralHeight = false;
            this.cbxMateria.Items.AddRange(new object[] {
            "Matutino",
            "Nocturno",
            "Vespertino"});
            this.cbxMateria.Location = new System.Drawing.Point(21, 79);
            this.cbxMateria.MaxDropDownItems = 25;
            this.cbxMateria.Name = "cbxMateria";
            this.cbxMateria.Size = new System.Drawing.Size(339, 67);
            this.cbxMateria.TabIndex = 74;
            this.cbxMateria.SelectedIndexChanged += new System.EventHandler(this.cbxMateria_SelectedIndexChanged);
            // 
            // lblSubMateria
            // 
            this.lblSubMateria.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(8)))), ((int)(((byte)(55)))));
            this.lblSubMateria.Location = new System.Drawing.Point(19, 151);
            this.lblSubMateria.Name = "lblSubMateria";
            this.lblSubMateria.Size = new System.Drawing.Size(342, 5);
            this.lblSubMateria.TabIndex = 58;
            // 
            // lblMateria
            // 
            this.lblMateria.AutoSize = true;
            this.lblMateria.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMateria.Location = new System.Drawing.Point(21, 8);
            this.lblMateria.Name = "lblMateria";
            this.lblMateria.Size = new System.Drawing.Size(216, 55);
            this.lblMateria.TabIndex = 55;
            this.lblMateria.Text = "Materia*:";
            // 
            // plTurno
            // 
            this.plTurno.Controls.Add(this.cbxTurno);
            this.plTurno.Controls.Add(this.lblSubTurno);
            this.plTurno.Controls.Add(this.lblTurno);
            this.plTurno.Enabled = false;
            this.plTurno.Location = new System.Drawing.Point(405, 7);
            this.plTurno.Margin = new System.Windows.Forms.Padding(7);
            this.plTurno.Name = "plTurno";
            this.plTurno.Size = new System.Drawing.Size(384, 186);
            this.plTurno.TabIndex = 76;
            // 
            // cbxTurno
            // 
            this.cbxTurno.DropDownHeight = 750;
            this.cbxTurno.Enabled = false;
            this.cbxTurno.Font = new System.Drawing.Font("Verdana", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxTurno.FormattingEnabled = true;
            this.cbxTurno.IntegralHeight = false;
            this.cbxTurno.Items.AddRange(new object[] {
            "Matutino",
            "Nocturno",
            "Vespertino"});
            this.cbxTurno.Location = new System.Drawing.Point(22, 79);
            this.cbxTurno.MaxDropDownItems = 25;
            this.cbxTurno.Name = "cbxTurno";
            this.cbxTurno.Size = new System.Drawing.Size(339, 67);
            this.cbxTurno.TabIndex = 74;
            // 
            // lblSubTurno
            // 
            this.lblSubTurno.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(8)))), ((int)(((byte)(55)))));
            this.lblSubTurno.Location = new System.Drawing.Point(19, 151);
            this.lblSubTurno.Name = "lblSubTurno";
            this.lblSubTurno.Size = new System.Drawing.Size(342, 5);
            this.lblSubTurno.TabIndex = 58;
            // 
            // lblTurno
            // 
            this.lblTurno.AutoSize = true;
            this.lblTurno.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTurno.Location = new System.Drawing.Point(21, 8);
            this.lblTurno.Name = "lblTurno";
            this.lblTurno.Size = new System.Drawing.Size(163, 55);
            this.lblTurno.TabIndex = 55;
            this.lblTurno.Text = "Turno:";
            // 
            // layout
            // 
            this.layout.AutoScroll = true;
            this.layout.Controls.Add(this.plGrupo);
            this.layout.Controls.Add(this.plTurno);
            this.layout.Controls.Add(this.plMateria);
            this.layout.Controls.Add(this.plDocente);
            this.layout.Controls.Add(this.plDia);
            this.layout.Controls.Add(this.plHoras);
            this.layout.Controls.Add(this.plClase);
            this.layout.Location = new System.Drawing.Point(21, 21);
            this.layout.Name = "layout";
            this.layout.Size = new System.Drawing.Size(1202, 814);
            this.layout.TabIndex = 81;
            // 
            // plDia
            // 
            this.plDia.Controls.Add(this.cbxDia);
            this.plDia.Controls.Add(this.lblSubDia);
            this.plDia.Controls.Add(this.lblDia);
            this.plDia.Enabled = false;
            this.plDia.Location = new System.Drawing.Point(405, 207);
            this.plDia.Margin = new System.Windows.Forms.Padding(7);
            this.plDia.Name = "plDia";
            this.plDia.Size = new System.Drawing.Size(384, 186);
            this.plDia.TabIndex = 76;
            // 
            // cbxDia
            // 
            this.cbxDia.DropDownHeight = 750;
            this.cbxDia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDia.Font = new System.Drawing.Font("Verdana", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxDia.FormattingEnabled = true;
            this.cbxDia.IntegralHeight = false;
            this.cbxDia.Items.AddRange(new object[] {
            "Matutino",
            "Nocturno",
            "Vespertino"});
            this.cbxDia.Location = new System.Drawing.Point(22, 79);
            this.cbxDia.MaxDropDownItems = 25;
            this.cbxDia.Name = "cbxDia";
            this.cbxDia.Size = new System.Drawing.Size(339, 67);
            this.cbxDia.TabIndex = 74;
            this.cbxDia.SelectedIndexChanged += new System.EventHandler(this.cbxDia_SelectedIndexChanged);
            // 
            // lblSubDia
            // 
            this.lblSubDia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(8)))), ((int)(((byte)(55)))));
            this.lblSubDia.Location = new System.Drawing.Point(19, 151);
            this.lblSubDia.Name = "lblSubDia";
            this.lblSubDia.Size = new System.Drawing.Size(342, 5);
            this.lblSubDia.TabIndex = 58;
            // 
            // lblDia
            // 
            this.lblDia.AutoSize = true;
            this.lblDia.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDia.Location = new System.Drawing.Point(21, 8);
            this.lblDia.Name = "lblDia";
            this.lblDia.Size = new System.Drawing.Size(128, 55);
            this.lblDia.TabIndex = 55;
            this.lblDia.Text = "Dia*:";
            // 
            // plHoras
            // 
            this.plHoras.Controls.Add(this.lblSubHoras);
            this.plHoras.Controls.Add(this.lblHoras);
            this.plHoras.Controls.Add(this.chlbHoras);
            this.plHoras.Enabled = false;
            this.plHoras.Location = new System.Drawing.Point(803, 207);
            this.plHoras.Margin = new System.Windows.Forms.Padding(7);
            this.plHoras.Name = "plHoras";
            this.plHoras.Size = new System.Drawing.Size(384, 186);
            this.plHoras.TabIndex = 77;
            // 
            // lblSubHoras
            // 
            this.lblSubHoras.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(8)))), ((int)(((byte)(55)))));
            this.lblSubHoras.Location = new System.Drawing.Point(19, 165);
            this.lblSubHoras.Name = "lblSubHoras";
            this.lblSubHoras.Size = new System.Drawing.Size(342, 5);
            this.lblSubHoras.TabIndex = 58;
            // 
            // lblHoras
            // 
            this.lblHoras.AutoSize = true;
            this.lblHoras.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHoras.Location = new System.Drawing.Point(16, 12);
            this.lblHoras.Name = "lblHoras";
            this.lblHoras.Size = new System.Drawing.Size(134, 39);
            this.lblHoras.TabIndex = 55;
            this.lblHoras.Text = "Horas*:";
            // 
            // chlbHoras
            // 
            this.chlbHoras.CheckOnClick = true;
            this.chlbHoras.FormattingEnabled = true;
            this.chlbHoras.Location = new System.Drawing.Point(22, 54);
            this.chlbHoras.Name = "chlbHoras";
            this.chlbHoras.Size = new System.Drawing.Size(339, 109);
            this.chlbHoras.TabIndex = 75;
            this.chlbHoras.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chlbHoras_ItemCheck);
            // 
            // plClase
            // 
            this.plClase.Controls.Add(this.cbxClase);
            this.plClase.Controls.Add(this.lblSubClase);
            this.plClase.Controls.Add(this.lblClase);
            this.plClase.Enabled = false;
            this.plClase.Location = new System.Drawing.Point(7, 407);
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
            this.lblClase.Size = new System.Drawing.Size(179, 55);
            this.lblClase.TabIndex = 55;
            this.lblClase.Text = "Salon*:";
            // 
            // btnAceptar
            // 
            this.btnAceptar.FlatAppearance.BorderSize = 0;
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.Image = global::AulaGO.Properties.Resources.ACEOPTAR;
            this.btnAceptar.Location = new System.Drawing.Point(1305, 360);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(227, 90);
            this.btnAceptar.TabIndex = 33;
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Image = global::AulaGO.Properties.Resources.cancelar;
            this.btnCancelar.Location = new System.Drawing.Point(1305, 468);
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
            // AgregarHorario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1601, 919);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.layout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AgregarHorario";
            this.Text = "AgregarAlumnos";
            this.Load += new System.EventHandler(this.AgregarHorario_Load);
            this.plDocente.ResumeLayout(false);
            this.plDocente.PerformLayout();
            this.plGrupo.ResumeLayout(false);
            this.plGrupo.PerformLayout();
            this.plMateria.ResumeLayout(false);
            this.plMateria.PerformLayout();
            this.plTurno.ResumeLayout(false);
            this.plTurno.PerformLayout();
            this.layout.ResumeLayout(false);
            this.plDia.ResumeLayout(false);
            this.plDia.PerformLayout();
            this.plHoras.ResumeLayout(false);
            this.plHoras.PerformLayout();
            this.plClase.ResumeLayout(false);
            this.plClase.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Panel plDocente;
        private System.Windows.Forms.Label lblSubDocente;
        private System.Windows.Forms.Label lblDocente;
        private System.Windows.Forms.Panel plGrupo;
        private System.Windows.Forms.Label lblSubGrupo;
        private System.Windows.Forms.Label lblGrupo;
        private System.Windows.Forms.ComboBox cbxGrupo;
        private System.Windows.Forms.Panel plMateria;
        private System.Windows.Forms.ComboBox cbxMateria;
        private System.Windows.Forms.Label lblSubMateria;
        private System.Windows.Forms.Label lblMateria;
        private System.Windows.Forms.Panel plTurno;
        private System.Windows.Forms.ComboBox cbxTurno;
        private System.Windows.Forms.Label lblSubTurno;
        private System.Windows.Forms.Label lblTurno;
        private System.Windows.Forms.TextBox txtDocente;
        private System.Windows.Forms.FlowLayoutPanel layout;
        private System.Windows.Forms.Panel plDia;
        private System.Windows.Forms.ComboBox cbxDia;
        private System.Windows.Forms.Label lblSubDia;
        private System.Windows.Forms.Label lblDia;
        private System.Windows.Forms.CheckedListBox chlbHoras;
        private System.Windows.Forms.Panel plHoras;
        private System.Windows.Forms.Label lblSubHoras;
        private System.Windows.Forms.Label lblHoras;
        private System.Windows.Forms.Panel plClase;
        private System.Windows.Forms.ComboBox cbxClase;
        private System.Windows.Forms.Label lblSubClase;
        private System.Windows.Forms.Label lblClase;
        private System.ComponentModel.BackgroundWorker bckgw;
    }
}