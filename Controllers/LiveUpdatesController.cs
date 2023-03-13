using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LiveUpdatesController : Controller
    {
        private readonly IHubContext<ContactsHub> _contactsHub;
    
        private readonly DataContext _context;
       
        public LiveUpdatesController(IHubContext<ContactsHub> contactsHub, DataContext context)
        {
            _context = context;

            _contactsHub = contactsHub;
        }

        [HttpGet("PushContacts")]
        public async Task<ActionResult> PushContacts(){
            
            var contacts = await _context.AppContact.Include(x => x.PhoneNumbers).ToListAsync();
            
            if(contacts == null) return BadRequest("No contacts to show");

            await _contactsHub.Clients.All.SendAsync("ReceiveContacts", contacts );
            
            return Ok("Contact Sent");
        }

        [HttpGet("PushContact/{id}")]
        public async Task<ActionResult> PushContact(int id){
            
            var contact = await _context.AppContact.Include(x => x.PhoneNumbers).FirstOrDefaultAsync(x => x.Id == id);
            
            if(contact == null) return BadRequest("No contact to show");

            await _contactsHub.Clients.All.SendAsync("ReceiveContact", contact);
            
            return Ok("Contact Sent");
        }
    }
}