using ClassLibrary1.Database;
using ClassLibrary1.Model;
using System.Collections.Generic;

namespace ClassLibrary1.Controller
{
    /// <summary>
    /// This is the class CTR_Order.
    /// </summary>
    public sealed class CTR_Order
    {
        #region Instances

        private DB_Order db_order;
        private CTR_OrderDetail ctr_orderdetail;
        private CTR_Product ctr_product;

        #endregion

        /// <summary>
        /// This is the constructor for the class CTR_Order.
        /// </summary>
        public CTR_Order()
        {
            this.db_order = DB_Order.GetInstance();
            this.ctr_orderdetail = new CTR_OrderDetail();
            this.ctr_product = new CTR_Product();
        }

        #region Create functions

        /// <summary>
        /// Creates an order.
        /// </summary>
        /// <param name="order"></param>
        /// <returns>string</returns>
        public string Create(string username, List<int> products)
        {
            Order new_order;
            List<Order> orders;
            OrderDetail order_detail;
            string result;
            double price = 0.0;

            //calculates the total price
            foreach (int id in products)
            {
                price += ctr_product.FindById(id).Price;
            }

            //creates a new order
            new_order = new Order
            {
                Price = price,
                IsShipped = false,
                Username = username
            };

            //saves new order in database
            result = db_order.CreateOrder(new_order);

            if (result.Equals("Success"))
            {
                //finds orders by username
                orders = FindByUsername(username);

                //gets the id for the newest order.
                foreach (Order order in orders)
                {
                    if (!order.IsShipped)
                    {
                        if (order.Id > new_order.Id)
                        {
                            new_order.Id = order.Id;
                        }
                    }
                }

                //creates orderdetails and saves them in the database.
                foreach (int id in products)
                {
                    order_detail = new OrderDetail
                    {
                        OrderId = new_order.Id,
                        ProductId = id
                    };

                    result = ctr_orderdetail.Create(order_detail);
                }
            }

            return result;
        }

        #endregion

        #region Search functions

        /// <summary>
        /// List all orders.
        /// </summary>
        /// <returns>List<Order></returns>
        public List<Order> ListAll()
        {
            return db_order.ListAllOrders();
        }

        /// <summary>
        /// Finds an order by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Order</returns>
        public Order FindById(int id)
        {
            return db_order.FindOrderById(id);
        }

        /// <summary>
        /// Finds a list of orders by their price.
        /// </summary>
        /// <param name="price"></param>
        /// <param name="where"></param>
        /// <returns>List<Order></returns>
        public List<Order> FindByPrice(double price, string where)
        {
            List<Order> result;

            switch (where)
            {
                case "Equal":
                    result = db_order.FindOrderByPrice(price);
                    break;

                case "Higher":
                    result = db_order.FindOrderByPriceHigher(price);
                    break;

                case "Lower":
                    result = db_order.FindOrderByPriceLower(price);
                    break;

                default:
                    result = new List<Order>();
                    break;
            }

            return result;
        }

        /// <summary>
        /// Finds a list of orders by their is_shipped.
        /// </summary>
        /// <param name="is_shipped"></param>
        /// <returns>List<Order></returns>
        public List<Order> FindByIsShipped(bool is_shipped)
        {
            return db_order.FindOrderByIsShipped(is_shipped);
        }

        /// <summary>
        /// Finds a list of orders by their username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns><List<Order>/returns>
        public List<Order> FindByUsername(string username)
        {
            return db_order.FindOrderByUsername(username);
        }

        #endregion

        #region Update functions

        /// <summary>
        /// Updates an order.
        /// </summary>
        /// <param name="order"></param>
        /// <returns>string</returns>
        public string Update(Order order)
        {
            return db_order.UpdateOrder(order);
        }

        #endregion

        #region Delete functions

        /// <summary>
        /// Deletes an order.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="call_origin"></param>
        /// <returns>string</returns>
        public string Delete(int id, string call_origin)
        {
            string result;

            result = db_order.DeleteOrder(id);

            return result;
        }

        #endregion
    }
}