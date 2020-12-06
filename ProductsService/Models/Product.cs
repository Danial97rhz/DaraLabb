using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsService.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public decimal Price { get; set; }
        public string ImgUrl { get; set; }
        public bool InStock { get; set; }
        public Guid? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
