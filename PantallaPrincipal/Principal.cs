using CapaEntidades;
using System;
using System.Windows.Forms;

namespace AulaGO
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
            Sesion sesion = new Sesion();
            sesion.LogOut();
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
            menu.ShowDialog();
        }
    }
}
