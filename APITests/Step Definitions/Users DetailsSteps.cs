using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharpDemo;
using RestSharpDemo.Models;
using RestSharpDemo.Models.Request;
using System;
using System.Configuration;
using System.Net;
using TechTalk.SpecFlow;


namespace APITests.Features
{
    [Binding]
    public class CreateUserSteps
    {
        
        private readonly CreateUserReq createUserReq;
        public HttpStatusCode statusCode;

        Demo api = new Demo();
        public static string GetApplicationUrl()
        {
            string applicationUrl = String.Empty;
            string appConfigApplicationURL = String.Empty;

            string getApplicationUrl = ConfigurationManager.AppSettings["BaseUrl"];
            // Get environment value from app config file
            string getEnvironment = ConfigurationManager.AppSettings["Environment"];
            if (getApplicationUrl != String.Empty)
            {
                appConfigApplicationURL = ConfigurationManager.AppSettings["BaseUrl"];
                applicationUrl = string.Format(appConfigApplicationURL, getEnvironment.ToLower());
            }
            else
            {
                throw new ArgumentException("The suggested application environment was not found");
            }

            return applicationUrl;
        }
        private  string BASE_URL = GetApplicationUrl();

        private RestResponse response;

        public CreateUserSteps(CreateUserReq createUserReq)
        {
            this.createUserReq = createUserReq;
        }

        [Given(@"I input ""(.*)"" of the user")]
        public void GivenIInputOfTheUser(string name)
        {
            createUserReq.name = name;

        }

        [When(@"I input ""(.*)"" of the user")]
        public void WhenIInputOfTheUser(string role)
        {
            createUserReq.job = role;

        }

        [Then(@"I should be displayed with response ""(.*)"" for CreateAddUserPostRequest")]
        public async System.Threading.Tasks.Task ThenIShouldBeDisplayedWithResponseForCreateAddUserPostRequest(int StatusCode)
        {
            
            var response = await api.CreateNewUser(BASE_URL, createUserReq);
            statusCode = response.StatusCode;
            var code = (int)statusCode;
            Assert.AreEqual(StatusCode, code);


        }



        [When(@"I send create user request")]
        public async System.Threading.Tasks.Task WhenISendCreateUserRequestAsync()
        {
            
            response = await api.CreateNewUser(BASE_URL, createUserReq);
        }

        [Then(@"validate user is created")]
        public void ThenValidateUserIsCreated()
        {
            var content = HandleContent.GetContent<CreateUserRes>(response);
            Assert.AreEqual(createUserReq.name, content.name);
            Assert.AreEqual(createUserReq.job, content.job);
        }

        [Given(@"I send a get users request")]
        public async System.Threading.Tasks.Task GivenISendAGetUsersRequest()
        {
            
            response = await api.GetUsers(BASE_URL);
        }
        [Then(@"I print the users")]
        public void ThenIPrintTheUsers()
        {
            var content = HandleContent.GetContent<Users>(response);
            for(int i=0;i< content.data.Count;i++)
            {
                Console.WriteLine(content.data[i].first_name);

            }
          
        }

        [Given(@"I send single user request to get the user details with id ""(.*)""")]
        public async System.Threading.Tasks.Task GivenISendSingleUserRequestToGetTheUserDetailsWithId(int id)
        {
            response = await api.GetSingleUser(BASE_URL,id);
        }

        [Then(@"I verify the user details of the user with id ""(.*)""")]
        public void ThenIVerifyTheUserDetailsOfTheUserWithId(int id)
        {
            var content = HandleContent.GetContent<Singleuser>(response);
            Assert.AreEqual(id, content.data.id);
        }
        
      
        [Given(@"I send user update request to update the ""(.*)"" and ""(.*)""")]
        public async System.Threading.Tasks.Task GivenISendUserUpdateRequestToUpdateTheAnd(string name, string job)
        {
            createUserReq.name = name;
            createUserReq.job = job;
            response = await api.UpdateUser(BASE_URL, name,job);
        }

        [Then(@"I verify the updated user details with ""(.*)""")]
        public void ThenIVerifyTheUpdatedUserDetailsWith(int StatusCode)
        {
            var content = HandleContent.GetContent<UserUpdateData>(response);
            Assert.AreEqual(createUserReq.name, content.Name);
            Assert.AreEqual(createUserReq.job, content.Job);

            statusCode = response.StatusCode;
            var code = (int)statusCode;
            Assert.AreEqual(StatusCode, code);

        }


        [Then(@"I verify the updated user details")]
        public void ThenIVerifyTheUpdatedUserDetails()
        {
            var content = HandleContent.GetContent<UserUpdateData>(response);
            Assert.AreEqual("zion resident", content.Job);

        }

        [Given(@"I send user patch request to update the ""(.*)""")]
        public async System.Threading.Tasks.Task GivenISendUserPatchRequestToUpdateThe(string name)
        {
            createUserReq.name = name;
            response = await api.PartiallyUpdateUser(BASE_URL, name);
        }


        [Then(@"I verify the partially updated user details with ""(.*)""")]
        public void ThenIVerifyThePartiallyUpdatedUserDetailsWith(int StatusCode)
        {
            var content = HandleContent.GetContent<UserUpdateData>(response);
            Assert.AreEqual(createUserReq.name, content.Name);
            Assert.AreEqual(createUserReq.job, content.Job);

            statusCode = response.StatusCode;
            var code = (int)statusCode;
            Assert.AreEqual(StatusCode, code);
        }


        [Then(@"I verfiy the ""(.*)"" from the list of users")]
        public void ThenIVerfiyTheFromTheListOfUsers(string username)
        {
            bool status = false;
            var content = HandleContent.GetContent<Users>(response);
            for (int i = 0; i < content.data.Count; i++)
            {
                string temp = content.data[i].first_name +" "+ content.data[i].last_name;
                //Console.WriteLine(content.data[i].first_name);
                if (temp == username)
                {
                    status = true;
                    break;
                }
             

            }
            Assert.IsTrue(status);
            

        }


    }

}
