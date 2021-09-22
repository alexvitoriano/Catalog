using System.ComponentModel.DataAnnotations;

public record CreatItemDto
{
    [Required]
    public string? Name { get; init; }

    [Required]
    [Range(1,1000)]
    public decimal Price { get; init; } 
}