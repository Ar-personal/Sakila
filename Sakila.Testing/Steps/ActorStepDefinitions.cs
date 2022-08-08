using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Sakila.Testing.Features
{
    [Binding]
    public class ActorStepDefinitions
    {

        private HttpClient _client { get; set; }
        protected HttpResponseMessage Response { get; set; }
        private WebApplicationFactory<Startup> factory;

        public ActorStepDefinitions(WebApplicationFactory<Startup> factory)
        {
            // instance of webapplication factory provided through Dependency Injection
            this.factory = factory;
        }

        [Given(@"I am a user")]
        public void GivenIAmAUser()
        {
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri($"http://localhost:44380") // The base address of the test server is http://localhost
            });
        }


        [When(@"I make a post request to ""(.*)"" when id is (.*)")]
        public virtual async Task WhenIMakeAPostRequestToWhenIdIs(string resourceEndPoint, string id)
        {
            var postRelativeUri = new Uri(resourceEndPoint, UriKind.Relative);
            var content = new StringContent(id, Encoding.UTF8, "application/json");
            Response = await _client.PostAsync(postRelativeUri, content).ConfigureAwait(false);
        }

        
        [Then(@"the response status code is ""(.*)""")]
        public void ThenTheResponseStatusCodeIs(int expected)
        {
            var expectedStatusCode = (System.Net.HttpStatusCode)expected;
            Assert.AreEqual(expected, Response.StatusCode);
        }
        
        [Then(@"the response data should be")]
        public void ThenTheResponseDataShouldBe()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
