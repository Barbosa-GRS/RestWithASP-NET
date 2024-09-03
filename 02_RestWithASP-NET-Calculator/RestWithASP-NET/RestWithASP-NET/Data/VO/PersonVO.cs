using System.Text.Json.Serialization;

namespace RestWithASP_NET.Data.VO;

public class PersonVO 
{
    // [JsonPropertyName("code")]--- para serializar a costumização 
    //  [JsonIgnore]--- para ignorar o item abaixo
    public long Id { get; set; }
   
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
   
    public string Addres { get; set; }
   
    public string Gender { get; set; }
}
