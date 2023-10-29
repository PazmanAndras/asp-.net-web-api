using ApiClientCrud.Models;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Newtonsoft.Json;
using System.Net;
using System.Text;


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

        public Customer CreateCustomer(Customer customer)
        {
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string json = JsonConvert.SerializeObject(customer);

            try
            {
                HttpResponseMessage response = httpClient.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<Customer>(result);
                    if (data != null)
                        customer = data;
                     
                }
                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error ocoured at api endpoint. eror info:" + result);
                }



            } 
            catch (Exception ex)
            {
                throw new Exception("Error ocoured at api endpoint. eror info:" + ex.Message);
            }
            finally { }
            return customer;
            

        }

        public Customer GetCustomer(int id)
        {
            Customer customer = new Customer();
            url = url + "/" + id;
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<Customer>(result);
                    if (data != null) 
                        customer = data;
                }
                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("erroro occoured at the api endpoint, error info:" + result);
                }


            } catch (Exception ex)
            {
                throw new Exception("erroro occoured at the api endpoint, error info:" + ex.Message);
            }
            finally { }
            return customer;
        }

        public void UpdateCustomer(Customer customer)
        {
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            int id = customer.id;
            
            url = url + "/" + id;

            string json = JsonConvert.SerializeObject(customer);

            try
            {
                HttpResponseMessage response = httpClient.PutAsync(url, new StringContent(json, Encoding.UTF8, "application/json")).Result;
                if(!response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("error occoured at the api endpoint, error info" + result);
                }
            }
            catch(Exception ex)
            {
                throw new Exception("error occoured at the api endpoint, error info" + ex.Message);
            }
            finally { }

            return;
        }

        public void DeleteCustomer(int id)
        {
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            url = url + "/" + id;

            try
            {
                HttpResponseMessage response = httpClient.DeleteAsync(url).Result;

                if (!response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("error occoured at the api endpoint, error info" + result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error occoured at the api endpoint, error info" + ex.Message);
            }
            finally { }


        }



    }
}
