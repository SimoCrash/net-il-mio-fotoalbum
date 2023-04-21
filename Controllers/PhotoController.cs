using Microsoft.AspNetCore.Mvc;

namespace net_il_mio_fotoalbum.Controllers
{
    public class PhotoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
