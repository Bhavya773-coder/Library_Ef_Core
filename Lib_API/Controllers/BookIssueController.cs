using Lib_API.Models.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Lib_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookIssueController : ControllerBase
    {
        private readonly LibManagementContext _context;
        public BookIssueController(LibManagementContext context)
        {
            _context = context;   
        }

        #region GetAll

        [HttpGet]
        public async Task<ActionResult> GetApi()
        {
            return Ok(await _context.BookIssues.ToListAsync());
            
        }

        #endregion
        
        #region GetById
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _context.BookIssues.FindAsync(id);

            if (entity == null)
            {
                return NotFound(new { Message = $"Entity with ID {id} not found." });
            }

            return Ok(entity);
        }
        #endregion
        
        #region Create
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] BookIssueCreateDto? createDto)
        {
            if (createDto == null)
            {
                return BadRequest(new { Message = "Invalid entity data." });
            }

            var bookIssue = new BookIssue()
            {
                BookId = createDto.BookId,
                StudentId = createDto.StudentId,
                ReturnDate = DateTime.Today.AddDays(15),
                IssuedBy = createDto?.IssuedBy,
                PenaltyAmount = createDto?.PenaltyAmount,
                Status = createDto?.Status,
            };

            await _context.BookIssues.AddAsync(bookIssue);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = bookIssue.IssueId }, bookIssue);
        }
        #endregion
        
        #region Update
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] BookIssue? updatedEntity)
        {
            if (updatedEntity == null || id != updatedEntity.BookId)
            {
                return BadRequest(new { Message = "Invalid entity data or mismatched ID." });
            }

            var existingEntity = await _context.BookIssues.FindAsync(id);

            if (existingEntity == null)
            {
                return NotFound(new { Message = $"Entity with ID {id} not found." });
            }

            // Update the properties of the existing entity
            existingEntity.Status = updatedEntity.Status;
            existingEntity.DueDate= updatedEntity.DueDate;
            existingEntity.IssuedBy = updatedEntity.IssuedBy;
            existingEntity.PenaltyAmount = updatedEntity.PenaltyAmount;
            
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
            var entity = await _context.BookIssues.FindAsync(id);
            if (entity == null)
            {
                return NotFound(new { Message = $"Entity with ID {id} not found." });
            }
            _context.BookIssues.Remove(entity);
            try
            {
                await _context.SaveChangesAsync();
                return NoContent();

            }
            catch (Exception e)
            {
                return BadRequest();
            }
            
        }
        #endregion
    }
}
