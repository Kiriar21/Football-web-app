namespace Lab2.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; } = "";
        public virtual ICollection<Article> ?Articles { get; set; }
    }
}
