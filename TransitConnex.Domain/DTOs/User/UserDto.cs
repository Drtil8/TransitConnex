namespace TransitConnex.Domain.DTOs.User;

public class UserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public bool Deleted { get; set; }
    public DateTime Updated { get; set; }
}
