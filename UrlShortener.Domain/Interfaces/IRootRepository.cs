using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.BLL.Interfaces
{
    public interface IRootRepository<T>
    {
        Task<T> Add(T entity);
    }
}
