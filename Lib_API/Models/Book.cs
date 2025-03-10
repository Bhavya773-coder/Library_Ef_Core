using System;
using System.Collections.Generic;
using FluentValidation;

namespace Lib_API.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string? Title { get; set; }

    public string? Author { get; set; }

    public string? Isbn { get; set; }

    public string? Publication { get; set; }

    public string? Category { get; set; }

    public int? Quantity { get; set; }

    public int? AvailableQuantity { get; set; }

    public string? ShelfNumber { get; set; }

    public int? AddedBy { get; set; }

    public virtual User? AddedByNavigation { get; set; }

    public virtual ICollection<BookIssue> BookIssues { get; set; } = new List<BookIssue>();

    // Nested validator class
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .Length(1, 200).WithMessage("Title must be between 1 and 200 characters");

            RuleFor(x => x.Author)
                .NotEmpty().WithMessage("Author is required")
                .Length(2, 100).WithMessage("Author name must be between 2 and 100 characters")
                .Matches("^[a-zA-Z .-]*$").WithMessage("Author name can only contain letters, spaces, dots, and hyphens");

            RuleFor(x => x.Isbn)
                .NotEmpty().WithMessage("ISBN is required")
                .Matches(@"^(?:ISBN(?:-1[03])?:? )?(?=[0-9X]{10}$|(?=(?:[0-9]+[- ]){3})[- 0-9X]{13}$|97[89][0-9]{10}$|(?=(?:[0-9]+[- ]){4})[- 0-9]{17}$)(?:97[89][- ]?)?[0-9]{1,5}[- ]?[0-9]+[- ]?[0-9]+[- ]?[0-9X]$")
                .WithMessage("Please enter a valid ISBN");

            RuleFor(x => x.Publication)
                .NotEmpty().WithMessage("Publication is required")
                .Length(2, 100).WithMessage("Publication name must be between 2 and 100 characters");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Category is required")
                .Length(2, 50).WithMessage("Category must be between 2 and 50 characters");

            RuleFor(x => x.Quantity)
                .NotNull().WithMessage("Quantity is required")
                .GreaterThan(0).WithMessage("Quantity must be greater than 0");

            RuleFor(x => x.AvailableQuantity)
                .NotNull().WithMessage("Available quantity is required")
                .GreaterThanOrEqualTo(0).WithMessage("Available quantity cannot be negative")
                .Must((book, availableQty) => availableQty <= book.Quantity)
                .WithMessage("Available quantity cannot be greater than total quantity");

            RuleFor(x => x.ShelfNumber)
                .NotEmpty().WithMessage("Shelf number is required")
                .Matches(@"^[A-Z]-[0-9]{3}$").WithMessage("Shelf number must be in format 'A-123'");

            RuleFor(x => x.AddedBy)
                .NotNull().WithMessage("AddedBy is required");
        }
    }
}