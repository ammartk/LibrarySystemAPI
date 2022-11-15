using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication16.Models
{
    public class issuebookclass
    {
        public string Username { get; set; }
        public string Bookname { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}