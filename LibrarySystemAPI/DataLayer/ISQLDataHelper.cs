using SampleProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication16.Models;

namespace SampleProject.DataLayer
{
    public interface ISQLDataHelper
    {
        public List<Country> GetCountriesData();
        public List<SpecialDay> GetSpecialDaysData();

        public bool InsertBook(book book);
        public book GetBook(string name);
        public bool InsertUser(user user);

        public user GetUser(string name);
        public IEnumerable<book> GetIssuedBooks(string name);
    }
}
