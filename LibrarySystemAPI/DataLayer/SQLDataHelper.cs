using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Xml.Linq;
using LibrarySystemAPI.Models;
using Microsoft.Extensions.Configuration;


namespace LibrarySystemAPI.DataLayer
{
    public class SQLDataHelper : ISQLDataHelper
    {
        //Please add server, db, user and password below
        string connectionString = "server=CMDLHRLTH60\\SQLEXPRESS;database=mvc ; Integrated Security = true;";
        public SQLDataHelper(IConfiguration config)
        {
            var c = config;
            connectionString = config.GetConnectionString("ProjectDB");
        }

        public book GetBook(string name)
        {
            connectionString = "server=CMDLHRLTH60\\SQLEXPRESS;database=mvc ; Integrated Security = true;";
            using (SqlConnection con = new SqlConnection(Constants.connectionString))
            {
                SqlDataAdapter d = new SqlDataAdapter("fetchbook", con);
                d.SelectCommand.CommandType = CommandType.StoredProcedure;
                d.SelectCommand.Parameters.AddWithValue("@bookname", name);
                DataTable dt = new DataTable();
                d.Fill(dt);
                book boook = new book();
                if (dt.Rows.Count > 0)
                {
                    boook.bookname = dt.Rows[0]["bookname"].ToString();
                    boook.bookid = Convert.ToInt32(dt.Rows[0]["bookid"].ToString());
                    boook.category = dt.Rows[0]["category"].ToString();
                    boook.shelf = Convert.ToInt32(dt.Rows[0]["shelf"].ToString());
                    boook.availibilty = Convert.ToInt32(dt.Rows[0]["availibilty"].ToString());
                }
                if (boook != null && boook.availibilty == -1)
                {
                    // -1 means book is avaialable 
                    return boook;
                }
                else
                {
                    return null;
                }
            }

        }


