using ClassLibrary1.Model;
using System.Collections.Generic;

namespace WebAPI.RabbitMQ
{
    /// <summary>
    /// This is the class RMQ_Product.
    /// </summary>
    public sealed class RMQ_Product
    {
        #region Instances

        private static volatile RMQ_Product _instance;
        private static readonly object syncRoot = new object();
        private RMQ_Sender rmq_sender;

        #endregion

        /// <summary>
        /// This is the constructor for the class RMQ_Product.
        /// </summary>
        private RMQ_Product()
        {
            rmq_sender = RMQ_Sender.GetInstance();
        }

        #region Singleton functions

        /// <summary>
        /// This is a multi threaded singleton for the class RMQ_Product.
        /// </summary>
        /// <returns>_instance</returns>
        public static RMQ_Product GetInstance()
        {
            if (_instance == null)
            {
                lock (syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new RMQ_Product();
                    }
                }
            }

            return _instance;
        }

        #endregion

        #region Create functions

        /// <summary>
        /// Sends a message to the queue to create a product.
        /// </summary>
        /// <param name="product"></param>
        public void Create(Product product)
        {
            List<string> informations = new List<string>
            {
                "Product",
                "Create",
                product.Name,
                product.Price.ToString(),
            };

            rmq_sender.SendMessage(informations);
        }

        #endregion

        #region Update functions

        /// <summary>
        /// Sends a message to the queue to update a product.
        /// </summary>
        /// <param name="product"></param>
        public void Update(Product product)
        {
            List<string> informations = new List<string>
            {
                "Product",
                "Update",
                product.Id.ToString(),
                product.Name,
                product.Price.ToString(),
            };

            rmq_sender.SendMessage(informations);
        }

        #endregion

        #region Delete functions

        /// <summary>
        /// Sends a message to the queue to delete a product.
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            List<string> informations = new List<string>
            {
                "Product",
                "Delete",
                id.ToString()
            };

            rmq_sender.SendMessage(informations);
        }

        #endregion
    }
}