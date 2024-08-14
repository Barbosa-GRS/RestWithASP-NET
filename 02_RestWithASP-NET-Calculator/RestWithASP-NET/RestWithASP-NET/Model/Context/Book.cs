using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace RestWithASP_NET.Model.Context
{
    [Table ("books")]
    public class Book
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("author")]
        public string Author { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("lauch_date")]
        public DateTime LauchDate { get; set; }


    }
}
