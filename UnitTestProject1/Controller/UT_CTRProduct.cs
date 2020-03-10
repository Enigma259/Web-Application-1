using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary1.Model;
using ClassLibrary1.Controller;

namespace UnitTestProject1.Controller
{
    [TestClass]
    public class UT_CTRProduct
    {
        CTRProduct product_ctr;

        /// <summary>
        /// initiates the necessary things to the tests can work.
        /// </summary>
        [TestInitialize]
        public void ProductInitialize()
        {
            product_ctr = CTRProduct.GetInstance();
        }

        /// <summary>
        /// Tests if you can create a product from the controller.
        /// </summary>
        [TestMethod]
        public void Success_Create()
        {
            //Arrange
            Product product;
            string result;

            //Act
            product = new Product();
            product.Name = "Test Create Product - Controller";
            product.Price = 500.75;

            result = product_ctr.Create(product);

            //Assert
            Assert.AreEqual("Success", result);
        }

        /// <summary>
        /// Tests if you can get a list of products from the controller.
        /// </summary>
        [TestMethod]
        public void Success_ListAll()
        {
            //Arrange
            int index;
            int max_products;
            Product product;
            List<Product> result;

            //Act
            index = 0;
            max_products = 500;

            while (index < max_products)
            {
                product = new Product();
                product.Name = "Test List All Product - Controller " + index;
                product.Price = 500.75 + index;
                product_ctr.Create(product);
                index++;
            }

            result = product_ctr.ListAll();

            //Assert
            Assert.IsTrue(result.Count >= max_products);
        }

        /// <summary>
        /// Tests if you can find a product by its id from the controller.
        /// </summary>
        [TestMethod]
        public void Success_FindById()
        {
            //Arrange
            Product expected_proudct;
            Product actual_proudct;

            //Act
            expected_proudct = new Product();
            expected_proudct.Name = "Test Find By Id - Controller";
            expected_proudct.Price = 500.75;
            product_ctr.Create(expected_proudct);
            expected_proudct.Id = product_ctr.FindName(expected_proudct.Name)[0].Id;
            actual_proudct = product_ctr.FindId(expected_proudct.Id);

            //Assert
            Assert.AreEqual(expected_proudct.Name, actual_proudct.Name);
        }

        /// <summary>
        /// Tests if you can find a list of products by its name from the controller.
        /// </summary>
        [TestMethod]
        public void Success_FindByName()
        {
            //Arrange
            Product expected_proudct;
            Product actual_proudct;

            //Act
            expected_proudct = new Product();
            expected_proudct.Name = "Test Find By Name - Controller";
            expected_proudct.Price = 500.75;
            product_ctr.Create(expected_proudct);
            actual_proudct = product_ctr.FindName(expected_proudct.Name)[0];

            //Assert
            Assert.AreEqual(expected_proudct.Name, actual_proudct.Name);
        }

        /// <summary>
        /// Tests if you can find a list of products by its price from the controller.
        /// </summary>
        [TestMethod]
        public void Success_FindByPrice()
        {
            //Arrange
            Product expected_proudct;
            Product actual_proudct;

            //Act
            expected_proudct = new Product();
            expected_proudct.Name = "Test Find By Price - Controller";
            expected_proudct.Price = 50;
            product_ctr.Create(expected_proudct);
            actual_proudct = product_ctr.FindPrice(expected_proudct.Price, "Equal")[0];

            //Assert
            Assert.AreEqual(expected_proudct.Name, actual_proudct.Name);
        }

        /// <summary>
        /// Tests if you can find a list of products where the price is higher than the given price from the controller.
        /// </summary>
        [TestMethod]
        public void Success_FindByPriceHigher()
        {
            //Arrange
            int index;
            int max_products;
            Product expected_proudct;
            List<Product> products;

            //Act
            index = 0;
            max_products = 10;

            while (index < max_products)
            {
                expected_proudct = new Product();
                expected_proudct.Name = "Test Find By Price Higher - Controller";
                expected_proudct.Price = 1000 * index;
                product_ctr.Create(expected_proudct);
                index++;
            }

            products = product_ctr.FindPrice(150, "Higher");

            //Assert
            Assert.IsTrue(products.Count >= 7);
        }

        /// <summary>
        /// Tests if you can find a list of products where the price is lower than the given price from the controller.
        /// </summary>
        [TestMethod]
        public void Success_FindByPriceLower()
        {
            //Arrange
            int index;
            int max_products;
            Product expected_proudct;
            List<Product> products;

            //Act
            index = 0;
            max_products = 10;

            while (index < max_products)
            {
                expected_proudct = new Product();
                expected_proudct.Name = "Test Find By Price Lower - Controller";
                expected_proudct.Price = 25 - index;
                product_ctr.Create(expected_proudct);
                index++;
            }

            products = product_ctr.FindPrice(21, "Higher");

            //Assert
            Assert.IsTrue(products.Count >= 4);
        }

        /// <summary>
        /// Tests if you can update a product from the controller.
        /// </summary>
        [TestMethod]
        public void Success_Update()
        {
            //Arrange
            Product prouduct;
            string result;

            //Act
            prouduct = new Product();
            prouduct.Name = "Test Update Product - Controller";
            prouduct.Price = 500.75;
            result = product_ctr.Create(prouduct);
            prouduct.Id = product_ctr.FindName(prouduct.Name)[0].Id;

            if (result == "Success")
            {
                prouduct.Price = 75.0;

                result = product_ctr.Update(prouduct);
            }

            //Assert
            Assert.AreEqual("Success", result);
        }

        /// <summary>
        /// Tests if you can delete a product from the controller.
        /// </summary>
        [TestMethod]
        public void Success_Delete()
        {
            //Arrange
            Product prouduct;
            string result;
            int id;

            //Act
            prouduct = new Product();
            prouduct.Name = "Test Delete Product - Controller";
            prouduct.Price = 500.75;
            result = product_ctr.Create(prouduct);
            id = product_ctr.FindName(prouduct.Name)[0].Id;

            if (result == "Success")
            {
                result = product_ctr.Delete(id);
            }

            //Assert
            Assert.AreEqual("Success", result);
        }

        /// <summary>
        /// cleans up after the database product tests.
        /// </summary>
        [TestCleanup]
        public void ProductCleanup()
        {
            List<Product> products;

            products = product_ctr.ListAll();

            foreach (Product product in products)
            {
                product_ctr.Delete(product.Id);
            }
        }
    }
}
