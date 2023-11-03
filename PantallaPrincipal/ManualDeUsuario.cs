using CapaEntidades;
using System;
using System.Windows.Forms;
using PdfiumViewer;

namespace AulaGO
{
    public partial class ManualDeUsuario : Form
    {
        public ManualDeUsuario()
        {
            InitializeComponent();
        }

        private void ManualDeUsuario_Load(object sender, EventArgs e)
        {
            Metodos metodos = new Metodos();
            metodos.ResetTimerCierreSesion(); // Reinicia el timer de cierre de sesion

            string ruta;
            if (Sesion.LoggedRol == TipoRol.Alumno || Sesion.LoggedRol == TipoRol.Docente || Sesion.LoggedRol == TipoRol.Visitante)
            {
                ruta = "Nitid Developing - ADA - Manual de usuario.pdf";
            }
            else
            {
                ruta = @"Nitid Developing - ADA - Manual de administrador.pdf";
            }

            PdfDocument pdfDocument = PdfDocument.Load(ruta);
            pdfViewer1.Document = pdfDocument;
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Metodos metodos = new Metodos();
            metodos.ResetTimerCierreSesion(); // Reinicia el timer de cierre de sesion

            Close();
        }
    }
}
