using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDbMovies.Repositories;

namespace MongoDbMovies.Controllers
{
    public class LoremPicsumController : Controller
    {
        ILoremPicsumService _loremPicsumService;
        public LoremPicsumController(ILoremPicsumService loremPicsumService)
        {
            this._loremPicsumService = loremPicsumService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetImage(IFormCollection formcollection)
        {
            int width = Convert.ToInt32(formcollection["txt_width"]);
            int height = Convert.ToInt32(formcollection["txt_height"]);
            var randomImageUrl = _loremPicsumService.GetRandomImage(width, height);

            return View("Index", randomImageUrl);
        }
    }
}