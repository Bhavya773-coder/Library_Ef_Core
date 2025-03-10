namespace Lib_API.Models.DTOS;

public class StudentCreateDto
{
    public int StudentId { get; set; }
    
    public string? StudentName { get; set; }

    public string? RegistrationNumber { get; set; }

    public string? Department { get; set; }

    public int? Semester { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public int? MaxBooksLimit { get; set; }

    public bool? IsApproved { get; set; }
}