using System;
using System.Data;
using System.Windows.Forms;
using MySqlConnector;

namespace Proyecto
{
    public class Metodos
    {
        static string query="";

        private static Form activeForm = null; //Declaramos una variable activeForm para que no se acumulen forms en el panel
        public static Menú menuForm = null;

        public static void SetMenuForm(Menú form) //Almacenamos la instancia del formulario menú
        {
            menuForm = form;
        }
        public static void openChildForm(Form childForm, Panel panel) //Abrir un formulario en el panel "plForms"
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
            childForm.Show(); //Mostramos el form
        }
        public static bool buscarCI(TextBox txtCI)
        {

            MySqlConnection conn = new MySqlConnection();
            MySqlCommand cmd = new MySqlCommand("select CI from usuario where CI = '" + txtCI.Text + "'", conn);
            conn.Open();

            MySqlDataReader dr = cmd.ExecuteReader();

            return dr.Read();

        }

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
                e.Handled = true;
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

        public static bool ValidarCI(string txtCI)
        {
            if (txtCI.Length >= 1 && txtCI.Length < 8)
            {
                return true;
            }
            return false;
        }

        public static bool ValidarPIN(string txtPIN)
        {
            if (txtPIN.Length >= 1 && txtPIN.Length < 4)
            {
                return true;
            }
            return false;
        }

        public static bool ValidarCampos(string txtCI, string txtPIN, string txtNombre, string txtApellido, ComboBox cbxCombobox, ComboBox cbxCombobox2)
        {
            if (txtCI == "" || txtPIN == "" || txtNombre == "" || txtApellido == "" || (cbxCombobox != null && cbxCombobox.Text == "") || (cbxCombobox2 != null && cbxCombobox2.Text == ""))
            {
                return true;
            }
            return false;
        }
        public static bool buscarPIN(TextBox txtPIN, TextBox txtCI)
        {
            Boolean find = false;

            query = "select pin from usuario where ci='" + txtCI.Text + "';";
            MySqlConnection conn = new MySqlConnection();
            MySqlCommand cmd = new MySqlCommand(query, conn);
            conn.Open();

            MySqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                int pin = dr.GetInt32("pin");
                if (pin.ToString().Equals(txtPIN.Text))
                {
                    find = true;
                }
            }
            return find;
        }

        public static string buscarCiRetornaNombreTipo(string ci)
        {
            string nombre = "";

            query = "select * from usuario where ci='" + ci + "';";
            MySqlConnection conn = new MySqlConnection();
            MySqlCommand cmd = new MySqlCommand(query, conn);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                nombre = dr.GetString("Nombre");
            }

            return nombre + " - ";
        }
        public static bool buscarCiRetornaAlumno()
        {
            query = "SELECT * FROM usuario JOIN alumno ON usuario.ci = alumno.ci_alumno;";
            MySqlConnection conn = new MySqlConnection();
            MySqlCommand cmd = new MySqlCommand(query, conn);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();

            return true;
        }

        public static bool buscarCiRetornaDocente()
        {
            query = "SELECT * FROM usuario JOIN docente ON usuario.ci = docente.ci_docente;";
            MySqlConnection conn = new MySqlConnection();
            MySqlCommand cmd = new MySqlCommand(query, conn);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();

            return true;

        }

        public static bool buscarCiRetornaOperador()
        {
            query = "SELECT * FROM usuario JOIN funcionario ON usuario.ci = funcionario.ci_funcionario;";
            MySqlConnection conn = new MySqlConnection();
            MySqlCommand cmd = new MySqlCommand(query, conn);
            conn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();

            return true;

        }

    }
}
