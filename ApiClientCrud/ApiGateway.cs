using ApiClientCrud.Models;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Newtonsoft.Json;
using System.Net;

namespace ApiClientCrud
{
    public class ApiGateway
    {
        private string url = "https://localhost:7278/api/Customer";
        private HttpClient httpClient = new HttpClient();

        public List<Customer> ListCustomers()
        {
            List<Customer> customers = new List<Customer>();
            if(url.Trim().Substring(0, 5).ToLower() == "https") //chack api endpoint url
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; //encrytion point, communication go throught this enctipted socket

            try
            {
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                if(response.IsSuccessStatusCode) //chech response  statuscode
                {
                    string result = response.Content.ReadAsStringAsync().Result;   //if call suces, output sting 
                    var datacol = JsonConvert.DeserializeObject<List<Customer>>(result);
                    if (datacol != null)  // chach customer exist
                        customers = datacol;
                }
                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("error occured at the api endpoint, eror info:" + result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error occured at the api endpoint, eror info:" + ex.Message);
            }
            finally { }

            return customers;


        }



    }
}
