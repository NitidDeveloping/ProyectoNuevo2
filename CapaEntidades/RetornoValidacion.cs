using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public enum RetornoValidacion
    {
        ErrorDeFormato,
        ErrorInesperadoBD,
        ErrorInesperadoBDCategorizacion,
        NoExiste,
        OK,
        YaExiste,
        YaExisteNombre
    }
}
