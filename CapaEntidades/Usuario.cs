using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades //De la capa de presentación y la de negocios puedo enviar información directamente a la capa entidad
                        //y esta va a estar ahí para ser obtenida cuando se necesite
{

    public class Usuario
    {
        //Agregamos las propiedades según los campos de la tabla.
        private string nombre;
        private string apellido;
        private int ci;
        private short pin;

        public Usuario(string nombre, string apellido, int ci, short pin)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.ci = ci;
            this.pin = pin;
        }

        public Usuario(string nombre, string apellido, int ci)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.ci = ci;
        }
        public Usuario(string nombre, string apellido)
        {
            this.nombre = nombre;
            this.apellido = apellido;
        }

        public int CI
        {
            get { return ci; }
        }

        public short PIN
        {
            get { return pin; }
        }

        public string Nombre
        {
            get { return nombre; }
        }

        public string Apellido
        {
            get { return apellido; }
        }
    }

}

