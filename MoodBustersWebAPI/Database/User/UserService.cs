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
            context.SaveChanges();
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

        public void Update(User userChanges)
        {
            context.Users.Update(userChanges);
        }

        public void Dispose()
        {
            context.SaveChanges();
            context.Dispose();
        }

    }
}