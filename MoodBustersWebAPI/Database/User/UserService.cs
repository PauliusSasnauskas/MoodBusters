﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoodBustersWebAPI.Database
{
    public class UserService : IUserRepository, IDisposable
    {
        readonly BistroContext context;

        public UserService()
        {
            context = new BistroContext();
            context.Database.EnsureCreated();
        }

        public void Add(User user)
        {
            context.Users.Add(user);
        }

        public void Delete(int id)
        {
            context.Users.Remove(GetUser(id));
        }

        public IEnumerable<User> GetAllUsers()
        {
            return context.Users.ToList();
        }

        public User GetUser(int id)
        {
            User user = context.Users.Find(id);
            if (user != null)
            {
                return user;
            }
            else throw new Exception("Specified user not found.");
        }

        public User GetUserByIP(string ip)
        {
            if (ip == null) ip = "penki";
            User user = context.Users.Where(u => u.Ip == ip).FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            else
            {
                User newUser = new User { Ip = ip, Name = "Vytas" };
                Add(newUser);
                return newUser;
            }
        }

        public void Update(User userChanges)
        {
            context.Users.Update(userChanges);
        }

        public void Dispose()
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw e;
                //implement exception
            }
            context.Dispose();
        }

    }
}