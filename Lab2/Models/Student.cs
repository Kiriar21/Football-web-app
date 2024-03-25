namespace Lab2.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string ?Imie { get; set; }
        public string ?Nazwisko { get; set; }
        public int Indeks {  get; set; }
        public DateTime DataUrodzenia { get; set; }
        public string ?Kierunek { get; set; }
    }
}
