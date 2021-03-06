using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace discovery.Library.identity.Seeds
{
    public static class DefaultUsers
    {
        public static async Task SeedBasicUserAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new IdentityUser
            {
                UserName = "import@gmail.com",
                Email = "import@gmail.com",
                EmailConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "J@vad6364!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                }
            }
        }
        public static async Task SeedSuperAdminAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new IdentityUser
            {
                UserName = "javadbayzavi@gmail.com",
                Email = "javadbayzavi@gmail.com",
                EmailConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "J@vad6364!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
                }
            }
            await roleManager.SeedClaimsForSuperAdmin();
        }
        private async static Task SeedClaimsForSuperAdmin(this RoleManager<IdentityRole> roleManager)
        {
            try
            {
                var adminRole = await roleManager.FindByNameAsync("SuperAdmin");
                await roleManager.AddPermissionClaim(adminRole, "scenario");
                await roleManager.AddPermissionClaim(adminRole, "dataset");
                await roleManager.AddPermissionClaim(adminRole, "patterns");
                await roleManager.AddPermissionClaim(adminRole, "result");
                await roleManager.AddPermissionClaim(adminRole, "analyze");
                await roleManager.AddPermissionClaim(adminRole, "import");
                await roleManager.AddPermissionClaim(adminRole, "categories");
                await roleManager.AddPermissionClaim(adminRole, "Users");
                await roleManager.AddPermissionClaim(adminRole, "UserRoles");
                await roleManager.AddPermissionClaim(adminRole, "Roles");
                await roleManager.AddPermissionClaim(adminRole, "Permission");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.GeneratePermissionsForModule(module);
            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
                }
            }
        }
    }
}
