using CapaEntidades;
using CapaNegocio;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AulaGO
{
    public partial class PIN : Form
    {
        private byte intentosFallidos = 0;
        public PIN()
        {
            InitializeComponent();

        }

        //Botones del numpad
        #region
        private void NumeroButton_Click(object sender, EventArgs e)
        {
            Metodos metodos = new Metodos();
            metodos.ResetTimerCierreSesion(); // Reinicia el timer de cierre de sesion

            Button boton = (Button)sender;
            string numero = boton.Text;

            if (txtNewPIN.Enabled == false)
            {
                txtOldPIN.Text += numero;           //Si txtPIN está desactivado, va a escribir el número clickeado de forma acumulada en txtCI,
                                                    //sino lo hará en txtPIN
            }
            else if (txtNewPIN2.Enabled == false)
            {
                txtNewPIN.Text += numero;
            }
            else if (txtNewPIN2.Enabled)
            {
                txtNewPIN2.Text += numero;
            }
        }
        private void btnBorrar_Click(object sender, EventArgs e)
        {
            Metodos metodos = new Metodos();
            metodos.ResetTimerCierreSesion(); // Reinicia el timer de cierre de sesion

            if (txtNewPIN.Enabled == false)
            {
                if (txtOldPIN.Text.Length > 0)
                {
                    txtOldPIN.Text = txtOldPIN.Text.Substring(0, txtOldPIN.Text.Length - 1);
                }
            }
            else if (txtNewPIN2.Enabled == false)
            {
                if (txtNewPIN.Text.Length > 0)
                {
                    txtNewPIN.Text = txtNewPIN.Text.Substring(0, txtNewPIN.Text.Length - 1);
                }
            }
            else if (txtNewPIN2.Enabled)
            {
                if (txtNewPIN2.Text.Length > 0)
                {
                    txtNewPIN2.Text = txtNewPIN2.Text.Substring(0, txtNewPIN2.Text.Length - 1);
                }
            }
        }
        private void btnSig_Click(object sender, EventArgs e)
        {
            Metodos metodos = new Metodos();
            metodos.ResetTimerCierreSesion(); // Reinicia el timer de cierre de sesion
            MsgBox msg;

            Validaciones validaciones = new Validaciones();
            if (txtNewPIN.Enabled == false)
            {
                if (txtOldPIN.Text != Sesion.LoggedPin.ToString())
                {
                    msg = new MsgBox("error", "Pin incorrecto, no coincide con el pin de esta cuenta.");
                    intentosFallidos++;

                    if (intentosFallidos >= 5)
                    {
                        msg = new MsgBox("error", "5 intentos fallidos, cerrando sesion.");
                        Sesion sesion = new Sesion();
                        sesion.LogOut();
                        Close();
                        Metodos.CloseMenuForm();

                    }
                    msg.ShowDialog();
                }
                else
                {
                    pbOk.Visible = true; //Muestra un ícono de verificación
                    txtNewPIN.Enabled = true;   //Si la cédula es correcta, activa txtPIN
                }
            }
            else if (txtNewPIN2.Enabled == false)
            {
                if (validaciones.ValidarPIN(txtNewPIN.Text))
                {
                    pbOk1.Visible = true;
                    txtNewPIN2.Enabled = true;
                }
                else
                {
                    msg = new MsgBox("error", "Debe ingresar un pin de 4 digitos"); //Personalizo el mensaje y declaro qué tipo de error me muestra
                    msg.ShowDialog();
                }
            }
            else if (txtNewPIN2.Enabled)
            {
                if (txtNewPIN.Text == txtNewPIN2.Text)
                {
                    pbOk2.Visible = true;
                    btnAceptar.Enabled = true;
                }
                else
                {
                    txtNewPIN2.Text = string.Empty;
                    txtNewPIN2.Enabled = false;
                    msg = new MsgBox("error", "El pin nuevo y la repeticion deben ser iguales (no coinciden entre si)."); //Personalizo el mensaje y declaro qué tipo de error me muestra
                    msg.ShowDialog();
                }
            }


        }

        #endregion

        private void txtOldPIN_Click(object sender, EventArgs e)
        {
            Metodos metodos = new Metodos();
            metodos.ResetTimerCierreSesion(); // Reinicia el timer de cierre de sesion

            Location = new Point(300, 224);
            ClientSize = new Size(1317, 591);    //Al seleccionar txtCI se mostrará el numpad
            panel1.ClientSize = new Size(1309, 583);
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Metodos metodos = new Metodos();
            metodos.ResetTimerCierreSesion(); // Reinicia el timer de cierre de sesion

            MsgBox confirm = new MsgBox("pregunta", "Esta seguro que desea cambiar su pin?");
            confirm.ShowDialog();

            if (confirm.DialogResult == DialogResult.Yes)
            {
                Negocio negocio = new Negocio();
                string pin = txtNewPIN.Text;
                MsgBox msgResultado;

                try
                {
                    RetornoValidacion resultadoOperacion = negocio.ActualizarPin(pin);

                    switch (resultadoOperacion)
                    {
                        case RetornoValidacion.OK:
                            msgResultado = new MsgBox("exito", "Operacion exitosa");
                            msgResultado.ShowDialog();
                            Close();
                            break;

                        case RetornoValidacion.ErrorInesperadoBD:
                            msgResultado = new MsgBox("error", "Error inesperado en la base de datos. Intentelo de nuevo, si el error persiste contacte con un administrador.");
                            msgResultado.ShowDialog();
                            break;

                    }
                }
                catch (Exception ex)
                {
                    MsgBox error = new MsgBox("error", ex.Message);
                    error.ShowDialog();
                }
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Metodos metodos = new Metodos();
            metodos.ResetTimerCierreSesion(); // Reinicia el timer de cierre de sesion

            Close();
        }

        //Manejadores de los subrayados
        #region
        private void txtNewPIN2_TextChanged(object sender, EventArgs e)
        {
            if (txtNewPIN2.Text == "")
            {
                label6.BackColor = Color.DodgerBlue; //Si el txt está vacío, el subrayado es celeste
            }
            if (txtNewPIN2.Text.Length >= 1 && txtNewPIN.Text.Length < 4) //Si el txt está tiene un número de caracteres menor a 4 es rojo
            {
                label6.BackColor = Color.Red;
            }
            if (txtNewPIN2.Text.Length == 4) //Si el txt está tiene un número de caracteres igual a 4 el subrayado es verde
            {
                label6.BackColor = Color.Green;
            }
            if (txtNewPIN2.Text.Length > 4)  //No permite que el texto sea de más de 4 números
            {
                txtNewPIN2.Text = txtNewPIN2.Text.Substring(0, txtNewPIN2.Text.Length - 1);
            }
        }
        private void txtOldPin_TextChanged(object sender, EventArgs e)
        {
            if (txtOldPIN.Text == "")
            {
                label3.BackColor = Color.DodgerBlue; //Si el txt está vacío, el subrayado es celeste
            }
            if (txtOldPIN.Text.Length >= 1 && txtOldPIN.Text.Length < 8)  //Si el txt está tiene un número de caracteres menor a 8 es rojo
            {
                label3.BackColor = Color.Red;
            }
            if (txtOldPIN.Text.Length == 4) //Si el txt está tiene un número de caracteres igual a 8 el subrayado es verde
            {
                label3.BackColor = Color.Green;
            }
            if (txtOldPIN.Text.Length > 4)    //No permite que el texto sea de más de 8 números
            {
                txtOldPIN.Text = txtOldPIN.Text.Substring(0, txtOldPIN.Text.Length - 1);
            }
        }
        private void txtNewPin_TextChanged(object sender, EventArgs e)
        {
            if (txtNewPIN.Text == "")
            {
                label4.BackColor = Color.DodgerBlue; //Si el txt está vacío, el subrayado es celeste
            }
            if (txtNewPIN.Text.Length >= 1 && txtNewPIN.Text.Length < 4) //Si el txt está tiene un número de caracteres menor a 4 es rojo
            {
                label4.BackColor = Color.Red;
            }
            if (txtNewPIN.Text.Length == 4) //Si el txt está tiene un número de caracteres igual a 4 el subrayado es verde
            {
                label4.BackColor = Color.Green;
            }
            if (txtNewPIN.Text.Length > 4)  //No permite que el texto sea de más de 4 números
            {
                txtNewPIN.Text = txtNewPIN.Text.Substring(0, txtNewPIN.Text.Length - 1);
            }
        }
        #endregion

    }
}
