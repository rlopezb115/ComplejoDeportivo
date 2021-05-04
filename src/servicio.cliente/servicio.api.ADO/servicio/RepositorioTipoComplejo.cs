namespace servicio.api.ADO.servicio
{
    using ADO.conexion;
    using ADO.entidades;
    using ADO.contrato;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    public class RepositorioTipoComplejo : IRepositorioTipoComplejo
    {
        private readonly IConexionSQLServer _conexionDb;

        public RepositorioTipoComplejo(IConexionSQLServer conexionDb)
        {
            _conexionDb = conexionDb;
        }

        public async Task<List<TipoComplejo>> ObtenerListadoAsync()
        {
            var response = new List<TipoComplejo>();
            using (SqlConnection conexion = _conexionDb.ObtenerConexionDb())
            {
                using (SqlCommand consulta = new SqlCommand("spObtenerTiposComplejos", conexion))
                {
                    consulta.CommandType = CommandType.StoredProcedure;
                    await conexion.OpenAsync();

                    using (var reader = await consulta.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapEntidad(reader));
                        }
                    }
                }
            }

            return response;
        }

        private TipoComplejo MapEntidad(SqlDataReader data)
        {
            return new TipoComplejo()
            {
                TipoComplejoId = (int)data["TipoComplejoId"],
                Nombre = data["Nombre"].ToString()
            };
        }
    }
}