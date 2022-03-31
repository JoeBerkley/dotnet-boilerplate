using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Models.Enums;
using System;
using System.Threading.Tasks;

namespace Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<ApplicationRole> roleManager)
        {
            foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                await roleManager.CreateAsync(new ApplicationRole()
                {
                    Name = role.ToString(),
                    CreatedDate = DateTime.Now
                });
            }
        }
    }
}