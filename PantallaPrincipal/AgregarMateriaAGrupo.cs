using CapaEntidades;
using CapaNegocio;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AulaGO
{
    public partial class AgregarMateriaAGrupo : Form
    {
        private Grupo grupoConsulta;

        public AgregarMateriaAGrupo(Grupo grupoConsulta)
        {
            InitializeComponent();
            this.grupoConsulta = grupoConsulta;
        }

        private void AgregarMateriaAGrupo_Load(object sender, EventArgs e)
        {
            Negocio negocio = new Negocio();
            //Setea los labels con los valores del grupo
            lblId.Text = grupoConsulta.Nombre;

            cbxMateria.DataSource = negocio.Listar(TipoReferencia.Materia, null, null);
            cbxMateria.DisplayMember = "Nombre";
            cbxMateria.ValueMember = "Id";

            cbxMateria.SelectedIndex = -1;
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
            MsgBox msg = new MsgBox("error", "Ha ocurrido un error inesperado a la hora de intentar hacer la operacion, intentelo de nuevo o contacte con un adiministrador del sistema");
            ushort idMateriaSeleccionada = (ushort)cbxMateria.SelectedValue;
            string nombreGrupo = grupoConsulta.Nombre;

            try
            {
                //Si no hay una materia seleccionada muestra un mensaje de que debe seleccionar una
                if (cbxMateria.SelectedIndex == -1)
                {
                    msg = new MsgBox("error", "Debe selecciona una materia antes de continuar");
                    cbxMateria.Focus();
                }
                //Si no realiza la operacion
                else
                {

                    resultadoOperacion = negocio.AgregarMateriaAGrupo(idMateriaSeleccionada, nombreGrupo); //Realiza operacion

                    //Muestra un mensaje u otro segun el resultado de la operacion
                    switch (resultadoOperacion)
                    {
                        case RetornoValidacion.OK:
                            msg = new MsgBox("exito", "Se ha agregado la materia a la lista del grupo correctamente");
                            cbxMateria.SelectedIndex = -1;
                            break;

                        case RetornoValidacion.ErrorInesperadoBD:
                            msg = new MsgBox("error", "Ha ocurrido un error inesperado a la hora de ingresar el valor a la base de datos, intentelo de nuevo o contacte con un administrador del sistema");
                            break;

                        case RetornoValidacion.YaExiste:
                            msg = new MsgBox("error", "Esta materia ya ha sido asignada a este grupo");
                            break;

                        default: throw new Exception("No se esperaba este tipo de retorno validacion");

                    }

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

        private void cbxMateria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxMateria.SelectedIndex != -1)
            {
                btnAceptar.Enabled = true;
            }
            else
            {
                btnAceptar.Enabled=false;
            }
        }
    }
}
