using System;
using System.Collections.Generic;
using FluentValidation;

namespace Lib_API.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? FullName { get; set; }

    public string? Role { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedAt { get; set; }

    public virtual ICollection<BookIssue> BookIssues { get; set; } = new List<BookIssue>();

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    // Nested validator class
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Please enter a valid email address")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("Password must contain at least one number")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required")
                .Length(2, 100).WithMessage("Full name must be between 2 and 100 characters")
                .Matches("^[a-zA-Z ]*$").WithMessage("Full name can only contain letters and spaces");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role is required")
                .Must(role => role == "Admin" || role == "Librarian" || role == "User")
                .WithMessage("Role must be either 'Admin', 'Librarian', or 'User'");

            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("Active status is required");

            RuleFor(x => x.CreatedAt)
                .NotEmpty().WithMessage("Created date is required")
                .Must(BeAValidDate).WithMessage("Please enter a valid date");
        }

        private bool BeAValidDate(string? date)
        {
            if (string.IsNullOrEmpty(date)) return false;
            return DateTime.TryParse(date, out _);
        }
    }
}