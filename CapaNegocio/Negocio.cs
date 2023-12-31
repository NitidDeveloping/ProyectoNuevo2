﻿using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;

namespace CapaNegocio
{
    public class Negocio
    {
        //Metodos y propiedades para operaciones basicas del menu de gestiones
        #region


        public DataTable Listar(TipoReferencia referencia, string columna, object valor)
        {
            Datos datos = new Datos();
            List<object> lista = datos.Listar(referencia, columna, valor);
            DataTable dt = new DataTable();

            //Segun la referencia arma los datatable y los rellena
            switch (referencia)
            {
                case TipoReferencia.Alumno:
                    dt.Columns.Add("CI", typeof(int));
                    dt.Columns.Add("Nombre", typeof(string));
                    dt.Columns.Add("Apellido", typeof(string));
                    dt.Columns.Add("Grupo", typeof(string));

                    foreach (object objeto in lista)
                    {
                        if (objeto is Alumno alumno)
                        {
                            dt.Rows.Add(alumno.CI, alumno.Nombre, alumno.Apellido, alumno.Strgrupos);
                        }

                    }
                    break;

                case TipoReferencia.Anio:
                    dt.Columns.Add("Anio", typeof(int));

                    foreach (object objeto in lista)
                    {
                        if (objeto is int anio)
                        {
                            dt.Rows.Add(anio);
                        }
                    }
                    break;

                case TipoReferencia.CargosFuncionarios:
                    dt.Columns.Add("Id_Cargo", typeof(byte));
                    dt.Columns.Add("Cargo", typeof(string));

                    foreach (object objeto in lista)
                    {
                        if (objeto is Cargo cargo)
                        {
                            dt.Rows.Add(cargo.Id, cargo.Nombre);
                        }
                    }
                    break;

                case TipoReferencia.Docente:
                    dt.Columns.Add("CI", typeof(int));
                    dt.Columns.Add("Nombre", typeof(string));
                    dt.Columns.Add("Apellido", typeof(string));

                    foreach (object objeto in lista)
                    {
                        if (objeto is Docente docente)
                        {
                            dt.Rows.Add(docente.CI, docente.Nombre, docente.Apellido);
                        }

                    }
                    break;

                case TipoReferencia.DiaSemana:
                    dt.Columns.Add("IdDiaSemana", typeof(byte));
                    dt.Columns.Add("Dia", typeof(string));

                    foreach (object objeto in lista)
                    {
                        if (objeto is Dia_Semana dia)
                        {
                            dt.Rows.Add(dia.Id, dia.Nombre);
                        }

                    }
                    break;

                case TipoReferencia.Funcionario:
                    dt.Columns.Add("CI", typeof(int));
                    dt.Columns.Add("Nombre", typeof(string));
                    dt.Columns.Add("Apellido", typeof(string));
                    dt.Columns.Add("ID_Cargo", typeof(byte));
                    dt.Columns.Add("Cargo", typeof(string));
                    dt.Columns.Add("Administrador", typeof(bool));
                    dt.Columns.Add("Fecha de ingreso", typeof(DateTime));


                    foreach (object objeto in lista)
                    {
                        if (objeto is Funcionario funcionario)
                        {
                            dt.Rows.Add(funcionario.CI, funcionario.Nombre, funcionario.Apellido, funcionario.Cargo.Id, funcionario.Cargo.Nombre, funcionario.IsAdmn, funcionario.FechaIngreso);
                        }

                    }
                    break;

                case TipoReferencia.Grupo:
                    dt.Columns.Add("Nombre", typeof(string));
                    dt.Columns.Add("ID_Turno", typeof(byte));
                    dt.Columns.Add("Turno", typeof(string));
                    dt.Columns.Add("ID_Orientacion", typeof(byte));
                    dt.Columns.Add("Orientación", typeof(string));
                    dt.Columns.Add("Año", typeof(int));
                    dt.Columns.Add("Lista", typeof(byte));

                    foreach (object objeto in lista)
                    {
                        if (objeto is Grupo grupo)
                        {
                            dt.Rows.Add(
                                grupo.Nombre,
                                grupo.Turno.Id,
                                grupo.Turno.Nombre,
                                grupo.Orientacion.Id,
                                grupo.Orientacion.Nombre,
                                grupo.Anio,
                                grupo.Lista);
                        }

                    }


                    break;

                case TipoReferencia.Hora:
                    dt.Columns.Add("ID_Turno", typeof(byte));
                    dt.Columns.Add("Turno", typeof(string));
                    dt.Columns.Add("Numero", typeof(byte));
                    dt.Columns.Add("Inicio", typeof(TimeSpan));
                    dt.Columns.Add("Fin", typeof(TimeSpan));

                    foreach (object objeto in lista)
                    {
                        if (objeto is Hora hora)
                        {
                            dt.Rows.Add(
                                hora.Turno.Id,
                                hora.Turno.Nombre,
                                hora.Nid,
                                hora.Inicio,
                                hora.Fin);
                        }

                    }

                    break;

                case TipoReferencia.Horario:
                    //Turno
                    dt.Columns.Add("IdTurno", typeof(byte));
                    dt.Columns.Add("Turno", typeof(string));
                    //Grupo
                    dt.Columns.Add("Grupo", typeof(string));
                    //Materia
                    dt.Columns.Add("IdMateria", typeof(ushort));
                    dt.Columns.Add("Materia", typeof(string));
                    //Docente
                    dt.Columns.Add("CI Docente", typeof(int));
                    dt.Columns.Add("Nombre Docente", typeof(string));
                    dt.Columns.Add("Apellido Docente", typeof(string));
                    //Dia Semana
                    dt.Columns.Add("IdDiaSemana", typeof(byte));
                    dt.Columns.Add("Dia", typeof(string));
                    //Horas
                    dt.Columns.Add("Horas", typeof(string));
                    //Inicio y fin
                    dt.Columns.Add("Inicio", typeof(TimeSpan));
                    dt.Columns.Add("Fin", typeof(TimeSpan));
                    //Salon predeterminado
                    dt.Columns.Add("ID_SalonP", typeof(ushort));
                    dt.Columns.Add("Salon", typeof(string));
                    //Asignado temporal
                    dt.Columns.Add("ID_SalonT", typeof(ushort));
                    dt.Columns.Add("Salon temporal", typeof(string));

                    foreach (object objeto in lista)
                    {
                        if (objeto is Horario horario)
                        {
                            Docente horarioDocente = new Docente(null, null, 0);
                            Lugar horarioSalon = new Lugar(0, null);
                            Lugar horarioAsignadoTemporal = new Lugar(0, null);


                            if (horario.Docente != null)
                            {
                                horarioDocente = horario.Docente;
                            }
                            if (horario.Salon != null)
                            {
                                horarioSalon = horario.Salon;
                            }
                            if (horario.SalonTemporal != null)
                            {
                                horarioAsignadoTemporal = horario.SalonTemporal;
                            }


                            dt.Rows.Add(
                                horario.Turno.Id,
                                horario.Turno.Nombre,
                                horario.Grupo,
                                horario.Materia.Id,
                                horario.Materia.Nombre,
                                horarioDocente.CI,
                                horarioDocente.Nombre,
                                horarioDocente.Apellido,
                                horario.Dia.Id,
                                horario.Dia.Nombre,
                                horario.StrHoras, //Muestro el string con las horas que abarca
                                horario.Inicio,
                                horario.Fin,
                                horarioSalon.ID,
                                horarioSalon.Nombre,
                                horarioAsignadoTemporal.ID,
                                horarioAsignadoTemporal.Nombre
                                );
                        }

                    }

                    break;

                case TipoReferencia.Clases:
                case TipoReferencia.Lugar:
                    dt.Columns.Add("ID_Lugar", typeof(ushort));
                    dt.Columns.Add("Nombre", typeof(string));
                    dt.Columns.Add("ID_Tipo", typeof(byte));
                    dt.Columns.Add("Tipo", typeof(string));
                    dt.Columns.Add("Piso", typeof(byte));
                    dt.Columns.Add("Coordenada_X", typeof(int));
                    dt.Columns.Add("Coordenada_Y", typeof(int));
                    dt.Columns.Add("Ubicación", typeof(string));
                    dt.Columns.Add("Clase", typeof(bool));
                    dt.Columns.Add("Uso común", typeof(bool));
                    dt.Columns.Add("Ocupado", typeof(bool));

                    foreach (object objeto in lista)
                    {
                        if (objeto is Lugar lugar)
                        {
                            dt.Rows.Add(
                                lugar.ID,
                                lugar.Nombre,
                                lugar.Tipo.Id,
                                lugar.Tipo.Nombre,
                                lugar.Piso,
                                lugar.Coordenada_x,
                                lugar.Coordenada_y,
                                "" + lugar.Coordenada_x + " , " + lugar.Coordenada_y + "",
                                lugar.IsClase,
                                lugar.IsUsoComun,
                                lugar.Ocupado);
                        }

                    }

                    break;

                case TipoReferencia.Materia:
                    dt.Columns.Add("Id", typeof(ushort));
                    dt.Columns.Add("Nombre", typeof(string));

                    foreach (object objeto in lista)
                    {
                        if (objeto is Materia materia)
                        {
                            dt.Rows.Add(
                                materia.Id,
                                materia.Nombre);
                        }

                    }

                    break;
                case TipoReferencia.Orientacion:
                    dt.Columns.Add("Id", typeof(byte));
                    dt.Columns.Add("Nombre", typeof(string));

                    foreach (object objeto in lista)
                    {
                        if (objeto is Orientacion orientacion)
                        {
                            dt.Rows.Add(
                                orientacion.Id,
                                orientacion.Nombre);
                        }

                    }

                    break;
                case TipoReferencia.TipoDeLugar:
                    dt.Columns.Add("Id", typeof(byte));
                    dt.Columns.Add("Nombre", typeof(string));

                    foreach (object objeto in lista)
                    {
                        if (objeto is TipoLugar tipolugar)
                        {
                            dt.Rows.Add(
                                tipolugar.Id,
                                tipolugar.Nombre);
                        }

                    }

                    break;

                case TipoReferencia.Turno:
                    dt.Columns.Add("Id", typeof(byte));
                    dt.Columns.Add("Nombre", typeof(string));

                    foreach (object objeto in lista)
                    {
                        if (objeto is Turno turno)
                        {
                            dt.Rows.Add(
                                turno.Id,
                                turno.Nombre);
                        }

                    }

                    break;

                default:
                    throw new Exception("No se ha implementado aun, listar capa de negocios");
            }
            return dt;
        }

