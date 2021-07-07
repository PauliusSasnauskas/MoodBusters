using MoodBustersWebAPI.Database;
using System.Net;

namespace MoodBustersWebAPI
{
    public class DbTest
    {
        public static void Main(string[] args)
        {
            using (UserService userService = new UserService())
            {
                User stasas = new User
                {
                    Ip = "2",
                    Name = "Stasys"
                };
                userService.Add(stasas);
            }

            using (LogRecordService logService = new LogRecordService())
            {
                logService.Add(new LogRecord { UserId = 1, ByteCount = 20 });
            }
        }
    }
}