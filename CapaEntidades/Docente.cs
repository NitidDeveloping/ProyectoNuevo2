﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Docente : Usuario
    {
        public Docente(string nombre, string apellido, int ci, short pin) : base(nombre, apellido, ci, pin)
        {

        }
        public Docente(string nombre, string apellido, int ci) : base(nombre, apellido, ci)
        {
        }
    }
}

