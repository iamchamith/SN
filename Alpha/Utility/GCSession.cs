﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alpha.Utility
{
    public class SessionUser
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Country { get; set; }
        public int Sex { get; set; }
        public int MaritalStatus { get; set; }
        public List<int> Tags { get; set; }
    }
    public class GCSession
    {
        private SessionUser user;

        public static void Logout()
        {
            HttpContext.Current.Session["user"] = null;
        }
        public static SessionUser User
        {
            get
            {
                return (SessionUser)HttpContext.Current.Session["user"];
            }
            set
            {
                HttpContext.Current.Session["user"] = value;
            }
        }
        public static bool IsSession
        {
            get
            {
                return HttpContext.Current.Session["user"] != null;
            }
        }
        public static Guid UserGuid
        {
            get
            {
                return User.UserId;
            }
        }
        public static int UserId
        {
            get
            {
                return User.Id;
            }
        }
        public static string UserDisplayName
        {
            get
            {
                return User.Name;
            }
        }
        public static string Email
        {
            get
            {
                return User.Email;
            }
        }

        public static string ProfileImage {
            get {
               return $"https://www.gravatar.com/avatar/{Alpha.Bo.Utility.Helper.MD5Hash(Email)}";
            }
        }

        public static int Country {
            get {
                return User.Country;
            }
        }
        public static int Sex
        {
            get
            {
                return User.Sex;
            }
        }
        public static int MaritalStatus
        {
            get
            {
                return User.MaritalStatus;
            }
        }
        public static List<int> Tags
        {
            get
            {
                return User.Tags;
            }
        }
    }
}