        public RetornoValidacion Agregar(TipoReferencia referencia, object item, string idnombre)
        {
            Datos datos = new Datos();

            if (referencia == TipoReferencia.Lugar ||
                referencia == TipoReferencia.Materia ||
                referencia == TipoReferencia.Orientacion ||
                referencia == TipoReferencia.Turno)
            {
                if (datos.VerificarNombreNuevo(referencia, idnombre)) //Verifica que no sea un nombre repetido
                {
                    //Agrega el id al item segun la referencia
                    switch (referencia)
                    {
                        case TipoReferencia.Lugar:
                            if (item is Lugar lugar)
                            {
                                ushort id = (ushort)datos.GenerarIdAutomatico(referencia);
                                item = new Lugar(
                                    id,
                                    lugar.Nombre,
                                    lugar.Tipo,
                                    lugar.Piso,
                                    lugar.Coordenada_x,
                                    lugar.Coordenada_y,
                                    lugar.IsClase,
                                    lugar.IsUsoComun);
                            }

                            break;

                        case TipoReferencia.Materia:
                            if (item is Materia materia)
                            {
                                ushort id = (ushort)datos.GenerarIdAutomatico(referencia);
                                item = new Materia(
                                    id,
                                    materia.Nombre
                                   );
                            }
                            break;

                        case TipoReferencia.Orientacion:
                            if (item is Orientacion orientacion)
                            {
                                byte id = (byte)datos.GenerarIdAutomatico(referencia);
                                item = new Orientacion(
                                    id,
                                    orientacion.Nombre
                                   );
                            }
                            break;

                        case TipoReferencia.Turno:
                            if (item is Turno turno)
                            {
                                byte id = (byte)datos.GenerarIdAutomatico(referencia);
                                item = new Turno(
                                    id,
                                    turno.Nombre
                                   );
                            }
                            break;

                    }

                    if (referencia != TipoReferencia.Lugar)
                    {
                        return datos.Agregar(referencia, item);
                    }
                    else
                    {
                        if (item is Lugar lugar)
                        {
                            RetornoValidacion resultadoAgregarAClase = RetornoValidacion.OK;
                            RetornoValidacion resultadoAgregarAUsoComun = RetornoValidacion.OK;
                            RetornoValidacion resultadoAgregarLugar = RetornoValidacion.OK;

                            resultadoAgregarLugar = datos.Agregar(TipoReferencia.Lugar, item);

                            if (resultadoAgregarLugar != RetornoValidacion.OK)
                            {
                                return resultadoAgregarLugar;
                            }

                            if (lugar.IsClase)
                            {
                                resultadoAgregarAClase = datos.Agregar(TipoReferencia.Clases, item);

                                if (resultadoAgregarAClase != RetornoValidacion.OK)
                                {
                                    return resultadoAgregarAClase;
                                }
                            }
                            if (lugar.IsUsoComun)
                            {
                                resultadoAgregarAUsoComun = datos.Agregar(TipoReferencia.UsoComun, item);
                                if (resultadoAgregarAUsoComun != RetornoValidacion.OK)
                                {
                                    return resultadoAgregarAUsoComun;
                                }
                            }
                            return resultadoAgregarLugar;
                        }

                    }

                }
                else
                {
                    return RetornoValidacion.YaExisteNombre;
                }
            }

            if (datos.Consultar(referencia, idnombre) != null)
            {
                return RetornoValidacion.YaExiste;
            }
            else
            {
                return datos.Agregar(referencia, item);

            }
        }

