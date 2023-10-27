using CapaEntidades;
using CapaNegocio;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static CustomControls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Proyecto
{
    public partial class Menú : Form
    {
        private readonly CustomControls customControls;
        public Menú()
        {
            InitializeComponent();
            customControls = new CustomControls(plLateral);
            CustomRadioButton rbPB = new CustomRadioButton
            {
                Text = "Planta Baja",
                Font = new Font("MADE INFINITY PERSONAL USE", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0),
                MinimumSize = new Size(170, 50),
                Location = new Point(10, 10)
            };


            CustomRadioButton rbP1 = new CustomRadioButton
            {
                Text = "Piso 1",
                Location = new Point(200, 11),
                Font = new Font("MADE INFINITY PERSONAL USE", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0),
            };

            CustomRadioButton rbP2 = new CustomRadioButton
            {
                Text = "Piso 2",
                Location = new Point(330, 11),
                Font = new Font("MADE INFINITY PERSONAL USE", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0)
            };

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
                    this.btnABMOp.Visible = false;
                    break;

                case TipoRol.Alumno:
                    this.btnDatos.Visible = false;
                    this.btnUsuarios.Visible = false;
                    this.btnClase.Visible = false;
                    break;

                case TipoRol.Docente:
                    this.btnDatos.Visible = false;
                    this.btnUsuarios.Visible = false;
                    this.btnGrupo.Visible = false;
                    break;

                case TipoRol.Visitante:
                    this.btnDatos.Visible = false;
                    this.btnClase.Visible = false;
                    this.btnGrupo.Visible = false;
                    this.btnUsuarios.Visible = false;
                    break;
            }

            lblPersona.Text = Sesion.LoggedNombre;
            #endregion

            timer1.Start();

            //Inicia timer de cierre de sesion automatico
            if (Sesion.LoggedRol == TipoRol.Alumno || Sesion.LoggedRol == TipoRol.Docente)
            {
                timerCierreSesion.Start();
            }

            cbxLugares.SelectedIndexChanged -= cbxLugares_SelectedIndexChanged;
            Mapa mapa = new Mapa();
            Metodos.SetMenuForm(this); //Almacenamos la instancia del formulario menú
            Metodos.OpenChildForm(mapa, plMapa);
            Negocio negocio = new Negocio();


            // Asignar el DataTable al ComboBox
            cbxLugares.DataSource = negocio.Listar(TipoReferencia.Lugar, null, null);
            cbxLugares.DisplayMember = "Nombre";
            cbxLugares.ValueMember = "Nombre";
            cbxLugares.SelectedIndex = -1;
            cbxLugares.SelectedIndexChanged += cbxLugares_SelectedIndexChanged;

            switch (Sesion.LoggedRol)
            {
                case TipoRol.Alumno:
                    btnClase.Visible = true;
                    break;

                case TipoRol.Docente:
                    btnGrupo.Visible = true;
                    break;
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToLongTimeString();
            lblFecha.Text = DateTime.Now.ToLongDateString();
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

        private void CerrarSesion()  //Creo el método para confirmar el cierre de sesión
        {
            MsgBox msg = new MsgBox("pregunta", "¿Desea cerrar sesión?"); //Hago la pregunta"
            msg.ShowDialog(); //Luego de asignar las funciones de cada botón, muestro el form con las modificaciones realizadas previamente

            if (msg.DialogResult == DialogResult.Yes)
            {
                this.Close(); //Cierro el menú
            }
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            CerrarSesion(); //Invoco el método "CerrarSesion()" para que se muestre al apretar el botón de cerrar sesión
        }

        private void btnModPIN_Click(object sender, EventArgs e)
        {
            PIN pin = new PIN();
            pin.ShowDialog();
        }

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


        //Botones para abrir listas
        #region
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
                    throw new ArgumentException("Error, no se ha implementado esa referencia aun, capa presentacion, Menu.AbrirLita()");
            }
            lblTitulo.Text = "Gestionar " + titulo;
            Sesion sesion = new Sesion();
            sesion.SetReferenciaActual(referencia);
            Lista lista = new Lista();//Almacenamos la instancia del formulario menú
            Metodos.OpenChildForm(lista, plForms);
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


        private void pbMapa_Click(object sender, EventArgs e)
        {
            cbxLugares.SelectedIndexChanged -= cbxLugares_SelectedIndexChanged;
            Mapa mapa = new Mapa();
            Metodos.OpenChildForm(mapa, plMapa);
            Negocio negocio = new Negocio();
            lblTitulo.Text = "Mapa";

            // Asignar el DataTable al ComboBox
            cbxLugares.DataSource = negocio.Listar(TipoReferencia.Lugar, null, null);
            cbxLugares.DisplayMember = "Nombre";
            cbxLugares.ValueMember = "Nombre";
            cbxLugares.SelectedIndex = -1;
            cbxLugares.SelectedIndexChanged += cbxLugares_SelectedIndexChanged;

        }

        private void cbxLugares_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetTimerCierreSesion(); //Reinicia el timer de cierre de sesion
            /*
            DataRowView row = cbxLugares.SelectedItem as DataRowView;
            if (row != null)
            {
                if (row.Row.Table.Columns.Contains("Coordenada_X") && row.Row.Table.Columns.Contains("Coordenada_Y"))
                {
                    int coordenadaX = Convert.ToInt32(row["Coordenada_X"]);
                    int coordenadaY = Convert.ToInt32(row["Coordenada_Y"]);

                    Mapa.CurrentMapa.SetNodoFinal(coordenadaX, coordenadaY);
                    Mapa.CurrentMapa.FindPath();
                }
            }
            else
            {
                Mapa.CurrentMapa.ClearPoints();
            }*/
        }

        private void MapaRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CustomRadioButton radioButton)
            {
                if (radioButton.Checked)
                {
                    int mapaSeleccionado = 0;

                    if (radioButton.Text == "Piso 1")
                    {
                        mapaSeleccionado = 1;
                    }
                    else if (radioButton.Text == "Piso 2")
                    {
                        mapaSeleccionado = 2;
                    }

                    Mapa.CurrentMapa.CambiarMapa(mapaSeleccionado);
                }
            }
        }

        //Cierre de sesion automatico
        #region
        //Si el temporizador de actividad llega al tick se cierra el form
        //El temporizador tiene el intervalo cada 10 000 milisegundos
        private void timerActividad_Tick(object sender, EventArgs e)
        {
            if (Sesion.LoggedRol != TipoRol.Operador || Sesion.LoggedRol != TipoRol.Administrador)
            {
                this.Close();
            }
        }

        //Si se hace click en algun panel del reinicia el temporizador de actividad
        private void plForms_Click(object sender, EventArgs e)
        {
            timerCierreSesion.Stop();
            timerCierreSesion.Start();
        }
        private void panel4_Click(object sender, EventArgs e)
        {
        }

        private void ResetTimerCierreSesion()
        {
            timerCierreSesion.Stop();
            timerCierreSesion.Start();
        }



        #endregion

        private void btnAyuda_Click(object sender, EventArgs e)
        {
            ResetTimerCierreSesion(); // Reinicia el timer de cierre de sesion

            //Abre el manual
            ManualDeUsuario manual = new ManualDeUsuario();
            Metodos.SetMenuForm(this); //Almacenamos la instancia del formulario menú
            Metodos.OpenChildForm(manual, plForms);
        }

        private void btnClase_Click(object sender, EventArgs e)
        {
            ResetTimerCierreSesion(); //Reinicia el timer de cierre de sesion
        }

        private void btnGrupo_Click(object sender, EventArgs e)
        {
            ResetTimerCierreSesion();//Reinicia el timer de cierre de sesion
        }
    }
}



