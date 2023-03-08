using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TelephoneNumberController : Controller
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public TelephoneNumberController(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }

        [HttpPost("AddTelephoneNumber/{idContact}")]
        public async Task<ActionResult> AddTelephoneNumber(int idContact, AppTelephoneNumberAddDTO TelNumber)
        {
            var contact = await _context.AppContact.Include(x => x.PhoneNumbers).FirstOrDefaultAsync(x => x.Id == idContact);

            if (contact == null) return BadRequest("Requested Contact doesn't exist");

            var mappedTelephoneNumber = _mapper.Map<AppTelephoneNumber>(TelNumber);

            contact.PhoneNumbers.Add(mappedTelephoneNumber);

            if (await _context.SaveChangesAsync() > 0) return Ok("Telephone number saved to database");

            return BadRequest("Problem occured while adding telephone number to database");
        }

        [HttpDelete("DeleteTelephoneNumber/{id}")]
        public async Task<ActionResult> DeleteTelephoneNumber(int id)
        {

            var contact = await _context.AppContact
                .Include(x => x.PhoneNumbers)
                .Where(x => x.PhoneNumbers.Any(x => x.Id == id))
                .FirstOrDefaultAsync();

            if (contact == null) return BadRequest("Telephone number doesn't exist");

            var phoneDel = contact.PhoneNumbers.FirstOrDefault(x => x.Id == id);

            contact.PhoneNumbers.Remove(phoneDel);

            if (await _context.SaveChangesAsync() > 0) return Ok("Telephone number deleted");

            return BadRequest("Problem deleting telephone number");
        }

        [HttpPut("UpdateTelephoneNumber")]
        public async Task<ActionResult> UpdateTelephoneNumber(AppTelephoneNumberDTO updateTelNumber)
        {

            var contact = await _context.AppContact
                .Include(x => x.PhoneNumbers)
                .Where(x => x.PhoneNumbers.Any(x => x.Id == updateTelNumber.Id))
                .FirstOrDefaultAsync();
            
            if(contact == null) return BadRequest("Contact with specified telephone number not found");

            var phone = contact.PhoneNumbers.FirstOrDefault(x => x.Id == updateTelNumber.Id);

            phone.Phonenumber = updateTelNumber.Phonenumber;

            if(await _context.SaveChangesAsync() > 0) return Ok("Changes saved to database");
            
            return BadRequest("Problem occured with saving changes to database");

        }


    }
}