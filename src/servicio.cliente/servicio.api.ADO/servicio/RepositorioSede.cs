namespace servicio.api.ADO.servicio
{
    using ADO.conexion;
    using ADO.entidades;
    using ADO.contrato;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    public class RepositorioSede : IRepositorioSede
    {
        private readonly IConexionSQLServer _conexionDb;

        public RepositorioSede(IConexionSQLServer conexionDb)
        {
            _conexionDb = conexionDb;
        }

        public async Task<List<Sede>> ObtenerListadoAsync()
        {
            var response = new List<Sede>();
            using (SqlConnection conexion = _conexionDb.ObtenerConexionDb())
            {
                using (SqlCommand consulta = new SqlCommand("spObtenerSedes", conexion))
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

        private Sede MapEntidad(SqlDataReader data)
        {
            return new Sede()
            {
                SedeId = (int)data["SedeId"],
                Nombre = data["Nombre"].ToString()
            };
        }
    }
}