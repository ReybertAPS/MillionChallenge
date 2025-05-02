namespace Million.Domain.Entities;

public class PropertyImage
{
    public string File { get; set; } = null!;
    public bool Enabled { get; set; } = true;
    public bool IsMain { get; set; } = false;
}
