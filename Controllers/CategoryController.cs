using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            using var ctx = new PhotoContext();
            var categories = ctx.Categories.Include(c => c.Photos).ToArray();   
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            using var ctx = new PhotoContext();
            ctx.Categories.Add(category);
            ctx.SaveChanges();

            return Redirect("Index");

        }

        public IActionResult Delete(int id)
        {
            using var ctx = new PhotoContext();
            var CategoryToDelete = ctx.Categories.FirstOrDefault(c => c.Id == id);

            if (CategoryToDelete == null)
            {
                return View($"Non è stato trovato l'id {id}");
            }

            ctx.Categories.Remove(CategoryToDelete);

            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
