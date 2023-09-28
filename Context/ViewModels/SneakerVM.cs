using Context.Data.Enum;

namespace Context.ViewModels
{
    public class SneakerVM
    {
        public int SneakerId { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public Company Company { get; set; }
    }
}
