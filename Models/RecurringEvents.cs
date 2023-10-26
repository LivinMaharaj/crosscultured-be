using System.ComponentModel.DataAnnotations.Schema;

namespace ccc_be.Models
{
    [Table("recurringevents")]
    public class RecurringEvents
    {

        public int EventID { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }

        public string EventDay { get; set; }

        public int EventDayId { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
