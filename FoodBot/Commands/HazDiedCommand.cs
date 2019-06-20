using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using FoodBot.Repositories;
using FoodBot.Model;
using System.Linq;

namespace FoodBot.Commands {
    public class HazDiedCommand : ICommand
    {

        private IRepository<Order> _repository;
        private readonly ITurnContext _turnContext;
        private readonly CancellationToken _cancellationToken;

        private Order _order;

        public HazDiedCommand(IRepository<Order> repository, ITurnContext turnContext, CancellationToken cancellationToken)
        {
            this._repository = repository;
            this._turnContext = turnContext;
            this._cancellationToken = cancellationToken;
        }

        public async Task<bool> Do(string arg)
        {
            var self = false;
            if (self = string.IsNullOrEmpty(arg)) {
                arg = this._turnContext.Activity.From.Name;
            }
            this._order = this._repository.Get().FirstOrDefault(o => o.Account.Name == arg);
            if (this._order == null) {
                if (self) {
                    await this._turnContext.SendActivityAsync($"Pingu is sad - you didn't order anything though.", cancellationToken: this._cancellationToken);
                }
                else {
                    await this._turnContext.SendActivityAsync($"Pingu is sad for your loss - they didn't order anything though.", cancellationToken: this._cancellationToken);
                }
                return false;
            }
            this._repository.Delete(this._order);

            await this._turnContext.SendActivityAsync($"Pingu is sad for your loss - I've removed their order.", cancellationToken: this._cancellationToken);

            return true;
        }

        public async Task Undo()
        {
            await this._turnContext.SendActivityAsync($"Pingu is sad for your loss - they didn't order anything though.", cancellationToken: this._cancellationToken);
            this._repository.SaveOrUpdate(this._order);
        }

    }
}
