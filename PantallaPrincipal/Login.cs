using System;
using System.Drawing;
using System.Windows.Forms;

namespace Proyecto
{

    public partial class Login : Form
    {             
        public Login()
        {
            InitializeComponent();

        }

        private void btn0_Click(object sender, EventArgs e)
        {
            if (txtPIN.Enabled == false)
            {
                txtCI.Text += "0";               //Si txtPIN está desactivado, va a escribir "0" de forma acumulada en txtCI, sino lo hará en txtPIN
                                                 //esto sucede en cada uno de los números (0-9)                                                                 
            }
            else
                txtPIN.Text += "0";
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if (txtPIN.Enabled == false)
            {
                txtCI.Text += "1";
            }
            else
                txtPIN.Text += "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            if (txtPIN.Enabled == false)
            {
                txtCI.Text += "2";
            }
            else
                txtPIN.Text += "2";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            if (txtPIN.Enabled == false)
            {
                txtCI.Text += "3";
            }
            else
                txtPIN.Text += "3";
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            if (txtPIN.Enabled == false)
            {
                txtCI.Text +="4";
            }
            else
                txtPIN.Text +="4";
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            if (txtPIN.Enabled == false)
            {
                txtCI.Text +="5";
            }
            else
                txtPIN.Text +="5";
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            if (txtPIN.Enabled == false)
            {
                txtCI.Text += "6";
            }
            else
                txtPIN.Text += "6";
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            if (txtPIN.Enabled == false)
            {
                txtCI.Text += "7";
            }
            else
                txtPIN.Text += "7";
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            if (txtPIN.Enabled == false)
            {
                txtCI.Text += "8";
            }
            else
                txtPIN.Text += "8";
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            if (txtPIN.Enabled == false)
            {
                txtCI.Text += "9";
            }
            else
                txtPIN.Text +="9";
        }

        private void txtCI_Click(object sender, EventArgs e)
        {
                this.Location = new Point(300, 267);
                this.ClientSize = new Size(1243, 505);              //Al seleccionar txtCI se mostrará el numpad                              
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (txtPIN.Enabled == false)
            {
                if (txtCI.Text.Length > 0)                                              // Si el txtPIN está desactivado, va a borrar
                                                                                        // en el txtCI, sino borra en el txtPIN.
                {                   
                    txtCI.Text = txtCI.Text.Substring(0, txtCI.Text.Length - 1);                    
                }
            }else
                if (txtPIN.Text.Length > 0)
            {
                txtPIN.Text = txtPIN.Text.Substring(0, txtPIN.Text.Length - 1);
            }

        }

        private void btnSig_Click(object sender, EventArgs e)
        {
            if (txtPIN.Enabled == false)
            {
                if (txtCI.Text == "")
                {
                    MsgBox msg = new MsgBox("error", "Ingrese una cédula."); //Personalizo el mensaje y declaro qué tipo de error me muestra
                    msg.ShowDialog();
                }
                else

                if (Metodos.buscarCI(txtCI) == true) //Invoco el método de "Metodos.cs"
                {
                    pbOk.Visible = true; //Muestra un ícono de verificación
                    txtPIN.Enabled = true;   //Si la cédula es correcta, activa txtPIN
                }
                else if (Metodos.buscarCI(txtCI) == false)
                {
                    lblSubCi.BackColor = Color.Red;
                    MsgBox msg = new MsgBox("error", "Cédula no válida."); //Personalizo el mensaje y declaro qué tipo de error me muestra
                    msg.ShowDialog();
                }
            }
            else if (Metodos.buscarPIN(txtPIN, txtCI) == true)  //Si el pin es el del usuario, entonces inicia sesión.
            {
                pbOk1.Visible = true;
            }
            else { 
                MsgBox msg = new MsgBox("error", "PIN no válido."); //Personalizo el mensaje y declaro qué tipo de error me muestra
                msg.ShowDialog();
        }


        }

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

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Menú menu = new Menú(); //Abro el menú principal

            if (txtCI.Text == "" || txtPIN.Text == "")
            {
                MsgBox msg = new MsgBox("error", "Debe completar todos los campos."); //Personalizo el mensaje y declaro qué tipo de error me muestra
                msg.ShowDialog();
            }

            if (Metodos.buscarCiRetornaOperador() == true)
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
                menu.lblPersona.Text = Metodos.buscarCiRetornaNombreTipo(txtCI.Text);
                MsgBox msg = new MsgBox("exito", "Inicio de sesión exitoso.");
                msg.ShowDialog();                
                menu.ShowDialog();
                this.Close(); //Cierro la ventana de login
                
                
            }

        }
        

        

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close(); //Cierro el login
        }

       
    }
}
