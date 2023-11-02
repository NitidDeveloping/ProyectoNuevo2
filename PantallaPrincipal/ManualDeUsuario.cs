using CapaEntidades;
using System;
using System.IO;
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

            /*string directorioAplicacion = Path.GetDirectoryName(Application.ExecutablePath);
            string ruta;
            if (Sesion.LoggedRol == TipoRol.Alumno || Sesion.LoggedRol == TipoRol.Docente || Sesion.LoggedRol == TipoRol.Visitante)
            {
                ruta = Path.Combine(directorioAplicacion, "Nitid Developing - Redes de datos y seguridad - Segunda entrega.rtf");
            }
            else
            {
                ruta = Path.Combine(directorioAplicacion, "Nitid Developing - Redes de datos y seguridad - Segunda entrega.rtf");
            }
            richTxt.LoadFile(ruta, RichTextBoxStreamType.RichText);*/

            string ruta_al_archivo = @"Nitid Developing - ADA - Manual de administrador.pdf";
            PdfDocument pdfDocument = PdfDocument.Load(ruta_al_archivo);
            pdfViewer1.Document = pdfDocument;
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Metodos metodos = new Metodos();
            metodos.ResetTimerCierreSesion(); // Reinicia el timer de cierre de sesion

            Close();
        }
        private void richTxt_VScroll(object sender, EventArgs e)
        {
            Metodos metodos = new Metodos();
            metodos.ResetTimerCierreSesion();
        }
    }
}
