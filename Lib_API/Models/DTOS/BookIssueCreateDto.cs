namespace Lib_API.Models.DTOS;

public class BookIssueCreateDto
{
    public int IssueId { get; set; }

    public int? BookId { get; set; }

    public int? StudentId { get; set; }

    public int? IssuedBy { get; set; }
    
    public DateTime? ReturnDate { get; set; }
    
    public int? PenaltyAmount { get; set; }

    public string? Status { get; set; }
}