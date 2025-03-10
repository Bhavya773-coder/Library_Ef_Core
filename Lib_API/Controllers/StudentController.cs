using Lib_API.Models.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lib_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly LibManagementContext _context;
        public StudentController(LibManagementContext context)
        {
            _context = context;   
        }

        #region GetAll

        [HttpGet]
        public async Task<ActionResult> GetApi()
        {
            return Ok(await _context.StudentProfiles.ToListAsync());
            
        }

        #endregion
        
        #region GetById
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _context.StudentProfiles.FindAsync(id);

            if (entity == null)
            {
                return NotFound(new { Message = $"Entity with ID {id} not found." });
            }

            return Ok(entity);
        }
        #endregion
        
        #region Create
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] StudentCreateDto? createDTO)
        {
            if (createDTO == null)
            {
                return BadRequest(new { Message = "Invalid entity data." });
            }

            var studentProfile = new StudentProfile
            {
                RegistrationNumber = createDTO.RegistrationNumber,
                StudentName = createDTO.StudentName,
                Department =  createDTO.Department,
                Semester = createDTO.Semester,
                PhoneNumber = createDTO.PhoneNumber,
                Address = createDTO.Address,
                MaxBooksLimit = createDTO.MaxBooksLimit,
                IsApproved = createDTO.IsApproved,
             
            };

            await _context.StudentProfiles.AddAsync(studentProfile);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = studentProfile.StudentId }, studentProfile);
        }
        #endregion
        
        #region Update
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] StudentProfile? updatedEntity)
        {
            if (updatedEntity == null || id != updatedEntity.StudentId)
            {
                return BadRequest(new { Message = "Invalid entity data or mismatched ID." });
            }

            var existingEntity = await _context.StudentProfiles.FindAsync(id);

            if (existingEntity == null)
            {
                return NotFound(new { Message = $"Entity with ID {id} not found." });
            }

            // Update the properties of the existing entity
            existingEntity.Address = updatedEntity.Address;
            existingEntity.StudentName = updatedEntity.StudentName;
            existingEntity.Department = updatedEntity.Department;
            existingEntity.Semester = updatedEntity.Semester;
            existingEntity.IsApproved = updatedEntity.IsApproved;
            existingEntity.RegistrationNumber = updatedEntity.RegistrationNumber;
            existingEntity.PhoneNumber = updatedEntity.PhoneNumber;
            existingEntity.MaxBooksLimit = updatedEntity.MaxBooksLimit;
            
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
