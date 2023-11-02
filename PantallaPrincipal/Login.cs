using CapaEntidades;
using CapaNegocio;
using System;
using System.Drawing;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace Proyecto
{

    public partial class Login : Form
    {
        public Login()
        {
            Sesion sesion = new Sesion();
            sesion.LogOut();
            InitializeComponent();

        }

        //Botones del numpad
        #region
        private void NumeroButton_Click(object sender, EventArgs e)
        {
            Button boton = (Button)sender;
            string numero = boton.Text;

            if (txtPIN.Enabled == false)
            {
                txtCI.Text += numero;           //Si txtPIN está desactivado, va a escribir el número clickeado de forma acumulada en txtCI,
                                                //sino lo hará en txtPIN
            }
            else
            {
                txtPIN.Text += numero;
            }
        }
        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (txtPIN.Enabled == false)
            {
                if (txtCI.Text.Length > 0)                                              // Si el txtPIN está desactivado, va a borrar
                                                                                        // en el txtCI, sino borra en el txtPIN.
                {
                    txtCI.Text = txtCI.Text.Substring(0, txtCI.Text.Length - 1);
                    pbOk.Visible = false;
                }
            }
            else
                if (txtPIN.Text.Length > 0)
            {
                txtPIN.Text = txtPIN.Text.Substring(0, txtPIN.Text.Length - 1);
                pbOk1.Visible = false;
            }

        }
        private void btnSig_Click(object sender, EventArgs e)
        {
            string ci = txtCI.Text;
            MsgBox msg;

            Validaciones validaciones = new Validaciones();
            if (txtPIN.Enabled == false)
            {
                if (txtCI.Text == "")
                {
                    msg = new MsgBox("error", "Ingrese su cédula."); //Personalizo el mensaje y declaro qué tipo de error me muestra
                    msg.ShowDialog();
                }
                else

                if (validaciones.ValidarCI(ci))
                {
                    pbOk.Visible = true; //Muestra un ícono de verificación
                    txtPIN.Enabled = true;   //Si la cédula es correcta, activa txtPIN
                }
                else if (!validaciones.ValidarCI(ci))
                {
                    lblSubCi.BackColor = Color.Red;
                    msg = new MsgBox("error", "Cédula no válida, debe ser un numero de 8 digitos."); //Personalizo el mensaje y declaro qué tipo de error me muestra
                    msg.ShowDialog();
                }
            }

            else
            {

                string pin = txtPIN.Text;

                if (validaciones.ValidarPIN(pin))  //Si el pin es el del usuario, entonces inicia sesión.
                {
                    pbOk1.Visible = true;
                }
                else
                {
                    msg = new MsgBox("error", "PIN no válido. Debe ingresar un numero de 4 cifras."); //Personalizo el mensaje y declaro qué tipo de error me muestra
                    msg.ShowDialog();
                }
            }
        }
        #endregion

        //Controles para los subrayados de los textbox
        #region
        private void txtCI_TextChanged(object sender, EventArgs e)
        {
            if (txtCI.Text == "")
            {
                lblSubCi.BackColor = Color.DodgerBlue; //Si el txt está vacío, el subrayado es celeste
            }
            if (txtCI.Text.Length >= 1 && txtCI.Text.Length < 8)  //Si el txt está tiene un número de caracteres menor a 8 es rojo
            {
                lblSubCi.BackColor = Color.Red;
            }
            if (txtCI.Text.Length == 8) //Si el txt está tiene un número de caracteres igual a 8 el subrayado es verde
            {
                lblSubCi.BackColor = Color.Green;
            }
            if (txtCI.Text.Length > 8)    //No permite que el texto sea de más de 8 números
            {
                txtCI.Text = txtCI.Text.Substring(0, txtCI.Text.Length - 1);
            }
        }
        private void txtPIN_TextChanged(object sender, EventArgs e)
        {
            if (txtPIN.Text == "")
            {
                lblSubPIN.BackColor = Color.DodgerBlue; //Si el txt está vacío, el subrayado es celeste
            }
            if (txtPIN.Text.Length >= 1 && txtPIN.Text.Length < 4) //Si el txt está tiene un número de caracteres menor a 4 es rojo
            {
                lblSubPIN.BackColor = Color.Red;
            }
            if (txtPIN.Text.Length == 4) //Si el txt está tiene un número de caracteres igual a 4 el subrayado es verde
            {
                lblSubPIN.BackColor = Color.Green;
            }
            if (txtPIN.Text.Length > 4)  //No permite que el texto sea de más de 4 números
            {
                txtPIN.Text = txtPIN.Text.Substring(0, txtPIN.Text.Length - 1);
            }
        }
        #endregion

        //Abre el numpad
        private void txtCI_Click(object sender, EventArgs e)
        {
            Location = new Point(300, 267);
            ClientSize = new Size(1243, 505);              //Al seleccionar txtCI se mostrará el numpad                              
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Negocio negocio = new Negocio();
            Validaciones validaciones = new Validaciones();
            Menú menu = new Menú();
            MsgBox msg;

            string ci = txtCI.Text;
            string pin = txtPIN.Text;

            if (validaciones.ValidarVacio(ci) || validaciones.ValidarVacio(pin))
            {
                msg = new MsgBox("error", "Debe completar todos los campos.");
                msg.ShowDialog();
            }
            else if (!validaciones.ValidarCI(ci) || !validaciones.ValidarPIN(pin))
            {
                msg = new MsgBox("error", "Formatos no válidos. La cedula debe ser un numero de 8 digitos y el PIN un numero de 4 digitos.");
                msg.ShowDialog();

            }
            else
            {
               // try
               // {
                    RetornoValidacion intentologin = negocio.IntentarLogIn(ci, pin);

                    if (intentologin == RetornoValidacion.OK)
                    {
                        menu.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        msg = new MsgBox("error", "No se encontro ningun usuario con ese CI y PIN, intentelo de nuevo.");
                        txtPIN.Text = string.Empty;
                        txtPIN.Enabled = false;
                        pbOk1.Visible = false;
                        msg.ShowDialog();
                    }
            /*    }
                catch (Exception ex)
                {
                    MsgBox excepcion = new MsgBox("error", ex.Message);
                    excepcion.ShowDialog();
                }
            */
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Close(); //Cierro el login
        }

        private void txtCI_KeyPress(object sender, KeyPressEventArgs e)
        {
            Metodos.SoloNumeros(e);
        }

        private void txtPIN_KeyPress(object sender, KeyPressEventArgs e)
        {
            Metodos.SoloNumeros(e);
        }
    }
}
