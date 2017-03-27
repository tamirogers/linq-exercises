using LinqExercises.Infrastructure;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace LinqExercises.Controllers
{
    public class CustomersController : ApiController
    {
        private NORTHWNDEntities _db;

        public CustomersController()
        {
            _db = new NORTHWNDEntities();
        }

        // GET: api/customers/city/London
        [HttpGet, Route("api/customers/city/{city}"), ResponseType(typeof(IQueryable<Customer>))]
        public IHttpActionResult GetAll(string city)
        {
            //("Write a query to return all customers in the given city");

            var resultSet = from customer in _db.Customers
                            where customer.City.Contains(city)
                            select customer;

            return Ok(resultSet);


        }

        // GET: api/customers/mexicoSwedenGermany
        [HttpGet, Route("api/customers/mexicoSwedenGermany"), ResponseType(typeof(IQueryable<Customer>))]
        public IHttpActionResult GetAllFromMexicoSwedenGermany()
        {
            //("Write a query to return all customers from Mexico, Sweden and Germany.");

            var resultSet = from customer in _db.Customers
                            where customer.Country.Contains("mexico") || customer.Country.Contains ("Sweden") || customer.Country.Contains("Germany")
                            select customer;

            return Ok(resultSet);



            //var resultset = _db.Customers
            //  .Where(c => c.Country == "Mexico"
            //  || c.Country == "Sweden"
            //  || c.Country == "Germany");

            //return Ok(resultset);




        }

        // GET: api/customers/shippedUsing/Speedy Express
        [HttpGet, Route("api/customers/shippedUsing/{shipperName}"), ResponseType(typeof(IQueryable<Customer>))]
        public IHttpActionResult GetCustomersThatShipWith(string shipperName)
        {
            //("Write a query to return all customers with orders that shipped using the given shipperName.");
            //orders is child of customers, key CustomerID

            //var resultSet = from customer in _db.Customers
                            
            //                join orders in _db.Orders on customer.CustomerID equals orders.CustomerID
            //                join shippers in _db.Shippers on orders.ShipVia equals shippers.ShipperID
            //                where shippers.CompanyName.Contains(shipperName)
            //                select customer;

            //return Ok(resultSet);

            var resultSet = _db.Customers
                           .Where(o => o.Orders.Any(s => s.Shipper.CompanyName == shipperName));
            return Ok(resultSet);







        }

        // GET: api/customers/withoutOrders
        [HttpGet, Route("api/customers/withoutOrders"), ResponseType(typeof(IQueryable<Customer>))]
        public IHttpActionResult GetCustomersWithoutOrders()
        {
            //("Write a query to return all customers with no orders in the Orders table.");

            var resultSet = from customer in _db.Customers
                                //customers and orders linked w/ CustomerID
                            where !customer.Orders.Any()
                            //where customer.Orders.Contains(null)
                            select customer;

            return Ok(resultSet);







        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }
    }
}
