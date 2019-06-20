using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using FoodBot.Repositories;
using FoodBot.Model;
using Microsoft.Bot.Schema;

namespace FoodBot.Commands {
    public class CanHazCommand : ICommand
    {

        private IRepository<Order> _repository;
        private readonly ITurnContext _turnContext;
        private readonly CancellationToken _cancellationToken;

        private Order _order;

        public CanHazCommand(IRepository<Order> repository, ITurnContext turnContext, CancellationToken cancellationToken)
        {
            this._repository = repository;
            this._turnContext = turnContext;
            this._cancellationToken = cancellationToken;
        }

        public async Task<bool> Do(string arg)
        {
            this._repository.SaveOrUpdate(this._order = new Order() {
                Account = this._turnContext.Activity.From,
                OrderDetail = arg
            });

            var activity = Activity.CreateMessageActivity();
            activity.Text = $"Ok, added _{arg}_ to your order. Thank pingu for this gift.";
            activity.TextFormat = TextFormatTypes.Markdown;
            await this._turnContext.SendActivityAsync(activity, cancellationToken: this._cancellationToken);

            return true;
        }

        public async Task Undo()
        {
            await this._turnContext.SendActivityAsync($"Make up your mind! I've deleted your order.", cancellationToken: this._cancellationToken);
            this._repository.Delete(this._order);
        }

    }
}
