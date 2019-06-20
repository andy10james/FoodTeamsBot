using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;

namespace FoodBot.Commands.CommandHandling {
    public class CommandBroker : ICommandBroker
    {

        private readonly ICommandFactory _commandFactory;
        private readonly ICommandHistory _commandHistory;

        public CommandBroker(ICommandFactory commandFactory, ICommandHistory commandHistory)
        {
            this._commandFactory = commandFactory;
            this._commandHistory = commandHistory;
        }

        public async Task Execute(ITurnContext turnContext, CancellationToken cancellationToken, string message)
        {
            var (command, arg) = this._commandFactory.Create(message, turnContext, cancellationToken);
            var pushToHistory = await command.Do(arg);
            if (pushToHistory)
                this._commandHistory.Push(turnContext, command);
        }

    }
}
