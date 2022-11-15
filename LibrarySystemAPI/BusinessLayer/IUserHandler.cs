using WebApplication16.Models;

namespace LibrarySystemAPI.BusinessLayer
{
    public interface IUserHandler
    {
        public bool InsertUser(user user);
        public UserDTO GetUser(string name);
        public bool UpdateUser(int id);
       
    }
}
