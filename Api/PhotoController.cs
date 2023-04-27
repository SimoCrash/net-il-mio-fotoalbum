using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Models;
using System.Reflection;

namespace net_il_mio_fotoalbum.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPhotos([FromQuery] string? title )
        {
            using var ctx = new PhotoContext();

            var photos = ctx.Photos.Where(p => title == null || p.Title.ToLower().Contains(title.ToLower())).ToList();
            return Ok(photos);
        }

        [HttpGet("{id}")]
        public IActionResult GetPhoto(int id)
        {
            using var ctx = new PhotoContext();

            var photo = ctx.Photos.FirstOrDefault(p => p.Id == id);
            if (photo is null)
            {
                return NotFound();
            }
             
            return Ok(photo);
        }
    }
}
