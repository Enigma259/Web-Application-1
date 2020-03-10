using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary1.Database;
using ClassLibrary1.Model;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Unit_Test.Database
{
    [TestClass]
    public class CL_UserProduct
    {
        #region Instances

        private DB_UserProduct db_userproduct;
        private DBCustomUser db_user;
        private DB_Product db_product;
        private CustomUser user;
        private List<Product> products;

        #endregion

        #region Unit Test functions

        [TestInitialize]
        public void Initialize()
        {
            int index = 0;
            int max_index = 100;
            db_user = DBCustomUser.GetInstance();
            db_product = DB_Product.GetInstance();
            db_userproduct = DB_UserProduct.GetInstance();
            Product product;

            user = new CustomUser
            {
                Username = "ClassLibriry1 - Test UserProduct Database - User ",
                Password = "ClassLibriry1 - Test UserProduct Database - User",
                Email = "ClassLibriry1 - Test UserProduct Database - User",
                Wallet = 5000.0,
                IsActive = true,
                LoggedIn = true
            };

            db_user.CreateUser(user);

            while (index < max_index)
            {
                product = new Product
                {
                    Name = "ClassLibriry1 - Test UserProduct Database - Product",
                    Price = 50.0 + (double)index
                };
                
                db_product.CreateProduct(product);
                index++;
            }
            
            products = db_product.FindProductByPriceHigher(25.0);
        }

        [TestCleanup]
        public void Cleanup()
        {
            List<UserProduct> user_products;

            db_userproduct.DeleteByUsername(user.Username);

            foreach (Product product in products)
            {
                user_products = db_userproduct.FindUserProductByProductId(product.Id);

                foreach (UserProduct user_product in user_products)
                {
                    db_userproduct.Delete(user_product);
                }

                db_product.DeleteProduct(product.Id);
            }
        }

        #endregion

        #region Success functions

        [TestMethod]
        public void Success_CreateUserProduct()
        {
            //Arrange
            int index;
            string result;
            UserProduct user_product;

            //Act
            index = 0;
            user_product = new UserProduct
            {
                ProductId = products[index].Id,
                Username = user.Username,
                IsActive = false
            };

            result = db_userproduct.CreateUserProduct(user_product);

            //Assert
            Assert.AreEqual("Success", result);
        }

        [TestMethod]
        public void Success_ListAllUserProducts()
        {
            //Arrange
            int index;
            int max_index;
            UserProduct user_product;
            List<UserProduct> user_products;

            //Act
            index = 1;
            max_index = 51;

            while (index < max_index)
            {
                user_product = new UserProduct
                {
                    ProductId = products[index].Id,
                    Username = user.Username,
                    IsActive = false
                };

                db_userproduct.CreateUserProduct(user_product);
                index++;
            }

            user_products = db_userproduct.ListAllUserProducts();


            //Assert
            Assert.IsTrue(user_products.Count >= 50);
        }

        [TestMethod]
        public void Success_FindUserProduct()
        {
            //Arrange
            int index;
            UserProduct user_product;
            UserProduct result;

            //Act
            index = 52;

            user_product = new UserProduct
            {
                ProductId = products[index].Id,
                Username = user.Username,
                IsActive = false
            };

            db_userproduct.CreateUserProduct(user_product);

            result = db_userproduct.FindUserProduct(user_product);


            //Assert
            Assert.AreEqual(user_product.Username, result.Username);
        }

        [TestMethod]
        public void Success_FindUserProductByUsername()
        {
            //Arrange
            int index;
            UserProduct user_product;
            UserProduct result;

            //Act
            index = 53;

            user_product = new UserProduct
            {
                ProductId = products[index].Id,
                Username = user.Username,
                IsActive = false
            };

            db_userproduct.CreateUserProduct(user_product);

            result = db_userproduct.FindUserProductByUsername(user.Username)[0];

            //Assert
            Assert.AreEqual(user_product.Username, result.Username);
        }

        [TestMethod]
        public void Success_FindUserProductByProductId()
        {
            //Arrange
            int index;
            UserProduct user_product;
            UserProduct result;

            //Act
            index = 54;

            user_product = new UserProduct
            {
                ProductId = products[index].Id,
                Username = user.Username,
                IsActive = false
            };

            db_userproduct.CreateUserProduct(user_product);

            result = db_userproduct.FindUserProductByProductId(products[index].Id)[0];

            //Assert
            Assert.AreEqual(user_product.ProductId, result.ProductId);
        }

        [TestMethod]
        public void Success_FindUserProductByIsActive()
        {
            //Arrange
            int index;
            UserProduct user_product;
            UserProduct result;

            //Act
            index = 55;

            user_product = new UserProduct
            {
                ProductId = products[index].Id,
                Username = user.Username,
                IsActive = true
            };

            db_userproduct.CreateUserProduct(user_product);

            result = db_userproduct.FindUserProductByIsActive(user.Username, true)[0];

            //Assert
            Assert.AreEqual(user_product.IsActive, result.IsActive);
        }

        [TestMethod]
        public void Success_UpdateUserProduct()
        {
            //Arrange
            int index;
            string result;
            UserProduct old_user_product;
            UserProduct new_user_product;

            //Act
            index = 56;
            old_user_product = new UserProduct
            {
                ProductId = products[index].Id,
                Username = user.Username,
                IsActive = false
            };

            result = db_userproduct.CreateUserProduct(old_user_product);

            if(result.Equals("Success"))
            {
                index++;
                new_user_product = new UserProduct
                {
                    ProductId = products[index].Id,
                    Username = user.Username
                };

                result = db_userproduct.UpdateUserProduct(old_user_product, new_user_product);
            }

            //Assert
            Assert.AreEqual("Success", result);
        }

        [TestMethod]
        public void Success_DeleteByUsername()
        {
            int index;
            string result;
            UserProduct user_product;

            //Act
            index = 58;
            user_product = new UserProduct
            {
                ProductId = products[index].Id,
                Username = user.Username,
                IsActive = false
            };

            result = db_userproduct.CreateUserProduct(user_product);

            if (result.Equals("Success"))
            {
                result = db_userproduct.DeleteByUsername(user_product.Username);
            }

            //Assert
            Assert.AreEqual("Success", result);
        }

        [TestMethod]
        public void Success_DeleteByProductId()
        {
            int index;
            string result;
            UserProduct user_product;

            //Act
            index = 59;
            user_product = new UserProduct
            {
                ProductId = products[index].Id,
                Username = user.Username,
                IsActive = false
            };

            result = db_userproduct.CreateUserProduct(user_product);

            if (result.Equals("Success"))
            {
                result = db_userproduct.DeleteByProductId(user_product.ProductId);
            }

            //Assert
            Assert.AreEqual("Success", result);
        }

        [TestMethod]
        public void Success_Delete()
        {
            int index;
            string result;
            UserProduct user_product;

            //Act
            index = 60;
            user_product = new UserProduct
            {
                ProductId = products[index].Id,
                Username = user.Username,
                IsActive = false
            };

            result = db_userproduct.CreateUserProduct(user_product);

            if (result.Equals("Success"))
            {
                result = db_userproduct.Delete(user_product);
            }

            //Assert
            Assert.AreEqual("Success", result);
        }

        #endregion
    }
}