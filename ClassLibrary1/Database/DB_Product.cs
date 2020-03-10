using ClassLibrary1.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ClassLibrary1.Database
{
    /// <summary>
    /// This is the class DBProduct
    /// </summary>
    public sealed class DB_Product
    {
        #region Instances

        private static volatile DB_Product _instance;
        private static object syncRoot = new object();
        private DatabaseString db;

        #endregion

        /// <summary>
        /// This is the constructor for the class DBProduct.
        /// </summary>
        private DB_Product()
        {
            db = DatabaseString.GetInstance();
        }

        #region Singleton funtions

        /// <summary>
        /// This is a multi threaded singleton for the class DBProduct.
        /// </summary>
        /// <returns>_instance</returns>
        public static DB_Product GetInstance()
        {
            if (_instance == null)
            {
                lock (syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new DB_Product();
                    }
                }
            }

            return _instance;
        }

        #endregion

        #region Create funtions

        /// <summary>
        /// Creates a product in the database.
        /// </summary>
        /// <param name="product"></param>
        /// <returns>string</returns>
        public string CreateProduct(Product product)
        {
            string result;

            try
            {
                if (product.Name == null || product.Price < 0.0)
                {
                    throw new ArgumentNullException("The product doesn't have any valid information");
                }

                else
                {
                    using (var connection = new SqlConnection(db.GetConnectionString()))
                    using (var cmd = connection.CreateCommand())
                    {
                        connection.Open();
                        cmd.CommandText = "INSERT INTO Table_Product (name, price) VALUES(@name, @price)";
                        cmd.Parameters.AddWithValue("name", product.Name);
                        cmd.Parameters.AddWithValue("price", product.Price);
                        cmd.ExecuteNonQuery();
                        connection.Close();

                        result = "Success";
                    }
                }
            }

            catch (Exception exception)
            {
                result = ProductException(exception).Name;
            }

            return result;
        }

        #endregion

        #region search funtions

        /// <summary>
        /// List all products from the database.
        /// </summary>
        /// <returns>List<Product></returns>
        public List<Product> ListAllProducts()
        {
            List<Product> products;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "SELECT * FROM Table_Product";

                    var reader = cmd.ExecuteReader();
                    products = new List<Product>();

                    while (reader.Read())
                    {
                        Product product = new Product
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            Price = reader.GetDouble(reader.GetOrdinal("price"))
                        };
                        products.Add(product);
                    }

                    connection.Close();
                }
            }

            catch (Exception exception)
            {
                products = new List<Product>
                {
                    ProductException(exception)
                };
            }


            return products;
        }

        /// <summary>
        /// Find the product by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Product</returns>
        public Product FindProductById(int id)
        {
            Product product;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "SELECT * FROM Table_product p WHERE p.Id = @id";
                    cmd.Parameters.AddWithValue("id", id);

                    var reader = cmd.ExecuteReader();
                    reader.Read();
                    product = new Product
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("id")),
                        Name = reader.GetString(reader.GetOrdinal("name")),
                        Price = reader.GetDouble(reader.GetOrdinal("price"))
                    };

                    connection.Close();

                }
            }

            catch (Exception exception)
            {
                product = ProductException(exception);
            }

            return product;
        }

        /// <summary>
        /// find a list og products by their name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>List<Product></returns>
        public List<Product> FindProductByName(string name)
        {
            List<Product> products;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "SELECT * FROM Table_Product p WHERE p.name = @name";
                    cmd.Parameters.AddWithValue("name", name);

                    var reader = cmd.ExecuteReader();
                    products = new List<Product>();

                    while (reader.Read())
                    {
                        Product product = new Product
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            Price = reader.GetDouble(reader.GetOrdinal("price"))
                        };
                        products.Add(product);
                    }

                    connection.Close();
                }
            }

            catch (Exception exception)
            {
                products = new List<Product>
                {
                    ProductException(exception)
                };
            }

            return products;
        }

        /// <summary>
        /// find a list og products by their price.
        /// </summary>
        /// <param name="price"></param>
        /// <returns>List<Product></returns>
        public List<Product> FindProductByPrice(double price)
        {
            List<Product> products;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "SELECT * FROM Table_Product p WHERE p.price = @price";
                    cmd.Parameters.AddWithValue("price", price);

                    var reader = cmd.ExecuteReader();
                    products = new List<Product>();

                    while (reader.Read())
                    {
                        Product product = new Product
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            Price = reader.GetDouble(reader.GetOrdinal("price"))
                        };
                        products.Add(product);
                    }

                    connection.Close();
                }
            }

            catch (Exception exception)
            {
                products = new List<Product>
                {
                    ProductException(exception)
                };
            }

            return products;
        }

        /// <summary>
        /// find a list of products where the price of the products is higher than the given price.
        /// </summary>
        /// <param name="price"></param>
        /// <returns>List<Product></returns>
        public List<Product> FindProductByPriceHigher(double price)
        {
            List<Product> products;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "SELECT * FROM Table_Product p WHERE p.price > @price";
                    cmd.Parameters.AddWithValue("price", price);

                    var reader = cmd.ExecuteReader();
                    products = new List<Product>();

                    while (reader.Read())
                    {
                        Product product = new Product
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            Price = reader.GetDouble(reader.GetOrdinal("price"))
                        };
                        products.Add(product);
                    }

                    connection.Close();
                }
            }

            catch (Exception exception)
            {
                products = new List<Product>
                {
                    ProductException(exception)
                };
            }

            return products;
        }

        /// <summary>
        /// find a list of products where the price of the products is lower than the given price.
        /// </summary>
        /// <param name="price"></param>
        /// <returns>List<Product></returns>
        public List<Product> FindProductByPriceLower(double price)
        {
            List<Product> products;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "SELECT * FROM Table_Product p WHERE p.price < @price";
                    cmd.Parameters.AddWithValue("price", price);

                    var reader = cmd.ExecuteReader();
                    products = new List<Product>();

                    while (reader.Read())
                    {
                        Product product = new Product
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            Price = reader.GetDouble(reader.GetOrdinal("price"))
                        };
                        products.Add(product);
                    }

                    connection.Close();
                }
            }

            catch (Exception exception)
            {
                products = new List<Product>
                {
                    ProductException(exception)
                };
            }

            return products;
        }

        #endregion

        #region Update funtions

        /// <summary>
        /// Updates a specific product in the database.
        /// </summary>
        /// <param name="product"></param>
        /// <returns>string</returns>
        public string UpdateProduct(Product product)
        {
            string result;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "UPDATE Table_Product SET name = @name, price = @price WHERE id = @id";
                    cmd.Parameters.AddWithValue("id", product.Id);
                    cmd.Parameters.AddWithValue("name", product.Name);
                    cmd.Parameters.AddWithValue("price", product.Price);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }

                result = "Success";
            }

            catch (Exception exception)
            {
                result = ProductException(exception).Name;
            }

            return result;
        }

        #endregion

        #region Delete funtions

        /// <summary>
        /// Deletes a specific product in the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>string</returns>
        public string DeleteProduct(int id)
        {
            string result;

            try
            {
                using (var connection = new SqlConnection(db.GetConnectionString()))
                using (var cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "DELETE FROM Table_Product WHERE id = @id";
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }

                result = "Success";
            }

            catch (Exception exception)
            {
                result = ProductException(exception).Name;
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
        private Product ProductException(Exception exception)
        {
            Product product = new Product();

            product.Id = -5;
            product.Name = exception.Message;
            product.Price = -5.0;

            return product;
        }

        #endregion
    }
}