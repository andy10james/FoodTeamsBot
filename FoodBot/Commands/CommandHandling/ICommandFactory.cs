using System.Threading;
using Microsoft.Bot.Builder;

namespace FoodBot.Commands.CommandHandling
{
    public interface ICommandFactory
    {

        (ICommand command, string arg) Create(string command, ITurnContext turnContext, CancellationToken cancellationToken);

    }
}
