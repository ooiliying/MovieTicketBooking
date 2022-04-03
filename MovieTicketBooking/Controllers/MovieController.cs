﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FrontEnd.Models;

namespace FrontEnd.Controllers {
    public class MovieController : Controller {
        //private MovieTicketBookingContext dbContext = new MovieTicketBookingContext();
        private readonly MovieTicketBookingContext dbContext;

        public MovieController( MovieTicketBookingContext dbContext ) {
            this.dbContext = dbContext;
        }

        public ActionResult Index( ) {
            //string query = "select * from Movies";


            var q = dbContext.Movies.AsEnumerable();

            return View(q);
        }

        //public ActionResult Index( string movieGenre, string searchString ) {
        //    var GenreLst = new List<string>();

        //    var GenreQry = from d in dbContext.Movies
        //                   orderby d.Genre
        //                   select d.Genre;

        //    GenreLst.AddRange( GenreQry.Distinct() );
        //    ViewBag.movieGenre = new SelectList( GenreLst );

        //    var movies = from m in dbContext.Movies
        //                 select m;

        //    if ( !String.IsNullOrEmpty( searchString ) ) {
        //        movies = movies.Where( s => s.Title.Contains( searchString ) );
        //    }

        //    if ( !string.IsNullOrEmpty( movieGenre ) ) {
        //        movies = movies.Where( x => x.Genre == movieGenre );
        //    }

        //    return View( movies );
        //}

        //// GET: Movies/Details/5
        //public ActionResult Details( int? id ) {
        //    if ( id == null ) {
        //        return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
        //    }
        //    Movie movie = db.Movies.Find( id );
        //    if ( movie == null ) {
        //        return HttpNotFound();
        //    }
        //    return View( movie );
        //}

        //// GET: Movies/Create
        //public ActionResult Create() {
        //    return View();
        //}

        //// POST: Movies/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create( [Bind( Include = "ID,Title,ReleaseDate,Genre,Price,Rating" )] Movie movie ) {
        //    if ( ModelState.IsValid ) {
        //        db.Movies.Add( movie );
        //        db.SaveChanges();
        //        return RedirectToAction( "Index" );
        //    }

        //    return View( movie );
        //}

        //// GET: Movies/Edit/5
        //public ActionResult Edit( int? id ) {
        //    if ( id == null ) {
        //        return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
        //    }
        //    Movie movie = db.Movies.Find( id );
        //    if ( movie == null ) {
        //        return HttpNotFound();
        //    }
        //    return View( movie );
        //}

        //// POST: Movies/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit( [Bind( Include = "ID,Title,ReleaseDate,Genre,Price,Rating" )] Movie movie ) {
        //    if ( ModelState.IsValid ) {
        //        db.Entry( movie ).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction( "Index" );
        //    }
        //    return View( movie );
        //}

        //// GET: Movies/Delete/5
        //public ActionResult Delete( int? id ) {
        //    if ( id == null ) {
        //        return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
        //    }
        //    Movie movie = db.Movies.Find( id );
        //    if ( movie == null ) {
        //        return HttpNotFound();
        //    }
        //    return View( movie );
        //}

        //// POST: Movies/Delete/5
        //[HttpPost, ActionName( "Delete" )]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed( int id ) {
        //    Movie movie = db.Movies.Find( id );
        //    db.Movies.Remove( movie );
        //    db.SaveChanges();
        //    return RedirectToAction( "Index" );
        //}

        protected override void Dispose( bool disposing ) {
            if ( disposing ) {
                dbContext.Dispose();
            }
            base.Dispose( disposing );
        }
    }
}
