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
        //[Template(TemplateUsage.EnumSelectOne, "¿Que tipo de {&} le conevendría? {||}", ChoiceStyle = ChoiceStyleOptions.PerLine)]
        public TamanoCamaOpciones? Tamañodecama;
        public int? NúmeroDeOcupantes;
        public DateTime? FechaDeLlegada;
        public int? NúmeroDeDíasDeEstadía;
        public List<ServicioOpciones> Servicios;
        public static IForm<ReservaHabitaciones> ConstruirForma()
        {
            return new FormBuilder<ReservaHabitaciones>()
                .Message("Bienvenido al servicio de reservacion de hoteles!")
                .Build();
        }
    }
}