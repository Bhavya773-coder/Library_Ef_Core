namespace Lib_API.Models.DTOS;

public class UserCreateDto
{
    public int UserId { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? FullName { get; set; }

    public string? Role { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedAt { get; set; }

}