using CapaEntidades;
using CapaNegocio;
using System;
using System.Windows.Forms;

namespace Proyecto
{
    public partial class AgregarEditar : Form
    {
        public object ObjetoDestino;
        public string IdDestino;
        public string NombreDestino;
        public string IdPadre;

        public AgregarEditar()
        {
            InitializeComponent();
        }

        private void AgregarEditar_Load(object sender, EventArgs e)
        {
            Negocio negocio = new Negocio(); //Instancia de negocio para trabajar con metodos de la clase

            plEditar.Visible = IdDestino != null; //Si el idDestino es distinto de null el plEditar se pone en Visible true, sino false. Usamos una operacion booleana para ahorrarnos el if
            lblDestino.Text = NombreDestino != null ? NombreDestino : ""; //Si el nombreDestino es distinto de null se asigna su valor al texto del lblDestino, sino este se deja vacio

            //Segun la referencia actual mostraremos unos paneles u otros para que el usuario cargue objetos
            switch (Sesion.ReferenciaActual)
            {
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

                        txtCI.MaxLength = 8; //Seteamos la longitud maxima en el campo de ci para que pueda poner como maximo 8 caracteres
                        txtPIN.MaxLength = 4;//Seteamos la longitud maxima en el campo de pin para que pueda poner como maximo 4 caracteres
                    }

                    break;

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

                case TipoReferencia.Grupo:

                    plComboBox1.Visible = true;
                    plCombobox2.Visible = true;
                    plCombobox3.Visible = true;

                    lblCbx1.Text = "Turno";
                    cbx1.DataSource = negocio.Listar(TipoReferencia.Turno, null, null);
                    cbx1.DisplayMember = "Nombre";
                    cbx1.ValueMember = "Id";

                    lblCbx2.Text = "Orientacion";
                    cbx2.DataSource = negocio.Listar(TipoReferencia.Orientacion, null, null);
                    cbx2.DisplayMember = "Nombre";
                    cbx2.ValueMember = "Id";

                    lblCbx3.Text = "Año";
                    cbx3.DataSource = negocio.Listar(TipoReferencia.Anio, null, null);
                    cbx3.DisplayMember = "Anio";
                    cbx3.ValueMember = "Anio";

                    //COSAS PARA EDITAR
                    if (IdDestino != null)
                    {
                        if (ObjetoDestino is Grupo grupo)
                        {
                            //COMPLETAR
                            //COMPLETAR
                            //COMPLETAR

                            lblCbx1.Text = "Turno";
                            cbx1.DataSource = negocio.Listar(TipoReferencia.Turno, null, null);
                            cbx1.DisplayMember = "Nombre";
                            cbx1.ValueMember = "Id";

                            lblCbx2.Text = "Orientacion";
                            cbx2.DataSource = negocio.Listar(TipoReferencia.Orientacion, null, null);
                            cbx2.DisplayMember = "Nombre";
                            cbx2.ValueMember = "Id";

                            lblCbx3.Text = "Anio";
                            cbx3.DataSource = negocio.Listar(TipoReferencia.Anio, null, null);
                            cbx3.DisplayMember = "Anio";
                            cbx3.ValueMember = "Anio";


                        }
                    }
                    else //NO TENGO NADA EN EL IDDESTINO, COSAS PARA AGREGAR
                    {
                        //Mostramos paneles
                        plNombre.Visible = true;

                        txtNombre.MaxLength = 5;
                    }
                    break;

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

                case TipoReferencia.Hora:

                    plComboBox1.Visible = true;
                    plInicio.Visible = true;
                    plFin.Visible = true;

                    //COSAS PARA EDITAR
                    if (IdDestino != null)
                    {
                        if (ObjetoDestino is Hora hora)
                        {
                            DateTime fechainicio = new DateTime(2000, 01, 01, hora.Inicio.Hours, hora.Inicio.Minutes, hora.Inicio.Seconds);
                            dtpInicio.Value = fechainicio;

                            DateTime fechafin = new DateTime(2000, 01, 01, hora.Fin.Hours, hora.Fin.Minutes, hora.Fin.Seconds);
                            dtpFin.Value = fechafin;

                            lblCbx1.Text = "Turno";
                            cbx1.DataSource = negocio.Listar(TipoReferencia.Turno, null, null);
                            cbx1.DisplayMember = "Nombre";
                            cbx1.ValueMember = "Id";
                        }
                    }
                    else //NO TENGO NADA EN EL IDDESTINO, COSAS PARA AGREGAR
                    {
                        plNombre.Visible = true;

                        txtNombre.MaxLength = 2;

                    }
                    break;

                case TipoReferencia.Anio:

                    plPIN.Visible = true;

                    txtPIN.MaxLength = 4;

                    break;

                case TipoReferencia.Lugar:

                    plNombre.Visible = true;
                    plComboBox1.Visible= true;
                    plCombobox2.Visible = true;
                    plCheckBox1.Visible = true;
                    plCheckBox2.Visible = true;

                    txtNombre.MaxLength = 45;

                    //COSAS PARA EDITAR
                    if (IdDestino != null)
                    {
                        if (ObjetoDestino is Lugar lugar)
                        {
                            txtNombre.Text = lugar.Nombre;

                            lblCbx1.Text = "Tipo";
                            cbx1.DataSource = negocio.Listar(TipoReferencia.TipoDeLugar, null, null);
                            cbx1.DisplayMember = "Nombre";
                            cbx1.ValueMember = "Id";

                            lblCbx2.Text = "Piso";
                            cbx2.Items.Add(0);
                            cbx2.Items.Add(1);
                            cbx2.Items.Add(2);

                            chck1.Checked = lugar.IsClase;
                            chck2.Checked = lugar.IsUsoComun;

                        }
                    }
                    break;


                case TipoReferencia.Funcionario:

                    plNombre.Visible = true;
                    plApellido.Visible = true;
                    plComboBox1.Visible = true;
                    plCheckBox1.Visible = true;

                    txtNombre.MaxLength = 30;
                    txtApellido.MaxLength = 30;

                    chck1.Text = "Administrador";

                    lblCbx1.Text = "Cargo";
                    cbx1.DataSource = negocio.Listar(TipoReferencia.CargosFuncionarios, null, null);
                    cbx1.DisplayMember = "Cargo";
                    cbx1.ValueMember = "Id_Cargo";

                    //COSAS PARA EDITAR
                    if (IdDestino != null)
                    {
                        if (ObjetoDestino is Funcionario funcionario)
                        {
                            txtApellido.Text = funcionario.Apellido;
                            txtNombre.Text = funcionario.Nombre;


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


                default:
                    throw new Exception("Referencia no implementada en el formulario AgregarEditar");

            }


            //Si la referencia actual es lugar ponemos en visible el boton siguiente
            if (Sesion.ReferenciaActual == TipoReferencia.Lugar && IdDestino == null)
            {
                btnSiguiente.Visible = true;
            }



        }


        //Restricciones sobre los controles 
        #region
        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(Sesion.ReferenciaActual != TipoReferencia.Grupo)
            {
                Metodos.SoloLetras(e);
            }
            else
            {
                Metodos.SoloLetrasYNumeros(e);
            }
        }

        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            Metodos.SoloLetras(e);
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
            this.Close();
            Metodos.openChildForm(lista, Metodos.menuForm.plForms);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            MsgBox msg = null;
            Negocio negocio = new Negocio();
            if(IdDestino == null)
            {
                //Segun la referencia aplicamos unas validaciones u otras
                switch (Sesion.ReferenciaActual)
                {
                    case TipoReferencia.Alumno:
                        if (ValidarAgregarAlumnos() == RetornoValidacion.OK)
                        {
                            string ci = txtCI.Text;
                            Alumno alumno = new Alumno(txtNombre.Text, txtApellido.Text, int.Parse(ci), short.Parse(txtPIN.Text));
                            RetornoValidacion resultadoAgregarUsuario = negocio.Agregar(TipoReferencia.Usuario, alumno, ci);

                            switch (resultadoAgregarUsuario)
                            {
                                case RetornoValidacion.OK:
                                    RetornoValidacion resultadoAgregarAlumno = negocio.Agregar(TipoReferencia.Alumno, alumno, txtCI.Text);
                                    switch (resultadoAgregarAlumno)
                                    {
                                        case RetornoValidacion.OK:
                                            msg = new MsgBox("exito", "Alumno cargado exitosamente");
                                            Limpiar();
                                            break;

                                        case RetornoValidacion.YaExiste:
                                            msg = new MsgBox("error", "Ya hay un alumno en el sistema registrado con esa cedula");
                                            break;

                                        case RetornoValidacion.ErrorInesperadoBD:
                                            msg = new MsgBox("error", "Ha sucedido un error inesperado, intentelo de nuevo, si el error perdurase contacte con un administrador del sistema");
                                            break;
                                    }
                                    break;

                                case RetornoValidacion.YaExiste:
                                    msg = new MsgBox("error", "Ya hay un usuario en el sistema registrado con esa cedula");
                                    break;

                                case RetornoValidacion.ErrorInesperadoBD:
                                    msg = new MsgBox("error", "Ha sucedido un error inesperado, intentelo de nuevo, si el error perdurase contacte con un administrador del sistema");
                                    break;
                            }
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
                        }
                        break;

                    case TipoReferencia.Docente:
                        if (ValidarAgregarDocentes() == RetornoValidacion.OK)
                        {
                            string ci = txtCI.Text;
                            Docente docente = new Docente(txtNombre.Text, txtApellido.Text, int.Parse(ci), short.Parse(txtPIN.Text));
                            RetornoValidacion resultadoAgregarUsuario = negocio.Agregar(TipoReferencia.Usuario, docente, ci);

                            switch (resultadoAgregarUsuario)
                            {
                                case RetornoValidacion.OK:
                                    RetornoValidacion resultadoAgregarDocente = negocio.Agregar(TipoReferencia.Docente, docente, txtCI.Text);
                                    switch (resultadoAgregarDocente)
                                    {
                                        case RetornoValidacion.OK:
                                            msg = new MsgBox("exito", "Docente cargado exitosamente");
                                            Limpiar();
                                            break;

                                        case RetornoValidacion.YaExiste:
                                            msg = new MsgBox("error", "Ya hay un docente en el sistema registrado con esa cedula");
                                            break;

                                        case RetornoValidacion.ErrorInesperadoBD:
                                            msg = new MsgBox("error", "Ha sucedido un error inesperado, intentelo de nuevo, si el error perdurase contacte con un administrador del sistema");
                                            break;
                                    }
                                    break;

                                case RetornoValidacion.YaExiste:
                                    msg = new MsgBox("error", "Ya hay un usuario en el sistema registrado con esa cedula");
                                    break;

                                case RetornoValidacion.ErrorInesperadoBD:
                                    msg = new MsgBox("error", "Ha sucedido un error inesperado, intentelo de nuevo, si el error perdurase contacte con un administrador del sistema");
                                    break;
                            }
                        }
                        break;

                    case TipoReferencia.Funcionario:
                        if (ValidarAgregarFuncionarios() == RetornoValidacion.OK)
                        {
                            string ci = txtCI.Text;
                            Cargo auxcargo = new Cargo((byte)cbx1.SelectedValue);
                            Funcionario funcionario = new Funcionario(txtNombre.Text, txtApellido.Text, int.Parse(ci), short.Parse(txtPIN.Text), auxcargo, chck1.Checked, dtpFechaIngreso.Value);
                            RetornoValidacion resultadoAgregarUsuario = negocio.Agregar(TipoReferencia.Usuario, funcionario, ci);

                            switch (resultadoAgregarUsuario)
                            {
                                case RetornoValidacion.OK:
                                    RetornoValidacion resultadoAgregarFuncionario = negocio.Agregar(TipoReferencia.Funcionario, funcionario, txtCI.Text);
                                    switch (resultadoAgregarFuncionario)
                                    {
                                        case RetornoValidacion.OK:
                                            msg = new MsgBox("exito", "Funcionario cargado exitosamente");
                                            Limpiar();
                                            break;

                                        case RetornoValidacion.YaExiste:
                                            msg = new MsgBox("error", "Ya hay un funcionario en el sistema registrado con esa cedula");
                                            break;

                                        case RetornoValidacion.ErrorInesperadoBD:
                                            msg = new MsgBox("error", "Ha sucedido un error inesperado, intentelo de nuevo, si el error perdurase contacte con un administrador del sistema");
                                            break;
                                    }
                                    break;

                                case RetornoValidacion.YaExiste:
                                    msg = new MsgBox("error", "Ya hay un usuario en el sistema registrado con esa cedula");
                                    break;

                                case RetornoValidacion.ErrorInesperadoBD:
                                    msg = new MsgBox("error", "Ha sucedido un error inesperado, intentelo de nuevo, si el error perdurase contacte con un administrador del sistema");
                                    break;
                            }
                        }
                        break;

                    case TipoReferencia.Grupo:
                        if (ValidarAgregarGrupo() == RetornoValidacion.OK)
                        {
                            string nombre = txtNombre.Text;
                            Turno turno = new Turno((byte)cbx1.SelectedValue);
                            Orientacion orientacion = new Orientacion((byte)cbx2.SelectedValue);
                            int anio = (int)cbx3.SelectedValue;
                            Grupo grupo = new Grupo(nombre, turno, orientacion, anio);
                            RetornoValidacion resultadoAgregar = negocio.Agregar(TipoReferencia.Grupo, grupo, nombre);

                            switch (resultadoAgregar)
                            {
                                case RetornoValidacion.OK:
                                    msg = new MsgBox("exito", "Grupo cargado exitosamente");
                                    Limpiar();
                                    break;

                                case RetornoValidacion.YaExiste:
                                    msg = new MsgBox("error", "El nombre de grupo que intenta ingresar ya esta registrado en el sistema.");
                                    break;

                                case RetornoValidacion.ErrorInesperadoBD:
                                    msg = new MsgBox("error", "Ha sucedido un error inesperado, intentelo de nuevo, si el error perdurase contacte con un administrador del sistema");
                                    break;
                            }
                        }
                        break;

                    case TipoReferencia.Hora:
                        if (ValidarAgregarHora() == RetornoValidacion.OK)
                        {
                            Turno turno = new Turno((byte)cbx1.SelectedValue);
                            (byte nid, Turno turno) id = (byte.Parse(txtCI.Text), turno);

                            DateTime fechaseleccionadainicio = dtpInicio.Value;
                            DateTime medianocheinicio = new DateTime(fechaseleccionadainicio.Year, fechaseleccionadainicio.Month, fechaseleccionadainicio.Day, 0, 0, 0);
                            TimeSpan horaInicio = fechaseleccionadainicio - medianocheinicio;

                            DateTime fechaseleccionadafin= dtpFin.Value;
                            DateTime medianochefin = new DateTime(fechaseleccionadafin.Year, fechaseleccionadafin.Month, fechaseleccionadafin.Day, 0, 0, 0);
                            TimeSpan horaFin = fechaseleccionadafin - medianochefin;


                            Hora hora = new Hora(id, horaInicio, horaFin);
                            RetornoValidacion resultadoAgregar = negocio.Agregar(TipoReferencia.Hora, hora, id.nid, id.turno.Id);

                            switch (resultadoAgregar)
                            {
                                case RetornoValidacion.OK:
                                    msg = new MsgBox("exito", "Hora cargada exitosamente");
                                    Limpiar();
                                    break;

                                case RetornoValidacion.YaExiste:
                                    msg = new MsgBox("error", "Ya existe una hora con ese numero asignada a ese turno en el sistema.");
                                    break;

                                case RetornoValidacion.ErrorInesperadoBD:
                                    msg = new MsgBox("error", "Ha sucedido un error inesperado, intentelo de nuevo, si el error perdurase contacte con un administrador del sistema");
                                    break;
                            }
                        }
                        break;

                    case TipoReferencia.Lugar:
                        if (ValidarAgregarLugar() == RetornoValidacion.OK)
                        {
                          /*  string nombre = txtNombre.Text;
                            TipoLugar tipo = new TipoLugar((byte)cbx1.SelectedValue);
                            byte piso = (byte)cbx2.SelectedValue;
                            //int coordenada_x = ;
                            //int coordenada_y = ;
                            bool isClase = chck1.Checked;
                            bool isUsoComun = chck2.Checked;
                           // Lugar lugar = new Lugar(nombre, tipo, piso, coordenada_x, coordenada_y, isClase, isUsoComun);
                           // RetornoValidacion resultadoAgregar = negocio.Agregar(TipoReferencia.Lugar, lugar, nombre);

                            switch (resultadoAgregar)
                            {
                                case RetornoValidacion.OK:
                                    msg = new MsgBox("exito", "Lugar cargado exitosamente");
                                    Limpiar();
                                    break;

                                case RetornoValidacion.YaExiste:
                                    msg = new MsgBox("error", "Ya existe un lugar con ese nombre en el sistema. Por favor seleccione uno nuevo.");
                                    break;

                                case RetornoValidacion.ErrorInesperadoBD:
                                    msg = new MsgBox("error", "Ha sucedido un error inesperado, intentelo de nuevo, si el error perdurase contacte con un administrador del sistema");
                                    break;
                            }*/
                        }
                        break;

                    case TipoReferencia.Materia:
                        if (ValidarMateriaTurnoOrientacion() == RetornoValidacion.OK)
                        {
                            string nombre = txtNombre.Text;
                            Materia materia = new Materia(nombre);
                            RetornoValidacion resultadoAgregar = negocio.Agregar(TipoReferencia.Materia, materia, nombre);

                            switch (resultadoAgregar)
                            {
                                case RetornoValidacion.OK:
                                    msg = new MsgBox("exito", "Materia cargada exitosamente");
                                    Limpiar();
                                    break;

                                case RetornoValidacion.YaExisteNombre:
                                    msg = new MsgBox("error", "La materai que intenta ingresar ya esta registrada en el sistema.");
                                    break;

                                case RetornoValidacion.ErrorInesperadoBD:
                                    msg = new MsgBox("error", "Ha sucedido un error inesperado, intentelo de nuevo, si el error perdurase contacte con un administrador del sistema");
                                    break;
                            }
                        }
                        break;

                    case TipoReferencia.Orientacion:
                        if (ValidarMateriaTurnoOrientacion() == RetornoValidacion.OK)
                        {
                            string nombre = txtNombre.Text;
                            Orientacion orientacion = new Orientacion(nombre);
                            RetornoValidacion resultadoAgregar = negocio.Agregar(TipoReferencia.Orientacion, orientacion, nombre);

                            switch (resultadoAgregar)
                            {
                                case RetornoValidacion.OK:
                                    msg = new MsgBox("exito", "Orientacion cargada exitosamente");
                                    Limpiar();
                                    break;

                                case RetornoValidacion.YaExisteNombre:
                                    msg = new MsgBox("error", "La orientacion que intenta ingresar ya esta registrada en el sistema.");
                                    break;

                                case RetornoValidacion.ErrorInesperadoBD:
                                    msg = new MsgBox("error", "Ha sucedido un error inesperado, intentelo de nuevo, si el error perdurase contacte con un administrador del sistema");
                                    break;
                            }
                        }
                        break;

                    case TipoReferencia.Turno:
                        if (ValidarMateriaTurnoOrientacion() == RetornoValidacion.OK)
                        {
                            string nombre = txtNombre.Text;
                            Turno turno = new Turno(nombre);
                            RetornoValidacion resultadoAgregar = negocio.Agregar(TipoReferencia.Turno, turno, nombre);

                            switch (resultadoAgregar)
                            {
                                case RetornoValidacion.OK:
                                    msg = new MsgBox("exito", "Turno cargado exitosamente");
                                    Limpiar();
                                    break;

                                case RetornoValidacion.YaExisteNombre:
                                    msg = new MsgBox("error", "El turno que intenta ingresar ya esta registrado en el sistema.");
                                    break;

                                case RetornoValidacion.ErrorInesperadoBD:
                                    msg = new MsgBox("error", "Ha sucedido un error inesperado, intentelo de nuevo, si el error perdurase contacte con un administrador del sistema");
                                    break;
                            }
                        }
                        break;
                }

            }

