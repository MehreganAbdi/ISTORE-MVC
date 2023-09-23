using I_STORE.Models;
using Microsoft.AspNetCore.Identity;

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




        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "mehreganabdix@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "MehreganAbdi",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            FullAddress = "JannatAbad , ChaharbaghGharbi"
                        }

                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");

                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

              
            }


        }
    }
}
