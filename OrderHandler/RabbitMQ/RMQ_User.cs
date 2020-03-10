using ClassLibrary1.Controller;
using ClassLibrary1.Model;
using System;
using System.Collections.Generic;

namespace OrderHandler.RabbitMQ
{
    /// <summary>
    /// This is the class RMQ_User.
    /// </summary>
    public sealed class RMQ_User
    {
        private static volatile RMQ_User _instance;
        private static readonly object syncRoot = new object();
        private CustomUserController ctr_user;

        /// <summary>
        /// This is the conctructor for the class RMQ_User.
        /// </summary>
        private RMQ_User()
        {
            ctr_user = new CustomUserController();
        }

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

        /// <summary>
        /// Functions for managing orders.
        /// </summary>
        /// <param name="informations"></param>
        public void UserFunctions(List<string> informations)
        {
            int index = 1;
            List<string> new_informations = new List<string>();

            while (index < informations.Count)
            {
                if (index > 1)
                {
                    new_informations.Add(informations[index]);
                }

                index++;
            }

            switch (informations[1])
            {
                case "Create":
                    CreateUser(new_informations);
                    break;

                case "ListAll":
                    //insert code here
                    break;

                case "Find - Username":
                    //insert code here
                    break;

                case "Find - Password":
                    //insert code here
                    break;

                case "Find - Email":
                    //insert code here
                    break;

                case "Find - Is Active":
                    //insert code here
                    break;

                case "Find - Logged In":
                    //insert code here
                    break;

                case "Find - Wallet":
                    //insert code here
                    break;

                case "Update":
                    UpdateUser(new_informations);
                    break;

                case "Delete":
                    DeleteUser(new_informations);
                    break;

                case "GetSessionKey":
                    //insert code here
                    break;

                default:
                    Console.WriteLine("The function is not available: " + informations[1]);
                    break;

            }
        }

        /// <summary>
        /// Creates a user.
        /// </summary>
        /// <param name="informations"></param>
        public void CreateUser(List<string> informations)
        {
            CustomUser user = new CustomUser
            {
                Username = informations[0],
                Password = informations[1],
                Email = informations[2],
                Wallet = Double.Parse(informations[3])
            };

            Console.WriteLine("Create User: " + ctr_user.Create(user));
        }

        /// <summary>
        /// Updates a user
        /// </summary>
        /// <param name="informations"></param>
        public void UpdateUser(List<string> informations)
        {
            CustomUser user = new CustomUser
            {
                Username = informations[0],
                Password = informations[1],
                Email = informations[2],
                Wallet = Double.Parse(informations[3])
            };

            Console.WriteLine("Update User: " + ctr_user.Update(user));
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="informations"></param>
        public void DeleteUser(List<string> informations)
        {
            string username = informations[0];

            Console.WriteLine("Delete User: " + ctr_user.Delete(username));
        }
    }
}