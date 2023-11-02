using CapaEntidades;
using System;
using System.Windows.Forms;

namespace Proyecto
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
            Menú menú = new Menú();
            menú.ShowDialog();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login login = new Login(); //Llamar al form
            login.ShowDialog();    //Mostrar el form       
        }

        private void btnGuest_Click(object sender, EventArgs e)
        {
            Menú menu = new Menú();
            Sesion sesion = new Sesion();
            sesion.LogIn();
            menu.Show();
        }
    }
}
