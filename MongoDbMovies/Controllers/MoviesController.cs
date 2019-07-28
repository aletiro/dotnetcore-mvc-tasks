using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDbMovies.Models;
using MongoDbMovies.Repositories;

namespace MongoDbMovies.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieRepository _movieRepository = null;
        public MoviesController(IMovieRepository movieRepository)
        {
            this._movieRepository = movieRepository;
        }

        // GET: Movies
        public async Task<ActionResult> Index()
        {
            var movies = await this._movieRepository.GetMovies();
            return View(movies);
        }

        // GET: Movies/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Movie movie)
        {
            try
            {
                if (ModelState.IsValid)
                    this._movieRepository.AddMovie(movie);
                else
                    return View(movie);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Movies/Edit/
        public async Task<ActionResult> Edit(string id)
        {
            var movie = await this._movieRepository.GetMovie(id);
            return View(nameof(Create), movie);
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, Movie movie)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var isUpdated = await this._movieRepository.UpdateMovie(movie);
                    if (!isUpdated)
                    {
                        ViewBag.ErrorMessage = "Could not update movie";
                        return View(nameof(Create), movie);
                    }
                    return RedirectToAction(nameof(Index));
                }
                else
                    return View(nameof(Create), movie);
            }
            catch
            {
                return View(nameof(Create));
            }
        }

        // GET: Movies/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var isDeleted = await this._movieRepository.RemoveMovie(id);
                if (!isDeleted)
                {
                    TempData["ErrorMessage"] = "Could not delete movie";
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Could not delete movie: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<ActionResult> Search(IFormCollection formCollection)
        {
            try
            {
                var search = formCollection["search"].ToString();
                if (!string.IsNullOrEmpty(search))
                {
                    var movies = await _movieRepository.SearchMovie(search);

                    return View(nameof(Index), movies);
                }
                else
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Search failed: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}