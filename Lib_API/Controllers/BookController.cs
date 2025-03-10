
using Microsoft.AspNetCore.Mvc;
using Lib_API.Models.DTOS;


namespace Lib_API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        
        private readonly LibManagementContext _context;
        public BookController(LibManagementContext context)
        {
         _context = context;   
        }

        #region GetAll

        [HttpGet]
        public async Task<ActionResult> GetApi()
        {
            return Ok(await _context.Books.ToListAsync());
            
        }

        #endregion
        
        #region GetById
      [HttpGet("{id}")]
      public async Task<IActionResult> GetById(int id)
      {
          var entity = await _context.Books.FindAsync(id);

          if (entity == null)
          {
              return NotFound(new { Message = $"Entity with ID {id} not found." });
          }

          return Ok(entity);
      }
      #endregion
        
      #region Create
      [HttpPost]
      public async Task<IActionResult> Add([FromBody] BookCreateDto? createDto)
      {
          if (createDto == null)
          {
              return BadRequest(new { Message = "Invalid entity data." });
          }

          var book = new Book
          {
              Title = createDto.Title,
              Author = createDto.Author,
              Isbn = createDto.Isbn,
              Publication = createDto.Publication,
              Category = createDto.Category,
              Quantity = createDto.Quantity,
              AvailableQuantity = createDto.AvailableQuantity,
              ShelfNumber = createDto.SelfNumber
          };

          await _context.Books.AddAsync(book);
          await _context.SaveChangesAsync();

          return CreatedAtAction(nameof(GetById), new { id = book.BookId }, book);
      }
      #endregion
      
        #region Update
              [HttpPut("{id}")]
              public async Task<IActionResult> Edit(int id, [FromForm] Book? updatedEntity)
              {
                  if (updatedEntity == null || id != updatedEntity.BookId)
                  {
                      return BadRequest(new { Message = "Invalid entity data or mismatched ID." });
                  }
      
                  var existingEntity = await _context.Books.FindAsync(id);
      
                  if (existingEntity == null)
                  {
                      return NotFound(new { Message = $"Entity with ID {id} not found." });
                  }
      
                  // Update the properties of the existing entity
                  existingEntity.Category = updatedEntity.Category;
                  existingEntity.Title= updatedEntity.Title;
                  existingEntity.Isbn = updatedEntity.Isbn;
                  existingEntity.Author = updatedEntity.Author;
                  existingEntity.Quantity = updatedEntity.Quantity;
                  existingEntity.AddedBy = updatedEntity.AddedBy;
                  existingEntity.ShelfNumber = updatedEntity.ShelfNumber;
                  existingEntity.Publication= updatedEntity.Publication;
                  existingEntity.AvailableQuantity = updatedEntity.AvailableQuantity;
                  // Add more fields as needed
      
                  try
                  {
                      await _context.SaveChangesAsync();
                      return NoContent(); // Successful update, no content to return
                  }
                  catch (DbUpdateException ex)
                  {
                      return StatusCode(500, new { Message = "An error occurred while updating the entity.", Details = ex.Message });
                  }
              }
              #endregion  
      
        #region Delete
        [HttpDelete("{id}")]
              public async Task<IActionResult> Delete(int id)
              {
                  var entity = await _context.Books.FindAsync(id);
                  if (entity == null)
                  {
                      return NotFound(new { Message = $"Entity with ID {id} not found." });
                  }
                  _context.Books.Remove(entity);
                  try
                  {
                      await _context.SaveChangesAsync();
                      return NoContent();

                  }
                  catch (Exception e)
                  {
                      return BadRequest(e);
                  }
            
              }
              #endregion
    }
}
