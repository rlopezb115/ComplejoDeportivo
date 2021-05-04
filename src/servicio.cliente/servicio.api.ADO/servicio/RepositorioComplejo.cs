namespace servicio.api.ADO.servicio
{
    using ADO.conexion;
    using ADO.entidades;
    using common.respuesta;
    using common.solicitud;
    using ADO.contrato;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    public class RepositorioComplejo : IRepositorioComplejo
    {
        private readonly IConexionSQLServer _conexionDb;

        public RepositorioComplejo(IConexionSQLServer conexionDb)
        {
            _conexionDb = conexionDb;
        }

        public async Task<ComplejoDeportivoCompleto> ObtenerAsync(int complejoId)
        {
            ComplejoDeportivoCompleto response = null;

            using (SqlConnection conexion = _conexionDb.ObtenerConexionDb())
            {
                using (SqlCommand consulta = new SqlCommand("spObtenerComplejo", conexion))
                {
                    consulta.CommandType = CommandType.StoredProcedure;
                    consulta.Parameters.Add(new SqlParameter("@ComplejoId", complejoId));
                    await conexion.OpenAsync();

                    using (var reader = await consulta.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = await MapEntidadAsync(reader);
                        }
                    }
                }
            }

            return response;
        }

        public async Task<RespuestaTablaDatos<ComplejoDeportivoCompleto>> ObtenerTablaDatosAsync(SolicitudTablaDatos param)
        {
            var response = new RespuestaTablaDatos<ComplejoDeportivoCompleto>();
            using (SqlConnection conexion = _conexionDb.ObtenerConexionDb())
            {
                using (SqlCommand consulta = new SqlCommand("spObtenerComplejos", conexion))
                {
                    consulta.CommandType = CommandType.StoredProcedure;
                    consulta.Parameters.Add(new SqlParameter("@Iniciar", param.Iniciar));
                    consulta.Parameters.Add(new SqlParameter("@Tamano", param.Tamano));
                    consulta.Parameters.Add(new SqlParameter("@Buscar", param.Buscar));
                    consulta.Parameters.Add(new SqlParameter("@TotalRegistros", SqlDbType.Int)).Direction = ParameterDirection.Output;
                    consulta.Parameters.Add(new SqlParameter("@TotalFiltrados", SqlDbType.Int)).Direction = ParameterDirection.Output;
                    consulta.Parameters.Add(new SqlParameter("@Paginas", SqlDbType.Int)).Direction = ParameterDirection.Output;
                    await conexion.OpenAsync();

                    using (var reader = await consulta.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var complejo = await MapEntidadAsync(reader);
                            response.data.Add(complejo);
                        }
                    }

                    response.recordsTotal = int.Parse(consulta.Parameters["@TotalRegistros"].Value.ToString());
                    response.recordsFiltered = int.Parse(consulta.Parameters["@TotalFiltrados"].Value.ToString());
                }
            }

            return response;
        }

        public async Task<ComplejoDeportivo> InsertarAsync(ComplejoDeportivo param)
        {
            using (SqlConnection conexion = _conexionDb.ObtenerConexionDb())
            {
                using (SqlCommand consulta = new SqlCommand("spInsertarComplejo", conexion))
                {
                    consulta.CommandType = CommandType.StoredProcedure;
                    consulta.Parameters.Add(new SqlParameter("@SedeId", param.SedeId));
                    consulta.Parameters.Add(new SqlParameter("@TipoComplejoId", param.TipoComplejoId));
                    consulta.Parameters.Add(new SqlParameter("@JefeId", param.JefeId));
                    consulta.Parameters.Add(new SqlParameter("@Complejo", param.Complejo));
                    consulta.Parameters.Add(new SqlParameter("@Localizacion", param.Localizacion));
                    consulta.Parameters.Add(new SqlParameter("@NoArea", param.NoArea));
                    consulta.Parameters.Add(new SqlParameter("@Estado", param.Estado));
                    consulta.Parameters.Add(new SqlParameter("@ComplejoId", SqlDbType.Int)).Direction = ParameterDirection.Output;
                    await conexion.OpenAsync();
                    await consulta.ExecuteNonQueryAsync();

                    param.ComplejoId = int.Parse(consulta.Parameters["@ComplejoId"].Value.ToString());
                }
            }

            return param;
        }

        public async Task<bool> ActualizarAsync(int complejoId, ComplejoDeportivo param)
        {
            bool actualizado = false;
            using (SqlConnection conexion = _conexionDb.ObtenerConexionDb())
            {
                using (SqlCommand consulta = new SqlCommand("spActualizarComplejo", conexion))
                {
                    consulta.CommandType = CommandType.StoredProcedure;
                    consulta.Parameters.Add(new SqlParameter("@SedeId", param.SedeId));
                    consulta.Parameters.Add(new SqlParameter("@TipoComplejoId", param.TipoComplejoId));
                    consulta.Parameters.Add(new SqlParameter("@JefeId", param.JefeId));
                    consulta.Parameters.Add(new SqlParameter("@Complejo", param.Complejo));
                    consulta.Parameters.Add(new SqlParameter("@Localizacion", param.Localizacion));
                    consulta.Parameters.Add(new SqlParameter("@NoArea", param.NoArea));
                    consulta.Parameters.Add(new SqlParameter("@Estado", param.Estado));
                    consulta.Parameters.Add(new SqlParameter("@ComplejoId", complejoId));
                    consulta.Parameters.Add(new SqlParameter("@Actualizado", SqlDbType.Bit)).Direction = ParameterDirection.Output;
                    await conexion.OpenAsync();
                    await consulta.ExecuteNonQueryAsync();

                    actualizado = bool.Parse(consulta.Parameters["@Actualizado"].Value.ToString());
                }
            }

            return actualizado;
        }

        public async Task<bool> EliminarAsync(int complejoId)
        {
            bool eliminado = false;
            using (SqlConnection conexion = _conexionDb.ObtenerConexionDb())
            {
                using (SqlCommand consulta = new SqlCommand("spEliminarComplejo", conexion))
                {
                    consulta.CommandType = CommandType.StoredProcedure;
                    consulta.Parameters.Add(new SqlParameter("@ComplejoId", complejoId));
                    consulta.Parameters.Add(new SqlParameter("@Eliminado", SqlDbType.Bit)).Direction = ParameterDirection.Output;
                    await conexion.OpenAsync();
                    await consulta.ExecuteNonQueryAsync();

                    eliminado = bool.Parse(consulta.Parameters["@Eliminado"].Value.ToString());
                }
            }

            return eliminado;
        }

        private async Task<ComplejoDeportivoCompleto> MapEntidadAsync(SqlDataReader data)
        {
            return await Task.Run(() =>
            {
                return new ComplejoDeportivoCompleto()
                {
                    ComplejoId = (int)data["ComplejoId"],
                    SedeId = (int)data["SedeId"],
                    TipoComplejoId = (int)data["TipoComplejoId"],
                    JefeId = (int)data["JefeId"],
                    Complejo = data["Complejo"].ToString(),
                    Localizacion = data["Localizacion"].ToString(),
                    NoArea = (int)data["NoArea"],
                    Estado = (bool)data["Estado"],
                    NombreJefe = data["NombreJefe"].ToString(),
                    NombreSede = data["NombreSede"].ToString(),
                    NombreTipoComplejo = data["NombreTipoComplejo"].ToString()
                };
            });
        }
    }
}