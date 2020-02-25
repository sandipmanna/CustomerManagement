using CustomerManagement.Models;
using CustomerManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace CustomerManagement.Controllers
{
    public class CustomerModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            HttpContextBase ObjectContext = controllerContext.HttpContext;
            string _customerName = ObjectContext.Request.Form["Customer.CustomerName"] == "" ? null : ObjectContext.Request.Form["Customer.CustomerName"];
            string _customerCode = ObjectContext.Request.Form["Customer.CustomerCode"] == "" ? null : ObjectContext.Request.Form["Customer.CustomerCode"];
            CustomerViewModel Obj = new CustomerViewModel();
            {
                Obj.Customer.CustomerName = _customerName;
                Obj.Customer.CustomerCode = _customerCode;
            }
            return Obj;
        }
    }

    [Authorize]
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Load()
        {
            return View();
        }

        public ActionResult Create()
        {
            CustomerViewModel CustomerVM = new CustomerViewModel();
            CustomerVM.Customer = new Customer();
            //No Need to Pass Data cause it will be fetch by ajax call on load usign thread;
            //CustomerVM.Customers = GetCustomerList();
            return View("Create", CustomerVM);
        }

        public ActionResult GetCustomer()
        {
            List<Customer> _customerList;
            using (CustomerDBContext _dbContext = new CustomerDBContext())
            {
                _customerList = _dbContext.Customers.ToList<Customer>();
                Thread.Sleep(5000);
            }
            return Json(_customerList, JsonRequestBehavior.AllowGet);
        }

        [ActionName(name: "CustomerByCode")]
        public ActionResult GetCustomer(string CustomerCode)
        {
            List<Customer> _customerList;
            using (CustomerDBContext _dbContext = new CustomerDBContext())
            {
                _customerList = (from cust in _dbContext.Customers
                                 where (cust.CustomerCode.ToUpper() == CustomerCode.ToUpper())
                                 select cust).ToList<Customer>();
                Thread.Sleep(5000);
            }
            return Json(_customerList, JsonRequestBehavior.AllowGet);
        }

        [Obsolete(message: "this funciton was used when there was a submit event, now it will be an Ajax call for save, instaed use \"AddCustomer\"", error: true)]
        public ActionResult SubmitCustomer()
        {
            Customer Cust = new Customer();
            Cust.CustomerName = Request.Form["Customer.CustomerName"];
            Cust.CustomerCode = Request.Form["Customer.CustomerCode"];
            if (ModelState.IsValid)
            {
                //insert customer object to database
                using (CustomerDBContext Dal = new CustomerDBContext())
                {
                    Dal.Customers.Add(Cust); //in Momory Commit
                    Dal.SaveChanges(); //Physical Commit
                }
            }
            return RedirectToAction("CreateCustomer");
        }

        public ActionResult AddCustomer(CustomerViewModel Obj)
        {
            if (ModelState.IsValid)
            {
                using (CustomerDBContext Dal = new CustomerDBContext())
                {
                    Dal.Customers.Add(Obj.Customer); //in Momory Commit
                    Dal.SaveChanges(); //Physical Commit
                }
            }

            List<Customer> CustomerCollection = new List<Customer>();
            CustomerCollection = GetCustomerList();
            return Json(CustomerCollection, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search()
        {
            CustomerViewModel CustVM = new CustomerViewModel();
            CustVM.Customer = new Customer();
            CustVM.Customers = new List<Customer>();
            return View(CustVM);
        }

        /*public ActionResult SearchCustomer()
        {
            string _customerCode = Request.Form["txtCustomerCode"];
            CustomerViewModel CustVM = new CustomerViewModel();
            using (CustomerDBContext _context = new CustomerDBContext())
            {
                List<Customer> _customerList = (from x in _context.Customers
                                               where (x.CustomerCode.ToUpper() == _customerCode.ToUpper())
                                               select x).ToList<Customer>();
                CustVM.Customers = _customerList;
            }
            CustVM.Customer.CustomerCode = _customerCode;
            return View("Search", CustVM);
        }*/

        [NonAction]
        public List<Customer> GetCustomerList()
        {
            List<Customer> CustomerList = new List<Customer>();
            using (CustomerDBContext dal = new CustomerDBContext())
            {
                CustomerList = dal.Customers.ToList<Customer>();
            }
            return CustomerList;
        }
    }
}