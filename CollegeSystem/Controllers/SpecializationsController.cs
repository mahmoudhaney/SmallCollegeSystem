using CollegeSystem.Data;
using CollegeSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeSystem.Controllers
{
    public class SpecializationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SpecializationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_context.Specializations);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Specialization specialization = _context.Specializations.Include(s => s.Students).FirstOrDefault(Item => Item.Id == id);
            if (specialization == null)
                return NotFound();
            return View(specialization);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken] // To Prevent CSRF attacks
        [HttpPost] // It's necessary to define the method
        // [Bind()] => prevents over-posting attacks
        public IActionResult AddSpecialization([Bind("Id,Name,Description")] Specialization specialization)
        {
            if (ModelState.IsValid == true)
            {
                _context.Specializations.Add(specialization);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("Create");
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Specialization specialization = _context.Specializations.FirstOrDefault(Item => Item.Id == id);
            if (specialization == null)
            {
                return NotFound();
            }
            return View(specialization);
        }

        [ValidateAntiForgeryToken] // To Prevent CSRF attacks
        [HttpPost] // It's necessary to define the method
        // [Bind()] => prevents over-posting attacks
        public IActionResult EditSpecialization(int id, [Bind("Id,Name,Description")] Specialization specialization)
        {
            if (id != specialization.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                _context.Specializations.Update(specialization);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("Edit");
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Specialization specialization = _context.Specializations.FirstOrDefault(Item => Item.Id == id);
            if (specialization == null)
            {
                return NotFound();
            }
            return View(specialization);
        }

        [HttpPost] // It's necessary to define the method
        public IActionResult DeleteSpecialization(int id)
        {
            Specialization specialization = _context.Specializations.FirstOrDefault(Item => Item.Id == id);
            _context.Specializations.Remove(specialization);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
