namespace CapaEntidades
{
    public class Docente : Usuario
    {
        public Docente(string nombre, string apellido, int ci, short pin) : base(nombre, apellido, ci, pin)
        {

        }
        public Docente(string nombre, string apellido, int ci) : base(nombre, apellido, ci)
        {
        }
    }
}

