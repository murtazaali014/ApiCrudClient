using ApiCrudClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace ApiCrudClient
{
    public class APIGateway
    {
        private string url = "http://localhost:7217/api/User";
        private string CreateUser = "http://localhost:7217/api/User/AddUser";
        private string GetById = "http://localhost:7217/api/User/GetUserById?id=";
        private string UpdateUser = "http://localhost:7217/api/User/UpdateUser?id=";
        private string DeleteUser = "http://localhost:7217/api/User/DeleteUser?id=";
        private HttpClient httpClient = new HttpClient();

        public List<User> ListUsers()
        {
            List<User> users = new List<User>();
            if (url.Trim().Substring(0, 5).ToLower() == "http")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                HttpResponseMessage res = httpClient.GetAsync(url).Result;
                if (res.IsSuccessStatusCode)
                {
                    string result = res.Content.ReadAsStringAsync().Result;
                    var datacol = JsonConvert.DeserializeObject<List<User>>(result);
                    if (datacol != null)
                        users = datacol;
                }
                else
                {
                    string result = res.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error occured at the API endpoint, Error info ." + result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occured at the API Endpoint. " + ex.Message);
            }
            finally { }
            return users;
        }
        public User CreateUsers(User users)
        {
            //List<User> users = new List<User>();
            if (url.Trim().Substring(0, 5).ToLower() == "http")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string json = JsonConvert.SerializeObject(users);
            try
            {
                HttpResponseMessage res = httpClient.PostAsync(CreateUser, new StringContent(json,Encoding.UTF8,"application/json")).Result;
                if (res.IsSuccessStatusCode)
                {
                    string result = res.Content.ReadAsStringAsync().Result;
                    var datacol = JsonConvert.DeserializeObject<User>(result);
                    if (datacol != null)
                        users = datacol;
                }
                else
                {
                    string result = res.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error occured at the API endpoint, Error info ." + result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occured at the API Endpoint. " + ex.Message);
            }
            finally { }
            return users;
        }
        public User GetUsers(int id)
        {
            User users = new User();
            url = GetById + id;
            if (url.Trim().Substring(0, 5).ToLower() == "http")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            try
            {
                HttpResponseMessage res = httpClient.GetAsync(url).Result;
                if (res.IsSuccessStatusCode)
                {
                    string result = res.Content.ReadAsStringAsync().Result;
                    var datacol = JsonConvert.DeserializeObject<User>(result);
                    if (datacol != null)
                        users = datacol;
                }
                else
                {
                    string result = res.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error occured at the API endpoint, Error info ." + result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occured at the API Endpoint. " + ex.Message);
            }
            finally { }
            return users;
        }
        public void UpdateUsers(User users)
        {
            if (url.Trim().Substring(0, 5).ToLower() == "http")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            int id = users.id;
            url = UpdateUser + id;
            string json = JsonConvert.SerializeObject(users);
            try
            {
                HttpResponseMessage res = httpClient.PutAsync(url , new StringContent(json,Encoding.UTF8,"application/json")).Result;
                if (!res.IsSuccessStatusCode)
                {
                    string result = res.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error Occured at the API Endpoint" + result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occured at the API Endpoint. " + ex.Message);
            }
            finally { }
            return ;
        }
        public void DeleteUsers(int id)
        {
            if (url.Trim().Substring(0, 5).ToLower() == "http")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            url = DeleteUser + id;
            try
            {
                HttpResponseMessage res = httpClient.DeleteAsync(url).Result;
                if (!res.IsSuccessStatusCode)
                {
                    string result = res.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error Occured at the API Endpoint" + result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occured at the API Endpoint. " + ex.Message);
            }
            finally { }
            return;
        }
    }
}
