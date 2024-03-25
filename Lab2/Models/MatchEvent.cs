namespace Lab2.Models
{
    public class MatchEvent
    {
        public int MatchEventId { get; set; }
        public int Minute { get; set; }
        public virtual int MatchId { get; set; }
        public virtual Match Match { get; set; } = null!;
        public virtual int MatchPlayerId { get; set; }
        public virtual MatchPlayer ?MatchPlayer { get; set; }
        public virtual int EventTypeId { get; set; }
        public virtual EventType EventType { get; set; } = null!;
    }
}
