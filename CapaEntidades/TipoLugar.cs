namespace CapaEntidades
{
    public class TipoLugar
    {
        private byte id;
        private string nombre;

        public byte Id { get { return id; } }
        public string Nombre { get { return nombre; } }

        public TipoLugar(byte id, string nombre)
        {
            this.id = id;
            this.nombre = nombre;
        }

        public TipoLugar(byte id)
        {
            this.id = id;
        }
    }
}
