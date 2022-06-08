using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VirtualMatch.Entities.Database;

namespace VirtualMatch.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            if (await context.Users.AnyAsync())
                return;

            var userData = await System.IO.File.ReadAllTextAsync("..\\VirtualMatch.Data\\UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<User>>(userData);

            foreach(var user in users)
            {
                using var hmac = new HMACSHA512();

                user.UserName = user.UserName.ToLower();
                user.Password = hmac.ComputeHash(Encoding.UTF8.GetBytes("123"));
                user.Salt = hmac.Key;

                context.Users.Add(user);
                context.SaveChanges();
            }
        }
    }
}
