using DataLayer.Data;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiConcerts.Models;

namespace WebApiConcerts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConcertsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ConcertsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Concerts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommonDataOfConcert>>> Get()
        {
            if (_context.ConcertsData == null)
            {
                return NotFound();
            }
            return await _context.ConcertsData.ToListAsync();
            
        }

        // GET: api/Concerts/5

        [HttpGet("{id}")]
        //[Authorize]
        public async Task<ActionResult<CommonDataOfConcert>> Get(int id)
        {
            if (id==0)
            {
                return BadRequest();
            }
            var consert = await _context.ConcertsData.Include(c=>c.Tickets).FirstOrDefaultAsync(c=>c.Id==id);
            if (consert == null) { return NotFound(); }
            return consert;
        }

        [HttpPost]
        [Authorize(Roles ="admin")]
        public async Task<ActionResult<CommonDataOfConcert>> Post([FromForm] CommonTypeData model)
        {
            CommonDataOfConcert data;
            if (ModelState.IsValid)
            {
                if (model.EventType == "Party")
                {
                    data = new Party
                    {
                        //Id = model.Id,
                        EventName = model.EventName,
                        EventType = model.EventType,
                        NamePerformer = model.NamePerformer,
                        AmountOfTickets = model.AmountOfTickets,
                        DateConcert = model.DateConcert,
                        Image = model.Image.FileName,
                        LocationConcert = model.LocationConcert,
                        Age = model.Age,
                    };
                }
                else if (model.EventType == "OpenAir")
                {
                    data = new OpenAir
                    {
                        //Id = model.Id,
                        EventName = model.EventName,
                        EventType = model.EventType,
                        NamePerformer = model.NamePerformer,
                        AmountOfTickets = model.AmountOfTickets,
                        DateConcert = model.DateConcert,
                        Image = model.Image.FileName,
                        LocationConcert = model.LocationConcert,
                        Headliner = model.Headliner,
                        HowToGet = model.HowToGet,
                    };
                }
                else
                {
                    data = new Classic
                    {
                       // Id = model.Id,
                        EventName = model.EventName,
                        EventType = model.EventType,
                        NamePerformer = model.NamePerformer,
                        AmountOfTickets = model.AmountOfTickets,
                        DateConcert = model.DateConcert,
                        Image = model.Image.FileName,
                        LocationConcert = model.LocationConcert,
                        Voicetype = model.Voicetype,
                    };
                }
                if (model.Image != null)
                {
                    // путь к папке Files
                    string path = "D:/Учебные проекты/Centaurea/front/src/assets/" + model.Image.FileName;
                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream( path, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(fileStream);
                    }
                   
                }

                _context.Add(data);
                _context.SaveChanges();
                return Ok(data);



            }

            return BadRequest("Model is not valid");
           
          
        }
        [HttpPut]
        [Authorize(Roles ="admin")]
        public async Task<ActionResult<CommonDataOfConcert>> Put([FromForm] CommonTypeData model)
        {
            
            if (ModelState.IsValid)
            {
                var concert = await _context.ConcertsData.FindAsync(model.Id);
                if(concert == null)
                {
                    return NotFound();
                }
                concert.EventName = model.EventName;
                concert.EventType = model.EventType;
                concert.NamePerformer = model.NamePerformer;
                concert.AmountOfTickets = model.AmountOfTickets;
                concert.DateConcert = model.DateConcert;
                concert.Image = model.Image.FileName;
                concert.LocationConcert = model.LocationConcert;
                if (model.EventType == "Party")
                {
                    ((Party)concert).Age = model.Age;
                }
                else if(model.EventType == "OpenAir")
                {
                    ((OpenAir)concert).HowToGet = model.HowToGet;
                    ((OpenAir)concert).Headliner = model.Headliner;
                }
                else
                {
                    ((Classic)concert).Voicetype = model.Voicetype;
                }

                _context.ConcertsData.Update(concert);
               await _context.SaveChangesAsync();

                return Ok(concert);
            }
            return BadRequest();  
           
        }

        [HttpDelete("{id}")]
        [Authorize(Roles ="admin")]
        public async Task<ActionResult> Delete(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var concert = await _context.ConcertsData.FindAsync(id);
            if (concert == null)
            {
                return NotFound();
            }
             _context.ConcertsData.Remove(concert);
             await _context.SaveChangesAsync();
             return Ok();



        }


    }



}
