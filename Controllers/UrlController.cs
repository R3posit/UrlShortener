using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using UrlShorterer.Models;
using UrlShorterer.Data;

    
namespace UrlShorterer.Controllers
{
        [ApiController]
        [Route("api/[controller]")] // kerem.com/api/url
 
        public class UrlController : ControllerBase
        {
            private readonly AppDbContext _context;
            public UrlController(AppDbContext context)
            {
                _context = context;
            }

            [HttpPost]

            public IActionResult Create(UrlModel model)
            {

                if (urlChecker(model.LongUrl ?? ""))
                {
                     model.ShortUrl = Guid.NewGuid().ToString().Substring(0, 6);
                        
                     _context.Urls.Add(model);
                     _context.SaveChanges();

                return Ok(model);
                } else
                {
                    return BadRequest("bye bye");
                
                }

        }

            [HttpGet("{shortUrl}")]
            public IActionResult RedirectUrl(string shortUrl)
            {
                var urlModel = _context.Urls.FirstOrDefault(x => x.ShortUrl == shortUrl);

                if (urlModel == null)
                {
                    return NotFound("Link bulunamadı!");
                }

                urlModel.ClickCount++;
                _context.SaveChanges();
                
                var replacedUrl = new Uri(urlModel.LongUrl).AbsoluteUri;

                return Redirect(replacedUrl);
            }

            [HttpGet("GetAll")]
            public IActionResult GelAllUrls()
            {
                var allUrls = _context.Urls.ToList();

                return Ok(allUrls);
            }

            private bool urlChecker(string url)
            {
                if (string.IsNullOrWhiteSpace(url))
                {
                    return false;
                }

             bool sonuc = Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            return sonuc;
            }
        }
}
