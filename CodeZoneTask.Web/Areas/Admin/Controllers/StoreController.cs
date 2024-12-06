using CodeZoneTask.DataAccess;
using CodeZoneTask.Entities.Models;
using CodeZoneTask.Entities.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CodeZoneTask.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StoreController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public StoreController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(int page = 1)
        {
            int pageSize = 7;
            var totalItems = _unitOfWork.Store.GetAll().Count();
            var stores = _unitOfWork.Store.GetAll()
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
                _unitOfWork.Store.Add(store);
                _unitOfWork.Complete();
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
            var StoresInDb = _unitOfWork.Store.GetById(id.Value);
            return View(StoresInDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Store store)
        {
            //ServerSide Validation
            if (ModelState.IsValid)
            {
                _unitOfWork.Store.Update(store);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            return View(store);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var storesInDb = _unitOfWork.Store.GetById(id.Value);
            if (storesInDb == null)
            {
                return NotFound();
            }

            return View(storesInDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteStore(Store store)
        {
            if (store == null || store.Id == 0)
            {
                return NotFound();
            }

            var storesInDb = _unitOfWork.Store.GetById(store.Id);
            if (storesInDb == null)
            {
                return NotFound();
            }

            _unitOfWork.Store.Remove(storesInDb);
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }
    }

}

