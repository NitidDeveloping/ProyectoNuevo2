using CapaEntidades;
using CapaNegocio;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Proyecto
{
    public partial class ConsultaGrupo : Form
    {
        private Grupo grupoConsulta;
        public ConsultaGrupo(Grupo grupoConsulta)
        {
            InitializeComponent();
            this.grupoConsulta = grupoConsulta;
        }
        private void ConsultaGrupo_Load(object sender, EventArgs e)
        {
            //Setea los labels con los valores del grupo
            lblId.Text = grupoConsulta.Nombre;
            lblOrientacion.Text = grupoConsulta.Orientacion.Nombre;
            lblAnio.Text = grupoConsulta.Anio.ToString();

            //Inicia los backgroundworkers
            bckgMateriasDocentes.RunWorkerAsync();
            bckgAlumnos.RunWorkerAsync();
        }

        //Botones
        #region
        private void btnVolver_Click(object sender, EventArgs e)
        {
            Lista lista = new Lista();
            this.Close();
            Metodos.openChildForm(lista, Metodos.menuForm.plForms);
        }

        private void btnAgregarAlumno_Click(object sender, EventArgs e)
        {
            AgregarAlumnoAGrupo agregar = new AgregarAlumnoAGrupo(grupoConsulta);
            this.Close();
            Metodos.openChildForm(agregar, Metodos.menuForm.plForms);
        }

        #endregion

        //Backgroundworkers
        #region

        //BckgMateriasDocentes
        #region
        private void bckgMateriasDocentes_DoWork(object sender, DoWorkEventArgs e)
        {
            Negocio negocio = new Negocio();
            try
            {
                e.Result = negocio.ListarMateriasYDocentes(grupoConsulta.Nombre);
            }
            catch (Exception ex)
            {
                MsgBox msg = new MsgBox("error", ex.Message);
                msg.ShowDialog();
            }
        }

        private void bckgMateriasDocentes_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DGVMateriasDocentes.DataSource = (DataTable)e.Result;
            DGVMateriasDocentes.Columns[0].Visible = false; //Pone la columna con la id de la materia en no visible
        }
        #endregion

        //BckgAlumnos
        #region
        private void bckgAlumnos_DoWork(object sender, DoWorkEventArgs e)
        {
            Negocio negocio = new Negocio();
            try
            {
                e.Result = negocio.ListarAlumnosDeGrupo(grupoConsulta.Nombre);
            }
            catch (Exception ex)
            {
                MsgBox msg = new MsgBox("error", ex.Message);
                msg.ShowDialog();
            }
        }

        private void bckgAlumnos_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DGVAlumnos.DataSource = (DataTable)e.Result;
        }
        #endregion

        #endregion


    }
}
