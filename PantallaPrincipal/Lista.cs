using CapaEntidades;
using CapaNegocio;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Proyecto
{
    public partial class Lista : Form
    {
        public Control activeSearchField = null;
        public Lista()
        {
            InitializeComponent();
        }
        private void Lista_Load(object sender, EventArgs e)
        {
            //Asignamos unas propiedades u otras al combobox para los filtros segun la referencia actual
            switch (Sesion.ReferenciaActual)
            {
                case TipoReferencia.Alumno:
                    comboColumn.DataSource = CargarPropsAlumnos();
                    break;

                case TipoReferencia.Anio:
                    comboColumn.DataSource = CargarPropsAnios();
                    break;

                case TipoReferencia.Docente:
                    comboColumn.DataSource = CargarPropsDocentes();
                    break;

                case TipoReferencia.Funcionario:
                    comboColumn.DataSource = CargarPropsFuncionarios();
                    break;

                case TipoReferencia.Grupo:
                    comboColumn.DataSource = CargarPropsGrupos();
                    break;
                case TipoReferencia.Hora:
                    comboColumn.DataSource = CargarPropsHoras();
                    break;

                case TipoReferencia.Horario:
                    comboColumn.DataSource = CargarPropsHorarios();
                    break;

                case TipoReferencia.Lugar:
                    comboColumn.DataSource = CargarPropsLugares();
                    break;

                case TipoReferencia.Materia:
                    comboColumn.DataSource = CargarPropsMaterias();
                    break;

                case TipoReferencia.Orientacion:
                    comboColumn.DataSource = CargarPropsOrientaciones();
                    break;

                case TipoReferencia.Turno:
                    comboColumn.DataSource = CargarPropsTurnos();
                    break;
            }
            comboColumn.DisplayMember = "Columna";
            comboColumn.ValueMember = "ColumnaBD";
            comboColumn.SelectedIndex = -1;

            //Seteamos los formatos personalizados del datepicker y el timepicker

            datePickerSearch.CustomFormat = "yyyy-MM-dd"; //Anio de 4 digitos - Mes de dos digitos (01/12), dia de dos digitos (01/31)
            timePickerSearch.CustomFormat = "HH:mm"; //Hora en formato 24 horas de dos digitos (00/23), minutos de dos digitos (01/59)

            if (backgroundWorker1.IsBusy != true)
            {
                backgroundWorker1.RunWorkerAsync(); //Si el background worker no esta ocupado entonces empieza la operacion, esto sirve para no sobrecargarlo
            }
        }

        // Botones
        #region
        private void BtnEliminar_Click(object sender, EventArgs e)
        {
           /* RetornoValidacion respuesta;
            string tipo id;

            switch (Sesion.ReferenciaActual)
            {
                case TipoReferencia.Alumno:
                case TipoReferencia.Docente:
                case TipoReferencia.Funcionario:
                    
                    break;
            }

            string id = DGV.SelectedRows[0].Cells["CI"].Value.ToString();
            MsgBox confirm = new MsgBox("pregunta", "Se eliminará al alumno ¿Está seguro que desea continuar?.");

            if (confirm.ShowDialog() == DialogResult.Yes)
            {
                respuesta = Negocio_alumno.Borrar(int.Parse(ci));
                if (respuesta.Equals("OK"))
                {
                    MsgBox msg = new MsgBox("exito", "Alumno eliminado correctamente.");
                    msg.ShowDialog();
                    this.Listar();
                }
                else if (respuesta.Equals("Error"))
                {
                    MsgBox msg = new MsgBox("error", "No se ha podido eliminar.");
                    msg.ShowDialog();
                }
            }*/
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            //Variables que enviaremos al formulario agregarEditar
            string idDestino; //idDestino del objeto sobre el que trabajaremos en la bd
            object objetoDestino; //Objeto que contiene la informacion sobre el objeto que vamos a editar en la base de datos
            string nombreDestino; // Nombre que mostraremos en el lblDestino para que el usuario lo vea, no debe ser id si el id es un numero que el usuario no tiene porque saber (ej:Id_materia)

            //Evaluamos si el usuario selecciono una linea del DGV
            //Si lo hizo abrimos el AgregarEditar con los valores correspondientes a lo que haya seleccionado
            //Sino mostramos un mensaje diciendo que debe seleccionar una linea de la lista
            if (DGV.SelectedRows.Count > 0)
            {
                //Instanciamos la linea que selecciono el usuario para trabajar con ella
                DataGridViewRow row = DGV.SelectedRows[0];

                //Segun la referencia actual en la seseion armamos los objetos con unos campos u otros de el dgv
                switch (Sesion.ReferenciaActual)
                {
                    case TipoReferencia.Alumno:

                        //Variables que usaremos para crear el objetoDestino (en este caso de la clase Alumno)
                        string nombreAlumno = row.Cells["Nombre"].Value.ToString();
                        string apellidoAlumno = row.Cells["Apellido"].Value.ToString();
                        int ciAlumno = (int)row.Cells["CI"].Value;

                        //idDestino con el que trabajara la bd (en este caso el CI del alumno)
                        idDestino = row.Cells["CI"].Value.ToString();

                        //Nombre que vera el usuario
                        nombreDestino = row.Cells["CI"].Value.ToString();

                        //Objeto del cual se cargaran los datos en el formulario a la hora de editar
                        objetoDestino = new Alumno(nombreAlumno, apellidoAlumno, ciAlumno);

                        /* ------- Para saber a que nombres de las columnas referirse ir a Negocios Listar(), ahi es donde se arman los datatable ------------ */
                        break;

                    case TipoReferencia.Turno:

                        //Variables que usaremos para crear el objetoDestino (en este caso de la clase Alumno)
                        string nombreTurno = row.Cells["Nombre"].Value.ToString();

                        //idDestino con el que trabajara la bd (en este caso el CI del alumno)
                        idDestino = row.Cells["Id"].Value.ToString();

                        //Nombre que vera el usuario
                        nombreDestino = row.Cells["Nombre"].Value.ToString();

                        //Objeto del cual se cargaran los datos en el formulario a la hora de editar
                        objetoDestino = new Turno (nombreTurno);

                        /* ------- Para saber a que nombres de las columnas referirse ir a Negocios Listar(), ahi es donde se arman los datatable ------------ */
                        break;

                    default: throw new Exception("No implementado, switch btnEditar_Click en Lista capa Presentacion");
                }
                //Abrimos el formulario agregar editar con los argumentos que hemos fabricado arriba
                AbrirAgregarEditar(objetoDestino, idDestino, nombreDestino);
            }
            else
            {
                //Mensaje de error
                MsgBox msg = new MsgBox("error", "Debe seleccionar una fila de la lista para editar. Para ello haga click en el cuadrado al principio de cada fila de la lista");
                msg.ShowDialog();
            }

        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            AbrirAgregarEditar();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy != true)
            {
                backgroundWorker1.RunWorkerAsync(); //Si el background worker no esta ocupado entonces empieza la operacion, esto sirve para no sobrecargarlo
            }
        }

        #endregion


        //BackgroundWorker
        #region
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //Consulta de bd para la cual se agregan parametros
                // (...) WHERE @Columna LIKE @Valor

                Negocio negocio = new Negocio(); //Instancia para ejecutar metodo Listar en la clase negocio
                string columna = null; //String para almacenar por que columna queremos buscar en la base de datos
                object valor = null; //Valor seleccionado en searchField actual para buscar en la consulta de bd


                //Asignamos los valores invocando de manera segura la informacion del comboColumn
                comboColumn.Invoke((MethodInvoker)(() =>
                {
                    if (comboColumn.SelectedIndex != -1)
                    {
                        //Asigna valor de la columna de bd. Seteada como value en el evento de comboColumn_SelectedIndexChanged()
                        columna = comboColumn.SelectedValue.ToString();

                        //Variabla auxiliar para recuperar el roy seleccionado actualmente en el comboColum
                        DataRowView selectedRow = (DataRowView)comboColumn.SelectedItem;
                    }
                }));

                if (activeSearchField == null)
                {
                    valor = null;
                }
                else if (activeSearchField == chkSearch)
                {
                    valor = chkSearch.Checked ? 1 : 0;
                }
                //Si es el combobox pasa su valor
                else if (activeSearchField == comboSearch)
                {
                    comboSearch.Invoke((MethodInvoker)(() =>
                    {
                        valor = comboSearch.SelectedValue;
                    }));
                }
                //Si es el textbox de busqueda pasa su texto
                else if (activeSearchField == txtSearch)
                {
                    valor = txtSearch.Text;
                }
                //Si es el datePicker pasa su fecha
                else if (activeSearchField == datePickerSearch)
                {
                    valor = datePickerSearch.Text;
                }
                //Si es el datePicker pasa su fecha
                else if (activeSearchField == timePickerSearch)
                {
                    valor = timePickerSearch.Text;
                }

                e.Result = negocio.Listar(Sesion.ReferenciaActual, columna, valor);

             }
             catch (Exception ex)
             {
                MsgBox msg = new MsgBox("error", ex.ToString());
                msg.ShowDialog();
             }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Si la operación en segundo plano se completó con éxito, actualiza la interfaz de usuario.
            DGV.DataSource = (DataTable)e.Result;
        }
        #endregion

        private void SetActiveSearchField(Control control)
        {
            if (comboSearch.Visible)
            {
                comboSearch.Visible = false;
            }
            if (chkSearch.Visible)
            {
                chkSearch.Visible = false;
            }
            if (txtSearch.Visible)
            {
                txtSearch.Visible = false;
            }
            if (datePickerSearch.Visible)
            {
                datePickerSearch.Visible = false;
            }

            if (timePickerSearch.Visible)
            {
                timePickerSearch.Visible = false;
            }

            if (control != null)
            {
                control.Visible = true;
                activeSearchField = control;
            }
            else
            {
                activeSearchField = null;
            }
        }

        private void AbrirAgregarEditar()
        {
            AgregarEditar agregarEditar = new AgregarEditar();
            Metodos.openChildForm(agregarEditar, Metodos.menuForm.plForms);
        }

        private void AbrirAgregarEditar(object ObjetoDestino, string IdDestino, string NombreDestino)
        {
            AgregarEditar agregarEditar = new AgregarEditar
            {
                IdDestino = IdDestino,
                ObjetoDestino = ObjetoDestino,
                NombreDestino = NombreDestino
            };
            Metodos.openChildForm(agregarEditar, Metodos.menuForm.plForms);
        }

        private void AbrirAgregarEditar(object ObjetoDestino, string IdDestino, string NombreDestino, string IdPadre)
        {
            AgregarEditar agregarEditar = new AgregarEditar
            {
                IdDestino = IdDestino,
                ObjetoDestino = ObjetoDestino,
                NombreDestino = NombreDestino,
                IdPadre = IdPadre;
            };
            Metodos.openChildForm(agregarEditar, Metodos.menuForm.plForms);
        }

        private void comboColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Setea el campo de busqueda actual segun lo que haya en la tercer celda de la fila seleccionada del comboColum
            if (comboColumn.SelectedIndex != -1)
            {
                DataRowView selectedRow = (DataRowView)comboColumn.SelectedItem;
                if (selectedRow.Row.ItemArray[2] is Control control)
                {
                    SetActiveSearchField(control);

                    if (control == comboSearch)
                    {
                        Negocio negocio = new Negocio();
                        TipoReferencia referencia = (TipoReferencia)selectedRow.Row.ItemArray[3];
                        DataTable dt = negocio.Listar(referencia, null, null);

                        string nombre = dt.Columns[1].ColumnName;
                        string id = dt.Columns[0].ColumnName;

                        comboSearch.DataSource = dt;
                        comboSearch.DisplayMember = nombre;
                        comboSearch.ValueMember = id;

                    }
                }
                btnSearch.Enabled = true;
            }
            else
            {
                SetActiveSearchField(null);
                btnSearch.Enabled = false;
            }
        }

        //Metodos para cargar el combobox de columna segun la referencia actual
        #region

        //Metodo para especificar el formato de datatable que usan todas las datatable para el combobox de las columnas
        private DataTable FormatodDataTablePropsColumns()
        {
            DataTable dt = new DataTable(); 
            dt.Columns.Add("Columna", typeof(string)); //Nombre que vera el usuario
            dt.Columns.Add("ColumnaBD", typeof(string)); //Nombre de la columna que recibira la bd
            dt.Columns.Add("TipoControl", typeof(Control)); //Tipo de campo de busqueda que se usara para seleccionar el valor
            dt.Columns.Add("ReferenciaLista", typeof(TipoReferencia)); //Con que se llenara el combosearch si este esta en uso
            return dt;
        }

        //Metodos para agregar filas para las props del combobox de las columnas
        #region
        private DataTable CargarPropsAlumnos()
        {
            DataTable dt = FormatodDataTablePropsColumns();

            dt.Rows.Add("CI", "CI_Alumno", txtSearch, null);
            dt.Rows.Add("Nombre", "Nombre", txtSearch, null);
            dt.Rows.Add("Apellido", "Apellido", txtSearch, null);
            dt.Rows.Add("Grupo(s)", "Grupos", txtSearch, null);

            return dt;
        }

        private DataTable CargarPropsAnios()
        {
            DataTable dt = FormatodDataTablePropsColumns();

            dt.Rows.Add("Año", "Anio", txtSearch, null);

            return dt;
        }

        private DataTable CargarPropsDocentes()
        {
            DataTable dt = FormatodDataTablePropsColumns();

            dt.Rows.Add("CI", "CI_Docente", txtSearch, null);
            dt.Rows.Add("Nombre", "Nombre", txtSearch, null);
            dt.Rows.Add("Apellido", "Apellido", txtSearch, null);

            return dt;
        }

        private DataTable CargarPropsFuncionarios()
        {
            //"SELECT Nombre, Apellido, CI_Funcionario, Cargo, Nombre_Cargo, Tipo, Fecha_Ingreso FROM Usuario_Funcionario;";
            DataTable dt = FormatodDataTablePropsColumns();

            dt.Rows.Add("CI", "CI_Funcionario", txtSearch, null);
            dt.Rows.Add("Nombre", "Nombre", txtSearch, null);
            dt.Rows.Add("Apellido", "Apellido", txtSearch, null);
            dt.Rows.Add("Cargo", "Cargo", comboSearch, TipoReferencia.CargosFuncionarios);
            dt.Rows.Add("Administradores", "Tipo", chkSearch, null);
            dt.Rows.Add("Fecha", "Fecha_Ingreso", datePickerSearch, null);

            return dt;
        }

        private DataTable CargarPropsGrupos()
        {
            /*"SELECT Grupo.ID_Grupo, Grupo.Anio, Grupo.Orientacion, Orientacion.Nombre_Orientacion, Grupo.Turno, Turno.Nombre_Turno, COALESCE(Lista, 0) AS Lista " +
            "FROM grupo " +
            "INNER JOIN Turno ON Grupo.Turno = Turno.Turno " +
            "INNER JOIN Orientacion ON Grupo.Orientacion=Orientacion.Orientacion " +
            "LEFT JOIN (" +
            "SELECT ID_Grupo, " +
            "COUNT(CI_Alumno) AS Lista " +
            "FROM grupo_alumno GROUP BY ID_Grupo) AS Subconsulta ON grupo.ID_Grupo = Subconsulta.ID_Grupo;"*/

            DataTable dt = FormatodDataTablePropsColumns();

            dt.Rows.Add("Nombre", "ID_Grupo", txtSearch, null);
            dt.Rows.Add("Año", "Anio", txtSearch, null);
            dt.Rows.Add("Orientacion", "Orientacion", comboSearch, TipoReferencia.Orientacion);
            dt.Rows.Add("Turno", "Turno", comboSearch, TipoReferencia.Turno);
            dt.Rows.Add("Cantidad de alumnos", "Lista", txtSearch, null);

            return dt;
        }

        private DataTable CargarPropsHoras()
        {
            /*"SELECT Horario.ID_Horario, Horario.Turno, Turno.Nombre_Turno, Horario.Hora_Inicio, Horario.Hora_Fin " +
                        "FROM Horario " +
                        "JOIN Turno ON Horario.Turno = Turno.Turno;";*/

            DataTable dt = FormatodDataTablePropsColumns();

            dt.Rows.Add("Numero de Hora", "ID_Horario", txtSearch, null);
            dt.Rows.Add("Turno", "Turno", comboSearch, TipoReferencia.Turno);
            dt.Rows.Add("Inicio", "Hora_Inicio", timePickerSearch, null);
            dt.Rows.Add("Fin", "Hora_Fin", timePickerSearch, null);

            return dt;
        }

        private DataTable CargarPropsHorarios()
        {
            /**/

            DataTable dt = FormatodDataTablePropsColumns();
            return dt;
        }

        private DataTable CargarPropsLugares()
        {
            DataTable dt = FormatodDataTablePropsColumns();

            dt.Rows.Add("Nombre", "Nombre", txtSearch, null);
            dt.Rows.Add("Tipo", "Tipo", comboSearch, TipoReferencia.TipoDeLugar);
            dt.Rows.Add("Piso", "Piso", txtSearch, null);
            dt.Rows.Add("Clases", "AptoParaClase", chkSearch, null);
            dt.Rows.Add("De uso comun", "UsoComun", chkSearch, null);
            dt.Rows.Add("Ocupado", "EstadoOcupacion", chkSearch, null);

            return dt;
        }

        private DataTable CargarPropsMaterias()
        {
            /*SELECT ID_Materia, Nombre FROM Materia;*/

            DataTable dt = FormatodDataTablePropsColumns();

            dt.Rows.Add("Nombre", "Nombre", txtSearch, null);

            return dt;
        }

        private DataTable CargarPropsOrientaciones()
        {
            /*SELECT Orientacion.Orientacion, Nombre_Orientacion FROM Orientacion;*/

            DataTable dt = FormatodDataTablePropsColumns();

            dt.Rows.Add("Nombre", "Nombre_Orientacion", txtSearch, null);

            return dt;
        }

        private DataTable CargarPropsTurnos()
        {
            /*SELECT Turno, Nombre_Turno FROM Turno*/

            DataTable dt = FormatodDataTablePropsColumns();

            dt.Rows.Add("Nombre", "Nombre_Turno", txtSearch, null);

            return dt;
        }
        #endregion

        #endregion

        private void DGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = DGV.Rows[e.RowIndex];
            MsgBox msg = new MsgBox("exito", selectedRow.Cells["CI"].Value.ToString());
            msg.ShowDialog();
        }
    }
}
