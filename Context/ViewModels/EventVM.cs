using Context.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.ViewModels
{
    public class EventVM
    {
        public List<Product>? allProducts{ get; set; }
        public List<Sneaker>? allSneakers { get; set; }
        public List<Product>? SelectedProducts { get; set; }
        public List<Sneaker>? SelectedSneakers { get; set; }
        public string  Name { get; set; }
        public int Id { get; set; }

    }
}
