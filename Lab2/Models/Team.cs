﻿namespace Lab2.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string Name { get; set; } = "";
        public string Country { get; set; }  = "";
        public string City { get; set; }  = "";
        public DateTime FoundingDate { get; set; }
        public virtual ICollection<Player> ?Players { get; set; }
        public virtual int LeagueId { get; set; }
        public virtual League League { get; set; } = null!;
        public virtual ICollection<Match> ?HomeMatches { get; set; }
        public virtual ICollection<Match> ?AwayMatches { get; set; }
    }
}
