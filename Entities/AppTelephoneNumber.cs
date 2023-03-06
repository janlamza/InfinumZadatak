using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class AppTelephoneNumber
    {
        public int Id { get; set; }
        public string Phonenumber { get; set; }
        public int AppContactId { get; set; }
        public AppContact AppContact { get; set; }

    }
}