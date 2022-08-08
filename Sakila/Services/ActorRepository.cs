﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Sakila.Models;
using System.Web;
using Z.EntityFramework.Plus;

namespace Sakila.Services
{
    public class ActorRepository
    {
        sakilaContext context = new sakilaContext();
        public Dictionary<string, List<string>> GetAllActors()
        {
            Dictionary<string, List<string>> actorDict = new Dictionary<string, List<string>>();
            List<string> ids = context.Actors.Select(a => a.ActorId.ToString()).ToList();
            List<string> fnames =  context.Actors.Select(a => a.FirstName).ToList();
            List<string> lnames = context.Actors.Select(a => a.LastName).ToList();
            actorDict.Add("ids", ids);
            actorDict.Add("firstNames", fnames);
            actorDict.Add("lastNames", lnames);

            return actorDict;
        }


        public IQueryable GetActorNameViaId(short id){
            IQueryable actor = context.Actors.Where(a => a.ActorId.Equals(id)).Select(a => new{a.FirstName, a.LastName}).AsQueryable();
            return actor;
        }


        public IQueryable GetActorNameViaFirstName(string firstName)
        {

            IQueryable actors;
            actors = context.Actors.Select(a => new
              {
                a.FirstName,
                a.LastName
            }).ToList().AsQueryable();

            
            return actors;
        }

        public async Task AddActorAsync(short ActorId, string FirstName, string LastName)
        {
            if (!context.Actors.Any(i => i.ActorId == ActorId))
            {
                context.Actors.Add(new Actor()
                {
                    ActorId = ActorId,
                    FirstName = FirstName,
                    LastName = LastName
                });

                var existingItem = context.Actors.FirstOrDefault(i => i.ActorId == ActorId);
                await context.SaveChangesAsync();
            }

        }


        public void UpdateActorById(short ActorId, string firstName, string lastName) {
            Actor actor = context.Actors.Where(a => a.ActorId.Equals((short)ActorId)).First();
            actor.FirstName = firstName;
            actor.LastName = lastName;
            context.SaveChanges();
        }

        public void DeleteAsync(int ActorId)
        {

            var actor = context.Actors.Where(s => s.ActorId.Equals((short) ActorId)).Delete();
            context.SaveChanges();
          
        }

    }
}
