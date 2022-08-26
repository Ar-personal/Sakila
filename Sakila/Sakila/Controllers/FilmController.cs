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

        [HttpPut("/updateFilm/{Title=string}/{Description=string}/{ReleaseYear:int}/{LanguageId=string}/{RentalDuration=string}/" +
            "{RentalRate=decimal}/{Length:int}/{ReplacementCost=decimal}/{Rating=string}")]
        public void UpdateFilm(string Title, string Description, short ReleaseYear, byte LanguageId,
            byte RentalDuration, decimal RentalRate, short Length, decimal ReplacementCost, string Rating) {
            Film film = context.Films.Where(a => a.Title.Equals(Title)).First();
            film.Title = Title;
            film.Description = Description;
            film.ReleaseYear = ReleaseYear;
            film.LanguageId = LanguageId;
            film.RentalDuration = RentalDuration;
            film.RentalRate = RentalRate;
            film.Length = Length;
            film.ReplacementCost = ReplacementCost;
            film.Rating = Rating;
            context.SaveChanges();
        }
    }
}
