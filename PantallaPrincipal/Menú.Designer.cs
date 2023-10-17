namespace Proyecto
{
    partial class Menú
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
            this.components = new System.ComponentModel.Container();
            this.plForms = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.plMapa = new System.Windows.Forms.Panel();
            this.lblBienvenido = new System.Windows.Forms.Label();
            this.lblPersona = new System.Windows.Forms.Label();
            this.plLateral = new System.Windows.Forms.Panel();
            this.plPersona = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnModPIN = new System.Windows.Forms.Button();
            this.plDatosSubMenu = new System.Windows.Forms.Panel();
            this.btnHoras = new System.Windows.Forms.Button();
            this.btnOrientacion = new System.Windows.Forms.Button();
            this.btnAnios = new System.Windows.Forms.Button();
            this.btnTurnos = new System.Windows.Forms.Button();
            this.btnMaterias = new System.Windows.Forms.Button();
            this.btnGrupos = new System.Windows.Forms.Button();
            this.btnLugares = new System.Windows.Forms.Button();
            this.btnHorarios = new System.Windows.Forms.Button();
            this.btnDatos = new System.Windows.Forms.Button();
            this.plABMSubMenu = new System.Windows.Forms.Panel();
            this.btnABMOp = new System.Windows.Forms.Button();
            this.btnABMDocentes = new System.Windows.Forms.Button();
            this.btnABMAlumnos = new System.Windows.Forms.Button();
            this.btnUsuarios = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pbMapa = new System.Windows.Forms.PictureBox();
            this.lblHora = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.cbxLugares = new System.Windows.Forms.ComboBox();
            this.btnClase = new System.Windows.Forms.Button();
            this.btnGrupo = new System.Windows.Forms.Button();
            this.plForms.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.plLateral.SuspendLayout();
            this.plPersona.SuspendLayout();
            this.panel5.SuspendLayout();
            this.plDatosSubMenu.SuspendLayout();
            this.plABMSubMenu.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMapa)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // plForms
            // 
            this.plForms.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plForms.Controls.Add(this.flowLayoutPanel1);
            this.plForms.Controls.Add(this.plMapa);
            this.plForms.Location = new System.Drawing.Point(319, 161);
            this.plForms.Name = "plForms";
            this.plForms.Size = new System.Drawing.Size(1601, 919);
            this.plForms.TabIndex = 3;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.cbxLugares);
            this.flowLayoutPanel1.Controls.Add(this.btnClase);
            this.flowLayoutPanel1.Controls.Add(this.btnGrupo);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(122, 36);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1253, 79);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // plMapa
            // 
            this.plMapa.Location = new System.Drawing.Point(64, 138);
            this.plMapa.Name = "plMapa";
            this.plMapa.Size = new System.Drawing.Size(1376, 743);
            this.plMapa.TabIndex = 0;
            // 
            // lblBienvenido
            // 
            this.lblBienvenido.AutoSize = true;
            this.lblBienvenido.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBienvenido.Location = new System.Drawing.Point(73, 775);
            this.lblBienvenido.Name = "lblBienvenido";
            this.lblBienvenido.Size = new System.Drawing.Size(168, 31);
            this.lblBienvenido.TabIndex = 4;
            this.lblBienvenido.Text = "Bienvenid@:";
            // 
            // lblPersona
            // 
            this.lblPersona.AutoSize = true;
            this.lblPersona.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPersona.Location = new System.Drawing.Point(57, 2);
            this.lblPersona.Name = "lblPersona";
            this.lblPersona.Size = new System.Drawing.Size(186, 31);
            this.lblPersona.TabIndex = 5;
            this.lblPersona.Text = "Nombre - Tipo";
            this.lblPersona.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // plLateral
            // 
            this.plLateral.BackColor = System.Drawing.Color.White;
            this.plLateral.Controls.Add(this.plPersona);
            this.plLateral.Controls.Add(this.panel5);
            this.plLateral.Controls.Add(this.plDatosSubMenu);
            this.plLateral.Controls.Add(this.btnDatos);
            this.plLateral.Controls.Add(this.plABMSubMenu);
            this.plLateral.Controls.Add(this.lblBienvenido);
            this.plLateral.Controls.Add(this.btnUsuarios);
            this.plLateral.Controls.Add(this.panel3);
            this.plLateral.Dock = System.Windows.Forms.DockStyle.Left;
            this.plLateral.Location = new System.Drawing.Point(0, 0);
            this.plLateral.Name = "plLateral";
            this.plLateral.Size = new System.Drawing.Size(319, 1080);
            this.plLateral.TabIndex = 6;
            // 
            // plPersona
            // 
            this.plPersona.Controls.Add(this.lblPersona);
            this.plPersona.Location = new System.Drawing.Point(0, 819);
            this.plPersona.Name = "plPersona";
            this.plPersona.Size = new System.Drawing.Size(319, 42);
            this.plPersona.TabIndex = 13;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnLogout);
            this.panel5.Controls.Add(this.btnModPIN);
            this.panel5.Location = new System.Drawing.Point(24, 870);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(270, 198);
            this.panel5.TabIndex = 12;
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.Transparent;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.Image = global::Proyecto.Properties.Resources.Logout;
            this.btnLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.Location = new System.Drawing.Point(13, 104);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(244, 91);
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Text = "CERRAR SESIÓN";
            this.btnLogout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.BtnLogout_Click);
            // 
            // btnModPIN
            // 
            this.btnModPIN.FlatAppearance.BorderSize = 0;
            this.btnModPIN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModPIN.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModPIN.Image = global::Proyecto.Properties.Resources.Pin_Pad;
            this.btnModPIN.Location = new System.Drawing.Point(13, 3);
            this.btnModPIN.Name = "btnModPIN";
            this.btnModPIN.Size = new System.Drawing.Size(244, 91);
            this.btnModPIN.TabIndex = 1;
            this.btnModPIN.Text = "MODIFICAR PIN";
            this.btnModPIN.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnModPIN.UseVisualStyleBackColor = true;
            this.btnModPIN.Click += new System.EventHandler(this.btnModPIN_Click);
            // 
            // plDatosSubMenu
            // 
            this.plDatosSubMenu.AutoScroll = true;
            this.plDatosSubMenu.BackColor = System.Drawing.Color.LightGray;
            this.plDatosSubMenu.Controls.Add(this.btnHoras);
            this.plDatosSubMenu.Controls.Add(this.btnOrientacion);
            this.plDatosSubMenu.Controls.Add(this.btnAnios);
            this.plDatosSubMenu.Controls.Add(this.btnTurnos);
            this.plDatosSubMenu.Controls.Add(this.btnMaterias);
            this.plDatosSubMenu.Controls.Add(this.btnGrupos);
            this.plDatosSubMenu.Controls.Add(this.btnLugares);
            this.plDatosSubMenu.Controls.Add(this.btnHorarios);
            this.plDatosSubMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.plDatosSubMenu.Location = new System.Drawing.Point(0, 527);
            this.plDatosSubMenu.Name = "plDatosSubMenu";
            this.plDatosSubMenu.Size = new System.Drawing.Size(319, 251);
            this.plDatosSubMenu.TabIndex = 11;
            this.plDatosSubMenu.Visible = false;
            // 
            // btnHoras
            // 
            this.btnHoras.BackColor = System.Drawing.Color.Silver;
            this.btnHoras.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnHoras.FlatAppearance.BorderSize = 0;
            this.btnHoras.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHoras.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHoras.ForeColor = System.Drawing.Color.Black;
            this.btnHoras.Image = global::Proyecto.Properties.Resources.Clock;
            this.btnHoras.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHoras.Location = new System.Drawing.Point(0, 505);
            this.btnHoras.Name = "btnHoras";
            this.btnHoras.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnHoras.Size = new System.Drawing.Size(302, 81);
            this.btnHoras.TabIndex = 9;
            this.btnHoras.Text = "     HORAS ";
            this.btnHoras.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHoras.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHoras.UseVisualStyleBackColor = false;
            this.btnHoras.Click += new System.EventHandler(this.btnHoras_Click);
            // 
            // btnOrientacion
            // 
            this.btnOrientacion.BackColor = System.Drawing.Color.Silver;
            this.btnOrientacion.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnOrientacion.FlatAppearance.BorderSize = 0;
            this.btnOrientacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOrientacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOrientacion.ForeColor = System.Drawing.Color.Black;
            this.btnOrientacion.Image = global::Proyecto.Properties.Resources.Diploma;
            this.btnOrientacion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOrientacion.Location = new System.Drawing.Point(0, 424);
            this.btnOrientacion.Name = "btnOrientacion";
            this.btnOrientacion.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnOrientacion.Size = new System.Drawing.Size(302, 81);
            this.btnOrientacion.TabIndex = 8;
            this.btnOrientacion.Text = "      ORIENTACIONES";
            this.btnOrientacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOrientacion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOrientacion.UseVisualStyleBackColor = false;
            this.btnOrientacion.Click += new System.EventHandler(this.btnOrientacion_Click);
            // 
            // btnAnios
            // 
            this.btnAnios.BackColor = System.Drawing.Color.Silver;
            this.btnAnios.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAnios.FlatAppearance.BorderSize = 0;
            this.btnAnios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnios.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnios.ForeColor = System.Drawing.Color.Black;
            this.btnAnios.Image = global::Proyecto.Properties.Resources.Plus_1_Year;
            this.btnAnios.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAnios.Location = new System.Drawing.Point(0, 343);
            this.btnAnios.Name = "btnAnios";
            this.btnAnios.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnAnios.Size = new System.Drawing.Size(302, 81);
            this.btnAnios.TabIndex = 7;
            this.btnAnios.Text = "      AÑOS";
            this.btnAnios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAnios.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAnios.UseVisualStyleBackColor = false;
            this.btnAnios.Click += new System.EventHandler(this.btnAnios_Click);
            // 
            // btnTurnos
            // 
            this.btnTurnos.BackColor = System.Drawing.Color.Silver;
            this.btnTurnos.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTurnos.FlatAppearance.BorderSize = 0;
            this.btnTurnos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTurnos.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTurnos.ForeColor = System.Drawing.Color.Black;
            this.btnTurnos.Image = global::Proyecto.Properties.Resources.Day_and_Night;
            this.btnTurnos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTurnos.Location = new System.Drawing.Point(0, 262);
            this.btnTurnos.Name = "btnTurnos";
            this.btnTurnos.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnTurnos.Size = new System.Drawing.Size(302, 81);
            this.btnTurnos.TabIndex = 4;
            this.btnTurnos.Text = "      TURNOS";
            this.btnTurnos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTurnos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTurnos.UseVisualStyleBackColor = false;
            this.btnTurnos.Click += new System.EventHandler(this.btnTurnos_Click);
            // 
            // btnMaterias
            // 
            this.btnMaterias.BackColor = System.Drawing.Color.Silver;
            this.btnMaterias.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMaterias.FlatAppearance.BorderSize = 0;
            this.btnMaterias.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaterias.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMaterias.ForeColor = System.Drawing.Color.Black;
            this.btnMaterias.Image = global::Proyecto.Properties.Resources.Book;
            this.btnMaterias.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMaterias.Location = new System.Drawing.Point(0, 199);
            this.btnMaterias.Name = "btnMaterias";
            this.btnMaterias.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnMaterias.Size = new System.Drawing.Size(302, 63);
            this.btnMaterias.TabIndex = 3;
            this.btnMaterias.Text = "      MATERIAS";
            this.btnMaterias.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMaterias.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMaterias.UseVisualStyleBackColor = false;
            this.btnMaterias.Click += new System.EventHandler(this.btnMaterias_Click);
            // 
            // btnGrupos
            // 
            this.btnGrupos.BackColor = System.Drawing.Color.Silver;
            this.btnGrupos.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnGrupos.FlatAppearance.BorderSize = 0;
            this.btnGrupos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGrupos.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGrupos.ForeColor = System.Drawing.Color.Black;
            this.btnGrupos.Image = global::Proyecto.Properties.Resources.Groups;
            this.btnGrupos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGrupos.Location = new System.Drawing.Point(0, 126);
            this.btnGrupos.Name = "btnGrupos";
            this.btnGrupos.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnGrupos.Size = new System.Drawing.Size(302, 73);
            this.btnGrupos.TabIndex = 2;
            this.btnGrupos.Text = "       GRUPOS";
            this.btnGrupos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGrupos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGrupos.UseVisualStyleBackColor = false;
            this.btnGrupos.Click += new System.EventHandler(this.btnGrupos_Click);
            // 
            // btnLugares
            // 
            this.btnLugares.BackColor = System.Drawing.Color.Silver;
            this.btnLugares.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnLugares.FlatAppearance.BorderSize = 0;
            this.btnLugares.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLugares.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLugares.ForeColor = System.Drawing.Color.Black;
            this.btnLugares.Image = global::Proyecto.Properties.Resources.Place_Marker;
            this.btnLugares.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLugares.Location = new System.Drawing.Point(0, 63);
            this.btnLugares.Name = "btnLugares";
            this.btnLugares.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnLugares.Size = new System.Drawing.Size(302, 63);
            this.btnLugares.TabIndex = 1;
            this.btnLugares.Text = "       LUGARES";
            this.btnLugares.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLugares.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLugares.UseVisualStyleBackColor = false;
            this.btnLugares.Click += new System.EventHandler(this.btnLugares_Click);
            // 
            // btnHorarios
            // 
            this.btnHorarios.BackColor = System.Drawing.Color.Silver;
            this.btnHorarios.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnHorarios.FlatAppearance.BorderSize = 0;
            this.btnHorarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHorarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHorarios.ForeColor = System.Drawing.Color.Black;
            this.btnHorarios.Image = global::Proyecto.Properties.Resources.Schedule;
            this.btnHorarios.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHorarios.Location = new System.Drawing.Point(0, 0);
            this.btnHorarios.Name = "btnHorarios";
            this.btnHorarios.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnHorarios.Size = new System.Drawing.Size(302, 63);
            this.btnHorarios.TabIndex = 0;
            this.btnHorarios.Text = "      HORARIOS";
            this.btnHorarios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHorarios.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHorarios.UseVisualStyleBackColor = false;
            this.btnHorarios.Click += new System.EventHandler(this.btnHorarios_Click);
            // 
            // btnDatos
            // 
            this.btnDatos.BackColor = System.Drawing.Color.Gainsboro;
            this.btnDatos.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDatos.FlatAppearance.BorderSize = 0;
            this.btnDatos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDatos.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDatos.ForeColor = System.Drawing.Color.Black;
            this.btnDatos.Location = new System.Drawing.Point(0, 452);
            this.btnDatos.Name = "btnDatos";
            this.btnDatos.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnDatos.Size = new System.Drawing.Size(319, 75);
            this.btnDatos.TabIndex = 10;
            this.btnDatos.Text = "DATOS";
            this.btnDatos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDatos.UseVisualStyleBackColor = false;
            this.btnDatos.Click += new System.EventHandler(this.btnDatos_Click);
            // 
            // plABMSubMenu
            // 
            this.plABMSubMenu.BackColor = System.Drawing.Color.LightGray;
            this.plABMSubMenu.Controls.Add(this.btnABMOp);
            this.plABMSubMenu.Controls.Add(this.btnABMDocentes);
            this.plABMSubMenu.Controls.Add(this.btnABMAlumnos);
            this.plABMSubMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.plABMSubMenu.Location = new System.Drawing.Point(0, 265);
            this.plABMSubMenu.Name = "plABMSubMenu";
            this.plABMSubMenu.Size = new System.Drawing.Size(319, 187);
            this.plABMSubMenu.TabIndex = 9;
            this.plABMSubMenu.Visible = false;
            // 
            // btnABMOp
            // 
            this.btnABMOp.BackColor = System.Drawing.Color.Silver;
            this.btnABMOp.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnABMOp.FlatAppearance.BorderSize = 0;
            this.btnABMOp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnABMOp.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnABMOp.ForeColor = System.Drawing.Color.Black;
            this.btnABMOp.Image = global::Proyecto.Properties.Resources.Settings;
            this.btnABMOp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnABMOp.Location = new System.Drawing.Point(0, 126);
            this.btnABMOp.Name = "btnABMOp";
            this.btnABMOp.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnABMOp.Size = new System.Drawing.Size(319, 63);
            this.btnABMOp.TabIndex = 2;
            this.btnABMOp.Text = "   FUNCIONARIOS";
            this.btnABMOp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnABMOp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnABMOp.UseVisualStyleBackColor = false;
            this.btnABMOp.Click += new System.EventHandler(this.btnABMOp_Click);
            // 
            // btnABMDocentes
            // 
            this.btnABMDocentes.BackColor = System.Drawing.Color.Silver;
            this.btnABMDocentes.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnABMDocentes.FlatAppearance.BorderSize = 0;
            this.btnABMDocentes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnABMDocentes.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnABMDocentes.ForeColor = System.Drawing.Color.Black;
            this.btnABMDocentes.Image = global::Proyecto.Properties.Resources.Teacher;
            this.btnABMDocentes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnABMDocentes.Location = new System.Drawing.Point(0, 63);
            this.btnABMDocentes.Name = "btnABMDocentes";
            this.btnABMDocentes.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnABMDocentes.Size = new System.Drawing.Size(319, 63);
            this.btnABMDocentes.TabIndex = 1;
            this.btnABMDocentes.Text = "      DOCENTES";
            this.btnABMDocentes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnABMDocentes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnABMDocentes.UseVisualStyleBackColor = false;
            this.btnABMDocentes.Click += new System.EventHandler(this.btnABMDocentes_Click);
            // 
            // btnABMAlumnos
            // 
            this.btnABMAlumnos.BackColor = System.Drawing.Color.Silver;
            this.btnABMAlumnos.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnABMAlumnos.FlatAppearance.BorderSize = 0;
            this.btnABMAlumnos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnABMAlumnos.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnABMAlumnos.ForeColor = System.Drawing.Color.Black;
            this.btnABMAlumnos.Image = global::Proyecto.Properties.Resources.Graduation_Cap;
            this.btnABMAlumnos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnABMAlumnos.Location = new System.Drawing.Point(0, 0);
            this.btnABMAlumnos.Name = "btnABMAlumnos";
            this.btnABMAlumnos.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnABMAlumnos.Size = new System.Drawing.Size(319, 63);
            this.btnABMAlumnos.TabIndex = 0;
            this.btnABMAlumnos.Text = "      ALUMNOS";
            this.btnABMAlumnos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnABMAlumnos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnABMAlumnos.UseVisualStyleBackColor = false;
            this.btnABMAlumnos.Click += new System.EventHandler(this.btnABMAlumnos_Click);
            // 
            // btnUsuarios
            // 
            this.btnUsuarios.BackColor = System.Drawing.Color.Gainsboro;
            this.btnUsuarios.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUsuarios.FlatAppearance.BorderSize = 0;
            this.btnUsuarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUsuarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUsuarios.ForeColor = System.Drawing.Color.Black;
            this.btnUsuarios.Location = new System.Drawing.Point(0, 190);
            this.btnUsuarios.Name = "btnUsuarios";
            this.btnUsuarios.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnUsuarios.Size = new System.Drawing.Size(319, 75);
            this.btnUsuarios.TabIndex = 8;
            this.btnUsuarios.Text = "USUARIOS";
            this.btnUsuarios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUsuarios.UseVisualStyleBackColor = false;
            this.btnUsuarios.Click += new System.EventHandler(this.btnAbm_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pbMapa);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(319, 190);
            this.panel3.TabIndex = 7;
            // 
            // pbMapa
            // 
            this.pbMapa.Image = global::Proyecto.Properties.Resources.ITS_500_322;
            this.pbMapa.Location = new System.Drawing.Point(15, 6);
            this.pbMapa.Name = "pbMapa";
            this.pbMapa.Size = new System.Drawing.Size(289, 178);
            this.pbMapa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbMapa.TabIndex = 0;
            this.pbMapa.TabStop = false;
            this.pbMapa.Click += new System.EventHandler(this.pbMapa_Click);
            // 
            // lblHora
            // 
            this.lblHora.AutoSize = true;
            this.lblHora.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHora.Location = new System.Drawing.Point(16, 15);
            this.lblHora.Name = "lblHora";
            this.lblHora.Size = new System.Drawing.Size(151, 39);
            this.lblHora.TabIndex = 5;
            this.lblHora.Text = "00:00:00";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.Location = new System.Drawing.Point(19, 65);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(80, 29);
            this.lblFecha.TabIndex = 7;
            this.lblFecha.Text = "Fecha";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblHora);
            this.panel2.Controls.Add(this.lblFecha);
            this.panel2.Location = new System.Drawing.Point(1100, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(473, 111);
            this.panel2.TabIndex = 8;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.lblTitulo);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(319, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1601, 161);
            this.panel4.TabIndex = 9;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(31, 45);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(341, 55);
            this.lblTitulo.TabIndex = 9;
            this.lblTitulo.Text = "Menú Principal";
            // 
            // cbxLugares
            // 
            this.cbxLugares.DropDownHeight = 750;
            this.cbxLugares.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLugares.Font = new System.Drawing.Font("Verdana", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxLugares.FormattingEnabled = true;
            this.cbxLugares.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbxLugares.IntegralHeight = false;
            this.cbxLugares.Location = new System.Drawing.Point(3, 3);
            this.cbxLugares.MaxDropDownItems = 25;
            this.cbxLugares.Name = "cbxLugares";
            this.cbxLugares.Size = new System.Drawing.Size(494, 67);
            this.cbxLugares.Sorted = true;
            this.cbxLugares.TabIndex = 75;
            // 
            // btnClase
            // 
            this.btnClase.BackColor = System.Drawing.Color.YellowGreen;
            this.btnClase.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClase.ForeColor = System.Drawing.Color.White;
            this.btnClase.Location = new System.Drawing.Point(503, 3);
            this.btnClase.Name = "btnClase";
            this.btnClase.Size = new System.Drawing.Size(227, 67);
            this.btnClase.TabIndex = 77;
            this.btnClase.Text = "MI CLASE";
            this.btnClase.UseVisualStyleBackColor = false;
            // 
            // btnGrupo
            // 
            this.btnGrupo.BackColor = System.Drawing.Color.YellowGreen;
            this.btnGrupo.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGrupo.ForeColor = System.Drawing.Color.White;
            this.btnGrupo.Location = new System.Drawing.Point(736, 3);
            this.btnGrupo.Name = "btnGrupo";
            this.btnGrupo.Size = new System.Drawing.Size(227, 67);
            this.btnGrupo.TabIndex = 78;
            this.btnGrupo.Text = "MI GRUPO";
            this.btnGrupo.UseVisualStyleBackColor = false;
            // 
            // Menú
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.plLateral);
            this.Controls.Add(this.plForms);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Menú";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menú";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Menú_Load);
            this.plForms.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.plLateral.ResumeLayout(false);
            this.plLateral.PerformLayout();
            this.plPersona.ResumeLayout(false);
            this.plPersona.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.plDatosSubMenu.ResumeLayout(false);
            this.plABMSubMenu.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbMapa)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label lblBienvenido;
        private System.Windows.Forms.Label lblHora;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.PictureBox pbMapa;
        private System.Windows.Forms.Button btnABMDocentes;
        private System.Windows.Forms.Button btnABMAlumnos;
        private System.Windows.Forms.Button btnGrupos;
        private System.Windows.Forms.Button btnLugares;
        private System.Windows.Forms.Button btnHorarios;
        private System.Windows.Forms.Button btnMaterias;
        public System.Windows.Forms.Button btnModPIN;
        private System.Windows.Forms.Panel plDatosSubMenu;
        public System.Windows.Forms.Button btnUsuarios;
        public System.Windows.Forms.Button btnDatos;
        public System.Windows.Forms.Label lblPersona;
        public System.Windows.Forms.Panel plABMSubMenu;
        public System.Windows.Forms.Button btnABMOp;
        public System.Windows.Forms.Panel plPersona;
        private System.Windows.Forms.Panel plLateral;
        public System.Windows.Forms.Panel plForms;
        public System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnTurnos;
        private System.Windows.Forms.Button btnAnios;
        private System.Windows.Forms.Button btnOrientacion;
        private System.Windows.Forms.Button btnHoras;
        private System.Windows.Forms.Panel plMapa;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ComboBox cbxLugares;
        private System.Windows.Forms.Button btnClase;
        private System.Windows.Forms.Button btnGrupo;
    }
}