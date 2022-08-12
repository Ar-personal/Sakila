using Microsoft.AspNetCore.Mvc;
using Sakila.Models;
using Sakila.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sakila.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActorController : ControllerBase
    {
        private ActorRepository actorRepository;
        public int actorId = -1;

        public ActorController() {
            this.actorRepository = new ActorRepository();
        }

        [HttpGet("/getAllActors")]
        public IQueryable GetAllActors()
        {
            return actorRepository.GetAllActors();
        }

        [HttpGet("/getActorByFirstName/{firstName=string}")]
        public IQueryable GetActorByFirstName(string firstName)
        {
            return actorRepository.GetActorNameByFirstName(firstName);
        }


        [HttpGet("/getActorById/{ActorId:int}")]
        public IQueryable GetActorByID(int ActorId)
        {
            actorId = ActorId;
            return actorRepository.GetActorNameViaId((short) ActorId);
        }

        [HttpPut("/putActor/{FirstName=string}/{LastName=string}")]
        public void Put(string FirstName, string LastName) {
            actorRepository.AddActorAsync(FirstName, LastName);
        }





        [HttpPut("/updateActorById/{ActorId:int}/{firstName=string}/{lastName=string}")]
        public void UpdateActorById(int ActorId, string firstName, string lastName) {
            actorRepository.UpdateActorById((short) ActorId, firstName, lastName);
        }


        [HttpPut("/UpdateFilmByActorId/{ActorId:int}/{filmTitle=string}/{filmDesc=string}/{filmRating=string}")]
        public void UpdateFilmByActorId(int ActorId, string filmTitle, string filmDesc, string filmRating)
        {
            actorRepository.UpdateActorById((short)ActorId, filmTitle, filmDesc, filmRating);
        }

        [HttpGet("/getActorFilmsById/{ActorId:int}")]
        public void getActorFilmsById(int ActorId) {
            actorRepository.GetActorFilmsById((short)ActorId);
        }

        [HttpDelete("/DeleteActorById/{ActorId:int}")]
        public void DeleteActorById(int ActorId) {
            actorRepository.DeleteAsync((short)ActorId);
        }


    }
}
