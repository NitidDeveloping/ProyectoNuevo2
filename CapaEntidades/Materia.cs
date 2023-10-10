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

        public Materia(string nombre, ushort id)
        {
            this.nombre = nombre;
            this.id = id;
        }
    }
}
