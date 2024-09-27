using Microsoft.AspNetCore.Mvc.Filters;

namespace RestWithASP_NET.Hypermedia.Abstract
{
    public interface IResponseEnricher
    {
        bool CanEnricher(ResultExecutingContext context);
        Task Enricher(ResultExecutingContext context);
    }
}
