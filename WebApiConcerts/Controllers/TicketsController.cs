using DataLayer.Data;
using DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApiConcerts.Models;

namespace WebApiConcerts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public TicketsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager; 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if(id == 0)
            {
               return BadRequest();
            }
            var ticket = await _context.Tickets.FindAsync(id);
            if(ticket == null)
            {
                return NotFound();
            }
            return Ok(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
               await _context.AddAsync(ticket);
               await  _context.SaveChangesAsync();
               return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromForm] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
              var updateTicket = await _context.Tickets.FindAsync(ticket.Id);
                if (updateTicket != null)
                {
                    updateTicket.Sector = ticket.Sector;
                    updateTicket.Price = ticket.Price;
                    updateTicket.booked = ticket.booked;
                     _context.Tickets.Update(updateTicket);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
             
            }
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return Ok("Delete is succsesful");
        }

        [HttpPost("Book")]
        public async Task<IActionResult> Book([FromForm] int id, [FromForm] string userId)
        {
            if(id == 0 || userId == null) { return BadRequest(); }  
            var user = await _userManager.FindByIdAsync(userId);
            var ticket = await _context.Tickets.FindAsync(id);
            if (user == null || ticket==null) { return NotFound(); }
            ticket.UserId = userId;
            ticket.booked = true;
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("UnBook")]
        public async Task<IActionResult> UnBook([FromForm] int id)
        {
            if (id == 0 ) { return BadRequest(); }
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null) { return NotFound(); }
            ticket.UserId = null;
            ticket.booked = false;
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
            return Ok();
        }


    }
}
