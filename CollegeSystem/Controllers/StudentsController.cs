using CollegeSystem.Data;
using CollegeSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeSystem.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StudentsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index(string? search, string? sortType, string? sortOrder, int pageSize = 5, int pageNumber = 1)
        {
            IQueryable<Student> stds = _context.Students.AsQueryable();

            // For Searching
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                stds = _context.Students.Where(e => e.FullName.Contains(search));
            }

            // For Sorting
            if (!string.IsNullOrWhiteSpace(sortType) && !string.IsNullOrWhiteSpace(sortOrder))
            {
                if (sortType == "FullName")
                {
                    if (sortOrder == "asc")
                        stds = stds.OrderBy(e => e.FullName);
                    else if (sortOrder == "desc")
                        stds = stds.OrderByDescending(e => e.FullName);
                }
            }

            ViewBag.CurrentSearch = search;

            stds = stds.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            ViewBag.pageSize = pageSize;
            ViewBag.pageNumber = pageNumber;

            return View(stds.ToList());
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Student student = _context.Students.Include(s => s.Specialization).FirstOrDefault(Item => Item.Id == id);
            if (student == null)
                return NotFound();
            return View(student);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.AllSpecialization = _context.Specializations.ToList(); // For Selection Specialization When creating new Student
            return View();
        }

        [ValidateAntiForgeryToken] // To Prevent CSRF attacks
        [HttpPost] // It's necessary to define the method
        // [Bind()] => prevents over-posting attacks
        public IActionResult AddStudent([Bind("Id,FullName,Email,ConfirmEmail,Password,ConfirmPassword," +
            "BirthDate,CreatedAt,LastUpdateAt,SpecializationId,ImageUrl")] Student student,
            IFormFile? imageFile)
        {
            if (ModelState.IsValid == true)
            {
                // For Image
                if (imageFile == null)
                {
                    student.ImageUrl = "\\images\\No_Image.png";
                }
                else
                {
                    string imgUrl = "\\images\\" + Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                    student.ImageUrl = imgUrl;

                    string imgPath = _webHostEnvironment.WebRootPath + imgUrl;

                    FileStream imgStream = new FileStream(imgPath, FileMode.Create);
                    imageFile.CopyTo(imgStream);
                    imgStream.Dispose();
                }
                student.CreatedAt = DateTime.Now;

                _context.Students.Add(student);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.AllSpecializations = _context.Specializations.ToList();
                return View("Create");
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Student student = _context.Students.FirstOrDefault(Item => Item.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            ViewBag.AllSpecializations = _context.Specializations.ToList();
            return View(student);
        }

        [ValidateAntiForgeryToken] // To Prevent CSRF attacks
        [HttpPost] // It's necessary to define the method
                   // [Bind()] => prevents over-posting attacks
        public IActionResult EditStudent(int id, [Bind("Id,FullName,Email,ConfirmEmail,Password,ConfirmPassword," +
            "BirthDate,CreatedAt,LastUpdateAt,SpecializationId,ImageUrl")] Student student,
            IFormFile? imageFile)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid == true)
            {
                // For Image
                if (imageFile != null)
                {
                    if (student.ImageUrl != "\\images\\No_Image.png")
                    {
                        string oldImgPath = _webHostEnvironment.WebRootPath + student.ImageUrl;
                        if (System.IO.File.Exists(oldImgPath))
                            System.IO.File.Delete(oldImgPath);
                    }
                    string imgUrl = "\\images\\" + Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                    student.ImageUrl = imgUrl;

                    string imgPath = _webHostEnvironment.WebRootPath + imgUrl;
                    FileStream imgStream = new FileStream(imgPath, FileMode.Create);
                    imageFile.CopyTo(imgStream);
                    imgStream.Dispose();
                }
                student.LastUpdateAt = DateTime.Now;

                _context.Students.Update(student);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.AllSpecializations = _context.Specializations.ToList();
                return View("Edit", student);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Student student = _context.Students.Include(s => s.Specialization).FirstOrDefault(Item => Item.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost] // It's necessary to define the method
        public IActionResult DeleteStudent(int id)
        {
            Student student = _context.Students.FirstOrDefault(Item => Item.Id == id);

            if (student.ImageUrl != "\\images\\No_Image.png")
            {
                string imgPath = _webHostEnvironment.WebRootPath + student.ImageUrl;
                System.IO.File.Delete(imgPath);
            }
            _context.Students.Remove(student);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