        public RetornoValidacion Editar(TipoReferencia referencia, object item, string idObjetivo, string nombre)
        {
            Datos datos = new Datos();
            if (referencia == TipoReferencia.Lugar ||
               referencia == TipoReferencia.Materia ||
               referencia == TipoReferencia.Orientacion ||
               referencia == TipoReferencia.Turno)
            {
                if (datos.VerificarNombreNuevo(referencia, nombre, idObjetivo)) //Verifica que no sea un nombre repetido
                {
                    //Agrega el id al item segun la referencia
                    switch (referencia)
                    {
                        case TipoReferencia.Lugar:
                            if (item is Lugar lugar)
                            {
                                ushort id = (ushort)datos.GenerarIdAutomatico(referencia);
                                item = new Lugar(
                                    id,
                                    lugar.Nombre,
                                    lugar.Tipo,
                                    lugar.Piso,
                                    lugar.Coordenada_x,
                                    lugar.Coordenada_y,
                                    lugar.IsClase,
                                    lugar.IsUsoComun);
                            }
                            break;

                        case TipoReferencia.Materia:
                            if (item is Materia materia)
                            {
                                ushort id = (ushort)datos.GenerarIdAutomatico(referencia);
                                item = new Materia(
                                    id,
                                    materia.Nombre
                                   );
                            }
                            break;

                        case TipoReferencia.Orientacion:
                            if (item is Orientacion orientacion)
                            {
                                byte id = (byte)datos.GenerarIdAutomatico(referencia);
                                item = new Orientacion(
                                    id,
                                    orientacion.Nombre
                                   );
                            }
                            break;

                        case TipoReferencia.Turno:
                            if (item is Turno turno)
                            {
                                byte id = (byte)datos.GenerarIdAutomatico(referencia);
                                item = new Turno(
                                    id,
                                    turno.Nombre
                                   );
                            }
                            break;

                    }
                    return datos.Editar(referencia, item, idObjetivo);

                }
                else
                {
                    return RetornoValidacion.YaExisteNombre;
                }
            }

            if (datos.Consultar(referencia, idObjetivo) != null)
            {
                if (referencia == TipoReferencia.Docente || referencia == TipoReferencia.Alumno)
                {
                    referencia = TipoReferencia.Usuario;
                }
                return datos.Editar(referencia, item, idObjetivo);

            }
            else
            {
                return RetornoValidacion.NoExiste;
            }
        }

