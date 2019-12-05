using MovieDirectory.Models;
using MovieDirectory.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieDirectory.Controllers
{
    [Authorize]
    public class MovieController : Controller
    {
        MovieDbContext db = new MovieDbContext();
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Movies.ToList());
        }
        public ActionResult Create()
        {
            Movie movie = new Movie();
            movie.GenreCollection = db.Genres.ToList<Genre>();
            return View(movie);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(Movie m)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(m);
        }
        public ActionResult Edit(int id)
        {
            var movie = db.Movies.FirstOrDefault(x => x.MovieId == id);
            movie.GenreCollection = db.Genres.ToList<Genre>();
            return View(movie);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(Movie m)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(m);
        }
        public ActionResult Delete(int id)
        {
            return View(db.Movies.FirstOrDefault(x => x.MovieId == id));
        }
        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            var movie = db.Movies.FirstOrDefault(x => x.MovieId == id);
            db.Entry(movie).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}