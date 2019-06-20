using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using FoodBot.Repositories;
using FoodBot.Model;
using Microsoft.Bot.Schema;
using System.Text;
using System.Linq;

namespace FoodBot.Commands {
    public class GetLunchCommand : ICommand
    {

        private IRepository<Order> _repository;
        private readonly ITurnContext _turnContext;
        private readonly CancellationToken _cancellationToken;

        public GetLunchCommand(IRepository<Order> repository, ITurnContext turnContext, CancellationToken cancellationToken)
        {
            this._repository = repository;
            this._turnContext = turnContext;
            this._cancellationToken = cancellationToken;
        }

        public async Task<bool> Do(string arg)
        {
            var strBuilder = new StringBuilder();
            var orders = this._repository.Get();
            if (!orders.Any()) {
                await this._turnContext.SendActivityAsync("I've not had any orders. Pingu sad.", cancellationToken: this._cancellationToken);
                return false;
            }
            foreach (var order in orders) {
                strBuilder.Append("**");
                strBuilder.Append(order.Account.Name);
                strBuilder.Append("**: ");
                strBuilder.Append(order.OrderDetail);
                strBuilder.Append("\n\n\r\n");
            }

            var activity = Activity.CreateMessageActivity();
            activity.Text = strBuilder.ToString();
            activity.TextFormat = TextFormatTypes.Markdown;
            await this._turnContext.SendActivityAsync(activity, cancellationToken: this._cancellationToken);

            return false;
        }

        public async Task Undo()
        {
        }

    }
}
