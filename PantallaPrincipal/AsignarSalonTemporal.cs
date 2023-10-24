using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Proyecto
{
    public partial class AsignarSalonTemporal : Form
    {
        Horario horarioDestino;
        public AsignarSalonTemporal(Horario horarioDestino)
        {
            InitializeComponent();
            this.horarioDestino = horarioDestino;
        }

        private void AsignarSalonTemporal_Load(object sender, EventArgs e)
        {
            cbxClase.Items.Clear();
        }



        //Botones
        #region
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Lista lista = new Lista();
            Close();
            Metodos.openChildForm(lista, Metodos.menuForm.plForms);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            MsgBox msg = null;
            Negocio negocio = new Negocio();
            RetornoValidacion resultadoOperacion;
            ushort idSalon;

            if (Validar() == RetornoValidacion.OK)
            {

                idSalon = (ushort)cbxClase.SelectedValue;

                //Verifica si no esta ocupado ya ese salon en el horario pedido
                MensajeSalonOcupado mslo = negocio.ConsultarSalonOcupado(idSalon, horarioDestino.Inicio, horarioDestino.Fin, horarioDestino.Dia.Id);

                //Si el salon esta ocupado muestra un mensaje y pide cofirmacion
                if (mslo != null)
                {
                    MsgBox msgMslo = new MsgBox("pregunta", "Este salón ya se encuentra ocupado por el grupo (" + mslo.NombreGrupo + ") durante el horario (" + mslo.HoraInicio + ") - (" + mslo.HoraFin + ") en el dia (" + mslo.NombreDia + ") con el profesor (" + mslo.NombreDocente + ") ¿Desea continuar de todos modos?");
                    msgMslo.label3.Visible = true;

                    //Si el usuario decide que no quiere continuar se cierra el metodo y no se hace la operacion
                    if (msgMslo.ShowDialog() == DialogResult.No)
                    {
                        return;
                    }

                }

                try
                {
                    resultadoOperacion = negocio.AsignarSalonTemporal(horarioDestino, idSalon);

                    switch (resultadoOperacion)
                    {
                        case RetornoValidacion.OK:
                            //Si la operacion sale bien muestra un mensaje de exito y cierra el formulario
                            msg = new MsgBox("exito", "Salon temporal asignado exitosamente");

                            Lista lista = new Lista();
                            Close();
                            Metodos.openChildForm(lista, Metodos.menuForm.plForms);
                            break;

                        case RetornoValidacion.NoExiste:
                            msg = new MsgBox("error", "No se puede asignar un salon temporal a un horario que no tenga un salon asignado.");
                            break;

                        case RetornoValidacion.ErrorInesperadoBD:
                            //Si algo sale mal muestra un mensaje de que hubo un error inesperado
                            msg = new MsgBox("error", "Parece que algo no ha salido bien, compruebe en la lista si el horario que ingresó se encuentra cargado");
                            break;
                    }
                }
                catch(Exception ex)
                {
                    msg = new MsgBox("error", ex.Message);
                }

                msg?.ShowDialog();
            }
        }


        #endregion

        //BackgroundWorker
        #region
        private void bckgw_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            object[] parametros = (object[])e.Argument;

            //Recibe los parametros
            System.Windows.Forms.ComboBox cbx = (System.Windows.Forms.ComboBox)parametros[0];
            TipoReferencia referencia = (TipoReferencia)parametros[1];
            string columnaFiltroBD = (string)parametros[2];
            string valorFiltro = (string)parametros[3];

            MsgBox msg;
            Negocio negocio = new Negocio();
            try
            {
                DataTable auxdt = negocio.Listar(referencia, columnaFiltroBD, valorFiltro);
                (DataTable rdt, System.Windows.Forms.ComboBox rcbx) resultado = (auxdt, cbx);
                e.Result = resultado;
            }
            catch (Exception ex)
            {
                msg = new MsgBox("error", ex.Message);
                msg.ShowDialog();
            }

        }

        private void bckgw_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            MsgBox msg;
            try
            {
                (DataTable rdt, System.Windows.Forms.ComboBox rcbx) resultado = ((DataTable, System.Windows.Forms.ComboBox))e.Result;
                System.Windows.Forms.ComboBox cbx = resultado.rcbx;
                DataTable dt = resultado.rdt;

                cbx.DataSource = dt;

                string nombre = cbx != cbxClase ? dt.Columns[0].ColumnName : dt.Columns[1].ColumnName;
                string id = dt.Columns[0].ColumnName;
                cbx.DisplayMember = nombre;
                cbx.ValueMember = id;
                cbx.DroppedDown = true;
            }
            catch (Exception ex)
            {
                msg = new MsgBox("error", ex.Message);
                msg.ShowDialog();
            }
        }
        #endregion

        //Habilitacion y cargado de los controles
        #region

        //Metodo para cargar un combobox con elementos despues
        //De que el usuario haya escrito una cantidad de caracteres
        //Usado para el cbx de grupo y clase
        private void CargarCbx(System.Windows.Forms.ComboBox cbx, KeyEventArgs e, TipoReferencia referencia, string columnaFiltroBD, int limitador, string valorFiltro)
        {
            //Si el usuario escribe mas de 3 
            //caracteres en el cbx lo carga con grupos
            //que tengan esos 3 caracteres en su nombre
            cbx.DroppedDown = false;

            if (cbx.DataSource != null)
            {
                cbx.DataSource = null;
            }

            if (cbx.Text.Length >= limitador && e.KeyCode != Keys.Delete)
            {
                if (!bckgw.IsBusy)
                {
                    object[] parametros = { cbx, referencia, columnaFiltroBD, valorFiltro };
                    bckgw.RunWorkerAsync(parametros);
                }

            }
        }

        private void cbxClase_KeyUp(object sender, KeyEventArgs e)
        {
            e.Handled = false;
            CargarCbx(cbxClase, e, TipoReferencia.Clases, "Nombre", 3, cbxClase.Text);
        }

        #endregion

        //Validaciones
        #region
        private RetornoValidacion Validar()
        {

            RetornoValidacion respuesta = RetornoValidacion.OK;
            Validaciones validaciones = new Validaciones();
            MsgBox msg = null;
            string camposobligatorios = "Debe completar los campos olbigatorios, estos estan marcados con un *.";

            if (cbxClase.SelectedValue == null)
            {
                msg = new MsgBox("error", camposobligatorios);
                cbxClase.Focus();
                respuesta = RetornoValidacion.ErrorDeFormato;
            }

            msg?.ShowDialog();

            return respuesta;


        }
        #endregion
    }
}
