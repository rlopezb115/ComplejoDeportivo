namespace servicio.api.common.extension
{
    public static class Validaciones
    {
        public static bool EsValido<T>(this T value) where T : class => value != null;
        public static bool EsValido(this int value) => value > 0;
    }
}