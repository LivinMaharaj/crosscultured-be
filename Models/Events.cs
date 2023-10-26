namespace ccc_be.Models
{
    public class Events
    {
        public int EventID { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public string EventCategory { get; set; }
        public DateTime EventStartTime { get; set; }
        public DateTime EventEndTime { get; set; }
        public string EventLocation { get; set; }
    }
}
