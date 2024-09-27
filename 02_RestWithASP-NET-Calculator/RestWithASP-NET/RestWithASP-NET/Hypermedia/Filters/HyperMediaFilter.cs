using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RestWithASP_NET.Hypermedia.Filters
{
    public class HyperMediaFilter : ResultFilterAttribute
    {
        private readonly HyperMediaFilterOptions _hyperMediaFilterOptions;
        public HyperMediaFilter (HyperMediaFilterOptions hyperMediaFilterOptions)
        {
            _hyperMediaFilterOptions = hyperMediaFilterOptions;
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            TryEnricherResult(context);
            base.OnResultExecuting(context);
        }

        private void TryEnricherResult(ResultExecutingContext context)
        {
            if (context.Result is OkObjectResult);
            var enricher = _hyperMediaFilterOptions.
                ContetResponseEnricherList
                .FirstOrDefault(x => x.CanEnricher(context));
            if (enricher != null)
            {
                Task.FromResult(enricher.Enricher(context));
            }
        }
    }
}
