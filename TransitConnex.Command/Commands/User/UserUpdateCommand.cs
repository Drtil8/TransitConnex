using System.ComponentModel.DataAnnotations;

namespace TransitConnex.Infrastructure.Commands.User;

public class UserUpdateCommand : IUserCommand
{
    public Guid Id { get; set; }
    
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}
