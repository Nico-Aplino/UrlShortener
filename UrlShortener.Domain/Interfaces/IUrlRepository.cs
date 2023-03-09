using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.BLL.Models;

namespace UrlShortener.BLL.Interfaces
{
    public interface IUrlRepository: IRootRepository<UrlModel>
    {
    }
}
