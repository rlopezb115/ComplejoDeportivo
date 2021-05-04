namespace servicio.api.common.solicitud
{
    public class SolicitudTablaDatos
    {
        public SolicitudTablaDatos()
        {
            Iniciar = 1;
            Tamano = 10;
            Buscar = string.Empty;
        }

        public int Iniciar { get; set; }
        public int Tamano { get; set; }
        public string Buscar { get; set; }
    }
}