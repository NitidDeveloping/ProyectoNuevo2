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
    public partial class AgregarAlumnoAGrupo : Form
    {
        private Grupo grupoConsulta;

        public AgregarAlumnoAGrupo(Grupo grupoConsulta)
        {
            InitializeComponent();
            this.grupoConsulta = grupoConsulta;
        }

        private void ConsultaGrupo_Load(object sender, EventArgs e)
        {
            //Setea los labels con los valores del grupo
            lblId.Text = grupoConsulta.Nombre;

            txtCI.Text = "Ingrese la cedula del alumno que desea agregar";
        }


        private void txtCI_Enter(object sender, EventArgs e) //Si se hace foco en el textbox por primera vez quita el texto de pista
        {
                txtCI.Text = string.Empty; 
        }



        private void txtCI_Leave(object sender, EventArgs e)
        {
           
        }

        private void txtCI_KeyPress(object sender, KeyPressEventArgs e)
        {
            btnAceptar.Enabled = false;
            Negocio negocio = new Negocio();
            string ciAlumno = txtCI.Text;
            Validaciones validaciones = new Validaciones();
            Usuario consulta;
            MsgBox msg = null;

            if (e.KeyChar == (char)Keys.Enter)
            {
                if (validaciones.ValidarVacio(ciAlumno))
                {
                    msg = new MsgBox("error", "Debe ingresar la cedula de un alumno inscrito en el sistema para continuar");
                }
                else if (!validaciones.ValidarCI(ciAlumno))
                {
                    msg = new MsgBox("error", "Cédula no válida");
                }
                else
                {
                    consulta = negocio.ConsultarAlumnosDocentes(TipoReferencia.Alumno, ciAlumno);
                    if (consulta == null)
                    {
                        msg = new MsgBox("error", "No se encontraron alumnos con esa cedula");
                    }
                    else if (negocio.ConsultarAlumnoEnGrupo(ciAlumno, grupoConsulta.Nombre))
                    {
                        msg = new MsgBox("error", "El alumno que intenta ingresar ya esta inscrito en este grupo");
                    }
                    else
                    {
                        txtApellido.Text = consulta.Apellido;
                        txtNombre.Text = consulta.Nombre;
                        btnAceptar.Enabled = true;
                    }
                }
                

                if ( msg != null)
                {
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
            Metodos.openChildForm(consultaGrupo, Metodos.menuForm.plForms);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Negocio negocio = new Negocio();
            RetornoValidacion resultadoOperacion;
            MsgBox msg = new MsgBox("error", "Ha ocurrido un error inesperado a la hora de intentar hacer la operacion, intentelo de nuvo o contacte con un adiministrador del sistema");

            try
            {
                resultadoOperacion = negocio.AgregarAlumnoAGrupo(txtCI.Text, grupoConsulta.Nombre);

                if (resultadoOperacion == RetornoValidacion.OK)
                {
                    msg = new MsgBox("exito", "Se ha agregado el alumno a la lista correctamente");
                    txtCI.Text = "";
                    txtApellido.Text = "";
                    txtNombre.Text = "";
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
