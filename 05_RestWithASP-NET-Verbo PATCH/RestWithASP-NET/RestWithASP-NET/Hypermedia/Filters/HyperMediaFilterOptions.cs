using RestWithASP_NET.Hypermedia.Abstract;

namespace RestWithASP_NET.Hypermedia.Filters
{
    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> ContetResponseEnricherList { get; set; } = new List<IResponseEnricher> ();
    }
}
