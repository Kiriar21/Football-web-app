using Microsoft.AspNetCore.Mvc;
using Lab2.Models;

namespace Lab2.Controllers
{
    public class StudentController : Controller
    {
        private List<Student> _students =
            [
                new Student
                {
                    Id = 1,
                    Imie = "Jan",
                    Nazwisko = "Kowalski",
                    DataUrodzenia = DateTime.Now,
                    Indeks = 111111,
                    Kierunek = "Zarzadzanko"
                },
                new Student
                {
                    Id = 2,
                    Imie = "Anna",
                    Nazwisko = "Nowak",
                    DataUrodzenia = DateTime.Now,
                    Indeks = 222222,
                    Kierunek = "Budownictwo"
                },
                new Student
                {
                    Id = 3,
                    Imie = "Marlena",
                    Nazwisko = "Krzywousta",
                    DataUrodzenia = DateTime.Now,
                    Indeks = 333333,
                    Kierunek = "Medycyna"
                },
            ];
        public IActionResult Index()
        {
            return View(_students);
        }

        public IActionResult Details(int id)
        {
            var student = _students.FirstOrDefault(x => x.Id == id);
            return View(student);
        }
    }
}
