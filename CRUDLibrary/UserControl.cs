﻿using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDLibrary
{
    public class UserControl
    {
        List<User> userList = new List<User>()
        {
            new User() { Username = "Myel", FirstName = "Mel", LastName = "Meji" }
        };
        public List<User> GetAllUser()
        {
            if (userList != null)
            {
                userList = new List<User>();
            }

            return userList;
        }

        public static void CreateNewUser(List<User> userList, User user)
        {
            if (string.IsNullOrWhiteSpace(user.Username))
            {
                throw new ArgumentException("You passed in an invalid parameter", "Username");
            }

            if (string.IsNullOrWhiteSpace(user.FirstName))
            {
                throw new ArgumentException("You passed in an invalid parameter", "FirstName");
            }

            if (string.IsNullOrWhiteSpace(user.LastName))
            {
                throw new ArgumentException("You passed in an invalid parameter", "LastName");
            }

            userList.Add(user);
        }

        public static void RetrieveUser(List<User> userList,User user)
        {
            
        }
        public static void UpdateUser(List<User> userList, User user)
        {
            User user_exist = userList.FirstOrDefault(u => u.UserId == user.UserId);

            if (user_exist == null)
            {
                throw new ArgumentException("You passed in an existing username", "");
            }

            foreach (var item in userList.Where(w => w.UserId == user.UserId))
            {
                    item.Username = user.Username;
                    item.FirstName = user.FirstName;
                    item.LastName = user.LastName;
            } 
        }
        public static void DeleteUser(User user)
        {

        }

        public static bool IsUserExist(List<User> userList, User user)
        {
            User user_exist = userList.FirstOrDefault(u => u.UserId == user.UserId);

            if (user_exist != null)
            {
                return false;

            }
            return true;
        }

    }
}