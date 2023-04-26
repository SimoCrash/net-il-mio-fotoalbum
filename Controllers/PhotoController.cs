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
                Categories = ctx.Categories.Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToArray(),
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
                form.Categories = ctx.Categories.Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToArray();
            }

            form.Photo.Categories = form.SelectedCategories.Select(sc => ctx.Categories.First(c => c.Id == Convert.ToInt32(sc))).ToList();

            ctx.Photos.Add(form.Photo);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
        
        public IActionResult Update(int id)
        {
            using var ctx = new PhotoContext();

            var photo = ctx.Photos.Include(p => p.Categories).SingleOrDefault(p => p.Id == id);

            if (photo == null)
            {
                return View($"Non è stato trovato l'id {id}");
            }

            var formModel = new PhotoFormModel
            {
                Photo = photo,
                Categories = ctx.Categories.ToArray().Select(c => new SelectListItem(c.Name, c.Id.ToString(), photo.Categories!.Any(_c => _c.Id == c.Id))).ToArray(),
            };
            
            formModel.SelectedCategories = formModel.Categories.Where(sc => sc.Selected).Select(sc => sc.Value).ToList();

            return View(formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PhotoFormModel form)
        {
            using var ctx = new PhotoContext();

            if (!ModelState.IsValid)
            {
                form.Categories = ctx.Categories.Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToArray();

                return View(form);
            }

            var PhotoToUpdate = ctx.Photos.Include(p => p.Categories).SingleOrDefault(i => i.Id == id);

            if (PhotoToUpdate == null)
            {
                return View($"Non è stato trovato l'id {id}");
            }

            PhotoToUpdate.Title = form.Photo.Title;
            PhotoToUpdate.Description = form.Photo.Description;
            PhotoToUpdate.ImageUrl = form.Photo.ImageUrl;
            PhotoToUpdate.Visible = form.Photo.Visible;
            PhotoToUpdate.CategoriesId = form.Photo.CategoriesId;
            PhotoToUpdate.Categories = form.SelectedCategories.Select(sc => ctx.Categories.First(c => c.Id == Convert.ToInt32(sc))).ToList();

            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            using var ctx = new PhotoContext();
            var PhotoToDelete = ctx.Photos.FirstOrDefault(p => p.Id == id);

            if (PhotoToDelete is null)
            {
                return View($"Non è stato trovato l'id {id}");
            }

            ctx.Photos.Remove(PhotoToDelete);

            ctx.SaveChanges();

            return RedirectToAction("Index");

        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
