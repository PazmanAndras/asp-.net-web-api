using Microsoft.AspNetCore.Mvc;
using ApiClientCrud.Models;

namespace ApiClientCrud.Controllers
{
    public class CustomerController : Controller
    {

        private readonly ApiGateway apiGateway;

        public CustomerController(ApiGateway apiGateway)
        {
            this.apiGateway = apiGateway;
        }

        public IActionResult Index()
        {
            List<Customer> customers;
            customers = apiGateway.ListCustomers();
            return View(customers);
        }
        [HttpGet]
        public IActionResult Create()
        {
            Customer customer = new Customer();
            return View(customer);
        }

        [HttpPost]
        public IActionResult Create(Customer customer) 
        { 
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id) 
        {
            Customer customer = new Customer();
            return View(customer);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Customer customer = new Customer();
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Customer customer = new Customer();
            return View(customer);
        }

        [HttpPost]

        public IActionResult Delete(Customer customer)
        {
            return RedirectToAction("index");
        }





    }
}
