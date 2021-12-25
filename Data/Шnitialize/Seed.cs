using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvrakYonetimSistemi.Models;
using Microsoft.AspNetCore.Identity;

namespace EvrakYonetimSistemi.Data.Initialize
{
    public class Seed
    {
        
        public static async Task Initilize(UserManager<User> _userManager)
        {
            if (!_userManager.Users.Any())
            {
                await _userManager.CreateAsync(new User() 
                    { Id = Guid.NewGuid().ToString(),
                        Email = "enesgurbuz4432@gmail.com",
                        UserName = "ahmet.gurbuz5@sakarya.edu.tr",
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        FirstName = "Ahmet Enes",
                        LastName = "Gürbüz"

                    }, "123");
            }
        }
    }
}
