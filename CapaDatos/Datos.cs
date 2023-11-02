using MySqlConnector;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;

namespace CapaDatos
{
    public class Datos
    {
        //Metodos para operaciones en el menu de gestiones (listar, agregar, eliminar, consultar)
        #region
        public List<object> Listar(TipoReferencia referencia, string columna, object valor) //Devuelve una lista de objetos segun la referencia que se le mande (searchtext no del todo implementado)
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
                    cmdstr = "SELECT ID_Materia, Nombre FROM Materia;";
                    break;

                case TipoReferencia.Grupo:
                    cmdstr = "SELECT ID_Grupo, Anio, Orientacion, Nombre_Orientacion, Turno, Nombre_Turno, Lista FROM Lista_Grupos;";
                    break;

                case TipoReferencia.DiaSemana:
                    cmdstr = "SELECT Dia_Semana, Nombre_Dia FROM Dia_Semana;";
                    break;

                case TipoReferencia.Docente:
                    cmdstr = "SELECT Nombre, Apellido, CI_Docente FROM Usuario_Docente;";
                    break;

                case TipoReferencia.Orientacion:
                    cmdstr = "SELECT Orientacion, Nombre_Orientacion FROM Orientacion;";
                    break;

                case TipoReferencia.Hora:
                    cmdstr = "SELECT ID_Horario, Turno, Nombre_Turno, Hora_Inicio, Hora_Fin FROM Lista_Horas;";
                    break;

                case TipoReferencia.Anio:
                    cmdstr = "SELECT Anio FROM Anio;";
                    break;

                case TipoReferencia.CargosFuncionarios:
                    cmdstr = "SELECT Cargo, Nombre_Cargo FROM Cargo;";
                    break;

                case TipoReferencia.Lugar:
                    if (Sesion.LoggedRol == TipoRol.Visitante)
                    {
                        cmdstr = "SELECT ID, Nombre, Tipo, Coordenada_X, Coordenada_Y, Piso FROM Solo_UsoComun;";
                    }
                    else
                    {
                        cmdstr = "SELECT ID, Nombre, Tipo, Nombre_Tipo, Coordenada_X, Coordenada_Y, Piso, AptoParaClase, UsoComun, EstadoOcupacion FROM Lugares;";
                    }
                    break;

                case TipoReferencia.Clases:
                    cmdstr = "SELECT ID, Nombre, Tipo, Nombre_Tipo, Coordenada_X, Coordenada_Y, Piso, AptoParaClase, UsoComun, EstadoOcupacion FROM Solo_Clase;";
                    break;

                case TipoReferencia.Funcionario:
                    cmdstr = "SELECT Nombre, Apellido, CI_Funcionario, Cargo, Nombre_Cargo, Tipo, Fecha_Ingreso FROM Usuario_Funcionario;";
                    break;

                case TipoReferencia.TipoDeLugar:
                    cmdstr = "SELECT Tipo, Nombre_Tipo FROM Tipo_Lugar;";
                    break;

                case TipoReferencia.Horario:
                    cmdstr = "SELECT " +
                        " IdTurno," + //0
                        " NombreTurno," + //1
                        " IdGrupo," + //2
                        " IdMateria," + //3
                        " NombreMateria," + //4
                        " CiDocente," + //5
                        " NombreDocente," + //6
                        " ApellidoDocente," + //7
                        " IdDiaSemana," + //8
                        " NombreDiaSemana," + //9
                        " Horas_Abarca," + //10
                        " IdSalon_Asignado_Predeterminado," + //11
                        " NombreSalon_Asignado_Predeterminado," + //12
                        " IdSalon_Asignado_Predeterminado," + //13
                        " IdAsignadoTemporal," + //14
                        " NombreAsignadoTemporal," + //15
                        " Inicio," + //16
                        " Fin" + //17
                        " FROM ListaHorarios;\r\n";
                    break;

                default: //Pongo el default porque sino me marca un error a la hora de asignar el cmdstring pero en realidad no lo pienso usar asi
                    throw new ArgumentException("Agrumento de lista invalido, contacte a un administrador si el problema persiste");
            }
#pragma warning restore IDE0010 // Agregar casos que faltan

