using bot_ej.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace bot_ej.Dialogs
{
    public class HotelBotDialog
    {
        public static readonly IDialog<string> dialog = Chain.PostToChain()
            .Select(msg => msg.Text)
            .Switch(
            new DefaultCase<string, IDialog<string>>((context, text) =>
             {
                 return Chain.ContinueWith(FormDialog.FromForm(ReservaHabitaciones.ConstruirForma, FormOptions.PromptInStart), AfterGreetingContinuation);
             }))
            .Unwrap()
            .PostToUser();

        private async static Task<IDialog<string>> AfterGreetingContinuation(IBotContext context, IAwaitable<object> item)
        {
            var token = await item;
            var name = "Usuario";
            context.UserData.TryGetValue<string>("Nombre", out name);
            return Chain.Return($"Gracias por usar el hotel bot: {name}");
        }
    }

}