using System;

namespace CapaEntidades
{
    public class Hora
    {
        //Representa la entidad horario de la bd, es parte de la clase horario
        private (byte nid, Turno turno) id;
        private TimeSpan inicio;
        private TimeSpan fin;

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

        public Hora((byte, Turno) id)
        {
            this.id = id;
        }
    }
}
