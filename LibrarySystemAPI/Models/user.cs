using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystemAPI.Models
{ 
     
    public class user
    {
        public string username { get; set; }
        public int userid { get; set; }
        public List<book>? issuelist { get; set; }
    }
    
}