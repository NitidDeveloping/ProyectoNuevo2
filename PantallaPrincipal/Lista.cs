﻿using CapaEntidades;
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
            if (backgroundWorker1.IsBusy != true)
            {
                backgroundWorker1.RunWorkerAsync(); //Si el background worker no esta ocupado entonces empieza la operacion, esto sirve para no sobrecargarlo
            }
        }

        // Botones
        #region
        private void BtnEliminar_Click(object sender, EventArgs e)
        {
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
                        string nombreAlumno = row.Cells["Nombre"].Value.ToString(); ;
                        string apellidoAlumno = row.Cells["Apellido"].Value.ToString(); ;
                        int ciAlumno = (int)row.Cells["CI"].Value; ;

                        //idDestino con el que trabajara la bd (en este caso el CI del alumno)
                        idDestino = row.Cells["CI"].Value.ToString();

                        //Nombre que vera el usuario
                        nombreDestino = row.Cells["CI"].Value.ToString();

                        //Objeto del cual se cargaran los datos en el formulario a la hora de editar
                        objetoDestino = new Alumno(nombreAlumno, apellidoAlumno, ciAlumno);

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
                Type tipo = null; //Typo para decidir que tipo le pasamos al parametro del select en la consulta en la base de datos (Metodo Datos.Listar(referencia, columna, valor, tipo))
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

                        //Transforma el cuarto elemento de la row selecciona en un type y lo asigna al tipo
                        if (selectedRow.Row.ItemArray[3] is Type type)
                        {
                            tipo = type;
                        }
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
                        DataRowView selectedRow = (DataRowView)comboSearch.SelectedItem;

                        if (selectedRow.Row.ItemArray[1] is object obj)
                        {
                            valor = obj;
                        }
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
                    valor = datePickerSearch.Value;
                }
                //Si es el datePicker pasa su fecha
                else if (activeSearchField == timePickerSearch)
                {
                    valor = timePickerSearch.Value;
                }

                e.Result = negocio.Listar(Sesion.ReferenciaActual, columna, valor, tipo);

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

        private void comboColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Setea el campo de busqueda actual segun lo que haya en la tercer celda de la fila seleccionada del comboColum
            if (comboColumn.SelectedIndex != -1)
            {
                DataRowView selectedRow = (DataRowView)comboColumn.SelectedItem;
                if (selectedRow.Row.ItemArray[2] is Control control)
                {
                    SetActiveSearchField(control);
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
            dt.Columns.Add("TipoValor", typeof(Type)); //Tipo del valor que debe enviar, se ingresa con un add de una forma u otra en la capa de Datos dependiendo de este
            dt.Columns.Add("ReferenciaLista", typeof(TipoReferencia)); //Con que se llenara el combosearch si este esta en uso
            return dt;
        }

        //Metodos para agregar filas para las props del combobox de las columnas
        #region
        private DataTable CargarPropsAlumnos()
        {
            DataTable dt = FormatodDataTablePropsColumns();

            dt.Rows.Add("CI", "CI_Alumno", txtSearch, typeof(int));
            dt.Rows.Add("Nombre", "Nombre", txtSearch, typeof(string));
            dt.Rows.Add("Apellido", "Apellido", txtSearch, typeof(string));
            dt.Rows.Add("Grupo(s)", "Grupos", txtSearch, typeof(string));

            return dt;
        }

        private DataTable CargarPropsAnios()
        {
            DataTable dt = FormatodDataTablePropsColumns();

            dt.Rows.Add("Año", "Anio", txtSearch, typeof(int));

            return dt;
        }

        private DataTable CargarPropsDocentes()
        {
            DataTable dt = FormatodDataTablePropsColumns();

            dt.Rows.Add("CI", "CI_Docente", txtSearch, typeof(int));
            dt.Rows.Add("Nombre", "Nombre", txtSearch, typeof(string));
            dt.Rows.Add("Apellido", "Apellido", txtSearch, typeof(string));

            return dt;
        }

        private DataTable CargarPropsFuncionarios()
        {
            //"SELECT Nombre, Apellido, CI_Funcionario, Cargo, Nombre_Cargo, Tipo, Fecha_Ingreso FROM Usuario_Funcionario;";
            DataTable dt = FormatodDataTablePropsColumns();

            dt.Rows.Add("CI", "CI_Funcionario", txtSearch, typeof(int));
            dt.Rows.Add("Nombre", "Nombre", txtSearch, typeof(string));
            dt.Rows.Add("Apellido", "Apellido", txtSearch, typeof(string));
            dt.Rows.Add("Cargo", "Cargo", comboSearch, typeof(byte));
            dt.Rows.Add("Administradores", "Tipo", chkSearch, typeof(bool));
            dt.Rows.Add("Fecha", "Fecha_Ingreso", datePickerSearch, typeof(DateTime));

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

            dt.Rows.Add("Nombre", "ID_Grupo", txtSearch, typeof(string));
            dt.Rows.Add("Año", "Anio", txtSearch, typeof(int));
            dt.Rows.Add("Orientacion", "Orientacion", comboSearch, typeof(byte));
            dt.Rows.Add("Turno", "Turno", comboSearch, typeof(byte));
            dt.Rows.Add("Cantidad de alumnos", "Lista", txtSearch, typeof(byte));

            return dt;
        }

        private DataTable CargarPropsHoras()
        {
            /*"SELECT Horario.ID_Horario, Horario.Turno, Turno.Nombre_Turno, Horario.Hora_Inicio, Horario.Hora_Fin " +
                        "FROM Horario " +
                        "JOIN Turno ON Horario.Turno = Turno.Turno;";*/

            DataTable dt = FormatodDataTablePropsColumns();

            dt.Rows.Add("Numero de Hora", "ID_Horario", txtSearch, typeof(byte));
            dt.Rows.Add("Turno", "Turno", comboSearch, typeof(byte));
            dt.Rows.Add("Inicio", "Hora_Inicio", timePickerSearch, typeof(TimeSpan));
            dt.Rows.Add("Fin", "Hora_Fin", timePickerSearch, typeof(TimeSpan));

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
            /*SELECT 
    Lugar.ID, Lugar.Nombre, Lugar.Tipo, Tipo_lugar.Nombre_Tipo, Lugar.Piso, Lugar.Coordenada_X, Lugar.Coordenada_Y, 
    CASE WHEN Clase.ID_Clase IS NOT NULL THEN true ELSE false END AS AptoParaClase, 
    CASE WHEN UC.ID_UsoComun IS NOT NULL THEN true ELSE false END AS UsoComun, 
    CASE 
        WHEN GMHC.ID_Grupo IS NOT NULL 
            AND (DATE_FORMAT(NOW(), '%H:%i') >= DATE_FORMAT(Horario.Hora_Inicio, '%H:%i') 
                AND DATE_FORMAT(NOW(), '%H:%i') < DATE_FORMAT(Horario.Hora_Fin, '%H:%i')) 
        THEN false 
        ELSE true 
    END AS EstadoOcupacion 
FROM lugar 
JOIN Tipo_Lugar ON Lugar.Tipo = Tipo_Lugar.Tipo 
LEFT JOIN Clase ON Lugar.ID = Clase.ID_Clase
LEFT JOIN Grupo_Materia_Horario_Clase GMHC ON Clase.ID_Clase = GMHC.ID_Clase 
LEFT JOIN Uso_Comun UC ON Lugar.ID = UC.ID_UsoComun 
LEFT JOIN Grupo_Materia_Horario GMH ON GMHC.ID_Grupo = GMH.ID_Grupo 
    AND GMHC.ID_Materia = GMH.ID_Materia 
    AND GMHC.Turno= GMH.Turno
    AND GMHC.ID_Horario = GMH.ID_Horario 
    AND GMHC.Dia_Semana= GMH.Dia_Semana
LEFT JOIN Horario ON GMH.ID_Horario = Horario.ID_Horario
	 AND GMH.Turno = Horario.Turno
GROUP BY 
    Lugar.ID,
    Lugar.Nombre, 
    Lugar.Tipo, 
    Lugar.Piso, 
    Lugar.Coordenada_X, 
    Lugar.Coordenada_Y; */

            DataTable dt = FormatodDataTablePropsColumns();

            dt.Rows.Add("Nombre", "Lugar.Nombre", txtSearch, typeof(string));
            dt.Rows.Add("Tipo", "Lugar.Tipo", comboSearch, typeof(byte));
            dt.Rows.Add("Piso", "Lugar.Piso", txtSearch, typeof(byte));
            dt.Rows.Add("Clases", "AptoParaClase", chkSearch, typeof(bool));
            dt.Rows.Add("De uso comun", "UsoComun", chkSearch, typeof(bool));
            dt.Rows.Add("Ocupado", "EstadoOcupacion", chkSearch, typeof(bool));

            return dt;
        }

        private DataTable CargarPropsMaterias()
        {
            /*SELECT ID_Materia, Nombre FROM Materia;*/

            DataTable dt = FormatodDataTablePropsColumns();

            dt.Rows.Add("Nombre", "Nombre", txtSearch, typeof(string));

            return dt;
        }

        private DataTable CargarPropsOrientaciones()
        {
            /*SELECT Orientacion.Orientacion, Nombre_Orientacion FROM Orientacion;*/

            DataTable dt = FormatodDataTablePropsColumns();

            dt.Rows.Add("Nombre", "Nombre_Orientacion", txtSearch, typeof(string));

            return dt;
        }

        private DataTable CargarPropsTurnos()
        {
            /*SELECT Turno, Nombre_Turno FROM Turno*/

            DataTable dt = FormatodDataTablePropsColumns();

            dt.Rows.Add("Nombre", "Nombre_Turno", txtSearch, typeof(string));

            return dt;
        }
        #endregion
        #endregion

    }
}
