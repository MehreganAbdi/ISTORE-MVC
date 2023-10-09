using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class EmailDTO
    {
        public string message { get; set; }
        public string subject { get; set; }
        public string reciever { get; set; }
    }
}
