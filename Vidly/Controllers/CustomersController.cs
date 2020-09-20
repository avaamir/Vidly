using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();


        // GET: Customer
        public ActionResult Index()
        {
            return View(_context.Customers.Include(c => c.MembershipType).ToList());
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }


        public ActionResult New() //go to create customer form
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer()  ,
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm", viewModel);
        }

        public ActionResult Edit(int id) //go to edit customer form
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();

            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer, String __RequestVerificationToken) //update or create new customer to db
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()

                };
                return View("CustomerForm", viewModel );
            }
            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}