        public bool InsertBook(book book)
        {
            
            using (SqlConnection con = new SqlConnection(Constants.connectionString))
            {
                if (book != null)
                {
                    SqlCommand cmd = new SqlCommand("insertbook", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@bookname", book.bookname);
                    cmd.Parameters.AddWithValue("@bookid", book.bookid);
                    cmd.Parameters.AddWithValue("@category", book.category);
                    cmd.Parameters.AddWithValue("@shelf", book.shelf);
                    cmd.Parameters.AddWithValue("@avail", book.availibilty);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    return true;
                }
                return false;
            }
        }

        public bool InsertUser(user user)
        {
            connectionString = "server=CMDLHRLTH60\\SQLEXPRESS;database=mvc ; Integrated Security = true;";
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                if (user != null)
                {
                    SqlCommand cmd = new SqlCommand("listofusers", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", user.username);
                    cmd.Parameters.AddWithValue("@userid", user.userid);
                    cmd.Parameters.AddWithValue("@issuelist", user.issuelist);

                    con.Open();
                    var i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }
        public user GetUser(int id)
        {
            connectionString = "server=CMDLHRLTH60\\SQLEXPRESS;database=mvc ; Integrated Security = true;";
            using (SqlConnection con = new SqlConnection(Constants.connectionString))
            {
                SqlDataAdapter d = new SqlDataAdapter("fetchusername", con);
                d.SelectCommand.CommandType = CommandType.StoredProcedure;
                d.SelectCommand.Parameters.AddWithValue("@userid", id);
                DataTable dt = new DataTable();
                d.Fill(dt);
                user user1 = new user();
                if (dt.Rows.Count > 0)
                {
                    user1.username = dt.Rows[0]["username"].ToString();
                    user1.userid = Convert.ToInt32(dt.Rows[0]["userid"].ToString());
                }
                if (user1 != null)
                {
                    return user1;
                }
                else
                {
                    return null;
                }
            }
        }

        public List<book> GetIssuedBooks(string name)
        {
            using (SqlConnection con = new SqlConnection(Constants.connectionString))
            {
                SqlDataAdapter d = new SqlDataAdapter("Fetchbooks", con);
                d.SelectCommand.CommandType = CommandType.StoredProcedure;
                d.SelectCommand.Parameters.AddWithValue("@name", name);
                DataTable dt = new DataTable();
                d.Fill(dt);
                List<book> books = new List<book>();
                foreach (DataRow i in dt.Rows)
                {
                    book boook = new book();
                    boook.bookname = i["bookname"].ToString();
                    boook.bookid = Convert.ToInt32(i["bookid"].ToString());
                    boook.category = i["category"].ToString();
                    boook.shelf = Convert.ToInt32(i["shelf"].ToString());
                    boook.availibilty = Convert.ToInt32(i["availibilty"].ToString());
                    books.Add(boook);

                }
                return books;
            }

        }

        public bool UpdateUser(int userid, user user)
        {
            connectionString = "server=CMDLHRLTH60\\SQLEXPRESS;database=mvc ; Integrated Security = true;";
            using (SqlConnection con = new SqlConnection(Constants.connectionString))
            {
                
                
                if (user != null)
                {
                    SqlCommand cmd = new SqlCommand("updateuser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userid", user.userid);
                    cmd.Parameters.AddWithValue("@username", user.username);

                    cmd.Parameters.AddWithValue("@issuelist", user.issuelist);

                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                
            }
            return false;
        }

        public bool UpdateBook(int bookid, book book)
        {
            connectionString = "server=CMDLHRLTH60\\SQLEXPRESS;database=mvc ; Integrated Security = true;";
            using (SqlConnection con = new SqlConnection(Constants.connectionString))
            {
                string msg = "";
                
                if (book != null)
                {
                    SqlCommand cmd = new SqlCommand("updatebook", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@bookname", book.bookname);
                    cmd.Parameters.AddWithValue("@bookid", bookid);
                    cmd.Parameters.AddWithValue("@category", book.category);
                    cmd.Parameters.AddWithValue("@shelf", book.shelf);
                    cmd.Parameters.AddWithValue("@avail", book.availibilty);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                return false;
            }
                
        }

        public bool DeleteBook(string bookname)
        {
            connectionString = "server=CMDLHRLTH60\\SQLEXPRESS;database=mvc ; Integrated Security = true;";
            using (SqlConnection con = new SqlConnection(Constants.connectionString))
            {
                
                SqlCommand cmd = new SqlCommand("deletebook", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@bookname", bookname);

                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
               
        }
        public bool DeleteUser(int id)
        {
            connectionString = "server=CMDLHRLTH60\\SQLEXPRESS;database=mvc ; Integrated Security = true;";
            using (SqlConnection con = new SqlConnection(Constants.connectionString))
            {
                
                SqlCommand cmd = new SqlCommand("deleteusers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userid",id);

                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
               
            }

        }

        public bool IssueBook(issuebookclass issue)
        {
            using (SqlConnection con = new SqlConnection(Constants.connectionString))
            {

                SqlCommand cmd = new SqlCommand("issue", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userid", issue.UserId);
                cmd.Parameters.AddWithValue("@bookname", issue.Bookname);
                cmd.Parameters.AddWithValue("@issuedate", issue.IssueDate);
                cmd.Parameters.AddWithValue("@returndate", issue.ReturnDate);

                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public bool ReturnBook(string bookname)
        {
            using (SqlConnection con = new SqlConnection(Constants.connectionString))
            {

                SqlCommand cmd = new SqlCommand("returnbook", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bookname", bookname);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public List<issuebookclass> IssuedLists(int id)
        {
            using (SqlConnection con = new SqlConnection(Constants.connectionString))
            {
                SqlDataAdapter d = new SqlDataAdapter("getissuedlist", con);
                d.SelectCommand.CommandType = CommandType.StoredProcedure;
                d.SelectCommand.Parameters.AddWithValue("@userid", id);
                DataTable dt = new DataTable();
                d.Fill(dt);
                List<issuebookclass> books = new List<issuebookclass>();
                foreach (DataRow i in dt.Rows)
                {
                    issuebookclass boook = new issuebookclass();
                    boook.Bookname = i["bookname"].ToString();
                    boook.UserId = Convert.ToInt32(i["issuedate"].ToString());
                    boook.IssueDate = DateTime.ParseExact(i["issuedate"].ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    boook.ReturnDate = DateTime.ParseExact(i["returndate"].ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                    books.Add(boook);

                }
                return books;
            }
        }

    }
}