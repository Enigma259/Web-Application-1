using ClassLibrary1.Model;
using System.Collections.Generic;

namespace WebAPI.RabbitMQ
{
    /// <summary>
    /// This is the class RMQ_User.
    /// </summary>
    public sealed class RMQ_User
    {
        #region Instances

        private static volatile RMQ_User _instance;
        private static readonly object syncRoot = new object();
        private RMQ_Sender rmq_sender;

        #endregion

        /// <summary>
        /// This is the constructor for the class RMQ_User.
        /// </summary>
        private RMQ_User()
        {
            rmq_sender = RMQ_Sender.GetInstance();
        }

        #region Singleton functions

        /// <summary>
        /// This is a multi threaded singleton for the class RMQ_User.
        /// </summary>
        /// <returns>_instance</returns>
        public static RMQ_User GetInstance()
        {
            if (_instance == null)
            {
                lock (syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new RMQ_User();
                    }
                }
            }

            return _instance;
        }

        #endregion

        #region Create functions

        /// <summary>
        /// Sends a message to the queue to create a user.
        /// </summary>
        /// <param name="user"></param>
        public void Create(CustomUser user)
        {
            List<string> informations = new List<string>
            {
                "User",
                "Create",
                user.Username,
                user.Password,
                user.Email,
                user.Wallet.ToString()
            };

            rmq_sender.SendMessage(informations);
        }

        #endregion

        #region Update functions

        /// <summary>
        /// Sends a message to the queue to Update a user.
        /// </summary>
        /// <param name="user"></param>
        public void Update(CustomUser user)
        {
            List<string> informations = new List<string>
            {
                "User",
                "Update",
                user.Username,
                user.Password,
                user.Email,
                user.Wallet.ToString()
            };

            rmq_sender.SendMessage(informations);
        }

        #endregion

        #region Delete functions

        /// <summary>
        /// Sends a message to the queue to Delete a user.
        /// </summary>
        /// <param name="username"></param>
        public void Delete(string username)
        {
            List<string> informations = new List<string>
            {
                "User",
                "Delete",
                username
            };

            rmq_sender.SendMessage(informations);
        }

        #endregion
    }
}