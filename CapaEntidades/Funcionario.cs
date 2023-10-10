using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Funcionario : Usuario
    {
        private bool isAdmn;
        private Cargo cargo;
        private DateTime fecha_Ingreso;
        public Funcionario(string nombre, string apellido, int ci, short pin, Cargo cargo, bool isAdmn, DateTime fecha_ingreso) : base(nombre, apellido, ci, pin)
        {
            this.isAdmn = isAdmn;
            this.cargo = cargo;
            fecha_Ingreso = fecha_ingreso;
        }

        public Funcionario(string nombre, string apellido, int ci, Cargo cargo, bool isAdmn, DateTime fecha_ingreso) : base(nombre, apellido, ci)
        {
            this.isAdmn = isAdmn;
            this.cargo = cargo;
            fecha_Ingreso=fecha_ingreso;
        }

        public bool IsAdmn
        {
            get { return isAdmn; }
        
        }
        public Cargo Cargo
        {
            get { return cargo; }
        }

        public DateTime FechaIngreso
        {
            get { return fecha_Ingreso; }
        }
    }
}
