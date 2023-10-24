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
    public partial class AgregarHorario : Form
    {

        public AgregarHorario()
        {
            InitializeComponent();
        }

        private void AgregarHorario_Load(object sender, EventArgs e)
        {
            chlbHoras.Items.Clear();
            cbxGrupo.Items.Clear();
            cbxClase.Items.Clear();
            cbxDia.Items.Clear();
            cbxMateria.Items.Clear();
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

            Horario horario;
            Lugar salon = null;

            if (Validar() == RetornoValidacion.OK)
            {
                //Arma el horario
                //Grupo
                string grupo = cbxGrupo.SelectedValue.ToString();

                //Turno
                DataRowView grupoSeleccionado = (DataRowView)cbxGrupo.SelectedItem;
                byte idturno = (byte)grupoSeleccionado.Row.ItemArray[1];
                Turno turno = new Turno(idturno);

                //Materia
                Materia materia = new Materia((ushort)cbxMateria.SelectedValue);

                //Dia
                Dia_Semana dia = new Dia_Semana((byte)cbxDia.SelectedValue);

                //Horas
                List<Hora> horas = new List<Hora>();
                Hora auxHora;
                string strItem;
                string[] fragmentosStr;
                string numeroHora;

                foreach (object item in chlbHoras.CheckedItems)
                {
                    strItem = item.ToString();
                    fragmentosStr = strItem.Split(' ');

                    //Resultado 
                    //0 = idHora
                    //1 = (
                    //2 = inicioHora
                    //3 = -
                    //4 = finalHora
                    //5 = )

                    numeroHora = fragmentosStr[0];
                    TimeSpan inicioHora = TimeSpan.Parse(fragmentosStr[2]);
                    TimeSpan finHora = TimeSpan.Parse(fragmentosStr[4]);

                    auxHora = new Hora((byte.Parse(numeroHora), turno), inicioHora, finHora);
                    horas.Add(auxHora);
                }

                //Salon
                //Si elige un salon valida si esta ocupado primero
                //y segun lo que el usuario decida sigue con la operacion o cancela
                ushort idSalon = (ushort)cbxClase.SelectedValue;

                MensajeSalonOcupado mslo = negocio.ConsultarSalonOcupado(idSalon, horas[0].Inicio, horas[horas.Count - 1].Fin, dia.Id);

                if (mslo != null)
                {
                    //Si se encuentra que el salon esta ocupado muestra un mensaje de confirmacion
                    MsgBox msgMslo = new MsgBox("pregunta", "Este salón ya se encuentra ocupado por el grupo (" + mslo.NombreGrupo + ") durante el horario (" + mslo.HoraInicio + ") - (" + mslo.HoraFin + ") en el dia (" + mslo.NombreDia + ") con el profesor (" + mslo.NombreDocente + ") ¿Desea continuar de todos modos?");
                    msgMslo.label3.Visible = true;

                    //Si el usuario decide que no quiere continuar se cierra el metodo y no se hace la operacion
                    if (msgMslo.ShowDialog() == DialogResult.No)
                    {
                        return;
                    }

                }

                salon = new Lugar((ushort)cbxClase.SelectedValue);


                if (salon != null)
                {
                    horario = new Horario(grupo, materia, dia, salon, horas, turno);
                }
                else
                {
                    horario = new Horario(grupo, materia, dia, horas, turno);
                }

                resultadoOperacion = negocio.AgregarHorario(horario);

                switch (resultadoOperacion)
                {
                    case RetornoValidacion.OK:
                        msg = new MsgBox("exito", "Horario cargado exitosamente");
                        cbxGrupo.SelectedIndex = -1;
                        cbxClase.SelectedIndex = -1;
                        break;

                    case RetornoValidacion.YaExiste:
                        msg = new MsgBox("error", "Ya existe un horario con las caracteristicas que ha intentado ingresar, verifique si no intento cargar dos veces el mismo horario o cargarlo dos veces en la misma hora.");
                        break;

                    case RetornoValidacion.ErrorInesperadoBD:
                        msg = new MsgBox("error", "Parece que algo no ha salido bien, compruebe en la lista si el horario que ingresó se encuentra cargado");
                        break;
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

        private void cbxGrupo_KeyUp(object sender, KeyEventArgs e)
        {
            e.Handled = false;
            CargarCbx(cbxGrupo, e, TipoReferencia.Grupo, "ID_Grupo", 2, cbxGrupo.Text);
        }

        private void cbxGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Negocio negocio = new Negocio();
            if (cbxGrupo.SelectedIndex != -1 && cbxGrupo.SelectedValue != null)
            {
                //Carga el cbx de materias con las materias asignadas al grupo
                //Y lo habilita
                DataTable dtMaterias = negocio.ListarMateriasYDocentes(cbxGrupo.SelectedValue.ToString());
                cbxMateria.DataSource = dtMaterias;
                cbxMateria.SelectedIndex = -1;
                cbxMateria.DisplayMember = dtMaterias.Columns[1].ColumnName;
                cbxMateria.ValueMember = dtMaterias.Columns[0].ColumnName;

                plMateria.Enabled = true;

                //Carga el cbx turno con el turno del grupo
                //seleccionado
                DataRowView selectedRow = (DataRowView)cbxGrupo.SelectedItem;
                cbxTurno.Text = selectedRow.Row.ItemArray[2].ToString();
                plTurno.Enabled = true;
            }
            else
            {
                //Limpia los cbx
                cbxMateria.SelectedIndex = -1;
                cbxMateria.Text = string.Empty;
                plMateria.Enabled = false;

                plTurno.Enabled = false;
                cbxTurno.Text = string.Empty;

            }
        }

        private void cbxMateria_SelectedIndexChanged(object sender, EventArgs e)
        {
            Negocio negocio = new Negocio();
            if (cbxMateria.SelectedIndex != -1 && cbxMateria.SelectedValue != null)
            {
                lblSubMateria.BackColor = Color.FromArgb(178, 8, 55);
                //Carga el txt de docente con el docente
                //asignado a la materia seleccionada
                DataRowView selectedRow = (DataRowView)cbxMateria.SelectedItem;
                txtDocente.Text = selectedRow.Row.ItemArray[3].ToString() + " - " + selectedRow.Row.ItemArray[4].ToString();
                plDocente.Enabled = true;

                //Carga el cbx de dia
                DataTable dtDias = negocio.Listar(TipoReferencia.DiaSemana, null, null);
                cbxDia.DataSource = dtDias;
                cbxDia.DisplayMember = dtDias.Columns[1].ColumnName;
                cbxDia.ValueMember = dtDias.Columns[0].ColumnName;
                cbxDia.SelectedIndex = -1;
                plDia.Enabled = true;
            }
            else
            {
                plDocente.Enabled = false;
                txtDocente.Text = string.Empty;

                plDia.Enabled = false;
                cbxDia.SelectedIndex = -1;
                cbxDia.Text = string.Empty;
            }
        }

        private void cbxDia_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblSubDia.BackColor = Color.FromArgb(178, 8, 55);
            Negocio negocio = new Negocio();
            string auxHora;
            if (cbxDia.SelectedIndex != -1 && cbxGrupo.SelectedValue != null)
            {
                //Carga el chlb de Horas
                DataRowView grupoSeleccionado = (DataRowView)cbxGrupo.SelectedItem; //Recupera el grupo elegido en el cbx de grupo
                string turno = grupoSeleccionado.Row.ItemArray[1].ToString(); //Recupera la id del turno del grupo elegido

                plHoras.Enabled = true;
                chlbHoras.Items.Clear();
                DataTable dtHoras = negocio.Listar(TipoReferencia.Hora, "Turno", turno);

                foreach (DataRow row in dtHoras.Rows)
                {
                    string idHora = row[2].ToString();
                    string inicioHora = row[3].ToString();
                    string finalHora = row[4].ToString();
                    auxHora = idHora + " ( " + inicioHora + " - " + finalHora + " )";
                    chlbHoras.Items.Add(auxHora);
                }
            }
            else
            {
                plHoras.Enabled = false;
                chlbHoras.Items.Clear();
            }
        }

        private void chlbHoras_ItemCheck(object sender, ItemCheckEventArgs e)
        {

            lblSubHoras.BackColor = Color.FromArgb(178, 8, 55);
            int suma;
            if (e.CurrentValue == CheckState.Unchecked)
            {
                suma = 1;
            }
            else
            {
                suma = -1;
            }

            int itemsMarcados = chlbHoras.CheckedItems.Count + suma;

            if (itemsMarcados > 0)
            {
                plClase.Enabled = true;
            }
            else
            {
                plClase.Enabled = false;
                cbxClase.SelectedIndex = -1;
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

            if (cbxGrupo.SelectedValue == null)
            {
                msg = new MsgBox("error", "Debe elegir un grupo");
                cbxGrupo.Focus();
                respuesta = RetornoValidacion.ErrorDeFormato;
            }
            else if (cbxMateria.SelectedValue == null)
            {
                msg = new MsgBox("error", "Debe elegir una materia");
                lblSubMateria.BackColor = Color.ForestGreen;
                respuesta = RetornoValidacion.ErrorDeFormato;
            }
            else if (cbxDia.SelectedValue == null)
            {
                msg = new MsgBox("error", "Debe elegir un dia");
                lblSubDia.BackColor = Color.ForestGreen;
                respuesta = RetornoValidacion.ErrorDeFormato;
            }
            else if (chlbHoras.CheckedItems.Count <= 0)
            {
                msg = new MsgBox("error", "Debe elegir al menos una hora.");
                lblSubHoras.BackColor = Color.ForestGreen;
                respuesta = RetornoValidacion.ErrorDeFormato;
            }
            else if (cbxClase.SelectedValue == null)
            {
                msg = new MsgBox("error", "Debe elegir un salon.");
                lblSubHoras.BackColor = Color.ForestGreen;
                respuesta = RetornoValidacion.ErrorDeFormato;
            }

            msg?.ShowDialog();

            return respuesta;


        }
        #endregion
    }
}
