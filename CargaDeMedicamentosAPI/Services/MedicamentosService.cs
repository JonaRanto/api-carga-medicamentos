using CargaDeMedicamentosAPI.Constants;
using CargaDeMedicamentosAPI.Entities;
using CargaDeMedicamentosAPI.Models;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CargaDeMedicamentosAPI.Services
{
    public class MedicamentosService
    {
        /// <summary>
        /// Actualización de precio farmacia individual
        /// </summary>
        /// <param name="cargaIndividual"></param>
        /// <param name="precioFarmacia"></param>
        /// <returns></returns>
        public static ServiceOutput UpdatePrecioFarmacia(CargaIndividualInput cargaIndividual, EprecioFarmacium precioFarmacia)
        {
            ServiceOutput serviceOutput = new();

            decimal inputPrecio = cargaIndividual.Precio;
            decimal actualPrecio = precioFarmacia.Precio;
            int inputStock = cargaIndividual.Stock;
            int actualStock = precioFarmacia.Stock;
            int minStock = precioFarmacia.StockMin;
            int maxStock = precioFarmacia.StockMax;

            bool changesFlag = false;
            if (inputPrecio != actualPrecio)
            {
                precioFarmacia.Precio = inputPrecio;
                precioFarmacia.FechaActualizacion = DateTime.Now;
                changesFlag = true;
            }

            if (inputStock != actualStock)
            {
                if (inputStock <= minStock)
                {
                    serviceOutput.Message = UpdatePrecioFarmaciaMessages.MIN_STOCK_EXCEEDED;
                    return serviceOutput;
                }
                else if (inputStock >= maxStock)
                {
                    serviceOutput.Message = UpdatePrecioFarmaciaMessages.MAX_STOCK_EXCEEDED;
                    return serviceOutput;
                }
                else
                {
                    precioFarmacia.Stock = inputStock;
                    precioFarmacia.FechaActualizacion = DateTime.Now;
                    precioFarmacia.FechaActualizacionStock = DateTime.Now;
                    changesFlag = true;
                }
            }

            // CONFIGURAR JSONSERIALIZER PARA EVITAR ERRORES DE CICLICIDAD.
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = false
            };

            if (changesFlag)
            {
                serviceOutput.Error = false;
                serviceOutput.Message = UpdatePrecioFarmaciaMessages.UPDATED_SUCCESSFUL;
                serviceOutput.Data = JsonSerializer.Serialize(precioFarmacia, options);
            }
            else
            {
                serviceOutput.Message = UpdatePrecioFarmaciaMessages.NO_CHANGES;
            }
            return serviceOutput;
        }

        /// <summary>
        /// Leer el archivo csv
        /// </summary>
        /// <param name="cargaMasiva"></param>
        /// <returns></returns>
        public static ServiceOutput ReadCSVFile(CargaMasivaInput cargaMasiva)
        {
            ServiceOutput serviceOutput = new();

            if (ValidateCSV(cargaMasiva))
            {
                Stream stream = cargaMasiva.File.OpenReadStream();
                List<DTOPrecioFarmacia> dtoPreciosFarmacias = new();
                using var reader = new StreamReader(stream);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                while (csv.Read())
                {
                    csv.GetField(1);
                    DTOPrecioFarmacia precioFarmacia = new();
                    precioFarmacia.CodigoTFC = csv.GetField(0);
                    precioFarmacia.CodigoInterno = csv.GetField(1);
                    precioFarmacia.NuevoPrecio = csv.GetField(2);
                    precioFarmacia.StockActual = csv.GetField(3);
                    precioFarmacia.CodigoBarra = csv.GetField(4);
                    precioFarmacia.DescripcionInterna = csv.GetField(5);
                    dtoPreciosFarmacias.Add(precioFarmacia);
                }

                serviceOutput.Error = false;
                serviceOutput.Message = ReadCSVFileMessages.READ_SUCCESSFUL;
                serviceOutput.Data = JsonSerializer.Serialize(dtoPreciosFarmacias);
            }
            else
            {
                serviceOutput.Message = ReadCSVFileMessages.FORMAT_ERROR;
            }
            return serviceOutput;
        }

        /// <summary>
        /// Validar si el archivo tiene el formato csv
        /// </summary>
        /// <param name="cargaMasiva"></param>
        /// <returns></returns>
        public static bool ValidateCSV(CargaMasivaInput cargaMasiva)
        {
            bool response = false;
            if (cargaMasiva.File != null && (Path.GetExtension(cargaMasiva.File.FileName) == ConfigControllers.CSV_FILE_EXTENSION))
            {
                response = true;
            }
            return response;
        }
    }
}
