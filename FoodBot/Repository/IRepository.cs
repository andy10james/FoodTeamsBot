using System.Collections.Generic;

namespace FoodBot.Repositories
{
    public interface IRepository<T>
    {

        void SaveOrUpdate(T item);

        void Delete(T item);

        IEnumerable<T> Get();

    }
}