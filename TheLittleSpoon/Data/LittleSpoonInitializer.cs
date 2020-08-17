using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheLittleSpoon.Models;

namespace TheLittleSpoon.Data
{
    public class LittleSpoonInitializer
    {
        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByEmailAsync("littlespoon@littlespoon.org").Result == null)
            {
                ApplicationUser admin = new ApplicationUser
                {
                    UserName = "littlespoon",
                    Email = "littlespoon@littlespoon.org",
                    PhoneNumber = "0505050505"
                };

                try
                {
                    if (!userManager.CreateAsync(admin, "123456").Result.Succeeded)
                    {
                        throw new Exception("Failed to add admin user");
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Error adding admin user");
                }
            }
        }
    }
}
