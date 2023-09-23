using I_STORE.Models;

namespace I_STORE.Data
{
    public static class SeedData
    {
        public static void SneakerSeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();
                if (!context.Sneakers.Any())
                {
                    context.Sneakers.Add(
                        new Sneaker()
                        {
                            Name = "Nike Air jordan 1",
                            Company = Enum.Company.NikeJordans,
                            Count = 2,
                            Size = 42
                        }
                        );
                    context.SaveChanges();
                }
            }
        }
    }
}
