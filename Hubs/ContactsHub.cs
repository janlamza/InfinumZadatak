using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs
{
    public class ContactsHub : Hub
    {
        public void BroadCastContacts(List<AppContact> contacts)
        {
            Clients.All.SendAsync("ReceiveContacts", contacts);
        }
        public void BroadCastContact(AppContact contact)
        {
            Clients.All.SendAsync("ReceiveContact", contact);
        }
    }
}