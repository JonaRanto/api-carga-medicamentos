﻿using System;
using System.Collections.Generic;
using System.Linq;
using CargaDeMedicamentosAPI.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CargaDeMedicamentosAPI.Constrollers
{
    [ApiController]
    [Route(InternalRoutes.WF)]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            Logger = logger;
        }

        private ILogger<WeatherForecastController> Logger { get; }

        /// <summary>
        /// Consulta de pruebas.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            try
            {
                var rng = new Random();
                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return null;
            }
        }
    }
}