        public RetornoValidacion Eliminar(TipoReferencia referencia, string idObjetivo)
        {
            Datos datos = new Datos();

            if (int.Parse(idObjetivo) == 11111111 || int.Parse(idObjetivo) == 22222222 || int.Parse(idObjetivo) == 23232323 || int.Parse(idObjetivo) == 45454545)
            {
                throw new Exception("No puede eliminar a los usuarios de prueba para la muestra.");
            }

            if (int.Parse(idObjetivo) == Sesion.LoggedCi)
            {
                throw new Exception("No puede eliminarse a si mismo.");
            }

            //Si no se encuentra el objeto devuelve que no existe
            if (datos.Consultar(referencia, idObjetivo) == null)
            {
                return RetornoValidacion.NoExiste;
            }

            //Si no es alumno docente ni funcionario elimina comun
            if (referencia != TipoReferencia.Alumno && referencia != TipoReferencia.Docente && referencia != TipoReferencia.Funcionario)
            {
                return datos.Eliminar(referencia, idObjetivo);
            }

            //Si la referencia es alumno primero intenta eliminar el alumno de los grupos en los que se encuentre y despues sigue
            if (referencia == TipoReferencia.Alumno)
            {
                List<Grupo> grupos = datos.ConsultarGruposAlumno(int.Parse(idObjetivo));

                if (grupos.Count > 0)
                {
                    foreach (Grupo grupo in grupos)
                    {
                        datos.EliminarAlumnoDeGrupo(idObjetivo, grupo.Nombre);
                    }
                }
            }
            // Si es alguno de esos elimina el alumno y despues el usuario
            RetornoValidacion eliminarDeCategorizacion = datos.Eliminar(referencia, idObjetivo);

            //Si eliminarDeCategorizacion sale mal devuelve que hubo un error
            if (eliminarDeCategorizacion == RetornoValidacion.ErrorInesperadoBD)
            {
                return RetornoValidacion.ErrorInesperadoBDCategorizacion;
            }

            return datos.Eliminar(TipoReferencia.Usuario, idObjetivo);


        }

