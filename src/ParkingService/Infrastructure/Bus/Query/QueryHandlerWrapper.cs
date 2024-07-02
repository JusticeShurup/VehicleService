using Application.Base.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Bus.Query
{
    internal abstract class QueryHandlerWrapper<TResponse>
    {
        public abstract Task<TResponse> Handle(Application.Base.Query.Query query, IServiceProvider provider);
    }

    internal class QueryHandlerWrapper<TQuery, TResponse> : QueryHandlerWrapper<TResponse> where TQuery
        : Application.Base.Query.Query
    {
        public override async Task<TResponse> Handle(Application.Base.Query.Query query, IServiceProvider provider)
        {
            var handler = (IQueryHandler<TQuery, TResponse>) provider.GetService(typeof(IQueryHandler<TQuery, TResponse>));

            if (handler == null)
                throw new NullReferenceException($"{nameof(QueryHandlerWrapper<TQuery, TResponse>)} Handler not found");

            return await handler.Handle((TQuery)query);
        }
    }
}
