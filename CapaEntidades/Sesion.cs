using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Sesion
    {
        //Clase para controlar quien esta logueado en este momento
        private static int loggedCi;
        private static int loggedPin;
        private static string loggedNombre;
        private static TipoRol loggedRol;
        private static TipoReferencia referenciaActual;

        public static string LoggedNombre { get { return loggedNombre; } }
        public static TipoRol LoggedRol { get { return loggedRol; } }
        public static int LoggedCi { get { return loggedCi; } }
        public static int LoggedPin { get { return loggedPin; } }
        public static TipoReferencia ReferenciaActual { get { return referenciaActual; } }

        public void LogIn(string nombre, TipoRol rol, int ci, int pin)
        {
            loggedCi = ci;
            loggedPin = pin;
            loggedNombre = nombre;
            loggedRol = rol;
        }

        //Logueo como visitante
        public void LogIn()
        {
            loggedCi = 0;
            loggedPin = 0;
            loggedNombre = "Visitante";
            loggedRol = TipoRol.Visitante;
        }

        public void LogOut()
        {
            loggedCi = 0;
            loggedPin = 0;
            loggedNombre = "default";
            loggedRol = TipoRol.Default;
        }

        public void SetReferenciaActual(TipoReferencia referencia)
        {
            referenciaActual = referencia;
        }

    }
}
