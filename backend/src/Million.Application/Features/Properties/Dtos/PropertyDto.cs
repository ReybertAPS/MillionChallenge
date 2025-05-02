namespace Million.Application.Features.Properties.Dtos;

public class PropertyDto
{
    public string Id { get; set; } = null!;
    public string IdOwner { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public decimal Price { get; set; }
    public string CodeInternal { get; set; } = null!;
    public int Year { get; set; }
    public string? ImageUrl { get; set; }

    public List<PropertyImageDto>? Images { get; set; }
}
