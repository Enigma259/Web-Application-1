using ClassLibrary1.Model;
using System.Collections.Generic;

namespace WebAPI.RabbitMQ
{
    /// <summary>
    /// This is the class RMQ_OrderDetail.
    /// </summary>
    public sealed class RMQ_OrderDetail
    {
        #region Instances

        private static volatile RMQ_OrderDetail _instance;
        private static readonly object syncRoot = new object();
        private RMQ_Sender rmq_sender;

        #endregion

        /// <summary>
        /// This is the constructor for the class RMQ_OrderDetail.
        /// </summary>
        private RMQ_OrderDetail()
        {
            rmq_sender = RMQ_Sender.GetInstance();
        }

        #region Singleton functions

        /// <summary>
        /// This is a multi threaded singleton for the class RMQ_OrderDetail.
        /// </summary>
        /// <returns>_instance</returns>
        public static RMQ_OrderDetail GetInstance()
        {
            if (_instance == null)
            {
                lock (syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new RMQ_OrderDetail();
                    }
                }
            }

            return _instance;
        }

        #endregion

        #region Create functions

        /// <summary>
        /// Sends a message to the queue to create an order detail.
        /// </summary>
        /// <param name="order_detail"></param>
        public void Create(OrderDetail order_detail)
        {
            List<string> informations = new List<string>
            {
                "Order Detail",
                "Create",
                order_detail.OrderId.ToString(),
                order_detail.ProductId.ToString(),
            };

            rmq_sender.SendMessage(informations);
        }

        #endregion

        #region Update functions

        /// <summary>
        /// Sends a message to the queue to update an order detail.
        /// </summary>
        /// <param name="old_order_detail"></param>
        /// <param name="new_order_detail"></param>
        public void Update(OrderDetail old_order_detail, OrderDetail new_order_detail)
        {
            List<string> informations = new List<string>
            {
                "Order Detail",
                "Update",
                old_order_detail.OrderId.ToString(),
                old_order_detail.ProductId.ToString(),
                new_order_detail.OrderId.ToString(),
                new_order_detail.ProductId.ToString(),
            };

            rmq_sender.SendMessage(informations);
        }

        #endregion

        #region Delete functions

        /// <summary>
        /// Sends a message to the queue to to delete an order detail.
        /// </summary>
        /// <param name="order_detail"></param>
        public void Delete(OrderDetail order_detail)
        {
            List<string> informations = new List<string>
            {
                "Order Detail",
                "Delete",
                order_detail.OrderId.ToString(),
                order_detail.ProductId.ToString(),
            };

            rmq_sender.SendMessage(informations);
        }

        #endregion
    }
}