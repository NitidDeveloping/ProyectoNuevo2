using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static CustomControls;
using RadioButton = System.Windows.Forms.RadioButton;

namespace AulaGO
{
    public partial class Menú : Form
    {
        private readonly CustomControls customControls;

        public static CustomRadioButton rbPB = new CustomRadioButton
        {
            Text = "Planta Baja",
            Font = new Font("MADE INFINITY PERSONAL USE", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0),
            MinimumSize = new Size(170, 50),
            Location = new Point(10, 10),
            Checked = true
        };

        public static CustomRadioButton rbP1 = new CustomRadioButton
        {
            Text = "Piso 1",
            Location = new Point(200, 11),
            Font = new Font("MADE INFINITY PERSONAL USE", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0),
        };

        public static CustomRadioButton rbP2 = new CustomRadioButton
        {
            Text = "Piso 2",
            Location = new Point(330, 11),
            Font = new Font("MADE INFINITY PERSONAL USE", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0)
        };

        public RadioButton RbPlantaBaja => rbPB;

        public RadioButton RbPiso1 => rbP1;

        public RadioButton RbPiso2 => rbP2;

        public Menú()
        {
            InitializeComponent();
            customControls = new CustomControls(plLateral);

            rbPB.Tag = 0;
            rbP1.Tag = 1;
            rbP2.Tag = 2;

            rbPB.CheckedChanged += MapaRadioButton_CheckedChanged;
            rbP1.CheckedChanged += MapaRadioButton_CheckedChanged;
            rbP2.CheckedChanged += MapaRadioButton_CheckedChanged;

            plPisos.Controls.Add(rbPB);
            plPisos.Controls.Add(rbP1);
            plPisos.Controls.Add(rbP2);

        }

        private void Menú_Load(object sender, EventArgs e)
        {
            //Mostrar unos controles u otros segun el rol logueado
            #region
            switch (Sesion.LoggedRol)
            {
                case TipoRol.Operador:
                    btnABMOp.Visible = false;
                    plABMSubMenu.Size = new Size(319, 127);
                    break;

                case TipoRol.Alumno:
                    btnDatos.Visible = false;
                    btnUsuarios.Visible = false;
                    btnClase.Visible = true;
                    break;

                case TipoRol.Docente:
                    btnDatos.Visible = false;
                    btnUsuarios.Visible = false;
                    btnGrupo.Visible = true;
                    break;

                case TipoRol.Visitante:
                    btnDatos.Visible = false;
                    btnClase.Visible = false;
                    btnGrupo.Visible = false;
                    btnUsuarios.Visible = false;
                    btnModPIN.Visible = false;
                    break;
            }

            lblPersona.Text = Sesion.LoggedNombre;
            #endregion

            timer1.Start();

            //Inicia timer de cierre de sesion automatico
            Metodos metodos = new Metodos();
            metodos.InitializeTimer();

            cbxLugares.SelectedIndexChanged -= cbxLugares_SelectedIndexChanged;
            Mapa mapa = new Mapa();
            Metodos.SetMenuForm(this); //Almacenamos la instancia del formulario menú
            Metodos.OpenChildForm(mapa, plMapa);
            Negocio negocio = new Negocio();


            // Asignar el DataTable al ComboBox
            try
            {
                cbxLugares.DataSource = negocio.Listar(TipoReferencia.Lugar, null, null);
                cbxLugares.DisplayMember = "Nombre";
                cbxLugares.ValueMember = "Nombre";
                cbxLugares.SelectedIndex = -1;
                cbxLugares.SelectedIndexChanged += cbxLugares_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MsgBox excepcion = new MsgBox("error", ex.Message);
            }
        }

        //Timer del reloj
        #region
        private void Timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToLongTimeString();
            lblFecha.Text = DateTime.Now.ToLongDateString();
        }
        #endregion

