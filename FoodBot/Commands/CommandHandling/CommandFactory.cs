using System;
using System.Threading;
using Microsoft.Bot.Builder;
using Microsoft.Extensions.DependencyInjection;
using FoodBot.Repositories;
using FoodBot.Model;

namespace FoodBot.Commands.CommandHandling {
    class CommandFactory : ICommandFactory
    {

        private readonly IServiceProvider _serviceProvider;

        public CommandFactory(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public (ICommand command, string arg) Create(string message, ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var messageCompare = message.ToLower();
            var orderRepo = this._serviceProvider.GetService<IRepository<Order>>();
            if (messageCompare.StartsWith("can has ") || messageCompare.StartsWith("can haz ")) {
                var arg = message.Substring(8);
                return (new CanHazCommand(orderRepo, turnContext, cancellationToken), arg);
            }
            if (messageCompare.StartsWith("i want ")) {
                var arg = message.Substring(7);
                return (new CanHazCommand(orderRepo, turnContext, cancellationToken), arg);
            }
            if (messageCompare == "clear") {
                return (new ClearCommand(orderRepo, turnContext, cancellationToken), null);
            }
            if (messageCompare == "get lunch") {
                return (new GetLunchCommand(orderRepo, turnContext, cancellationToken), null);
            }
            if (messageCompare.EndsWith(" haz died") || messageCompare.EndsWith(" has died")) {
                var arg = message.Substring(0, message.Length - 9);
                return (new HazDiedCommand(orderRepo, turnContext, cancellationToken), arg.ToLower() == "i" ? null : arg);
            }
            if (messageCompare == "undo") {
                var commandHistory = this._serviceProvider.GetService<ICommandHistory>();
                return (new UndoCommand(commandHistory, turnContext, cancellationToken), null);
            }
            if (messageCompare == "help") {
                return (new HelpCommand(turnContext, cancellationToken), null);
            }
            return (new NoCommand(turnContext, cancellationToken), null);
        }

    }
}
