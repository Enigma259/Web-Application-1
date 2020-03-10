using ClassLibrary1.Model;
using System.Collections.Generic;

namespace WebAPI.RabbitMQ
{
    /// <summary>
    /// This is the class RMQ_UserProduct.
    /// </summary>
    public sealed class RMQ_UserProduct
    {
        #region Instances

        private static volatile RMQ_UserProduct _instance;
        private static readonly object syncRoot = new object();
        private RMQ_Sender rmq_sender;

        #endregion

        /// <summary>
        /// This is the constructor for the class RMQ_UserProduct.
        /// </summary>
        private RMQ_UserProduct()
        {
            rmq_sender = RMQ_Sender.GetInstance();
        }

        #region Singleton functions

        /// <summary>
        /// This is a multi threaded singleton for the class RMQ_UserProduct.
        /// </summary>
        /// <returns>_instance</returns>
        public static RMQ_UserProduct GetInstance()
        {
            if (_instance == null)
            {
                lock (syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new RMQ_UserProduct();
                    }
                }
            }

            return _instance;
        }

        #endregion

        #region Create functions

        /// <summary>
        /// Sends a message to the queue to Create a user product.
        /// </summary>
        /// <param name="user_product"></param>
        public void Create(UserProduct user_product)
        {
            List<string> informations = new List<string>
            {
                "User Product",
                "Create",
                user_product.Username,
                user_product.ProductId.ToString(),
                user_product.IsActive.ToString()
            };

            rmq_sender.SendMessage(informations);
        }

        #endregion

        #region Update functions

        /// <summary>
        /// Sends a message to the queue to Update a user product.
        /// </summary>
        /// <param name="old_user_product"></param>
        /// <param name="new_user_product"></param>
        public void Update(UserProduct old_user_product, UserProduct new_user_product)
        {
            List<string> informations = new List<string>
            {
                "User Product",
                "Update",
                old_user_product.Username,
                old_user_product.ProductId.ToString(),
                old_user_product.IsActive.ToString(),
                new_user_product.Username,
                new_user_product.ProductId.ToString(),
                new_user_product.IsActive.ToString()
            };

            rmq_sender.SendMessage(informations);
        }

        #endregion

        #region Delete functions

        /// <summary>
        /// Sends a message to the queue to Delete a user product.
        /// </summary>
        /// <param name="user_product"></param>
        public void Delete(UserProduct user_product)
        {
            List<string> informations = new List<string>
            {
                "User Product",
                "Delete",
                user_product.Username,
                user_product.ProductId.ToString(),
                user_product.IsActive.ToString()
            };

            rmq_sender.SendMessage(informations);
        }

        #endregion
    }
}