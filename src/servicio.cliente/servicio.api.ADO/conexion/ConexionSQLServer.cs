namespace servicio.api.ADO.conexion
{
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    public class ConexionSQLServer : IConexionSQLServer
    {
        private readonly string _cadenaConexion;

        public ConexionSQLServer(string cadenaConexion)
        {
            _cadenaConexion = cadenaConexion;
        }

        public async Task<SqlConnection> ObtenerConexionDbAsync()
        {
            return await Task.Run(() => {
                return new SqlConnection(_cadenaConexion);
            });
        }

        public SqlConnection ObtenerConexionDb()
        {
            return new SqlConnection(_cadenaConexion);
        }
    }
}