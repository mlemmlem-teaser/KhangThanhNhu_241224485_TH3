using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication_TH2_Lab5.Models;

namespace WebApplication_TH2_Lab5.Controllers
{
    public class StudentController : Controller
    {
        // static: controller được tạo mới cho mỗi request nên danh sách phải static để giữ dữ liệu đã thêm
        private static readonly List<Student> listStudents = new List<Student>
        {
            new Student { Id = 101, Name = "Hải Nam",     Email = "nam@gmail.com",   Password = "Abc@12345", Branch = Branch.IT, Gender = Gender.Male,   IsRegular = true,  Address = "A1-2018", DateOfBirth = new DateTime(2000, 5, 12), Score = 8.5 },
            new Student { Id = 102, Name = "Minh Tú",     Email = "tu@gmail.com",    Password = "Abc@12345", Branch = Branch.BE, Gender = Gender.Female, IsRegular = true,  Address = "A1-2019", DateOfBirth = new DateTime(2001, 3, 20), Score = 7.0 },
            new Student { Id = 103, Name = "Hoàng Phong", Email = "phong@gmail.com", Password = "Abc@12345", Branch = Branch.CE, Gender = Gender.Male,   IsRegular = false, Address = "A1-2020", DateOfBirth = new DateTime(1999, 8, 1),  Score = 6.5 },
            new Student { Id = 104, Name = "Xuân Mai",    Email = "mai@gmail.com",   Password = "Abc@12345", Branch = Branch.EE, Gender = Gender.Female, IsRegular = false, Address = "A1-2021", DateOfBirth = new DateTime(2002, 11, 30), Score = 9.0 }
        };

        public IActionResult Index()
        {
            return View(listStudents);
        }

        [HttpGet]
        public IActionResult Create()
        {
            LoadLists();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student s)
        {
            if (ModelState.IsValid)
            {
                s.Id = listStudents.Last<Student>().Id + 1;
                listStudents.Add(s);
                return View("Index", listStudents);
            }

            LoadLists();
            return View(s);
        }

        private void LoadLists()
        {
            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();

            ViewBag.AllBranches = new List<SelectListItem>
            {
                new SelectListItem { Text = "IT", Value = "1" },
                new SelectListItem { Text = "BE", Value = "2" },
                new SelectListItem { Text = "CE", Value = "3" },
                new SelectListItem { Text = "EE", Value = "4" }
            };
        }
    }
}
