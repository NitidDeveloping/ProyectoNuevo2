using System;
using System.Data;
using System.Windows.Forms;
using CapaEntidades;
using MySqlConnector;

namespace Proyecto
{
    public class Metodos
    {
        private static Form activeForm = null; //Declaramos una variable activeForm para que no se acumulen forms en el panel
        public static Menú menuForm = null;
        public static AgregarEditar AgregarForm = null;

        //Cierre de sesion automatico
        public Timer timerCierreSesion;
        private const int tiempoInactividadEnSegundos = 5 * 60; //5 minutos

        //Metodos para el timer cierre de sesion automatico
        #region
        public void InitializeTimer()
        {
            if (Sesion.LoggedRol == TipoRol.Alumno || Sesion.LoggedRol == TipoRol.Docente || Sesion.LoggedRol == TipoRol.Visitante)
            {
                timerCierreSesion = new Timer();
                timerCierreSesion.Interval = tiempoInactividadEnSegundos * 1000; // Convertir segundos a milisegundos
                timerCierreSesion.Tick += TimerCierreSesion_Tick;
                timerCierreSesion.Start();
            }
        }

        public void ResetTimerCierreSesion()
        {
            TipoRol rolActual = Sesion.LoggedRol;
            if (rolActual == TipoRol.Alumno || rolActual == TipoRol.Docente || rolActual == TipoRol.Visitante)
            {
                timerCierreSesion.Stop();
                timerCierreSesion.Start();
            }
        }

        private void TimerCierreSesion_Tick(object sender, EventArgs e)
        {
            TipoRol rolActual = Sesion.LoggedRol;
            if (rolActual == TipoRol.Alumno || rolActual == TipoRol.Docente || rolActual == TipoRol.Visitante)
            {
                Sesion sesion = new Sesion();
                sesion.LogOut();
                CloseMenuForm();
            }
        }
        #endregion



        public static void SetMenuForm(Menú form) //Almacenamos la instancia del formulario menú
        {
            menuForm = form;
        }
        public static void CloseMenuForm()
        {
            if (menuForm != null)
            {
                menuForm.Close();
            }
        }

        public static void SetAgregarForm(AgregarEditar form) //Almacenamos la instancia del formulario AgregarEditar
        {
            AgregarForm = form;
        }
        public static void OpenChildForm(Form childForm, Panel panel) //Abrir un formulario en el panel "plForms"
        {
            if (activeForm != null)
            {  //Si hay un form activo, lo cerramos
                activeForm.Close();
            }

            activeForm = childForm; //Guardamos el form que se abre en la variable "activeForm", esto gracias al parametro "childForm"
            childForm.TopLevel = false; //Aclaramos que el childForm no es de un nivel superior, es decir, va a actuar como un controlador          
            childForm.Dock = DockStyle.Fill; //Llenamos todo el panel
            panel.Controls.Add(childForm); //Agregamos el form al panel para poder controlarlo
            panel.Tag = childForm;  //Asociamos el form con el panel
            childForm.BringToFront(); //Traemos el form hacia el frente en caso de que contenga alguna imagen
            childForm.Show();
        }

        public static void OpenMapForm(Form childForm, Panel panel) //Abrir un formulario en el panel "plForms"
        {

            activeForm = childForm; //Guardamos el form que se abre en la variable "activeForm", esto gracias al parametro "childForm"
            childForm.TopLevel = false; //Aclaramos que el childForm no es de un nivel superior, es decir, va a actuar como un controlador          
            childForm.Dock = DockStyle.Fill; //Llenamos todo el panel
            panel.Controls.Add(childForm); //Agregamos el form al panel para poder controlarlo
            panel.Tag = childForm;  //Asociamos el form con el panel
            childForm.BringToFront(); //Traemos el form hacia el frente en caso de que contenga alguna imagen
            childForm.Show();
        }


        //Controles para los campos
        #region
        public static void SoloLetras(KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        public static void SoloLetrasYNumeros(KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        public static void SoloLetrasYEspacio(KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || char.IsControl(e.KeyChar) || char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        public static void SoloNumeros(KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }
        #endregion

    }
}
