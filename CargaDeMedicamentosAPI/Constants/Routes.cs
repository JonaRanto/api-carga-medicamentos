namespace CargaDeMedicamentosAPI.Constants
{
    public class RoutesPaths
    {
        // MAIN
        public const string MAIN = "api";

        // MEDICAMENTOS
        public const string MAIN_MEDIC = "medicamentos";
        public const string MEDIC_STATE = "estado";
        public const string MEDIC_ERROR = "error";

        // TOKEN
        public const string MAIN_TOKEN = "token";
        public const string TOKEN_GENERATE = "generar";

        // WEATHER_FORECAST
        public const string MAIN_WF = "weatherforecast";
    }

    public class InternalRoutes
    {
        public const string MEDIC = RoutesPaths.MAIN + "/" + RoutesPaths.MAIN_MEDIC;
        public const string TOKEN = RoutesPaths.MAIN + "/" + RoutesPaths.MAIN_TOKEN;
        public const string WF = RoutesPaths.MAIN + "/" + RoutesPaths.MAIN_WF;
    }
}
