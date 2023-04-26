using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers
{
    public class PhotoController : Controller
    {
        public IActionResult Index()
        {
            using var ctx = new PhotoContext();
            var photos = ctx.Photos.Include(p => p.Categories).ToArray();
            return View(photos);
        }

        public IActionResult Detail(int id)
        {
            using var ctx = new PhotoContext();
            var photo = ctx.Photos.Include(p => p.Categories).SingleOrDefault(p => p.Id == id);
            return View(photo);
        }

        public IActionResult Create()
        {
            using var ctx = new PhotoContext();
            var formModel = new PhotoFormModel
            {
                Categories = ctx.Categories.Select(i => new SelectListItem(i.Name, i.Id.ToString())).ToArray(),
            };

            return View(formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PhotoFormModel form)
        {
            using var ctx = new PhotoContext();

            if (!ModelState.IsValid)
            {
                form.Categories = ctx.Categories.Select(i => new SelectListItem(i.Name, i.Id.ToString())).ToArray();
            }

            form.Photo.Categories = form.SelectedCategories.Select(sc => ctx.Categories.First(c => c.Id == Convert.ToInt32(sc))).ToList();

            ctx.Photos.Add(form.Photo);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
        

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
