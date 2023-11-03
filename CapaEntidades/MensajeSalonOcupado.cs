namespace CapaEntidades
{
    public class MensajeSalonOcupado
    {
        private string nombreDocente;
        private string horaInicio;
        private string horaFin;
        private string nombreGrupo;
        private string nombreDia;

        public MensajeSalonOcupado(string nombreDocente, string nombreGrupo, string horaInicio, string horaFin, string nombreDia)
        {
            this.horaFin = horaFin;
            this.horaInicio = horaInicio;
            this.nombreGrupo = nombreGrupo;
            this.nombreDocente = nombreDocente;
            this.nombreDia = nombreDia;
        }

        public string NombreDocente { get { return nombreDocente;} }
        public string HoraInicio { get {  return horaInicio;} } 
        public string HoraFin { get {  return horaFin;} }
        public string NombreGrupo { get {  return nombreGrupo;} }
        public string NombreDia { get {  return nombreDia;} }
    }
}
