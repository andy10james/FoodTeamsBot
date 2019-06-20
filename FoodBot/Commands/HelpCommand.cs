using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace FoodBot.Commands {
    public class HelpCommand : ICommand
    {

        private readonly ITurnContext _turnContext;
        private readonly CancellationToken _cancellationToken;

        public HelpCommand(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            this._turnContext = turnContext;
            this._cancellationToken = cancellationToken;
        }

        public async Task<bool> Do(string arg)
        {
            var activity = Activity.CreateMessageActivity();
            activity.Text = "@ me or message me directly to tell me what to do. Here's what pingu can do for you:\n\n\r\n\u00A0\n\n\r\n" +
                "**\"can haz [order]\" or \"can has [order]\" or \"i want [order]\"** - Order the food you want!\n\n\r\n" +
                "**\"get lunch\"** - List all the order's I've received.\n\n\r\n" +
                "**\"clear\"** - Clean out the orders list and start afresh.\n\n\r\n" +
                "**\"i haz died\" or \"[name] has died\" or \"[name] haz died\"** - Scrath off your or someone else's order.\n\n\r\n" +
                "**\"undo\"** - Undo the last thing I was asked to do.\n";
            activity.TextFormat = TextFormatTypes.Markdown;
            await this._turnContext.SendActivityAsync(activity, cancellationToken: this._cancellationToken);
            return false;
        }

        public async Task Undo()
        {
        }

    }
}
