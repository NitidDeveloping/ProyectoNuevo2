    using MySqlConnector;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.CodeDom;
using System.Windows.Forms;

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
                    cmdstr = "SELECT ID, Nombre, Tipo, Nombre_Tipo, Coordenada_X, Coordenada_Y, Piso, AptoParaClase, UsoComun, EstadoOcupacion FROM Lugares;";
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
                            aux = new Lugar(dr.GetUInt16(0), dr.GetString(1), dr.GetInt32(4), dr.GetInt32(5), dr.GetByte(6), dr.GetBoolean(7), dr.GetBoolean(8), auxtipolugar, dr.GetBoolean(9));
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

                case TipoReferencia.Horario: //esto esta mal creo, creo q habia q hacer un metodo especial para horario (grupo_materia_horario_clase)
                    cmdstr = "DELETE from Grupo_Materia_Horario_Clase where ID_Grupo=@ID_Grupo AND ID_Materia=@ID_Materia AND ID_Horario=@ID_Horario AND Turno=@Turno AND Dia_Semana=@Dia_Semana";
                    break;

                case TipoReferencia.Anio:
                    cmdstr = "DELETE from Anio where Anio=@Anio";
                    break;

                case TipoReferencia.CargosFuncionarios:
                    cmdstr = "DELETE from Cargo where Cargo=@Cargo";
                    break;

                case TipoReferencia.Lugar:
                    cmdstr = "DELETE from Lugar where ID=@ID";
                    break;

                case TipoReferencia.Funcionario:
                    cmdstr = "DELETE from Funcionario where CI_Funcionario=@CI_Funcionario";
                    break;

                case TipoReferencia.TipoDeLugar:
                    cmdstr = "DELETE from Tipo_Lugar where Tipo=@Tipo";
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

                /*case TipoReferencia.Horario: //esto esta mal creo, creo q habia q hacer un metodo especial para horario (grupo_materia_horario_clase)
                    cmd.Parameters.Add("@ID_Grupo", MySqlDbType.VarChar).Value = Convert.ToString(idObjetivo);
                    cmd.Parameters.Add("@ID_Materia", MySqlDbType.Int16).Value = Convert.ToInt16(idObjetivo);
                    cmd.Parameters.Add("@ID_Horario", MySqlDbType.Int16).Value = Convert.ToInt16(idObjetivo);
                    cmd.Parameters.Add("@Dia_Semana", MySqlDbType.Byte).Value = Convert.ToByte(idObjetivo);
                    cmd.Parameters.Add("@Turno", MySqlDbType.Byte).Value = Convert.ToByte(idObjetivo);
                    break;*/

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

                case TipoReferencia.Horario:
                    cmdstr = "INSERT INTO Grupo_Materia_Horario_Clase (ID_Grupo, ID_Materia, ID_Horario, ID_Clase, Asignado_Temporal, Dia_Semana, Turno) VALUES (@ID_Grupo, @ID_Materia, @ID_Horario, @ID_Clase, @Asignado_Temporal, @Dia_Semana, @Turno);";
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

                case TipoReferencia.Horario: //esto esta mal creo, creo q habia q hacer un metodo especial para horario (grupo_materia_horario_clase)
                    if (item is Horario horario)
                    {
                        cmd.Parameters.Add("@ID_Grupo", MySqlDbType.VarChar).Value = horario.Grupo;
                        cmd.Parameters.Add("@ID_Materia", MySqlDbType.Int16).Value = horario.Materia;
                        cmd.Parameters.Add("@ID_Horario", MySqlDbType.Int16).Value = horario.Hora;
                        cmd.Parameters.Add("@ID_Clase", MySqlDbType.Int32).Value = horario.Salon;
                        cmd.Parameters.Add("@Asignado_Temporal", MySqlDbType.VarChar).Value = horario.SalonTemporal;
                        cmd.Parameters.Add("@Dia_Semana", MySqlDbType.Byte).Value = horario.Dia;
                        cmd.Parameters.Add("@Turno", MySqlDbType.Byte).Value = horario.Turno;
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

                case TipoReferencia.Horario: //esto esta mal creo, creo q habia q hacer un metodo especial para horario (grupo_materia_horario_clase)
                    cmdstr = "UPDATE Grupo_Materia_Horario_Clase SET Asignado_Temporal=@Asignado_Temporal WHERE ID_Grupo=@ID_Grupo AND ID_Materia=@ID_Materia AND ID_Horario=@ID_Horario AND Turno=@Turno AND Dia_Semana=@Dia_Semana;";
                    break;

                case TipoReferencia.Lugar:
                    cmdstr = "UPDATE Lugar SET Nombre=@Nombre, Tipo=@Tipo, Piso=@Piso WHERE ID=@ID;";
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

                //HORARIO (GMHC)
                //HORARIO (GMHC)
                //HORARIO (GMHC)
                //HORARIO (GMHC)

                case TipoReferencia.Lugar:
                    if (item is Lugar lugar)
                    {
                        cmd.Parameters.Add("@ID", DbType.Int32).Value = Convert.ToInt32(idObjetivo);
                        cmd.Parameters.Add("@Nombre", MySqlDbType.VarChar).Value = lugar.Nombre;
                        cmd.Parameters.Add("@Tipo", MySqlDbType.Byte).Value = lugar.Tipo.Id;
                        cmd.Parameters.Add("@Piso", MySqlDbType.Byte).Value = lugar.Piso;
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

            switch (referencia){
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

                    //HORARIO (grupo materia horario clase)

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

                default : throw new ArgumentException("No se pudo conseguir id, referencia no reconocida");
            }
            cmd = new MySqlCommand(cmdstr, conn);
            try
            {
                conn.Open();
                respuesta =  Convert.ToInt32(cmd.ExecuteScalar());
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

        public DataTable CargarLugares(TipoRol rol)
        {
            MySqlConnection conn = Conector.crearInstancia().crearConexion();
            MySqlCommand cmd;
            string cmdstr;

            switch (rol)
            {
                case TipoRol.Alumno:
                case TipoRol.Docente:
                    cmdstr = "SELECT Nombre FROM Lugares;";
                    break;
                default:
                    cmdstr = "SELECT * FROM Lugar WHERE Nombre IN ('Bedelía', 'Adscripción 1er piso', 'Adscripción 2do piso', 'Baños planta baja', 'Baños primer piso', 'Gimnasio', 'Patio', 'Auditorio', 'Hall');";
                    break;
            }

            DataTable table = new DataTable();

            try
            {
                conn.Open();
                cmd = new MySqlCommand(cmdstr, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                table.Load(reader); // Cargar los datos en el DataTable
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

            return table;
        }
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
                "\r\n FROM grupo grp" +
                "\r\n JOIN grupo_alumno ga ON grp.ID_Grupo = ga.ID_Grupo" +
                "\r\n JOIN alumno a ON ga.CI_Alumno = a.CI_Alumno" +
                "\r\n JOIN usuario u ON a.CI_Alumno = u.CI" +
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
        #endregion
    }



    // Link a un chat que me tiro el sabio despues de que le pedi una revision
    //https://chat.openai.com/share/c0d53224-e843-49b8-b3ec-1ff4c607c6e0

}

