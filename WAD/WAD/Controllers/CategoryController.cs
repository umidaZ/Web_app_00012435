using Microsoft.AspNetCore.Mvc;
using WAD_12435.Repositories;
using WAD_12435.Models;
using System.Transactions;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WAD_12435.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        private ActionResult HandleSuccessfulOperation(object result)
        {
            using var scope = new TransactionScope();
            scope.Complete();
            return Ok(result);
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public IActionResult Get()
        {
            var cats = _categoryRepository.GetCategory();
            return new OkObjectResult(cats);
        }

        // GET api/<CategoryController>/5
        [HttpGet, Route("{id}", Name = "GetH")]
        public IActionResult Get(int id)
        {
            var cat = _categoryRepository.GetCategoryById(id);
            return new OkObjectResult(cat);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public ActionResult Post([FromBody] Category category)
        {
            _categoryRepository.InsertCategory(category);
            return HandleSuccessfulOperation(category);
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Category category)
        {
            var isCatExist = _categoryRepository.GetCategoryById(id);
            if (isCatExist != null)
            {
                _categoryRepository.UpdateCategory(category);
                return HandleSuccessfulOperation(null);
            }
            return NotFound();
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _categoryRepository.DeleteCategory(id);
            return HandleSuccessfulOperation(null);
        }
    }
}