        public RetornoValidacion EliminarLugar(string idObjetivo, bool isClase, bool isUsoComun)
        {
            Datos datos = new Datos();
            RetornoValidacion resultadoEliminarClase = RetornoValidacion.OK;
            RetornoValidacion resultadoEliminarUsoComun = RetornoValidacion.OK;
            RetornoValidacion resultadoEliminarLugar = RetornoValidacion.OK;

            //Si no se encuentra el objeto devuelve que no existe
            if (datos.Consultar(TipoReferencia.Lugar, idObjetivo) == null)
            {
                return RetornoValidacion.NoExiste;
            }

            if (isClase)
            {
                resultadoEliminarClase = datos.Eliminar(TipoReferencia.Clases, idObjetivo);
            }

            if (isUsoComun)
            {
                resultadoEliminarUsoComun = datos.Eliminar(TipoReferencia.UsoComun, idObjetivo);
            }

            resultadoEliminarLugar = datos.Eliminar(TipoReferencia.Lugar, idObjetivo);

            if (resultadoEliminarClase != RetornoValidacion.OK)
            {
                return resultadoEliminarClase;
            }

            if (resultadoEliminarUsoComun != RetornoValidacion.OK)
            {
                return resultadoEliminarUsoComun;
            }

            if (resultadoEliminarLugar != RetornoValidacion.OK)
            {
                return resultadoEliminarLugar;
            }

            return resultadoEliminarLugar;
        }

        //Sobrecargas para horas
        #region
        public RetornoValidacion Agregar(TipoReferencia referencia, object item, byte idObjetivo, byte idPadre)
        {
            Datos datos = new Datos();

            if (datos.Consultar(referencia, idObjetivo, idPadre) != null)
            {
                return RetornoValidacion.YaExiste;
            }
            else
            {
                return datos.Agregar(referencia, item);

            }
        }

