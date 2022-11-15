using SampleProject.DataLayer;
using WebApplication16.Models;

namespace LibrarySystemAPI.BusinessLayer
{
    public class UserHandler : IUserHandler
    {
        ISQLDataHelper DatabaseHandler;
        public UserHandler(ISQLDataHelper sqldatahelper)
        {
            DatabaseHandler = sqldatahelper;
        }
        public user GetUser(string name)
        {
            throw new System.NotImplementedException();
        }

        public bool InsertUser(user user)
        {
            return DatabaseHandler.InsertUser(user);
        }

        public bool UpdateUser(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
