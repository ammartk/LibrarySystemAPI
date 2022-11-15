using WebApplication16.Models;

namespace LibrarySystemAPI.BusinessLayer
{
    public interface IUserHandler
    {
        public bool InsertUser(user user);
        public user GetUser(int id);
        public bool UpdateUser(int id, user user);
       
        public bool DeleteUser(int id)
        {

        }
    }
}
