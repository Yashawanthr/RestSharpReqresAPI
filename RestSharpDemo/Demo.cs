
using Newtonsoft.Json;
using RestSharp;
using RestSharpDemo.Models;
using RestSharpDemo.Models.Request;
using System;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using AventStack.ExtentReports.Configuration;


namespace RestSharpDemo
{
    public class Demo
    {
        private Helper helper;

        public Demo()
        {
            helper = new Helper();
        }

        //This method is used to get the list of users 
        public async Task<RestResponse> GetUsers(string baseUrl)
        {
            //helper.AddCertificate("", "");
            var client = helper.SetUrl(baseUrl, "api/users?page=2");
            var request = helper.CreateGetRequest();
            request.RequestFormat = DataFormat.Json;
            var response = await helper.GetResponseAsync(client, request);
            return response;
        }
        //This method is used to get the single of users 
        public async Task<RestResponse> GetSingleUser(string baseUrl, int id)
        {
            //helper.AddCertificate("", "");
            var client = helper.SetUrl(baseUrl, "api/users/"+id);
            var request = helper.CreateGetRequest();
            request.RequestFormat = DataFormat.Json;
            var response = await helper.GetResponseAsync(client, request);
            return response;
        }
        //This method is used to create the user 
        public async Task<RestResponse> CreateNewUser(string baseUrl, dynamic payload)
        {
            var client = helper.SetUrl(baseUrl, "api/users");
            //var jsonString = HandleContent.SerializeJsonString(payload);
            var request = helper.CreatePostRequest<CreateUserReq>(payload);
            var response = await helper.GetResponseAsync(client, request);
            return response;
        }

        //This method is used to update the user using the Put method
        public async Task<RestResponse> UpdateUser(string baseUrl,string Name,string Job)
        {
            var client = helper.SetUrl(baseUrl, "api/users/2");
            //var request = helper.CreatePutRequest<CreatePutReq>(payload);
            // var response = await helper.GetResponseAsync(client, request);
            var request = new RestRequest("api/resource/{id}", Method.Put);
            request.AddParameter("id", 2, ParameterType.UrlSegment);
            request.AddJsonBody(new { name = Name, job = Job });
            var response = client.ExecuteAsync(request);
            return await response;
        }

        //This method is used to update the user using the patch method
        public async Task<RestResponse> PartiallyUpdateUser(string baseUrl,string Name)
        {
            var client = helper.SetUrl(baseUrl, "api/users/2");
            var request = new RestRequest("api/resource/{id}", Method.Patch);
            request.AddParameter("id", 2, ParameterType.UrlSegment);
            request.AddJsonBody(new { name = Name });
            var response = client.ExecuteAsync(request);
            return await response;
        }

       
    }
}
