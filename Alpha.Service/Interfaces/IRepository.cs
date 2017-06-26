using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Service.Interfaces
{
    public interface IRepository<T, TKey> where T : class
    {
        Task<List<T>> Read();
        Task<T> Read(TKey id);
        Task<List<T>> Read(Guid userid);
        Task Update(T item);
        Task Delete(TKey id);
        Task Delete(TKey id,Guid userid);
        Task<T> Insert(T item);
    }
}
