using System;
using System.Collections.Generic;
using FluentValidation;

namespace Lib_API.Models;

public partial class BookIssue
{
    public int IssueId { get; set; }

    public int? BookId { get; set; }

    public int? StudentId { get; set; }

    public int? IssuedBy { get; set; }

    public DateTime? DueDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public int? PenaltyAmount { get; set; }

    public string? Status { get; set; }

    public virtual Book? Book { get; set; }

    public virtual User? IssuedByNavigation { get; set; }

    public virtual StudentProfile? Student { get; set; }

    // Nested validator class
    public class BookIssueValidator : AbstractValidator<BookIssue>
    {
        private readonly string[] validStatuses = new[] 
        { 
            "Issued",
            "Returned",
            "Overdue",
            
        };

        public BookIssueValidator()
        {
            RuleFor(x => x.BookId)
                .NotNull().WithMessage("Book ID is required");

            RuleFor(x => x.StudentId)
                .NotNull().WithMessage("Student ID is required");

            RuleFor(x => x.IssuedBy)
                .NotNull().WithMessage("Issuer ID is required");

            RuleFor(x => x.DueDate)
                .NotNull().WithMessage("Due date is required")
                .Must(dueDate => dueDate > DateTime.Now)
                .WithMessage("Due date must be in the future");

            RuleFor(x => x.ReturnDate)
                .Must((bookIssue, returnDate) => 
                    !returnDate.HasValue || 
                    (bookIssue.DueDate.HasValue && returnDate.Value >= bookIssue.DueDate.Value))
                .WithMessage("Return date must be after or equal to the due date");

            RuleFor(x => x.PenaltyAmount)
                .GreaterThanOrEqualTo(0).When(x => x.PenaltyAmount.HasValue)
                .WithMessage("Penalty amount cannot be negative");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required")
                .Must(status => validStatuses.Contains(status, StringComparer.OrdinalIgnoreCase))
                .WithMessage($"Status must be one of the following: {string.Join(", ", validStatuses)}");

            // Custom rule for return date and status consistency
            RuleFor(x => x)
                .Must(x => !x.ReturnDate.HasValue || x.Status == "Returned" || x.Status == "Damaged")
                .WithMessage("If return date is set, status must be either 'Returned' or 'Damaged'")
                .When(x => x.ReturnDate.HasValue);
        }
    }
}