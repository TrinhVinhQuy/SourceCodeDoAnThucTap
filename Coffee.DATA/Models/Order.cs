using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.DATA.Models
{
    public class Order: BaseEntities
    {
        public int? UserId { get; set; }

        public DateTime? CreateOn { get; set; }

        public bool? Status { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public User User { get; set; }
    }
}
