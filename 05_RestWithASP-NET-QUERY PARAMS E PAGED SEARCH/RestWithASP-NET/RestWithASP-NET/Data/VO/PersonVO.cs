using RestWithASP_NET.Hypermedia;
using RestWithASP_NET.Hypermedia.Abstract;
using System.Text.Json.Serialization;

namespace RestWithASP_NET.Data.VO;

public class PersonVO : ISupportsHyperMedia
{
    // [JsonPropertyName("code")]--- para serializar a costumização 
    //  [JsonIgnore]--- para ignorar o item abaixo
    public long Id { get; set; }
   
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
   
    public string Addres { get; set; }
   
    public string Gender { get; set; }

    public bool Enabled { get; set; }

    public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
}
