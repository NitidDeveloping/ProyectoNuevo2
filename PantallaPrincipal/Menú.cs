using CapaEntidades;
using CapaNegocio;
using Proyecto;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Proyecto
{
    public partial class Menú : Form
    {
        private Button currentButton;

        public Menú()
        {
            InitializeComponent();

            plPersona.Paint += Panel1_Paint;
        }

        private void Menú_Load(object sender, EventArgs e)
        {
            timer1.Start();

            //Timer de cierre de sesion automatico
            if (Sesion.LoggedRol == TipoRol.Alumno || Sesion.LoggedRol == TipoRol.Docente)
            {
                timerCierreSesion.Start();
            }

            Mapa mapa = new Mapa();
            Metodos.SetMenuForm(this); //Almacenamos la instancia del formulario menú
            Metodos.openChildForm(mapa, plMapa);
            Negocio negocio = new Negocio();


            // Asignar el DataTable al ComboBox
            cbxLugares.DataSource = negocio.Listar(TipoReferencia.Lugar, null, null);
            cbxLugares.DisplayMember = "Nombre";
            cbxLugares.ValueMember = "Nombre";
            cbxLugares.SelectedIndex = -1;

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

        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    Color color = Color.FromArgb(178, 8, 55);
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = color;
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new System.Drawing.Font("MADE INFINITY PERSONAL USE", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                }
            }
        }

        private void DisableButton()
        {
            foreach (Control previousBtn in plLateral.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.Gainsboro;
                    previousBtn.ForeColor = Color.Black;
                    previousBtn.Font = new System.Drawing.Font("MADE INFINITY PERSONAL USE", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }
        private void CerrarSesion()  //Creo el método para confirmar el cierre de sesión
        {
            MsgBox msg = new MsgBox("pregunta", "¿Desea cerrar sesión?"); //Hago la pregunta

            msg.btnAceptar.Visible = false; //Oculto el botón "Aceptar" del form "MsgBox"
            msg.btnSi.Visible = true; //Muestro el botón "Sí" del form "MsgBox"
            msg.btnNo.Visible = true; //Muestro el botón "No" del form "MsgBox"
            msg.label3.Visible = true; //Muestro el "label3" del form "MsgBox"

            msg.btnSi.Click += (sender, e) => //Le asigno una función al botón "Sí" del form "MsgBox"
            {
                msg.Close(); //Cierro el mensaje
                Close(); //Cierro el menú
            };
            msg.btnNo.Click += (sender, e) => //Le asigno una función al botón "No" del form "MsgBox"
            {
                msg.Close(); //Cierro el mensaje
            };
            msg.ShowDialog(); //Luego de asignar las funciones de cada botón, muestro el form con las modificaciones realizadas previamente
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
            ActivateButton(sender);
        }

        private void btnDatos_Click(object sender, EventArgs e)
        {
            ShowSubMenu(plDatosSubMenu);
            ActivateButton(sender);
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
            // Centrar el label en el panel
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
            Lista lista = new Lista();
            Metodos.SetMenuForm(this); //Almacenamos la instancia del formulario menú
            Metodos.openChildForm(lista, plForms);
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
        {/*
            Mapa mapa = new Mapa();
            Metodos.SetMenuForm(this); //Almacenamos la instancia del formulario menú
            Metodos.openChildForm(mapa, plMapa);
            Negocio negocio = new Negocio();
            lblTitulo.Text = "Mapa";

            // Asignar el DataTable al ComboBox
            cbxLugares.DataSource = negocio.Listar(TipoReferencia.Lugar, null, null);
            cbxLugares.DisplayMember = "Nombre";
            cbxLugares.ValueMember = "Nombre";
            cbxLugares.SelectedIndex = -1;
            */
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
                }
            }
            else
            {
                Mapa.CurrentMapa.ClearPoints();
            }
            */
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
            Metodos.openChildForm(manual, plForms);
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



