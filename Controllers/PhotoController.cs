using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
