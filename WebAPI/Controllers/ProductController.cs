using ClassLibrary1.Controller;
using ClassLibrary1.Model;
using WebAPI.RabbitMQ;
using System.Collections.Generic;
using System.Web.Http;

namespace WebAPI.Controllers
{
    /// <summary>
    /// This is the class ProductController.
    /// </summary>
    public class ProductController : ApiController
    {
        #region Instances

        private CTR_Product product_ctr = new CTR_Product();
        private RMQ_Product rmq_product = RMQ_Product.GetInstance();

        #endregion

        #region IEnumerable functions

        // GET: api/Product
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        #endregion

        #region Create functions

        /// <summary>
        /// Creates a product.
        /// </summary>
        /// <param name="product"></param>
        /// <returns>string</returns>
        [Route("api/Product/Create")]
        [HttpPost]
        public void Create(Product product)
        {
            rmq_product.Create(product);

            //return product_ctr.Create(product);
        }

        #endregion

        #region Search functions

        /// <summary>
        /// List all products.
        /// </summary>
        /// <returns>List<Product></returns>
        [Route("api/Product/ListAll")]
        [HttpGet]
        public List<Product> ListAll()
        {
            return product_ctr.ListAll();
        }

        /// <summary>
        /// find a product by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Product</returns>
        [Route("api/Product/FindById/{id}")]
        [HttpGet]
        public Product FindById(int id)
        {
            return product_ctr.FindById(id);
        }

        /// <summary>
        /// find a list of products by their name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>List<Product></returns>
        [Route("api/Product/FindByName/{name}")]
        [HttpGet]
        public List<Product> FindByName(string name)
        {
            return product_ctr.FindByName(name);
        }

        /// <summary>
        /// find a list of products by their price.
        /// </summary>
        /// <param name="price"></param>
        /// <param name="where"></param>
        /// <returns>List<Product></returns>
        [Route("api/Product/FindByPrice/{price}/{where}")]
        [HttpGet]
        public List<Product> FindByPrice(double price, string where)
        {
            return product_ctr.FindByPrice(price, where);
        }

        #endregion

        #region Update functions

        /// <summary>
        /// Updates a product.
        /// </summary>
        /// <param name="product"></param>
        /// <returns>string</returns>
        [Route("api/Product/Update")]
        [HttpPost]
        public void Update(Product product)
        {
            rmq_product.Update(product);

            //return product_ctr.Update(product);
        }

        #endregion

        #region Delete functions

        /// <summary>
        /// Deletes a product.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>string</returns>
        [Route("api/Product/Delete")]
        [HttpPost]
        public void Delete(int id)
        {
            rmq_product.Delete(id);

            //return product_ctr.Delete(id, "Product");
        }

        #endregion
    }
}