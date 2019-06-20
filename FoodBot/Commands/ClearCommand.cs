using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using FoodBot.Repositories;
using FoodBot.Model;
using Microsoft.Bot.Schema;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace FoodBot.Commands {
    public class ClearCommand : ICommand
    {

        private IRepository<Order> _repository;
        private readonly ITurnContext _turnContext;
        private readonly CancellationToken _cancellationToken;

        private IEnumerable<Order> _orders;

        public ClearCommand(IRepository<Order> repository, ITurnContext turnContext, CancellationToken cancellationToken)
        {
            this._repository = repository;
            this._turnContext = turnContext;
            this._cancellationToken = cancellationToken;
        }

        public async Task<bool> Do(string arg)
        {
            this._orders = this._repository.Get().ToList();
            foreach (var order in this._orders) {
                this._repository.Delete(order);
            }

            await this._turnContext.SendActivityAsync("Pingu has cleaned up!", cancellationToken: this._cancellationToken);
            return true;
        }

        public async Task Undo()
        {
            foreach (var order in this._orders) {
                this._repository.SaveOrUpdate(order);
            }
            await this._turnContext.SendActivityAsync("Pingu has restored the mess.", cancellationToken: this._cancellationToken);
        }

    }
}
