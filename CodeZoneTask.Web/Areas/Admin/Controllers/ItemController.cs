using CodeZoneTask.DataAccess;
using CodeZoneTask.Entities.Models;
using CodeZoneTask.Entities.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CodeZoneTask.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ItemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ItemController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(int page = 1)
        {
            int pageSize = 7;
            var totalItems = _unitOfWork.Item.GetAll().Count();
            var items = _unitOfWork.Item.GetAll()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.Page = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            return View(items);
        }




        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]   // Cross side Forgery Attacks
        public IActionResult Create(Item item)
        {
            //ServerSide Validation
            if (ModelState.IsValid)
            {
                _unitOfWork.Item.Add(item);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null | id == 0)
            {
                NotFound();
            }
            var itemsInDb = _unitOfWork.Item.GetById(id.Value);
            return View(itemsInDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Item item)
        {
            //ServerSide Validation
            if (ModelState.IsValid)
            {
                _unitOfWork.Item.Update(item);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var itemsInDb = _unitOfWork.Item.GetById(id.Value);
            if (itemsInDb == null)
            {
                return NotFound();
            }

            return View(itemsInDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteItem(Item item)
        {
            if (item == null || item.Id == 0)
            {
                return NotFound();
            }

            var itemsInDb = _unitOfWork.Item.GetById(item.Id);
            if (itemsInDb == null)
            {
                return NotFound();
            }

            _unitOfWork.Item.Remove(itemsInDb);
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }
    }

}

