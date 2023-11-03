namespace CapaEntidades
{
    public class Grupo
    {
        //Representa la entidad grupo de la bd
        private string nombre;
        private Orientacion orientacion;
        private Turno turno;
        private int anio;
        private byte lista;

        public string Nombre { get { return nombre; } }
        public Orientacion Orientacion { get { return orientacion; } }
        public int Anio { get { return anio; } }
        public Turno Turno { get { return turno; } }
        public byte Lista { get { return lista; } }

        public Grupo(string nombre, Turno turno, Orientacion orientacion, int anio)
        {
            this.nombre = nombre;
            this.orientacion = orientacion;
            this.turno = turno;
            this.anio = anio;
        }

        public Grupo(string nombre, Orientacion orientacion, int anio)
        {
            this.nombre = nombre;
            this.orientacion = orientacion;
            this.anio = anio;
        }

        public Grupo(string nombre, Turno turno, Orientacion orientacion, int anio, byte lista)
        {
            this.nombre = nombre;
            this.orientacion = orientacion;
            this.turno = turno;
            this.anio = anio;
            this.lista = lista;
        }

        public Grupo(string nombre)
        {
            this.nombre = nombre;
        }
    }
}
