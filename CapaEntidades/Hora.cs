using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Hora
    {
        //Representa la entidad horario de la bd, es parte de la clase horario
        private (byte nid, Turno turno) id;
        private TimeSpan inicio;
        private TimeSpan fin;

        public (byte, Turno) Id { get { return id; } }
        public TimeSpan Inicio { get { return inicio; } }
        public TimeSpan Fin { get { return fin; } }
        public Turno Turno { get { return id.turno; } }
        public byte Nid { get { return id.nid; } }

        public Hora((byte, Turno) id, TimeSpan inicio, TimeSpan fin)
        {
            this.id = id;
            this.inicio = inicio;
            this.fin = fin;
        }

        public override string ToString()
        {
            return " " + Nid.ToString() + "-" + Turno.ToString();
        }
    }
}
