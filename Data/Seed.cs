using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedConctacts(DataContext context){

            if(await context.AppContact.AnyAsync()) return;

            //PAZI Format atributa u .json! (case sensitive iako uključiš JsonSerializerOptions!)

            var contactData = await File.ReadAllTextAsync("Data/ContactSeedData.json");

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var contacts = JsonSerializer.Deserialize<List<AppContact>>(contactData);

            foreach (var contact in contacts)
            {
                await context.AddAsync(contact);
                
            }

            await context.SaveChangesAsync();
        }
    }
}