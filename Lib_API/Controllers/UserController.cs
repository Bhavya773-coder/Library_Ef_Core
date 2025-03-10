using Lib_API.Models.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Lib_API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        private readonly LibManagementContext _context;
        public UserController(LibManagementContext context)
        {
         _context = context;   
        }

        #region GetAll

        [HttpGet]
        public async Task<ActionResult> GetApi()
        {
            return Ok(await _context.Users.ToListAsync());
            
        }

        #endregion
        
        #region GetById
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _context.Users.FindAsync(id);

            if (entity == null)
            {
                return NotFound(new { Message = $"Entity with ID {id} not found." });
            }

            return Ok(entity);
        }
        #endregion
        
        #region Create
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] UserCreateDto? createDTO)
        {
            if (createDTO == null)
            {
                return BadRequest(new { Message = "Invalid entity data." });
            }

            var user = new User()
            {
                FullName = createDTO.FullName,
                Email = createDTO.Email,
                Password = createDTO.Password,
                Role = createDTO.Role,
                IsActive = createDTO.IsActive,
                    
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = user.UserId }, user);
        }
        #endregion
        
        #region Update
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] User? updatedEntity)
        {
            if (updatedEntity == null || id != updatedEntity.UserId)
            {
                return BadRequest(new { Message = "Invalid entity data or mismatched ID." });
            }

            var existingEntity = await _context.Users.FindAsync(id);

            if (existingEntity == null)
            {
                return NotFound(new { Message = $"Entity with ID {id} not found." });
            }

            // Update the properties of the existing entity
            existingEntity.FullName = updatedEntity.FullName;
            existingEntity.Email= updatedEntity.Email;
            existingEntity.Password = updatedEntity.Password;
            existingEntity.Role = updatedEntity.Role;
            existingEntity.IsActive= updatedEntity.IsActive;
            
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
            var entity = await _context.StudentProfiles.FindAsync(id);
            if (entity == null)
            {
                return NotFound(new { Message = $"Entity with ID {id} not found." });
            }
            _context.StudentProfiles.Remove(entity);
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
