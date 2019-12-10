using System;
using System.Collections.Generic;
using System.Linq;

namespace MoodBustersWebAPI.Database
{
    public class LogRecordService : ILogRecordRepository, IDisposable
    {
        readonly BistroContext context;

        public LogRecordService() 
        {
            context = new BistroContext();
            context.Database.EnsureCreated();
        }

        public void Add(LogRecord logRecord)
        {
            context.LogRecords.Add(logRecord);
        }

        public void Delete(int id)
        {
            context.LogRecords.Remove(GetLogRecord(id));
        }

        public IEnumerable<LogRecord> GetAllLogRecords()
        {
            return context.LogRecords.ToList();
        }

        public LogRecord GetLogRecord(int id)
        {
            LogRecord logRecord = context.LogRecords.Find(id);
            if (logRecord != null)
            {
                return logRecord;
            }
            else throw new Exception("Specified log record not found.");
        }

        public void Update(LogRecord logRecordChanges)
        {
            context.LogRecords.Update(logRecordChanges);
        }

        public void Dispose()
        {
            context.SaveChanges();
            context.Dispose();
        }
    }
}