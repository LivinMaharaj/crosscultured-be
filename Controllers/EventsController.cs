using Microsoft.AspNetCore.Mvc;
using ccc_be.Data;
using Microsoft.EntityFrameworkCore;
using ccc_be.Models;
using ccc_be.Service;

namespace ccc_be.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventsController : Controller 
    {

        private readonly EventsDbContext _eventsDbContext;

        public EventsController(EventsDbContext eventsDbContext)
        {
            _eventsDbContext = eventsDbContext;
            
        }
        private readonly EventService _eventService = new EventService();


        [HttpGet]
        [Route("/recurring")]
        public async Task<IActionResult> GetAllRecurringEvents()

        {
            var recurringEvents =  _eventService.TargetEvent(_eventsDbContext);

            return Ok(recurringEvents);
        }

        [HttpGet]
        [Route("/nonrecurring")]
        public async Task<IActionResult> GetAllNonRecurringEvents()
        {
            return Ok();
        }
    }
}
