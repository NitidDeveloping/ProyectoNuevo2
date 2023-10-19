using CapaEntidades;
using MySqlConnector;
using System;
using System.Data;

namespace CapaDatos
{
    class Conector
    {
        private string server;
        private string uid;
        private string pwd;
        private string database;
        private Sesion sesion;
        private string userVariables;
        
        private static Conector conn = null;


        private Conector()  //Metodo en private para que no se tenga acceso al constructor
        {
            this.database = "nitid"; //Establecemos los valores en el constructor.
            this.server = "localhost";
            this.uid = "root";
            this.pwd = "1234";
               
        }

        //Método público para devolver string de conexión

        public MySqlConnection crearConexion()
        {
            MySqlConnection conn = new MySqlConnection();

            try
            {
                //Crear cadena de conexión

                conn.ConnectionString = "server=" + this.server + ";uid=" + this.uid + ";pwd=" + this.pwd + ";database=" + this.database + ";Allow User Variables = true";
            }
            catch(Exception ex)
            {
                conn = null;
                throw ex; //En caso de que no se conecte, muestra un error.
            }
            return conn; //Retorno la cadena de conexión.
        }


        //Creamos un método para generar una instancia al constructor dentro de esta clase.
        //Esto para poder activar el constructor y asignarle los valores, más que nada para mayor seguridad.

        public static Conector crearInstancia() //Al crear este método automáticamente le asigna los valores a las variables privadas.
        {
            if(conn == null)
            {
                conn = new Conector(); //Crea la conexión en caso de que no exista
            }
            return conn; //Sino simplemente la devuelve

        }


    }
}
