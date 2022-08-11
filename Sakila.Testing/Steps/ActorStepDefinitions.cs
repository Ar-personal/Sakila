using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Sakila.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using WireMock.Admin.Mappings;

namespace Sakila.Testing.Features
{
    [Binding]
    public class ActorStepDefinitions
    {

        private HttpClient _client { get; set; }
        protected HttpResponseMessage Response { get; set; }
        protected string lastName { get; set; }
        protected string firstName { get; set; }
        private readonly IScenarioContext _scenarioContext;
        private string reasonPhrase;
        private IEnumerable<Actor> model;
        private string responseString;
        private Actor example;

        public ActorStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I am a user interacting with the database api")]
        public void GivenIAmAUserInteractingWithTheDatabaseApi()
        {
            _client = new HttpClient();
        }



        [When(@"I make a get request to getactorbyid with (.*)")]
        public async Task WhenIMakeAGetRequestToGetactorbyidWith(string id)
        {
            var url = new Uri("https://sakila20220809143255.azurewebsites.net/getActorById/" + id);
            Response = await _client.GetAsync(url);
            responseString = await Response.Content.ReadAsStringAsync();
            reasonPhrase = Response.ReasonPhrase;
            model = JsonConvert.DeserializeObject<IEnumerable<Actor>>(responseString);
            example = model.ElementAt(0);
        }


        [Then(@"the response status code is ""(.*)""")]
        public void ThenTheResponseStatusCodeIs(int expected)
        {
            Assert.AreEqual(expected, (int)Response.StatusCode);
        }

        [Then(@"reasonPhrase is ""(.*)""")]
        public void ThenReasonPhraseIs(string expected)
        {
            Assert.AreEqual(expected, "OK");
        }


        //scenario 2

        [When(@"I make a put request to putactor with (.*) and (.*)")]
        public async Task WhenIMakeAPutRequestToPutactorWithAnd(string firstName, string lastName)
        {
            var url = "https://sakila20220809143255.azurewebsites.net/putActor/" + firstName + "/" + lastName;
            var data = new StringContent(url, Encoding.UTF8, "application/json");
            Response = await _client.PutAsync(url, data);
            responseString = await Response.Content.ReadAsStringAsync();
            reasonPhrase = Response.ReasonPhrase;
            model = JsonConvert.DeserializeObject<IEnumerable<Actor>>(responseString);
        }


        //scenario 3
        [When(@"I make a get request to GetActorByFirstName with (.*)")]
        public async Task WhenIMakeAGetRequestToGetActorByFirstNameWith(string firstName)
        {
            var url = new Uri("https://sakila20220809143255.azurewebsites.net/getActorByFirstName/" + firstName);
            Response = await _client.GetAsync(url);
            responseString = await Response.Content.ReadAsStringAsync();
            reasonPhrase = Response.ReasonPhrase;
            model = JsonConvert.DeserializeObject<IEnumerable<Actor>>(responseString);
        }


        [Then(@"all returned firstName are (.*)")]
        public void ThenAllReturnedFirstNameAre(string expected)
        {
            foreach (Actor x in model)
            {
                example = x;
                Assert.AreEqual(x.FirstName, expected);
            }
        }


        //scenario 4

        [When(@"I make a get request to UpdateActorByID with (.*) and (.*) and (.*)")]
        public async Task WhenIMakeAGetRequestToUpdateActorByIDWithAndAnd(string actorId, string firstName, string lastName)
        {
            var url = "https://sakila20220809143255.azurewebsites.net/updateActorById/" + actorId + "/" + firstName + "/" + lastName;
            var data = new StringContent(url, Encoding.UTF8, "application/json");
            Response = await _client.PutAsync(url, data);
            responseString = await Response.Content.ReadAsStringAsync();
            reasonPhrase = Response.ReasonPhrase;
            model = JsonConvert.DeserializeObject<IEnumerable<Actor>>(responseString);
        }

        [Then(@"actor details match (.*) and (.*)")]
        public void ThenActorDetailsMatchAnd(string expectedFirstName, string expectedLastName)
        {
            Actor actor = model.ElementAt(0);
            Assert.AreEqual(actor.FirstName, expectedFirstName);
            Assert.AreEqual(actor.LastName, expectedLastName);
        }



    }
}
