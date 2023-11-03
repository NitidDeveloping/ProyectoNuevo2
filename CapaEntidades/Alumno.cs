namespace CapaEntidades
{
    public class Alumno : Usuario
    {
        private string strgrupos;

        public string Strgrupos { get => strgrupos; }

        public Alumno(string nombre, string apellido, int ci, short pin) : base(nombre, apellido, ci, pin)
        {
        }
        public Alumno(string nombre, string apellido, int ci, string strgrupos) : base(nombre, apellido, ci)
        {
            this.strgrupos = strgrupos;
        }

        public Alumno(string nombre, string apellido, int ci) : base(nombre, apellido, ci)
        {
        }

    }
}
