using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Sakila.Models;
using System.Web;
using Z.EntityFramework.Plus;
using LinqKit;

namespace Sakila.Services
{
    public class ActorRepository
    {
        sakilaContext context = new sakilaContext();
        public IQueryable GetAllActors()
        {

            IQueryable actors;
            actors = context.Actors.Select(a => new {
                a.ActorId,
                a.FirstName,
                a.LastName
            }).ToList().AsQueryable();

            return actors;
        }


        public IQueryable GetActorNameViaId(short id){
            IQueryable actor = context.Actors.Where(a => a.ActorId.Equals(id)).Select(a => new{a.FirstName, a.LastName, a.FilmActors}).AsQueryable();
            return actor;
        }


        public IQueryable GetActorNameByFirstName(string firstName)
        {

            IQueryable actors;
            actors = context.Actors.Where(a => a.FirstName.Equals(firstName)).Select(a => new{
                a.ActorId,
                a.FirstName,
                a.LastName
            }).ToList().AsQueryable();

            
            return actors;
        }

        public async Task AddActorAsync(string FirstName, string LastName)
        {

                context.Actors.Add(new Actor()
                {
                    FirstName = FirstName,
                    LastName = LastName
                });

                await context.SaveChangesAsync();
        }


        public void UpdateActorById(short ActorId, string firstName, string lastName) {
            Actor actor = context.Actors.Where(a => a.ActorId.Equals((short)ActorId)).First();
            actor.FirstName = firstName;
            actor.LastName = lastName;
            context.SaveChanges();
        }

        public object GetActorFilmsById(short ActorId) {
            
            object anonObj()
            {
                return new
                {
                    actorFilms = (from actor in context.Actors
                                  join filmactor in context.FilmActors
                                  on actor.ActorId equals filmactor.ActorId
                                  join film in context.Films
                                  on filmactor.ActorId equals film.FilmId
                                  where (ActorId == film.FilmId)
                                  select new
                                  {
                                      actorFirstName = actor.FirstName,
                                      actorLastName = actor.LastName,
                                      filmTitle = film.Title,
                                      filmDesc = film.Description,
                                      filmRating = film.Rating
                                  }).ToList()
                };
            }

            dynamic newtype = anonObj();
            return newtype;
        }


        public void UpdateFilmByActorId(short ActorId, string filmTitle, string filmDesc, string filmRating)
        {
            var ft = filmTitle;

            IQueryable editable;
            object ob()
            {
                return new
                {
                    editable = (from actor in context.Actors
                                join filmactor in context.FilmActors
                                on actor.ActorId equals filmactor.ActorId
                                join film in context.Films
                                on filmactor.ActorId equals film.FilmId
                                where (ActorId == film.FilmId)
                                select new {
                                     title = film.Title
                                
                                }).AsQueryable()
                };


            }
        }

        public void DeleteAsync(short ActorId)
        {

            var actor = context.Actors.Where(s => s.ActorId.Equals((short) ActorId)).Delete();
            context.SaveChanges();
          
        }

    }
}
