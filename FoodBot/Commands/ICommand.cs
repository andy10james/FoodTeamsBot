using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodBot.Commands
{

    public interface ICommand
    {

        Task<bool> Do(string arg);

        Task Undo();

    }

}
