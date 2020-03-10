using ClassLibrary1.Database;
using ClassLibrary1.Model;
using System.Collections.Generic;

namespace ClassLibrary1.Controller
{
    /// <summary>
    /// This is the class CTR_Product.
    /// </summary>
    public sealed class CTR_Product
    {
        #region Instances

        private DB_Product db_product;

        #endregion

        /// <summary>
        /// This is the constructor for the class CTR_Product.
        /// </summary>
        public CTR_Product()
        {
            this.db_product = DB_Product.GetInstance();
        }

        #region Create functions

        /// <summary>
        /// Creates a product.
        /// </summary>
        /// <param name="product"></param>
        /// <returns>string</returns>
        public string Create(Product product)
        {
            return db_product.CreateProduct(product);
        }

        #endregion

        #region Search functions

        /// <summary>
        /// List all products.
        /// </summary>
        /// <returns>List<Product></returns>
        public List<Product> ListAll()
        {
            return db_product.ListAllProducts();
        }

        /// <summary>
        /// Find a product by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Product</returns>
        public Product FindById(int id)
        {
            return db_product.FindProductById(id);
        }

        /// <summary>
        /// Finds a list of products by their name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>List<Product></returns>
        public List<Product> FindByName(string name)
        {
            return db_product.FindProductByName(name);
        }

        /// <summary>
        /// Finds a list of products by their price.
        /// </summary>
        /// <param name="price"></param>
        /// <param name="where"></param>
        /// <returns>List<Product></returns>
        public List<Product> FindByPrice(double price, string where)
        {
            List<Product> result;

            switch (where)
            {
                case "Equal":
                    result = db_product.FindProductByPrice(price);
                    break;

                case "Higher":
                    result = db_product.FindProductByPriceHigher(price);
                    break;

                case "Lower":
                    result = db_product.FindProductByPriceLower(price);
                    break;

                default:
                    result = new List<Product>();
                    break;
            }

            return result;
        }

        #endregion

        #region Update functions

        /// <summary>
        /// Updates a product.
        /// </summary>
        /// <param name="product"></param>
        /// <returns>string</returns>
        public string Update(Product product)
        {
            string result;

            if (FindById(product.Id).Equals(product))
            {
                result = db_product.UpdateProduct(product);
            }

            else
            {
                result = "The product doesn't exist.";
            }

            return result;
        }

        #endregion

        #region Delete functions

        /// <summary>
        /// Deletes a product.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="call_origin"></param>
        /// <returns>string</returns>
        public string Delete(int id, string call_origin)
        {
            string result;

            result = db_product.DeleteProduct(id);

            return result;
        }

        #endregion
    }
}