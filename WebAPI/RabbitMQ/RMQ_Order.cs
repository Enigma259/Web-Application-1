using ClassLibrary1.Model;
using System.Collections.Generic;

namespace WebAPI.RabbitMQ
{
    /// <summary>
    /// This is the class RMQ_Order.
    /// </summary>
    public sealed class RMQ_Order
    {
        #region Instances

        private static volatile RMQ_Order _instance;
        private static readonly object syncRoot = new object();
        private RMQ_Sender rmq_sender;

        #endregion

        #region Delete functions

        /// <summary>
        /// This is the constructor or the class RMQ_Order.
        /// </summary>
        private RMQ_Order()
        {
            rmq_sender = RMQ_Sender.GetInstance();
        }

        #endregion

        #region Singleton functions

        /// <summary>
        /// This is a multi threaded singleton for the class RMQ_Order.
        /// </summary>
        /// <returns>_instance</returns>
        public static RMQ_Order GetInstance()
        {
            if (_instance == null)
            {
                lock (syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new RMQ_Order();
                    }
                }
            }

            return _instance;
        }

        #endregion

        #region Create functions

        /// <summary>
        /// Sends a message to the queue to create an order.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="products"></param>
        public void Create(string username, List<int> products)
        {
            List<string> informations = new List<string>
            {
                "Order Detail",
                "Create",
                username
            };

            foreach (int id in products)
            {
                informations.Add(id.ToString());
            }

            rmq_sender.SendMessage(informations);
        }

        #endregion

        #region Update functions

        /// <summary>
        /// Sends a message to the queue to update an order.
        /// </summary>
        /// <param name="order"></param>
        public void Update(Order order)
        {
            List<string> informations = new List<string>
            {
                "Order Detail",
                "Update",
                order.Id.ToString(),
                order.Price.ToString(),
                order.IsShipped.ToString(),
                order.Username
            };

            rmq_sender.SendMessage(informations);
        }

        #endregion

        #region Delete functions

        /// <summary>
        /// Sends a message to the queue to delete an order.
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            List<string> informations = new List<string>
            {
                "Order Detail",
                "Delete",
                id.ToString()
            };

            rmq_sender.SendMessage(informations);
        }

        #endregion
    }
}