        public RetornoValidacion Editar(TipoReferencia referencia, object item, byte idObjetivo, byte idPadre)
        {
            Datos datos = new Datos();

            if (datos.Consultar(referencia, idObjetivo, idPadre) == null)
            {
                return RetornoValidacion.NoExiste;
            }
            else
            {
                return datos.Editar(referencia, item, idObjetivo, idPadre);

            }
        }

        public RetornoValidacion Eliminar(TipoReferencia referencia, byte idObjetivo, byte idPadre)
        {
            Datos datos = new Datos();
            // if (datos.Consultar(referencia, idObjetivo) != null)
            // {
            return datos.Eliminar(referencia, idObjetivo, idPadre);

            // }
            //  else
            // {
            //     return RetornoValidacion.NoExiste;
            // }
        }
        #endregion
        #endregion

        //Metodos para la consulta de grupos
        #region
        public DataTable ListarMateriasYDocentes(string idGrupo)
        {
            Datos datos = new Datos();
            List<(Materia, Docente)> lista = datos.ListarMateriasYDocentesDeGrupo(idGrupo);
            DataTable dt = new DataTable();

            dt.Columns.Add("ID_Materia", typeof(ushort));
            dt.Columns.Add("Materia", typeof(string));
            dt.Columns.Add("CI Docente", typeof(int));
            dt.Columns.Add("Nombre Docente", typeof(string));
            dt.Columns.Add("Apellido Docente", typeof(string));

            foreach ((Materia m, Docente d) md in lista)
            {
                if (md.d != null)
                {
                    dt.Rows.Add(md.m.Id, md.m.Nombre, md.d.CI, md.d.Nombre, md.d.Apellido);
                }
                else
                {
                    dt.Rows.Add(md.m.Id, md.m.Nombre);
                }

            }

            return dt;
        }

