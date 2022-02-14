using CargaDeMedicamentosAPI.Constants;
using CargaDeMedicamentosAPI.Entities;
using CargaDeMedicamentosAPI.Models;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CargaDeMedicamentosAPI.Services
{
    public class MedicamentosService
    {
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
    }
}
