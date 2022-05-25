using HrAppWeb.Data;
using HrAppWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace HrAppWeb.Controllers
{
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PersonController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Person> objPersonList = _db.People;
            return View(objPersonList);
        }
        //GET
        public IActionResult Add()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Person person)
        {
            if(person.Name == person.DisplayOrder.ToString() || person.Surname == person.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the name or surname");
            }
            if (ModelState.IsValid)
            {
                _db.People.Add(person);
                _db.SaveChanges();
                TempData["success"] = "Person added successfully";
                return RedirectToAction("Index");
            }
            return View(person);
        }
        //GET
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var personFromDb = _db.People.Find(id);
            //var personFromDbFirst = _db.People.FirstOrDefault(x => x.Id == id);
            //var personFromDbSingle = _db.People.SingleOrDefault(x => x.Id == id);

            if(personFromDb == null)
            {
                return NotFound();
            }

            return View(personFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Person person)
        {
            if (person.Name == person.DisplayOrder.ToString() || person.Surname == person.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the name or surname");
            }
            if (ModelState.IsValid)
            {
                _db.People.Update(person);
                _db.SaveChanges();
                TempData["success"] = "Person updated successfully";
                return RedirectToAction("Index");
            }
            return View(person);
        }
        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var personFromDb = _db.People.Find(id);

            if (personFromDb == null)
            {
                return NotFound();
            }

            return View(personFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Person person)
        {
            _db.People.Remove(person);
            _db.SaveChanges();
            TempData["success"] = "Person deleted successfully";
            return RedirectToAction("Index");
        }
    }
}