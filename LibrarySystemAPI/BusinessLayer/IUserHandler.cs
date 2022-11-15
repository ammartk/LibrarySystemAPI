using WebApplication16.Models;

namespace LibrarySystemAPI.BusinessLayer
{
    public interface IUserHandler
    {
        public bool InsertUser(user book);
        public user GetUser(string name);

        public bool UpdateUser(int id);
       
    }
}
