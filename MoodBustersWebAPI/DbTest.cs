using MoodBustersWebAPI.Database;
using System.Net;

namespace MoodBustersWebAPI
{
    public class DbTest
    {
        public static void Main(string[] args)
        {
            using (UserService service = new UserService())
            {
                //do whatewer you want ;)
                service.Add(new User { Ip = 2, Name = "Stasys" });
            }
        }
    }
}