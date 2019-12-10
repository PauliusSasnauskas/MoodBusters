using System.Collections.Generic;

namespace MoodBustersWebAPI.Database
{
    interface IUserRepository
    {
        User GetUser(int id);
        IEnumerable<User> GetAllUsers();
        void Add(User user);
        void Update(User userChanges);
        void Delete(int id);
    }
}
