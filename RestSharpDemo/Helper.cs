using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpDemo
{
    public class Helper
    {
        private RestClient client;
        private RestRequest request;
      
        

       
        //This Method is used to Set the URL
        public RestClient SetUrl(string baseUrl, string endpoint)
        {
            var url = Path.Combine(baseUrl, endpoint);
            client = new RestClient(url);         
            return client;
        }
        //This method is used to create the get request 
        public RestRequest CreateGetRequest()
        {
            request = new RestRequest()
            {
                Method = Method.Get
            };
            request.AddHeader("Accept", "application/json");          
            return request;
        }
        //This method is used to create the post request 
        public RestRequest CreatePostRequest<T>(T payload) where T: class
        {
            request = new RestRequest()
            {
                Method = Method.Post
            };            
            request.AddHeader("Accept", "application/json");
            //request.AddParameter("application/json", payload, ParameterType.RequestBody);
            request.AddBody(payload);
            request.RequestFormat = DataFormat.Json;
            return request;
        }
        //This method is used to create the put request
        public RestRequest CreatePutRequest<T>(T payload) where T: class
        {
            request = new RestRequest()
            {
                Method = Method.Put
            };
            request.AddHeader("Accept", "application/json");
            //request.AddParameter("application/json", payload, ParameterType.RequestBody);
            request.AddBody(payload);
            request.RequestFormat = DataFormat.Json;
            return request;
        }
   
        //This method is used to get the response
        public async Task<RestResponse> GetResponseAsync(RestClient restClient, RestRequest restRequest)
        {
            return await restClient.ExecuteAsync(restRequest);
        }
    }
}
