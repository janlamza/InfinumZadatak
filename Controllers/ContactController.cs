using System.Text.Json;
using api.Helpers;
using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : Controller
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ContactController(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }

        [HttpPost("AddContact")]
        public async Task<ActionResult> AddContact(AddContactDTO NewContact)
        {
            var IsConstrained = await _context.AppContact.AnyAsync(x => x.Name == NewContact.Name || x.Address == NewContact.Address);

            if (IsConstrained) return BadRequest("User Name or User Address already exist!");

            var contact = _mapper.Map<AppContact>(NewContact);

            await _context.AppContact.AddAsync(contact);

            var result = await _context.SaveChangesAsync();

            if (result > 0) return Ok("new contact saved to database");

            return BadRequest("Contact not saved to database");
        }


        [HttpPut("UpdateContact")]
        public async Task<ActionResult> UpdateContact(AppContact updateContact)
        {

            var contact = await _context.AppContact.AsNoTracking().AnyAsync(x => x.Id == updateContact.Id);

            if (contact == false) return NotFound("Contact Not Found");

            var IsConstrained = await _context.AppContact.AnyAsync(x => x.Name == updateContact.Name || x.Address == updateContact.Address);

            if (IsConstrained) return BadRequest("User Name or User Address already exist");

            _context.AppContact.Update(updateContact);

            if (await _context.SaveChangesAsync() > 0) return Ok("changes saved to database");

            return BadRequest("No updates were made");
        }

        [HttpDelete("DeleteContact/{id}")]
        public async Task<ActionResult> DeleteContact(int id)
        {

            var contact = await _context.AppContact.FirstOrDefaultAsync(x => x.Id == id);

            if (contact == null) return BadRequest("Contact doesnt exist!");

            // može i ovako... nije lijepo ali je brže za -1 poziv za bazu
            // _context.AppContact.Remove(new AppContact{Id = id});

            _context.AppContact.Remove(contact);

            if (await _context.SaveChangesAsync() > 0) return Ok("contact remove from databse!");

            return BadRequest("Contact not deleted!");

        }

        [HttpGet("GetContact/{id}")]
        public async Task<ActionResult<AppContact>> GetContact(int id)
        {

            var contact = await _context.AppContact.Include(x => x.PhoneNumbers).FirstOrDefaultAsync(x => x.Id == id);

            //lazy loading
            // var contact = await _context.AppContact
            //     .Where(x => x.Id == id)
            //     .ProjectTo<AddContactDTO>(_mapper.ConfigurationProvider)
            //     .SingleOrDefaultAsync();

            if (contact == null) return BadRequest("Contact doesnt exist");

            return contact;
        }

        [HttpGet("GetContactsPaginated")]
        public async Task<ActionResult> GetContactsPaginated([FromQuery] ContactParams contactParams)
        {

            var contacts = _context.AppContact.Include(x => x.PhoneNumbers)
                .OrderBy(x => x.Name).AsQueryable();
            
            var contactsCount= contacts.Count();

            if (contactsCount == 0) return BadRequest("No contacts to show");

            var pagination = new PagedList(contactsCount, contactParams.Page, contactParams.ItemsPerPage);

            var returnContacts = await contacts
                .Skip((contactParams.Page - 1) * contactParams.ItemsPerPage)
                .Take(contactParams.ItemsPerPage)
                .ToListAsync();

            Response.Headers.Add("Pagination", System.Text.Json.JsonSerializer.Serialize(pagination));

            return Ok(returnContacts);

        }
    }
}