using Microsoft.AspNetCore.Mvc;
using Sakila.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sakila.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmController : ControllerBase
    {
        sakilaContext context = new sakilaContext();
        public int filmId = -1;
        [HttpGet("/getFilmById/{FilmId:int}")]
        public IQueryable GetFilmByID(short FilmId)
        {
            IQueryable film = context.Films.Where(a => a.FilmId.Equals(FilmId)).Select(a =>
            new { a.Title,
                  a.Description,
                  a.ReleaseYear,
                  a.LanguageId,
                  a.RentalDuration,
                  a.RentalRate,
                  a.Length,
                  a.ReplacementCost,
                  a.Rating,
                  a.FilmCategories,
                  a.Inventories
                   }).ToList().AsQueryable();
            return film;
        }


        [HttpGet("/getFilmByTitle/{FilmTitle=string}")]
        public IQueryable GetFilmByTitle(string FilmTitle)
        {
            IQueryable film = context.Films.Where(a => a.Title.Equals(FilmTitle)).Select(a =>
            new {
                a.Title,
                a.Description,
                a.ReleaseYear,
                a.LanguageId,
                a.RentalDuration,
                a.RentalRate,
                a.Length,
                a.ReplacementCost,
                a.Rating,
                a.FilmCategories,
                a.Inventories
            }).ToList().AsQueryable();
            return film;
        }
    }
}
