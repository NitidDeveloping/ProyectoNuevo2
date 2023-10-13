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
                    else
                    {
                        //Mostramos paneles
                        plCI.Visible = true;
                        plPIN.Visible = true;
                        plComboBox1.Visible = true;

                        txtCI.MaxLength = 8; //Seteamos la longitud maxima en el campo de ci para que pueda poner como maximo 8 caracteres
                        txtPIN.MaxLength = 4;//Seteamos la longitud maxima en el campo de pin para que pueda poner como maximo 4 caracteres

                        //Asignamos valores al combobox
                        lblCbx1.Text = "Grupos"; // Seteamos nombre del label
                        cbx1.DataSource = negocio.Listar(TipoReferencia.Grupo, null, null, null); //Seteamos datasource en una lista con todos los grupos existentes en la base de datos
                        cbx1.DisplayMember = "Nombre"; //Seteamos la columna que se le mostrara al usuario en la columna del nombre del grupo para que le resulte amigable al usuario
                        cbx1.ValueMember = "Nombre"; // Seteamos el valor con el que trabajara el sistema en la id del grupo (en este caso ambos son lo mismo)

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
