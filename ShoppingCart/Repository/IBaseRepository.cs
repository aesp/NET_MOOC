using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingCart.Repository
{
    public interface IBaseRepository<T>
    {

		Task<bool> Save(T entity);
		Task<IEnumerable<T>> GetAll();
		Task<T> Get(long id);
		Task<bool> Delete(long id);
		Task<bool> Update(T entity);

    }
}
