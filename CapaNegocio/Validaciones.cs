using System;

namespace CapaNegocio
{
    public class Validaciones
    {
        public bool ValidarCI(string txtCI)
        {
            if (txtCI.Length == 8)
            {
                return true;
            }
            return false;
        }

        public bool ValidarPIN(string txtPIN)
        {
            if (txtPIN.Length == 4)
            {
                return true;
            }
            return false;
        }

        public bool ValidarVacio(string txt)
        {
            return txt.Trim() == string.Empty;
        }

        public bool ValidarAnio(short anio)
        {
            if (anio > DateTime.Now.Year + 2 || anio < DateTime.Now.Year)
            {
                return false;
            }
            return true;
        }


    }
}
