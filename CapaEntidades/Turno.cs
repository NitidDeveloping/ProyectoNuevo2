using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Turno
    {
        private byte id;
        private string nombre;

        public byte Id { get { return id; }}
        public string Nombre { get { return nombre; } }

        public Turno(byte id, string nombre)
        {
            this.id = id;
            this.nombre = nombre;
        }

        public Turno(byte id)
        {
            this.id = id;
        }

        public Turno(string nombre)
        {
            this.nombre = nombre;
        }
    }

}
