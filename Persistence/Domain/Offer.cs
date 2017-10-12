using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Domain
{
    public class Offer : DomainEntity
    {
        [Key]
        public new int Id { get; set; }
        public string name { get; set; }
        public string item { get; set; }
        public string stats { get; set; }
        public string colour { get; set; }
        public string amount { get; set; }
        public int price { get; set; }
        public string sellerName { get; set; }
        public int sellerId { get; set; }
        public string itemCategory { get; set; }
        public DateTime Time { get; set; }
    }
}
