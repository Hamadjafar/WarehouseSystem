﻿namespace DomainLayer.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }

    }
}
