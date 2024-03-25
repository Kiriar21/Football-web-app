namespace Lab2.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Title { get; set; } = "";
        public string Content { get; set; }  = "";
        public virtual int ArticleId { get; set; }
        public virtual Article Article { get; set; } = null!;

    }
}
