using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary1.Model;
using ClassLibrary1.Database;

namespace UnitTestProject1.Database
{
    /// <summary>
    /// Summary description for UT_DBProduct
    /// </summary>
    [TestClass]
    public class UT_DBProduct
    {
        DBProduct product_db;

        /// <summary>
        /// initiates the necessary things to the tests can work.
        /// </summary>
        [TestInitialize]
        public void ProductInitialize()
        {
            product_db = DBProduct.GetInstance();
        }

        /// <summary>
        /// Tests if you can create a product.
        /// </summary>
        [TestMethod]
        public void Success_Create()
        {
            //Arrange
            Product product;
            string result;

            //Act
            product = new Product();
            product.Name = "Test Create Product";
            product.Price = 500.75;

            result = product_db.CreateProduct(product);

            //Assert
            Assert.AreEqual("Success", result);
        }

        /// <summary>
        /// Tests if you can get a list of products.
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
                product.Name = "Test List All products " + index;
                product.Price = 500.75 + index;
                product_db.CreateProduct(product);
                index++;
            }

            result = product_db.ListAllProducts();

            //Assert
            Assert.IsTrue(result.Count >= max_products);
        }

        /// <summary>
        /// Tests if you can find a product by its id.
        /// </summary>
        [TestMethod]
        public void Success_FindById()
        {
            //Arrange
            Product expected_proudct;
            Product actual_proudct;

            //Act
            expected_proudct = new Product();
            expected_proudct.Name = "Test Find By Id";
            expected_proudct.Price = 500.75;
            product_db.CreateProduct(expected_proudct);
            expected_proudct.Id = product_db.FindProductByName(expected_proudct.Name)[0].Id;
            actual_proudct = product_db.FindProductById(expected_proudct.Id);

            //Assert
            Assert.AreEqual(expected_proudct.Name, actual_proudct.Name);
        }

        /// <summary>
        /// Tests if you can find a list of products by its name.
        /// </summary>
        [TestMethod]
        public void Success_FindByName()
        {
            //Arrange
            Product expected_proudct;
            Product actual_proudct;

            //Act
            expected_proudct = new Product();
            expected_proudct.Name = "Test Find By Name";
            expected_proudct.Price = 500.75;
            product_db.CreateProduct(expected_proudct);
            actual_proudct = product_db.FindProductByName(expected_proudct.Name)[0];

            //Assert
            Assert.AreEqual(expected_proudct.Name, actual_proudct.Name);
        }

        /// <summary>
        /// Tests if you can find a list of products by its price.
        /// </summary>
        [TestMethod]
        public void Success_FindByPrice()
        {
            //Arrange
            Product expected_proudct;
            Product actual_proudct;

            //Act
            expected_proudct = new Product();
            expected_proudct.Name = "Test Find By Price";
            expected_proudct.Price = 50;
            product_db.CreateProduct(expected_proudct);
            actual_proudct = product_db.FindProductByPrice(expected_proudct.Price)[0];

            //Assert
            Assert.AreEqual(expected_proudct.Name, actual_proudct.Name);
        }

        /// <summary>
        /// Tests if you can find a list of products where the price is higher than the given price.
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
                expected_proudct.Name = "Test Find By Price Higher";
                expected_proudct.Price = 1000 * index;
                product_db.CreateProduct(expected_proudct);
                index++;
            }

            products = product_db.FindProductByPriceHigher(150);

            //Assert
            Assert.IsTrue(products.Count >= 7);
        }

        /// <summary>
        /// Tests if you can find a list of products where the price is lower than the given price.
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
                expected_proudct.Name = "Test Find By Price Lower";
                expected_proudct.Price = 25 - index;
                product_db.CreateProduct(expected_proudct);
                index++;
            }

            products = product_db.FindProductByPriceLower(21);

            //Assert
            Assert.IsTrue(products.Count >= 4);
        }

        /// <summary>
        /// Tests if you can update a product.
        /// </summary>
        [TestMethod]
        public void Success_Update()
        {
            //Arrange
            Product prouduct;
            string result;

            //Act
            prouduct = new Product();
            prouduct.Name = "Test Update Product";
            prouduct.Price = 500.75;
            result = product_db.CreateProduct(prouduct);
            prouduct.Id = product_db.FindProductByName(prouduct.Name)[0].Id;

            if(result == "Success")
            {
                prouduct.Price = 75.0;

                result = product_db.UpdateProduct(prouduct);
            }

            //Assert
            Assert.AreEqual("Success", result);
        }

        /// <summary>
        /// Tests if you can delete a product.
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
            prouduct.Name = "Test Delete Product";
            prouduct.Price = 500.75;
            result = product_db.CreateProduct(prouduct);
            id = product_db.FindProductByName(prouduct.Name)[0].Id;

            if (result == "Success")
            {
                result = product_db.DeleteProduct(id);
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

            products = product_db.ListAllProducts();

            foreach(Product product in products)
            {
                product_db.DeleteProduct(product.Id);
            }
        }
    }
}
