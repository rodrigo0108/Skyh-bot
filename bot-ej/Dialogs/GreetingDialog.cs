using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace bot_ej.Dialogs
{
    [Serializable]
    public class GreetingDialog : IDialog
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Hola soy El Bot! ");
            await Respond(context);
            context.Wait(MessageRecievedAsync);
         
        }

        private static async Task Respond(IDialogContext context)
        {
            var userName = String.Empty;
            context.UserData.TryGetValue<string>("Nombre", out userName);
            if (string.IsNullOrEmpty(userName))
            {
                await context.PostAsync("¿Cúal es tu nombre?");
                context.UserData.SetValue<bool>("GetName", true);
            }
            else
            {
                await context.PostAsync(String.Format("Hola {0}. ¿En que puedo ayudarte?", userName));
            }
        }
        public async Task MessageRecievedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            var userName = String.Empty;
            var getName = false;

            context.UserData.TryGetValue<string>("Nombre", out userName);
            context.UserData.TryGetValue<bool>("GetName", out getName);

            if (getName)
            {
                userName = message.Text;
                context.UserData.SetValue<string>("Nombre", userName);
                context.UserData.SetValue<bool>("GetName", false);
            }

            await Respond(context);
            context.Done(message);
        }
    }
}