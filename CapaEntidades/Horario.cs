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
        private Dia_Semana dia;
        private Lugar salon;
        private Lugar salonTemporal;
        private List<Hora> horas;
        private TimeSpan inicio;
        private TimeSpan fin;
        private string strhoras;

        public Horario(string grupo, Materia materia, Dia_Semana dia, List<Hora> horas, Turno turno, TimeSpan inicio, TimeSpan fin)
        {
            this.grupo = grupo;
            this.materia = materia;
            this.dia = dia;
            this.horas = horas;
            this.turno = turno;
            this.inicio = inicio;
            this.fin = fin;
        }

        public Horario(string grupo, Materia materia, Docente docente, Dia_Semana dia, Lugar salon, Lugar salonT, string horas, Turno turno, TimeSpan inicio, TimeSpan fin)
        {
            this.grupo = grupo;
            this.materia = materia;
            this.docente = docente;
            this.dia = dia;
            this.salon = salon;
            this.strhoras = horas;
            this.turno = turno;
            this.salonTemporal = salonT;
            this.inicio = inicio;
            this.fin = fin;
        }

        public Horario(string grupo, Materia materia, Dia_Semana dia, Lugar salon, List<Hora> horas, Turno turno)
        {
            this.grupo = grupo;
            this.materia = materia;
            this.dia = dia;
            this.salon = salon;
            this.horas = horas;
            this.turno = turno;
        }

        public Horario(string grupo, Materia materia, Dia_Semana dia, List<Hora> horas, Turno turno)
        {
            this.grupo = grupo;
            this.materia = materia;
            this.dia = dia;
            this.horas = horas;
            this.turno = turno;
        }

        public string Grupo { get { return grupo; } }
        public Materia Materia { get { return materia; } }
        public Docente Docente { get { return docente; } }
        public Lugar Salon { get { return salon; } }
        public Dia_Semana Dia { get { return dia; } }
        public Lugar SalonTemporal { get { return salonTemporal; } }
        public List<Hora> Horas { get { return horas; } }
        public Turno Turno { get { return turno; } }
        public string StrHoras { get { return strhoras; } }
        public TimeSpan Inicio { get { return inicio; } }
        public TimeSpan Fin { get { return fin; } }

    }
}
