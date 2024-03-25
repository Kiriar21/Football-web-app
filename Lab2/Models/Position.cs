namespace Lab2.Models
{
    public class Position
    {
        public int PositionId {  get; set; }
        public string Name { get; set; } = "";
        public virtual ICollection<MatchPlayer> ?MatchPlayers { get; set; }
        public virtual ICollection<Player> ?Players { get; set; }
    }
}
