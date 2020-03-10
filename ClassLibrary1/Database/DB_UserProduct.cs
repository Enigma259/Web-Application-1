using ClassLibrary1.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ClassLibrary1.Database
{
    /// <summary>
    /// This is the class DBUserProduct.
    /// </summary>
    public sealed class DB_UserProduct
    {
        #region Instances

        private static volatile DB_UserProduct _instance;
        private static object syncRoot = new object();
        private DatabaseString db;
        public PasswordHasher pw;

        #endregion

        /// <summary>
        /// This is the constructor for the class DBUserProduct.
        /// </summary>
        private DB_UserProduct()
        {
            db = DatabaseString.GetInstance();
            pw = new PasswordHasher();
        }

        #region Singleton funtions

        /// <summary>
        /// This is a multi threaded singleton for the class DBUserProduct.
        /// </summary>
        /// <returns>_instance</returns>
        public static DB_UserProduct GetInstance()
        {
            if (_instance == null)
            {
                lock (syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new DB_UserProduct();
                    }
                }
            }

            return _instance;
        }

        #endregion

        #region Create funtions

        /// <summary>
        /// Creates a user product in the database.
        /// </summary>
        /// <param name="user_product"></param>
        /// <returns>string</returns>
        public string CreateUserProduct(UserProduct user_product)
        {
            string result;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "INSERT INTO Table_UserProduct (username, productId, isActive) VALUES(@username, @productId, @isActive)";
                    cmd.Parameters.AddWithValue("username", user_product.Username);
                    cmd.Parameters.AddWithValue("productId", user_product.ProductId);
                    cmd.Parameters.AddWithValue("isActive", user_product.IsActive);
                    cmd.ExecuteNonQuery();
                    connection.Close();

                    result = "Success";
                }
            }

            catch (Exception exception)
            {
                result = UserProductException(exception).Username;
            }

            return result;
        }

        #endregion

        #region Search funtions

        /// <summary>
        /// List all user products from the database.
        /// </summary>
        /// <returns>List<UserProduct></returns>
        public List<UserProduct> ListAllUserProducts()
        {
            List<UserProduct> user_products;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "SELECT * FROM Table_UserProduct";

                    var reader = cmd.ExecuteReader();
                    user_products = new List<UserProduct>();

                    while (reader.Read())
                    {
                        UserProduct user_product = new UserProduct
                        {
                            ProductId = reader.GetInt32(reader.GetOrdinal("productId")),
                            Username = reader.GetString(reader.GetOrdinal("username")),
                            IsActive = reader.GetBoolean(reader.GetOrdinal("isActive"))
                        };
                        user_products.Add(user_product);
                    }

                    connection.Close();
                }
            }

            catch (Exception exception)
            {
                user_products = new List<UserProduct>
                {
                    UserProductException(exception)
                };
            }

            return user_products;
        }

        /// <summary>
        /// Finds a user products by their username and productId.
        /// </summary>
        /// <param name="user_product"></param>
        /// <returns>UserProduct</returns>
        public UserProduct FindUserProduct(UserProduct user_product)
        {
            UserProduct result;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "SELECT * FROM Table_UserProduct WHERE username = @username AND productId = @productId";
                    cmd.Parameters.AddWithValue("username", user_product.Username);
                    cmd.Parameters.AddWithValue("productId", user_product.ProductId);

                    var reader = cmd.ExecuteReader();
                    reader.Read();
                    result = new UserProduct
                    {
                        ProductId = reader.GetInt32(reader.GetOrdinal("productId")),
                        Username = reader.GetString(reader.GetOrdinal("username")),
                        IsActive = reader.GetBoolean(reader.GetOrdinal("isActive"))
                    };

                    connection.Close();
                }
            }

            catch (Exception exception)
            {
                result = UserProductException(exception);
            }

            return result;
        }

        /// <summary>
        /// Finds a list of user products by their username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>List<UserProduct></returns>
        public List<UserProduct> FindUserProductByUsername(string username)
        {
            List<UserProduct> user_products;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "SELECT * FROM Table_UserProduct p WHERE p.username = @username";
                    cmd.Parameters.AddWithValue("username", username);

                    var reader = cmd.ExecuteReader();
                    user_products = new List<UserProduct>();

                    while (reader.Read())
                    {
                        UserProduct user_product = new UserProduct
                        {
                            ProductId = reader.GetInt32(reader.GetOrdinal("productId")),
                            Username = reader.GetString(reader.GetOrdinal("username")),
                            IsActive = reader.GetBoolean(reader.GetOrdinal("isActive"))
                        };
                        user_products.Add(user_product);
                    }

                    connection.Close();
                }
            }

            catch (Exception exception)
            {
                user_products = new List<UserProduct>
                {
                    UserProductException(exception)
                };
            }

            return user_products;
        }

        /// <summary>
        /// Finds a list of user products by their product_id.
        /// </summary>
        /// <param name="product_id"></param>
        /// <returns>List<UserProduct></returns>
        public List<UserProduct> FindUserProductByProductId(int product_id)
        {
            List<UserProduct> user_products;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "SELECT * FROM Table_UserProduct p WHERE p.productId = @productId";
                    cmd.Parameters.AddWithValue("productId", product_id);

                    var reader = cmd.ExecuteReader();
                    user_products = new List<UserProduct>();

                    while (reader.Read())
                    {
                        UserProduct user_product = new UserProduct
                        {
                            ProductId = reader.GetInt32(reader.GetOrdinal("productId")),
                            Username = reader.GetString(reader.GetOrdinal("username")),
                            IsActive = reader.GetBoolean(reader.GetOrdinal("isActive"))
                        };
                        user_products.Add(user_product);
                    }

                    connection.Close();
                }
            }

            catch (Exception exception)
            {
                user_products = new List<UserProduct>
                {
                    UserProductException(exception)
                };
            }

            return user_products;
        }

        /// <summary>
        /// Finds a list of user products by their is_active.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="is_active"></param>
        /// <returns>List<UserProduct></returns>
        public List<UserProduct> FindUserProductByIsActive(string username, bool is_active)
        {
            List<UserProduct> user_products;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "SELECT * FROM Table_UserProduct p WHERE p.isActive = @isActive AND p.username = @username";
                    cmd.Parameters.AddWithValue("isActive", is_active);
                    cmd.Parameters.AddWithValue("username", username);

                    var reader = cmd.ExecuteReader();
                    user_products = new List<UserProduct>();

                    while (reader.Read())
                    {
                        UserProduct user_product = new UserProduct
                        {
                            ProductId = reader.GetInt32(reader.GetOrdinal("productId")),
                            Username = reader.GetString(reader.GetOrdinal("username")),
                            IsActive = reader.GetBoolean(reader.GetOrdinal("isActive"))
                        };
                        user_products.Add(user_product);
                    }

                    connection.Close();
                }
            }

            catch (Exception exception)
            {
                user_products = new List<UserProduct>
                {
                    UserProductException(exception)
                };
            }

            return user_products;
        }

        /// <summary>
        /// Finds a list of userproducts owned by a specific user in the database.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>List<OwnedProducts></returns>
        public List<OwnedProducts> GetOwnedProducts(string username)
        {
            List<OwnedProducts> owned_products;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "SELECT * from Table_Product p INNER JOIN Table_UserProduct up ON p.id = up.productId WHERE up.username = @input";
                    cmd.Parameters.AddWithValue("input", username);

                    var reader = cmd.ExecuteReader();
                    owned_products = new List<OwnedProducts>();

                    while (reader.Read())
                    {
                        OwnedProducts owned_product = new OwnedProducts
                        {
                            IsActive = reader.GetBoolean(reader.GetOrdinal("isActive")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            ProductId = reader.GetInt32(reader.GetOrdinal("productId"))
                        };
                        owned_products.Add(owned_product);
                    }

                    connection.Close();
                }
            }

            catch (Exception exception)
            {
                owned_products = new List<OwnedProducts>
                {
                    OwnedProductException(exception)
                };
            }

            return owned_products;
        }

        #endregion

        #region Update funtions

        /// <summary>
        /// Updates a user product in the database.
        /// </summary>
        /// <param name="old_user_product"></param>
        /// <param name="new_user_product"></param>
        /// <returns>string</returns>
        public string UpdateUserProduct(UserProduct old_user_product, UserProduct new_user_product)
        {
            string result;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "UPDATE Table_UserProduct SET username = @new_username, productId = @new_productId, isActive = @new_isActive WHERE username = @old_username AND productId = @old_productId";
                    cmd.Parameters.AddWithValue("old_productId", old_user_product.ProductId);
                    cmd.Parameters.AddWithValue("old_username", old_user_product.Username);
                    cmd.Parameters.AddWithValue("new_productId", new_user_product.ProductId);
                    cmd.Parameters.AddWithValue("new_username", new_user_product.Username);
                    cmd.Parameters.AddWithValue("new_isActive", new_user_product.IsActive);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }

                result = "Success";
            }

            catch (Exception exception)
            {
                result = UserProductException(exception).Username;
            }

            return result;
        }

        /// <summary>
        /// Activates a user product in the database.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="id"></param>
        public void ActivateProduct(CustomUser user, string id)
        {
            if (pw.ValidatePassword(user))
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "UPDATE Table_UserProduct SET isActive = 1 WHERE username = @username AND productId = @id";
                    cmd.Parameters.AddWithValue("username", user.Username);
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }            
        }

        /// <summary>
        /// Deactivates a user product in the database.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="id"></param>
        public void DeactivateProduct(CustomUser user, string id)
        {
            if (pw.ValidatePassword(user))
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "UPDATE Table_UserProduct SET isActive = 0 WHERE username = @username AND productId = @id";
                    cmd.Parameters.AddWithValue("username", user.Username);
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        #endregion

        #region Delete funtions

        /// <summary>
        /// Deletes a list of user product by their username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>string</returns>
        public string DeleteByUsername(string username)
        {
            string result;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "DELETE FROM Table_UserProduct WHERE username = @username";
                    cmd.Parameters.AddWithValue("username", username);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }

                result = "Success";
            }

            catch (Exception exception)
            {
                result = UserProductException(exception).Username;
            }

            return result;
        }

        /// <summary>
        /// Deletes a list of user product by their product_id.
        /// </summary>
        /// <param name="product_id"></param>
        /// <returns>string</returns>
        public string DeleteByProductId(int product_id)
        {
            string result;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "DELETE FROM Table_UserProduct WHERE productId = @productId";
                    cmd.Parameters.AddWithValue("productId", product_id);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }

                result = "Success";
            }

            catch (Exception exception)
            {
                result = UserProductException(exception).Username;
            }

            return result;
        }

        /// <summary>
        /// Deletes a user product by its username and product_id.
        /// </summary>
        /// <param name="product_id"></param>
        /// <returns>string</returns>
        public string Delete(UserProduct user_product)
        {
            string result;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "DELETE FROM Table_UserProduct WHERE username = @username AND productId = @productId";
                    cmd.Parameters.AddWithValue("username", user_product.Username);
                    cmd.Parameters.AddWithValue("productId", user_product.ProductId);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }

                result = "Success";
            }

            catch (Exception exception)
            {
                result = UserProductException(exception).Username;
            }

            return result;
        }

        #endregion

        #region Exception funtions

        /// <summary>
        /// Returns a product with a exception message in it.
        /// </summary>
        /// <param name="exception"></param>
        /// <returns>Product</returns>
        private UserProduct UserProductException(Exception exception)
        {
            UserProduct user_product = new UserProduct();

            user_product.Username = exception.Message;
            user_product.ProductId = -5;

            return user_product;
        }

        /// <summary>
        /// Returns a owned product with a exception message in it.
        /// </summary>
        /// <param name="exception"></param>
        /// <returns>Product</returns>
        private OwnedProducts OwnedProductException(Exception exception)
        {
            OwnedProducts owned_product = new OwnedProducts
            {
                IsActive = false,
                Name = exception.Message,
                ProductId = -5
            };

            return owned_product;
        }

        #endregion
    }
}