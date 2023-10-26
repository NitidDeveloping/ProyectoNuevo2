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

        /*============= USUARIOS DE LA BD =============*/

        /*CREATE USER 'Visitante' IDENTIFIED BY 'b+ygCVepakZpste+%+yeC%NoDD9L_KwQx*`bZbhn=xr@w';

        CREATE USER 'Alumno' IDENTIFIED BY 'uVn5`rxeA/A92ZFgnG99qhSoi43`Wq/SACoenG^G6~~+i';

        CREATE USER 'Docente' IDENTIFIED BY 'TcdiyM~ikDgd&VyFfjtPd74B=V^7D-Von%hKc^T/jnx-G';

        CREATE USER 'Default_User' IDENTIFIED BY '';

        CREATE USER 'OperadorEjemplo' IDENTIFIED BY 'contraseñaOperador';

        CREATE USER 'AdministradorEjemplo' IDENTIFIED BY 'contraseñaAdmin'; */


        private Conector()  //Metodo en private para que no se tenga acceso al constructor
        {
            database = "sreyes"; //Establecemos los valores en el constructor.
            server = "192.168.2.53";
            uid = "sreyes";
            pwd = "55591147";

            /*switch (Sesion.LoggedRol)
            {
                case TipoRol.Administrador:
                    uid = "mgilino";
                    pwd = "54828274";
                    break;
                case TipoRol.Operador:
                    uid = "ngarcia";
                    pwd = "59556020";
                    break;
                case TipoRol.Docente:
                    uid = "lleyton";
                    pwd = "53618088";
                    break;
                case TipoRol.Alumno:
                    uid = "acisnero";
                    pwd = "54819065";
                    break;
                case TipoRol.Default:
                    uid = "";
                    pwd = "";
                    break;
                case TipoRol.Visitante:
                    uid = "";
                    pwd = "";
                    break;
            }*/
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
            if (conn == null)
            {
                conn = new Conector(); //Crea la conexión en caso de que no exista
            }
            return conn; //Sino simplemente la devuelve

        }




    }
}
