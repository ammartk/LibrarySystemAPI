﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using WebApplication16.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.AspNetCore.Mvc;
using SampleProject.BusinessLayer;

namespace WebApplication16.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class bookController : Controller
    {
        IBookHandler bookHandler;
        public bookController(IBookHandler handler)
        {
            bookHandler = handler;
        }
        // GET api/values

        //public List<book> Get()
        //{
        //    SqlDataAdapter d = new SqlDataAdapter("", con);
        //    d.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    DataTable dt = new DataTable();
        //    d.Fill(dt);
        //    List<book> isbook = new List<book>();
        //    if (dt.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            book boook = new book();
        //            boook.bookname = dt.Rows[i]["bookname"].ToString();
        //            boook.bookid = Convert.ToInt32(dt.Rows[i]["bookid"].ToString());
        //            boook.category = dt.Rows[i]["bookname"].ToString();
        //            boook.shelf = Convert.ToInt32(dt.Rows[i]["bookname"].ToString());
        //            boook.availibilty = Convert.ToInt32(dt.Rows[i]["bookname"].ToString());
        //            isbook.Add(boook);
        //        }
        //    }
        //    if (isbook.Count > 0)
        //    {
        //        return (isbook);
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        // GET api/values/5
        //   question3

        [HttpGet("{name}")]
        public ActionResult<book> GetWithName(string name)
        {
            var book = bookHandler.GetBook(name);
            if(book != null)
            {
                return Ok(book);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public ActionResult Post(book bookk)
        {
            bookHandler.InsertBook(bookk);
            if (true)
            {
                return Created(nameof(Post), bookk);
            }
            return BadRequest();
        }

        //public string Put(int id)
        //{
        //    string msg = "";
        //    if (bookk != null)
        //    {
        //        SqlCommand cmd = new SqlCommand("updatebook", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@bookname", bookk.bookname);
        //        cmd.Parameters.AddWithValue("@bookid",id);
        //        cmd.Parameters.AddWithValue("@category", bookk.category);
        //        cmd.Parameters.AddWithValue("@shelf", bookk.shelf);
        //        cmd.Parameters.AddWithValue("@avail", bookk.availibilty);
        //        con.Open();
        //        int i = cmd.ExecuteNonQuery();
        //        con.Close();
        //        if (i > 0)
        //        {
        //            msg = "data has been updated";
        //        }
        //        else
        //        {
        //            msg = "data has not beenupdated";
        //        }
        //    }
        //    return msg;
        //}
    }
}
