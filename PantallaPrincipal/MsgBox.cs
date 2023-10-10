using System;
using System.Drawing;
using System.Windows.Forms;

namespace Proyecto
{
    public partial class MsgBox : Form
    {

        public MsgBox(string pTipo, string pMensaje)
        {
            InitializeComponent();
            lblMsg.Text = pMensaje;     //cambiamos el text del label por el string que recibimos
                                        //Condición para mostrar imagen y cambiar color
            switch (pTipo)
            {
                case "pregunta":
                    lblTitulo.Text = "Pregunta.";//Cambiamos el label titulo
                    lblTitulo.ForeColor = Color.FromArgb(33, 150, 243);//color de letra
                    pL1.BackColor = Color.FromArgb(33, 150, 243);//panel la primera línea
                    pbQuest.Visible = true;//mostramos la imagen pregunta
                    btnAceptar.Visible = false;
                    btnNo.Visible = true;
                    btnSi.Visible = true;
                    break;
                case "aviso":
                    lblTitulo.Text = "Aviso.";
                    lblTitulo.ForeColor = Color.FromArgb(255, 193, 7);
                    pL1.BackColor = Color.FromArgb(255, 193, 7);
                    pbWar.Visible = true;//mostramos la imagen aviso
                    break;
                case "error":
                    lblTitulo.Text = "Error.";
                    lblTitulo.ForeColor = Color.FromArgb(244, 67, 54);
                    pL1.BackColor = Color.FromArgb(244, 67, 54);
                    pbError.Visible = true;//mostramos la imagen Error
                    break;
                case "exito":
                    lblTitulo.Text = "Operación realizada con éxito.";//Cambiamos el label titulo
                    lblTitulo.ForeColor = Color.FromArgb(28, 100, 192);//color de letra
                    pL1.BackColor = Color.FromArgb(28, 100, 192);//panel la primera línea
                    pbExito.Visible = true;//mostramos la imagen error
                    break;
                default:
                    lblTitulo.Text = "Error al seleccionar";
                    break;
            }
        }

        public void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       private void btnSiLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.Close();        }
    }
}
