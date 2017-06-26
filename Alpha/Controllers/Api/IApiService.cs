using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Alpha.Controllers.Api
{
    public interface IApiService<T,TKey>
    {
        Task<IHttpActionResult> Read();
        Task<IHttpActionResult> Read(TKey item);
        Task<IHttpActionResult> Create(T item);
        Task<IHttpActionResult> Update(T item);
        Task<IHttpActionResult> Remove(TKey item);
    }
}
