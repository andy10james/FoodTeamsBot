using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema.Teams;
using FoodBot.Commands.CommandHandling;

namespace FoodBot.Commands {
    public class UndoCommand : ICommand {

        private readonly ICommandHistory _history;
        private readonly ITurnContext _turnContext;
        private readonly CancellationToken _cancellationToken;

        public UndoCommand(ICommandHistory history, ITurnContext turnContext, CancellationToken cancellationToken) {
            this._history = history;
            this._turnContext = turnContext;
            this._cancellationToken = cancellationToken;
        }

        public async Task<bool> Do(string arg) {
            var lastCommand = this._history.Pop(this._turnContext);
            if (lastCommand == null) {
                await this._turnContext.SendActivityAsync($"There's nothing to undo. :/", cancellationToken: this._cancellationToken);
                return false;
            }
            await lastCommand.Undo();
            return false;
        }

        public async Task Undo() { }

    }
}
