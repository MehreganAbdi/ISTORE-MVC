using I_STORE.Data.Enum;

namespace I_STORE.ViewModels
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
