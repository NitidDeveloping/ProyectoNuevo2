using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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

        public RetornoValidacion Agregar(TipoReferencia referencia, object item, string id)
        {
            Datos datos = new Datos();
            if (datos.Consultar(referencia, id) != null)
            {
                return RetornoValidacion.YaExiste;
            }
            else
            {
                return datos.Agregar(referencia, item);

            }
        }

        public RetornoValidacion Editar(TipoReferencia referencia, object item, string idObjetivo)
        {
            Datos datos = new Datos();
            if (datos.Consultar(referencia, idObjetivo) != null)
            {
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
            if (datos.Consultar(referencia, idObjetivo) != null)
            {
                return datos.Eliminar(referencia, idObjetivo);

            }
            else
            {
                return RetornoValidacion.NoExiste;
            }
        }

        #endregion



    }
}
