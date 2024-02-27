using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.DATA.Models
{
    public class User : BaseEntities
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Address {  get; set; }
        public string? City { get; set; }
        public int? Region { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
        public ICollection<New> News { get; set; } = new List<New>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
