

using LibrarySystemAPI.Models;

namespace LibrarySystemAPI.BusinessLayer
{
    public interface IUserHandler
    {
        public bool InsertUser(user user);
        public user GetUser(int id);
        public bool UpdateUser(int id, user user);

        public bool DeleteUser(int id);
        public int CalculateFine(int id);
    }
}
