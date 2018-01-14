using System;

namespace Shop.Core.DTO
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public RoleDto Role { get; set; }
    }
}
