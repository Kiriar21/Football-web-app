namespace Lab2.Models
{
    public class Article
    {
        public int ArticleId { get; set; }
        public string Title { get; set; } = "";
        public string Lead { get; set; } = "";
        public string Content { get; set; } = "";
        public DateTime CreationDate { get; set; }
        public virtual int AuthorId { get; set; }
        public virtual Author Author { get; set; } = null!;
        public virtual int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<Comment> ?Comments { get; set; }
        public virtual ICollection<Tag> ?Tags { get; set; }
        public virtual int MatchId { get; set; }
        public virtual Match ?Match { get; set; } 

        public Article()
        {
            ArticleId = 0;
            Title = "Nie odnaleziony artykuł";
            Lead = "Nie wiem co to lead";
            Content = "";
            CreationDate = DateTime.Now;
        }
    }

}
