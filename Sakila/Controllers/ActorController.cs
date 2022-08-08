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
        public Dictionary<string, List<string>> GetAllActors()
        {
            return actorRepository.GetAllActors();
        }

        [HttpGet("/getActorByFirstName/{firstName=string}")]
        public IQueryable GetActorByFirstName(string firstName)
        {
            return actorRepository.GetActorNameViaFirstName(firstName);
        }


        [HttpGet("/getActorById/{ActorId:int}")]
        public IQueryable GetActorByID(int ActorId)
        {
            actorId = ActorId;
            return actorRepository.GetActorNameViaId((short) ActorId);
        }

        [HttpPut("/putActor/{ActorId:int}/{FirstName=string}/{LastName=string}")]
        public void Put(int ActorId, string FirstName, string LastName) {
            actorRepository.AddActorAsync((short) ActorId, FirstName, LastName);
        }


        [HttpPatch("/updateActorById/{ActorId:int}/{firstName=string}/{lastName=string}")]
        public void UpdateActorById(int ActorId, string firstName, string lastName) {
            actorRepository.UpdateActorById((short) ActorId, firstName, lastName);
        }

        [HttpDelete("/DeleteActorById/{ActorId:int}")]
        public void DeleteActorById(int ActorId) {
            actorRepository.DeleteAsync((short)ActorId);
        }


    }
}
