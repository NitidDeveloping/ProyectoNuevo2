using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

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
                    btnEditar.Visible = false; //Si la referencia actual es anio se pone en no visible
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
                    btnEditar.Visible = false;
                    btnAsignarSalonTemporal.Visible = true;
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
            if (DGV.SelectedRows.Count <= 0)
            {
                MsgBox error = new MsgBox("error", "Debe seleccionar un elemento de la lista para eliminar");
                error.ShowDialog();
                return;
            }
            RetornoValidacion respuesta; //Resultado de la operacion
            string id; // Id del objeto que se va a pasar al metodo eliminar
            string idPadre = null; // Id padre en caso de que sea una entidad con debilidad  (hora)
            string tipoId = string.Empty; // Nombre de la columna por el que se va a buscar el id en el datatable
            string tipoIdPadre = null; // Nombre de la columna para el id padre

            Negocio negocio = new Negocio();
            MsgBox confirm; //Solicita confirmacion antes de eliminar

            MsgBox msg; //Muestra un mensaje de exito o error
            if (Sesion.ReferenciaActual != TipoReferencia.Horario)
            {
                //Asignacion de nombre de tipoId segun la referencia actual
                switch (Sesion.ReferenciaActual)
                {
                    case TipoReferencia.Alumno:
                    case TipoReferencia.Docente:
                    case TipoReferencia.Funcionario:
                        tipoId = "CI";
                        break;

                    case TipoReferencia.Anio:
                        tipoId = "Anio";
                        break;

                    case TipoReferencia.Grupo:
                        tipoId = "Nombre";
                        break;

                    case TipoReferencia.Hora:
                        tipoId = "Numero";
                        tipoIdPadre = "ID_Turno";
                        break;

                    case TipoReferencia.Lugar:
                        tipoId = "ID_Lugar";
                        break;

                    case TipoReferencia.Materia:
                    case TipoReferencia.Orientacion:
                    case TipoReferencia.TipoDeLugar:
                    case TipoReferencia.Turno:
                        tipoId = "Id";
                        break;
                }

                //En caso de que se haya asginado algo al tipo id padre se hacen operaciones para entidades con debilidad
                if (tipoIdPadre != null)
                {
                    idPadre = DGV.SelectedRows[0].Cells[tipoIdPadre].Value.ToString();
                }
                id = DGV.SelectedRows[0].Cells[tipoId].Value.ToString();

                confirm = new MsgBox("pregunta", "Se eliminará el elemento ¿Está seguro que desea continuar?.");
                confirm.label3.Visible = true;

                if (confirm.ShowDialog() == DialogResult.Yes)
                {
                    try
                    {

                        if (tipoIdPadre != null)
                        {
                            respuesta = negocio.Eliminar(Sesion.ReferenciaActual, byte.Parse(id), byte.Parse(idPadre));
                        }
                        else
                        {
                            respuesta = negocio.Eliminar(Sesion.ReferenciaActual, id);
                        }

                        if (respuesta == RetornoValidacion.OK)
                        {
                            msg = new MsgBox("exito", "Elemento eliminado correctamente.");
                            msg.ShowDialog();
                            backgroundWorker1.RunWorkerAsync();
                        }
                        else if (respuesta == RetornoValidacion.NoExiste)
                        {
                            msg = new MsgBox("error", "No se ha podido encontrar el elemento en la base de datos.");
                            msg.ShowDialog();
                        }
                        else if (respuesta == RetornoValidacion.ErrorInesperadoBD)
                        {
                            msg = new MsgBox("error", "Ha surgido un error inesperado, intente de nuevo, en caso de que el problema persista contacte con un tecnico.");
                            msg.ShowDialog();
                        }
                        else if (respuesta == RetornoValidacion.ErrorInesperadoBDCategorizacion)
                        {
                            msg = new MsgBox("error", "Ha surgido un error inesperado al intentar eliminar al " + Sesion.ReferenciaActual + ", intente de nuevo o contacte con un administrador");
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
            //Eliminar horario
            else
            {
                if (DGV.SelectedRows.Count > 0)
                {
                    //Ensamblado del objeto horario
                    #region
                    Horario horarioDestino;
                    DataGridViewRow row = DGV.SelectedRows[0];

                    //Turno
                    byte idTurno = (byte)row.Cells["IdTurno"].Value;
                    Turno turno = new Turno(idTurno);

                    //Grupo
                    string idGrupo = row.Cells["Grupo"].Value.ToString();

                    //Materia
                    ushort idMateria = (ushort)row.Cells["IdMateria"].Value;
                    Materia materia = new Materia(idMateria);

                    //Horas
                    List<Hora> horas = new List<Hora>();
                    Hora auxHora;
                    string strHoras = row.Cells["Horas"].Value.ToString();
                    string[] fragmentosStr = strHoras.Split(',');

                    foreach (string nHora in fragmentosStr)
                    {
                        auxHora = new Hora((byte.Parse(nHora), turno));
                        horas.Add(auxHora);
                    }

                    //Inicio y fin
                    TimeSpan inicio = (TimeSpan)row.Cells["Inicio"].Value;
                    TimeSpan fin = (TimeSpan)row.Cells["Fin"].Value;

                    //Dia Semana
                    byte idDia = (byte)row.Cells["IdDiaSemana"].Value;
                    Dia_Semana dia = new Dia_Semana(idDia);

                    //Ensamblado de horario
                    horarioDestino = new Horario(idGrupo, materia, dia, horas, turno, inicio, fin);
                    #endregion

                    confirm = new MsgBox("pregunta", "Se eliminará el horario ¿Está seguro que desea continuar?.");
                    confirm.label3.Visible = true;

                    if (confirm.ShowDialog() == DialogResult.Yes)
                    {
                        try
                        {

                            respuesta = negocio.EliminarHorario(horarioDestino);

                            if (respuesta == RetornoValidacion.OK)
                            {
                                msg = new MsgBox("exito", "Elemento eliminado correctamente.");
                                msg.ShowDialog();
                                backgroundWorker1.RunWorkerAsync();
                            }
                            else if (respuesta == RetornoValidacion.NoExiste)
                            {
                                msg = new MsgBox("error", "No se ha podido encontrar el elemento en la base de datos.");
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
            }

        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            //Variables que enviaremos al formulario agregarEditar
            string idDestino; //idDestino del objeto sobre el que trabajaremos en la bd
            object objetoDestino; //Objeto que contiene la informacion sobre el objeto que vamos a editar en la base de datos
            string nombreDestino; // Nombre que mostraremos en el lblDestino para que el usuario lo vea, no debe ser id si el id es un numero que el usuario no tiene porque saber (ej:Id_materia)
            string idPadre = null;

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

                        string nombreTurno = row.Cells["Nombre"].Value.ToString();

                        idDestino = row.Cells["Id"].Value.ToString();

                        nombreDestino = row.Cells["Nombre"].Value.ToString();

                        objetoDestino = new Turno(nombreTurno);

                        break;

                    case TipoReferencia.Materia:

                        string nombreMateria = row.Cells["Nombre"].Value.ToString();

                        idDestino = row.Cells["Id"].Value.ToString();

                        nombreDestino = row.Cells["Nombre"].Value.ToString();

                        objetoDestino = new Materia(nombreMateria);

                        break;

                    case TipoReferencia.Grupo:

                        string nombre = row.Cells["Nombre"].Value.ToString();

                        byte idTurno = (byte)row.Cells["ID_Turno"].Value;
                        byte idOrientacion = (byte)row.Cells["ID_Orientacion"].Value;
                        int anio = (int)row.Cells["Año"].Value;

                        Turno turno = new Turno(idTurno);
                        Orientacion orientacion = new Orientacion(idOrientacion);

                        idDestino = nombre;

                        nombreDestino = nombre;

                        objetoDestino = new Grupo(nombre, turno, orientacion, anio);

                        break;

                    case TipoReferencia.Docente:

                        string nombreDocente = row.Cells["Nombre"].Value.ToString();
                        string apellidoDocente = row.Cells["Apellido"].Value.ToString();
                        int ciDocente = (int)row.Cells["CI"].Value;

                        idDestino = row.Cells["CI"].Value.ToString();

                        nombreDestino = row.Cells["CI"].Value.ToString();

                        objetoDestino = new Docente(nombreDocente, apellidoDocente, ciDocente);

                        break;

                    case TipoReferencia.Orientacion:

                        string nombreOrientacion = row.Cells["Nombre"].Value.ToString();

                        idDestino = row.Cells["Id"].Value.ToString();

                        nombreDestino = row.Cells["Nombre"].Value.ToString();

                        objetoDestino = new Orientacion(nombreOrientacion);

                        break;

                    case TipoReferencia.Hora:

                        TimeSpan inicio = (TimeSpan)row.Cells["Inicio"].Value;
                        TimeSpan fin = (TimeSpan)row.Cells["Fin"].Value;
                        byte numeroHora = (byte)row.Cells["Numero"].Value;
                        Turno turnoHora = new Turno((byte)row.Cells["ID_Turno"].Value);

                        idDestino = numeroHora.ToString();
                        idPadre = row.Cells["ID_Turno"].Value.ToString();

                        nombreDestino = row.Cells["Numero"].Value.ToString() + " " + row.Cells["Turno"].Value.ToString();

                        objetoDestino = new Hora((numeroHora, turnoHora), inicio, fin);

                        break;

                    case TipoReferencia.Lugar:

                        string nombreLugar = row.Cells["Nombre"].Value.ToString();

                        byte tipo = (byte)row.Cells["ID_Tipo"].Value;
                        TipoLugar tipolugar = new TipoLugar(tipo);

                        byte piso = (byte)row.Cells["Piso"].Value;
                        bool clase = (bool)row.Cells["Clase"].Value;
                        bool usocomun = (bool)row.Cells["Uso común"].Value;
                        int coordenada_x = (int)row.Cells["Coordenada_X"].Value;
                        int coordenada_y = (int)row.Cells["Coordenada_Y"].Value;

                        idDestino = row.Cells["ID_Lugar"].Value.ToString();

                        nombreDestino = row.Cells["Nombre"].Value.ToString();

                        objetoDestino = new Lugar(nombreLugar, tipolugar, piso, coordenada_x, coordenada_y, clase, usocomun);

                        break;

                    case TipoReferencia.Funcionario:

                        string nombreFuncionario = row.Cells["Nombre"].Value.ToString();
                        string apellidoFuncionario = row.Cells["Apellido"].Value.ToString();
                        int ciFuncionario = (int)row.Cells["CI"].Value;

                        byte cargoFuncionario = (byte)row.Cells["ID_Cargo"].Value;
                        Cargo cargo = new Cargo(cargoFuncionario);

                        bool isadmFuncionario = (bool)row.Cells["Administrador"].Value;
                        DateTime inicioFuncionario = (DateTime)row.Cells["Fecha de ingreso"].Value;

                        idDestino = row.Cells["CI"].Value.ToString();

                        nombreDestino = row.Cells["CI"].Value.ToString();

                        objetoDestino = new Funcionario(nombreFuncionario, apellidoFuncionario, ciFuncionario, cargo, isadmFuncionario, inicioFuncionario);

                        break;

                    default: throw new Exception("No implementado, switch btnEditar_Click en Lista capa Presentacion");
                }

                //Abrimos el formulario agregar editar con los argumentos que hemos fabricado arriba
                if (idPadre != null)
                {
                    AbrirAgregarEditar(objetoDestino, idDestino, nombreDestino, idPadre);
                }
                else
                {
                    AbrirAgregarEditar(objetoDestino, idDestino, nombreDestino);
                }

            }
            else
            {
                //Mensaje de error
                MsgBox msg = new MsgBox("error", "Debe seleccionar una fila de la lista para editar. Para ello haga en la fila de la lista.");
                msg.ShowDialog();
            }

        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            if (Sesion.ReferenciaActual != TipoReferencia.Horario)
            {
                AbrirAgregarEditar();
            }
            else
            {
                AgregarHorario agH = new AgregarHorario();
                Metodos.OpenChildForm(agH, Metodos.menuForm.plForms);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy != true)
            {
                backgroundWorker1.RunWorkerAsync(); //Si el background worker no esta ocupado entonces empieza la operacion, esto sirve para no sobrecargarlo
            }
        }

        private void btnAsignarSalonTemporal_Click(object sender, EventArgs e)
        {
            if (Sesion.ReferenciaActual != TipoReferencia.Horario)
            { return; }

            if (DGV.SelectedRows.Count > 0)
            {
                Horario horarioDestino;
                DataGridViewRow row = DGV.SelectedRows[0];

                //Turno
                byte idTurno = (byte)row.Cells["IdTurno"].Value;
                Turno turno = new Turno(idTurno);

                //Grupo
                string idGrupo = row.Cells["Grupo"].Value.ToString();

                //Materia
                ushort idMateria = (ushort)row.Cells["IdMateria"].Value;
                Materia materia = new Materia(idMateria);

                //Horas
                List<Hora> horas = new List<Hora>();
                Hora auxHora;
                string strHoras = row.Cells["Horas"].Value.ToString();
                string[] fragmentosStr = strHoras.Split(',');

                foreach (string nHora in fragmentosStr)
                {
                    auxHora = new Hora((byte.Parse(nHora), turno));
                    horas.Add(auxHora);
                }

                //Inicio y fin
                TimeSpan inicio = (TimeSpan)row.Cells["Inicio"].Value;
                TimeSpan fin = (TimeSpan)row.Cells["Fin"].Value;

                //Dia Semana
                byte idDia = (byte)row.Cells["IdDiaSemana"].Value;
                Dia_Semana dia = new Dia_Semana(idDia);

                //Ensamblado de horario
                horarioDestino = new Horario(idGrupo, materia, dia, horas, turno, inicio, fin);


                AsignarSalonTemporal ast = new AsignarSalonTemporal(horarioDestino);
                Metodos.OpenChildForm(ast, Metodos.menuForm.plForms);
            }

        }
        private void DGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            //Si hace doble click sobre un elemento de la lista y la referencia actual es grupo abre la consulta de grupo con el grupo que haya seleccionado
            if (Sesion.ReferenciaActual == TipoReferencia.Grupo)
            {
                if (DGV.SelectedRows.Count <= 0 || e.RowIndex < 0)
                {
                    MsgBox error = new MsgBox("error", "Debe seleccionar una fila en la lista para ver los datos del grupo. Para ello haga click sobre la fila que desee eliminar y luego presione el boton.");
                    error.ShowDialog();
                    return;
                }

                Grupo grupoConsulta;
                DataGridViewRow selectedRow = DGV.Rows[e.RowIndex];
                ConsultaGrupo consultaGrupo; //Formulario de consulta grupo

                string nombregrupo = selectedRow.Cells["Nombre"].Value.ToString();
                string nombreOrientacion = selectedRow.Cells["Orientación"].Value.ToString();
                Orientacion orientacion = new Orientacion(nombreOrientacion);
                int anio = (int)selectedRow.Cells["Año"].Value;

                grupoConsulta = new Grupo(nombregrupo, orientacion, anio);

                consultaGrupo = new ConsultaGrupo(grupoConsulta);
                Metodos.OpenChildForm(consultaGrupo, Metodos.menuForm.plForms);
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

            //Segun la tabla ponemos datos que no tiene por que ver el usuario como no visibles
            switch (Sesion.ReferenciaActual)
            {
                //Funcionario
                #region
                case TipoReferencia.Funcionario:
                    DGV.Columns[3].Visible = false; //Id tipo funcionario
                    break;
                #endregion

                //Grupo
                #region
                case TipoReferencia.Grupo:
                    DGV.Columns[1].Visible = false; //Id Turno
                    DGV.Columns[3].Visible = false; //Id Orientacion
                    break;
                #endregion

                //Hora
                #region
                case TipoReferencia.Hora:
                    DGV.Columns[0].Visible = false; //Id turno
                    break;
                #endregion

                //Horario
                #region
                case TipoReferencia.Horario:
                    DGV.Columns[0].Visible = false; //Id turno
                    DGV.Columns[3].Visible = false; //Id Materia
                    DGV.Columns[8].Visible = false; //Id dia semana
                    DGV.Columns[13].Visible = false; //Id salon predeterminado
                    DGV.Columns[15].Visible = false; //Id salon temporal
                    break;
                #endregion


                //Lugar
                #region
                case TipoReferencia.Lugar:
                    DGV.Columns[0].Visible = false;//Id lugar
                    DGV.Columns[2].Visible = false;//Id tipo lugar
                    DGV.Columns[5].Visible = false;//Coordenada x
                    DGV.Columns[6].Visible = false;//Coordenada y
                    break;
                #endregion

                //Materia
                #region
                case TipoReferencia.Materia:
                    DGV.Columns[0].Visible = false;//Id materia
                    break;
                #endregion

                //Orientacion
                #region
                case TipoReferencia.Orientacion:
                    DGV.Columns[0].Visible = false;//Id orientacion
                    break;
                #endregion

                //Turno
                #region
                case TipoReferencia.Turno:
                    DGV.Columns[0].Visible = false;//Id turno
                    break;
                    #endregion
            }
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
            Metodos.OpenChildForm(agregarEditar, Metodos.menuForm.plForms);
        }

        private void AbrirAgregarEditar(object ObjetoDestino, string IdDestino, string NombreDestino)
        {
            AgregarEditar agregarEditar = new AgregarEditar
            {
                IdDestino = IdDestino,
                ObjetoDestino = ObjetoDestino,
                NombreDestino = NombreDestino
            };
            Metodos.OpenChildForm(agregarEditar, Metodos.menuForm.plForms);
        }

        private void AbrirAgregarEditar(object ObjetoDestino, string IdDestino, string NombreDestino, string IdPadre)
        {
            AgregarEditar agregarEditar = new AgregarEditar
            {
                IdDestino = IdDestino,
                ObjetoDestino = ObjetoDestino,
                NombreDestino = NombreDestino,
                IdPadre = IdPadre
            };
            Metodos.OpenChildForm(agregarEditar, Metodos.menuForm.plForms);
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
            /*"SELECT " +
                        "IdTurno," + //0
                        " NombreTurno," + //1
                        " IdGrupo," + //2
                        " IdMateria," + //3
                        " NombreMateria," + //4
                        " CiDocente," + //5
                        " NombreDocente," + //6
                        " ApellidoDocente," + //7
                        " IdDiaSemana," + //8
                        " NombreDiaSemana," + //9
                        " Horas_Abarca," + //10
                        " IdSalon_Asignado_Predeterminado," + //11
                        " NombreSalon_Asignado_Predeterminado," + //12
                        " IdSalon_Asignado_Predeterminado," + //13
                        " IdAsignadoTemporal," + //14
                        " NombreAsignadoTemporal" + //15
                        " FROM ListaHorarios;\r\n"; //16*/

            DataTable dt = FormatodDataTablePropsColumns();


            dt.Rows.Add("Turno", "IdTurno", comboSearch, TipoReferencia.Turno);
            dt.Rows.Add("Grupo", "IdGrupo", txtSearch, null);
            dt.Rows.Add("Materia", "NombreMateria", txtSearch, null);
            dt.Rows.Add("CI Docente", "CiDocente", txtSearch, null);
            dt.Rows.Add("Nombre Docente", "NombreDocente", txtSearch, null);
            dt.Rows.Add("Apellido Docente", "ApellidoDocente", txtSearch, null);
            dt.Rows.Add("Dia", "IdDiaSemana", comboSearch, TipoReferencia.DiaSemana);
            dt.Rows.Add("Horas", "Horas_Abarca", txtSearch, null);
            dt.Rows.Add("Salon", "NombreSalon_Asignado_Predeterminado", txtSearch, null);
            dt.Rows.Add("Asignado temporal", "NombreAsignadoTemporal", txtSearch, null);


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
    }
}
