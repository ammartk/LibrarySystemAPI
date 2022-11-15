
using LibrarySystemAPI.DataLayer;
using LibrarySystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibrarySystemAPI.BusinessLayer
{
    public class UserHandler : IUserHandler
    {
        ISQLDataHelper DatabaseHandler;
        public UserHandler(ISQLDataHelper sqldatahelper)
        {
            DatabaseHandler = sqldatahelper;
        }

        public int CalculateFine(int id)
        {
            List<issuebookclass> list = DatabaseHandler.IssuedLists(id);
            int fine = 0;
            foreach(issuebookclass issue in list)
            {
                if(issue.ReturnDate < DateTime.Now)
                {
                    DateTime value = issue.ReturnDate;
                    
                    while(value < DateTime.Now)
                    {
                        if(value.DayOfWeek != DayOfWeek.Sunday && value.DayOfWeek != DayOfWeek.Saturday)
                        {
                            fine++;
                        }
                        value.AddDays(1);
                    }
                }
            }
            return fine;
        }

        public bool DeleteUser(int id)
        {
            var user = GetUser(id);
            if(user.issuelist.Count == 0)
            {
                DatabaseHandler.DeleteUser(id);
                return true;
            }
            else
            {
                return false;
            }
        }

        public user GetUser(int id)
        {
            var user = DatabaseHandler.GetUser(id);
            user.issuelist = DatabaseHandler.GetIssuedBooks(user.username).ToList();
            return user;
        }

        public bool InsertUser(user user)
        {
            return DatabaseHandler.InsertUser(user);
        }

        public bool UpdateUser(int id, user user)
        {
            return DatabaseHandler.UpdateUser(id, user);
        }
    }
}
