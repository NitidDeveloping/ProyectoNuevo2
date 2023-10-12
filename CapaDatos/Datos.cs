﻿using MySqlConnector;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.CodeDom;

namespace CapaDatos
{
    public class Datos
    {
        //Metodos para operaciones en el menu de gestiones (listar, agregar, eliminar, consultar)
        #region
        public List<object> Listar(TipoReferencia referencia, string columna, object valor, Type tipo) //Devuelve una lista de objetos segun la referencia que se le mande (searchtext no del todo implementado)
        {
            //Variables
            MySqlCommand cmd;
            MySqlDataReader dr; //Datareader usado para capturar el resultado de la consulta
            MySqlConnection conn = Conector.crearInstancia().crearConexion();//Usada para la conexion con la bd
            List<object> objetos = new List<object>();//Devuelve esta lista
            object aux = 1;//Usado para agregarlo a la lista
            string cmdstr;//Usado para asignar el query al comando de base de datos

            //Setea la consulta dependiendo de la referencia
#pragma warning disable IDE0010 // Agregar casos que faltan
            switch (referencia)
            {
                case TipoReferencia.Alumno:
                    cmdstr = "SELECT Nombre, Apellido, CI_Alumno, Grupos FROM Usuario_Alumno;";
                    break;

                case TipoReferencia.Turno:
                    cmdstr = "SELECT Turno, Nombre_Turno FROM Turno;";
                    break;

                case TipoReferencia.Materia:
                    cmdstr ="SELECT ID_Materia, Nombre FROM Materia;";
                    break;

                case TipoReferencia.Grupo:
                    cmdstr =
                        "SELECT Grupo.ID_Grupo, Grupo.Anio, Grupo.Orientacion, Orientacion.Nombre_Orientacion, Grupo.Turno, Turno.Nombre_Turno, COALESCE(Lista, 0) AS Lista " +
                        "FROM grupo " +
                        "INNER JOIN Turno ON Grupo.Turno = Turno.Turno " +
                        "INNER JOIN Orientacion ON Grupo.Orientacion=Orientacion.Orientacion " +
                        "LEFT JOIN (" +
                        "SELECT ID_Grupo, " +
                        "COUNT(CI_Alumno) AS Lista " +
                        "FROM grupo_alumno GROUP BY ID_Grupo) AS Subconsulta ON grupo.ID_Grupo = Subconsulta.ID_Grupo;";
                    break;

                case TipoReferencia.Docente:
                    cmdstr = "SELECT Nombre, Apellido, CI_Docente FROM Usuario_Docente;";
                    break;

                case TipoReferencia.Orientacion:
                    cmdstr = "SELECT Orientacion.Orientacion, Nombre_Orientacion FROM Orientacion;";
                    break;

                case TipoReferencia.Hora:
                    cmdstr = "SELECT Horario.ID_Horario, Horario.Turno, Turno.Nombre_Turno, Horario.Hora_Inicio, Horario.Hora_Fin " +
                        "FROM Horario " +
                        "JOIN Turno ON Horario.Turno = Turno.Turno;";
                    break;

                case TipoReferencia.Anio:
                    cmdstr = "SELECT Anio.Anio FROM anio;";
                    break;

                case TipoReferencia.CargosFuncionarios:
                    cmdstr = "SELECT Cargo.Cargo, Cargo.Nombre_Cargo FROM Cargo;";
                    break;

                case TipoReferencia.Lugar:
                    cmdstr = "SELECT " +
                        "Lugar.ID, " +
                        "Lugar.Nombre, " +
                        "Lugar.Tipo, " +
                        "Tipo_Lugar.Nombre_Tipo, " +
                        "Lugar.Piso, " +
                        "Lugar.Coordenada_X, " +
                        "Lugar.Coordenada_Y, " +
                        "    CASE WHEN Clase.ID_Clase IS NOT NULL THEN true ELSE false END AS AptoParaClase, " +
                        "    CASE WHEN UC.ID_UsoComun IS NOT NULL THEN true ELSE false END AS UsoComun," +
                        "    CASE " +
                        "        WHEN CASE WHEN GMHC.ID_Grupo IS NOT NULL THEN true ELSE false END = 1 " +
                        "            AND (DATE_FORMAT(NOW(), '%H:%i') >= DATE_FORMAT(Horario.Hora_Inicio, '%H:%i') " +
                        "                AND DATE_FORMAT(NOW(), '%H:%i') < DATE_FORMAT(Horario.Hora_Fin, '%H:%i')) " +
                        "        THEN false " +
                        "        ELSE true " +
                        "    END AS EstadoOcupacion " +
                        "FROM Lugar JOIN Tipo_Lugar ON Lugar.Tipo = Tipo_Lugar.Tipo " +
                        "LEFT JOIN Grupo_Materia_Horario_Clase GMHC ON Lugar.ID = GMHC.ID_Clase " +
                        "LEFT JOIN Clase ON Lugar.ID = Clase.ID_Clase " +
                        "LEFT JOIN Uso_Comun UC ON Lugar.ID = UC.ID_UsoComun " +
                        "LEFT JOIN Grupo_Materia_Horario GMH ON GMHC.ID_Grupo = GMH.ID_Grupo " +
                        "    AND GMHC.ID_Materia = GMH.ID_Materia " +
                        "    AND GMHC.ID_Horario = GMH.ID_Horario " +
                        "LEFT JOIN Horario ON GMH.ID_Horario = Horario.ID_Horario " +
                        "\r /*Where*/ \r" +
                        " GROUP BY " +
                        "    Lugar.ID, " +
                        "    Lugar.Nombre, " +
                        "    Lugar.Tipo, " +
                        "    Lugar.Piso, " +
                        "    Lugar.Coordenada_X, " +
                        "    Lugar.Coordenada_Y;";
                    break;

                case TipoReferencia.Funcionario:
                    cmdstr = "SELECT Nombre, Apellido, CI_Funcionario, Cargo, Nombre_Cargo, Tipo, Fecha_Ingreso FROM Usuario_Funcionario;";
                    break;

                case TipoReferencia.TipoDeLugar:
                    cmdstr = "SELECT Tipo, Nombre_Tipo FROM Tipo_Lugar;";
                    break;

                default: //Pongo el default porque sino me marca un error a la hora de asignar el cmdstring pero en realidad no lo pienso usar asi
                    throw new ArgumentException("Agrumento de lista invalido, contacte a un administrador si el problema persiste");
            }
#pragma warning restore IDE0010 // Agregar casos que faltan

            //Si el metodo se invoca con algo en la columna se agregan parametros para hacer los filtros
            if (columna != null)
            {
                //Si la referencia es el lugar entonces reemplazamos comentaria --Where porque sino la consulta da problemas por el where
                cmdstr = referencia == TipoReferencia.Lugar
                    ? cmdstr.Replace("/*Where*/", " WHERE " + columna + "  LIKE @Valor ")
                    : cmdstr.Replace(";", " WHERE " + columna + "  LIKE @Valor ;");

                cmd = new MySqlCommand(cmdstr, conn); //Asigno el cmdstring al mysqlcommand
                valor = "%" + valor.ToString() + "%";
                cmd.Parameters.AddWithValue("@Valor", valor);
                /*if (valor is int)
                {
                    cmd.Parameters.Add("@Valor", MySqlDbType.Int32).Value = valor;
                }*/

                /* switch (tipo.Name)
                 {
                     case "Int32":
                         if (valor is int num)
                         {
                             cmd.Parameters.Add("@Valor", MySqlDbType.Int32).Value = num;
                         }
                         break;

                 }*/

            }
            else
            {
                cmd = new MySqlCommand(cmdstr, conn);
            }


            //Ejecuta el comando en la base de datos
            try
            {
                conn.Open(); //Abro la conexión
                dr = cmd.ExecuteReader(); //Inicio el comando

                //Arma los objetos para la lista
                while (dr.Read())//Mientras haya registros en el datareader agrega campos a la lista de objetos
                {
                    //Segun la referencia con la que se invoque el metodo se agregan objetos a la lista de un tipo u otro
                    switch (referencia)//Segun la referencia inicializa aux de una forma u otra
                    {
                        case TipoReferencia.Alumno:
                            aux = new Alumno(dr.GetString(0), dr.GetString(1), dr.GetInt32(2), dr.GetString(3));
                            break;

                        case TipoReferencia.Turno:
                            aux = new Turno(dr.GetByte(0), dr.GetString(1));
                            break;

                        case TipoReferencia.Materia:
                            aux = new Materia(dr.GetString(1), dr.GetUInt16(0));
                            break;

                        case TipoReferencia.Grupo:
                            Turno auxturnogrupo = new Turno(dr.GetByte(4), dr.GetString(5));
                            Orientacion auxorientacion = new Orientacion(dr.GetByte(2), dr.GetString(3));
                            aux = new Grupo(dr.GetString(0), auxturnogrupo, auxorientacion, dr.GetInt32(1), dr.GetByte(6));
                            break;

                        case TipoReferencia.Docente:
                            aux = new Docente(dr.GetString(0), dr.GetString(1), dr.GetInt32(2));
                            break;

                        case TipoReferencia.Orientacion:
                            aux = new Orientacion(dr.GetByte(0), dr.GetString(1));
                            break;

                        case TipoReferencia.Hora:
                            Turno auxturnohora = new Turno(dr.GetByte(1), dr.GetString(2));
                            aux = new Hora((dr.GetByte(0), auxturnohora), dr.GetTimeSpan(3), dr.GetTimeSpan(4));
                            break;

                        case TipoReferencia.Anio:
                            aux = dr.GetInt32(0);
                            break;

                        case TipoReferencia.CargosFuncionarios:
                            aux = new Cargo(dr.GetByte(0), dr.GetString(1));
                            break;

                        case TipoReferencia.Lugar:
                            TipoLugar auxtipolugar = new TipoLugar(dr.GetByte(2), dr.GetString(3));
                            aux = new Lugar(dr.GetUInt16(0), dr.GetString(1), dr.GetInt32(5), dr.GetInt32(6), dr.GetByte(4), dr.GetBoolean(7), dr.GetBoolean(8), auxtipolugar);
                            break;

                        case TipoReferencia.Funcionario:
                            Cargo auxcargofuncionario = new Cargo(dr.GetByte(3), dr.GetString(4));
                            aux = new Funcionario(dr.GetString(0), dr.GetString(1), dr.GetInt32(2), auxcargofuncionario, dr.GetBoolean(5), dr.GetDateTime(6));
                            break;

                        case TipoReferencia.TipoDeLugar:
                            aux = new TipoLugar(dr.GetByte(0), dr.GetString(1));
                            break;


                    }
                    objetos.Add(aux);// Agrega el aux a la lista
                }

                return objetos;//Retorna la lista
            }
            catch (Exception ex)// En caso de excepcion throwea esta
            {
                throw ex;
            }
            finally//Al final haya excepcion o no cierra la base de datos
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close(); //Cerramos la conexión en caso de que esté abierta
                }
            }
        }

        public RetornoValidacion Eliminar(TipoReferencia referencia, string idObjetivo)
        {
            RetornoValidacion respuesta;
            string cmdstr = "DELETE from @TablaReferencia where @ClavePrimaria=@ID";
            MySqlConnection conn = Conector.crearInstancia().crearConexion();
            MySqlCommand cmd;

            switch (referencia)
            {
                case TipoReferencia.Usuario:
                    cmdstr = "DELETE from Usuario where CI=@ID";
                    break;

                default:
                    throw new ArgumentException("Agrumento de eliminado invalido, contacte a un administrador si el problema persiste");

            }

            cmd = new MySqlCommand(cmdstr, conn);

            switch (referencia)
            {
                case TipoReferencia.Usuario:
                cmd.Parameters.Add("@ID", MySqlDbType.Int32).Value = Convert.ToInt32(idObjetivo);
                    break;

        }
            try
            {
                conn.Open();
                respuesta = cmd.ExecuteNonQuery() == 1 ? RetornoValidacion.OK : RetornoValidacion.ErrorInesperadoBD;
            }
            catch (Exception ex)
            {
                throw ex; //esto lo manejamos con un try catch en la presentación
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return respuesta;

        }

        public RetornoValidacion Agregar(TipoReferencia referencia, object item)
        {
            //Variables
            RetornoValidacion respuesta;
            string cmdstr;
            MySqlConnection conn = Conector.crearInstancia().crearConexion(); ;
            MySqlCommand cmd;

            //Decide valores de tabla, parametros y valores para el string del comando
            switch (referencia)
            {
                case TipoReferencia.Usuario:
                    cmdstr = "INSERT INTO Usuario (CI, PIN, Nombre, Apellido) VALUES (@CI, @PIN, @Nombre, @Apellido);";
                    break;

                case TipoReferencia.Grupo:
                    cmdstr = "INSERT INTO Grupo (Anio, Turno, ID_Grupo, Orientacion) VALUES (@Anio, @Turno, @ID_Grupo, @Orientacion);";
                    break;

                default:
                    throw new ArgumentException("Argumento de agregado invalido, contacte a un administrador si el problema persiste");

            }

            //Agrega los parametros al comando
            cmd = new MySqlCommand(cmdstr, conn);

            //Asigna los valores para agregar en el comando
            switch (referencia)
            {
                case TipoReferencia.Usuario:
                    if (item is Usuario usuario)
                    {
                        cmd.Parameters.Add("@CI", MySqlDbType.Int32).Value = usuario.CI;
                        cmd.Parameters.Add("@PIN", MySqlDbType.Int32).Value = usuario.PIN;
                        cmd.Parameters.Add("@Nombre", MySqlDbType.VarChar).Value = usuario.Nombre;
                        cmd.Parameters.Add("@Apellido", MySqlDbType.VarChar).Value = usuario.Apellido;
                    }
                    break;
            }

            // Ejecuta el comando y devuelve un retorno segun como salga la operacion
            try
            {
                conn.Open();
                respuesta = cmd.ExecuteNonQuery() == 1 ? RetornoValidacion.OK : RetornoValidacion.ErrorInesperadoBD; //Asgina un valor al resultado de la operaciones
            }
            catch (Exception ex)
            {
                throw ex; //esto lo manejamos con un try catch en la presentación
            }
            finally
            {
                //Cierra la conexion
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return respuesta;
        }

        public RetornoValidacion Editar(TipoReferencia referencia, object item, string idObjetivo)
        {
            //Variables
            RetornoValidacion respuesta;
            string cmdstr = "UPDATE @TablaReferencia SET @ValoresReferencia WHERE @ClavePrimaria=@ID;";
            string tabla;
            string valores; //Seteara los parametros por donde pasaremos los valores para editar
            string clave;
            MySqlConnection conn = Conector.crearInstancia().crearConexion(); ;
            MySqlCommand cmd;

            //Decide valores de tabla valores y clave para el string del comando
            switch (referencia)
            {
                case TipoReferencia.Usuario:
                    cmdstr = "UPDATE Usuario SET Nombre=@Nombre, Apellido=@Apellido WHERE CI=@ID;";
                    break;

                default:
                    throw new ArgumentException("Agrumento de editado invalido, contacte a un administrador si el problema persiste");

            }

            //Agrega los parametros al comando
            cmd = new MySqlCommand(cmdstr, conn);

            //Asigna los valores para agregar en el comando
            switch (referencia)
            {
                case TipoReferencia.Usuario:
                    if (item is Usuario usuario)
                    {
                        cmd.Parameters.Add("@ID", DbType.Int32).Value = Convert.ToInt32(idObjetivo);
                        cmd.Parameters.Add("@Nombre", MySqlDbType.VarChar).Value = usuario.Nombre;
                        cmd.Parameters.Add("@Apellido", MySqlDbType.VarChar).Value = usuario.Apellido;
                    }
                    break;
            }

            // Ejecuta el comando y devuelve un retorno segun como salga la operacion
            try
            {
                conn.Open();
                respuesta = cmd.ExecuteNonQuery() == 1 ? RetornoValidacion.OK : RetornoValidacion.ErrorInesperadoBD; //Asgina un valor al resultado de la operaciones
            }
            catch (Exception ex)
            {
                throw ex; //esto lo manejamos con un try catch en la presentación
            }
            finally
            {
                //Cierra la conexion
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return respuesta;
        }

        public object Consultar(TipoReferencia referencia, string idObjetivo)
        {
            //Variables
            object respuesta;
            string cmdstr;
            MySqlConnection conn = Conector.crearInstancia().crearConexion(); ;
            MySqlCommand cmd;
            MySqlDataReader dr;

            switch (referencia){
                case TipoReferencia.Alumno:
                    cmdstr = "SELECT (CI, Nombre, Apellido) FROM Usuario_Alumno WHERE CI=@ID;";
                    break;
                default:
                    throw new ArgumentException("Argumento de consulta invalido, contacte a un administrador si el problema persiste");
            }

            //Agrega los parametros al comando
            cmd = new MySqlCommand(cmdstr, conn);

            //Segun la referencia pasa la id como un valor de base de datos u otro
            switch (referencia)
            {
                case TipoReferencia.Alumno:
                    cmd.Parameters.Add("@ID", MySqlDbType.Int32).Value = Convert.ToInt32(idObjetivo);
                    break;
            }

            //Ejecuta la consulta
            try
            {
                conn.Open(); //Abro la conexión

                dr = cmd.ExecuteReader(); //Inicio el comando

                switch (referencia)//Segun la referencia inicializa aux de una forma u otra
                {
                    case TipoReferencia.Alumno:
                        respuesta = new Alumno(dr.GetString(1), dr.GetString(2), dr.GetInt32(0), dr.GetString(3));
                        break;
                    default: throw new ArgumentException("Argumento de consulta imposible de transformar, contacte a un administrador si el problema persiste");
                }

                return respuesta;//Retorna la lista
            }
            catch (Exception ex)// En caso de excepcion throwea esta
            {
                throw ex;
            }
            finally//Al final haya excepcion o no cierra la base de datos
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close(); //Cerramos la conexión en caso de que esté abierta
                }
            }



        }


        //Sobrecargas de operaciones basicas para entidades con debilidad (programado solo para horas ya que son la unica entidad debil que tenemos)
        #region
        public RetornoValidacion Eliminar(TipoReferencia referencia, byte idObjetivo, byte idPadre)
        {
            RetornoValidacion respuesta;
            string cmdstr = "DELETE from @TablaReferencia where @ClavePrimaria=@ID AND @ClavePadre=@IDPadre";
            string tabla; //Recibe el valor de la tabla de la bd segun la referencia seleccionada
            string claveprimaria; //Recibe el valor de la clave de la tabla segun la referencia
            string clavepadre;
            MySqlConnection conn = Conector.crearInstancia().crearConexion();
            MySqlCommand cmd;

            switch (referencia)
            {
                case TipoReferencia.Hora:
                    cmdstr = "DELETE from Horario where ID_Horario=@ID AND Turno=@IDPadre";
                    break;

                default:
                    throw new ArgumentException("Agrumento de eliminado invalido, contacte a un administrador si el problema persiste");

            }

            cmd = new MySqlCommand(cmdstr, conn);

            switch (referencia)
            {
                case TipoReferencia.Hora:
                    cmd.Parameters.Add("@ID", MySqlDbType.Int16).Value = idObjetivo;
                    cmd.Parameters.Add("@IDPadre", MySqlDbType.Byte).Value = idPadre;
                    break;

                default:
                    throw new ArgumentException("Agrumento de eliminado invalido, contacte a un administrador si el problema persiste");

            }

            try
            {
                conn.Open();
                respuesta = cmd.ExecuteNonQuery() == 1 ? RetornoValidacion.OK : RetornoValidacion.ErrorInesperadoBD;
            }
            catch (Exception ex)
            {
                throw ex; //esto lo manejamos con un try catch en la presentación
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return respuesta;

        }

        public RetornoValidacion Editar(TipoReferencia referencia, object item, byte idObjetivo, byte idPadre)
        {
            //Variables
            RetornoValidacion respuesta;
            string cmdstr = "UPDATE @TablaReferencia SET @ValoresReferencia WHERE @ClavePrimaria=@ID AND @ClavePadre=@IDPadre;";
            string tabla;
            string valores; //Seteara los parametros por donde pasaremos los valores para editar
            string clave;
            string clavepadre;
            MySqlConnection conn = Conector.crearInstancia().crearConexion(); ;
            MySqlCommand cmd;

            //Decide valores de tabla valores y clave para el string del comando
            switch (referencia)
            {
                case TipoReferencia.Hora:
                    cmdstr = "UPDATE Horario SET Hora_Inicio=@Hora_Inicio, Hora_Fin=@Hora_Fin WHERE ID_Horario=@ID AND @Turno=@IDPadre;";
                    break;

                default:
                    throw new ArgumentException("Agrumento de editado invalido, contacte a un administrador si el problema persiste");

            }

            //Agrega los parametros al comando
            cmd = new MySqlCommand(cmdstr, conn);

            //Asigna los valores para agregar en el comando
            switch (referencia)
            {
                case TipoReferencia.Usuario:
                    if (item is Hora hora)
                    {
                        cmd.Parameters.Add("@ID", MySqlDbType.Int16).Value = idObjetivo;
                        cmd.Parameters.Add("@IDPadre", MySqlDbType.Byte).Value = idPadre;
                        cmd.Parameters.Add("@Hora_Inicio", MySqlDbType.Time).Value = hora.Inicio;
                        cmd.Parameters.Add("@Hora_Fin", MySqlDbType.Time).Value = hora.Fin;
                    }
                    break;
            }

            // Ejecuta el comando y devuelve un retorno segun como salga la operacion
            try
            {
                conn.Open();
                respuesta = cmd.ExecuteNonQuery() == 1 ? RetornoValidacion.OK : RetornoValidacion.ErrorInesperadoBD; //Asgina un valor al resultado de la operaciones
            }
            catch (Exception ex)
            {
                throw ex; //esto lo manejamos con un try catch en la presentación
            }
            finally
            {
                //Cierra la conexion
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return respuesta;
        }

        public object Consultar(TipoReferencia referencia, byte idObjetivo, byte idPadre)
        {
            //Variables
            object respuesta;
            string cmdstr;
            MySqlConnection conn = Conector.crearInstancia().crearConexion(); ;
            MySqlCommand cmd;
            MySqlDataReader dr;

            switch (referencia)
            {
                case TipoReferencia.Hora:
                    cmdstr = "SELECT (Horario.ID_Horario, Horario.Turno, Turno.Nombre_Turno, Horario.Hora_Inicio, Horario.Hora_Fin) FROM Horario JOIN Turno ON Horario.Turno = Turno.Turno WHERE ID_Horario=@ID AND Turno=@IDPadre;";
                    break;
                default:
                    throw new ArgumentException("Argumento de consulta invalido, contacte a un administrador si el problema persiste");
            }

            //Agrega los parametros al comando
            cmd = new MySqlCommand(cmdstr, conn);

            //Segun la referencia pasa la id como un valor de base de datos u otro
            switch (referencia)
            {
                case TipoReferencia.Alumno:
                    cmd.Parameters.Add("@ID", MySqlDbType.Int16).Value = idObjetivo;
                    cmd.Parameters.Add("@IDPadre", MySqlDbType.Byte).Value = idPadre;
                    break;
            }

            //Ejecuta la consulta
            try
            {
                conn.Open(); //Abro la conexión

                dr = cmd.ExecuteReader(); //Inicio el comando


                switch (referencia)//Segun la referencia inicializa aux de una forma u otra
                {
                    case TipoReferencia.Hora:
                        Turno auxturno = new Turno(dr.GetByte(1), dr.GetString(2));
                       respuesta = new Hora((dr.GetByte(0), auxturno), dr.GetTimeSpan(3), dr.GetTimeSpan(4));
                        break;
                    default: throw new ArgumentException("Argumento de consulta imposible de transformar, contacte a un administrador si el problema persiste");
                }

                return respuesta;//Retorna la lista
            }
            catch (Exception ex)// En caso de excepcion throwea esta
            {
                throw ex;
            }
            finally//Al final haya excepcion o no cierra la base de datos
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close(); //Cerramos la conexión en caso de que esté abierta
                }
            }



        }


        #endregion

        #endregion

        //Metodos para operaciones propias de los alumnos
        #region
        public List<Grupo> ConsultarGruposAlumno(int ciAlumno) //Devuelve los grupos en los que se encuentra el alumno solicitado
        {
            MySqlDataReader dr;
            MySqlConnection conn = new MySqlConnection();
            List<Grupo> grupos = new List<Grupo>();
            Grupo auxGrupo;

            try
            {
                conn = Conector.crearInstancia().crearConexion();
                MySqlCommand cmd = new MySqlCommand("SELECT Grupo.* FROM Grupo INNER JOIN Grupo_Alumno ON Grupo.ID_Grupo = Grupo_Alumno.ID_Grupo" +
                    " WHERE Grupo_Alumno.CI_Alumno = @CI_Alumno;", conn);
                cmd.Parameters.Add("@CI_Alumno", MySqlDbType.Int32).Value = ciAlumno;

                conn.Open();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    auxGrupo = new Grupo(dr.GetString(0));
                    grupos.Add(auxGrupo);
                }
                return grupos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        #endregion
    }



    // Link a un chat que me tiro el sabio despues de que le pedi una revision
    //https://chat.openai.com/share/c0d53224-e843-49b8-b3ec-1ff4c607c6e0

}

