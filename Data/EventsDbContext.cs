using ccc_be.Models;
using Microsoft.EntityFrameworkCore;

namespace ccc_be.Data
{
    public class EventsDbContext : DbContext
    {
        public EventsDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<RecurringEvents> RecurringEvents { get; set; }

        public DbSet<NonRecurringEvents> NonRecurringEvents { get; set; }

        public DbSet<Events> Events { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RecurringEvents>()
                .ToTable("recurringevents")
                .HasKey(e => e.EventID);

            modelBuilder.Entity<NonRecurringEvents>()
                .ToTable("nonrecurringevents")
                .HasKey(e => e.EventID);

            modelBuilder.Entity<Events>().ToTable("events").HasKey(e => e.EventID);
        }

        public IEnumerable<RecurringEvents> UpcomingRecurringEvents(int eventDayId, TimeSpan startTime)
        {
            var resultant = RecurringEvents.Where(e => e.EventDayId == eventDayId && e.StartTime > startTime).OrderBy(e=> e.EventDayId).Take(1).ToList();
            if (resultant.Count > 0) {
                return resultant;
            }
            resultant = RecurringEvents.Where(e => e.EventDayId > eventDayId).OrderBy(e => e.EventDayId).ThenBy(e=> e.StartTime).Take(1).ToList();
            if (resultant.Count > 0) { 
                return resultant; 
            }
            resultant = RecurringEvents.Where(e => e.EventDayId < eventDayId).OrderBy(e => e.EventDayId).Take(1).ToList();
            return resultant;
        }
        public IEnumerable<Events> UpcomingEvents(DateTime startTime) {
            var subquery = Events.Where(e => e.EventStartTime > startTime).OrderBy(e => e.EventStartTime).OrderByDescending(e => e.EventCategory).Select(e => e.EventStartTime).FirstOrDefault();
            var resultant = Events.Where(e => e.EventStartTime == subquery);

            return resultant;
        }
        public IEnumerable<Events> OngoingEvents(DateTime startTime)
        {
            var resultant = Events.Where(e => e.EventStartTime < startTime && e.EventEndTime > startTime).OrderByDescending(e=>e.EventCategory);
            return resultant;
        }

    }
}
