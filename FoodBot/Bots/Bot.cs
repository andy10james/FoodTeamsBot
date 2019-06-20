using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using FoodBot.Commands.CommandHandling;
using ICommand = FoodBot.Commands.ICommand;

namespace FoodBot.Bots
{
    public class Bot : ActivityHandler
    {

        private readonly ICommandBroker _commandBroker;

        public Bot(ICommandBroker commandBroker)
        {
            this._commandBroker = commandBroker;
        }

        public override async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default)
        {
            if (turnContext.Activity.Type != ActivityTypes.Message) {
                return;
            }
            var input = turnContext.Activity.Text;
            input = new Regex(@"<\/?at>").Replace(input, "").Trim(' ', (char)160, '\n', '\r');
            await this._commandBroker.Execute(turnContext, cancellationToken, input);
        }

    }
}
