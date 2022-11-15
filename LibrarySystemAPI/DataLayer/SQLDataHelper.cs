using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using LibrarySystemAPI;
using Microsoft.Extensions.Configuration;
using SampleProject.Models;
using WebApplication16.Models;

namespace SampleProject.DataLayer
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

        public List<Country> GetCountriesData()
        {
            List<Country> lstCountry = new List<Country>();
            connectionString = "server=CMDLHRLTH60\\SQLEXPRESS;database=mvc ; Integrated Security = true;";
            using (SqlConnection con = new SqlConnection(Constants.connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetCountries", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    Country Country = new Country();

                    Country.Id = Convert.ToInt32(sdr["Id"]);
                    Country.CountryName = sdr["CountryName"].ToString();
                    Country.CurrencyCode = sdr["CurrencyCode"].ToString();
                    Country.PenaltyAmount = Convert.ToDecimal(sdr["PenaltyAmount"].ToString());
                    Country.OffDay1 = sdr["OffDay1"].ToString();
                    Country.OffDay2 = sdr["OffDay2"].ToString();
                    lstCountry.Add(Country);
                }
                con.Close();
            }
            return lstCountry;
        }
        public List<SpecialDay> GetSpecialDaysData()
        {
            List<SpecialDay> lstSpecialDay = new List<SpecialDay>();
            connectionString = "server=CMDLHRLTH60\\SQLEXPRESS;database=mvc ; Integrated Security = true;";
            using (SqlConnection con = new SqlConnection(Constants.connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetCountriesSpecialDays", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    SpecialDay specialDay = new SpecialDay();

                    specialDay.Id = Convert.ToInt32(sdr["Id"]);
                    specialDay.CountryId = Convert.ToInt32(sdr["CountryId"].ToString());
                    specialDay.SpecialDate = Convert.ToDateTime(sdr["SpecialDate"].ToString());
                    lstSpecialDay.Add(specialDay);
                }
                con.Close();
            }
            return lstSpecialDay;
        }

        public bool InsertBook(book book)
        {
            connectionString = "server=CMDLHRLTH60\\SQLEXPRESS;database=mvc ; Integrated Security = true;";
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

        public List<book> GetIsuedBooks(string name)
        {
            connectionString = "server=CMDLHRLTH60\\SQLEXPRESS;database=mvc ; Integrated Security = true;";
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

        public string PutUser(int userid)
        {
            connectionString = "server=CMDLHRLTH60\\SQLEXPRESS;database=mvc ; Integrated Security = true;";
            using (SqlConnection con = new SqlConnection(Constants.connectionString))
            {
                user user = new user();
                string msg = "";
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
                        msg = "user has been updated";
                    }
                    else
                    {
                        msg = "user has not beenupdated";
                    }
                }
                return msg;
            }
        
        }

        public string PutBook(int bookid)
        {
            connectionString = "server=CMDLHRLTH60\\SQLEXPRESS;database=mvc ; Integrated Security = true;";
            using (SqlConnection con = new SqlConnection(Constants.connectionString))
            {
                string msg = "";
                book bookk = new book();
                if (bookk != null)
                {
                    SqlCommand cmd = new SqlCommand("updatebook", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@bookname", bookk.bookname);
                    cmd.Parameters.AddWithValue("@bookid", bookid);
                    cmd.Parameters.AddWithValue("@category", bookk.category);
                    cmd.Parameters.AddWithValue("@shelf", bookk.shelf);
                    cmd.Parameters.AddWithValue("@avail", bookk.availibilty);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i > 0)
                    {
                        msg = "bookdata has been updated";
                    }
                    else
                    {
                        msg = "bookdata has not beenupdated";
                    }

                }
                return msg;
            }
                
        }

        public string DeleteBook(string bookname)
        {
            connectionString = "server=CMDLHRLTH60\\SQLEXPRESS;database=mvc ; Integrated Security = true;";
            using (SqlConnection con = new SqlConnection(Constants.connectionString))
            {
                book book = new book();
                string msg = "";
                if (book != null)
                {
                    SqlCommand cmd = new SqlCommand("deletebook", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@bookname", bookname);

                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i > 0)
                    {
                        msg = "book is deleted";
                    }
                    else
                    {
                        msg = "book is not deleted";
                    }
                }
                return msg;
            }
               
        }
        public string DeleteUser(int id)
        {
            connectionString = "server=CMDLHRLTH60\\SQLEXPRESS;database=mvc ; Integrated Security = true;";
            using (SqlConnection con = new SqlConnection(Constants.connectionString))
            {
                user user = new user();
                string msg = "";
                if (user != null)
                {
                    SqlCommand cmd = new SqlCommand("deleteusers", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                   cmd.Parameters.AddWithValue("@userid",id);

                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i > 0)
                    {
                        msg = "user is deleted";
                    }
                    else
                    {
                        msg = "user is not deleted";
                    }
                }
                return msg;
            }

        }



    }
}