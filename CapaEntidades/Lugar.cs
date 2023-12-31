﻿namespace CapaEntidades
{
    public class Lugar
    {
        //Representa la entidad lugar de la bd
        private ushort id;
        private string nombre;

        //Coordenadas para el mapa
        private int coordenada_x;
        private int coordenada_y;

        private TipoLugar tipo;// Hay que poner el enum de la bd
        private byte piso;
        private bool isClase;
        private bool isUsoComun;
        private bool ocupado;

        public Lugar(ushort id, string nombre, int coordenada_x, int coordenada_y, byte piso, bool isClase, bool isUsoComun, TipoLugar tipo, bool ocupado)
        {
            this.id = id;
            this.nombre = nombre;
            this.coordenada_x = coordenada_x;
            this.coordenada_y = coordenada_y;
            this.piso = piso;
            this.tipo = tipo;
            this.isClase = isClase;
            this.isUsoComun = isUsoComun;
            this.ocupado = ocupado;
        }

        public Lugar(string nombre, TipoLugar tipo, byte piso, int coordenada_x, int coordenada_y, bool isClase, bool isUsoComun)
        {
            this.nombre = nombre;
            this.coordenada_x = coordenada_x;
            this.coordenada_y = coordenada_y;
            this.tipo = tipo;
            this.piso = piso;
            this.isClase = isClase;
            this.isUsoComun = isUsoComun;
        }

        public Lugar(string nombre, TipoLugar tipo, bool isClase, bool isUsoComun)
        {
            this.nombre = nombre;
            this.tipo = tipo;
            this.isClase = isClase;
            this.isUsoComun = isUsoComun;
        }


        public Lugar(ushort id, string nombre, TipoLugar tipo, byte piso, int coordenada_x, int coordenada_y, bool isClase, bool isUsoComun)
        {
            this.id = id;
            this.nombre = nombre;
            this.coordenada_x = coordenada_x;
            this.coordenada_y = coordenada_y;
            this.tipo = tipo;
            this.piso = piso;
            this.isClase = isClase;
            this.isUsoComun = isUsoComun;
        }

        public Lugar(ushort id, string nombre, TipoLugar tipo, int coordenada_x, int coordenada_y, byte piso) 
        {
            this.id= id;
            this.nombre = nombre;
            this.tipo= tipo;
            this.coordenada_x = coordenada_x;
            this.coordenada_y = coordenada_y;
            this.piso = piso;

        }

        public Lugar (ushort id, string nombre)
        {
            this.id = id;
            this.nombre = nombre ;
        }
        public Lugar(ushort id)
        {
            this.id = id;
        }

        public ushort ID { get { return id; } }
        public string Nombre { get { return nombre; } }
        public int Coordenada_x { get { return coordenada_x; } }
        public int Coordenada_y { get { return coordenada_y; } }
        public bool IsClase { get { return isClase; } }
        public bool IsUsoComun { get { return isUsoComun; } }
        public byte Piso { get { return piso; } }
        public TipoLugar Tipo { get { return tipo; } }
        public bool Ocupado { get { return ocupado; } }

    }
}
