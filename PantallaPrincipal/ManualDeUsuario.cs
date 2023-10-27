using CapaEntidades;
using System;
using System.IO;
using System.Windows.Forms;

namespace Proyecto
{
    public partial class ManualDeUsuario : Form
    {
        public ManualDeUsuario()
        {
            InitializeComponent();
        }

        private void ManualDeUsuario_Load(object sender, EventArgs e)
        {
            string directorioAplicacion = Path.GetDirectoryName(Application.ExecutablePath);
            string path;
            if (Sesion.LoggedRol == TipoRol.Alumno || Sesion.LoggedRol == TipoRol.Docente)
            {
                path = Path.Combine(directorioAplicacion, "Nitid Developing - Redes de datos y seguridad - Segunda entrega.rtf");
            }
            else
            {
                path = Path.Combine(directorioAplicacion, "Nitid Developing - Redes de datos y seguridad - Segunda entrega.rtf");
            }
            richTxt.LoadFile(path, RichTextBoxStreamType.RichText);
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
