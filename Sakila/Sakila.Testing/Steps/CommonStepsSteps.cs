using System;
using System.Net.Http;
using TechTalk.SpecFlow;

namespace Sakila.Testing.Features
{
    [Binding]
    public class CommonStepsSteps
    {

        private HttpClient _client { get; set; }
        protected HttpResponseMessage Response { get; set; }
        private readonly IScenarioContext _scenarioContext;
        private string reasonPhrase;

        public CommonStepsSteps(HttpClient httpClient)
        {
            _client = httpClient;
        }

        [Then(@"the response status code is x")]
        public void ThenTheResponseStatusCodeIsX(int expectedStatusCode)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
