namespace CapaEntidades
{
    public class Dia_Semana
    {
        private byte id;
        private string nombre;

        public byte Id { get { return id; } }
        public string Nombre { get { return nombre; } }

        public Dia_Semana(byte id, string nombre)
        {
            this.id = id;
            this.nombre = nombre;
        }

        public Dia_Semana(byte id)
        {
            this.id = id;
        }
    }
}
