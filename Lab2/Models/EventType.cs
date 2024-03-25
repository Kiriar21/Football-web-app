namespace Lab2.Models
{
    public class EventType
    {
        public int EventTypeId { get; set; }
        public string Name { get; set; } = "";
        public virtual ICollection<MatchEvent> MatchEvents { get; set; } = null!;
    }
}
