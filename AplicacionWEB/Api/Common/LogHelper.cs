namespace Api.Common
{
    public static class LogHelper
    {
        public static void LogCustomError(this ILogger logger, string method, Exception ex)
        {
            logger.LogError($"Ocurrio un error en el metodo {method}: {ex.Message ?? ex.InnerException?.ToString()}");
        }
    }
}
