using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;

namespace FoodBot.Commands.CommandHandling
{
    public interface ICommandBroker
    {

        Task Execute(ITurnContext turnContext, CancellationToken cancellationToken, string message);

    }
}