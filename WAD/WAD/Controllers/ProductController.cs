using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using WAD_12435.Models;
using WAD_12435.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WAD_12435.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        private ActionResult HandleSuccessfulOperation(object result)
        {
            using var scope = new TransactionScope();
            scope.Complete();
            return Ok(result);
        }

        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult Get()
        {
            var product = _productRepository.GetProduct();
            return new OkObjectResult(product);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}", Name = "GetP")]
        public IActionResult Get(int id)
        {
            var product = _productRepository.GetProductById(id);
            return new OkObjectResult(product);
        }

        // POST api/<ProductController>
        [HttpPost]
        public ActionResult Post([FromBody] Product product)
        {
            
            _productRepository.InsertProdcut(product);
            return HandleSuccessfulOperation(product);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Product product)
        {
            var isProductExists = _productRepository.GetProductById(id);
            if (isProductExists != null)
            {
                _productRepository.UpdateProduct(product);
                return HandleSuccessfulOperation(null);
            }
            return NotFound();
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _productRepository.DeleteProduct(id);
            return HandleSuccessfulOperation(null);
        }
    }
}
