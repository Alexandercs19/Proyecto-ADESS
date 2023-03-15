using ProyectoADESS.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.SignalR;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using NuGet.Protocol;

namespace ProyectoADESS.SQL
{
    public class Contacto
    {
        public List<ClassAdd> Listar()
        {

            var olista = new List<ClassAdd>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Listar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        olista.Add(new ClassAdd()
                        {
                            Cedula_add = dr["Cedula_add"].ToString(),
                            Apellido = dr["Apellido"].ToString(),
                            Nombre = dr["Nombre"].ToString(),
                            Sub = dr["Sub"].ToString(),
                            Monto = dr["Monto"].ToString(),
                            Fecha_add = dr["Fecha_add"].ToString(),
                            Id_add = Convert.ToInt32(dr["Id_add"])
                        });
                    }
                }
            }
            return (olista);
        }

        public ClassAdd ObtenerCedula(string cedula)
        {
            var oCedula = new ClassAdd();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ObtenerPorCedula", conexion);
                cmd.Parameters.AddWithValue("cedula_add", cedula);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oCedula.Cedula_add = dr["cedula_add"].ToString();
                        oCedula.Apellido = dr["apellido"].ToString();
                        oCedula.Nombre = dr["nombre"].ToString();
                        oCedula.Sub = dr["sub"].ToString();
                        oCedula.Fecha_add = dr["fecha_add"].ToString();
                        oCedula.Monto = dr["monto"].ToString();
                        oCedula.Id_add =(int)dr["id_add"];
                    }
                }
            }
            return oCedula;

        }

        public bool EditarFecha(ClassAdd ocontacto)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EditarFecha", conexion);
                    cmd.Parameters.AddWithValue("fecha_add", ocontacto.Fecha_add);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                rpta = false;
            }

            return rpta;
        }

        public JsonResult Paginar()
        {
            var cn = new Conexion();
            List<ClassAdd> lista = new List<ClassAdd>();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                var cmd = new SqlCommand("sp_PaginarIncluidos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        lista.Add(new ClassAdd
                        {
                            Cedula_add = dr["Cedula_add"].ToString(),
                            Apellido = dr["Apellido"].ToString(),
                            Nombre = dr["Nombre"].ToString(),
                            Sub = dr["Sub"].ToString(),
                            Monto = dr["Monto"].ToString(),
                            Fecha_add = dr["Fecha_add"].ToString(),
                            Id_add = Convert.ToInt32(dr["Id_add"])
                        });
                }
            }
            return Json(new { data = lista });
        }

        private JsonResult Json(object value)
        {
            throw new NotImplementedException();
        }

        public ClassAdd Obtener(int idApp)
        {

            var oContacto = new ClassAdd();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener", conexion);
                cmd.Parameters.AddWithValue("id_add", idApp);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oContacto.Cedula_add = dr["cedula_add"].ToString();
                        oContacto.Apellido = dr["Apellido"].ToString();
                        oContacto.Nombre = dr["Nombre"].ToString();
                        oContacto.Sub = dr["Sub"].ToString();
                        oContacto.Monto = dr["Monto"].ToString();
                        oContacto.Fecha_add = dr["Fecha_add"].ToString();
                        oContacto.Id_add = Convert.ToInt32(dr["id_add"]);
                    }
                }
            }
            return oContacto;
        }

        public bool Guardar(ClassAdd ocontacto)
        {
            bool rpta;

            var RegistroObtenido = ObtenerCedula(ocontacto.Cedula_add);
            if(RegistroObtenido.Cedula_add != null) return false;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Guardar", conexion);
                    cmd.Parameters.AddWithValue("cedula_add", ocontacto.Cedula_add);
                    cmd.Parameters.AddWithValue("apellido", ocontacto.Apellido);
                    cmd.Parameters.AddWithValue("nombre", ocontacto.Nombre);
                    cmd.Parameters.AddWithValue("sub", ocontacto.Sub);
                    cmd.Parameters.AddWithValue("monto", ocontacto.Monto);
                    cmd.Parameters.AddWithValue("fecha_add", ocontacto.Fecha_add);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                rpta = false;
            }


            return rpta;
        }


        public bool Editar(ClassAdd ocontacto)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Editar", conexion);
                    cmd.Parameters.AddWithValue("id_add", ocontacto.Id_add);
                    cmd.Parameters.AddWithValue("cedula_add", ocontacto.Cedula_add);
                    cmd.Parameters.AddWithValue("apellido", ocontacto.Apellido);
                    cmd.Parameters.AddWithValue("nombre", ocontacto.Nombre);
                    cmd.Parameters.AddWithValue("sub", ocontacto.Sub);
                    cmd.Parameters.AddWithValue("monto", ocontacto.Monto);
                    cmd.Parameters.AddWithValue("fecha_add", ocontacto.Fecha_add);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                rpta = false;
            }

            return rpta;
        }
        public bool Eliminar(int Id_add)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Eliminar", conexion);
                    cmd.Parameters.AddWithValue("id_add", Id_add);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                rpta = false;
            }
            return rpta;
        }

        public bool Limpiar()
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Limpiar", conexion);
                    //cmd.Parameters.AddWithValue("id_add", Id_add);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                rpta = false;
            }
            return rpta;
        }

        internal object ContactoLista()
        {
            throw new NotImplementedException();
        }

        public bool Guardarsubidos(ClassAdd registro)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Guardar", conexion);
                    cmd.Parameters.AddWithValue("cedula_add", registro.Cedula_add);
                    cmd.Parameters.AddWithValue("apellido", registro.Apellido);
                    cmd.Parameters.AddWithValue("nombre", registro.Nombre);
                    cmd.Parameters.AddWithValue("sub", registro.Sub);
                    cmd.Parameters.AddWithValue("monto", registro.Monto);
                    cmd.Parameters.AddWithValue("fecha_add", registro.Fecha_add);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                rpta = false;
            }


            return rpta;
        }

    }
}
