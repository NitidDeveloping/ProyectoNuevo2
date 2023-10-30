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
    public partial class AgregarAlumnoDocenteAGrupo : Form
    {
        private Grupo grupoConsulta;
        private Materia materia = null; //Materia a la que se asiganara el docente

        public AgregarAlumnoDocenteAGrupo(Grupo grupoConsulta)
        {
            InitializeComponent();
            this.grupoConsulta = grupoConsulta;
        }

        public AgregarAlumnoDocenteAGrupo(Grupo grupoConsulta, Materia materia)
        {
            InitializeComponent();
            this.grupoConsulta = grupoConsulta;
            this.materia = materia;
        }

        private void AgregarAlumnoDocenteAGrupo_Load(object sender, EventArgs e)
        {
            bool isAgregarDocente = materia != null;
            //Setea los labels con los valores del grupo
            lblId.Text = grupoConsulta.Nombre;

            if (isAgregarDocente)
            {
                txtCI.Text = "CI del Docente";
                lblAuxMateria.Visible = isAgregarDocente;
                lblMateria.Visible = isAgregarDocente;
                lblMateria.Text = materia.Nombre.ToString();
            }
            else
            {
                txtCI.Text ="CI del Alumno";
            }
        }


        private void txtCI_Enter(object sender, EventArgs e) //Si se hace foco en el textbox por primera vez quita el texto de pista
        {
            txtCI.Text = string.Empty;
        }

        private void txtCI_KeyPress(object sender, KeyPressEventArgs e)
        {
            btnAceptar.Enabled = false;
            Negocio negocio = new Negocio();
            string ci = txtCI.Text;
            Validaciones validaciones = new Validaciones();
            Usuario consulta;
            MsgBox msg = null;
            bool isAgregarDocente = materia != null; // Es verdadero si la materia es distinto de null, que solo debe pasar cuando se invoca desde el boton agregar docente
            string auxMensaje = null; //Usado para asignarle un mensaje al msg box

            //Si presiona enter mientras escribe hacemos validaciones sobre la cedula escrita y mostramos un mensaje si algo no esta bien
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (validaciones.ValidarVacio(ci))
                {
                    auxMensaje = isAgregarDocente ? "Debe ingresar la cedula de un docente inscrito en el sistema para continuar" : "Debe ingresar la cedula de un alumno inscrito en el sistema para continuar";
                }

                else if (!validaciones.ValidarCI(ci))
                {
                    auxMensaje = "Cédula no válida";
                }
                else
                {
                    try
                    {
                        consulta = isAgregarDocente ? negocio.ConsultarAlumnosDocentes(TipoReferencia.Docente, ci) : negocio.ConsultarAlumnosDocentes(TipoReferencia.Alumno, ci);

                        if (consulta == null)
                        {
                            auxMensaje = isAgregarDocente ? "No se encontraron docentes con esa cedula" : "No se encontraron alumnos con esa cedula";
                        }
                        else if (!isAgregarDocente && negocio.ConsultarAlumnoEnGrupo(ci, grupoConsulta.Nombre)) //Si se esta agregando un alumno y se encuentra que ya esta ingresado en el grupo muestra un mensaje de error
                        {
                            auxMensaje = "El alumno que intenta ingresar ya esta inscrito en este grupo";
                        }
                        else if (isAgregarDocente && negocio.ConsultarDocenteEnGrupoMateria(ci, grupoConsulta.Nombre, materia.Id))
                        {
                            auxMensaje = "El docente que intenta ingresar ya esta inscrito en esta materia en este grupo";
                        }
                        else
                        {
                            txtApellido.Text = consulta.Apellido;
                            txtNombre.Text = consulta.Nombre;
                            btnAceptar.Enabled = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MsgBox mensajeDeError = new MsgBox("error", ex.Message);
                        mensajeDeError.ShowDialog();
                    }
                    
                }


                if (auxMensaje != null)
                {
                    msg = new MsgBox("error", auxMensaje);
                    msg.ShowDialog();
                    txtCI.Focus();
                }

            }
            else
            {
                Metodos.SoloNumeros(e);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ConsultaGrupo consultaGrupo; //Formulario de consulta grupo
            consultaGrupo = new ConsultaGrupo(grupoConsulta);
            Metodos.OpenChildForm(consultaGrupo, Metodos.menuForm.plForms);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Negocio negocio = new Negocio();
            RetornoValidacion resultadoOperacion;
            MsgBox msg = new MsgBox("error", "Ha ocurrido un error inesperado a la hora de intentar hacer la operacion, intentelo de nuvo o contacte con un adiministrador del sistema");
            bool isAgregarDocente = materia != null;

            try
            {
                resultadoOperacion = isAgregarDocente ? negocio.AgregarDocenteEnGrupoMateria(int.Parse(txtCI.Text), grupoConsulta.Nombre, materia.Id) : negocio.AgregarAlumnoAGrupo(txtCI.Text, grupoConsulta.Nombre);

                if (resultadoOperacion == RetornoValidacion.OK)
                {
                    msg = new MsgBox("exito", isAgregarDocente ? "Se ha asignado el docente satisfactoriamente" : "Se ha asignado el alumno en el grupo satisfactoriamente");

                    //Si se esta agregando un docente cierra la ventana para que no agregue mas de un docente a una materia
                    //Sino vacia los campos para que siga inscribiendo alumnos si quiere
                    if (isAgregarDocente)
                    {
                        btnCancelar_Click(sender, e);
                    }
                    else
                    {
                        txtCI.Text = "";
                        txtApellido.Text = "";
                        txtNombre.Text = "";
                        btnAceptar.Enabled = false;
                    }
                }
                else if (resultadoOperacion == RetornoValidacion.ErrorInesperadoBD)
                {
                    msg = new MsgBox("error", "Ha ocurrido un error inesperado a la hora de ingresar el valor a la base de datos, intentelo de nuevo o contacte con un administrador del sistema");
                }
                else
                {
                    throw new Exception("No se esperaba este tipo de retorno validacion");
                }
            }
            catch (Exception ex)
            {
                msg = new MsgBox("error", ex.Message);
            }
            finally
            {
                msg.ShowDialog();
            }

        }

    }
}
