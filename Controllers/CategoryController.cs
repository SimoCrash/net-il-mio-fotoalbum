using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;
using System.Data;

namespace net_il_mio_fotoalbum.Controllers
{
    public class CategoryController : Controller
    {
        [Authorize(Roles = "ADMIN, USER")]
        public IActionResult Index()
        {
            using var ctx = new PhotoContext();
            var categories = ctx.Categories.Include(c => c.Photos).ToArray();   
            return View(categories);
        }

        [Authorize(Roles = "ADMIN")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            using var ctx = new PhotoContext();
            ctx.Categories.Add(category);
            ctx.SaveChanges();

            return Redirect("Index");

        }

        [Authorize(Roles = "ADMIN")]
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
