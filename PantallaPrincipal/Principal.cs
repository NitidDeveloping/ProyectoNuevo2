﻿using System;
using System.Windows.Forms;

namespace Proyecto
{
    public partial class Principal : Form
    {
        Metodos metodos = new Metodos(); //Invoco la clase metodos
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
            menu.btnUsuarios.Visible = false;
            menu.btnDatos.Visible = false;
            menu.btnModPIN.Visible = false;
            menu.lblPersona.Text = "Invitado";
            menu.ShowDialog();
        }
    }
}