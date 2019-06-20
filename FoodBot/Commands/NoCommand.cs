using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;

namespace FoodBot.Commands {
    public class NoCommand : ICommand
    {

        private readonly ITurnContext _turnContext;
        private readonly CancellationToken _cancellationToken;

        public NoCommand(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            this._turnContext = turnContext;
            this._cancellationToken = cancellationToken;
        }

        public async Task<bool> Do(string arg)
        {
            await this._turnContext.SendActivityAsync($"Pingu is confused, I don't know what you want me to do. Type 'help' to see what pepe can do for you.", cancellationToken: this._cancellationToken);
            return false;
        }

        public async Task Undo()
        {
        }

    }
}
