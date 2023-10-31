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
            Metodos.OpenChildForm(lista, Metodos.menuForm.plForms);
        }

        private void btnAgregarAlumno_Click(object sender, EventArgs e)
        {
            AgregarAlumnoDocenteAGrupo agregar = new AgregarAlumnoDocenteAGrupo(grupoConsulta);
            this.Close();
            Metodos.OpenChildForm(agregar, Metodos.menuForm.plForms);
        }

        private void btnAsignarMateria_Click(object sender, EventArgs e)
        {
            AgregarMateriaAGrupo agregar = new AgregarMateriaAGrupo(grupoConsulta);
            this.Close();
            Metodos.OpenChildForm(agregar, Metodos.menuForm.plForms);
        }
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
                    Metodos.OpenChildForm(agregar, Metodos.menuForm.plForms);
                }
            }
            else
            {
                msg = new MsgBox("error", "Debe seleccionar una materia antes de continuar");
                msg.ShowDialog();
            }
        }

        private void btnEliminarAlumno_Click(object sender, EventArgs e)
        {
            string ciAlumno;
            MsgBox msg; //Mensaje de respuesta
            MsgBox confirm; //Mensaje de confirmacion
            RetornoValidacion respuesta;
            Negocio negocio = new Negocio();
            ciAlumno = DGVAlumnos.SelectedRows[0].Cells["CI"].Value.ToString();

            confirm = new MsgBox("pregunta", "Se eliminará el alumno del grupo ¿Está seguro que desea continuar?.");
            confirm.label3.Visible = true;

            if (confirm.ShowDialog() == DialogResult.Yes)
            {
                try
                {

                    respuesta = negocio.EliminarAlumnoDeGrupo(ciAlumno, grupoConsulta.Nombre);

                    if (respuesta == RetornoValidacion.OK)
                    {
                        msg = new MsgBox("exito", "Alumno eliminado correctamente.");
                        msg.ShowDialog();
                        bckgAlumnos.RunWorkerAsync();
                    }
                    else if (respuesta == RetornoValidacion.NoExiste)
                    {
                        msg = new MsgBox("error", "El alumno no esta ingresado en el grupo en la base de datos.");
                        msg.ShowDialog();
                    }
                    else if (respuesta == RetornoValidacion.ErrorInesperadoBD)
                    {
                        msg = new MsgBox("error", "Ha surgido un error inesperado, intente de nuevo, en caso de que el problema persista contacte con un tecnico.");
                        msg.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    msg = new MsgBox("error", ex.Message);
                    msg.ShowDialog();
                }
            }
        }

        private void btnDesasignarDocenteAMateria_Click(object sender, EventArgs e)
        {
            string ciDocente;
            string idMateria;
            MsgBox msg; //Mensaje de respuesta
            MsgBox confirm; //Mensaje de confirmacion
            RetornoValidacion respuesta;
            Negocio negocio = new Negocio();
            ciDocente = DGVMateriasDocentes.SelectedRows[0].Cells["CI Docente"].Value.ToString();
            idMateria = DGVMateriasDocentes.SelectedRows[0].Cells["ID_Materia"].Value.ToString();

            if (ciDocente == string.Empty)
            {
                msg = new MsgBox("error", "La materia seleccionada no tiene ningun docente asignado.");
                msg.ShowDialog();
                return;
            }

            confirm = new MsgBox("pregunta", "Se desasignara el docente de la materia ¿Está seguro que desea continuar?.");
            confirm.label3.Visible = true;

            if (confirm.ShowDialog() == DialogResult.Yes)
            {
                try
                {

                    respuesta = negocio.EliminarDocenteDeGrupoMateria(idMateria, grupoConsulta.Nombre, ciDocente);

                    if (respuesta == RetornoValidacion.OK)
                    {
                        msg = new MsgBox("exito", "Docente desasignado correctamente.");
                        msg.ShowDialog();
                        bckgMateriasDocentes.RunWorkerAsync();
                    }
                    else if (respuesta == RetornoValidacion.NoExiste)
                    {
                        msg = new MsgBox("error", "El docente no esta ingresado en la materia en la base de datos.");
                        msg.ShowDialog();
                    }
                    else if (respuesta == RetornoValidacion.ErrorInesperadoBD)
                    {
                        msg = new MsgBox("error", "Ha surgido un error inesperado, intente de nuevo, en caso de que el problema persista contacte con un tecnico.");
                        msg.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    msg = new MsgBox("error", ex.Message);
                    msg.ShowDialog();
                }
            }
        }

        private void btnDesasignarMateria_Click(object sender, EventArgs e)
        {
            string idMateria;
            MsgBox msg; //Mensaje de respuesta
            MsgBox confirm; //Mensaje de confirmacion
            RetornoValidacion respuesta;
            Negocio negocio = new Negocio();
            idMateria = DGVMateriasDocentes.SelectedRows[0].Cells["ID_Materia"].Value.ToString();

            confirm = new MsgBox("pregunta", "Se desasignara la materia del grupo ¿Está seguro que desea continuar?.");
            confirm.label3.Visible = true;

            if (confirm.ShowDialog() == DialogResult.Yes)
            {
                try
                {

                    respuesta = negocio.EliminarMateriaDeGrupo(idMateria, grupoConsulta.Nombre);

                    if (respuesta == RetornoValidacion.OK)
                    {
                        msg = new MsgBox("exito", "Materia desasignada correctamente.");
                        msg.ShowDialog();
                        bckgMateriasDocentes.RunWorkerAsync();
                    }
                    else if (respuesta == RetornoValidacion.NoExiste)
                    {
                        msg = new MsgBox("error", "La materia no esta ingresada en el grupo.");
                        msg.ShowDialog();
                    }
                    else if (respuesta == RetornoValidacion.ErrorInesperadoBD)
                    {
                        msg = new MsgBox("error", "Ha surgido un error inesperado, intente de nuevo, en caso de que el problema persista contacte con un tecnico.");
                        msg.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    msg = new MsgBox("error", ex.Message);
                    msg.ShowDialog();
                }
            }
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
