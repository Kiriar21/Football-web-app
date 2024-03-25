namespace Lab2.Models
{
    public class MatchPlayer
    {
        public int MatchPlayerId { get; set; }
        public DateTime ?StartTime { get; set; }
        public DateTime ?EndTime { get; set; }
        public virtual int MatchId { get; set; }
        public virtual Match Match { get; set; } = null!;
        public virtual ICollection<MatchEvent> ?MatchEvents { get; set; }
        public virtual int PositionId { get; set; }
        public virtual Position Position { get; set; } = null!;
        public virtual int PlayerId { get; set; }
        public virtual Player Player { get; set; } = null!;
    }
}
