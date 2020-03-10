using ClassLibrary1.Controller;
using ClassLibrary1.Model;
using System;
using System.Collections.Generic;

namespace OrderHandler.RabbitMQ
{
    /// <summary>
    /// This is the class RMQ_Product.
    /// </summary>
    public sealed class RMQ_Product
    {
        private static volatile RMQ_Product _instance;
        private static readonly object syncRoot = new object();
        private CTR_Product ctr_product;

        /// <summary>
        /// This is the constructor for the class RMQ_Product.
        /// </summary>
        private RMQ_Product()
        {
            ctr_product = new CTR_Product();
        }

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

        /// <summary>
        /// Functions for managing orders.
        /// </summary>
        /// <param name="informations"></param>
        public void ProductFunctions(List<string> informations)
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
                    CreateProduct(new_informations);
                    break;

                case "ListAll":
                    //insert code here
                    break;

                case "Find - Id":
                    //insert code here
                    break;

                case "Find - Name":
                    //insert code here
                    break;

                case "Find - Price":
                    //insert code here
                    break;

                case "Update":
                    UpdateProduct(new_informations);
                    break;

                case "Delete":
                    DeleteProduct(new_informations);
                    break;

                default:
                    Console.WriteLine("The function is not available: " + informations[1]);
                    break;

            }
        }

        /// <summary>
        /// Creates a product.
        /// </summary>
        /// <param name="informations"></param>
        public void CreateProduct(List<string> informations)
        {
            Product product = new Product
            {
                Name = informations[0],
                Price = Double.Parse(informations[1])
            };

            Console.WriteLine("Create Product: " + ctr_product.Create(product));
        }

        /// <summary>
        /// Updates a product.
        /// </summary>
        /// <param name="informations"></param>
        public void UpdateProduct(List<string> informations)
        {
            Product product = new Product
            {
                Id = Int32.Parse(informations[0]),
                Name = informations[1],
                Price = Double.Parse(informations[2])
            };

            Console.WriteLine("Update Product: " + ctr_product.Update(product));
        }

        /// <summary>
        /// Deletes a product.
        /// </summary>
        /// <param name="informations"></param>
        public void DeleteProduct(List<string> informations)
        {
            int id = Int32.Parse(informations[0]);

            Console.WriteLine("Delete Product: " + ctr_product.Delete(id, "Product"));
        }
    }
}