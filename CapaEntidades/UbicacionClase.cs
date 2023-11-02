namespace CapaEntidades
{
    public class UbicacionClase
    {
        private string grupo = null;
        private string salon = null;
        private string materia = null;
        private int coordenadaX = 0;
        private int coordenadaY = 0;
        private int piso = 0;

        public string Grupo
        {
            get { return grupo; }
            set { grupo = value; }
        }
        public string Salon
        {
            get { return salon; }
            set { salon = value; }
        }
        public string Materia
        {
            get { return materia; }
            set { materia = value; }
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
