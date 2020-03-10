using ClassLibrary1.Model;
using System.Collections.Generic;

namespace WebAPI.RabbitMQ
{
    /// <summary>
    /// This is the class RMQ_SpecialCalls.
    /// </summary>
    public sealed class RMQ_SpecialCalls
    {
        #region Instances

        private static volatile RMQ_SpecialCalls _instance;
        private static readonly object syncRoot = new object();
        private RMQ_Sender rmq_sender;

        #endregion

        /// <summary>
        /// This is the constructor for the class RMQ_SpecialCalls.
        /// </summary>
        private RMQ_SpecialCalls()
        {
            rmq_sender = RMQ_Sender.GetInstance();
        }

        #region Singleton functions

        /// <summary>
        /// This is a multi threaded singleton for the class RMQ_SpecialCalls.
        /// </summary>
        /// <returns>_instance</returns>
        public static RMQ_SpecialCalls GetInstance()
        {
            if (_instance == null)
            {
                lock (syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new RMQ_SpecialCalls();
                    }
                }
            }

            return _instance;
        }

        #endregion

        #region Order functions

        /// <summary>
        /// Sends a message to the queue with a user that have made new orders.
        /// </summary>
        /// <param name="username"></param>
        public void NewOrders(string username)
        {
            List<string> informations = new List<string>
            {
                "Special Calls",
                "Ship Orders",
                username
            };

            rmq_sender.SendMessage(informations);
        }

        #endregion

        #region Login functions

        /// <summary>
        /// Sends a message to the queue to log a user in.
        /// </summary>
        /// <param name="user"></param>
        public void UserLogin(CustomUser user)
        {
            List<string> informations = new List<string>
            {
                "Special Calls",
                "Log In",
                user.Username,
                user.Password,
                user.Email,
                user.Wallet.ToString()
            };

            rmq_sender.SendMessage(informations);
        }

        /// <summary>
        /// Sends a message to the queue to log a user out.
        /// </summary>
        /// <param name="user"></param>
        public void UserLogout(CustomUser user)
        {
            List<string> informations = new List<string>
            {
                "Special Calls",
                "Log Out",
                user.Username,
                user.Password,
                user.Email,
                user.Wallet.ToString()
            };

            rmq_sender.SendMessage(informations);
        }

        #endregion

        #region Session Key functions

        /// <summary>
        /// Sends a message to the queue to assign a session key to a user.
        /// </summary>
        /// <param name="user"></param>
        public void UserAssignSessionKey(CustomUser user)
        {
            List<string> informations = new List<string>
            {
                "Special Calls",
                "Assign Session Key",
                user.Username,
                user.Password,
                user.Email,
                user.Wallet.ToString()
            };

            rmq_sender.SendMessage(informations);
        }

        #endregion
    }
}