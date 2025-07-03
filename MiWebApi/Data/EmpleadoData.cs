using MiWebApi.Models;
using System.Data;
using System.Data.SqlClient;
namespace MiWebApi.Data
{
    public class EmpleadoData
    {
        private readonly string conexion;

        public EmpleadoData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("StringSQL")!;
            
        }
        public async Task<List<Empleado>> Lista()
        { 
            List<Empleado> lista = new List<Empleado>();
            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_listaEmpleados", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new Empleado
                        {
                            idEmpleado = Convert.ToInt32(reader["idEmpleado"]),
                            nombre = reader["nombre"].ToString(),
                            correo = reader["correo"].ToString(),
                            cargo = reader["cargo"].ToString(),
                            telefono = reader["telefono"].ToString(),
                            departamento = reader["departamento"].ToString(),
                            activo = reader["activo"] != DBNull.Value && Convert.ToBoolean(reader["Activo"]),
                            fechaContrato = Convert.ToDateTime(reader["fechaContrato"]).ToString("dd/MM/yy")
                        });


                    }
                 
                }
             }
                    return lista;




        }
        public async Task<Empleado> Obtener(int id)
        {
            Empleado objeto = new Empleado();
            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_obtenerEmpleado", con);
                cmd.Parameters.AddWithValue("@idEmpleado", id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto=new Empleado
                        {
                            idEmpleado = Convert.ToInt32(reader["idEmpleado"]),
                            nombre = reader["nombre"].ToString(),
                            correo = reader["correo"].ToString(),
                            cargo = reader["cargo"].ToString(),
                            departamento = reader["departamento"].ToString(),
                            telefono = reader["telefono"].ToString(),
                            activo = reader["activo"] != DBNull.Value && Convert.ToBoolean(reader["Activo"]),
                            fechaContrato = Convert.ToDateTime(reader["fechaContrato"]).ToString("dd/MM/yy")
                        };


                    }

                }
            }
            return objeto;




        }

        public async Task<bool> Crear(Empleado objeto)
        {
            bool respuesta=true;
            using (var con = new SqlConnection(conexion))
            {
                
                SqlCommand cmd = new SqlCommand("sp_crearEmpleado", con);
                cmd.Parameters.AddWithValue("@nombre", objeto.nombre);
                cmd.Parameters.AddWithValue("@correo", objeto.correo);
                cmd.Parameters.AddWithValue("@cargo", objeto.cargo);
                cmd.Parameters.AddWithValue("@telefono", objeto.telefono);
                cmd.Parameters.AddWithValue("@activo", objeto.activo);
                cmd.Parameters.AddWithValue("@departamento", objeto.departamento);
                cmd.Parameters.AddWithValue("@fechaContrato", objeto.fechaContrato);
                cmd.CommandType = CommandType.StoredProcedure;

                try 
                {
                    await con.OpenAsync() ;
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true:false;
                }
                catch(Exception ex)
                {
                    respuesta = false;
                    throw ex;


                }

                

                
            }
            return respuesta;




        }

        public async Task<bool> Editar(Empleado objeto)
        {
            bool respuesta = true;
            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_editarEmpleado", con);
                cmd.Parameters.AddWithValue("@idEmpleado", objeto.idEmpleado);
                cmd.Parameters.AddWithValue("@nombre", objeto.nombre);
                cmd.Parameters.AddWithValue("@correo", objeto.correo);
                cmd.Parameters.AddWithValue("@cargo", objeto.cargo);
                cmd.Parameters.AddWithValue("@telefono", objeto.telefono);
                cmd.Parameters.AddWithValue("@activo", objeto.activo);
                cmd.Parameters.AddWithValue("@departamento", objeto.departamento);
                cmd.Parameters.AddWithValue("@fechaContrato", objeto.fechaContrato);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;

                }




            }
            return respuesta;




        }

        public async Task<bool> Eliminar(int id)
        {
            bool respuesta = true;
            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_eliminarEmpleado", con);
                cmd.Parameters.AddWithValue("@idEmpleado", id);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;

                }




            }
            return respuesta;




        }

    }
}
