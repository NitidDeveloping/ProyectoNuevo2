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
                        plComboBox1.Visible = true;

                        txtCI.MaxLength = 8; //Seteamos la longitud maxima en el campo de ci para que pueda poner como maximo 8 caracteres
                        txtPIN.MaxLength = 4;//Seteamos la longitud maxima en el campo de pin para que pueda poner como maximo 4 caracteres

                        //Asignamos valores al combobox
                        lblCbx1.Text = "Grupos"; // Seteamos nombre del label
                        cbx1.DataSource = negocio.Listar(TipoReferencia.Grupo, null, null); //Seteamos datasource en una lista con todos los grupos existentes en la base de datos
                        cbx1.DisplayMember = "Nombre"; //Seteamos la columna que se le mostrara al usuario en la columna del nombre del grupo para que le resulte amigable al usuario
                        cbx1.ValueMember = "Nombre"; // Seteamos el valor con el que trabajara el sistema en la id del grupo (en este caso ambos son lo mismo)

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

                            lblCbx2.Text = "Anio";
                            cbx2.DataSource = negocio.Listar(TipoReferencia.Anio, null, null);
                            cbx2.DisplayMember = "Anio";
                            cbx2.ValueMember = "Anio";


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
                    plCombobox2.Visible = true;

                    txtNombre.MaxLength = 30;
                    txtApellido.MaxLength = 30;

                    //COSAS PARA EDITAR
                    if (IdDestino != null)
                    {
                        if (ObjetoDestino is Funcionario funcionario)
                        {
                            txtApellido.Text = funcionario.Apellido;
                            txtNombre.Text = funcionario.Nombre;

                            lblCbx1.Text = "Cargo";
                            cbx1.DataSource = negocio.Listar(TipoReferencia.CargosFuncionarios, null, null);
                            cbx1.DisplayMember = "Cargo";
                            cbx1.ValueMember = "Id_Cargo";

                            lblCbx2.Text = "Tipo";
                            cbx2.DataSource = negocio.Listar(TipoReferencia.Funcionario, null, null);
                            cbx2.Items.Add(1);
                            cbx2.Items.Add(2);

                        }
                    }
                    else //NO TENGO NADA EN EL IDDESTINO, COSAS PARA AGREGAR
                    {
                        plCI.Visible = true;
                        plPIN.Visible = true;
                        plCheckBox1.Visible = true;

                        txtCI.MaxLength = 8;
                        txtPIN.MaxLength = 4;

                        chck1.Text = "Administrador";

                    }
                    
                    break;


                default:
                    throw new Exception("Referencia no implementada en el formulario AgregarEditar");

            }
        }


        //Restricciones sobre los controles 
        #region
        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            Metodos.SoloLetras(e);
        }

        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            Metodos.SoloLetras(e);
        }

        #endregion

        private void txtCI_KeyPress(object sender, KeyPressEventArgs e)
        {
            Metodos.SoloNumeros(e);
        }

        private void txtPIN_KeyPress(object sender, KeyPressEventArgs e)
        {
            Metodos.SoloNumeros(e);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Lista lista = new Lista();
            this.Close();
            Metodos.openChildForm(lista, Metodos.menuForm.plForms);
        }
    }
}
