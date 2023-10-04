using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime TimeRemaining { get; set; }
        public IEnumerable<Sneaker>? SneakerEvents { get; set; }
        public IEnumerable<Product>? ProductsEvents { get; set; }
    }
}