        public DataTable ListarAlumnosDeGrupo(string idGrupo)
        {
            Datos datos = new Datos();
            List<Alumno> lista = datos.ListarAlumnosDeGrupo(idGrupo);
            DataTable dt = new DataTable();
            byte nLista = 0;

            dt.Columns.Add("N°", typeof(byte));
            dt.Columns.Add("CI", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Apellido", typeof(string));

            foreach (Alumno alumno in lista)
            {
                nLista++;
                dt.Rows.Add(nLista, alumno.CI, alumno.Nombre, alumno.Apellido);
            }

            return dt;
        }

        public Usuario ConsultarAlumnosDocentes(TipoReferencia referencia, string ci)
        {
            Datos datos = new Datos();
            Usuario consultaCruda = (Usuario)datos.Consultar(referencia, ci);
            Usuario respuesta = null;

            if (consultaCruda != null)
            {
                respuesta = new Usuario(consultaCruda.Nombre, consultaCruda.Apellido, consultaCruda.CI);
            }
            return respuesta;

        }

        public RetornoValidacion AgregarAlumnoAGrupo(string cialumno, string idgrupo)
        {
            Datos datos = new Datos();
            RetornoValidacion respuesta = datos.AgregarAlumnoAGrupo(cialumno, idgrupo);
            return respuesta;
        }

        public bool ConsultarAlumnoEnGrupo(string cialumno, string idgrupo)
        {
            Datos datos = new Datos();
            bool respuesta = datos.ConsultarAlumnoEnGrupo(cialumno, idgrupo);
            return respuesta;

        }
        public bool ConsultarDocenteEnGrupoMateria(string ciDocente, string idGrupo, ushort idMateria)
        {
            Datos datos = new Datos();
            bool respuesta = datos.ConsultarDocenteEnGrupoMateria(ciDocente, idGrupo, idMateria);
            return respuesta;
        }
        public RetornoValidacion AgregarDocenteEnGrupoMateria(int ciDocente, string idGrupo, ushort idMateria)
        {
            Datos datos = new Datos();
            RetornoValidacion respuesta = datos.AgregarDocenteAGrupoMateria(ciDocente, idGrupo, idMateria);
            return respuesta;
        }

        public RetornoValidacion AgregarMateriaAGrupo(ushort idMateria, string idgrupo)
        {
            Datos datos = new Datos();
            if (datos.ConsultarMateriaEnGrupo(idMateria, idgrupo))
            {
                return RetornoValidacion.YaExiste;
            }
            else
            {
                return datos.AgregarMateriaAGrupo(idMateria, idgrupo);

            }
        }

        public RetornoValidacion EliminarAlumnoDeGrupo(string ciAlumno, string idGrupo)
        {
            Datos datos = new Datos();
            if (datos.ConsultarAlumnoEnGrupo(ciAlumno, idGrupo))
            {
                return datos.EliminarAlumnoDeGrupo(ciAlumno, idGrupo);

            }
            else
            {
                return RetornoValidacion.NoExiste;
            }
        }
        public RetornoValidacion EliminarDocenteDeGrupoMateria(string idMateria, string idGrupo, string ciDocente)
        {
            Datos datos = new Datos();
            if (datos.ConsultarDocenteEnGrupoMateria(ciDocente, idGrupo, ushort.Parse(idMateria)))
            {
                return datos.EliminarDocenteDeMateriaGrupo(idMateria, idGrupo, ciDocente);

            }
            else
            {
                return RetornoValidacion.NoExiste;
            }
        }
        public RetornoValidacion EliminarMateriaDeGrupo(string idMateria, string idGrupo)
        {
            Datos datos = new Datos();
            if (datos.ConsultarMateriaEnGrupo(ushort.Parse(idMateria), idGrupo))
            {
                return datos.EliminarMateriaDeGrupo(idMateria, idGrupo);

            }
            else
            {
                return RetornoValidacion.NoExiste;
            }
        }



        #endregion

        //Metodos para el login
        #region
        public RetornoValidacion IntentarLogIn(string ci, string pin)
        {
            Datos datos = new Datos();
            return datos.IntentarLogIn(ci, pin);
        }

        public RetornoValidacion ActualizarPin(string pin)
        {
            if (Sesion.LoggedCi == 11111111 || Sesion.LoggedCi == 22222222 || Sesion.LoggedCi == 23232323 || Sesion.LoggedCi == 45454545)
            {
                throw new Exception("No puede actualizar pin de los usuarios de prueba para la muestra.");
            }
            Datos datos = new Datos();
            return datos.ActualizarPin(pin);
        }
        #endregion

        //Metodos para los horarios
        #region
        public RetornoValidacion AgregarHorario(Horario horario)
        {
            Datos datos = new Datos();

            if (datos.ConsultarHorarioExiste(horario))
            {
                return RetornoValidacion.YaExiste;
            }
            else
            {
                return datos.AgregarHorario(horario);
            }
        }

        public MensajeSalonOcupado ConsultarSalonOcupado(ushort idLugar, TimeSpan inicio, TimeSpan fin, byte idDia)
        {
            Datos datos = new Datos();
            return datos.ConsultarSalonOcupado(idLugar, inicio, fin, idDia);
        }

        public RetornoValidacion AsignarSalonTemporal(Horario horarioDestino, ushort idSalon)
        {
            Datos datos = new Datos();
            return datos.AsignarSalonTemporal(horarioDestino, idSalon);
        }

        public RetornoValidacion EliminarHorario(Horario horario)
        {
            Datos datos = new Datos();
            if (!datos.ConsultarHorarioExiste(horario))
            {
                return RetornoValidacion.NoExiste;
            }

            return datos.EliminarHorario(horario);
        }

        #endregion


        public UbicacionClase ObtenerUbicacion(int CI, TipoRol rol, string Grupo)
        {
            Datos datos = new Datos();
            return datos.ObtenerUbicacion(CI, rol, Grupo);
        }

        public List<string> ObtenerGruposDelAlumno(int ciAlumno)
        {
            Datos datos = new Datos();
            return datos.ObtenerGruposDelAlumno(ciAlumno);
        }
    }
}
