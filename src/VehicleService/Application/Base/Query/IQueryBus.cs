using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Base.Query
{
    public interface IQueryBus
    {
        Task<TResponse> Send<TResponse>(Query query);
    }
}
