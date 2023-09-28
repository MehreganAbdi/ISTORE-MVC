using Context.Data.Enum;
using Microsoft.AspNetCore.Http;

namespace Context.ViewModels
{
    public class CreateSneakerVM
    {
        public int SneakerId { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public int Price { get; set; }
        public IFormFile? Image { get; set; }
        public int Count { get; set; }
        public Company Company { get; set; }
    }
}
