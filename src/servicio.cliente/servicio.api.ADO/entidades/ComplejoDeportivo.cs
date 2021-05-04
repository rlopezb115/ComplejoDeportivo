namespace servicio.api.ADO.entidades
{
    public class ComplejoDeportivo
    {
        public int ComplejoId { get; set; }
        public int SedeId { get; set; }
        public int TipoComplejoId { get; set; }
        public int JefeId { get; set; }
        public string Complejo { get; set; }
        public string Localizacion { get; set; }
        public int NoArea { get; set; }
        public bool Estado { get; set; }
    }
}