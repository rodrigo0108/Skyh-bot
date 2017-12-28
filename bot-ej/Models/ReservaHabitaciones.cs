using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bot_ej.Models
{
    public enum TamanoCamaOpciones
    {
        Rey,
        Reina,
        Simple,
        Doble
    }
    public enum ServicioOpciones
    {
        Cocina,
        ToallasAdicionales,
        AccesoGimnasio,
        Wifi
    }
    [Serializable]
    public class ReservaHabitaciones
    {
        public TamanoCamaOpciones? TamañoDeCama;
        public int? NumeroDeOcupantes;
        public DateTime? FechaDeLlegada;
        public int? NumeroDeDiasDeEstadia;
        public List<ServicioOpciones> Servicios;

        public static IForm<ReservaHabitaciones> ConstruirForma()
        {
            return new FormBuilder<ReservaHabitaciones>()
                .Message("Bienvenido al servicio de reservacion de hoteles!")
                .Build();
        }
    }
}