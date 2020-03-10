using ClassLibrary1.Controller;
using ClassLibrary1.Model;
using System.Collections.Generic;
using System.Web.Http;

namespace WebAPI.Controllers
{
    /// <summary>
    /// This is the class UserProductController.
    /// </summary>
    public class UserProductController : ApiController
    {
        #region Instances

        private CTR_UserProduct user_product_ctr = new CTR_UserProduct();

        #endregion

        #region IEnumerable functions

        // GET: api/UserProduct
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        #endregion

        #region Create functions

        /// <summary>
        /// Creates a user product.
        /// </summary>
        /// <param name="user_product"></param>
        /// <returns></returns>
        [Route("api/UserProduct/Create")]
        [HttpPost]
        public string Create(UserProduct user_product)
        {
            return user_product_ctr.Create(user_product);
        }

        #endregion

        #region Search functions

        /// <summary>
        /// List all user products.
        /// </summary>
        /// <returns></returns>
        [Route("api/UserProduct/ListAll")]
        [HttpGet]
        public List<UserProduct> ListAll()
        {
            return user_product_ctr.ListAll();
        }

        /// <summary>
        /// Finds a list of user products by their username, productId or both.
        /// </summary>
        /// <param name="user_product"></param>
        /// <param name="instance_using"></param>
        /// <returns></returns>
        [Route("api/UserProduct/FindByUsername")]
        [HttpGet]
        public List<UserProduct> FindByUsername(string username)
        {
            UserProduct user_product = new UserProduct
            {
                Username = username
            };

            return user_product_ctr.Find(user_product, "Username");
        }

        /// <summary>
        /// Finds a list of user products by their username, productId or both.
        /// </summary>
        /// <param name="user_product"></param>
        /// <param name="instance_using"></param>
        /// <returns></returns>
        [Route("api/UserProduct/FindByProductId")]
        [HttpGet]
        public List<UserProduct> FindByProductId(int product_id)
        {
            UserProduct user_product = new UserProduct
            {
                ProductId = product_id
            };

            return user_product_ctr.Find(user_product, "Product Id");
        }

        /// <summary>
        /// Finds a list of user products by their username, productId or both.
        /// </summary>
        /// <param name="user_product"></param>
        /// <param name="instance_using"></param>
        /// <returns></returns>
        [Route("api/UserProduct/FindByBoth")]
        [HttpGet]
        public List<UserProduct> FindByBoth(UserProduct user_product)
        {
            return user_product_ctr.Find(user_product, "Both");
        }

        /// <summary>
        /// Finds a list of user products by their is_active.
        /// </summary>
        /// <param name="is_active"></param>
        /// <returns></returns>
        [Route("api/UserProduct/FindByIsActive")]
        [HttpGet]
        public List<UserProduct> FindByIsActive(string username, bool is_active)
        {
            return user_product_ctr.FindByIsActive(username, is_active);
        }
        
        /// <summary>
        /// Finds a list of user products by their is_active.
        /// </summary>
        /// <param name="is_active"></param>
        /// <returns></returns>
        [Route("api/UserProduct/GetOwnedProducts")]
        [HttpGet]
        public List<OwnedProducts> FindByIsActive(string username)
        {
            return user_product_ctr.GetOwnedproduct(username);
        }

        #endregion

        #region Update functions

        /// <summary>
        /// Updates a user product.
        /// </summary>
        /// <param name="old_user_product"></param>
        /// <param name="new_user_product"></param>
        /// <returns></returns>
        [Route("api/UserProduct/Update")]
        [HttpPost]
        public string Update(UserProduct old_user_product, UserProduct new_user_product)
        {
            return user_product_ctr.Update(old_user_product, new_user_product);
        }

        #endregion

        #region Activate functions

        /// <summary>
        /// Activates a product that a user owns.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="id"></param>
        [Route("api/UserProduct/ActivateProduct")]
        [HttpPost]
        public void ActivateProduct(CustomUser user, string id)
        {
            user_product_ctr.ActivateProduct(user, id);
        }

        /// <summary>
        /// Deactivates a product that a user owns.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="id"></param>
        [Route("api/UserProduct/DeactivateProduct")]
        [HttpPost]
        public void DeactivateProduct(CustomUser user, string id)
        {
            user_product_ctr.DeactivateProduct(user, id);
        }

        #endregion

        #region Delete functions

        /// <summary>
        /// Deletes a list of user products by their username, productId or both.
        /// </summary>
        /// <param name="user_product"></param>
        /// <param name="instance_using"></param>
        /// <returns></returns>
        [Route("api/UserProduct/Delete")]
        [HttpPost]
        public string Delete(UserProduct user_product, string instance_using)
        {
            return user_product_ctr.Delete(user_product, instance_using);
        }

        #endregion
    }
}