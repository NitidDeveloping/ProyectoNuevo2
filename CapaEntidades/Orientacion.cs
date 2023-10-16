using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Orientacion
    {
        private byte id;
        private string nombre;

        public byte Id { get { return id; } }
        public string Nombre { get { return nombre; } }

        public Orientacion(byte id, string nombre)
        {
            this.id = id;
            this.nombre = nombre;
        }

        public Orientacion(byte id)
        {
            this.id = id;
        }

        public Orientacion(string nombre)
        {
            this.nombre = nombre;
        }

    }
}
