using CodeZoneTask.Web.Data;
using CodeZoneTask.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeZoneTask.Web.Controllers
{
    public class StoreController : Controller
    {
        private readonly ApplicationDbContext _context;
        public StoreController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            int pageSize = 7;  
            var totalItems = _context.Stores.Count();  
            var stores = _context.Stores
                .Skip((page - 1) * pageSize)  
                .Take(pageSize) 
                .ToList();

            ViewBag.Page = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            return View(stores);
        }




        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]   // Cross side Forgery Attacks
        public IActionResult Create(Store store)
        {
            //ServerSide Validation
            if (ModelState.IsValid)
            {
                _context.Stores.Add(store);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(store);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null | id == 0)
            {
                NotFound();
            }
            var StoresInDb = _context.Stores.Find(id);
            return View(StoresInDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Store store)
        {
            //ServerSide Validation
            if (ModelState.IsValid)
            {
                _context.Stores.Update(store);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(store);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null | id == 0)
            {
                NotFound();
            }
            var StoresInDb = _context.Stores.Find(id);
            return View(StoresInDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteStore(int? id)
        {
            var StoreInDb= _context.Stores.Find(id);
            if (StoreInDb == null)
            {
                NotFound();
            }
            _context.Stores.Remove(StoreInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
