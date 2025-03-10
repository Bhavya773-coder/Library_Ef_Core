namespace Lib_API.Models.DTOS;

public class BookCreateDto
{
    public string? Title { get; set; }
    public string? Author { get; set; }
    public string? Isbn { get; set; }
    public string? Publication { get; set; }
    public string? Category { get; set; }
    public int? Quantity { get; set; }
    public int? AvailableQuantity { get; set; }
    public string? SelfNumber { get; set; }
}