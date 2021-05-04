namespace servicio.api.Model
{
    public class Enlace
    {
        public string Href { get; private set; }
        public string Rel { get; private set; }
        public string Metodo { get; private set; }
        public string Id { get; private set; }

        public Enlace(string href, string rel, string metodo, string id)
        {
            Href = href;
            Rel = rel;
            Metodo = metodo;
            Id = id;
        }
    }
}