using System;
using System.Drawing;
using System.Windows.Forms;

namespace Proyecto
{

    public partial class PIN : Form
    {
        public PIN()
        {
            InitializeComponent();

        }

        private void btn0_Click(object sender, EventArgs e)
        {
            if (txtNewPIN.Enabled == false)
            {
                txtOldPIN.Text += "0";               //Si txtPIN está desactivado, va a escribir "0" de forma acumulada en txtCI, sino lo hará en txtPIN
                                                     //esto sucede en cada uno de los números (0-9)                                                                 
            }
            else
            {
                txtNewPIN.Text += "0";
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if (txtNewPIN.Enabled == false)
            {
                txtOldPIN.Text += "1";
            }
            else
            {
                txtNewPIN.Text += "1";
            }
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            if (txtNewPIN.Enabled == false)
            {
                txtOldPIN.Text += "2";
            }
            else
            {
                txtNewPIN.Text += "2";
            }
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            if (txtNewPIN.Enabled == false)
            {
                txtOldPIN.Text += "3";
            }
            else
            {
                txtNewPIN.Text += "3";
            }
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            if (txtNewPIN.Enabled == false)
            {
                txtOldPIN.Text += "4";
            }
            else
            {
                txtNewPIN.Text += "4";
            }
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            if (txtNewPIN.Enabled == false)
            {
                txtOldPIN.Text += "5";
            }
            else
            {
                txtNewPIN.Text += "5";
            }
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            if (txtNewPIN.Enabled == false)
            {
                txtOldPIN.Text += "6";
            }
            else
            {
                txtNewPIN.Text += "6";
            }
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            if (txtNewPIN.Enabled == false)
            {
                txtOldPIN.Text += "7";
            }
            else
            {
                txtNewPIN.Text += "7";
            }
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            if (txtNewPIN.Enabled == false)
            {
                txtOldPIN.Text += "8";
            }
            else
            {
                txtNewPIN.Text += "8";
            }
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            if (txtNewPIN.Enabled == false)
            {
                txtOldPIN.Text += "9";
            }
            else
            {
                txtNewPIN.Text += "9";
            }
        }

        private void txtOldPIN_Click(object sender, EventArgs e)
        {
            Location = new Point(300, 224);
            ClientSize = new Size(1317, 591);    //Al seleccionar txtCI se mostrará el numpad
            panel1.ClientSize = new Size(1309, 583);
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (txtNewPIN.Enabled == false)
            {
                if (txtOldPIN.Text.Length > 0)                                              // Si el txtPIN está desactivado, va a borrar
                                                                                            // en el txtCI, sino borra en el txtPIN.
                {
                    txtOldPIN.Text = txtOldPIN.Text.Substring(0, txtOldPIN.Text.Length - 1);
                }
            }
            else
                if (txtNewPIN.Text.Length > 0)
            {
                txtNewPIN.Text = txtNewPIN.Text.Substring(0, txtNewPIN.Text.Length - 1);
            }

        }

        private void btnSig_Click(object sender, EventArgs e)
        {
            if (txtNewPIN.Enabled == false)
            {
                if (txtOldPIN.Text == "")
                {
                    MsgBox msg = new MsgBox("error", "Ingrese una cédula."); //Personalizo el mensaje y declaro qué tipo de error me muestra
                    msg.ShowDialog();
                }
                else

                if (Metodos.buscarCI(txtOldPIN) == true) //Invoco el método de "Metodos.cs"
                {
                    pbOk.Visible = true; //Muestra un ícono de verificación
                    txtNewPIN.Enabled = true;   //Si la cédula es correcta, activa txtPIN
                }
                else if (Metodos.buscarCI(txtOldPIN) == false)
                {
                    label3.BackColor = Color.Red;
                    MsgBox msg = new MsgBox("error", "Cédula no válida."); //Personalizo el mensaje y declaro qué tipo de error me muestra
                    msg.ShowDialog();
                }
            }
            else if (Metodos.buscarPIN(txtNewPIN, txtOldPIN) == true)  //Si el pin es el del usuario, entonces inicia sesión.
            {
                pbOk1.Visible = true;
            }
            else
            {
                MsgBox msg = new MsgBox("error", "PIN no válido."); //Personalizo el mensaje y declaro qué tipo de error me muestra
                msg.ShowDialog();
            }


        }

        private void txtCI_TextChanged(object sender, EventArgs e)
        {
            if (txtOldPIN.Text == "")
            {
                label3.BackColor = Color.DodgerBlue; //Si el txt está vacío, el subrayado es celeste
            }
            if (txtOldPIN.Text.Length >= 1 && txtOldPIN.Text.Length < 8)  //Si el txt está tiene un número de caracteres menor a 8 es rojo
            {
                label3.BackColor = Color.Red;
            }
            if (txtOldPIN.Text.Length == 8) //Si el txt está tiene un número de caracteres igual a 8 el subrayado es verde
            {
                label3.BackColor = Color.Green;
            }
            if (txtOldPIN.Text.Length > 8)    //No permite que el texto sea de más de 8 números
            {
                txtOldPIN.Text = txtOldPIN.Text.Substring(0, txtOldPIN.Text.Length - 1);
            }
        }

        private void txtPIN_TextChanged(object sender, EventArgs e)
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

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Menú menu = new Menú(); //Abro el menú principal

            if (txtOldPIN.Text == "" || txtNewPIN.Text == "")
            {
                MsgBox msg = new MsgBox("error", "Debe completar todos los campos."); //Personalizo el mensaje y declaro qué tipo de error me muestra
                msg.ShowDialog();
            }

            if (Metodos.buscarCiRetornaOperador())
            {
                menu.btnABMOp.Visible = false;
                menu.plABMSubMenu.Size = new Size(307, 127);


            }
            else if (Metodos.buscarCiRetornaAlumno() || Metodos.buscarCiRetornaDocente())
            {
                menu.btnUsuarios.Visible = false;
                menu.btnDatos.Visible = false;
            }


            if (pbOk1.Visible == true)  //Si el pin es el del usuario, entonces inicia sesión.
            {
                menu.lblPersona.Text = Metodos.buscarCiRetornaNombreTipo(txtOldPIN.Text);
                MsgBox msg = new MsgBox("exito", "Inicio de sesión exitoso.");
                msg.ShowDialog();
                menu.ShowDialog();
                Close(); //Cierro la ventana de login


            }

        }




        private void btnExit_Click(object sender, EventArgs e)
        {
            Close(); //Cierro el login
        }


    }
}
