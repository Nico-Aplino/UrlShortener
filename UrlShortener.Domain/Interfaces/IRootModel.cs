using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.BLL.Interfaces
{
    internal interface IRootModel<T>
    {
        public T Id { get; set; }
    }
}
