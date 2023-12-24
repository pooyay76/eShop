using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (userManager.Users.Any() == false)
            {
                var user = new AppUser()
                {
                    DisplayName = "Bob",
                    Email = "bob2020@gmail.com",
                    UserName = "bobity",
                    Address = new Address()
                    {
                        FirstName = "Bob",
                        LastName = "Barker",
                        City = "New York",
                        Street = "10th Street",
                        PostalCode = "217289513",
                        Province = "NY"
                    }
                };
                await userManager.CreateAsync(user, "p@$$word1");
                return;
            }
        }
    }
}
