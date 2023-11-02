using CapaEntidades;
using CapaNegocio;
using Proyecto.Properties;
using System;
using System.Data;
using System.Windows.Forms;


namespace Proyecto
{
    public partial class AgregarEditar : Form
    {
        public object ObjetoDestino;
        public string IdDestino;
        public string NombreDestino;
        public string IdPadre = null;
        private bool isVolverMode = false;

        public AgregarEditar()
        {
            InitializeComponent();
        }

        private void AgregarEditar_Load(object sender, EventArgs e)
        {
            Metodos.SetAgregarForm(this);
            Negocio negocio = new Negocio(); //Instancia de negocio para trabajar con metodos de la clase

            plEditar.Visible = IdDestino != null; //Si el idDestino es distinto de null el plEditar se pone en Visible true, sino false. Usamos una operacion booleana para ahorrarnos el if
            lblDestino.Text = NombreDestino != null ? NombreDestino : ""; //Si el nombreDestino es distinto de null se asigna su valor al texto del lblDestino, sino este se deja vacio

            try
            {
                switch (Sesion.ReferenciaActual)
                {
                    //Alumnos
                    #region
                    case TipoReferencia.Alumno:

                        //Mostramos paneles comunes a ambas operaciones (agregar y editar alumnos siempre mostraran los paneles para nombre y apellido)
                        plNombre.Visible = true;
                        plApellido.Visible = true;

                        txtNombre.MaxLength = 30; //Seteamos la longitud maxima en el campo de nombre para que pueda poner como maximo 30 caracteres
                        txtApellido.MaxLength = 30; //Seteamos la longitud maxima en el campo de apellido para que pueda poner como maximo 30 caracteres
                                                    //Estos valores se han tomado en cuenta viendo las restricciones en la creacion de la bd


                        //COSAS PARA EDITAR
                        //Si el id destino es distinto de null mostramos las cosas de acuerdo a lo que se necesita para editar
                        if (IdDestino != null)
                        {
                            //Si el Objeto almacenado en ObjetoDestino es transformable en un objeto de la clase Alumno lo transformamos y asignamos sus valores a los campos de texto
                            if (ObjetoDestino is Alumno alumno)
                            {
                                txtApellido.Text = alumno.Apellido;
                                txtNombre.Text = alumno.Nombre;
                            }
                        }
                        else //NO TENGO NADA EN EL IDDESTINO, COSAS PARA AGREGAR
                        {
                            //Mostramos paneles
                            plCI.Visible = true;
                            plPIN.Visible = true;
                            plComboBox1.Visible = true;

                            txtCI.MaxLength = 8; //Seteamos la longitud maxima en el campo de ci para que pueda poner como maximo 8 caracteres
                            txtPIN.MaxLength = 4;//Seteamos la longitud maxima en el campo de pin para que pueda poner como maximo 4 caracteres

                            lblCbx1.Text = "Grupos";
                            cbx1.DropDownStyle = ComboBoxStyle.DropDownList;
                            cbx1.DataSource = negocio.Listar(TipoReferencia.Grupo, null, null);
                            cbx1.DisplayMember = "Nombre";
                            cbx1.ValueMember = "Nombre";
                            cbx1.SelectedIndex = -1;
                        }

                        break;
                    #endregion

                    //Turno
                    #region
                    case TipoReferencia.Turno:

                        plNombre.Visible = true;

                        txtNombre.MaxLength = 20;

                        //COSAS PARA EDITAR
                        if (IdDestino != null)
                        {
                            if (ObjetoDestino is Turno turno)
                            {
                                txtNombre.Text = turno.Nombre;
                            }
                        }
                        break;
                    #endregion

                    //Materia
                    #region
                    case TipoReferencia.Materia:

                        plNombre.Visible = true;

                        txtNombre.MaxLength = 45;

                        //COSAS PARA EDITAR
                        if (IdDestino != null)
                        {
                            if (ObjetoDestino is Materia materia)
                            {
                                txtNombre.Text = materia.Nombre;
                            }
                        }
                        break;
                    #endregion

                    //Grupo
                    #region
                    case TipoReferencia.Grupo:

                        plComboBox1.Visible = true;
                        plCombobox2.Visible = true;
                        plCombobox3.Visible = true;

                        lblCbx1.Text = "Turno";
                        DataTable dtTurno = negocio.Listar(TipoReferencia.Turno, null, null);
                        cbx1.DataSource = dtTurno;
                        cbx1.DisplayMember = "Nombre";
                        cbx1.ValueMember = "Id";

                        lblCbx2.Text = "Orientacion";
                        DataTable dtOrientacion = negocio.Listar(TipoReferencia.Orientacion, null, null);
                        cbx2.DataSource = dtOrientacion;
                        cbx2.DisplayMember = "Nombre";
                        cbx2.ValueMember = "Id";

                        lblCbx3.Text = "Año";
                        DataTable dtAnio = negocio.Listar(TipoReferencia.Anio, null, null);
                        cbx3.DataSource = dtAnio;
                        cbx3.DisplayMember = "Anio";
                        cbx3.ValueMember = "Anio";

                        //COSAS PARA EDITAR
                        if (IdDestino != null)
                        {
                            if (ObjetoDestino is Grupo grupo)
                            {
                                //Id de los objetos que se van a setear en el combobox
                                byte idTurnoEditar = grupo.Turno.Id;
                                byte idOrientacionEditar = grupo.Orientacion.Id;
                                int idAnioEditar = grupo.Anio;

                                //ROWS FILTRADAS POR LA ID 
                                DataRow[] rowTurno = dtTurno.Select("ID = " + idTurnoEditar);
                                DataRow[] rowOrientacion = dtOrientacion.Select("ID = " + idOrientacionEditar);
                                DataRow[] rowAnio = dtAnio.Select("Anio = " + idAnioEditar);


                                //TURNO
                                // Busca en el DataTable el objeto que coincida con el ID deseado
                                if (rowTurno.Length > 0)
                                {
                                    // Si se encontró una fila que coincide con el ID deseado, establece el elemento seleccionado en el ComboBox
                                    cbx1.SelectedValue = rowTurno[0]["ID"];
                                }

                                //ORIENTACION
                                if (rowOrientacion.Length > 0)
                                {
                                    cbx2.SelectedValue = rowOrientacion[0]["ID"];
                                }

                                //Anio
                                if (rowAnio.Length > 0)
                                {
                                    cbx3.SelectedValue = rowAnio[0]["Anio"];
                                }


                            }
                        }
                        else //NO TENGO NADA EN EL IDDESTINO, COSAS PARA AGREGAR
                        {
                            //Mostramos paneles
                            plNombre.Visible = true;

                            txtNombre.MaxLength = 5;
                        }
                        break;
                    #endregion

                    //Docente
                    #region
                    case TipoReferencia.Docente:

                        plNombre.Visible = true;
                        plApellido.Visible = true;

                        txtNombre.MaxLength = 30;
                        txtApellido.MaxLength = 30;

                        //COSAS PARA EDITAR
                        if (IdDestino != null)
                        {
                            if (ObjetoDestino is Docente docente)
                            {
                                txtApellido.Text = docente.Apellido;
                                txtNombre.Text = docente.Nombre;
                            }

                        }
                        else //NO TENGO NADA EN EL IDDESTINO, COSAS PARA AGREGAR
                        {
                            plCI.Visible = true;
                            plPIN.Visible = true;

                            txtCI.MaxLength = 8;
                            txtPIN.MaxLength = 4;

                        }
                        break;
                    #endregion

                    //Orientacion
                    #region
                    case TipoReferencia.Orientacion:

                        plNombre.Visible = true;

                        txtNombre.MaxLength = 45;

                        //COSAS PARA EDITAR
                        if (IdDestino != null)
                        {
                            if (ObjetoDestino is Orientacion orientacion)
                            {
                                txtNombre.Text = orientacion.Nombre;
                            }
                        }
                        break;
                    #endregion

                    //Hora
                    #region
                    case TipoReferencia.Hora:
                        plInicio.Visible = true;
                        plFin.Visible = true;

                        //COSAS PARA EDITAR
                        if (IdDestino != null)
                        {
                            if (ObjetoDestino is Hora hora)
                            {
                                //DATETIMES para los DateTimePickers
                                DateTime fechafin = new DateTime(2000, 01, 01, hora.Fin.Hours, hora.Fin.Minutes, hora.Fin.Seconds);
                                DateTime fechainicio = new DateTime(2000, 01, 01, hora.Inicio.Hours, hora.Inicio.Minutes, hora.Inicio.Seconds);

                                //Asignamos los valores a loss DateTimePicker
                                dtpInicio.Value = fechainicio;
                                dtpFin.Value = fechafin;
                            }
                        }
                        else
                        {
                            plComboBox1.Visible = true;
                            lblCbx1.Text = "Turno";
                            DataTable dtTurnoHora = negocio.Listar(TipoReferencia.Turno, null, null);
                            cbx1.DataSource = dtTurnoHora;
                            cbx1.DisplayMember = "Nombre";
                            cbx1.ValueMember = "Id";

                            plCI.Visible = true;
                            txtCI.MaxLength = 2;
                            lblCI.Text = "Numero de hora";
                        }
                        break;
                    #endregion

                    //Anio
                    #region
                    case TipoReferencia.Anio:

                        plPIN.Visible = true;
                        lblPIN.Text = "Año";
                        txtPIN.MaxLength = 4;

                        break;
                    #endregion

                    //Lugar
                    #region
                    case TipoReferencia.Lugar:

                        plNombre.Visible = true;
                        plComboBox1.Visible = true;
                        plCheckBox1.Visible = true;
                        plCheckBox2.Visible = true;

                        lblCbx1.Text = "Tipo";
                        DataTable dtTipoLugar = negocio.Listar(TipoReferencia.TipoDeLugar, null, null);
                        cbx1.DataSource = dtTipoLugar;
                        cbx1.DisplayMember = "Nombre";
                        cbx1.ValueMember = "Id";

                        txtNombre.MaxLength = 45;

                        //COSAS PARA EDITAR
                        if (IdDestino != null)
                        {
                            if (ObjetoDestino is Lugar lugar)
                            {
                                txtNombre.Text = lugar.Nombre;

                                chck1.Checked = lugar.IsClase;
                                chck2.Checked = lugar.IsUsoComun;

                                byte idTipoLugar = lugar.Tipo.Id;
                                DataRow[] rowTipoLugar = dtTipoLugar.Select("Id = " + idTipoLugar);

                                if (rowTipoLugar.Length > 0)
                                {
                                    cbx1.SelectedValue = rowTipoLugar[0]["Id"];
                                }

                                cbx2.SelectedIndex = lugar.Piso;

                            }
                        }
                        else
                        {
                            plCombobox2.Visible = true;
                            lblCbx2.Text = "Piso";
                            cbx2.Items.Clear();
                            cbx2.Items.Add(0);
                            cbx2.Items.Add(1);
                            cbx2.Items.Add(2);
                        }
                        break;
                    #endregion

                    //Funcionario
                    #region
                    case TipoReferencia.Funcionario:

                        plNombre.Visible = true;
                        plApellido.Visible = true;
                        plComboBox1.Visible = true;
                        plCheckBox1.Visible = true;
                        txtNombre.MaxLength = 30;
                        txtApellido.MaxLength = 30;

                        chck1.Text = "Administrador";

                        lblCbx1.Text = "Cargo";
                        DataTable dtCargo = negocio.Listar(TipoReferencia.CargosFuncionarios, null, null);
                        cbx1.DataSource = dtCargo;
                        cbx1.DisplayMember = "Cargo";
                        cbx1.ValueMember = "Id_Cargo";

                        //COSAS PARA EDITAR
                        if (IdDestino != null)
                        {
                            if (ObjetoDestino is Funcionario funcionario)
                            {
                                txtApellido.Text = funcionario.Apellido;
                                txtNombre.Text = funcionario.Nombre;
                                chck1.Checked = funcionario.IsAdmn;

                                byte idCargo = funcionario.Cargo.Id;
                                DataRow[] rowCargo = dtCargo.Select("Id_Cargo = " + idCargo);
                                if (rowCargo.Length > 0)
                                {
                                    cbx1.SelectedValue = rowCargo[0]["Id_Cargo"];
                                }



                            }
                        }
                        else //NO TENGO NADA EN EL IDDESTINO, COSAS PARA AGREGAR
                        {
                            plCI.Visible = true;
                            plPIN.Visible = true;
                            txtCI.MaxLength = 8;
                            txtPIN.MaxLength = 4;
                            plFechaIngreso.Visible = true;

                        }

                        break;
                    #endregion


                    default:
                        throw new Exception("Referencia no implementada en el formulario AgregarEditar");

                }


                //Si la referencia actual es lugar ponemos en visible el boton siguiente
                if (Sesion.ReferenciaActual == TipoReferencia.Lugar && IdDestino == null)
                {
                    btnSiguiente.Visible = true;
                }
            }
            //Segun la referencia actual mostraremos unos paneles u otros para que el usuario cargue objetos
            catch (Exception ex)
            {
                MsgBox excepcion = new MsgBox("error", ex.Message);
                excepcion.ShowDialog();
            }



        }


