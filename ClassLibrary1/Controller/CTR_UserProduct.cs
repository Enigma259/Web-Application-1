using ClassLibrary1.Database;
using ClassLibrary1.Model;
using System.Collections.Generic;

namespace ClassLibrary1.Controller
{
    /// <summary>
    /// This is the class CTR_UserProduct.
    /// </summary>
    public sealed class CTR_UserProduct
    {
        #region Instances

        private DB_UserProduct db_userproduct;

        #endregion

        /// <summary>
        /// This is the constructor for the class CTR_UserProduct.
        /// </summary>
        public CTR_UserProduct()
        {
            this.db_userproduct = DB_UserProduct.GetInstance();
        }

        #region Create functions

        /// <summary>
        /// Creates a user product.
        /// </summary>
        /// <param name="user_product"></param>
        /// <returns>string</returns>
        public string Create(UserProduct user_product)
        {
            return db_userproduct.CreateUserProduct(user_product);
        }

        #endregion

        #region Search functions

        /// <summary>
        /// lists all user products.
        /// </summary>
        /// <returns>List<UserProduct></returns>
        public List<UserProduct> ListAll()
        {
            return db_userproduct.ListAllUserProducts();
        }

        /// <summary>
        /// finds a list of user products by username, productId or both.
        /// </summary>
        /// <param name="user_product"></param>
        /// <param name="instance_use"></param>
        /// <returns>List<UserProduct></returns>
        public List<UserProduct> Find(UserProduct user_product, string instance_use)
        {
            List<UserProduct> result;

            switch (instance_use)
            {
                case "Username":
                    result = db_userproduct.FindUserProductByUsername(user_product.Username);
                    break;

                case "Product Id":
                    result = db_userproduct.FindUserProductByProductId(user_product.ProductId);
                    break;

                case "Both":
                    result = new List<UserProduct>();
                    result.Add(db_userproduct.FindUserProduct(user_product));
                    break;

                default:
                    result = new List<UserProduct>();
                    break;
            }

            return result;
        }

        /// <summary>
        /// Finds a list of user_products by their is_active.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="is_active"></param>
        /// <returns>List<UserProduct></returns>
        public List<UserProduct> FindByIsActive(string username, bool is_active)
        {
            return db_userproduct.FindUserProductByIsActive(username, is_active);
        }

        /// <summary>
        /// Findsa a list of products that are owned by a specific user.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>List<OwnedProducts></returns>
        public List<OwnedProducts> GetOwnedproduct(string username)
        {
            return db_userproduct.GetOwnedProducts(username);
        }

        #endregion

        #region Update functions

        /// <summary>
        /// Updates a user product.
        /// </summary>
        /// <param name="old_user_product"></param>
        /// <param name="new_user_product"></param>
        /// <returns>string</returns>
        public string Update(UserProduct old_user_product, UserProduct new_user_product)
        {
            return db_userproduct.UpdateUserProduct(old_user_product, new_user_product);
        }

        #endregion

        #region Delete functions

        /// <summary>
        /// Deletes a user product.
        /// </summary>
        /// <param name="user_product"></param>
        /// <param name="instance_use"></param>
        /// <returns>string</returns>
        public string Delete(UserProduct user_product, string instance_use)
        {
            string result;

            switch (instance_use)
            {
                case "Username":
                    result = db_userproduct.DeleteByUsername(user_product.Username);
                    break;

                case "Product Id":
                    result = db_userproduct.DeleteByProductId(user_product.ProductId);
                    break;

                case "Both":
                    result = db_userproduct.Delete(user_product);
                    break;

                default:
                    result = "The chosen instanse to use is invalid.";
                    break;
            }

            return result;
        }

        #endregion

        #region Other functions

        /// <summary>
        /// Activates a user product.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="id"></param>
        public void ActivateProduct(CustomUser user, string id)
        {
            db_userproduct.ActivateProduct(user, id);

        }

        /// <summary>
        /// Deactivates a user product.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="id"></param>
        public void DeactivateProduct(CustomUser user, string id)
        {
            db_userproduct.DeactivateProduct(user, id);
        }

        #endregion
    }
}