        //Boton de cierre de sesion
        #region
        private void CerrarSesion()  //Creo el método para confirmar el cierre de sesión
        {
            MsgBox msg = new MsgBox("pregunta", "¿Desea cerrar sesión?"); //Hago la pregunta"
            msg.ShowDialog(); //Luego de asignar las funciones de cada botón, muestro el form con las modificaciones realizadas previamente

            if (msg.DialogResult == DialogResult.Yes)
            {
                Sesion sesion = new Sesion();
                sesion.LogOut();
                Close(); //Cierro el menú
            }
        }
        private void BtnLogout_Click(object sender, EventArgs e)
        {
            CerrarSesion(); //Invoco el método "CerrarSesion()" para que se muestre al apretar el botón de cerrar sesión
        }
        #endregion

        //Boton para modificar pin
        #region
        private void btnModPIN_Click(object sender, EventArgs e)
        {
            PIN pin = new PIN();
            pin.ShowDialog();
        }
        #endregion

        //Controles de los menus desplegables para el panel lateral de usuarios y de datos
        #region
        private void btnAbm_Click(object sender, EventArgs e)
        {
            ShowSubMenu(plABMSubMenu);
            customControls.ActivateButton(sender);
        }
        private void btnDatos_Click(object sender, EventArgs e)
        {
            ShowSubMenu(plDatosSubMenu);
            customControls.ActivateButton(sender);
        }
        private void HideSubMenu()
        {
            if (plABMSubMenu.Visible)
            {
                plABMSubMenu.Visible = false;
            }

            if (plDatosSubMenu.Visible)
            {
                plDatosSubMenu.Visible = false;
            }
        }
        private void ShowSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                HideSubMenu();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }
        #endregion

        //Centrar lblPersona en el panel
        #region
        private void CenterLabelInPanel() //Centrar el lblPersona en el panel sin importar el nombre o tipo de persona
        {

            int labelWidth = lblPersona.PreferredWidth; //Tomamos el ancho del lbl
            int labelHeight = lblPersona.PreferredHeight; //Su altura
            int panelWidth = plPersona.ClientSize.Width; //Tomamos el ancho del panel
            int panelHeight = plPersona.ClientSize.Height; //La altura del panel

            int x = (panelWidth - labelWidth) / 2;
            int y = (panelHeight - labelHeight) / 2;

            lblPersona.Location = new Point(x, y);
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            CenterLabelInPanel();
        }
        #endregion

        //Botones para abrir listas
        #region
        private void AbrirLista(TipoReferencia referencia)
        {
            string titulo;

            switch (referencia)
            {
                case TipoReferencia.Alumno:
                    titulo = "Alumnos";
                    break;

                case TipoReferencia.Anio:
                    titulo = "Años";
                    break;

                case TipoReferencia.Docente:
                    titulo = "Docentes";
                    break;

                case TipoReferencia.Funcionario:
                    titulo = "Funcionarios";
                    break;

                case TipoReferencia.Grupo:
                    titulo = "Grupos";
                    break;

                case TipoReferencia.Hora:
                    titulo = "Horas";
                    break;

                case TipoReferencia.Horario:
                    titulo = "Horario";
                    break;

                case TipoReferencia.Lugar:
                    titulo = "Lugares";
                    break;

                case TipoReferencia.Materia:
                    titulo = "Materias";
                    break;

                case TipoReferencia.Orientacion:
                    titulo = "Orientaciones";
                    break;

                case TipoReferencia.Turno:
                    titulo = "Turnos";
                    break;

                default:
                    throw new ArgumentException("Error, no se ha implementado esa referencia aún, capa presentaciín, Menu.AbrirLista()");
            }
            lblTitulo.Text = "Gestionar " + titulo;
            Sesion sesion = new Sesion();
            sesion.SetReferenciaActual(referencia);
            Lista lista = new Lista();//Almacenamos la instancia del formulario menú
            Metodos.OpenChildForm(lista, plForms);
        }
        private void btnABMAlumnos_Click(object sender, EventArgs e)
        {
            AbrirLista(TipoReferencia.Alumno);

        }
        private void btnABMDocentes_Click(object sender, EventArgs e)
        {
            AbrirLista(TipoReferencia.Docente);
        }
        private void btnABMOp_Click(object sender, EventArgs e)
        {
            AbrirLista(TipoReferencia.Funcionario);
        }
        private void btnHorarios_Click(object sender, EventArgs e)
        {
            AbrirLista(TipoReferencia.Horario);
        }
        private void btnLugares_Click(object sender, EventArgs e)
        {
            AbrirLista(TipoReferencia.Lugar);
        }
        private void btnGrupos_Click(object sender, EventArgs e)
        {
            AbrirLista(TipoReferencia.Grupo);
        }
        private void btnMaterias_Click(object sender, EventArgs e)
        {
            AbrirLista(TipoReferencia.Materia);
        }
        private void btnTurnos_Click(object sender, EventArgs e)
        {
            AbrirLista(TipoReferencia.Turno);
        }
        private void btnAnios_Click_1(object sender, EventArgs e)
        {
            AbrirLista(TipoReferencia.Anio);
        }
        private void btnOrientacion_Click(object sender, EventArgs e)
        {
            AbrirLista(TipoReferencia.Orientacion);
        }
        private void btnHoras_Click(object sender, EventArgs e)
        {
            AbrirLista(TipoReferencia.Hora);
        }
        #endregion

        //Cosas para el mapa
        #region
        private void pbMapa_Click(object sender, EventArgs e)
        {
            Metodos metodos = new Metodos();
            metodos.ResetTimerCierreSesion();
            cbxLugares.SelectedIndexChanged -= cbxLugares_SelectedIndexChanged;
            Mapa mapa = new Mapa();
            Metodos.OpenChildForm(mapa, plMapa);
            Negocio negocio = new Negocio();
            lblTitulo.Text = "Mapa";
            rbPB.Checked = true;

            // Asignar el DataTable al ComboBox
            cbxLugares.DataSource = negocio.Listar(TipoReferencia.Lugar, null, null);
            cbxLugares.DisplayMember = "Nombre";
            cbxLugares.ValueMember = "Nombre";
            cbxLugares.SelectedIndex = -1;
            cbxLugares.SelectedIndexChanged += cbxLugares_SelectedIndexChanged;

        }
        private void cbxLugares_SelectedIndexChanged(object sender, EventArgs e)
        {
            Metodos metodos = new Metodos();
            metodos.ResetTimerCierreSesion();

            if (cbxLugares.SelectedItem is DataRowView row)
            {
                if (row.Row.Table.Columns.Contains("Coordenada_X") && row.Row.Table.Columns.Contains("Coordenada_Y") && row.Row.Table.Columns.Contains("Piso"))
                {
                    int coordenadaX = Convert.ToInt32(row["Coordenada_X"]);
                    int coordenadaY = Convert.ToInt32(row["Coordenada_Y"]);
                    int piso = Convert.ToInt32(row["Piso"]);
                    Mapa.CurrentMapa.SetNodoFinal(coordenadaX, coordenadaY, piso);
                    Mapa.CurrentMapa.FindPath();
                }
            }
            else
            {
                Mapa.CurrentMapa.ClearPoints();
            }
        }
        private void MapaRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            Metodos metodos = new Metodos();
            metodos.ResetTimerCierreSesion();

            if (sender is CustomRadioButton radioButton)
            {
                if (radioButton.Checked)
                {
                    int mapaSeleccionado = 0;
                    Mapa.CurrentMapa.startNode.Reset();

                    if (radioButton.Text == "Piso 1")
                    {
                        mapaSeleccionado = 1;
                        Mapa.CurrentMapa.startNode.Reset();
                    }
                    else if (radioButton.Text == "Piso 2")
                    {
                        mapaSeleccionado = 2;
                        Mapa.CurrentMapa.startNode.Reset();
                    }
                    Mapa.CurrentMapa.CambiarMapa(mapaSeleccionado);
                }
            }
        }
        private void btnClase_Click(object sender, EventArgs e)
        {
            Metodos metodos = new Metodos();
            metodos.ResetTimerCierreSesion();
            Negocio negocio = new Negocio();
            List<string> gruposDelAlumno = negocio.ObtenerGruposDelAlumno(Sesion.LoggedCi);

            if (gruposDelAlumno.Count == 1)
            {
                // El alumno pertenece a un solo grupo, muestra la ubicación de ese grupo
                UbicacionClase ubicacionClase = negocio.ObtenerUbicacion(Sesion.LoggedCi, Sesion.LoggedRol, null);
                if (ubicacionClase.Salon != null)
                {
                    MsgBox msg = new MsgBox("aviso", $"Grupo: {ubicacionClase.Grupo}\nSalón: {ubicacionClase.Salon}\nMateria: {ubicacionClase.Materia}");
                    msg.ShowDialog();

                    int coordX = ubicacionClase.CoordenadaX;
                    int coordY = ubicacionClase.CoordenadaY;
                    int piso = ubicacionClase.Piso;

                    Mapa.CurrentMapa.SetNodoFinal(coordX, coordY, piso);
                    Mapa.CurrentMapa.FindPath();
                }
                else
                {
                    MsgBox msg = new MsgBox("aviso", "No tienes clases en el horario actual.");
                    msg.ShowDialog();
                }
            }
            else if (gruposDelAlumno.Count > 1)
            {
                // El alumno pertenece a varios grupos, muestra el MsgBox con los RadioButton
                MsgBox msg = new MsgBox("aviso", "Selecciona un grupo:");
                msg.rbGrupo1.Visible = true;
                msg.rbGrupo2.Visible = true;
                msg.rbGrupo1.Text = gruposDelAlumno[0];
                msg.rbGrupo2.Text = gruposDelAlumno[1];
                msg.ShowDialog();

                // Obtener el grupo seleccionado
                string grupoSeleccionado = string.Empty;
                if (msg.rbGrupo1.Checked)
                {
                    grupoSeleccionado = msg.rbGrupo1.Text;
                }
                else if (msg.rbGrupo2.Checked)
                {
                    grupoSeleccionado = msg.rbGrupo2.Text;
                }

                UbicacionClase ubicacionClase = negocio.ObtenerUbicacion(Sesion.LoggedCi, Sesion.LoggedRol, grupoSeleccionado);

                if (ubicacionClase.Salon != null)
                {
                    ubicacionClase.Grupo = grupoSeleccionado;

                    MsgBox msgb = new MsgBox("aviso", $"Grupo: {ubicacionClase.Grupo}\nSalón: {ubicacionClase.Salon}\nMateria: {ubicacionClase.Materia}");
                    msgb.ShowDialog();

                    int coordX = ubicacionClase.CoordenadaX;
                    int coordY = ubicacionClase.CoordenadaY;
                    int piso = ubicacionClase.Piso;

                    Mapa.CurrentMapa.SetNodoFinal(coordX, coordY, piso);
                    Mapa.CurrentMapa.FindPath();
                }
                else
                {
                    MsgBox msgb = new MsgBox("aviso", "No tienes clases en el horario actual.");
                    msgb.ShowDialog();
                }
            }
            else
            {
                // El alumno no pertenece a ningún grupo
                MsgBox msg = new MsgBox("error", "No estás en ningún grupo.");
                msg.ShowDialog();
            }
        }
        private void btnGrupo_Click(object sender, EventArgs e)
        {
            Metodos metodos = new Metodos();
            metodos.ResetTimerCierreSesion();
            Negocio negocio = new Negocio();
            UbicacionClase ubicacionGrupo = negocio.ObtenerUbicacion(Sesion.LoggedCi, Sesion.LoggedRol, null);

            if (ubicacionGrupo.Salon != null)
            {
                MsgBox msg = new MsgBox("aviso", $"Grupo: {ubicacionGrupo.Grupo}\nSalón: {ubicacionGrupo.Salon}\nMateria: {ubicacionGrupo.Materia}");
                msg.ShowDialog();

                int coordX = ubicacionGrupo.CoordenadaX;
                int coordY = ubicacionGrupo.CoordenadaY;
                int piso = ubicacionGrupo.Piso;

                Mapa.CurrentMapa.SetNodoFinal(coordX, coordY, piso);
                Mapa.CurrentMapa.FindPath();
            }
            else
            {
                MsgBox msg = new MsgBox("aviso", $"No tienes clases a dictar en el horario actual.");
                msg.ShowDialog();
            }
        }
        #endregion

        //Boton ayuda
        #region
        private void btnAyuda_Click(object sender, EventArgs e)
        {
            Metodos metodos = new Metodos();
            metodos.ResetTimerCierreSesion(); // Reinicia el timer de cierre de sesion

            //Abre el manual
            ManualDeUsuario manual = new ManualDeUsuario();
            manual.ShowDialog();

            
        }

        #endregion
    }
}