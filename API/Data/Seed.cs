using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager, 
            RoleManager<AppRole> roleManager, DataContext context)
        {
            if (await userManager.Users.AnyAsync()) return;

            // var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            // var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            // if (users == null) return;

            var roles = new List<AppRole>
            {
                new AppRole{Name = "Member"},
                new AppRole{Name = "Admin"},
                new AppRole{Name = "Agent"},
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }
            
            // foreach (var user in users)
            // {
            //     user.UserName = user.UserName.ToLower();
            //     await userManager.CreateAsync(user, "Pa$$w0rd");
            //     await userManager.AddToRoleAsync(user, "Member");
            // }

            var admin = new AppUser
            {
                UserName = "admin",
                Email = "admin@kibokohouse.com"
            };

            var result = await userManager.CreateAsync(admin, "Kibokohouse1502@2022");

            if(!result.Succeeded) {
                Console.WriteLine(result.Errors);
            } 
            await userManager.AddToRoleAsync(admin, "Admin"); 

            // Add the product to the DbSet
            var statuses = new List<Status>
            {
                new Status{Identifier = 1, Value = "WithDrawn"},
                new Status{Identifier = 2, Value = "NotWithDrawn"},
            };

            foreach (var status in statuses)
            {
                context.Statuses.Add(status);
            }
            await context.SaveChangesAsync();
        }
    }
}
