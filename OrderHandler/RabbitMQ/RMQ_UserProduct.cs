using ClassLibrary1.Controller;
using ClassLibrary1.Model;
using System;
using System.Collections.Generic;

namespace OrderHandler.RabbitMQ
{
    /// <summary>
    /// This is the class RMQ_UserProduct.
    /// </summary>
    public sealed class RMQ_UserProduct
    {
        private static volatile RMQ_UserProduct _instance;
        private static readonly object syncRoot = new object();
        private CTR_UserProduct ctr_userproduct;

        /// <summary>
        /// This is the conctructor for the class RMQ_UserProduct.
        /// </summary>
        private RMQ_UserProduct()
        {
            ctr_userproduct = new CTR_UserProduct();
        }

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

        /// <summary>
        /// Functions for managing orders.
        /// </summary>
        /// <param name="informations"></param>
        public void UserProductFunctions(List<string> informations)
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
                    CreateUserProduct(new_informations);
                    break;

                case "ListAll":
                    //Insert code here
                    break;

                case "Find":
                    //Insert code here
                    break;

                case "findByIsActive":
                    //Insert code here
                    break;

                case "Update":
                    UpdateUserProduct(new_informations);
                    break;

                case "Delete":
                    DeleteUserProduct(new_informations);
                    break;

                default:
                    Console.WriteLine("The function is not available: " + informations[1]);
                    break;
            }
        }

        /// <summary>
        /// Creates a user product.
        /// </summary>
        /// <param name="informations"></param>
        public void CreateUserProduct(List<string> informations)
        {
            UserProduct user_product = new UserProduct
            {
                Username = informations[0],
                ProductId = Int32.Parse(informations[1]),
                IsActive = Boolean.Parse(informations[2])
            };

            Console.WriteLine("Create User Product: " + ctr_userproduct.Create(user_product));
        }

        /// <summary>
        /// Updates a user product.
        /// </summary>
        /// <param name="informations"></param>
        public void UpdateUserProduct(List<string> informations)
        {
            UserProduct old_user_product = new UserProduct
            {
                Username = informations[0],
                ProductId = Int32.Parse(informations[1]),
                IsActive = Boolean.Parse(informations[2])
            };
            UserProduct new_user_product = new UserProduct
            {
                Username = informations[3],
                ProductId = Int32.Parse(informations[4]),
                IsActive = Boolean.Parse(informations[5])
            };

            Console.WriteLine("Update User Product: " + ctr_userproduct.Update(old_user_product, new_user_product));
        }

        /// <summary>
        /// Deletes a user product.
        /// </summary>
        /// <param name="informations"></param>
        public void DeleteUserProduct(List<string> informations)
        {
            string instance_use = informations[2];

            UserProduct user_product = new UserProduct
            {
                Username = informations[0],
                ProductId = Int32.Parse(informations[1])
            };

            Console.WriteLine("Create User Product: " + ctr_userproduct.Delete(user_product, instance_use));
        }
    }
}