using DevConnect.Api.Services.Interfaces;
using DevConnect.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DevConnect.Api.Contollers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _service;

        public ArticlesController(IArticleService service) => _service = service;

        /// <summary>Get all articles</summary>
        /// <remarks>Returns a list of all articles in the system.</remarks>
        [HttpGet]
        [SwaggerOperation(Summary = "Get all articles", Description = "Returns all articles with their metadata", Tags = new[] { "Articles" })]
        public async Task<ActionResult<List<Article>>> GetAll() => Ok(await _service.GetAllAsync());

        /// <summary>Get a specific article</summary>
        /// <param name="id">The ID of the article</param>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get article by ID", Description = "Retrieve a single article by its ID", Tags = new[] { "Articles" })]
        public async Task<ActionResult<Article>> GetById(int id)
        {
            var article = await _service.GetByIdAsync(id);
            if (article == null) return NotFound();
            return Ok(article);
        }

        /// <summary>Create a new article</summary>
        /// <param name="article">The article to create</param>
        [HttpPost]
        [SwaggerOperation(Summary = "Create article", Description = "Adds a new article to the system", Tags = new[] { "Articles" })]
        public async Task<ActionResult<Article>> Create(Article article)
        {
            var created = await _service.CreateAsync(article);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>Update an article</summary>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update article", Description = "Updates an existing article by ID", Tags = new[] { "Articles" })]
        public async Task<IActionResult> Update(int id, Article article)
        {
            if (id != article.Id) return BadRequest();
            var result = await _service.UpdateAsync(id, article);
            if (!result) return NotFound();
            return NoContent();
        }

        /// <summary>Delete an article</summary>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete article", Description = "Removes an article by ID from the system", Tags = new[] { "Articles" })]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
