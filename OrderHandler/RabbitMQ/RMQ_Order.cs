using ClassLibrary1.Controller;
using ClassLibrary1.Model;
using System;
using System.Collections.Generic;

namespace OrderHandler.RabbitMQ
{
    /// <summary>
    /// This is the class RMQ_Order.
    /// </summary>
    public sealed class RMQ_Order
    {
        private static volatile RMQ_Order _instance;
        private static readonly object syncRoot = new object();
        private CTR_Order ctr_order;
        private CTR_OrderDetail ctr_orderdetail;
        private CTR_UserProduct ctr_userproduct;
        private CustomUserController ctr_user;

        /// <summary>
        /// This is the constructor for the class RMQ_Order.
        /// </summary>
        private RMQ_Order()
        {
            ctr_order = new CTR_Order();
            ctr_orderdetail = new CTR_OrderDetail();
            ctr_userproduct = new CTR_UserProduct();
            ctr_user = new CustomUserController();
        }

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

        /// <summary>
        /// Functions for managing orders.
        /// </summary>
        /// <param name="informations"></param>
        public void OrderFunctions(List<string> informations)
        {
            int index = 1;
            List<string> new_informations = new List<string>();

            while (index < informations.Count)
            {
                if(index > 1)
                {
                    new_informations.Add(informations[index]);
                }

                index++;
            }

            switch (informations[1])
            {
                case "Create":
                    CreateOrder(new_informations);
                    break;

                case "ListAll":
                    //insert code here
                    break;

                case "Find - Id":
                    //insert code here
                    break;

                case "Find - Price":
                    //insert code here
                    break;

                case "Find - Is Shipped":
                    //insert code here
                    break;

                case "Find - Username":
                    //insert code here
                    break;

                case "Update":
                    UpdateOrder(new_informations);
                    break;

                case "Delete":
                    DeleteOrder(new_informations);
                    break;

                default:
                    Console.WriteLine("The function is not available: " + informations[1]);
                    break;

            }
        }

        /// <summary>
        /// Creates an order.
        /// </summary>
        /// <param name="informations"></param>
        public void CreateOrder(List<string> informations)
        {
            int index = 0;
            string username = "";
            List<int> products = new List<int>();

            while (index < informations.Count)
            {
                if (index < 1)
                {
                    username = informations[index];
                }

                else
                {
                    products.Add(Int32.Parse(informations[index]));
                }

                index++;
            }

            Console.WriteLine("Create order: " + ctr_order.Create(username, products));
        }

        /// <summary>
        /// Updates an order.
        /// </summary>
        /// <param name="informations"></param>
        public void UpdateOrder(List<string> informations)
        {
            Order order = new Order
            {
                Id = Int32.Parse(informations[0]),
                Price = Double.Parse(informations[1]),
                IsShipped = Boolean.Parse(informations[2]),
                Username = informations[3]
            };

            Console.WriteLine("Update order: " + ctr_order.Update(order));
        }

        /// <summary>
        /// Deletes an order.
        /// </summary>
        /// <param name="informations"></param>
        public void DeleteOrder(List<string> informations)
        {
            int id = Int32.Parse(informations[0]);

            Console.WriteLine("Delete order: " + ctr_order.Delete(id, "Order"));
        }
    }
}