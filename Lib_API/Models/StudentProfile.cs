using System;
using System.Collections.Generic;
using FluentValidation;

namespace Lib_API.Models;

public partial class StudentProfile
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

   

    public virtual ICollection<BookIssue> BookIssues { get; set; } = new List<BookIssue>();

    // Nested validator class
    public class StudentProfileValidator : AbstractValidator<StudentProfile>
    {
        public StudentProfileValidator()
        {
            RuleFor(x => x.StudentName)
                .NotEmpty().WithMessage("Student name is required")
                .Length(2, 100).WithMessage("Name must be between 2 and 100 characters");

            RuleFor(x => x.RegistrationNumber)
                .NotEmpty().WithMessage("Registration number is required")
                .Matches(@"^[A-Za-z0-9-]+$").WithMessage("Registration number can only contain letters, numbers, and hyphens");

            RuleFor(x => x.Department)
                .NotEmpty().WithMessage("Department is required")
                .Length(2, 50).WithMessage("Department must be between 2 and 50 characters");

            RuleFor(x => x.Semester)
                .NotNull().WithMessage("Semester is required")
                .InclusiveBetween(1, 8).WithMessage("Semester must be between 1 and 8");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^\+?[1-9][0-9]{7,14}$").WithMessage("Please enter a valid phone number");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required")
                .MaximumLength(200).WithMessage("Address cannot exceed 200 characters");

            RuleFor(x => x.MaxBooksLimit)
                .NotNull().WithMessage("Maximum books limit is required")
                .InclusiveBetween(1, 10).WithMessage("Maximum books limit must be between 1 and 10");

            RuleFor(x => x.IsApproved)
                .NotNull().WithMessage("Approval status is required");
        }
    }
}