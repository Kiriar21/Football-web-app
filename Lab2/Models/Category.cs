namespace Lab2.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = "";
        public virtual ICollection<Article> ?Articles { get; set; }
    }
}
