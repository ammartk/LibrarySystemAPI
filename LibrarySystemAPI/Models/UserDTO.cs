using System.Collections;
using System.Collections.Generic;
using WebApplication16.Models;

namespace LibrarySystemAPI.Models
{
    public class UserDTO
    {
        public string username { get; set; }
        public int userid { get; set; }
        public List<book> issuelist { get; set; }
    }
}
