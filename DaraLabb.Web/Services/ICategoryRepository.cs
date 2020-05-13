using System.Collections.Generic;

namespace DaraLabb.Web.Models
{
    public interface ICategoryRepository
    {
        public IEnumerable<Category> GetAll { get; }
    }
}
