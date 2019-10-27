using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace SportsStore.Models
{
    public static class IdentitySeedData
    {
        private const string admUser = "Admin";
        private const string admPassword = "Secret123$";

        public static async Task EnsurePopulated
            (UserManager<IdentityUser> userManager) {

            IdentityUser user = await userManager.FindByIdAsync(admUser);
            if (user == null) {
                user = new IdentityUser("Admin");
                await userManager.CreateAsync(user, admPassword);
            }

        }
    }
}