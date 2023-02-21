using ProyectoADESS.Models;
using System.Data.SqlClient;
using System.Data;

namespace ProyectoADESS.SQL
{
    public class ContactoExcluidos
    {

        public List<Excluidos> Listar()
        {

            var olista = new List<Excluidos>();

            var cn = new Conexion();

            using (var conexionExcluidos = new SqlConnection(cn.getCadenaSQL()))
            {
                conexionExcluidos.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarExcluidos", conexionExcluidos);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        olista.Add(new Excluidos()
                        {
                            IdExcluidos = Convert.ToInt32(dr["IdExcluidos"]),
                            Cedula = dr["Cedula"].ToString(),
                            Motivo = dr["Motivo"].ToString(),
                            Fecha = dr["Fecha"].ToString(),

                        });
                    }
                }
            }
            return olista;
        }

        public Excluidos Obtener(int idApp)
        {

            var oContacto = new Excluidos();

            var cn = new Conexion();

            using (var conexionExcluidos = new SqlConnection(cn.getCadenaSQL()))
            {
                conexionExcluidos.Open();
                SqlCommand cmd = new SqlCommand("sp_ObtenerExcluidos", conexionExcluidos);
                cmd.Parameters.AddWithValue("id", idApp);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oContacto.IdExcluidos = Convert.ToInt32(dr["IdExcluidos"]);
                        oContacto.Cedula = dr["Cedula"].ToString();
                        oContacto.Motivo = dr["Motivo"].ToString();
                        oContacto.Fecha = dr["Fecha"].ToString();
                    }
                }
            }
            return oContacto;
        }
        public bool Guardar(Excluidos oContactoExcluidos)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexionExcluidos = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexionExcluidos.Open();
                    SqlCommand cmd = new SqlCommand("sp_guardarex", conexionExcluidos);
                    cmd.Parameters.AddWithValue("IdExcluidos", oContactoExcluidos.IdExcluidos);
                    cmd.Parameters.AddWithValue("Cedula", oContactoExcluidos.Cedula);
                    cmd.Parameters.AddWithValue("Motivo", oContactoExcluidos.Motivo);
                    cmd.Parameters.AddWithValue("Fecha", oContactoExcluidos.Fecha);
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
        public bool Editar(Excluidos ocontacto)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexionExcluidos = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexionExcluidos.Open();
                    SqlCommand cmd = new SqlCommand("sp_EditarExcluidos", conexionExcluidos);
                    cmd.Parameters.AddWithValue("IdExcluidos", ocontacto.IdExcluidos);
                    cmd.Parameters.AddWithValue("Cedula", ocontacto.Cedula);
                    cmd.Parameters.AddWithValue("Motivo", ocontacto.Fecha);
                    cmd.Parameters.AddWithValue("Fecha", ocontacto.Motivo);
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
        public bool Eliminar(int IdExcluidos)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarExcluidos", conexion);
                    cmd.Parameters.AddWithValue("IdExcluidos", IdExcluidos);
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
    }
}

    
