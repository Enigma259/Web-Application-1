using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary1.Database;
using ClassLibrary1.Model;
using System.Collections.Generic;

namespace Unit_Test.Database
{
    [TestClass]
    public class CL_Product
    {
        #region Instances

        private DB_Product db_product;
        private double product_price;

        #endregion

        #region Unit Test functions.

        [TestInitialize]
        public void Initialize()
        {
            db_product = DB_Product.GetInstance();
            product_price = 1000.0;
        }

        [TestCleanup]
        public void Cleanup()
        {
            List<Product> products = db_product.FindProductByPriceHigher(999.0);

            foreach (Product product in products)
            {
                db_product.DeleteProduct(product.Id);
            }
        }

        #endregion

        #region Success functions

        [TestMethod]
        public void Success_CreateProduct()
        {
            //Arrange
            int index;
            string result;
            Product product;

            //Act
            index = 0;
            product = new Product
            {
                Name = "ClassLibrary1 - Test product database - CreatePtoduct",
                Price = product_price + index
            };

            result = db_product.CreateProduct(product);

            //Assert
            Assert.AreEqual("Success", result);
        }

        [TestMethod]
        public void Success_ListAllProducts()
        {
            //Arrange
            int index;
            Product product;
            List<Product> result;

            //Act
            index = 1;

            while (index < 500)
            {
                product = new Product
                {
                    Name = "ClassLibrary1 - Test product database - ListAllProducts",
                    Price = product_price + index
                };

                db_product.CreateProduct(product);
                index++;
            }

            result = db_product.ListAllProducts();

            //Assert
            Assert.IsTrue(result.Count >= 500, result[0].Name);
        }

        [TestMethod]
        public void Success_FindProductById()
        {
            //Arrange
            int index;
            Product product;
            Product result;

            //Act
            index = 500;

            product = new Product
            {
                Name = "ClassLibrary1 - Test product database - FindProductById",
                Price = product_price + index
            };

            db_product.CreateProduct(product);

            product = db_product.FindProductByName(product.Name)[0];
            result = db_product.FindProductById(product.Id);

            //Assert
            Assert.AreEqual(product.Id, result.Id);
        }

        [TestMethod]
        public void Success_FindProductByName()
        {
            //Arrange
            int index;
            Product product;
            Product result;

            //Act
            index = 501;

            product = new Product
            {
                Name = "ClassLibrary1 - Test product database - FindProductByName",
                Price = product_price + index
            };

            db_product.CreateProduct(product);

            result = db_product.FindProductByName(product.Name)[0];

            //Assert
            Assert.AreEqual(product.Name, result.Name);
        }

        [TestMethod]
        public void Success_FindProductByPrice()
        {
            //Arrange
            int index;
            Product product;
            Product result;

            //Act
            index = 501;

            product = new Product
            {
                Name = "ClassLibrary1 - Test product database - FindProductByPrice",
                Price = product_price + index
            };

            db_product.CreateProduct(product);

            result = db_product.FindProductByPrice(product.Price)[0];

            //Assert
            Assert.AreEqual(product.Price, result.Price);
        }

        [TestMethod]
        public void Success_FindProductByPriceHigher()
        {
            //Arrange
            int index;
            Product product;
            Product result;

            //Act
            index = 502;

            product = new Product
            {
                Name = "ClassLibrary1 - Test product database - FindProductByPriceHigher",
                Price = product_price + index
            };

            db_product.CreateProduct(product);

            result = db_product.FindProductByPriceHigher(1500)[0];

            //Assert
            Assert.IsTrue(product.Price == result.Price, result.Name);
        }

        [TestMethod]
        public void Success_FindProductByPriceLower()
        {
            //Arrange
            int index;
            Product product;
            Product result;

            //Act
            index = 503;

            product = new Product
            {
                Name = "ClassLibrary1 - Test product database - FindProductByPriceLower",
                Price = product_price + index
            };

            db_product.CreateProduct(product);

            result = db_product.FindProductByPriceLower(product.Price)[0];

            //Assert
            Assert.IsTrue(2000 > result.Price, result.Name);
        }

        [TestMethod]
        public void Success_UpdateProduct()
        {
            //Arrange
            int index;
            string result;
            Product product;

            //Act
            index = 504;
            product = new Product
            {
                Name = "ClassLibrary1 - Test product database - UpdateProduct",
                Price = product_price + index
            };

            result = db_product.CreateProduct(product);

            if(result.Equals("Success"))
            {
                product.Price += 0.5;
                product.Id = db_product.FindProductByName(product.Name)[0].Id;

                result = db_product.UpdateProduct(product);
            }

            //Assert
            Assert.AreEqual("Success", result);
        }

        [TestMethod]
        public void Success_DeleteProduct()
        {
            //Arrange
            int index;
            string result;
            Product product;

            //Act
            index = 504;
            product = new Product
            {
                Name = "ClassLibrary1 - Test product database - DeleteProduct",
                Price = product_price + index
            };

            result = db_product.CreateProduct(product);

            if (result.Equals("Success"))
            {
                product.Id = db_product.FindProductByName(product.Name)[0].Id;

                result = db_product.DeleteProduct(product.Id);
            }

            //Assert
            Assert.AreEqual("Success", result);
        }

        #endregion
    }
}