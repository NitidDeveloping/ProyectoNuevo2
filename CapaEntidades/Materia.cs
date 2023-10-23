using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Materia
    {
        private string nombre;
        private ushort id;

        public string Nombre { get { return nombre; } }
        public ushort Id { get { return id; } }

        public Materia(ushort id, string nombre)
        {
            this.nombre = nombre;
            this.id = id;
        }

        public Materia(string nombre)
        {
            this.nombre = nombre;
        }

        public Materia(ushort id)
        {
            this.id = id;
        }
    }
}
