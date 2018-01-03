using bot_ej.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace bot_ej.Dialogs
{
    [LuisModel("0817e33f-5419-433f-a59f-4f919d632f36", "55b706c0133e457e9f928188adeabe1e")]
    [Serializable]
    public class LUISDialog : LuisDialog<ReservaHabitaciones> 
    {
        private readonly BuildFormDelegate<ReservaHabitaciones> ReservacionCuarto;
        public LUISDialog(BuildFormDelegate<ReservaHabitaciones> reservaCuarto)
        {
            this.ReservacionCuarto = reservaCuarto;
        }

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Lo siento, no sé lo que quieres decir");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Saludo")]
        public async Task Saludo(IDialogContext context, LuisResult result)=>context.Call(new GreetingDialog(),Callback);
        

        private async Task Callback(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(MessageReceived);
        }

        [LuisIntent("Reservacion")]
        public async Task Reservacion(IDialogContext context, LuisResult result)
        {
            var formularioRegistro = new FormDialog<ReservaHabitaciones>(new ReservaHabitaciones(), this.ReservacionCuarto, FormOptions.PromptInStart);
            context.Call<ReservaHabitaciones>(formularioRegistro, Callback);
            

        }
        [LuisIntent("Consulta.Servicios")]
        public async Task ConsultaServicios(IDialogContext context, LuisResult result)
        {
            foreach (var entidad in result.Entities.Where(Entidad => Entidad.Type == "Servicio"))
            {
                var valor = entidad.Entity.ToLower();
                if(valor== "piscina" || valor == "gimnasio" ||valor=="wifi"||valor=="toallas")
                {
                    await context.PostAsync("Si lo tenemos!");
                    context.Wait(MessageReceived);
                    return;
                }
                else
                {
                    await context.PostAsync("Lo siento no tenemos " + valor);
                    context.Wait(MessageReceived);
                    return;
                }
            }

            await context.PostAsync("Lo siento no lo tenemos.");
            context.Wait(MessageReceived);
            return;


        }


    }
}