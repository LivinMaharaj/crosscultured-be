using ccc_be.Data;
using ccc_be.Models;
using System.Diagnostics.Eventing.Reader;

namespace ccc_be.Service
{
    public class EventService
    {
        
        public Dictionary<string, List<Events>> TargetEvent(EventsDbContext _eventsDbContext)
        {
            DateTime currentDate = DateTime.Now;
            string dayOfWeek = currentDate.ToString("dddd").ToLower();
            string currentTimeAsString = DateTime.Now.ToString("HH:mm:ss");
            var currentTime = TimeSpan.Parse(currentTimeAsString);


            int eventDayId = Models.DaysOfTheWeek.daysOfTheWeek[dayOfWeek];
            var upComingEvents = _eventsDbContext.UpcomingEvents(currentDate).ToList();
            var onGoingEvents = _eventsDbContext.OngoingEvents(currentDate).ToList();
            Dictionary<string, List<Events>> resultant = new Dictionary<string, List<Events>>();
            List<Events> targetEvent = new List<Events>();
            List<Events> otherEvents = new List<Events>();
            if (onGoingEvents.Count()>0) 
            {
                targetEvent.Add(onGoingEvents[0]);
                resultant.Add("TargetOngoingEvent", targetEvent);
                onGoingEvents.RemoveAt(0);
            }
            else if (upComingEvents.Count()>0)
            {
                targetEvent.Add(upComingEvents[0]);
                resultant.Add("TargetUpcomingEvent", targetEvent);
                upComingEvents.RemoveAt(0);
            };
            otherEvents.AddRange(onGoingEvents);
            otherEvents.AddRange(upComingEvents);
            resultant.Add("OtherOngoingEvents", onGoingEvents);
            resultant.Add("OtherUpcomingEvents", upComingEvents);

            return resultant;
        }
    }
}
