using Context.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.ViewModels
{
    public class PSDetailVM
    {
        public List<Product>? Products { get; set; }
        public List<Sneaker>? Sneakers { get; set; }
    }
}