            //Si el metodo se invoca con algo en la columna se agregan parametros para hacer los filtros
            if (columna != null)
            {
                //Si consulta por una hora o por una fecha, como son valores especiales la consulta se tiene que hacer con =
                bool esformatotemporal = columna == "Hora_Inicio" || columna == "Hora_Fin" || columna == "Fecha_Ingreso"; //Verifica si consulta por una de esas columnas
                string comparador = esformatotemporal ? "=" : "LIKE"; //Si da true setea el comparador en igual, sino en LIKE

                cmdstr = cmdstr.Replace(";", " WHERE " + columna + "  " + comparador + " @Valor ;");

                cmd = new MySqlCommand(cmdstr, conn); //Asigno el cmdstring al mysqlcommand
                if (!esformatotemporal)
                {
                    valor = "%" + valor.ToString() + "%";
                }

                cmd.Parameters.AddWithValue("@Valor", valor);
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
                            if (dr.IsDBNull(3))
                            {
                                aux = new Alumno(dr.GetString(0), dr.GetString(1), dr.GetInt32(2));
                            }
                            else
                            {
                                aux = new Alumno(dr.GetString(0), dr.GetString(1), dr.GetInt32(2), dr.GetString(3));
                            }
                            break;

                        case TipoReferencia.Turno:
                            aux = new Turno(dr.GetByte(0), dr.GetString(1));
                            break;

                        case TipoReferencia.Materia:
                            aux = new Materia(dr.GetUInt16(0), dr.GetString(1));
                            break;

                        case TipoReferencia.Grupo:
                            Turno auxturnogrupo = new Turno(dr.GetByte(4), dr.GetString(5));
                            Orientacion auxorientacion = new Orientacion(dr.GetByte(2), dr.GetString(3));
                            aux = new Grupo(dr.GetString(0), auxturnogrupo, auxorientacion, dr.GetInt32(1), dr.GetByte(6));
                            break;

                        case TipoReferencia.Docente:
                            aux = new Docente(dr.GetString(0), dr.GetString(1), dr.GetInt32(2));
                            break;

                        case TipoReferencia.DiaSemana:
                            aux = new Dia_Semana(dr.GetByte(0), dr.GetString(1));
                            break;

                        case TipoReferencia.Orientacion:
                            aux = new Orientacion(dr.GetByte(0), dr.GetString(1));
                            break;

                        case TipoReferencia.Hora:
                            Turno auxturnohora = new Turno(dr.GetByte(1), dr.GetString(2));
                            aux = new Hora((dr.GetByte(0), auxturnohora), dr.GetTimeSpan(3), dr.GetTimeSpan(4));
                            break;

                        case TipoReferencia.Horario:
                            Turno auxTurnoH = new Turno(dr.GetByte(0), dr.GetString(1));
                            string auxGrupoH = dr.GetString(2);
                            Materia auxMateriaH = new Materia(dr.GetUInt16(3), dr.GetString(4));

                            //Si el campo de cedula del docente contiene algo lo agrego
                            //al horario
                            Docente auxDocenteH = null;
                            if (!dr.IsDBNull(5))
                            {
                                auxDocenteH = new Docente(dr.GetString(6), dr.GetString(7), dr.GetInt32(5));
                            }


                            Dia_Semana auxDiaSH = new Dia_Semana(dr.GetByte(8), dr.GetString(9));

                            //Si el campo con la id de la clase tiene algo
                            //Inicializa la clase y la agregar al horario
                            Lugar auxSalonH = null;
                            if (!dr.IsDBNull(11))
                            {
                                auxSalonH = new Lugar(dr.GetUInt16(11), dr.GetString(12));
                            }

                            //Si el campo con la id de el salon temporal tiene algo
                            //Inicializa el salon temporal y lo agrega al horario
                            Lugar auxSalonTH = null;
                            if (!dr.IsDBNull(14))
                            {

                                auxSalonTH = new Lugar(dr.GetUInt16(14), dr.GetString(15));
                            }

                            //Instancia lista de horas que abarca un grupo
                            List<Hora> auxListaHorasH = new List<Hora>();
                            Hora auxHoraHorario;

                            //Recupera el texto con la lista de horas, lo divide y por cada hora
                            //resultante de la division agrega una hora a la lista
                            string cadenaHorasH = dr.GetString(10);

                            TimeSpan inicio = dr.GetTimeSpan(16);
                            TimeSpan fin = dr.GetTimeSpan(17);

                            aux = new Horario(auxGrupoH, auxMateriaH, auxDocenteH, auxDiaSH, auxSalonH, auxSalonTH, cadenaHorasH, auxTurnoH, inicio, fin);

                            break;

                        case TipoReferencia.Anio:
                            aux = dr.GetInt32(0);
                            break;

                        case TipoReferencia.CargosFuncionarios:
                            aux = new Cargo(dr.GetByte(0), dr.GetString(1));
                            break;

                        case TipoReferencia.Clases:
                        case TipoReferencia.Lugar:

                            if (Sesion.LoggedRol == TipoRol.Visitante)
                            {
                                TipoLugar auxtipolugar = new TipoLugar(dr.GetByte(2));
                                aux = new Lugar(dr.GetUInt16(0), dr.GetString(1), auxtipolugar, dr.GetInt32(3), dr.GetInt32(4), dr.GetByte(5));
                            }
                            else
                            {
                                TipoLugar auxtipolugar = new TipoLugar(dr.GetByte(2), dr.GetString(3));
                                aux = new Lugar(dr.GetUInt16(0), dr.GetString(1), dr.GetInt32(4), dr.GetInt32(5), dr.GetByte(6), dr.GetBoolean(7), dr.GetBoolean(8), auxtipolugar, dr.GetBoolean(9));
                            }
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
            string cmdstr;
            MySqlConnection conn = Conector.crearInstancia().crearConexion();
            MySqlCommand cmd;

            switch (referencia)
            {
                case TipoReferencia.Usuario:
                    cmdstr = "DELETE from Usuario where CI=@CI";
                    break;

                case TipoReferencia.Alumno:
                    cmdstr = "DELETE from Alumno where CI_Alumno=@CI_Alumno";
                    break;

                case TipoReferencia.Turno:
                    cmdstr = "DELETE from Turno where Turno=@Turno";
                    break;

                case TipoReferencia.Materia:
                    cmdstr = "DELETE from Materia where ID_Materia=@ID_Materia";
                    break;

                case TipoReferencia.Grupo:
                    cmdstr = "DELETE from Grupo where ID_Grupo=@ID_Grupo";
                    break;

                case TipoReferencia.Docente:
                    cmdstr = "DELETE from Docente where CI_Docente=@CI_Docente";
                    break;

                case TipoReferencia.Orientacion:
                    cmdstr = "DELETE from Orientacion where Orientacion=@Orientacion";
                    break;

                case TipoReferencia.Hora:
                    cmdstr = "DELETE from Horario where ID_Horario=@ID_Horario";
                    break;

                case TipoReferencia.Horario:
                    cmdstr = "DELETE from Grupo_Materia_Horario_Clase where ID_Grupo=@ID_Grupo AND ID_Materia=@ID_Materia AND ID_Horario=@ID_Horario AND Turno=@Turno AND Dia_Semana=@Dia_Semana";
                    break;

                case TipoReferencia.Anio:
                    cmdstr = "DELETE from Anio where Anio=@Anio";
                    break;

                case TipoReferencia.CargosFuncionarios:
                    cmdstr = "DELETE from Cargo where Cargo=@Cargo";
                    break;

                case TipoReferencia.Lugar:
                    cmdstr = "DELETE from Lugar WHERE ID = @ID";
                    break;

                case TipoReferencia.Funcionario:
                    cmdstr = "DELETE from Funcionario where CI_Funcionario=@CI_Funcionario";
                    break;

                case TipoReferencia.TipoDeLugar:
                    cmdstr = "DELETE from Tipo_Lugar where Tipo=@Tipo";
                    break;

                case TipoReferencia.UsoComun:
                    cmdstr = "DELETE from Uso_Comun where ID_UsoComun=@Id";
                    break;

                case TipoReferencia.Clases:
                    cmdstr = "DELETE from Clase where ID_Clase=@Id";
                    break;

                default:
                    throw new ArgumentException("Agrumento de eliminado invalido, contacte a un administrador si el problema persiste");

            }

            cmd = new MySqlCommand(cmdstr, conn);

            switch (referencia)
            {
                case TipoReferencia.Usuario:
                    cmd.Parameters.Add("@CI", MySqlDbType.Int32).Value = Convert.ToInt32(idObjetivo);
                    break;

                case TipoReferencia.Alumno:
                    cmd.Parameters.Add("@CI_Alumno", MySqlDbType.Int32).Value = Convert.ToInt32(idObjetivo);
                    break;

                case TipoReferencia.Turno:
                    cmd.Parameters.Add("@Turno", MySqlDbType.Byte).Value = Convert.ToByte(idObjetivo);
                    break;

                case TipoReferencia.Materia:
                    cmd.Parameters.Add("@ID_Materia", MySqlDbType.Int16).Value = Convert.ToInt16(idObjetivo);
                    break;

                case TipoReferencia.Grupo:
                    cmd.Parameters.Add("@ID_Grupo", MySqlDbType.VarChar).Value = Convert.ToString(idObjetivo);
                    break;

                case TipoReferencia.Docente:
                    cmd.Parameters.Add("@CI_Docente", MySqlDbType.Int32).Value = Convert.ToInt32(idObjetivo);
                    break;

                case TipoReferencia.Orientacion:
                    cmd.Parameters.Add("@Orientacion", MySqlDbType.Byte).Value = Convert.ToByte(idObjetivo);
                    break;

                case TipoReferencia.Hora:
                    cmd.Parameters.Add("@ID_Horario", MySqlDbType.Int16).Value = Convert.ToInt16(idObjetivo);
                    break;

                case TipoReferencia.Anio:
                    cmd.Parameters.Add("@Anio", MySqlDbType.Year).Value = Convert.ToInt32(idObjetivo);
                    break;

                case TipoReferencia.CargosFuncionarios:
                    cmd.Parameters.Add("@Cargo", MySqlDbType.Byte).Value = Convert.ToByte(idObjetivo);
                    break;

                case TipoReferencia.Lugar:
                    cmd.Parameters.Add("@ID", MySqlDbType.Int32).Value = Convert.ToInt32(idObjetivo);
                    break;

                case TipoReferencia.Funcionario:
                    cmd.Parameters.Add("@CI_Funcionario", MySqlDbType.Int32).Value = Convert.ToInt32(idObjetivo);
                    break;

                case TipoReferencia.TipoDeLugar:
                    cmd.Parameters.Add("@Tipo", MySqlDbType.Byte).Value = Convert.ToByte(idObjetivo);
                    break;

                case TipoReferencia.Clases:
                case TipoReferencia.UsoComun:
                    cmd.Parameters.Add("@Id", MySqlDbType.Int32).Value = Convert.ToInt32(idObjetivo);
                    break;
            }
            try
            {
                conn.Open();
                respuesta = cmd.ExecuteNonQuery() == 1 ? RetornoValidacion.OK : RetornoValidacion.ErrorInesperadoBD;
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
                Exception exceptionPersonalizada;

                if (mensaje.Contains("CONSTRAINT `fk_GrupoMateriaDocente_CIDOCENTE"))
                {
                    exceptionPersonalizada = new Exception("Este docente se encuentra asignado a una o mas materias en uno o mas grupos. Desasigne este docente de las materias a las que este asignado antes de eliminarlo.");
                    throw exceptionPersonalizada;
                }

                if (mensaje.Contains("CONSTRAINT `fk_Orientacion_Grupo` "))
                {
                    exceptionPersonalizada = new Exception("Esta orientacion se encuentra asignada a uno o mas grupos. Desasigne esta orientacion de los grupos a los que se encuentre asignada antes de eliminarla.");
                    throw exceptionPersonalizada;
                }

                if (mensaje.Contains("CONSTRAINT `fk_Anio`"))
                {
                    exceptionPersonalizada = new Exception("Este año se encuentra asignado a uno o mas grupos. Desasigne este año de los grupos a los que se encuentre asignado antes de eliminarlo.");
                    throw exceptionPersonalizada;
                }

                if (mensaje.Contains("CONSTRAINT `fk_Turno_Grupo`"))
                {
                    exceptionPersonalizada = new Exception("Este turno se encuentra asignado a uno o mas grupos. Desasigne este turno de los grupos a los que se encuentre asignado antes de eliminarlo.");
                    throw exceptionPersonalizada;
                }

                if (mensaje.Contains("`Grupo_Materia`, CONSTRAINT `fk_GrupoMateria_IDMATERIA` "))
                {
                    exceptionPersonalizada = new Exception("Esta materia se encuentra asignada a uno o mas grupos. Desasigne esta materia de los grupos a los que se encuentre asignada antes de eliminarla.");
                    throw exceptionPersonalizada;
                }

                if (mensaje.Contains("`.`Grupo_Alumno`, CONSTRAINT `fk_GrupoAlumno_IDGRUPO` "))
                {
                    exceptionPersonalizada = new Exception("Este grupo tiene alumnos asignados. Desasigne todos los alumnos de este grupo antes de eliminarlo.");
                    throw exceptionPersonalizada;
                }

                if (mensaje.Contains("`.`Grupo_Materia`, CONSTRAINT `fk_GrupoMateria_IDGRUPO`"))
                {
                    exceptionPersonalizada = new Exception("Este grupo tiene materias asignadas. Desasigne todas las materias asignadas a este grupo antes de eliminarlo.");
                    throw exceptionPersonalizada;
                }

                if (mensaje.Contains("`.`Grupo_Materia`, CONSTRAINT `fk_GrupoMateria_IDGRUPO`"))
                {
                    exceptionPersonalizada = new Exception("Este grupo tiene materias asignadas. Desasigne todas las materias asignadas a este grupo antes de eliminarlo.");
                    throw exceptionPersonalizada;
                }

                if (mensaje.Contains("`.`grupo_materia_horario_clase`, CONSTRAINT `fk_GrupoMateriaHorarioClase_IDCLASE`"))
                {
                    exceptionPersonalizada = new Exception("Este lugar esta asignado como salon en uno o mas horarios. Desasigne este lugar de todos los horarios a los que pueda estar asignado antes de eliminarlo.");
                    throw exceptionPersonalizada;
                }

                if (mensaje.Contains("`.`grupo_materia_horario_clase`, CONSTRAINT `fk_GrupoMateriaHorarioClase_ASIGNADOTEMPORAL"))
                {
                    exceptionPersonalizada = new Exception("Este lugar esta asignado como salon temporal en uno o mas horarios. Desasigne este lugar de todos los horarios a los que pueda estar asignado antes de eliminarlo.");
                    throw exceptionPersonalizada;
                }



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

            //Decide el string del comando segun la referencia
            switch (referencia)
            {
                case TipoReferencia.Usuario:
                    cmdstr = "INSERT INTO Usuario (CI, PIN, Nombre, Apellido) VALUES (@CI, @PIN, @Nombre, @Apellido);";
                    break;

                case TipoReferencia.Alumno:
                    cmdstr = "INSERT INTO Alumno (CI_Alumno) VALUES (@CI_Alumno);";
                    break;

                case TipoReferencia.Turno:
                    cmdstr = "INSERT INTO Turno (Turno, Nombre_Turno) VALUES (@Turno, @Nombre_Turno);";
                    break;

                case TipoReferencia.Materia:
                    cmdstr = "INSERT INTO Materia (ID_Materia, Nombre) VALUES (@ID_Materia, @Nombre);";
                    break;

                case TipoReferencia.Grupo:
                    cmdstr = "INSERT INTO Grupo (Anio, Turno, ID_Grupo, Orientacion) VALUES (@Anio, @Turno, @ID_Grupo, @Orientacion);";
                    break;

                case TipoReferencia.Docente:
                    cmdstr = "INSERT INTO Docente (CI_Docente) VALUES (@CI_Docente);";
                    break;

                case TipoReferencia.Orientacion:
                    cmdstr = "INSERT INTO Orientacion (Orientacion, Nombre_Orientacion) VALUES (@Orientacion, @Nombre_Orientacion);";
                    break;

                case TipoReferencia.Hora:
                    cmdstr = "INSERT INTO Horario (ID_Horario, Hora_Inicio, Hora_Fin, Turno) VALUES (@ID_Horario, @Hora_Inicio, @Hora_Fin, @Turno);";
                    break;

                case TipoReferencia.Anio:
                    cmdstr = "INSERT INTO Anio (Anio) VALUES (@Anio);";
                    break;

                case TipoReferencia.CargosFuncionarios:
                    cmdstr = "INSERT INTO Cargo (Cargo, Nombre_Cargo) VALUES (@Cargo, @Nombre_Cargo);";
                    break;

                case TipoReferencia.Lugar:
                    cmdstr = "INSERT INTO Lugar (ID, Nombre, Coordenada_X, Coordenada_Y, Tipo, Piso) VALUES (@ID, @Nombre, @Coordenada_X, @Coordenada_Y, @Tipo, @Piso);";
                    break;

                case TipoReferencia.Funcionario:
                    cmdstr = "INSERT INTO Funcionario (CI_Funcionario, Cargo, Tipo, Fecha_Ingreso) VALUES (@CI_Funcionario, @Cargo, @Tipo, @Fecha_Ingreso);";
                    break;

                case TipoReferencia.TipoDeLugar:
                    cmdstr = "INSERT INTO Tipo_Lugar (Tipo, Nombre_Tipo) VALUES (@Tipo, @Nombre_Tipo);";
                    break;

                case TipoReferencia.Clases:
                    cmdstr = "INSERT INTO Clase (ID_Clase) VALUES (@Id);";
                    break;

                case TipoReferencia.UsoComun:
                    cmdstr = "INSERT INTO Uso_Comun (ID_UsoComun) VALUES (@Id);";
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

                case TipoReferencia.Alumno:
                    if (item is Alumno alumno)
                    {
                        cmd.Parameters.Add("@CI_Alumno", MySqlDbType.Int32).Value = alumno.CI;
                    }
                    break;

                case TipoReferencia.Turno:
                    if (item is Turno turno)
                    {
                        cmd.Parameters.Add("@Turno", MySqlDbType.Byte).Value = turno.Id;
                        cmd.Parameters.Add("@Nombre_Turno", MySqlDbType.VarChar).Value = turno.Nombre;
                    }
                    break;

                case TipoReferencia.Materia:
                    if (item is Materia materia)
                    {
                        cmd.Parameters.Add("@ID_Materia", MySqlDbType.Int16).Value = materia.Id;
                        cmd.Parameters.Add("@Nombre", MySqlDbType.VarChar).Value = materia.Nombre;
                    }
                    break;

                case TipoReferencia.Grupo:
                    if (item is Grupo grupo)
                    {
                        cmd.Parameters.Add("@Anio", MySqlDbType.Year).Value = grupo.Anio;
                        cmd.Parameters.Add("@Turno", MySqlDbType.Byte).Value = grupo.Turno.Id;
                        cmd.Parameters.Add("@ID_Grupo", MySqlDbType.VarChar).Value = grupo.Nombre;
                        cmd.Parameters.Add("@Orientacion", MySqlDbType.Byte).Value = grupo.Orientacion.Id;
                    }
                    break;

                case TipoReferencia.Docente:
                    if (item is Docente docente)
                    {
                        cmd.Parameters.Add("@CI_Docente", MySqlDbType.Int32).Value = docente.CI;
                    }
                    break;

                case TipoReferencia.Orientacion:
                    if (item is Orientacion orientacion)
                    {
                        cmd.Parameters.Add("@Orientacion", MySqlDbType.Byte).Value = orientacion.Id;
                        cmd.Parameters.Add("@Nombre_Orientacion", MySqlDbType.VarChar).Value = orientacion.Nombre;
                    }
                    break;

                case TipoReferencia.Hora:
                    if (item is Hora hora)
                    {
                        cmd.Parameters.Add("@ID_Horario", MySqlDbType.Int16).Value = hora.Nid;
                        cmd.Parameters.Add("@Hora_Inicio", MySqlDbType.Time).Value = hora.Inicio;
                        cmd.Parameters.Add("@Hora_Fin", MySqlDbType.Time).Value = hora.Fin;
                        cmd.Parameters.Add("@Turno", MySqlDbType.Byte).Value = hora.Turno.Id;
                    }
                    break;

                case TipoReferencia.Anio:
                    if (item is int Anio)
                    {
                        cmd.Parameters.Add("@Anio", MySqlDbType.Int32).Value = Anio;
                    }
                    break;

                case TipoReferencia.CargosFuncionarios:
                    if (item is Cargo cargo)
                    {
                        cmd.Parameters.Add("@Cargo", MySqlDbType.Byte).Value = cargo.Id;
                        cmd.Parameters.Add("@Nombre_Cargo", MySqlDbType.VarChar).Value = cargo.Nombre;
                    }
                    break;

                case TipoReferencia.Lugar:
                    if (item is Lugar lugar)
                    {
                        cmd.Parameters.Add("@ID", MySqlDbType.Int32).Value = lugar.ID;
                        cmd.Parameters.Add("@Nombre", MySqlDbType.VarChar).Value = lugar.Nombre;
                        cmd.Parameters.Add("@Coordenada_X", MySqlDbType.Int32).Value = lugar.Coordenada_x;
                        cmd.Parameters.Add("@Coordenada_Y", MySqlDbType.Int32).Value = lugar.Coordenada_y;
                        cmd.Parameters.Add("@Tipo", MySqlDbType.Byte).Value = lugar.Tipo.Id;
                        cmd.Parameters.Add("@Piso", MySqlDbType.Byte).Value = lugar.Piso;
                    }
                    break;

                case TipoReferencia.Funcionario:
                    if (item is Funcionario funcionario)
                    {
                        byte admn = (byte)(funcionario.IsAdmn ? 1 : 0);
                        cmd.Parameters.Add("@CI_Funcionario", MySqlDbType.Int32).Value = funcionario.CI;
                        cmd.Parameters.Add("@Tipo", MySqlDbType.Byte).Value = admn;
                        cmd.Parameters.Add("@Cargo", MySqlDbType.Byte).Value = funcionario.Cargo.Id;
                        cmd.Parameters.Add("@Fecha_Ingreso", MySqlDbType.Date).Value = funcionario.FechaIngreso;
                    }
                    break;

                case TipoReferencia.TipoDeLugar:
                    if (item is TipoLugar tipoLugar)
                    {
                        cmd.Parameters.Add("@Tipo", MySqlDbType.Byte).Value = tipoLugar.Id;
                        cmd.Parameters.Add("@Nombre_Tipo", MySqlDbType.VarChar).Value = tipoLugar.Nombre;
                    }
                    break;

                case TipoReferencia.Clases:
                    if (item is Lugar lugarAuxiliarParaClases)
                    {
                        cmd.Parameters.Add("@Id", MySqlDbType.Int32).Value = lugarAuxiliarParaClases.ID;
                    }
                    break;
                case TipoReferencia.UsoComun:
                    if (item is Lugar lugarAuxiliarParaUsoComun)
                    {
                        cmd.Parameters.Add("@Id", MySqlDbType.Int32).Value = lugarAuxiliarParaUsoComun.ID;
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
            string cmdstr;
            MySqlConnection conn = Conector.crearInstancia().crearConexion(); ;
            MySqlCommand cmd;

            //Decide valores de tabla valores y clave para el string del comando
            switch (referencia)
            {
                case TipoReferencia.Usuario:
                    cmdstr = "UPDATE Usuario SET Nombre=@Nombre, Apellido=@Apellido WHERE CI=@CI;";
                    break;

                case TipoReferencia.Turno:
                    cmdstr = "UPDATE Turno SET Nombre_Turno=@Nombre_Turno WHERE Turno=@Turno;";
                    break;

                case TipoReferencia.Materia:
                    cmdstr = "UPDATE Materia SET Nombre=@Nombre WHERE ID_Materia=@ID_Materia;";
                    break;

                case TipoReferencia.Grupo:
                    cmdstr = "UPDATE Grupo SET Anio=@Anio, Turno=@Turno, Orientacion=@Orientacion WHERE ID_Grupo=@ID_Grupo;";
                    break;

                case TipoReferencia.Orientacion:
                    cmdstr = "UPDATE Orientacion SET Nombre_Orientacion=@Nombre_Orientacion WHERE Orientacion=@Orientacion;";
                    break;

                case TipoReferencia.Lugar:
                    cmdstr = "UPDATE Lugar SET Nombre=@Nombre, Tipo=@Tipo WHERE ID=@ID;";
                    break;

                case TipoReferencia.Funcionario:
                    cmdstr = "UPDATE Funcionario SET Cargo=@Cargo, Tipo=@Tipo WHERE CI_Funcionario=@CI_Funcionario;";
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
                        cmd.Parameters.Add("@CI", DbType.Int32).Value = Convert.ToInt32(idObjetivo);
                        cmd.Parameters.Add("@Nombre", MySqlDbType.VarChar).Value = usuario.Nombre;
                        cmd.Parameters.Add("@Apellido", MySqlDbType.VarChar).Value = usuario.Apellido;
                    }
                    break;

                case TipoReferencia.Turno:
                    if (item is Turno turno)
                    {
                        cmd.Parameters.Add("@Turno", DbType.Byte).Value = Convert.ToByte(idObjetivo);
                        cmd.Parameters.Add("@Nombre_Turno", MySqlDbType.VarChar).Value = turno.Nombre;
                    }
                    break;

                case TipoReferencia.Materia:
                    if (item is Materia materia)
                    {
                        cmd.Parameters.Add("@ID_Materia", DbType.Int16).Value = Convert.ToInt16(idObjetivo);
                        cmd.Parameters.Add("@Nombre", MySqlDbType.VarChar).Value = materia.Nombre;
                    }
                    break;

                case TipoReferencia.Grupo:
                    if (item is Grupo grupo)
                    {
                        cmd.Parameters.Add("@ID_Grupo", MySqlDbType.VarChar).Value = Convert.ToString(idObjetivo);
                        cmd.Parameters.Add("@Anio", MySqlDbType.Year).Value = grupo.Anio;
                        cmd.Parameters.Add("@Turno", MySqlDbType.Byte).Value = grupo.Turno.Id;
                        cmd.Parameters.Add("@Orientacion", MySqlDbType.Byte).Value = grupo.Orientacion.Id;
                    }
                    break;

                case TipoReferencia.Orientacion:
                    if (item is Orientacion orientacion)
                    {
                        cmd.Parameters.Add("@Orientacion", DbType.Byte).Value = Convert.ToByte(idObjetivo);
                        cmd.Parameters.Add("@Nombre_Orientacion", MySqlDbType.VarChar).Value = orientacion.Nombre;
                    }
                    break;

                case TipoReferencia.Lugar:
                    if (item is Lugar lugar)
                    {
                        cmd.Parameters.Add("@ID", DbType.Int32).Value = Convert.ToInt32(idObjetivo);
                        cmd.Parameters.Add("@Nombre", MySqlDbType.VarChar).Value = lugar.Nombre;
                        cmd.Parameters.Add("@Tipo", MySqlDbType.Byte).Value = lugar.Tipo.Id;
                    }
                    break;

                case TipoReferencia.Funcionario:
                    if (item is Funcionario funcionario)
                    {
                        cmd.Parameters.Add("@CI_Funcionario", DbType.Int32).Value = Convert.ToInt32(idObjetivo);
                        cmd.Parameters.Add("@Tipo", MySqlDbType.Byte).Value = funcionario.IsAdmn;
                        cmd.Parameters.Add("@Cargo", MySqlDbType.Byte).Value = funcionario.Cargo.Id;
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
            object respuesta = null;
            string cmdstr;
            MySqlConnection conn = Conector.crearInstancia().crearConexion(); ;
            MySqlCommand cmd;
            MySqlDataReader dr;

            switch (referencia)
            {
                case TipoReferencia.Usuario:
                    cmdstr = "SELECT CI, Nombre, Apellido FROM Usuario WHERE CI = @CI;";
                    break;

                case TipoReferencia.Alumno:
                    cmdstr = "SELECT CI_Alumno, Nombre, Apellido FROM Usuario_Alumno WHERE CI_Alumno = @CI_Alumno;";
                    break;

                case TipoReferencia.Turno:
                    cmdstr = "SELECT Turno, Nombre_Turno FROM Turno WHERE Turno=@Turno;";
                    break;

                case TipoReferencia.Materia:
                    cmdstr = "SELECT ID_Materia, Nombre FROM Materia WHERE ID_Materia=@ID_Materia;";
                    break;

                case TipoReferencia.Grupo:
                    cmdstr = "SELECT ID_Grupo, Anio, Orientacion, Nombre_Orientacion, Turno, Nombre_Turno, Lista FROM Lista_Grupos WHERE ID_Grupo = @ID_Grupo;";
                    break;

                case TipoReferencia.Docente:
                    cmdstr = "SELECT Nombre, Apellido, CI_Docente FROM Usuario_Docente WHERE CI_Docente=@CI_Docente;";
                    break;

                case TipoReferencia.Orientacion:
                    cmdstr = "SELECT Orientacion, Nombre_Orientacion FROM Orientacion WHERE Orientacion=@Orientacion;";
                    break;

                case TipoReferencia.Hora:
                    cmdstr = "SELECT ID_Horario, Turno, Nombre_Turno, Hora_Inicio, Hora_Fin FROM Lista_Horas WHERE ID_Horario=@ID_Horario;";
                    break;

                case TipoReferencia.Anio:
                    cmdstr = "SELECT Anio FROM Anio WHERE Anio=@Anio;";
                    break;

                case TipoReferencia.CargosFuncionarios:
                    cmdstr = "SELECT Cargo, Nombre_Cargo FROM Cargo WHERE Cargo=@Cargo;";
                    break;

                case TipoReferencia.Lugar:
                    cmdstr = "SELECT ID, Nombre, Tipo, Nombre_Tipo, Coordenada_X, Coordenada_Y, Piso, AptoParaClase, UsoComun, EstadoOcupacion FROM Lugares WHERE ID=@ID;";
                    break;

                case TipoReferencia.Funcionario:
                    cmdstr = "SELECT Nombre, Apellido, CI_Funcionario, Cargo, Nombre_Cargo, Tipo, Fecha_Ingreso FROM Usuario_Funcionario WHERE CI_Funcionario=@CI_Funcionario;";
                    break;

                case TipoReferencia.TipoDeLugar:
                    cmdstr = "SELECT Tipo, Nombre_Tipo FROM Tipo_Lugar WHERE Tipo=@Tipo;";
                    break;
                default:
                    throw new ArgumentException("Argumento de consulta invalido, contacte a un administrador si el problema persiste");
            }

            //Agrega los parametros al comando
            cmd = new MySqlCommand(cmdstr, conn);

            //Segun la referencia pasa la id como un valor de base de datos u otro
            switch (referencia)
            {
                case TipoReferencia.Usuario:
                    cmd.Parameters.Add("@CI", MySqlDbType.Int32).Value = Convert.ToInt32(idObjetivo);
                    break;

                case TipoReferencia.Alumno:
                    cmd.Parameters.Add("@CI_Alumno", MySqlDbType.Int32).Value = Convert.ToInt32(idObjetivo);
                    break;

                case TipoReferencia.Turno:
                    cmd.Parameters.Add("@Turno", MySqlDbType.Byte).Value = Convert.ToByte(idObjetivo);
                    break;

                case TipoReferencia.Materia:
                    cmd.Parameters.Add("@ID_Materia", MySqlDbType.Int16).Value = Convert.ToInt16(idObjetivo);
                    break;

                case TipoReferencia.Grupo:
                    cmd.Parameters.Add("@ID_Grupo", MySqlDbType.VarChar).Value = Convert.ToString(idObjetivo);
                    break;

                case TipoReferencia.Docente:
                    cmd.Parameters.Add("@CI_Docente", MySqlDbType.Int32).Value = Convert.ToInt32(idObjetivo);
                    break;

                case TipoReferencia.Orientacion:
                    cmd.Parameters.Add("@Orientacion", MySqlDbType.Byte).Value = Convert.ToByte(idObjetivo);
                    break;

                // HORARIO (grupo materia horario clase)

                case TipoReferencia.Hora:
                    cmd.Parameters.Add("@ID_Horario", MySqlDbType.Int16).Value = Convert.ToInt16(idObjetivo);
                    break;

                case TipoReferencia.Anio:
                    cmd.Parameters.Add("@Anio", MySqlDbType.Year).Value = Convert.ToInt32(idObjetivo);
                    break;

                case TipoReferencia.CargosFuncionarios:
                    cmd.Parameters.Add("@Cargo", MySqlDbType.Byte).Value = Convert.ToByte(idObjetivo);
                    break;

                case TipoReferencia.Lugar:
                    cmd.Parameters.Add("@ID", MySqlDbType.Int32).Value = Convert.ToInt32(idObjetivo);
                    break;

                case TipoReferencia.Funcionario:
                    cmd.Parameters.Add("@CI_Funcionario", MySqlDbType.Int32).Value = Convert.ToInt32(idObjetivo);
                    break;

                case TipoReferencia.TipoDeLugar:
                    cmd.Parameters.Add("@Tipo", MySqlDbType.Byte).Value = Convert.ToByte(idObjetivo);
                    break;
            }

            //Ejecuta la consulta
            try
            {
                conn.Open(); //Abro la conexión

                dr = cmd.ExecuteReader(); //Inicio el comando
                if (dr.HasRows)
                {
                    dr.Read();

                    switch (referencia)//Segun la referencia inicializa aux de una forma u otra
                    {
                        case TipoReferencia.Usuario:
                            respuesta = new Usuario(dr.GetString(1), dr.GetString(2), dr.GetInt32(0));
                            break;

                        case TipoReferencia.Alumno:
                            respuesta = new Alumno(dr.GetString(1), dr.GetString(2), dr.GetInt32(0));
                            break;

                        case TipoReferencia.Turno:
                            respuesta = new Turno(dr.GetByte(0), dr.GetString(1));
                            break;

                        case TipoReferencia.Materia:
                            respuesta = new Materia(dr.GetUInt16(0), dr.GetString(1));
                            break;

                        case TipoReferencia.Grupo:
                            Turno auxturnogrupo = new Turno(dr.GetByte(4), dr.GetString(5));
                            Orientacion auxorientacion = new Orientacion(dr.GetByte(2), dr.GetString(3));
                            respuesta = new Grupo(dr.GetString(0), auxturnogrupo, auxorientacion, dr.GetInt32(1), dr.GetByte(6));
                            break;

                        case TipoReferencia.Docente:
                            respuesta = new Docente(dr.GetString(0), dr.GetString(1), dr.GetInt32(2));
                            break;

                        case TipoReferencia.Orientacion:
                            respuesta = new Orientacion(dr.GetByte(0), dr.GetString(1));
                            break;

                        case TipoReferencia.Hora:
                            Turno auxturnohora = new Turno(dr.GetByte(1), dr.GetString(2));
                            respuesta = new Hora((dr.GetByte(0), auxturnohora), dr.GetTimeSpan(3), dr.GetTimeSpan(4));
                            break;

                        case TipoReferencia.Anio:
                            respuesta = dr.GetInt32(0);
                            break;

                        case TipoReferencia.CargosFuncionarios:
                            respuesta = new Cargo(dr.GetByte(0), dr.GetString(1));
                            break;

                        case TipoReferencia.Lugar:
                            TipoLugar auxtipolugar = new TipoLugar(dr.GetByte(2), dr.GetString(3));
                            respuesta = new Lugar(dr.GetUInt16(0), dr.GetString(1), dr.GetInt32(4), dr.GetInt32(5), dr.GetByte(6), dr.GetBoolean(7), dr.GetBoolean(8), auxtipolugar, dr.GetBoolean(9));
                            break;

                        case TipoReferencia.Funcionario:
                            Cargo auxcargofuncionario = new Cargo(dr.GetByte(3), dr.GetString(4));
                            respuesta = new Funcionario(dr.GetString(0), dr.GetString(1), dr.GetInt32(2), auxcargofuncionario, dr.GetBoolean(5), dr.GetDateTime(6));
                            break;

                        case TipoReferencia.TipoDeLugar:
                            respuesta = new TipoLugar(dr.GetByte(0), dr.GetString(1));
                            break;

                        default: throw new ArgumentException("Argumento de consulta imposible de transformar, contacte a un administrador si el problema persiste");
                    }
                }
                return respuesta;//Retorna lo que encuentre
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
        public int GenerarIdAutomatico(TipoReferencia referencia) //Metodo para generar un id automatico para las tablas a las que el usuario no les asigna uno
                                                                  //Segun la referencia devuelve el id mas alto de una tabla mas uno
        {
            int respuesta;
            string cmdstr;
            MySqlConnection conn = Conector.crearInstancia().crearConexion(); ;
            MySqlCommand cmd;

            switch (referencia)
            {
                case TipoReferencia.Lugar:
                    cmdstr = "SELECT MAX(ID) FROM Lugar;";
                    break;
                case TipoReferencia.Materia:
                    cmdstr = "SELECT MAX(ID_Materia) FROM Materia;";
                    break;
                case TipoReferencia.Orientacion:
                    cmdstr = "SELECT MAX(Orientacion) FROM Orientacion;";
                    break;
                case TipoReferencia.Turno:
                    cmdstr = "SELECT MAX(Turno) FROM Turno;";
                    break;

                default: throw new ArgumentException("No se pudo conseguir id, referencia no reconocida");
            }
            cmd = new MySqlCommand(cmdstr, conn);
            try
            {
                conn.Open();
                respuesta = Convert.ToInt32(cmd.ExecuteScalar());
                respuesta++;
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
            return respuesta;
        }
        public bool VerificarNombreNuevo(TipoReferencia referencia, string nombre)
        {
            bool respuesta;
            string cmdstr;
            MySqlConnection conn = Conector.crearInstancia().crearConexion(); ;
            MySqlCommand cmd;

            switch (referencia)
            {
                case TipoReferencia.Lugar:
                    cmdstr = "SELECT COUNT(Lugar.ID) FROM Lugar WHERE Lugar.Nombre = @Nombre;";
                    break;
                case TipoReferencia.Materia:
                    cmdstr = "SELECT COUNT(ID_Materia) FROM Materia WHERE Materia.Nombre = @Nombre;";
                    break;
                case TipoReferencia.Orientacion:
                    cmdstr = "SELECT COUNT(Orientacion) FROM Orientacion WHERE Orientacion.Nombre_Orientacion = @Nombre;";
                    break;
                case TipoReferencia.Turno:
                    cmdstr = "SELECT COUNT(Turno) FROM Turno WHERE Turno.Nombre_Turno = @Nombre;";
                    break;

                default: throw new ArgumentException("No se pudo conseguir id, referencia no reconocida");
            }

            cmd = new MySqlCommand(cmdstr, conn);
            cmd.Parameters.AddWithValue("@Nombre", nombre);
            try
            {
                conn.Open();
                respuesta = Convert.ToInt32(cmd.ExecuteScalar()) == 0;
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
            return respuesta;
        }
        public bool VerificarNombreNuevo(TipoReferencia referencia, string nombre, string id) //Este es para usarse en ediciones para verificar que no haya un mismo nombre en otro id
        {
            bool respuesta;
            string cmdstr;
            MySqlConnection conn = Conector.crearInstancia().crearConexion(); ;
            MySqlCommand cmd;

            switch (referencia)
            {
                case TipoReferencia.Lugar:
                    cmdstr = "SELECT COUNT(Lugar.ID) FROM Lugar WHERE Lugar.Nombre = @Nombre AND Lugar.ID != @ID;";
                    break;
                case TipoReferencia.Materia:
                    cmdstr = "SELECT COUNT(ID_Materia) FROM Materia WHERE Materia.Nombre = @Nombre AND ID_Materia != @ID;";
                    break;
                case TipoReferencia.Orientacion:
                    cmdstr = "SELECT COUNT(Orientacion) FROM Orientacion WHERE Orientacion.Nombre_Orientacion = @Nombre AND Orientacion.Orientacion != @ID;";
                    break;
                case TipoReferencia.Turno:
                    cmdstr = "SELECT COUNT(Turno) FROM Turno WHERE Turno.Nombre_Turno = @Nombre AND Turno.Turno != @ID;";
                    break;

                default: throw new ArgumentException("No se pudo conseguir id, referencia no reconocida");
            }

            cmd = new MySqlCommand(cmdstr, conn);
            cmd.Parameters.AddWithValue("@Nombre", nombre);
            cmd.Parameters.AddWithValue("@ID", id);
            try
            {
                conn.Open();
                respuesta = Convert.ToInt32(cmd.ExecuteScalar()) == 0;
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
            return respuesta;
        }

        //Sobrecargas de operaciones basicas para entidades con debilidad (programado solo para horas ya que son la unica entidad debil que tenemos)
        #region
        public RetornoValidacion Eliminar(TipoReferencia referencia, byte idObjetivo, byte idPadre)
        {
            RetornoValidacion respuesta;
            string cmdstr = "DELETE from @TablaReferencia where @ClavePrimaria=@ID AND @ClavePadre=@IDPadre";
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
                string mensaje = ex.Message;
                Exception exceptionPersonalizada;

                if (mensaje.Contains("CONSTRAINT `fk_GrupoMateriaHorario_IDHORARIO_TURNO`"))
                {
                    exceptionPersonalizada = new Exception("Esta hora se encuentra asignada en uno o mas horarios. Elimine los horarios a los que esta hora este asignada antes de eliminarla.");
                    throw exceptionPersonalizada;
                }

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
            string cmdstr;
            MySqlConnection conn = Conector.crearInstancia().crearConexion();
            MySqlCommand cmd;

            //Decide valores de tabla valores y clave para el string del comando
            switch (referencia)
            {
                case TipoReferencia.Hora:
                    cmdstr = "UPDATE Horario SET Hora_Inicio = @Hora_Inicio, Hora_Fin = @Hora_Fin WHERE ID_Horario = @ID AND Horario.Turno = @IDPadre;";
                    break;

                default:
                    throw new ArgumentException("Argumento de editado invalido, contacte a un administrador si el problema persiste");

            }

            //Agrega los parametros al comando
            cmd = new MySqlCommand(cmdstr, conn);

            //Asigna los valores para agregar en el comando
            switch (referencia)
            {
                case TipoReferencia.Hora:
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
            object respuesta = null;
            string cmdstr;
            MySqlConnection conn = Conector.crearInstancia().crearConexion(); ;
            MySqlCommand cmd;
            MySqlDataReader dr;

            switch (referencia)
            {
                case TipoReferencia.Hora:
                    cmdstr = "SELECT Horario.ID_Horario, Horario.Turno, Turno.Nombre_Turno, Horario.Hora_Inicio, Horario.Hora_Fin FROM Horario JOIN Turno ON Horario.Turno = Turno.Turno WHERE ID_Horario=@ID AND Horario.Turno=@IDPadre;";
                    break;
                default:
                    throw new ArgumentException("Argumento de consulta invalido, contacte a un administrador si el problema persiste");
            }

            //Agrega los parametros al comando
            cmd = new MySqlCommand(cmdstr, conn);

            //Segun la referencia pasa la id como un valor de base de datos u otro
            switch (referencia)
            {
                case TipoReferencia.Hora:
                    cmd.Parameters.Add("@ID", MySqlDbType.Int16).Value = idObjetivo;
                    cmd.Parameters.Add("@IDPadre", MySqlDbType.Byte).Value = idPadre;
                    break;
            }

            //Ejecuta la consulta
            try
            {
                conn.Open(); //Abro la conexión

                dr = cmd.ExecuteReader(); //Inicio el comando

                if (dr.HasRows)
                {
                    dr.Read();
                    switch (referencia)//Segun la referencia inicializa aux de una forma u otra
                    {
                        case TipoReferencia.Hora:
                            Turno auxturno = new Turno(dr.GetByte(1), dr.GetString(2));
                            respuesta = new Hora((dr.GetByte(0), auxturno), dr.GetTimeSpan(3), dr.GetTimeSpan(4));
                            break;
                        default: throw new ArgumentException("Argumento de consulta imposible de transformar, contacte a un administrador si el problema persiste");
                    }
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
                MySqlCommand cmd = new MySqlCommand("SELECT Grupo.ID_Grupo FROM Grupo INNER JOIN Grupo_Alumno ON Grupo.ID_Grupo = Grupo_Alumno.ID_Grupo" +
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

        //Metodos para usar con la consulta de grupo
        #region
        public List<(Materia, Docente)> ListarMateriasYDocentesDeGrupo(string idGrupo)
        {
            //Variables
            List<(Materia, Docente)> lista = new List<(Materia, Docente)>();
            Materia auxmateria;
            Docente auxdocente;
            (Materia m, Docente d) auxmateriadocente;
            string cmdstr;
            MySqlConnection conn = Conector.crearInstancia().crearConexion();
            MySqlCommand cmd;
            MySqlDataReader dr;

            //Asiganmos el valor de la consulta
            cmdstr = "SELECT gm.ID_Materia, m.Nombre, gmd.CI_Docente, u.Nombre, u.Apellido " +
                "\r\n FROM Grupo grp" +
                "\r\n JOIN Grupo_Materia gm ON grp.ID_Grupo = gm.ID_Grupo" +
                "\r\n JOIN Materia m ON gm.ID_Materia = m.ID_Materia " +
                "\r\n LEFT JOIN Grupo_Materia_Docente gmd ON  gm.ID_Grupo = gmd.ID_Grupo AND gm.ID_Materia = gmd.ID_Materia" +
                "\r\n LEFT JOIN Docente d ON gmd.CI_Docente = d.CI_Docente" +
                "\r\n LEFT JOIN Usuario u ON d.CI_Docente = u.CI" +
                "\r\n WHERE grp.ID_Grupo = @IdGrupo;";


            //Inicializa y agrega los parametros al comando
            cmd = new MySqlCommand(cmdstr, conn);
            cmd.Parameters.AddWithValue("@IdGrupo", idGrupo);

            //Ejecuta la consulta
            try
            {
                conn.Open(); //Abro la conexión

                dr = cmd.ExecuteReader(); //Inicio el comando

                if (dr.HasRows) //Si el comando devuelve algo ejecuta el while
                {
                    while (dr.Read()) //Mientras haya registros en el datareader lee lo que hay y lo agrega a la lista
                    {
                        auxmateria = null;
                        auxdocente = null;
                        auxmateriadocente = (null, null);

                        auxmateria = new Materia(dr.GetUInt16(0), dr.GetString(1));
                        if (!dr.IsDBNull(3))
                        {
                            auxdocente = new Docente(dr.GetString(3), dr.GetString(4), dr.GetInt32(2));
                        }

                        auxmateriadocente = (auxmateria, auxdocente);

                        lista.Add(auxmateriadocente);
                    }

                }
                return lista;//Retorna lo que encuentre
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

        public List<Alumno> ListarAlumnosDeGrupo(string idGrupo)
        {
            //Variables
            List<Alumno> lista = new List<Alumno>();
            Alumno auxalumno;
            string cmdstr;
            MySqlConnection conn = Conector.crearInstancia().crearConexion();
            MySqlCommand cmd;
            MySqlDataReader dr;

            //Asiganmos el valor de la consulta
            cmdstr = "SELECT ga.CI_Alumno, u.Nombre, u.Apellido" +
                "\r\n FROM Grupo grp" +
                "\r\n JOIN Grupo_Alumno ga ON grp.ID_Grupo = ga.ID_Grupo" +
                "\r\n JOIN Alumno a ON ga.CI_Alumno = a.CI_Alumno" +
                "\r\n JOIN Usuario u ON a.CI_Alumno = u.CI" +
                "\r\n WHERE grp.ID_Grupo = @IdGrupo;";


            //Inicializa y agrega los parametros al comando
            cmd = new MySqlCommand(cmdstr, conn);
            cmd.Parameters.AddWithValue("@IdGrupo", idGrupo);

            //Ejecuta la consulta
            try
            {
                conn.Open(); //Abro la conexión

                dr = cmd.ExecuteReader(); //Inicio el comando

                if (dr.HasRows) //Si el comando devuelve algo ejecuta el while
                {
                    while (dr.Read()) //Mientras haya registros en el datareader lee lo que hay y lo agrega a la lista
                    {
                        auxalumno = new Alumno(dr.GetString(1), dr.GetString(2), dr.GetInt32(0));

                        lista.Add(auxalumno);
                    }

                }
                return lista;//Retorna lo que encuentre
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

        public bool ConsultarAlumnoEnGrupo(string ciAlumno, string idGrupo) //Devuelve true si un alumno esta ingresado dentro de un grupo
        {
            //Variables
            bool respuesta;
            string cmdstr;
            MySqlConnection conn = Conector.crearInstancia().crearConexion();
            MySqlCommand cmd;

            //Asiganmos el valor de la consulta
            cmdstr = "SELECT COUNT(u.CI)" +
                "\r\n FROM Grupo g " +
                "\r\n JOIN Grupo_Alumno ga ON g.ID_Grupo = ga.ID_Grupo" +
                "\r\n JOIN Alumno a ON ga.CI_Alumno = a.CI_Alumno " +
                "\r\n JOIN Usuario u ON a.CI_Alumno = u.CI" +
                "\r\n WHERE g.ID_Grupo = @ID_Grupo AND a.CI_Alumno = @CI_Alumno;";


            //Inicializa y agrega los parametros al comando
            cmd = new MySqlCommand(cmdstr, conn);
            cmd.Parameters.AddWithValue("@ID_Grupo", idGrupo);
            cmd.Parameters.AddWithValue("@CI_Alumno", ciAlumno);

            //Ejecuta la consulta
            try
            {
                conn.Open(); //Abro la conexión

                respuesta = Convert.ToInt32(cmd.ExecuteScalar()) == 1;

                return respuesta;//Retorna lo que encuentre
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

        public bool ConsultarMateriaEnGrupo(ushort idMateria, string idGrupo) //Devuelve true si una materia esta ingresada dentro de un grupo
        {
            //Variables
            bool respuesta;
            string cmdstr;
            MySqlConnection conn = Conector.crearInstancia().crearConexion();
            MySqlCommand cmd;

            //Asiganmos el valor de la consulta
            cmdstr = "SELECT COUNT(m.ID_Materia) " +
                "\r\n FROM Grupo g " +
                "\r\n JOIN Grupo_Materia gm ON g.ID_Grupo = gm.ID_Grupo " +
                "\r\n JOIN Materia m ON gm.ID_Materia= m.ID_Materia" +
                "\r\n WHERE g.ID_Grupo = @ID_Grupo AND m.ID_Materia = @IdMateria;";


            //Inicializa y agrega los parametros al comando
            cmd = new MySqlCommand(cmdstr, conn);
            cmd.Parameters.AddWithValue("@ID_Grupo", idGrupo);
            cmd.Parameters.AddWithValue("@IdMateria", idMateria);

            //Ejecuta la consulta
            try
            {
                conn.Open(); //Abro la conexión

                respuesta = Convert.ToInt32(cmd.ExecuteScalar()) == 1;

                return respuesta;//Retorna lo que encuentre
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

        public bool ConsultarDocenteEnGrupoMateria(string ciDocente, string idGrupo, ushort idMateria) //Devuelve true si un docente esta ingresado en una materia en un grupo
        {
            //Variables
            bool respuesta;
            string cmdstr;
            MySqlConnection conn = Conector.crearInstancia().crearConexion();
            MySqlCommand cmd;

            //Asiganmos el valor de la consulta
            cmdstr = "SELECT COUNT(u.CI) " +
                "\r\n FROM Grupo g " +
                "\r\n JOIN Grupo_Materia gm ON g.ID_Grupo = gm.ID_Grupo " +
                "\r\n JOIN Grupo_Materia_Docente gmd ON gm.ID_Grupo = gmd.ID_Grupo AND gm.ID_Materia = gmd.ID_Materia" +
                "\r\n JOIN Docente d ON gmd.CI_Docente = d.CI_Docente" +
                "\r\n JOIN Usuario u ON d.CI_Docente = u.CI " +
                "\r\n WHERE g.ID_Grupo = @IdGrupo AND gm.ID_Materia = @IdMateria AND d.CI_Docente = @CiDocente;";

            //Inicializa y agrega los parametros al comando
            cmd = new MySqlCommand(cmdstr, conn);
            cmd.Parameters.AddWithValue("@IdGrupo", idGrupo);
            cmd.Parameters.AddWithValue("@CiDocente", ciDocente);
            cmd.Parameters.AddWithValue("@IdMateria", idMateria);


            //Ejecuta la consulta
            try
            {
                conn.Open(); //Abro la conexión

                respuesta = Convert.ToInt32(cmd.ExecuteScalar()) == 1;

                return respuesta;//Retorna lo que encuentre
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


        public RetornoValidacion AgregarAlumnoAGrupo(string ciAlumno, string idGrupo)
        {
            //Variables
            RetornoValidacion respuesta;
            string cmdstr;
            MySqlConnection conn = Conector.crearInstancia().crearConexion(); ;
            MySqlCommand cmd;

            cmdstr = "INSERT INTO Grupo_Alumno (CI_Alumno, ID_Grupo) VALUES (@CiAlumno, @IdGrupo);";

            //Agrega los parametros al comando
            cmd = new MySqlCommand(cmdstr, conn);

            cmd.Parameters.Add("@CiAlumno", MySqlDbType.Int32).Value = ciAlumno;
            cmd.Parameters.Add("@IdGrupo", MySqlDbType.VarChar).Value = idGrupo;

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

        public RetornoValidacion AgregarMateriaAGrupo(ushort idMateria, string idGrupo)
        {
            //Variables
            RetornoValidacion respuesta;
            string cmdstr;
            MySqlConnection conn = Conector.crearInstancia().crearConexion(); ;
            MySqlCommand cmd;

            cmdstr = "INSERT Grupo_Materia (ID_Grupo, ID_Materia) VALUES (@IdGrupo, @IdMateria);";

            //Agrega los parametros al comando
            cmd = new MySqlCommand(cmdstr, conn);

            cmd.Parameters.Add("@IdMateria", MySqlDbType.Int16).Value = idMateria;
            cmd.Parameters.Add("@IdGrupo", MySqlDbType.VarChar).Value = idGrupo;

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
        public RetornoValidacion AgregarDocenteAGrupoMateria(int ciDocente, string idGrupo, ushort idMateria)
        {
            //Variables
            RetornoValidacion respuesta;
            string cmdstr;
            MySqlConnection conn = Conector.crearInstancia().crearConexion();
            MySqlCommand cmd;

            cmdstr = "INSERT Grupo_Materia_Docente (ID_Grupo, ID_Materia, CI_Docente) VALUES (@IdGrupo, @IdMateria, @CiDocente);";

            //Agrega los parametros al comando
            cmd = new MySqlCommand(cmdstr, conn);

            cmd.Parameters.Add("@CiDocente", MySqlDbType.Int32).Value = ciDocente;
            cmd.Parameters.Add("@IdMateria", MySqlDbType.Int16).Value = idMateria;
            cmd.Parameters.Add("@IdGrupo", MySqlDbType.VarChar).Value = idGrupo;

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
        public RetornoValidacion EliminarAlumnoDeGrupo(string ciAlumno, string idGrupo)
        {
            RetornoValidacion respuesta;
            string cmdstr;
            MySqlConnection conn = Conector.crearInstancia().crearConexion();
            MySqlCommand cmd;

            cmdstr = "DELETE FROM Grupo_Alumno WHERE Grupo_Alumno.ID_Grupo = @IdGrupo AND Grupo_Alumno.CI_Alumno = @CiAlumno;";

            cmd = new MySqlCommand(cmdstr, conn);

            cmd.Parameters.Add("@CiAlumno", MySqlDbType.Int32).Value = Convert.ToInt32(ciAlumno);
            cmd.Parameters.Add("@IdGrupo", MySqlDbType.VarChar).Value = idGrupo;

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
        public RetornoValidacion EliminarMateriaDeGrupo(string idMateria, string idGrupo)
        {
            RetornoValidacion respuesta;
            string cmdstr;
            MySqlConnection conn = Conector.crearInstancia().crearConexion();
            MySqlCommand cmd;

            cmdstr = "DELETE from Grupo_Materia where Grupo_Materia.ID_Grupo = @IdGrupo AND Grupo_Materia.ID_Materia = @IdMateria;";

            cmd = new MySqlCommand(cmdstr, conn);

            cmd.Parameters.Add("@IdMateria", MySqlDbType.Int16).Value = Convert.ToInt16(idMateria);
            cmd.Parameters.Add("@IdGrupo", MySqlDbType.VarChar).Value = idGrupo;

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

        public RetornoValidacion EliminarDocenteDeMateriaGrupo(string idMateria, string idGrupo, string ciDocente)
        {
            RetornoValidacion respuesta;
            string cmdstr;
            MySqlConnection conn = Conector.crearInstancia().crearConexion();
            MySqlCommand cmd;

            cmdstr = "DELETE from Grupo_Materia_Docente where Grupo_Materia_Docente.ID_Grupo = @idGrupo AND Grupo_Materia_Docente.ID_Materia = @IdMateria AND Grupo_Materia_Docente.CI_Docente = @CiDocente;";

            cmd = new MySqlCommand(cmdstr, conn);


            cmd.Parameters.Add("@CiDocente", MySqlDbType.Int32).Value = Convert.ToInt32(ciDocente);
            cmd.Parameters.Add("@IdMateria", MySqlDbType.Int16).Value = Convert.ToInt16(idMateria);
            cmd.Parameters.Add("@IdGrupo", MySqlDbType.VarChar).Value = idGrupo;

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


        #endregion

        //Metodos para el login
        #region
        public RetornoValidacion IntentarLogIn(string ci, string pin)
        {
            //Variables
            RetornoValidacion respuesta;
            string cmdstr;
            MySqlConnection conn = Conector.crearInstancia().crearConexion();
            MySqlCommand cmd;
            MySqlDataReader dr;

            cmdstr = "SELECT CI, Nombre, Pin, Rol FROM ConsultaLogIn WHERE CI = @CI AND PIN = @Pin;";

            //Agrega los parametros al comando
            cmd = new MySqlCommand(cmdstr, conn);

            cmd.Parameters.Add("@CI", MySqlDbType.Int32).Value = Convert.ToInt32(ci);
            cmd.Parameters.Add("@Pin", MySqlDbType.Int32).Value = Convert.ToInt32(pin);

            //Ejecuta la consulta
            try
            {
                conn.Open(); //Abro la conexión

                dr = cmd.ExecuteReader(); //Inicio el comando
                if (dr.HasRows)
                {
                    Sesion sesion = new Sesion();
                    dr.Read();
                    int ciLog = dr.GetInt32(0);
                    string nombreLog = dr.GetString(1);
                    int pinLog = dr.GetInt32(2);
                    int auxRol = dr.GetInt32(3);
                    TipoRol rolLog;

                    if (auxRol == 2)
                    {
                        rolLog = TipoRol.Docente;
                    }

                    switch (auxRol)
                    {
                        case 0:
                            rolLog = TipoRol.Operador;
                            break;
                        case 1:
                            rolLog = TipoRol.Administrador;
                            break;
                        case 2:
                            rolLog = TipoRol.Docente;
                            break;
                        case 3:
                            rolLog = TipoRol.Alumno;
                            break;
                        default: throw new Exception("Rol no valido");
                    }

                    sesion.LogIn(nombreLog, rolLog, ciLog, pinLog);
                    respuesta = RetornoValidacion.OK;
                    return respuesta;
                }
                else
                {
                    respuesta = RetornoValidacion.NoExiste;
                    return respuesta;
                }
            }
            catch (Exception ex)//En caso de excepcion throwea esta
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
        public RetornoValidacion ActualizarPin(string pin)
        {
            //Variables
            RetornoValidacion respuesta;
            string cmdstr;
            MySqlConnection conn = Conector.crearInstancia().crearConexion(); ;
            MySqlCommand cmd;

            cmdstr = "UPDATE Usuario SET PIN = @Pin WHERE CI = @Ci;";

            //Agrega los parametros al comando
            cmd = new MySqlCommand(cmdstr, conn);

            cmd.Parameters.Add("@CI", MySqlDbType.Int32).Value = Convert.ToInt32(Sesion.LoggedCi);
            cmd.Parameters.Add("@Pin", MySqlDbType.Int32).Value = Convert.ToInt32(pin);

            //Ejecuta la consulta
            try
            {
                conn.Open(); //Abro la conexión

                respuesta = cmd.ExecuteNonQuery() == 1 ? RetornoValidacion.OK : RetornoValidacion.ErrorInesperadoBD;
                return respuesta;
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

        //Metodos para los horarios
        #region
        public RetornoValidacion AgregarHorario(Horario horario)
        {
            //Variables
            RetornoValidacion respuesta = RetornoValidacion.OK;
            string cmdstr1;
            string cmdstr2;
            MySqlConnection conn = Conector.crearInstancia().crearConexion();
            List<(MySqlCommand cmd1, MySqlCommand cmd2)> listacmds = new List<(MySqlCommand, MySqlCommand)>();
            MySqlCommand auxcmd1;
            MySqlCommand auxcmd2;

            foreach (Hora hora in horario.Horas)
            {
                cmdstr1 = null;
                cmdstr2 = null;
                auxcmd1 = null;
                auxcmd2 = null;

                cmdstr1 = "INSERT INTO Grupo_Materia_Horario (ID_Grupo, ID_Materia, ID_Horario, Dia_Semana, Turno) VALUES (@IdGrupo, @IdMateria, @IdHora, @IdDiaSemana, @IdTurno);";

                //Agrega los parametros al comando
                auxcmd1 = new MySqlCommand(cmdstr1, conn);

                auxcmd1.Parameters.Add("@IdGrupo", MySqlDbType.VarChar).Value = horario.Grupo;
                auxcmd1.Parameters.Add("@IdMateria", MySqlDbType.Int16).Value = horario.Materia.Id;
                auxcmd1.Parameters.Add("@IdHora", MySqlDbType.Int16).Value = hora.Nid;
                auxcmd1.Parameters.Add("@IdDiaSemana", MySqlDbType.Byte).Value = horario.Dia.Id;
                auxcmd1.Parameters.Add("@IdTurno", MySqlDbType.Byte).Value = horario.Turno.Id;

                cmdstr2 = "INSERT INTO Grupo_Materia_Horario_Clase (ID_Grupo, ID_Materia, ID_Horario, ID_Clase, Dia_Semana, Turno) " +
                "\r\n VALUES (@IdGrupo, @IdMateria, @IdHora, @IdClase, @IdDiaSemana, @IdTurno);";

                //Agrega los parametros al comando
                auxcmd2 = new MySqlCommand(cmdstr2, conn);

                auxcmd2.Parameters.Add("@IdGrupo", MySqlDbType.VarChar).Value = horario.Grupo;
                auxcmd2.Parameters.Add("@IdMateria", MySqlDbType.Int16).Value = horario.Materia.Id;
                auxcmd2.Parameters.Add("@IdHora", MySqlDbType.Int16).Value = hora.Nid;
                auxcmd2.Parameters.Add("@IdDiaSemana", MySqlDbType.Byte).Value = horario.Dia.Id;
                auxcmd2.Parameters.Add("@IdTurno", MySqlDbType.Byte).Value = horario.Turno.Id;

                auxcmd2.Parameters.Add("@IdClase", MySqlDbType.Int32).Value = horario.Salon.ID;

                listacmds.Add((auxcmd1, auxcmd2));


            }

            // Ejecuta el comando y devuelve un retorno segun como salga la operacion
            try
            {
                conn.Open();

                foreach ((MySqlCommand cmd1, MySqlCommand cmd2) in listacmds)
                {
                    if (cmd1.ExecuteNonQuery() != 1)
                    {
                        respuesta = RetornoValidacion.ErrorInesperadoBD;
                        return respuesta;
                    }
                    if (cmd2.ExecuteNonQuery() != 1)
                    {
                        respuesta = RetornoValidacion.ErrorInesperadoBD;
                        return respuesta;
                    }


                }
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
        public RetornoValidacion EliminarHorario(Horario horario)
        {
            //Variables
            RetornoValidacion respuesta = RetornoValidacion.OK;
            string cmdstr1;
            string cmdstr2;
            MySqlConnection conn = Conector.crearInstancia().crearConexion(); ;
            List<(MySqlCommand cmd1, MySqlCommand cmd2)> listacmds = new List<(MySqlCommand, MySqlCommand)>();
            MySqlCommand auxcmd1;
            MySqlCommand auxcmd2;

            cmdstr1 = "DELETE FROM Grupo_Materia_Horario_Clase " +
                " WHERE ID_Grupo = @IdGrupo " +
                " AND ID_Materia = @IdMateria" +
                " AND ID_Horario = @IdHora " +
                " AND Turno = @IdTurno " +
                " AND Dia_Semana = @IdDiaSemana;";

            cmdstr2 = "DELETE FROM Grupo_Materia_Horario " +
                " WHERE ID_Grupo = @IdGrupo " +
                " AND ID_Materia = @IdMateria " +
                " AND ID_Horario = @IdHora " +
                " AND Turno = @IdTurno " +
                " AND Dia_Semana = @IdDiaSemana;";

            foreach (Hora hora in horario.Horas)
            {
                //Agrega los parametros al comando
                auxcmd1 = new MySqlCommand(cmdstr1, conn);

                auxcmd1.Parameters.Add("@IdGrupo", MySqlDbType.VarChar).Value = horario.Grupo;
                auxcmd1.Parameters.Add("@IdMateria", MySqlDbType.Int16).Value = horario.Materia.Id;
                auxcmd1.Parameters.Add("@IdHora", MySqlDbType.Int16).Value = hora.Nid;
                auxcmd1.Parameters.Add("@IdDiaSemana", MySqlDbType.Byte).Value = horario.Dia.Id;
                auxcmd1.Parameters.Add("@IdTurno", MySqlDbType.Byte).Value = horario.Turno.Id;

                //Agrega los parametros al comando
                auxcmd2 = new MySqlCommand(cmdstr2, conn);

                auxcmd2.Parameters.Add("@IdGrupo", MySqlDbType.VarChar).Value = horario.Grupo;
                auxcmd2.Parameters.Add("@IdMateria", MySqlDbType.Int16).Value = horario.Materia.Id;
                auxcmd2.Parameters.Add("@IdHora", MySqlDbType.Int16).Value = hora.Nid;
                auxcmd2.Parameters.Add("@IdDiaSemana", MySqlDbType.Byte).Value = horario.Dia.Id;
                auxcmd2.Parameters.Add("@IdTurno", MySqlDbType.Byte).Value = horario.Turno.Id;

                listacmds.Add((auxcmd1, auxcmd2));
            }

            // Ejecuta el comando y devuelve un retorno segun como salga la operacion
            try
            {
                conn.Open();

                foreach ((MySqlCommand cmd1, MySqlCommand cmd2) in listacmds)
                {
                    if (cmd1.ExecuteNonQuery() != 1)
                    {
                        // respuesta = RetornoValidacion.ErrorInesperadoBD;
                        // return respuesta;
                    }
                    if (cmd2.ExecuteNonQuery() != 1)
                    {
                        respuesta = RetornoValidacion.ErrorInesperadoBD;
                        return respuesta;
                    }


                }
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
        public bool ConsultarHorarioExiste(Horario horario)
        {
            //Variables
            bool respuesta = false;
            string cmdstr;
            MySqlConnection conn = Conector.crearInstancia().crearConexion(); ;
            List<MySqlCommand> listacmds = new List<MySqlCommand>();
            MySqlCommand auxcmd;
            MySqlDataReader dr;


            cmdstr = "SELECT ID_Grupo, ID_Materia, ID_Horario, Dia_Semana, Turno" +
                "\r\n FROM Grupo_Materia_Horario gmh " +
                "\r\n WHERE gmh.ID_Grupo = @IdGrupo " +
                "\r\n AND gmh.ID_Materia = @IdMateria" +
                "\r\n AND gmh.ID_Horario = @IdHora " +
                "\r\n AND gmh.Turno = @IdTurno" +
                "\r\n AND gmh.Dia_Semana = @DiaSemana;";


            //Agrega los parametros al comando
            foreach (Hora hora in horario.Horas)
            {
                auxcmd = new MySqlCommand(cmdstr, conn);

                auxcmd.Parameters.Add("@IdGrupo", MySqlDbType.VarChar).Value = horario.Grupo;
                auxcmd.Parameters.Add("@IdMateria", MySqlDbType.Int16).Value = horario.Materia.Id;
                auxcmd.Parameters.Add("@IdHora", MySqlDbType.Int16).Value = hora.Nid;
                auxcmd.Parameters.Add("@IdTurno", MySqlDbType.Byte).Value = horario.Turno.Id;
                auxcmd.Parameters.Add("@DiaSemana", MySqlDbType.Byte).Value = horario.Dia.Id;

                listacmds.Add(auxcmd);
            }

            //Ejecuta la consulta
            try
            {
                foreach (MySqlCommand cmd in listacmds)
                {
                    conn.Open(); //Abro la conexión
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        return true;
                    }
                    conn.Close();
                }

                return respuesta;//Retorna lo que encuentre
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
        public MensajeSalonOcupado ConsultarSalonOcupado(ushort idlugar, TimeSpan inicio, TimeSpan fin, byte dia) //Devuelve un MensajeSalonOcupado si un lugar esta ocupado dentro de un intervalo de tiempo
        {
            //Variables
            MensajeSalonOcupado respuesta = null;
            string cmdstr;
            MySqlConnection conn = Conector.crearInstancia().crearConexion(); ;
            MySqlCommand cmd;
            MySqlDataReader dr;


            cmdstr = "SELECT IdGrupo, CONCAT(NombreDocente, ' ', ApellidoDocente) AS NCDocente, Inicio, Fin, NombreDiaSemana FROM ListaHorarios" +
                "\r\n WHERE NOT (@HNInicio >= Fin) -- Si el horario nuevo inicia antes del fin del original no devuelve nada" +
                "\r\n  AND NOT (@HNFin <= Inicio) -- Si el horario nuevo termina antes del inicio del original no devuelve nada" +
                "\r\n  AND ((@HNInicio <= Fin AND @HNInicio >= Inicio) OR (Inicio >= @HNInicio AND Fin <= @HNFin)) -- " +
                "Si el horario nuevo esta contenido dentro del horario original devuelve algo." +
                " Tambien devuelve si el horario original esta contenido dentro del horario nuevo" +
                "\r\n  AND IdSalon_Asignado_Predeterminado = @IdSalon " +
                "\r\n  AND (IdDiaSemana = @IdDiaSemana);";

            cmd = new MySqlCommand(cmdstr, conn);

            cmd.Parameters.Add("@HNInicio", MySqlDbType.Time).Value = inicio;
            cmd.Parameters.Add("@HNFin", MySqlDbType.Time).Value = fin;
            cmd.Parameters.Add("@IdSalon", MySqlDbType.Int32).Value = idlugar;
            cmd.Parameters.Add("@IdDiaSemana", MySqlDbType.Byte).Value = dia;

            //Ejecuta la consulta
            try
            {
                conn.Open();
                dr = cmd.ExecuteReader();
                dr.Read();
                if (!dr.HasRows)
                {
                    return respuesta; //Si la consulta no devuelve nada devuelve un horario inicializado como null
                }


                string nombreGrupo = dr.GetString(0);

                //Si el campo de cedula del docente contiene algo lo agrego
                string nombreDocente = null;
                if (!dr.IsDBNull(1))
                {
                    nombreDocente = dr.GetString(1);
                }

                string horaInicio = dr.GetTimeSpan(2).ToString();
                string horaFin = dr.GetTimeSpan(3).ToString();
                string diaSemana = dr.GetString(4);

                respuesta = new MensajeSalonOcupado(nombreDocente, nombreGrupo, horaInicio, horaFin, diaSemana);


                return respuesta;//Retorna lo que encuentre
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
        public RetornoValidacion AsignarSalonTemporal(Horario horarioDestino, ushort idSalon)
        {
            MySqlConnection conn = Conector.crearInstancia().crearConexion();
            List<MySqlCommand> listacmds = new List<MySqlCommand>();
            MySqlCommand auxcmd;
            string cmdstr;
            RetornoValidacion respuesta = RetornoValidacion.OK;

            //Actualiza el asignado temporal en el horario indicado
            cmdstr = "UPDATE Grupo_Materia_Horario_Clase " +
                " SET Asignado_Temporal = @IdAsignadoTemporal" +
                " WHERE ID_Grupo = @IdGrupo " +
                " AND ID_Materia = @IdMateria " +
                " AND ID_Horario = @IdHora " +
                " AND Turno = @IdTurno " +
                " AND Dia_Semana = @IdDiaSemana;";

            //Arma los comandos para agregarlos a la lista
            foreach (Hora hora in horarioDestino.Horas)
            {
                auxcmd = null;

                auxcmd = new MySqlCommand(cmdstr, conn);

                auxcmd.Parameters.Add("@IdAsignadoTemporal", MySqlDbType.Int32).Value = idSalon;
                auxcmd.Parameters.Add("@IdGrupo", MySqlDbType.VarChar).Value = horarioDestino.Grupo;
                auxcmd.Parameters.Add("@IdMateria", MySqlDbType.Int16).Value = horarioDestino.Materia.Id;
                auxcmd.Parameters.Add("@IdHora", MySqlDbType.Int16).Value = hora.Nid;
                auxcmd.Parameters.Add("@IdTurno", MySqlDbType.Byte).Value = horarioDestino.Turno.Id;
                auxcmd.Parameters.Add("@IdDiaSemana", MySqlDbType.Byte).Value = horarioDestino.Dia.Id;

                listacmds.Add(auxcmd);
            }

            try
            {
                conn.Open();
                //Ejectua la operacion por cada comando
                foreach (MySqlCommand cmd in listacmds)
                {
                    //Ejecuta el comando y si no se ejecuta bien corta el metodo y retorna que hubo un error inesperado
                    if (cmd.ExecuteNonQuery() != 1)
                    {
                        respuesta = RetornoValidacion.ErrorInesperadoBD;
                        return respuesta;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
        #endregion

        //Métodos para lugares de clase
        #region
        public UbicacionClase ObtenerUbicacion(int ci, TipoRol rol, string grupoSeleccionado)
        {
            MySqlConnection conn = Conector.crearInstancia().crearConexion();
            MySqlDataReader dr;
            MySqlCommand cmd;
            string cmdstr = "";
            UbicacionClase ubicacion = new UbicacionClase();

            try
            {
                conn.Open();
                switch (rol)
                {
                    case TipoRol.Alumno:
                        cmdstr = "SELECT L.Nombre AS Salón, L.Coordenada_X, L.Coordenada_Y, L.Piso, G.ID_Grupo AS Grupo, M.Nombre AS Materia FROM Grupo_Alumno AS GA " +
                                 "JOIN Grupo AS G ON GA.ID_Grupo = G.ID_Grupo " +
                                 "JOIN Grupo_Materia_Horario_Clase AS GMHC ON G.ID_Grupo = GMHC.ID_Grupo " +
                                 "JOIN Lugar AS L ON GMHC.ID_Clase = L.ID " +
                                 "JOIN Dia_Semana AS DS ON GMHC.Dia_Semana = DS.Dia_Semana " +
                                 "JOIN Horario AS H ON GMHC.ID_Horario = H.ID_Horario AND GMHC.Turno = H.Turno " +
                                 "JOIN Grupo_Materia AS GM ON G.ID_Grupo = GM.ID_Grupo " +
                                 "JOIN Materia AS M ON GM.ID_Materia = M.ID_Materia " +
                                 "WHERE GA.CI_Alumno = @CI " +
                                 "AND GMHC.Asignado_Temporal IS NULL " +
                                 "AND DAYOFWEEK(CURDATE()) = DS.Dia_Semana " +
                                 "AND CURTIME() BETWEEN H.Hora_Inicio AND H.Hora_Fin";

                        if (!string.IsNullOrEmpty(grupoSeleccionado))
                        {
                            // Si se proporciona un grupo seleccionado, agrega la condición para ese grupo
                            cmdstr += " AND G.ID_Grupo = @GrupoSeleccionado";
                        }

                        cmdstr += ";";
                        break;
                    case TipoRol.Docente:
                        cmdstr = "SELECT L.Nombre AS Salón, L.Coordenada_X, L.Coordenada_Y, L.Piso, G.ID_Grupo AS Grupo, " +
                            "M.Nombre AS Materia FROM usuario_docente AS UD JOIN Grupo_Materia_Docente AS GMD ON UD.CI_Docente = GMD.CI_Docente JOIN Grupo AS G " +
                            "ON GMD.ID_Grupo = G.ID_Grupo JOIN Grupo_Materia AS GM ON G.ID_Grupo = GM.ID_Grupo JOIN Materia AS M ON GM.ID_Materia = M.ID_Materia " +
                            "JOIN Grupo_Materia_Horario AS GMH ON GM.ID_Grupo = GMH.ID_Grupo AND GM.ID_Materia = GMH.ID_Materia JOIN Horario AS H " +
                            "ON GMH.ID_Horario = H.ID_Horario AND GMH.Turno = H.Turno JOIN Dia_Semana AS DS ON GMH.Dia_Semana = DS.Dia_Semana " +
                            "LEFT JOIN Grupo_Materia_Horario_Clase AS GMHC ON GMH.ID_Grupo = GMHC.ID_Grupo AND GMH.ID_Materia = GMHC.ID_Materia " +
                            "AND GMH.ID_Horario = GMHC.ID_Horario AND GMH.Turno = GMHC.Turno AND GMH.Dia_Semana = GMHC.Dia_Semana LEFT JOIN Clase AS C " +
                            "ON GMHC.ID_Clase = C.ID_Clase LEFT JOIN Lugar AS L ON C.ID_Clase = L.ID WHERE UD.CI_Docente = @CI AND CURTIME() BETWEEN H.Hora_Inicio" +
                            " AND H.Hora_Fin AND DAYOFWEEK(CURDATE()) = DS.Dia_Semana;";
                        break;
                }
                cmd = new MySqlCommand(cmdstr, conn);
                cmd.Parameters.AddWithValue("@CI", ci);

                if (!string.IsNullOrEmpty(grupoSeleccionado))
                {
                    cmd.Parameters.AddWithValue("@GrupoSeleccionado", grupoSeleccionado);
                }

                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    ubicacion.Salon = dr["Salón"].ToString();
                    ubicacion.CoordenadaX = Convert.ToInt32(dr["Coordenada_X"]);
                    ubicacion.CoordenadaY = Convert.ToInt32(dr["Coordenada_Y"]);
                    ubicacion.Piso = Convert.ToInt32(dr["Piso"]);
                    ubicacion.Grupo = dr["Grupo"].ToString();
                    ubicacion.Materia = dr["Materia"].ToString();
                }
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

            return ubicacion;
        }

        public List<string> ObtenerGruposDelAlumno(int ciAlumno)
        {
            MySqlConnection conn = Conector.crearInstancia().crearConexion();
            MySqlCommand cmd = new MySqlCommand("SELECT Grupos FROM usuario_alumno WHERE CI_Alumno = @CI_Alumno", conn);
            cmd.Parameters.AddWithValue("@CI_Alumno", ciAlumno);

            List<string> gruposDelAlumno = new List<string>();

            try
            {
                conn.Open();
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        string gruposConcatenados = dr["Grupos"].ToString();
                        string[] grupos = gruposConcatenados.Split(new[] { ", " }, StringSplitOptions.None);
                        gruposDelAlumno.AddRange(grupos);
                    }
                }
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

            return gruposDelAlumno;
        }
        #endregion
    }

}

