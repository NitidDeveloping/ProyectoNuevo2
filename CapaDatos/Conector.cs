using CapaEntidades;
using MySqlConnector;
using System;
using System.IO;
using System.Data;

namespace CapaDatos
{
    class Conector
    {
        private string server;
        private string uid;
        private string pwd;
        private string database;
        private string userVariables;

        private static Conector conn = null;

        /*============= USUARIOS DE LA BD =============*/

        /*CREATE USER 'Alumno' IDENTIFIED BY 'uVn5rxeA/A92ZFgnG99qhSoi43`Wq/SACoenG^G6~~+i';

        CREATE USER 'Docente' IDENTIFIED BY 'TcdiyM~ikDgd&VyFfjtPd74B=V^7D-Von%hKc^T/jnx-G';

        CREATE USER 'Default' IDENTIFIED BY '';

        CREATE USER 'Operador' IDENTIFIED BY 'erñqg{er{gqe{43{r{ñt3.23{g.hy4{ñ5mh.-,9';
 
        CREATE USER 'Administrador' IDENTIFIED BY 'qefqweflmrgqelkvm4325.54ñ{-4{ñ´+,,.'; */


        private Conector()  //Metodo en private para que no se tenga acceso al constructor
        {
            /* uid = "root";
             server = "localhost";
             pwd = "1234";
             database = "nitid";
            */

            string[] lineasDeDocumentoDeConexionABD = File.ReadAllLines("Conexion a bd.txt");
            database = lineasDeDocumentoDeConexionABD[1]; //Segunda linea del documento para el nombre de la base de datos
            server = lineasDeDocumentoDeConexionABD[3]; //Cuarta linea del documento para la ip del servidor

            //Asignacion de nombres de usuario y pins
            switch (Sesion.LoggedRol)
            {
                //Usuarios espefcificos para administradores y operadores.
                case TipoRol.Operador:
                    uid = "Operador";
                    pwd = "Operador1";
                    break;
                case TipoRol.Administrador:
                    uid = "Administrador";
                    pwd = "Administrador1";
                    break;

                //Usuarios genericos
                case TipoRol.Docente:
                    uid = "Docente";
                    pwd = "Docente1";
                    break;
                case TipoRol.Alumno:
                    uid = "Alumno";
                    pwd = "Alumno1";
                    break;
                case TipoRol.Default:
                    uid = "Default";
                    pwd = "Default1";
                    break;
                case TipoRol.Visitante:
                    uid = "Visitante";
                    pwd = "Visitante1"; //Contrasenia del usuario visitante
                    break;
            }

        }

        //Método público para devolver string de conexión

        public MySqlConnection crearConexion()
        {
            MySqlConnection conn = new MySqlConnection();

            try
            {
                //Crear cadena de conexión

                conn.ConnectionString = "server=" + server + ";uid=" + uid + ";pwd=" + pwd + ";database=" + database + ";Allow User Variables = true";
            }
            catch (Exception ex)
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

            conn = new Conector(); //Crea la conexión en caso de que no exista

            return conn; //Sino simplemente la devuelve

        }




    }
}
