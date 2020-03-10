using ClassLibrary1.Controller;
using ClassLibrary1.Model;
using System.Collections.Generic;
using System.Web.Http;
using WebAPI.RabbitMQ;

namespace WebAPI.Controllers
{
    /// <summary>
    /// This is the class OrderController.
    /// </summary>
    public class OrderController : ApiController
    {
        #region Instances

        private CTR_Order order_ctr = new CTR_Order();
        PasswordHasher pw = new PasswordHasher();
        private RMQ_SpecialCalls rmq_special = RMQ_SpecialCalls.GetInstance();

        #endregion

        #region IEnumerable functions

        // GET: api/Order
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        #endregion

        #region Create functions

        /// <summary>
        /// Creates an order.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="products"></param>
        /// <returns>string</returns>
        [Route("api/Order/CreateWithMultiple")]
        [HttpPost]
        public string CreateWithMultiple(CustomUser user, List<int> product_ids)
        {
            string result;
            result = order_ctr.Create(user.Username, product_ids);
            rmq_special.NewOrders(user.Username);
            return result;
        }

        /// <summary>
        /// Creates an order.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="products"></param>
        /// <returns>string</returns>
        [Route("api/Order/CreateWithSingle")]
        [HttpPost]
        public string CreateWithSingle(CustomUser user, int product_id)
        {
            string result;

            if (pw.ValidatePassword(user))
            {
                List<int> product_ids = new List<int>();
                product_ids.Add(product_id);

                result = order_ctr.Create(user.Username, product_ids);
                rmq_special.NewOrders(user.Username);
            }

            else
            {
                result = "Password not Validated: " + user.Password;
            }

            return result;
        }

        #endregion

        #region Search functions

        /// <summary>
        /// List All Orders.
        /// </summary>
        /// <returns>List<Order></returns>
        [Route("api/Order/ListAll")]
        [HttpGet]
        public List<Order> ListAll()
        {
            return order_ctr.ListAll();
        }

        /// <summary>
        /// Find an order by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Order</returns>
        [Route("api/Order/FindById/{id}")]
        [HttpGet]
        public Order FindById(int id)
        {
            return order_ctr.FindById(id);
        }

        /// <summary>
        /// Find a list of orders by their price.
        /// </summary>
        /// <param name="price"></param>
        /// <param name="where"></param>
        /// <returns>List<Order></returns>
        [Route("api/Order/FindByPrice/{price}/{where}")]
        [HttpGet]
        public List<Order> FindByPrice(double price, string where)
        {
            return order_ctr.FindByPrice(price, where);
        }

        /// <summary>
        /// Find a list of orders by their is_shipped.
        /// </summary>
        /// <param name="is_shipped"></param>
        /// <returns>List<Order></returns>
        [Route("api/Order/FindByIsShipped/{is_shipped}")]
        [HttpGet]
        public List<Order> FindByIsShipped(bool is_shipped)
        {
            return order_ctr.FindByIsShipped(is_shipped);
        }

        /// <summary>
        /// Find a list of orders by their username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>List<Order></returns>
        [Route("api/Order/FindByUsername/{username}")]
        [HttpGet]
        public List<Order> FindByUsername(string username)
        {
            return order_ctr.FindByUsername(username);
        }

        #endregion

        #region Update functions

        /// <summary>
        /// Updates an order.
        /// </summary>
        /// <param name="order"></param>
        /// <returns>string</returns>
        [Route("api/Order/Update")]
        [HttpPost]
        public string Update(Order order)
        {
            return order_ctr.Update(order);
        }

        #endregion

        #region Delete functions

        /// <summary>
        /// Deletes an order.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>string</returns>
        [Route("api/Order/Delete")]
        [HttpPost]
        public string Delete(int id)
        {
            return order_ctr.Delete(id, "Order");
        }

        #endregion
    }
}