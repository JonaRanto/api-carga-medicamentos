using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CargaDeMedicamentosAPI.Constants
{
    public class UpdatePrecioFarmaciaMessages
    {
        // SUCCESSFUL
        public const string UPDATED_SUCCESSFUL = "La actualización del precio de la farmacia se ha realizado exitosamente.";

        // ERROR
        public const string UPDATED_ERROR = "Ha ocurrido un error al intentar actualizar el precio de la farmacia.";
        public const string MAX_STOCK_EXCEEDED = "Ha ocurrido un error. El stock de medicamento no puede ser mayor al stock máximo.";
        public const string MIN_STOCK_EXCEEDED = "Ha ocurrido un error. El stock de medicamento no puede ser menor al stock mínimo.";
        public const string NO_CHANGES = "Ha ocurrido un error. No hay cambios en el precio o stock del medicamentos.";
    }

    public class ReadCSVFileMessages
    {
        // SUCCESSFUL
        public const string READ_SUCCESSFUL = "La lectura del archivo se ha realizado correctamente.";

        // ERROR
        public const string READ_ERROR = "Ha ocurrido un error al intentar leer el archivo.";
        public const string FORMAT_ERROR = "Ha ocurrido un error. No se ha podido validar el formato del archivo.";
    }

    public class CargaMasivaMessages
    {
        // SUCCESSFUL
        public const string CARGA_SUCCESSFUL = "La carga se ha realizado exitosamente.";

        // ERROR
        public const string CARGA_ERROR = "Ha ocurrido un error al intentar realizar la carga masiva.";
        public const string NOT_FOUND = "No se ha encontrado el medicamento.";
    }
}