        //Restricciones sobre los controles 
        #region
        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Si la referencia actual es grupo o lugar permite ingresar letras y numeros, sino solo permite ingresar letras
            if (Sesion.ReferenciaActual == TipoReferencia.Grupo || Sesion.ReferenciaActual == TipoReferencia.Lugar || Sesion.ReferenciaActual == TipoReferencia.Materia)
            {
                Metodos.SoloLetrasYNumeros(e);
            }
            else
            {
                Metodos.SoloLetrasYEspacio(e);
            }
        }

        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            Metodos.SoloLetrasYEspacio(e);
        }

        private void txtCI_KeyPress(object sender, KeyPressEventArgs e)
        {
            Metodos.SoloNumeros(e);
        }
        private void txtPIN_KeyPress(object sender, KeyPressEventArgs e)
        {
            Metodos.SoloNumeros(e);
        }

        private void Limpiar()
        {
            // Limpiar los campos después de un registro exitoso
            txtCI.Text = "";
            txtPIN.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            cbx1.SelectedIndex = -1;
            cbx2.SelectedIndex = -1;
            cbx3.SelectedIndex = -1;
            chck1.Checked = false;
            chck2.Checked = false;
        }

        #endregion

        //Botones
        #region
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Lista lista = new Lista();
            Close();
            Metodos.OpenChildForm(lista, Metodos.menuForm.plForms);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            MsgBox msg = null;
            MsgBox msgsecundario = null; //Usado para cuando se agrega un alumno ya con un grupo
            Negocio negocio = new Negocio();

            try
            {
                //Segun la referencia aplicamos unas validaciones u otras
                switch (Sesion.ReferenciaActual)
                {
                    case TipoReferencia.Alumno:
                        if (ValidarAgregarAlumnos() == RetornoValidacion.OK)
                        {
                            string ci = IdDestino != null ? IdDestino : txtCI.Text;
                            Alumno alumno = IdDestino != null ? new Alumno(txtNombre.Text, txtApellido.Text, int.Parse(ci)) : new Alumno(txtNombre.Text, txtApellido.Text, int.Parse(ci), short.Parse(txtPIN.Text));
                            RetornoValidacion resultadoAgregarUsuario;

                            if (IdDestino != null)
                            {
                                resultadoAgregarUsuario = negocio.Editar(TipoReferencia.Usuario, alumno, IdDestino, null);
                            }
                            else
                            {
                                resultadoAgregarUsuario = negocio.Agregar(TipoReferencia.Usuario, alumno, ci);
                            }

                            switch (resultadoAgregarUsuario)
                            {
                                case RetornoValidacion.OK:

                                    RetornoValidacion resultadoAgregarAlumno;
                                    RetornoValidacion resultadoAgregarAlumnoGrupo;

                                    if (IdDestino != null)
                                    {
                                        resultadoAgregarAlumno = negocio.Editar(TipoReferencia.Alumno, alumno, IdDestino, null);
                                    }
                                    else
                                    {
                                        resultadoAgregarAlumno = negocio.Agregar(TipoReferencia.Alumno, alumno, txtCI.Text);
                                    }
                                    switch (resultadoAgregarAlumno)
                                    {
                                        case RetornoValidacion.OK:
                                            msg = new MsgBox("exito", "Alumno cargado exitosamente");
                                            if (cbx1.SelectedIndex != -1)
                                            {
                                                resultadoAgregarAlumnoGrupo = negocio.AgregarAlumnoAGrupo(txtCI.Text, cbx1.SelectedValue.ToString());

                                                switch (resultadoAgregarAlumnoGrupo)
                                                {
                                                    case RetornoValidacion.OK:
                                                        msgsecundario = new MsgBox("exito", "El alumno se registro en el grupo satisfactoriamente");
                                                        break;

                                                    case RetornoValidacion.ErrorInesperadoBD:
                                                        msgsecundario = new MsgBox("error", "Error inesperado. No se pudo registrar el alumno en el grupo");
                                                        break;

                                                }
                                            }
                                            Limpiar();
                                            break;

                                        case RetornoValidacion.YaExiste:
                                            msg = new MsgBox("error", "Ya hay un alumno en el sistema registrado con esa cedula");
                                            break;

                                        case RetornoValidacion.NoExiste:
                                            msg = new MsgBox("error", "No se pudo encontrar el alumno que intento editar, intente de nuevo o contacte con un tecnico del sistema.");
                                            break;

                                        case RetornoValidacion.ErrorInesperadoBD:
                                            msg = new MsgBox("error", "Ha sucedido un error inesperado, intentelo de nuevo, si el error perdurase contacte con un administrador del sistema");
                                            break;
                                    }
                                    break;

                                case RetornoValidacion.YaExiste:
                                    msg = new MsgBox("error", "Ya hay un usuario en el sistema registrado con esa cedula");
                                    break;

                                case RetornoValidacion.NoExiste:
                                    msg = new MsgBox("error", "No se pudo encontrar el usuario que intento editar, intente de nuevo o contacte con un tecnico del sistema.");
                                    break;

                                case RetornoValidacion.ErrorInesperadoBD:
                                    msg = new MsgBox("error", "Ha sucedido un error inesperado, intentelo de nuevo, si el error perdurase contacte con un administrador del sistema");
                                    break;
                            }
                            cerrarFormSiSeEdito();
                        }
                        break;

                    case TipoReferencia.Anio:
                        if (ValidarAnio() == RetornoValidacion.OK)
                        {
                            string anio = txtPIN.Text;
                            RetornoValidacion resultadoAgregar = negocio.Agregar(TipoReferencia.Anio, int.Parse(anio), anio);

                            switch (resultadoAgregar)
                            {
                                case RetornoValidacion.OK:
                                    msg = new MsgBox("exito", "Año cargado exitosamente");
                                    Limpiar();
                                    break;

                                case RetornoValidacion.YaExiste:
                                    msg = new MsgBox("error", "El año que intenta ingresar ya esta registrado en el sistema.");
                                    break;

                                case RetornoValidacion.ErrorInesperadoBD:
                                    msg = new MsgBox("error", "Ha sucedido un error inesperado, intentelo de nuevo, si el error perdurase contacte con un administrador del sistema");
                                    break;
                            }
                            cerrarFormSiSeEdito();
                        }
                        break;

                    case TipoReferencia.Docente:
                        if (ValidarAgregarDocentes() == RetornoValidacion.OK)
                        {
                            string ci = IdDestino != null ? IdDestino : txtCI.Text;
                            Docente docente = IdDestino != null ? new Docente(txtNombre.Text, txtApellido.Text, int.Parse(ci)) : new Docente(txtNombre.Text, txtApellido.Text, int.Parse(ci), short.Parse(txtPIN.Text));
                            RetornoValidacion resultadoAgregarUsuario;

                            if (IdDestino != null)
                            {
                                resultadoAgregarUsuario = negocio.Editar(TipoReferencia.Usuario, docente, IdDestino, null);
                            }
                            else
                            {
                                resultadoAgregarUsuario = negocio.Agregar(TipoReferencia.Usuario, docente, ci);
                            }

                            switch (resultadoAgregarUsuario)
                            {
                                case RetornoValidacion.OK:
                                    RetornoValidacion resultadoAgregarDocente;

                                    if (IdDestino != null)
                                    {
                                        resultadoAgregarDocente = negocio.Editar(TipoReferencia.Docente, docente, IdDestino, null);
                                    }
                                    else
                                    {
                                        resultadoAgregarDocente = negocio.Agregar(TipoReferencia.Docente, docente, txtCI.Text);
                                    }
                                    switch (resultadoAgregarDocente)
                                    {
                                        case RetornoValidacion.OK:
                                            msg = new MsgBox("exito", "Docente cargado exitosamente");
                                            Limpiar();
                                            break;

                                        case RetornoValidacion.YaExiste:
                                            msg = new MsgBox("error", "Ya hay un docente en el sistema registrado con esa cedula");
                                            break;

                                        case RetornoValidacion.NoExiste:
                                            msg = new MsgBox("error", "No se pudo encontrar el docente que intento editar, intente de nuevo o contacte con un tecnico del sistema.");
                                            break;

                                        case RetornoValidacion.ErrorInesperadoBD:
                                            msg = new MsgBox("error", "Ha sucedido un error inesperado, intentelo de nuevo, si el error perdurase contacte con un administrador del sistema");
                                            break;
                                    }
                                    break;

                                case RetornoValidacion.YaExiste:
                                    msg = new MsgBox("error", "Ya hay un usuario en el sistema registrado con esa cedula");
                                    break;

                                case RetornoValidacion.NoExiste:
                                    msg = new MsgBox("error", "No se pudo encontrar el usuario que intento editar, intente de nuevo o contacte con un tecnico del sistema.");
                                    break;

                                case RetornoValidacion.ErrorInesperadoBD:
                                    msg = new MsgBox("error", "Ha sucedido un error inesperado, intentelo de nuevo, si el error perdurase contacte con un administrador del sistema");
                                    break;
                            }
                            cerrarFormSiSeEdito();
                        }
                        break;

                    case TipoReferencia.Funcionario:
                        if (ValidarAgregarFuncionarios() == RetornoValidacion.OK)
                        {
                            string ci = IdDestino != null ? IdDestino : txtCI.Text;
                            Cargo auxcargo = new Cargo((byte)cbx1.SelectedValue);

                            Funcionario funcionario =
                                IdDestino != null ?
                                new Funcionario(txtNombre.Text, txtApellido.Text, int.Parse(ci), auxcargo, chck1.Checked, dtpFechaIngreso.Value) :
                                new Funcionario(txtNombre.Text, txtApellido.Text, int.Parse(ci), short.Parse(txtPIN.Text), auxcargo, chck1.Checked, dtpFechaIngreso.Value);

                            RetornoValidacion resultadoAgregarUsuario;

                            if (IdDestino != null)
                            {
                                resultadoAgregarUsuario = negocio.Editar(TipoReferencia.Usuario, funcionario, IdDestino, null);
                            }
                            else
                            {
                                resultadoAgregarUsuario = negocio.Agregar(TipoReferencia.Usuario, funcionario, ci);
                            }

                            switch (resultadoAgregarUsuario)
                            {
                                case RetornoValidacion.OK:
                                    RetornoValidacion resultadoAgregarFuncionario;
                                    if (IdDestino != null)
                                    {
                                        resultadoAgregarFuncionario = negocio.Editar(TipoReferencia.Funcionario, funcionario, IdDestino, null);
                                    }
                                    else
                                    {
                                        resultadoAgregarFuncionario = negocio.Agregar(TipoReferencia.Funcionario, funcionario, ci);
                                    }

                                    switch (resultadoAgregarFuncionario)
                                    {
                                        case RetornoValidacion.OK:
                                            msg = new MsgBox("exito", "Funcionario cargado exitosamente");
                                            Limpiar();
                                            break;

                                        case RetornoValidacion.YaExiste:
                                            msg = new MsgBox("error", "Ya hay un funcionario en el sistema registrado con esa cedula");
                                            break;

                                        case RetornoValidacion.NoExiste:
                                            msg = new MsgBox("error", "No se pudo encontrar el funcionario que intento editar, intente de nuevo o contacte con un tecnico del sistema.");
                                            break;

                                        case RetornoValidacion.ErrorInesperadoBD:
                                            msg = new MsgBox("error", "Ha sucedido un error inesperado, intentelo de nuevo, si el error perdurase contacte con un administrador del sistema");
                                            break;
                                    }
                                    break;

                                case RetornoValidacion.YaExiste:
                                    msg = new MsgBox("error", "Ya hay un usuario en el sistema registrado con esa cedula");
                                    break;

                                case RetornoValidacion.NoExiste:
                                    msg = new MsgBox("error", "No se pudo encontrar el usuario que intento editar, intente de nuevo o contacte con un tecnico del sistema.");
                                    break;

                                case RetornoValidacion.ErrorInesperadoBD:
                                    msg = new MsgBox("error", "Ha sucedido un error inesperado, intentelo de nuevo, si el error perdurase contacte con un administrador del sistema");
                                    break;
                            }
                            cerrarFormSiSeEdito();
                        }
                        break;

                    case TipoReferencia.Grupo:
                        if (ValidarAgregarGrupo() == RetornoValidacion.OK)
                        {
                            string nombre = IdDestino != null ? IdDestino : txtNombre.Text;
                            Turno turno = new Turno((byte)cbx1.SelectedValue);
                            Orientacion orientacion = new Orientacion((byte)cbx2.SelectedValue);
                            int anio = (int)cbx3.SelectedValue;
                            Grupo grupo = new Grupo(nombre, turno, orientacion, anio);
                            RetornoValidacion resultadoAgregar;
                            if (IdDestino != null)
                            {
                                resultadoAgregar = negocio.Editar(TipoReferencia.Grupo, grupo, IdDestino, null);
                            }
                            else
                            {
                                resultadoAgregar = negocio.Agregar(TipoReferencia.Grupo, grupo, nombre);
                            }

                            switch (resultadoAgregar)
                            {
                                case RetornoValidacion.OK:
                                    msg = new MsgBox("exito", "Grupo cargado exitosamente");
                                    Limpiar();
                                    break;

                                case RetornoValidacion.YaExiste:
                                    msg = new MsgBox("error", "El nombre de grupo que intenta ingresar ya esta registrado en el sistema.");
                                    break;

                                case RetornoValidacion.NoExiste:
                                    msg = new MsgBox("error", "No se pudo encontrar el grupo que intento editar, intente de nuevo o contacte con un tecnico del sistema.");
                                    break;

                                case RetornoValidacion.ErrorInesperadoBD:
                                    msg = new MsgBox("error", "Ha sucedido un error inesperado, intentelo de nuevo, si el error perdurase contacte con un administrador del sistema");
                                    break;
                            }
                            cerrarFormSiSeEdito();
                        }
                        break;

                    case TipoReferencia.Hora:
                        if (ValidarAgregarHora() == RetornoValidacion.OK)
                        {
                            Turno turno;
                            (byte nid, Turno turno) id;

                            //Si hay algo en IdDestino setea el turno y el id en lo que haya ahi
                            if (IdPadre != null)
                            {
                                turno = new Turno(byte.Parse(IdPadre));
                                id = (byte.Parse(IdDestino), turno);
                            }
                            //Sino en lo que ingrese el usuario
                            else
                            {
                                turno = new Turno((byte)cbx1.SelectedValue);
                                id = (byte.Parse(txtCI.Text), turno);
                            }

                            DateTime fechaseleccionadainicio = dtpInicio.Value;
                            DateTime medianocheinicio = new DateTime(fechaseleccionadainicio.Year, fechaseleccionadainicio.Month, fechaseleccionadainicio.Day, 0, 0, 0);
                            TimeSpan horaInicio = fechaseleccionadainicio - medianocheinicio;

                            DateTime fechaseleccionadafin = dtpFin.Value;
                            DateTime medianochefin = new DateTime(fechaseleccionadafin.Year, fechaseleccionadafin.Month, fechaseleccionadafin.Day, 0, 0, 0);
                            TimeSpan horaFin = fechaseleccionadafin - medianochefin;

                            Hora hora = new Hora(id, horaInicio, horaFin);

                            RetornoValidacion resultadoAgregar;

                            if (IdDestino != null)
                            {
                                resultadoAgregar = negocio.Editar(TipoReferencia.Hora, hora, byte.Parse(IdDestino), byte.Parse(IdPadre));
                            }
                            else
                            {
                                resultadoAgregar = negocio.Agregar(TipoReferencia.Hora, hora, id.nid, id.turno.Id);
                            }

                            switch (resultadoAgregar)
                            {
                                case RetornoValidacion.OK:
                                    msg = new MsgBox("exito", "Hora cargada exitosamente");
                                    Limpiar();
                                    break;

                                case RetornoValidacion.YaExiste:
                                    msg = new MsgBox("error", "Ya existe una hora con ese numero asignada a ese turno en el sistema.");
                                    break;

                                case RetornoValidacion.NoExiste:
                                    msg = new MsgBox("error", "No se encontro una hora con ese numero asignada a ese turno en el sistema.");
                                    break;

                                case RetornoValidacion.ErrorInesperadoBD:
                                    msg = new MsgBox("error", "Ha sucedido un error inesperado, intentelo de nuevo, si el error perdurase contacte con un administrador del sistema");
                                    break;
                            }
                            cerrarFormSiSeEdito();

                        }
                        break;

                    case TipoReferencia.Lugar:
                        if (ValidarAgregarLugar() == RetornoValidacion.OK)
                        {
                            //Variables usadas para armar el lugar
                            string nombre;
                            TipoLugar tipo;
                            byte piso;
                            int coordX;
                            int coordY;
                            bool isClase;
                            bool isUsoComun;
                            ushort id; // El id solo se usara si se quiere editar y se conseguira desde el idDestino
                            Lugar lugar; //Lugar que se enviara a los metodos de la capa de negocio y datos

                            //Variable que se utilizara para conocer el resultado de la operacion en la capa de negocio y datos
                            RetornoValidacion resultadoAgregar;

                            //ASIGNACION DE VALORES
                            nombre = txtNombre.Text;
                            tipo = new TipoLugar((byte)cbx1.SelectedValue); //Obtiene el id del tipolugar seleccionado en el combobox

                            isClase = chck1.Checked; //Asigna true si la casilla esta marcada o false si no lo esta
                            isUsoComun = chck2.Checked;


                            if (IdDestino == null)
                            {
                                piso = (byte)(int)cbx2.SelectedItem; //Obtiene el piso seleccionado en el combobox
                                coordX = Mapa.CurrentMapa.SelectedX;
                                coordY = Mapa.CurrentMapa.SelectedY;
                                lugar = new Lugar(nombre, tipo, piso, coordX, coordY, isClase, isUsoComun);
                            }
                            else
                            {
                                lugar = new Lugar(nombre, tipo, isClase, isUsoComun);
                            }


                            //En caso de edicion Asigna el valor al id y ejecuta la operacion como editar
                            if (IdDestino != null)
                            {
                                id = ushort.Parse(IdDestino);
                                resultadoAgregar = negocio.Editar(TipoReferencia.Lugar, lugar, id.ToString(), nombre);
                                Mapa.CurrentMapa.MapaClick = true;
                            }
                            //En caso contrario ejecuta la operacion como agregar
                            else
                            {
                                resultadoAgregar = negocio.Agregar(TipoReferencia.Lugar, lugar, nombre);
                            }

                            switch (resultadoAgregar)
                            {
                                case RetornoValidacion.OK:
                                    msg = new MsgBox("exito", "Lugar cargado exitosamente");
                                    Mapa.CurrentMapa.MapaClick = false;
                                    Limpiar();
                                    break;

                                case RetornoValidacion.YaExisteNombre:
                                    msg = new MsgBox("error", "Ya existe un lugar con ese nombre en el sistema. Por favor seleccione uno nuevo.");
                                    break;

                                case RetornoValidacion.NoExiste:
                                    msg = new MsgBox("error", "No se pudo encontrar el lugar que busco en el sistema.");
                                    break;

                                case RetornoValidacion.ErrorInesperadoBD:
                                    msg = new MsgBox("error", "Ha sucedido un error inesperado, intentelo de nuevo, si el error perdurase contacte con un administrador del sistema");
                                    break;
                            }
                            Lista lista = new Lista();
                            Close();
                            Metodos.OpenChildForm(lista, Metodos.menuForm.plForms);
                        }
                        break;

                    case TipoReferencia.Materia:
                        if (ValidarMateriaTurnoOrientacion() == RetornoValidacion.OK)
                        {
                            string nombre = txtNombre.Text;
                            Materia materia = new Materia(nombre);
                            string id;
                            RetornoValidacion resultadoAgregar;

                            if (IdDestino != null)
                            {
                                id = IdDestino;
                                resultadoAgregar = negocio.Editar(TipoReferencia.Materia, materia, id, nombre);
                            }
                            else
                            {
                                resultadoAgregar = negocio.Agregar(TipoReferencia.Materia, materia, nombre);
                            }

                            switch (resultadoAgregar)
                            {
                                case RetornoValidacion.OK:
                                    msg = new MsgBox("exito", "Materia cargada exitosamente");
                                    Limpiar();
                                    break;

                                case RetornoValidacion.YaExisteNombre:
                                    msg = new MsgBox("error", "La materai que intenta ingresar ya esta registrada en el sistema.");
                                    break;

                                case RetornoValidacion.NoExiste:
                                    msg = new MsgBox("error", "No se pudo encontrar la materia en el sistema.");
                                    break;

                                case RetornoValidacion.ErrorInesperadoBD:
                                    msg = new MsgBox("error", "Ha sucedido un error inesperado, intentelo de nuevo, si el error perdurase contacte con un administrador del sistema");
                                    break;
                            }
                            cerrarFormSiSeEdito();
                        }
                        break;

                    case TipoReferencia.Orientacion:
                        if (ValidarMateriaTurnoOrientacion() == RetornoValidacion.OK)
                        {
                            string nombre = txtNombre.Text;
                            Orientacion orientacion = new Orientacion(nombre);
                            string id;
                            RetornoValidacion resultadoAgregar;

                            if (IdDestino != null)
                            {
                                id = IdDestino;
                                resultadoAgregar = negocio.Editar(TipoReferencia.Orientacion, orientacion, id, nombre);
                            }
                            else
                            {
                                resultadoAgregar = negocio.Agregar(TipoReferencia.Orientacion, orientacion, nombre);
                            }

                            switch (resultadoAgregar)
                            {
                                case RetornoValidacion.OK:
                                    msg = new MsgBox("exito", "Orientacion cargada exitosamente");
                                    Limpiar();
                                    break;

                                case RetornoValidacion.YaExisteNombre:
                                    msg = new MsgBox("error", "La orientacion que intenta ingresar ya esta registrada en el sistema.");
                                    break;

                                case RetornoValidacion.NoExiste:
                                    msg = new MsgBox("error", "No se pudo encontrar la orientacion en el sistema.");
                                    break;

                                case RetornoValidacion.ErrorInesperadoBD:
                                    msg = new MsgBox("error", "Ha sucedido un error inesperado, intentelo de nuevo, si el error perdurase contacte con un administrador del sistema");
                                    break;
                            }
                            cerrarFormSiSeEdito();
                        }
                        break;

                    case TipoReferencia.Turno:
                        if (ValidarMateriaTurnoOrientacion() == RetornoValidacion.OK)
                        {
                            string nombre = txtNombre.Text;
                            Turno turno = new Turno(nombre);
                            RetornoValidacion resultadoAgregar;

                            if (IdDestino != null)
                            {
                                resultadoAgregar = negocio.Editar(TipoReferencia.Turno, turno, IdDestino, nombre);
                            }
                            else
                            {
                                resultadoAgregar = negocio.Agregar(TipoReferencia.Turno, turno, nombre);
                            }

                            switch (resultadoAgregar)
                            {
                                case RetornoValidacion.OK:
                                    msg = new MsgBox("exito", "Turno cargado exitosamente");
                                    Limpiar();
                                    break;

                                case RetornoValidacion.YaExisteNombre:
                                    msg = new MsgBox("error", "El turno que intenta ingresar ya esta registrado en el sistema.");
                                    break;

                                case RetornoValidacion.NoExiste:
                                    msg = new MsgBox("error", "No se pudo encontrar el turno en el sistema.");
                                    break;

                                case RetornoValidacion.ErrorInesperadoBD:
                                    msg = new MsgBox("error", "Ha sucedido un error inesperado, intentelo de nuevo, si el error perdurase contacte con un administrador del sistema");
                                    break;
                            }
                            cerrarFormSiSeEdito();
                        }
                        break;
                }

                if (msg != null)
                {
                    msg.ShowDialog();
                }
                if (msgsecundario != null)
                {
                    msgsecundario.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MsgBox excepcion = new MsgBox("error", ex.Message);
                excepcion.ShowDialog();
            }
        }

        #endregion

        //Validaciones
        #region
        //Usado para validar los atributos comunes entre docentes, alumnos y funcionarios.
        private RetornoValidacion ValidarAgregarUsuarios()
        {
            RetornoValidacion respuesta = RetornoValidacion.OK;
            Validaciones validaciones = new Validaciones();
            MsgBox msg;

            //Validar vacio
            if (IdDestino == null)
            {
                if (validaciones.ValidarVacio(txtCI.Text))
                {
                    msg = new MsgBox("error", "Debe completar el campo de CI.");
                    msg.ShowDialog();
                    txtCI.Focus();
                    respuesta = RetornoValidacion.ErrorDeFormato;
                }
                else if (validaciones.ValidarVacio(txtPIN.Text))
                {
                    msg = new MsgBox("error", "Debe completar el campo de PIN.");
                    msg.ShowDialog();
                    txtPIN.Focus();
                    respuesta = RetornoValidacion.ErrorDeFormato;
                }
                else if (!validaciones.ValidarCI(txtCI.Text))// Validar la cédula
                {
                    txtCI.Focus();
                    msg = new MsgBox("error", "Cédula no válida. Debe ingresar un número de 8 cifras.");
                    msg.ShowDialog();
                    respuesta = RetornoValidacion.ErrorDeFormato;
                }
                else if (!validaciones.ValidarPIN(txtPIN.Text))// Validar el PIN
                {
                    txtPIN.Focus();
                    msg = new MsgBox("error", "PIN no válido. Debe ingresar un número de 4 cifras.");
                    msg.ShowDialog();
                    respuesta = RetornoValidacion.ErrorDeFormato;
                }
            }

            if (validaciones.ValidarVacio(txtNombre.Text))
            {
                msg = new MsgBox("error", "Debe completar el campo de Nombre.");
                msg.ShowDialog();
                txtNombre.Focus();
                respuesta = RetornoValidacion.ErrorDeFormato;
            }
            else if (validaciones.ValidarVacio(txtApellido.Text))
            {
                msg = new MsgBox("error", "Debe completar el campo de Apellido.");
                msg.ShowDialog();
                txtApellido.Focus();
                respuesta = RetornoValidacion.ErrorDeFormato;
            }



            return respuesta;

        }

        private RetornoValidacion ValidarAgregarAlumnos()
        {
            RetornoValidacion respuesta = RetornoValidacion.OK;
            Validaciones validaciones = new Validaciones();

            respuesta = ValidarAgregarUsuarios();

            return respuesta;

        }

        private RetornoValidacion ValidarAnio()
        {
            RetornoValidacion respuesta = RetornoValidacion.OK;
            Validaciones validaciones = new Validaciones();

            //Validar vacio
            if (validaciones.ValidarVacio(txtPIN.Text))
            {
                MsgBox msg = new MsgBox("error", "Debe completar el campo de Año antes de continuar.");
                msg.ShowDialog();
                txtPIN.Focus();
                respuesta = RetornoValidacion.ErrorDeFormato;
            }
            else if (!validaciones.ValidarAnio(short.Parse(txtPIN.Text)))
            {
                MsgBox msg = new MsgBox("error", "El año no puede ser mayor que el año actual + 2. Tampoco puede ser anterior a este.");
                msg.ShowDialog();
                txtPIN.Focus();
                respuesta = RetornoValidacion.ErrorDeFormato;
            }

            return respuesta;
        }

        private RetornoValidacion ValidarAgregarDocentes()
        {
            RetornoValidacion respuesta = RetornoValidacion.OK;
            Validaciones validaciones = new Validaciones();

            respuesta = ValidarAgregarUsuarios();


            return respuesta;

        }

        private RetornoValidacion ValidarAgregarFuncionarios()
        {
            RetornoValidacion respuesta = RetornoValidacion.OK;
            Validaciones validaciones = new Validaciones();

            RetornoValidacion resultadoUsuarios = ValidarAgregarUsuarios();

            if (resultadoUsuarios != RetornoValidacion.OK)
            {
                respuesta = resultadoUsuarios;
            }
            else if (cbx1.SelectedIndex == -1)
            {
                MsgBox msg = new MsgBox("error", "Debe seleccionar un cargo.");
                msg.ShowDialog();
                cbx1.Focus();
                respuesta = RetornoValidacion.ErrorDeFormato;
            }

            return respuesta;

        }

        private RetornoValidacion ValidarAgregarGrupo()
        {
            RetornoValidacion respuesta = RetornoValidacion.OK;
            Validaciones validaciones = new Validaciones();

            if (IdDestino == null)
            {
                if (validaciones.ValidarVacio(txtNombre.Text))
                {
                    MsgBox msg = new MsgBox("error", "Debe completar el campo de Nombre.");
                    msg.ShowDialog();
                    txtNombre.Focus();
                    respuesta = RetornoValidacion.ErrorDeFormato;
                }
            }

            if (cbx1.SelectedIndex == -1)
            {
                MsgBox msg = new MsgBox("error", "Debe seleccionar una orientación.");
                msg.ShowDialog();
                cbx1.Focus();
                respuesta = RetornoValidacion.ErrorDeFormato;
            }
            else if (cbx2.SelectedIndex == -1)
            {
                MsgBox msg = new MsgBox("error", "Debe seleccionar un año.");
                msg.ShowDialog();
                cbx2.Focus();
                respuesta = RetornoValidacion.ErrorDeFormato;
            }
            else if (cbx3.SelectedIndex == -1)
            {
                MsgBox msg = new MsgBox("error", "Debe seleccionar un turno.");
                msg.ShowDialog();
                cbx3.Focus();
                respuesta = RetornoValidacion.ErrorDeFormato;
            }

            return respuesta;

        }

        private RetornoValidacion ValidarAgregarHora()
        {
            RetornoValidacion respuesta = RetornoValidacion.OK;
            Validaciones validaciones = new Validaciones();

            //Validar vacio
            if (IdDestino == null)
            {
                if (validaciones.ValidarVacio(txtCI.Text))
                {
                    MsgBox msg = new MsgBox("error", "Debe ingresar un número de hora.");
                    msg.ShowDialog();
                    txtCI.Focus();
                    respuesta = RetornoValidacion.ErrorDeFormato;
                }
                else if (cbx1.SelectedIndex == -1)
                {
                    MsgBox msg = new MsgBox("error", "Debe seleccionar un turno.");
                    msg.ShowDialog();
                    cbx1.Focus();
                    respuesta = RetornoValidacion.ErrorDeFormato;
                }
            }

            if (dtpInicio.Value > dtpFin.Value)
            {
                MsgBox msg = new MsgBox("error", "La hora inicial no puede ser mayor a la hora final.");
                msg.ShowDialog();
                dtpInicio.Focus();
                respuesta = RetornoValidacion.ErrorDeFormato;
            }

            return respuesta;

        }

        private RetornoValidacion ValidarAgregarLugar()
        {
            RetornoValidacion respuesta = RetornoValidacion.OK;
            Validaciones validaciones = new Validaciones();
            MsgBox msg;

            //Validar vacio
            if (validaciones.ValidarVacio(txtNombre.Text))
            {
                msg = new MsgBox("error", "Debe ingresar un nombre.");
                msg.ShowDialog();
                txtNombre.Focus();
                respuesta = RetornoValidacion.ErrorDeFormato;
            }
            else if (cbx1.SelectedIndex == -1)
            {
                msg = new MsgBox("error", "Debe seleccionar un tipo de lugar.");
                msg.ShowDialog();
                cbx1.Focus();
                respuesta = RetornoValidacion.ErrorDeFormato;
            }

            if (IdDestino == null)
            {
                if (!Mapa.CurrentMapa.MapaClick)
                {
                    msg = new MsgBox("error", "Debe seleccionar un lugar en el mapa.");
                    msg.ShowDialog();
                    respuesta = RetornoValidacion.ErrorDeFormato;
                }
                else if (cbx2.SelectedIndex == -1)
                {
                    msg = new MsgBox("error", "Debe seleccionar un piso.");
                    msg.ShowDialog();
                    cbx1.Focus();
                    respuesta = RetornoValidacion.ErrorDeFormato;
                }
            }

            return respuesta;

        }

        private RetornoValidacion ValidarMateriaTurnoOrientacion()
        {
            RetornoValidacion respuesta = RetornoValidacion.OK;
            Validaciones validaciones = new Validaciones();

            //Validar vacio
            if (validaciones.ValidarVacio(txtNombre.Text))
            {
                MsgBox msg = new MsgBox("error", "Debe ingresar un nombre antes de continuar.");
                msg.ShowDialog();
                txtNombre.Focus();
                respuesta = RetornoValidacion.ErrorDeFormato;
            }

            return respuesta;
        }


        #endregion

        private void cerrarFormSiSeEdito()
        {
            //Si se edito algo cierra el form y abre la lista
            if (IdDestino != null)
            {
                Lista lista = new Lista();
                Close();
                Metodos.OpenChildForm(lista, Metodos.menuForm.plForms);
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (cbx2.SelectedIndex == -1)
            {
                MsgBox msg = new MsgBox("error", "Seleccione un piso antes de elegir las coordenadas.");
                msg.ShowDialog();
            }
            else
            {
                if (!isVolverMode)
                {
                    Mapa mapa = new Mapa();
                    mapa.CambiarMapa(cbx2.SelectedIndex);
                    plLugares.Visible = true;
                    Metodos.OpenMapForm(mapa, plLugares);

                    plNombre.Visible = false;
                    plComboBox1.Visible = false;
                    plCombobox2.Visible = false;
                    plCheckBox1.Visible = false;
                    plCheckBox2.Visible = false;
                    btnSiguiente.Image = Resources.VOLVER;
                    isVolverMode = true;
                }
                else
                {
                    plLugares.Visible = false;
                    plNombre.Visible = true;
                    plComboBox1.Visible = true;
                    plCombobox2.Visible = true;
                    plCheckBox1.Visible = true;
                    plCheckBox2.Visible = true;
                    btnSiguiente.Image = Resources.siguiente;
                    isVolverMode = false;
                }
            }
        }
    }
}
