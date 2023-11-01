namespace CapaEntidades
{
    public class UbicacionClase
    {
        private string nombre = null;
        private int coordenadaX = 0;
        private int coordenadaY = 0;
        private int piso = 0;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public int CoordenadaX
        {
            get { return coordenadaX; }
            set { coordenadaX = value; }
        }

        public int CoordenadaY
        {
            get { return coordenadaY; }
            set { coordenadaY = value; }
        }

        public int Piso
        {
            get { return piso; }
            set { piso = value; }
        }
    }
}
