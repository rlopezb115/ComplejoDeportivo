namespace servicio.api.common.respuesta
{
    using System.Collections.Generic;
    using System.Linq;

    public class RespuestaTablaDatos<T>
    {
        public RespuestaTablaDatos()
        {
            data = new List<T>();
        }

        public bool HasItem
        {
            get
            {
                return data != null && data.Any();
            }
        }

        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<T> data { get; set; }
    }
}