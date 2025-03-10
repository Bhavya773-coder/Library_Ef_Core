using System;
using System.Collections.Generic;
using FluentValidation;

namespace Lib_API.Models;

public partial class Author
{
    public int AuthorId { get; set; }

    public string? AuthorName { get; set; }

    public int? NumOfBooks { get; set; }

    public string? AuthorRating { get; set; }

    // Nested validator class
    public class AuthorValidator : AbstractValidator<Author>
    {
        public AuthorValidator()
        {
            
            RuleFor(x => x.AuthorName)
                .NotEmpty().WithMessage("Author name is required")
                .Length(2, 100).WithMessage("Author name must be between 2 and 100 characters")
                .Matches("^[a-zA-Z .-]*$").WithMessage("Author name can only contain letters, spaces, dots, and hyphens");

            RuleFor(x => x.NumOfBooks)
                .NotNull().WithMessage("Number of books is required")
                .GreaterThanOrEqualTo(0).WithMessage("Number of books cannot be negative");
            RuleFor(x => x.AuthorRating)
                .NotEmpty().WithMessage("Author rating is required")
                .Must(rating => validRatings.Contains(rating, StringComparer.OrdinalIgnoreCase))
                .WithMessage($"Rating must be one of the following: {string.Join(", ", validRatings)}");

                 }
        private readonly string[] validRatings = new[] 
        { 
            "Poor", 
            "Fair", 
            "Good", 
            "Very Good", 
            "Excellent" 
        };

        
    }
}