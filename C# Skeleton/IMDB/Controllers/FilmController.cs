﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using IMDB.Models;

namespace IMDB.Controllers
{
    [ValidateInput(false)]
    public class FilmController : Controller
    {
        [HttpGet]
        [Route("")]
        public ActionResult Index()
        {
            using (var db = new IMDBDbContext())
            {
                List<Film> films = db.Films.ToList();
                return View(films);
            }
        }

        [HttpGet]
        [Route("create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Film film)
        {
            using (var db = new IMDBDbContext())
            {
                db.Films.Add(film);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Film");
        }

        [HttpGet]
        [Route("edit/{id}")]
        public ActionResult Edit(int? id)
        {
            using (var db = new IMDBDbContext())
            {
                Film film = db.Films.Find(id);
                return View(film);
            }
        }

        [HttpPost]
        [Route("edit/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirm(int? id, Film filmModel)
        {
            using (var db = new IMDBDbContext())
            {
                Film film = db.Films.Find(id);
                film.Genre = filmModel.Genre;
                film.Director = filmModel.Director;
                film.Name = filmModel.Name;
                film.Year = filmModel.Year;
                db.SaveChanges();
                return RedirectToAction("Index", "Film");
            }
        }

        [HttpGet]
        [Route("delete/{id}")]
        public ActionResult Delete(int? id)
        {
            using (var db = new IMDBDbContext())
            {
                Film film = db.Films.Find(id);
                return View(film);
            }
        }

        [HttpPost]
        [Route("delete/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int? id, Film filmModel)
        {
            using (var db = new IMDBDbContext())
            {
                Film film = db.Films.Find(id);
                db.Films.Remove(film);
                db.SaveChanges();
                return RedirectToAction("Index", "Film");
            }
        }
    }
}