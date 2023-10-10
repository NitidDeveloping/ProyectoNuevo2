using CapaEntidades;
using CapaNegocio;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Proyecto
{
    public partial class Lista : Form
    {
        public Control activeSearchField;
        public Lista()
        {
            InitializeComponent();
        }
        private void Lista_Load(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy != true)
            {
                backgroundWorker1.RunWorkerAsync(); //Si el background worker no esta ocupado entonces empieza la operacion, esto sirve para no sobrecargarlo
            }

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
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Negocio negocio = new Negocio();

                if (activeSearchField == null)
                {
                    e.Result = negocio.Listar(Sesion.ReferenciaActual, null, null);
                }
                else if (activeSearchField == chkSearch)
                {
                    e.Result = chkSearch.Checked ? negocio.Listar(Sesion.ReferenciaActual, comboColumn.SelectedText, true) : negocio.Listar(Sesion.ReferenciaActual, comboColumn.SelectedText, false);
                }
                //Si es el combobox pasa su valor
                else if (activeSearchField == comboSearch)
                {
                    e.Result = negocio.Listar(Sesion.ReferenciaActual, comboColumn.SelectedText, comboSearch.SelectedItem);
                }
                //Si es el textbox de busqueda pasa su texto
                else if (activeSearchField == txtSearch)
                {
                    e.Result = negocio.Listar(Sesion.ReferenciaActual, comboColumn.SelectedText, txtSearch.Text);
                }


            }
            catch (Exception ex)
            {
                MsgBox msg = new MsgBox("error", ex.ToString());
                msg.ShowDialog();
            }
        }

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

            control.Visible = true;
            activeSearchField = control;
        }


        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Si la operación en segundo plano se completó con éxito, actualiza la interfaz de usuario.
            DGV.DataSource = (DataTable)e.Result;
        }

        private void comboColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Setea el campo de busqueda actual segun lo que haya en la tercer celda de la fila seleccionada del comboColum
            DataRowView selectedRow = (DataRowView)comboColumn.SelectedItem;
            if (selectedRow.Row.ItemArray[2] is Control control)
            {
                SetActiveSearchField(control);
            }
            btnSearch.Enabled = true;

        }

        //Metodos para cargar el combobox de columna segun la referencia actual
        #region
        private DataTable CargarPropsAlumnos()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Columna", typeof(string));
            dt.Columns.Add("ColumnaBD", typeof(string));
            dt.Columns.Add("TipoControl", typeof(Control));

            dt.Rows.Add("CI", "CI_Alumno", txtSearch);
            dt.Rows.Add("Nombre", "Nombre", txtSearch);
            dt.Rows.Add("Apellido", "Apellido", txtSearch);
            dt.Rows.Add("Grupo(s)", "Grupo", comboSearch);

            return dt;
        }

        private DataTable CargarPropsAnios()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Columna", typeof(string));
            dt.Columns.Add("ColumnaBD", typeof(string));
            dt.Columns.Add("TipoControl", typeof(Control));

            dt.Rows.Add("Año", "Anio", txtSearch);

            return dt;
        }

        private DataTable CargarPropsDocentes()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Columna", typeof(string));
            dt.Columns.Add("ColumnaBD", typeof(string));
            dt.Columns.Add("TipoControl", typeof(Control));

            dt.Rows.Add("CI", "CI_Docente", txtSearch);
            dt.Rows.Add("Nombre", "Nombre", txtSearch);
            dt.Rows.Add("Apellido", "Apellido", txtSearch);

            return dt;
        }

        private DataTable CargarPropsFuncionarios()
        {
            //"SELECT Nombre, Apellido, CI_Funcionario, Cargo, Nombre_Cargo, Tipo, Fecha_Ingreso FROM Usuario_Funcionario;";
            DataTable dt = new DataTable();
            dt.Columns.Add("Columna", typeof(string));
            dt.Columns.Add("ColumnaBD", typeof(string));
            dt.Columns.Add("TipoControl", typeof(Control));

            dt.Rows.Add("CI", "CI_Funcionario", txtSearch);
            dt.Rows.Add("Nombre", "Nombre", txtSearch);
            dt.Rows.Add("Apellido", "Apellido", txtSearch);
            dt.Rows.Add("Cargo", "Nombre_Cargo", comboSearch);
            dt.Rows.Add("Administradores", "Tipo", chkSearch);
            dt.Rows.Add("Fecha", "Fecha_Ingreso", datePickerSearch);

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

            DataTable dt = new DataTable();
            dt.Columns.Add("Columna", typeof(string));
            dt.Columns.Add("ColumnaBD", typeof(string));
            dt.Columns.Add("TipoControl", typeof(Control));

            dt.Rows.Add("Nombre", "ID_Grupo", txtSearch);
            dt.Rows.Add("Año", "Anio", txtSearch);
            dt.Rows.Add("Orientacion", "Orientacion", comboSearch);
            dt.Rows.Add("Turno", "Turno", comboSearch);
            dt.Rows.Add("Cantidad de alumnos", "Lista", txtSearch);

            return dt;
        }

        private DataTable CargarPropsHoras()
        {
            /*"SELECT Horario.ID_Horario, Horario.Turno, Turno.Nombre_Turno, Horario.Hora_Inicio, Horario.Hora_Fin " +
                        "FROM Horario " +
                        "JOIN Turno ON Horario.Turno = Turno.Turno;";*/

            DataTable dt = new DataTable();
            dt.Columns.Add("Columna", typeof(string));
            dt.Columns.Add("ColumnaBD", typeof(string));
            dt.Columns.Add("TipoControl", typeof(Control));

            dt.Rows.Add("Numero de Hora", "ID_Horario", txtSearch);
            dt.Rows.Add("Turno", "Turno", comboSearch);
            dt.Rows.Add("Inicio", "Hora_Inicio", timePickerSearch);
            dt.Rows.Add("Fin", "Hora_Fin", timePickerSearch);

            return dt;
        }

        private DataTable CargarPropsHorarios()
        {
            /**/

            DataTable dt = new DataTable();
            dt.Columns.Add("Columna", typeof(string));
            dt.Columns.Add("ColumnaBD", typeof(string));
            dt.Columns.Add("TipoControl", typeof(Control));

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

            DataTable dt = new DataTable();
            dt.Columns.Add("Columna", typeof(string));
            dt.Columns.Add("ColumnaBD", typeof(string));
            dt.Columns.Add("TipoControl", typeof(Control));

            dt.Rows.Add("Nombre", "Nombre", txtSearch);
            dt.Rows.Add("Tipo", "Lugar.Tipo", comboSearch);
            dt.Rows.Add("Piso", "Lugar.Piso", txtSearch);
            dt.Rows.Add("Clases", "AptoParaClase", chkSearch);
            dt.Rows.Add("De uso comun", "UsoComun", chkSearch);
            dt.Rows.Add("Ocupado", "EstadoOcupacion", chkSearch);

            return dt;
        }

        private DataTable CargarPropsMaterias()
        {
            /*SELECT ID_Materia, Nombre FROM Materia;*/

            DataTable dt = new DataTable();
            dt.Columns.Add("Columna", typeof(string));
            dt.Columns.Add("ColumnaBD", typeof(string));
            dt.Columns.Add("TipoControl", typeof(Control));

            dt.Rows.Add("Nombre", "Nombre", txtSearch);

            return dt;
        }

        private DataTable CargarPropsOrientaciones()
        {
            /*SELECT Orientacion.Orientacion, Nombre_Orientacion FROM Orientacion;*/

            DataTable dt = new DataTable();
            dt.Columns.Add("Columna", typeof(string));
            dt.Columns.Add("ColumnaBD", typeof(string));
            dt.Columns.Add("TipoControl", typeof(Control));

            dt.Rows.Add("Nombre", "Nombre_Orientacion", txtSearch);

            return dt;
        }

        private DataTable CargarPropsTurnos()
        {
            /*SELECT Turno, Nombre_Turno FROM Turno*/

            DataTable dt = new DataTable();
            dt.Columns.Add("Columna", typeof(string));
            dt.Columns.Add("ColumnaBD", typeof(string));
            dt.Columns.Add("TipoControl", typeof(Control));

            dt.Rows.Add("Nombre", "Nombre_Turno", txtSearch);

            return dt;
        }

        #endregion
    }
}
