using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore_API.Services
{
    public interface IRepositoryBase<T> where T:class
    {
        Task<IList<T>> FindAll();
        Task<T> FindById(int id);
        Task<bool> Update(T entity);
        Task<bool>Create(T entity);
        Task<bool>Delete(T entity);
        Task<bool> Save();
        Task<bool> ifExists(int id);
    }
}
