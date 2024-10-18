namespace DomainLayer.Dtos
{
    public class UsersOutputDto
    {
        public List<GetAllUsersOutput> UserDto { get; set; }
        public int TotalItems { get; set; }
    }

}
