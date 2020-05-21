using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XFAuthentication.Helpers;
using XFAuthentication.Models;
using Microsoft.CSharp;


namespace XFAuthentication.Services
{
    public class ApiServices
    {
        public async Task<bool> RegisterAsync(string email, string password, string confirmPassword)
        {
            var client = new HttpClient();

            var model = new RegisterBindingModel {
                   Email = email,
                   Password = password,
                   ConfirmPassword = confirmPassword
            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(Constant.RegisterAsyncUrl, content);

            return response.IsSuccessStatusCode;
        }

        public async Task<string> LoginAsync(string userName, string password)
        {
            var keyValues = new List<KeyValuePair<string, string>> {
                    new KeyValuePair<string, string>("username",userName),
                    new KeyValuePair<string, string>("password",password),
                    new KeyValuePair<string, string>("grant_type","password")
            };

            var request = new HttpRequestMessage(HttpMethod.Post, Constant.GetTokenUrl);
            request.Content = new FormUrlEncodedContent(keyValues);

            var client = new HttpClient();
            var response = await client.SendAsync(request);

            var jwt = await response.Content.ReadAsStringAsync();

            JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(jwt);
            var accesstoken = jwtDynamic.Value<string>("access_token");
            var accesstokenExpiration = jwtDynamic.Value<DateTime>(".expires");
            Settings.AccessTokenExpiration = accesstokenExpiration;

            Debug.WriteLine(jwt);

            return accesstoken;
        }

        public async Task<List<Idea>> GetIdeasAsync(string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",accessToken);
           var json = await client.GetStringAsync(Constant.GetIdeaAsyncUrl);

           var ideas = JsonConvert.DeserializeObject<List<Idea>>(json);

           return ideas;
        }

        public async Task<bool> PostIdeaAsync(Idea idea, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var json = JsonConvert.SerializeObject(idea);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(Constant.PostIdeaAsyncUrl, content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> PutIdeaAsync(Idea idea, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var json = JsonConvert.SerializeObject(idea);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(Constant.PutIdeaAsyncUrl + "/" + idea.Id, content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteIdeaAsync(int id, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            /*
            var json = JsonConvert.SerializeObject(idea);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            */
            var response = await client.DeleteAsync(Constant.DeleteIdeaAsyncUrl + "/" + id);

            return response.IsSuccessStatusCode;
        }

        public async Task<List<Idea>> SearchIdeasAsync(string keyword,string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var json = await client.GetStringAsync(Constant.SearchIdeaAsyncUrl + "/" + keyword);

            var ideas = JsonConvert.DeserializeObject<List<Idea>>(json);

            return ideas;
        }

    }
}
