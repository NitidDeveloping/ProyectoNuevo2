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
            Close();
            Metodos.openChildForm(lista, Metodos.menuForm.plForms);
        }

        private void btnAgregarAlumno_Click(object sender, EventArgs e)
        {
            AgregarAlumnoDocenteAGrupo agregar = new AgregarAlumnoDocenteAGrupo(grupoConsulta);
            this.Close();
            Metodos.openChildForm(agregar, Metodos.menuForm.plForms);
        }

        private void btnAsignarMateria_Click(object sender, EventArgs e)
        {
            AgregarMateriaAGrupo agregar = new AgregarMateriaAGrupo(grupoConsulta);
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

        private void btnAsignarDocenteAMateria_Click(object sender, EventArgs e)
        {
            MsgBox msg;
            if (DGVMateriasDocentes.SelectedRows.Count > 0)
            {
                DataGridViewRow row = DGVMateriasDocentes.SelectedRows[0];
                if (row.Cells["CI Docente"].Value.ToString() != string.Empty)
                {
                    msg = new MsgBox("error", "No puede seleccionar materias que ya tengan un docente asignado");
                    msg.ShowDialog();
                }
                else
                {
                    string nombreMateria = row.Cells["Materia"].Value.ToString();
                    ushort idMateria = (ushort)row.Cells["ID_Materia"].Value;
                    Materia materia = new Materia(idMateria, nombreMateria);


                    AgregarAlumnoDocenteAGrupo agregar = new AgregarAlumnoDocenteAGrupo(grupoConsulta, materia);
                    this.Close();
                    Metodos.openChildForm(agregar, Metodos.menuForm.plForms);
                }
            }
            else
            {
                msg = new MsgBox("error", "Debe seleccionar una materia antes de continuar");
                msg.ShowDialog();
            }
        }
    }

}
