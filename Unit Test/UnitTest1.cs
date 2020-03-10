using ClassLibrary1.Controller;
using ClassLibrary1.Model;
using WebAPI.RabbitMQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Unit_Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AreEqual()
        {
            Assert.AreEqual("Are Equal", "Are Equal");
        }

        [TestMethod]
        public void AreNotEqual()
        {
            Assert.AreNotEqual("Are Not Equal", "Are Equal");
        }

        [TestMethod]
        public void AreNotSame()
        {
            Assert.AreNotSame("Are Not The Same", "Are The Same");
        }

        [TestMethod]
        public void AreSame()
        {
            Assert.AreSame("Are The Same", "Are The Same");
        }

        [TestMethod]
        public void IsFalse()
        {
            Assert.IsFalse(false);
        }

        [TestMethod]
        public void IsNotNull()
        {
            Assert.IsNotNull(new object());
        }

        [TestMethod]
        public void IsNull()
        {
            Assert.IsNull(null);
        }

        [TestMethod]
        public void IsTrue()
        {
            TestProducts();
            Assert.IsTrue(true);
        }

        public void TestProducts()
        {
            CTR_Product ctr_product = new CTR_Product();
            List<Product> products = ListOfProducts();

            foreach(Product product in products)
            {
                ctr_product.Create(product);
            }
        }

        public List<Product> ListOfProducts()
        {
            List<Product> products = new List<Product>();

            Product product_1 = new Product();
            product_1.Name = "Blue Hat";
            product_1.Price = 50.0;
            Product product_2 = new Product();
            product_2.Name = "Yellow Hat";
            product_2.Price = 50.0;
            Product product_3 = new Product();
            product_3.Name = "Red Hat";
            product_3.Price = 50.0;
            Product product_4 = new Product();
            product_4.Name = "Purple Hat";
            product_4.Price = 50.0;
            Product product_5 = new Product();
            product_5.Name = "Green Hat";
            product_5.Price = 50.0;
            Product product_6 = new Product();
            product_6.Name = "Orange Hat";
            product_6.Price = 50.0;
            Product product_7 = new Product();
            product_7.Name = "White Hat";
            product_7.Price = 50.0;
            Product product_8 = new Product();
            product_8.Name = "Grey Hat";
            product_8.Price = 50.0;
            Product product_9 = new Product();
            product_9.Name = "Black Hat";
            product_9.Price = 50.0;
            
            Product product_10 = new Product();
            product_10.Name = "Blue Scarf";
            product_10.Price = 50.0;
            Product product_11 = new Product();
            product_11.Name = "Yellow Scarf";
            product_11.Price = 50.0;
            Product product_12 = new Product();
            product_12.Name = "Red Scarf";
            product_12.Price = 50.0;
            Product product_13 = new Product();
            product_13.Name = "Purple Scarf";
            product_13.Price = 50.0;
            Product product_14 = new Product();
            product_14.Name = "Green Scarf";
            product_14.Price = 50.0;
            Product product_15 = new Product();
            product_15.Name = "Orange Scarf";
            product_15.Price = 50.0;
            Product product_16 = new Product();
            product_16.Name = "White Scarf";
            product_16.Price = 50.0;
            Product product_17 = new Product();
            product_17.Name = "Grey Scarf";
            product_17.Price = 50.0;
            Product product_18 = new Product();
            product_18.Name = "Black Scarf";
            product_18.Price = 50.0;

            products.Add(product_1);
            products.Add(product_2);
            products.Add(product_3);
            products.Add(product_4);
            products.Add(product_5);
            products.Add(product_6);
            products.Add(product_7);
            products.Add(product_8);
            products.Add(product_9);
            products.Add(product_10);
            products.Add(product_11);
            products.Add(product_12);
            products.Add(product_13);
            products.Add(product_14);
            products.Add(product_15);
            products.Add(product_16);
            products.Add(product_17);
            products.Add(product_18);

            return products;
        }
    }
}