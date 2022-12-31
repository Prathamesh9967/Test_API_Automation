using API_Automation;
using API_Automation.Models.Request;
using API_Automation.Models.Response;
using API_Automation.Utility;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Tests.StepDefinition
{
    [Binding]
    public class CreateUserSteps
    {
        private CreateUserReq createUserReq;
        private RestResponse response;
        private ScenarioContext scenarioContext;
        private HttpStatusCode statusCode;

        public CreateUserSteps(CreateUserReq createUserReq , ScenarioContext scenarioContext)
        {
            this.createUserReq = createUserReq;
            this.scenarioContext = scenarioContext;
        }

        [Given(@"user with name ""([^""]*)""")]
        public void GivenUserWithName(string name)
        {
            createUserReq.name = name;   
        }

        [Given(@"user with job ""([^""]*)""")]
        public void GivenUserWithJob(string job)
        {
            createUserReq.job = job;
        }

        [When(@"send req to create user")]
        public async Task WhenSendReqToCreateUser()
        {
            var api = new APIClient();
            response = await api.CreateUser<CreateUserReq>(createUserReq);
        }

        [Then(@"validate user is created")]
        public void ThenValidateUserIsCreated()
        {
            statusCode = response.StatusCode;
            var code = (int)statusCode;
            Assert.AreEqual(201, code);

            var content = HandleContent.GetContent<CreateUserRes>(response);
            Assert.AreEqual(createUserReq.name, content.name);
            Assert.AreEqual(createUserReq.job, content.job);
        }

    }
}