            if (msg != null)
            {
                msg.ShowDialog();
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

            //Validar vacio
            if (validaciones.ValidarVacio(txtCI.Text))
            {
                MsgBox msg = new MsgBox("error", "Debe completar el campo de CI");
                msg.ShowDialog();
                txtCI.Focus();
                respuesta = RetornoValidacion.ErrorDeFormato;
            }
            else if (validaciones.ValidarVacio(txtPIN.Text))
            {
                MsgBox msg = new MsgBox("error", "Debe completar el campo de PIN");
                msg.ShowDialog();
                txtPIN.Focus();
                respuesta = RetornoValidacion.ErrorDeFormato;
            }
            else if (validaciones.ValidarVacio(txtNombre.Text))
            {
                MsgBox msg = new MsgBox("error", "Debe completar el campo de Nombre");
                msg.ShowDialog();
                txtNombre.Focus();
                respuesta = RetornoValidacion.ErrorDeFormato;
            }
            else if (validaciones.ValidarVacio(txtApellido.Text))
            {
                MsgBox msg = new MsgBox("error", "Debe completar el campo de Apellido");
                msg.ShowDialog();
                txtApellido.Focus();
                respuesta = RetornoValidacion.ErrorDeFormato;
            }
            else if (!validaciones.ValidarCI(txtCI.Text))// Validar la cédula
            {
                txtCI.Focus();
                MsgBox msg = new MsgBox("error", "Cédula no válida.");
                msg.ShowDialog();
                respuesta = RetornoValidacion.ErrorDeFormato;
            }
            else if (!validaciones.ValidarPIN(txtPIN.Text))// Validar el PIN
            {
                txtPIN.Focus();
                MsgBox msg = new MsgBox("error", "PIN no válido.");
                msg.ShowDialog();
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
                MsgBox msg = new MsgBox("error", "Debe completar el campo de Año antes de continuar");
                msg.ShowDialog();
                txtPIN.Focus();
                respuesta = RetornoValidacion.ErrorDeFormato;
            }
            else if (!validaciones.ValidarAnio(short.Parse(txtPIN.Text)))
            {
                MsgBox msg = new MsgBox("error", "El año no puede ser mayor que el año actual + dos ni anterior a este");
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
                MsgBox msg = new MsgBox("error", "Debe seleccionar un cargo");
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

            //Validar vacio
            if (validaciones.ValidarVacio(txtNombre.Text))
            {
                MsgBox msg = new MsgBox("error", "Debe completar el campo de Nombre");
                msg.ShowDialog();
                txtNombre.Focus();
                respuesta = RetornoValidacion.ErrorDeFormato;
            }
            else if (cbx1.SelectedIndex == -1)
            {
                MsgBox msg = new MsgBox("error", "Debe seleccionar una orientacion");
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
            if (validaciones.ValidarVacio(txtCI.Text))
            {
                MsgBox msg = new MsgBox("error", "Debe ingresar un numero de hora");
                msg.ShowDialog();
                txtCI.Focus();
                respuesta = RetornoValidacion.ErrorDeFormato;
            }
            else if (cbx1.SelectedIndex == -1)
            {
                MsgBox msg = new MsgBox("error", "Debe seleccionar un turno");
                msg.ShowDialog();
                cbx1.Focus();
                respuesta = RetornoValidacion.ErrorDeFormato;
            }
            else if (dtpInicio.Value > dtpFin.Value)
            {
                MsgBox msg = new MsgBox("error", "La hora inicial no puede ser mayor a la hora final");
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

            //Validar vacio
            if (validaciones.ValidarVacio(txtNombre.Text))
            {
                MsgBox msg = new MsgBox("error", "Debe ingresar un nombre");
                msg.ShowDialog();
                txtNombre.Focus();
                respuesta = RetornoValidacion.ErrorDeFormato;
            }
            else if (cbx1.SelectedIndex == -1)
            {
                MsgBox msg = new MsgBox("error", "Debe seleccionar un tipo de lugar");
                msg.ShowDialog();
                cbx1.Focus();
                respuesta = RetornoValidacion.ErrorDeFormato;
            }
            else if (cbx2.SelectedIndex == -1)
            {
                MsgBox msg = new MsgBox("error", "Debe seleccionar un piso");
                msg.ShowDialog();
                cbx1.Focus();
                respuesta = RetornoValidacion.ErrorDeFormato;
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
                MsgBox msg = new MsgBox("error", "Debe completar ingresar un nombre antes de continuar");
                msg.ShowDialog();
                txtNombre.Focus();
                respuesta = RetornoValidacion.ErrorDeFormato;
            }

            return respuesta;
        }

        #endregion

       
    }
}
