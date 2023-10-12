using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Horario
    {
        private Turno turno;
        private string grupo;
        private Materia materia;
        private Docente docente;
        private Dia_Semana dia; //Lunes, Martes, Miercoles, Jueves, Viernes, Sabado
        private Lugar salon;
        private Lugar salonTemporal;
        private List<Hora> horas;

        public Horario (string grupo, Materia materia, Docente docente, Dia_Semana dia, Lugar salon, List<Hora> horas, Turno turno)
        {
            this.grupo = grupo;
            this.materia = materia;
            this.docente = docente;
            this.dia = dia;
            this.salon = salon;
            this.horas = horas;
            this.turno = turno;
        }
        
        public string Grupo { get { return grupo; } }
        public Materia Materia { get {  return materia; } }
        public Docente Docente { get {  return docente; } }
        public Lugar Salon { get {  return salon; } }
        public Dia_Semana Dia { get { return dia; } }
        public Lugar SalonTemporal { get {  return salonTemporal; } }
        public List<Hora> Hora { get {  return horas; } }

        public string StrListaHoras()
        {
            string str = "";

            foreach (Hora hora in horas)
            {
                str += hora.ToString()+", ";
            }
            str.Remove(str.Length - 2);
            return str;
        }
    }
}
