using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.DATA.Models
{
    public class Product : BaseEntities
    {
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? Url { get; set; }
        public string? Description { get; set; }
        public string? Keywords { get; set; }
        public string? Image { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
